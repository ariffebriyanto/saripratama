var pods = [];
podetailds = [];
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

    //$.when(getData()).done(function () {
        $.when(getDetail()).done(function () {
            //$.when(GetBarang()).done(function () {
            //    //fillBarang();
            //    $("#barang").kendoDropDownList({
            //        dataTextField: "nama_Barang",
            //        dataValueField: "kode_Barang",
            //        filter: "contains",
            //        dataSource: listBarang,
            //        optionLabel: "ALL",
            //        virtual: {
            //            valueMapper: function (options) {
            //                options.success([options.nama_Barang || 0]);
            //            }
            //        },

            //    }).closest(".k-widget");

            //    $("#barang").data("kendoDropDownList").list.width("400px");
            //    startSpinner('loading..', 0);
            //   // $("#barang").val('ALL').change();
            //    $("#barang").data("kendoDropDownList").select(0);
            //});
            startSpinner('loading..', 0);

        });
    //});
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

    var urlLink = urlGetData;
    var filterdata = {
        no_po: $("#NoTrans").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_GridPO) {
                $('#GridPO').kendoGrid('destroy').empty();
            }
            pods = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function getDetail() {
    var filterdata = {
        no_trans: $("#NoTrans").val(),
        DateFrom: $("#tanggalfrom").val(), //startdateserver
        DateTo: $("#tanggalto").val(), //enddateserver
        
    };
    return $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            pods = result;
            bindGrid();
            prepareActionGrid();
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
        printpage(id);

    });

}
function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
          
          
           // { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
            { field: "doc_status", title: "Status", width: "80px", attributes: { class: "text-center borderStatus", 'style': "#if(doc_status == 'QC'){#background-color: cornflowerblue;color: white;#}else if(doc_status == 'BATAL'){#background-color: orangered;color: white;#}else if(doc_status == 'Stocked'){#background-color: green;color: white;#}else if(doc_status == 'OPEN'){#background-color: deepskyblue;color: black;#}else if(doc_status == 'ENTRY'){#background-color: aquamarine;color: black;#}else if(doc_status == 'REVISE'){#background-color: lightpink;color: black;#}#" } },
            { field: "no_trans", title: "No QC", width: "120px" },
            { field: "no_ref", title: "No PO", width: "120px" },
            { field: "last_Create_Date", title: "Tanggal QC", width: "100px", template: "#= kendo.toString(kendo.parseDate(last_Create_Date, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "nama_barang", title: "Nama Barang", width: "200px" },
            { field: "kd_satuan", title: "Satuan", width: "50px" },
            { field: "qty", title: "Qty Order", width: "60px", attributes: { class: "text-right " } },
            { field: "qty_qc_pass", title: "Qty Good", width: "60px", attributes: { class: "text-right " }},
            //{ field: "qty_sisa", title: "Qty Sisa", width: "110px", attributes: { class: "text-right " } },
            { field: "nama_Gudang", title: "Lokasi Simpan", width: "90px" },
            { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
            { field: "nama_Supplier", title: "Supplier", width: "180px" },
            { field: "Action", width: "100px", template: "#if(doc_status != 'Stocked'){#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>#}else{#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>#}#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center>" }

        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        no: { type: "string", editable: false },
                        no_trans: { type: "string", editable: false },
                        nama_barang: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        qty: { type: "string", editable: false },
                        qty_qc_pass: { type: "string", editable: true },
                        //qty_sisa: { type: "string", editable: true },
                        lokasi: { type: "string", editable: true },
                        harga: { type: "number", editable: false },
                        kd_supplier: { type: "string", editable: false },
                        rp_trans: { type: "number", editable: true },
                        nama_Supplier: { type: "string", editable: false },
                        nama_Gudang: { type: "string", editable: true } ,
                            nama_Gudang: { type: "string", editable: true },
                        last_Create_Date: { type: "date", editable: true },
                        no_ref: { type: "string", editable: true }
                    }
                }
            },
            pageSize: 50
        },
        //pageable: {
        //    pageSizes: [5, 10, 20, 100],
        //    change: function () {

        //    }
        //},
        change: onChange,
        noRecords: true,
      //  detailTemplate: kendo.template($("#template").html()),
        //detailInit: detailInit,
        dataBound: function () {
            prepareActionGrid();
          //  this.expandRow(this.tbody.find("tr.k-master-row"));
        },
       // height: 350,
        scrollable: true,
        height: 550,
        scrollable: {
            virtual: true
        },
        resizable: true,

    }).data("kendoGrid");

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

function searchData() {
    //var urlLink = urlGetData;
    startSpinner('loading..', 1);
    var barang = $('#barang').val();

    var filterdata = {
        barang: $('#barang').val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val()
        
       
    };
    //$.ajax({
    //    url: urlLink,
    //    type: "POST",
    //    data: filterdata,
    //    success: function (result) {
    //        //console.log(result);

    //        pods = result;
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    pods = result;
                    $('#GridPO').kendoGrid('destroy').empty();

                    console.log(result);

                    bindGrid();
                    prepareActionGrid();
                    startSpinner('loading..', 0);

                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });

        //},
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});
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
                        "field": "nama_barang",
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

        $('#GridPO').kendoGrid('destroy').empty();
        bindGrid();
    }
}