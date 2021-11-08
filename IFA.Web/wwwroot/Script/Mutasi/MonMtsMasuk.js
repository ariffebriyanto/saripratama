var pods = [];
var Gudangdetailds = [];
var Gudangds = [];
var urlAction = "";
var listBarang = [];
var optionsGrid = {
    pageSize: 10
};
var _GridPO;
$(document).ready(function () {

    $('#divtanggalfrom').datepicker({
        format: 'dd/MM/yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalto').datepicker('setStartDate', minDate);
    });

    $('#divtanggalto').datepicker({
        format: 'dd/MM/yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalfrom').datepicker('setEndDate', minDate);
    });
    $('#tanggalfrom').val(startdateserver);
    $('#tanggalto').val(enddateserver);
    startSpinner('Loading..', 1);

    bindGrid();
    startSpinner('loading..', 0);
});

function GetBarang() {
    return $.ajax({
        url: urlGetBarang,
        type: "POST",
        success: function (result) {

            listBarang = result;

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillBarang() {
    $("#barang").empty();
    $("#PONumber").append('<option value="" selected disabled>Please select</option>');
    for (var i = 0; i < listBarang.length; i++) {
        $("#barang").append('<option value="' + listBarang[i].kode_Barang + '">' + listBarang[i].nama_Barang + '</option>');
    }
    $('#barang').selectpicker('refresh');
    $('#barang').selectpicker('render');
}

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetHeader;
    var filterdata = {
        id: $("#no_trans").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        barang: $("#barang").val(),
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_GridPO) {
                $('#GridPO').kendoGrid('destroy').empty();
            }
            Gudangds = result;
            //console.log(result);
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    Gudangdetailds = result;
                    //console.log(Gudangdetailds);
                    bindGrid();
                    prepareActionGrid();
                    startSpinner('loading..', 0);

                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });


        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function getDetail(trans_no) {
    var filterdata = {
        id: trans_no,
        DateFrom: $("#tanggalfrom").val(), //startdateserver
        DateTo: $("#tanggalto").val(), //enddateserver
        //status_po: $("#status").val()
    };
    return $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Gudangdetailds=[];
            Gudangdetailds = result;
            
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function prepareActionGrid() {
    $(".viewData").on("click", function () {
        var id = $(this).data("id");
        window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
    $(".editData").on("click", function () {
        var id = $(this).data("id");

        window.location.href = urlCreate + '?id=' + id + '&mode=EDIT';
    });
    $(".printData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        window.open(
            serverUrl + "Reports/WebFormRpt.aspx?type=mutasimasukcabang&id=" + id, "_blank");

    });

    $(".deleteData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        pembatalan(id);
    });
}
function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "no_trans", title: "No transaksi", width: "100px" },
            { field: "no_ref", title: "No Ref", width: "100px" },
            { field: "tgl_trans", title: "Tanggal Mutasi", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "Nama_Gudang", title: "Gudang", width: "120px" },
            { field: "penyerah", title: "penyerah", width: "100px" },
            { field: "keterangan", title: "Keterangan", width: "100px" },
            { field: "Action", width: "100px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center> &nbsp&nbsp <center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center> " }
        ],
        dataSource: {
            transport: {
                read: function (option) {
                    $.ajax({
                        url: urlGetHeader,
                        data:
                        {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            DateFrom: $("#tanggalfrom").val(),
                            DateTo: $("#tanggalto").val(),
                            barang: $("#barang").val()
                        },
                        dataType: 'json',
                        success: function (result) {
                            option.success(result);
                        },
                        error: function (result) {
                            alert("error");

                        }
                    });
                }
            },
            serverFiltering: true,
            serverSorting: true,
            serverPaging: true,

            schema: {
                data: "data",
                total: "total",
            },
            pageSize: 100,
        },
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        resizable: true,
        sortable: true,
        change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
            prepareActionGrid();
            //this.expandRow(this.tbody.find("tr.k-master-row"));
        },
        scrollable: true,
        height: 650,
        scrollable: {
            endless: true
        },
        pageable: {
            refresh: true,
            pageSizes: false,
            numeric: false,
            previousNext: false,
        },
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

    $.when(getDetail(e.data.no_trans)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: Gudangdetailds,
                schema: {
                    model: {
                        id: "no_seq",
                        fields: {
                            no_seq: { type: "string", editable: false },
                            kd_stok: { type: "string", editable: false },
                            nama_Barang: { type: "string", editable: false },
                            kd_satuan: { type: "string", editable: false },
                            // qty_out: { type: "number", editable: false },
                            qty_in: { type: "number", editable: true },
                            keterangan: { type: "string", editable: false },
                            lokasi_simpan: { type: "string", editable: false },
                            nama_Gudang: { type: "string", editable: false }

                        }
                    }
                },
            },
            columns: [
                { field: "nama_Gudang", title: "Gudang Asal", width: "110px" },
                { field: "lokasi_simpan", title: "Gudang Tujuan", width: "110px" },
                { field: "nama_Barang", title: "Nama Barang", width: "110px" },
                { field: "kd_satuan", title: "Satuan", width: "80px" },
                { field: "qty_in", title: "Jumlah Mutasi", width: "110px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "keterangan", width: "110px" }

            ]
        });
        startSpinner('loading..', 0);
    });
   
}
function onChange(arg) {
    var key = this.selectedKeyNames().join(", ");
    if (key) {
        $('#btnPrint').show();
    }
    else {
        $('#btnPrint').hide();

    }
}

