var pods = [];
podetailds = [];
var urlAction = "";
var listBarang = [];
var statusds = [];
var optionsGrid = {
    pageSize: 10
};
var _GridPO;
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
    startSpinner('Loading..', 1);

    //$.when(getData()).done(function () {
        //    $.when(getDetail()).done(function () {
        //        //$.when(GetBarang()).done(function () {
        //        //   // fillBarang();
        //        //    $("#barang").kendoDropDownList({
        //        //        dataTextField: "nama_Barang",
        //        //        dataValueField: "kode_Barang",
        //        //        filter: "contains",
        //        //        dataSource: listBarang,
        //        //      //  optionLabel: "ALL",
        //        //        virtual: {
        //        //            valueMapper: function (options) {
        //        //                options.success([options.nama_Barang || 0]);
        //        //            }
        //        //        },

        //        //    }).closest(".k-widget");

        //        //    $("#barang").data("kendoDropDownList").list.width("400px");
        //        //    startSpinner('loading..', 0);
        //        //});
        //        startSpinner('loading..', 0);
    //    });
    statusds = ['ENTRY', 'OPEN', 'CLOSE', 'OUTSTANDING'];
    bindGrid();
    startSpinner('loading..', 0);
       

   // });

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
    $("#barang").append('<option value="" selected >ALL</option>');
    for (var i = 0; i < listBarang.length; i++) {
        $("#barang").append('<option value="' + listBarang[i].kode_Barang + '">' + listBarang[i].nama_Barang + '</option>');
    }
    $('#barang').selectpicker('refresh');
    $('#barang').selectpicker('render');
}

function getData() {

    var urlLink = urlGetData;
    var filterdata = {
        no_po: $("#PONo").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        status_po: $("#status").val()
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

function getDetail(id) {
    var filterdata = {
        no_po: id,
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        status_po: $("#status").val()
    };
    return $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            podetailds = result;

            //bindGrid();
            //prepareActionGrid();
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
    $(".printMemo").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        printMemo(id);

    });

    $(".printAkunting").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        printAkunting(id);
    });
    $(".deleteData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        pembatalanPO(id);
    });
}

function statusfilter(element) {
    element.kendoDropDownList({
        dataSource: statusds,
        optionLabel: "--Select Value--"
    });
}

