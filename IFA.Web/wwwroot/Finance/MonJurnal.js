var optionsGrid = {
    pageSize: 20
};
var listds = [];
var detailds = [];
var _gvList;
var selectRequest;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    $.when(getData()).done(function () {
        bindGrid();
        startSpinner('loading..', 0);
    });
});

function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            listds = [];
            listds = result;
            //  //console.log(JSON.stringify(listds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}
function getDetail(id) {
    var urlLink = urlGetDetail + "/?nojur=" + id;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            detailds = [];
            detailds = result;
            //console.log(JSON.stringify(detailds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function bindGrid() {
    _gvList = $("#gvList").kendoGrid({

        columns: [
            { selectable: true, width: "40px", headerAttributes: { style: "text-align: center;" } },
            //{ field: "IsSelected", title: "<input type='checkbox' id='chkSelectAll'>", width: "34px", template: "<input  type='checkbox' #= IsSelected ? checked='checked' : '' #/>" }, 
            { field: "cabang", title: "Cabang", width: "120px" },
            { field: "no_jur", title: "No. Jurnal", width: "140px" },
            { field: "tgl_trans", title: "Tanggal", width: "100px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: true },
            { field: "no_ref1", title: "No. Ref", width: "140px" },
            { field: "tipe_desc", title: "Tipe Jurnal", width: "170px" },
            { field: "nama", title: "Kepada", width: "220px" },
            { field: "jml_rp_trans", title: "Nominal", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "keterangan", width: "170px" },
        ],
        dataSource: {
            data: listds,
            schema: {
                model: {
                    id: "no_jur",
                    fields: {
                        no_jur: { type: "string" },
                        no_ref1: { type: "string" },
                        keterangan: { type: "string" },
                        nama: { type: "string" },
                        cabang: { type: "string" },
                        tipe_desc: { type: "string" },
                        tgl_trans: { type: "date" },
                        jml_rp_trans: { type: "number" }

                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true,
        dataBound: function () {
            prepareActionGrid();
        },
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        change: onChange,
    }).data("kendoGrid");

}
function detailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });
    startSpinner('loading..', 1);
    $.when(getDetail(e.data.id)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: detailds,
                schema: {
                    model: {
                        id: "no_seq",
                        fields: {
                            no_seq: { type: "string" },
                            rekening: { type: "string" },
                            barang: { type: "string" },
                            val_ref1: { type: "number" },
                            harga: { type: "number" },
                            saldo_val_debet: { type: "number" },
                            saldo_val_kredit: { type: "number" },
                            saldo_rp_debet: { type: "number" },
                            saldo_rp_kredit: { type: "number" },
                            keterangan: { type: "string" },
                        }
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "no_seq", title: "No", width: "40px" },
                { field: "rekening", title: "Rekening", width: "140px" },
                { field: "barang", title: "Barang", width: "180px" },
                { field: "val_ref1", title: "Qty", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "harga", width: "70px", title: "Harga", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_val_debet", width: "70px", title: "Saldo Val. Debet", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_val_kredit", width: "70px", title: "Saldo Val. Kredit", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_debet", width: "70px", title: "Saldo Rp. Debet", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_kredit", width: "70px", title: "Saldo Rp. Kredit", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "Keterangan", width: "180px" },
            ],
        });
        startSpinner('loading..', 0);
    });
}

function prepareActionGrid() {

}

function onChange(arg) {
    selectRequest = this.selectedKeyNames().join(";");
}

function onPosting() {
    if (!selectRequest) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: 'Pilih Jurnal'
        });
    }
    else {
        swal({
            type: 'warning',
            title: 'Anda Yakin?',
            html: 'Posting Jurnal',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            if (isConfirm.value === true) {
                startSpinner('loading..', 1);
                $.ajax({
                    url: urlPosting + "/?nojur=" + selectRequest,
                    success: function (result) {
                        if (result.success === false) {
                            Swal.fire({
                                type: 'error',
                                title: 'Warning',
                                html: result.message
                            });
                            startSpinner('loading..', 0);
                        } else {

                            $.when(getData()).done(function () {
                                if ($('#gvList').hasClass("k-grid")) {
                                    $('#gvList').kendoGrid('destroy').empty();
                                }
                                bindGrid();
                                startSpinner('loading..', 0);
                            });
                        }
                    },
                    error: function (data) {
                        alert('Something Went Wrong');
                        startSpinner('loading..', 0);
                    }
                });

            }
        });
    }
}