function addnewPO() {
    window.location.href = urlCreate;

}

function printpage(id) {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + id;
    //wrapperList
    var wrapper = document.getElementById("wrapperList");

    $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            var MainWindow = window.open('', '', 'height=500,width=800');

            //  MainWindow.document.write('<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style="margin-bottom: 20px;"><tr ><td style="width: 40%;border: 0px solid #dddddd;" ><h2>IFA Company</h2><p>Jalan Diponego 21</p><p>031-9992190</p><p>Surabaya - Jawa Timur</p></td><td style="width: 20%;border: 0px solid #dddddd;"></td><td style="width: 40%;border: 0px solid #dddddd;"><h2>PURCHASE ORDER</h2><p>PO No: 00002/POM/1/20151207</p><p>03 September 2019</p><p>PO Status: ENTRY</p></td></tr></table> <table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">SUPPLIER</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;">ALAMAT PENGIRIMAN</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SUMBER REJEKI TEKNIK-PENGHELA , UD.</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. DIPONEGORO 21, SURABAYA</td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. RAYA BAMBE KM.19, DRIYOREJO GRESIK</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Telp No: 7590102</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"> </td></tr><tr > <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Jatuh Tempo: 02 Oktober 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">TANGGAL KIRIM</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">REQUESTED BY</th> <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">APPROVED BY</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">20 September 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Ricardo Kaka</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Rui Costa</td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">NOTES</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SEGERA DIKIRIM, STOK HABIS</td></tr></table> <table> <tr class="headerTable"> <th>Barang</th><th>Satuan</th> <th>Qty</th> <th>Harga</th><th>Disc Rp</th> <th>Total</th> </tr><tr> <td>HYDRAULIC CRIMPING TOOL YQK 300</td><td>PCS</td><td style="text-align: right;padding: 2px;">100.00</td><td style="text-align: right;padding: 2px;">10,000,000.00</td><td style="text-align: right;padding: 2px;">288,650,000.00</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">SUBTOTAL</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">ONGKOS KIRIM</td><td style="text-align: right;padding: 2px;">10,000,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">PPN</td><td style="text-align: right;padding: 2px;">71,135,000.00</td></tr><tr> <th colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">GRAND TOTAL (Rp)</th> <th style="text-align: right;padding: 2px;">792,485,000.00</th> </tr></table></body></html>');
            MainWindow.document.write(result);
            MainWindow.document.close();
            setTimeout(function () {
                MainWindow.print();
            }, 500);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


    return false;

}

function pembatalan(id) {

    var urlLink = urlPembatalan + '?id=' + id;

    swal({
        type: 'warning',
        title: 'Anda Yakin',
        html: 'Mutasi Brg Masuk akan di batalkan, stok di keluarkan?',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);

            $.ajax({
                url: urlLink,
                type: "POST",
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        $.when(getData()).done(function () {
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
    return false;

}

function oncboFilterChanged() {
    var ds = $("#GridPO").data("kendoGrid").dataSource;
    var filter = $('#filter').val();
    startSpinner('loading..', 1);
    if (filter) {
        ds.filter([
            {
                logic: "or",
                filters: [
                    {
                        "field": "no_trans",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "no_ref",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "nama_Gudang",
                        "operator": "contains",
                        "value": filter
                    },
                    //lokasi_simpan
                ]
            }
        ]);
        startSpinner('loading..', 0);
    }
    else {
        startSpinner('loading..', 0);

        $('#GridPO').kendoGrid('destroy').empty();
        bindGrid();
    }
}

function searchData() {
    startSpinner('loading..', 1);
    $('#GridPO').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
}