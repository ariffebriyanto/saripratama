
var _GridDetails;
var returinvds = [];
var detailsds = [];
var kdcust;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    $.when(GetReturInv()).done(function () {
        bindGrid();
        fillNoDO();
        startSpinner('Loading..', 0);
    });
});

function GetReturInv() {
    return $.ajax({
        url: urlGetReturInv,
        success: function (result) {
            returinvds = [];
            returinvds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillNoDO() {
    $("#noDO").empty();
    $("#noDO").append('<option value="" selected disabled>Please select</option>');
    var data = returinvds;

    for (var i = 0; i < data.length; i++) {
        $("#noDO").append('<option value="' + data[i].no_ref2 + '">' + data[i].no_ref2 + '</option>');
    }

    $('#noDO').selectpicker('refresh');
    $('#noDO').selectpicker('render');
}

function onNoDOChanged() {
    startSpinner('loading..', 1);
    var id=$("#noDO").val();
    var found = GetDODetailByInv(id);
    kdcust = found[0].kd_Customer;
    $("#NoInv").val(found[0].no_inv);
    $("#NamaCustomer").val(found[0].nm_cust);
    $("#JenisDO").val(found[0].jenis_sp);
    $("#alamat").val(found[0].alamat);
    //$.when(GetDODetails(found[0].no_inv)).done(function () { // $.when(GetDODetails(found[0].no_inv)).done(function () {
    $.when(GetDODetails(id).done(function () {
        if ($('#GridDetails').hasClass("k-grid")) {
            $('#GridDetails').kendoGrid('destroy').empty();
        }
       bindGrid();
        startSpinner('Loading..', 0);
    }));
}

function GetDODetails(id) {
    return $.ajax({
        url: urlGetDODetails + '?id=' + id,
        success: function (result) {
            detailsds = [];
            detailsds = result;
            console.log(JSON.stringify(detailsds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function GetDODetailByInv(code) {
    return returinvds.filter(
        function (returinvds) { return returinvds.no_ref2 === code; }
    );
}

function bindGrid() {
    _GridDetails = $("#GridDetails").kendoGrid({
        columns: [
            { field: "nama_Barang", title: "Nama Barang", width: "60px" },
            { field: "kd_satuan", title: "Satuan", width: "15px" },
            { field: "harga", title: "Harga", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "potongan", title: "Diskon (Rp)", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "qty", title: "Qty Nota", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "qty_available", title: "available",  width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "qty_tarik", title: "Qty Retur", width: "20px", format: "{0:#,0}", attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, editor: valueValidation },
            { field: "total", title: "Total Penjualan", hidden: true, width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },

            { field: "retur_total", title: "Total", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { command: ["destroy"], title: "Actions", width: "20px" }
        ],
        dataSource: {
            data: detailsds,
            schema: {
                model: {
                    fields: {
                        no_sp: { type: "string", editable: false },
                        kd_Stok: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        qty_nota: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        qty_available: { type: "number", editable: false },
                        qty_tarik: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        harga: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        potongan: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        total: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        qty_total: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        retur_total: { type: "number", validation: { required: true, min: 1, defaultValue: 1 }, editable: false },
                        keterangan: { type: "string" },
                        jenis_sp: { type: "string" },
                        no_inv: { type: "string" },
                        no_jurnal: { type: "string" },
                        nilai_hpp: { type: "number" },
                        kd_Customer: { type: "string" },
                        disc1: { type: "number" },
                        disc2: { type: "number" },
                        disc3: { type: "number" },
                        disc4: { type: "number" },
                        potongan_total: { type: "number" },
                        bonus: { type: "string" },
                        atas_Nama: { type: "string" },
                        //Atas_Nama

                    }
                }
            }
        },
        cancel: function (e) {
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
        },
        save: function (data) {
            setTimeout(function () {
                var ds = $('#GridDetails').data('kendoGrid').dataSource.data();
                detailsds = [];
                for (var i = 0; i <= ds.length - 1; i++) {

                    detailsds.push({
                        no_sp: ds[i].no_sp,
                        kd_Stok: ds[i].kd_Stok,
                        jenis_sp: ds[i].jenis_sp,
                        kd_satuan: ds[i].kd_satuan,
                        nama_Barang: ds[i].nama_Barang,
                        Nama_Satuan: ds[i].Nama_Satuan,
                        qty: ds[i].qty,
                        qty_available: ds[i].qty_available,
                        qty_nota: ds[i].qty_nota,
                        qty_tarik: ds[i].qty_tarik,
                        no_inv: ds[i].no_inv,
                        no_jurnal: ds[i].no_jurnal,
                        harga: ds[i].harga,
                        nilai_hpp: ds[i].nilai_hpp,
                        total: ds[i].qty - ds[i].qty_tarik,//  ds[i].total,
                        kd_Customer: ds[i].kd_Customer,
                        qty_total: ds[i].qty_total,
                        keterangan: ds[i].keterangan,
                        disc1: ds[i].disc1,
                        disc2: ds[i].disc2,
                        disc3: ds[i].disc3,
                        disc4: ds[i].disc4,
                        potongan: ds[i].potongan,
                        potongan_total: ds[i].potongan_total,
                        bonus: ds[i].bonus,
                        retur_total: ds[i].harga * ds[i].qty_tarik,
                        atas_Nama: ds[i].harga * ds[i].atas_Nama

                    });
                }
                $('#GridDetails').kendoGrid('destroy').empty();
                bindGrid();
            });
        },
        noRecords: true,
        editable: true
    }).data("kendoGrid");

}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 0,
        max: options.model.qty_available,
        min: 1
    });
}

function onSaveClicked() {
    SaveData();
}

function SaveData() {
    //$("#NoInv").val(found[0].no_inv);
    //$("#NamaCustomer").val(found[0].nm_cust);
    //$("#JenisDO").val(found[0].jenis_sp);
    //$("#alamat").val(found[0].alamat);
    var savedata = {
        Kd_Customer: kdcust,
        No_ref1: $("#noDO").val(),
        Jenis_Retur: $("#jenisDO").val(),
        //Kd_Customer: $("#Kd_Customer").val(),
        Nama_agen: $("#NamaCustomer").val(),
        //Tgl_retur: $("#tanggal").val(),
    
        details: _GridDetails.dataSource.data().toJSON()
    };
    swal({
        type: 'warning',
        title: 'Are you sure?',
        html: 'You want to submit this data',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);
            $.ajax({
                type: "POST",
                data: savedata,
                url: urlSave2,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {

                        //Swal.fire({
                        //    type: 'success',
                        //    title: 'Success',
                        //    html: "Save Successfully"
                        //});
                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
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