function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            {
                field: "status_po", title: "Status", width: "60px"
                , attributes: { class: "text-center borderStatus", 'style': "#if(status_po == 'OUTSTANDING'){#background-color: cornflowerblue;color: white;#}else if(status_po == 'BATAL'){#background-color: orangered;color: white;#}else if(status_po == 'CLOSE'){#background-color: green;color: white;#}else if(status_po == 'OPEN'){#background-color: deepskyblue;color: black;#}else if(status_po == 'ENTRY'){#background-color: aquamarine;color: black;#}else if(status_po == 'REVISE'){#background-color: lightpink;color: black;#}#" }
                , filterable: {
                    ui: statusfilter,
                    extra: false,
                    operators: {
                        string: { contains: "Filter" }
                    }
                }
            },
            { field: "no_po", title: "PO Number", width: "100px" },
            { field: "atas_nama", title: "Atas Nama", width: "80px" },
            { field: "kop_surat", title: "Kop Surat", width: "80px" },
            { field: "Nama_Supplier", title: "Supplier", width: "170px" },
            { field: "tgl_po", title: "Tanggal PO", width: "80px", template: "#= kendo.toString(kendo.parseDate(tgl_po, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: false },
            { field: "tgl_kirim", title: "Tanggal Kirim", width: "80px", template: "#= kendo.toString(kendo.parseDate(tgl_kirim, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: false },
            { field: "kd_valuta", title: "Valuta", width: "30px", hidden: true },
            { field: "kurs_valuta", title: "Kurs", width: "30px", hidden: true },
            {
                field: "ongkir"
                , filterable: {
                    extra: true,
                    operators: {
                        string: {
                            gte: "Lebih dari sama dengan",
                            gt: "Lebih dari",
                            lte: "Kurang dari sama dengan",
                            lt: "Kurang dari"
                        }
                    }
                }, title: "Ongkir", width: "60px", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                field: "jml_ppn"
                , filterable: {
                    extra: true,
                    operators: {
                        string: {
                            gte: "Lebih dari sama dengan",
                            gt: "Lebih dari",
                            lte: "Kurang dari sama dengan",
                            lt: "Kurang dari"
                        }
                    }
                }, title: "PPN", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                field: "jml_rp_trans"
                , filterable: {
                    extra: true,
                    operators: {
                        string: {
                            gte: "Lebih dari sama dengan",
                            gt: "Lebih dari",
                            lte: "Kurang dari sama dengan",
                            lt: "Kurang dari"
                        }
                    }
                }, title: "Total", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                field: "total"
                , filterable: {
                    extra: true,
                    operators: {
                        string: {
                            gte: "Lebih dari sama dengan",
                            gt: "Lebih dari",
                            lte: "Kurang dari sama dengan",
                            lt: "Kurang dari"
                        }
                    }
                }, title: "Grand Total (Rp)", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                field: "Action", width: "150px", filterable: false,
                template: "#if(status_po == 'CLOSE'){#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true' title='Lihat PO'></i></a></center> <center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-print' aria-hidden='true' title='Print PO'></i></a>&nbsp&nbsp <a class='btn btn-info btn-sm printMemo' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-list-alt' title='Print Memo' aria-hidden='true'></i></a>&nbsp&nbsp <a class='btn btn-info btn-sm printAkunting' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-usd' title='To Accounting' aria-hidden='true'></i></a> </center> <center style='display:inline;'><a class='btn btn-danger btn-sm deleteData' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-trash' title='Pembatalan PO' aria-hidden='true'></i></a></center> #}" +
                    "else{#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-info btn-sm editData' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-pencil' title='Edit PO' aria-hidden='true'></i></a>&nbsp&nbsp<a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_po#'><i class='glyphicon glyphicon-print' aria-hidden='true' title='Print PO'></i></a></center>#}#"

            }
        ],
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        resizable: true,
        sortable: true,
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
        change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
            prepareActionGrid();
            //  this.expandRow(this.tbody.find("tr.k-master-row"));
        },
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
    $.when(getDetail(e.data.no_po)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: podetailds,
                schema: {
                    model: {
                        id: "no_seq",
                        fields: {
                            no_seq: { type: "number" },
                            nama_barang: { type: "string" },
                            satuan: { type: "string" },
                            qty: { type: "number" },
                            prosen_diskon: { type: "number" },
                            diskon2: { type: "number" },
                            diskon3: { type: "number" },
                            diskon4: { type: "number" },
                            harga: { type: "number" },
                            total: { type: "number" },
                            tgl_kirim: { type: "date" },
                            qty_kirim: { type: "number" },
                            qty_sisa: { type: "number" }
                        }
                    }
                },
                sort: { field: "no_seq", dir: "asc" },
                //  pageSize: 7,
                //  filter: { field: "no_po", operator: "eq", value: e.data.no_po }
            },
            scrollable: true,
            sortable: true,
            //pageable: true,
            columns: [
                { field: "no_seq", title: "No", width: "10px" },
                { field: "nama_barang", title: "Nama Stok", width: "100px" },
                //    { field: "keterangan", title: "Nama PO", width: "110px" },
                { field: "satuan", title: "Satuan", width: "25px" },
                { field: "tgl_kirim", title: "Tanggal Kirim", width: "30px", template: "#= kendo.toString(kendo.parseDate(tgl_kirim, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
                { field: "qty", title: "Qty PO", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "qty_kirim", title: "Qty Kirim", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "qty_sisa", title: "Qty Sisa", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "harga", title: "Harga", width: "35px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "prosen_diskon", title: "Disc %1", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "diskon2", title: "Disc %2", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "diskon3", title: "Disc %3", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "diskon4", title: "Disc Rp.", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },

                { field: "total", title: "Total", width: "40px", format: "{0:#,0.00}", attributes: { class: "text-right " } }
            ]
        });

        startSpinner('loading..', 0);
    });

  
    //adjasjdkasjdksakdj
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
    var filter = $('#filter').val();
    startSpinner('loading..', 1);
    if (status && filter) {
        ds.filter([
            {
                logic: "and",
                "filters": [
                    {
                        "field": "status_po",
                        "operator": "eq",
                        "value": status
                    },
                    {
                        logic: "or",
                        filters: [
                            {
                                "field": "no_po",
                                "operator": "contains",
                                "value": filter
                            },
                            {
                                "field": "nama_Supplier",
                                "operator": "contains",
                                "value": filter
                            },
                            {
                                "field": "stuffbarang",
                                "operator": "contains",
                                "value": filter
                            },
                        ]
                    }
                ]
            }
        ]);
        startSpinner('loading..', 0);

    }
    else if (status) {
        ds.filter([
            {
                logic: "and",
                "filters": [
                    {
                        "field": "status_po",
                        "operator": "eq",
                        "value": status
                    }
                ]
            }
        ]);
        startSpinner('loading..', 0);

    }
    else if (filter) {
        ds.filter([
            {
                logic: "or",
                filters: [
                    {
                        "field": "no_po",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "nama_Supplier",
                        "operator": "contains",
                        "value": filter
                    },
                    {
                        "field": "stuffbarang",
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

function printMemo(id) {
    startSpinner('loading..', 1);
    var urlLink = urlMemo + '?id=' + id;
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

function printAkunting(id) {
    // console.log(id);
    startSpinner('loading..', 1);
    var urlLink = urlAkunting + '?id=' + id;
    //wrapperList
    var wrapper = document.getElementById("wrapperList");

    $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            var MainWindow = window.open('', '', 'height=500,width=800');
            console.log(result);
            // MainWindow.document.write('<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style="margin-bottom: 20px;"><tr ><td style="width: 40%;border: 0px solid #dddddd;" ><h2>IFA Company</h2><p>Jalan Diponego 21</p><p>031-9992190</p><p>Surabaya - Jawa Timur</p></td><td style="width: 20%;border: 0px solid #dddddd;"></td><td style="width: 40%;border: 0px solid #dddddd;"><h2>PURCHASE ORDER</h2><p>PO No: 00002/POM/1/20151207</p><p>03 September 2019</p><p>PO Status: ENTRY</p></td></tr></table> <table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">SUPPLIER</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;">ALAMAT PENGIRIMAN</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SUMBER REJEKI TEKNIK-PENGHELA , UD.</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. DIPONEGORO 21, SURABAYA</td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. RAYA BAMBE KM.19, DRIYOREJO GRESIK</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Telp No: 7590102</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"> </td></tr><tr > <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Jatuh Tempo: 02 Oktober 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">TANGGAL KIRIM</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">REQUESTED BY</th> <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">APPROVED BY</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">20 September 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Ricardo Kaka</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Rui Costa</td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">NOTES</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SEGERA DIKIRIM, STOK HABIS</td></tr></table> <table> <tr class="headerTable"> <th>Barang</th><th>Satuan</th> <th>Qty</th> <th>Harga</th><th>Disc Rp</th> <th>Total</th> </tr><tr> <td>HYDRAULIC CRIMPING TOOL YQK 300</td><td>PCS</td><td style="text-align: right;padding: 2px;">100.00</td><td style="text-align: right;padding: 2px;">10,000,000.00</td><td style="text-align: right;padding: 2px;">288,650,000.00</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">SUBTOTAL</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">ONGKOS KIRIM</td><td style="text-align: right;padding: 2px;">10,000,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">PPN</td><td style="text-align: right;padding: 2px;">71,135,000.00</td></tr><tr> <th colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">GRAND TOTAL (Rp)</th> <th style="text-align: right;padding: 2px;">792,485,000.00</th> </tr></table></body></html>');
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
    //var barang = $('#barang').val();

    //var filterdata = {
    //    no_po: $("#PONo").val(),
    //    DateFrom: $("#tanggalfrom").val(),
    //    DateTo: $("#tanggalto").val(),
    //    status_po: $("#status").val(),
    //    barang: $('#barang').val()
    //};
    //$.ajax({
    //    url: urlLink,
    //    type: "POST",
    //    data: filterdata,
    //    success: function (result) {
    //        //console.log(result);

    //        pods = result;
    //        $('#GridPO').kendoGrid('destroy').empty();

    //        bindGrid();
    //        prepareActionGrid();
    //        startSpinner('loading..', 0);
    //        //$.ajax({
    //        //    url: urlGetDetailData,
    //        //    type: "POST",
    //        //    data: filterdata,
    //        //    success: function (result) {
    //        //        podetailds = result;
    //        //        $('#GridPO').kendoGrid('destroy').empty();

    //        //        bindGrid();
    //        //        prepareActionGrid();
    //        //        startSpinner('loading..', 0);

    //        //    },
    //        //    error: function (data) {
    //        //        alert('Something Went Wrong');
    //        //        startSpinner('loading..', 0);
    //        //    }
    //        //});

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});
    startSpinner('loading..', 1);
    $('#GridPO').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
}

function pembatalanPO(id) {

    var urlLink = urlPembatalanPO + '?id=' + id;

    swal({
        type: 'warning',
        title: 'Anda Yakin',
        html: 'untuk membatalkan PO ini?',
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