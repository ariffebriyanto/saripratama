var Gudangds = [];
Gudangdetailds = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 10
};
var _GridTerima;
$(document).ready(function () {

    $('#divtanggalfrom').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalto').datepicker('setStartDate', minDate);
    });

    $('#divtanggalto').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalfrom').datepicker('setEndDate', minDate);
    });
    $('#tanggalfrom').val(startdateserver);
    $('#tanggalto').val(enddateserver);

    //getData();
    bindGrid();
});

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        id: $("#no_trans").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_GridTerima) {
                $('#GridTerima').kendoGrid('destroy').empty();
            }
            Gudangds = result;
            console.log(result);
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    Gudangdetailds = result;
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

function getDetail(no_transid) {
    var filterdata = {
        id: no_transid,
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
    };
    return $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Gudangdetailds = result;
            startSpinner('loading..', 0);
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
    $(".printData").on("click", function () {
        var id = $(this).data("id");
        printpage(id);

    });
    $(".deleteData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        pembatalan(id);
    });

}
function bindGrid() {
    _GridTerima = $("#GridTerima").kendoGrid({
        columns: [
            { field: "no_trans", title: "No transaksi", width: "80px" },
            { field: "no_ref", title: "No PO", width: "80px" },
            { field: "tgl_trans", title: "Tanggal Trans", width: "50px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: false },
            { field: "nama_Supplier", title: "Nama Supplier", width: "100px" },
            { field: "penyerah", title: "Penyerah", width: "50px" },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "Action", width: "50px", filterable: false, template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center> &nbsp&nbsp <center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center> " }

        ],
        //dataSource: {
        //    data: Gudangds,
        //    schema: {
        //        model: {
        //            id: "no_trans",
        //            fields: {
        //                no_trans: { type: "string" },
        //                tipe_trans: { type: "string" },
        //                tgl_trans: { type: "date" },
        //                nama_Supplier: { type: "string" },
        //                penyerah: { type: "string" },
        //                keterangan: { type: "string" }
        //            }
        //        }
        //    },
        //    pageSize: 50
        //},
        dataSource: {
            transport: {
                read: function (option) {
                    $.ajax({
                        url: urlGetData,
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
        toolbar: ["excel"],
        excel: {
            fileName: "ExportSaldo.xlsx", allPages: true, Filterable: true
        },
       // change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        scrollable: true,
        height: 550,
        scrollable: {
            endless: true
        },

        pageable: {
            refresh: true,
            pageSizes: false,
            numeric: false,
            previousNext: false,
        },
        resizable: true,
        sortable: true,
        dataBound: function () {
            prepareActionGrid();
            //this.expandRow(this.tbody.find("tr.k-master-row").first());
        }

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
                            qty_order: { type: "number", editable: false },
                            qty_in: { type: "number", editable: true },
                            gudang_asal: { type: "string", editable: false },
                            gudang_tujuan: { type: "string", editable: true },
                            lokasi_simpan: { type: "string", editable: true },
                            harga: { type: "number", editable: false },
                            rp_trans: { type: "number", editable: true }
                        }
                    }
                },
               // sort: { field: "no_seq", dir: "asc" },
                //pageSize: 7,
                //filter: { field: "no_trans", operator: "eq", value: e.data.no_trans }
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
             //   { field: "no_seq", title: "No", width: "30px" },
                { field: "nama_Barang", title: "Nama Barang", width: "110px" },
                { field: "kd_satuan", title: "Satuan", width: "80px" },
                { field: "qty_order", title: "Qty PO", width: "110px" },
                { field: "qty_in", title: "Qty IN", width: "110px" },
                { field: "gudang_tujuan", title: "Lokasi Simpan", width: "180px" }
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

function oncboFilterChanged() {
    var ds = $("#GridPO").data("kendoGrid").dataSource;
    var status = $('#status').val();

    if (status) {
        ds.filter([
            {
                "filters": [
                    {
                        "field": "status_po",
                        "operator": "eq",
                        "value": status
                    }
                ]
            }
        ]);
    }
    else {
        $('#GridPO').kendoGrid('destroy').empty();
        bindGrid();
    }
}

function addnewPO() {
    window.location.href = urlCreateQC;

}

function printpage(id) {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + id;
    //wrapperList
    debugger;
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


function searchPO() {
    //var urlLink = urlGetData;
    //startSpinner('loading..', 1);

    //var filterdata = {
    //    no_po: $("#PONo").val(),
    //    DateFrom: $("#tanggalfrom").val(),
    //    DateTo: $("#tanggalto").val(),
    //    status_po: $("#status").val()
    //};
    //$.ajax({
    //    url: urlLink,
    //    type: "POST",
    //    data: filterdata,
    //    success: function (result) {
    //        //console.log(result);
    //        $('#GridPO').kendoGrid('destroy').empty();

    //        pods = result;
    //        bindGrid();
    //        startSpinner('loading..', 0);

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});

    startSpinner('loading..', 1);
    $('#GridTerima').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
}

function oncboFilterChanged() {
    var ds = $("#GridTerima").data("kendoGrid").dataSource;
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
                        "field": "no_qc",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "nama_Supplier",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "no_ref",
                        "operator": "contains",
                        "value": filter
                    },
                ]
            }
        ]);
        startSpinner('loading..', 0);
    }
    else {
        startSpinner('loading..', 0);

        $('#GridTerima').kendoGrid('destroy').empty();
        bindGrid();
    }
}