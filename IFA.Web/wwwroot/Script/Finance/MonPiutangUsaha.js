var _gvList;
var _gvDetail;
var _gvPenjualan;
var customerds = [];
var detailds = [];
var penjualands = [];
$(document).ready(function () {

    $('#divtanggalfrom').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalto').datepicker('setStartDate', minDate);
    });

   
    $('#tanggalfrom').val(dateserver);
    startSpinner('Loading..', 1);
    $.when(getCustomer()).done(function () {
        bindGrid();
        fillCboCustomer();
        bindDetailGrid();
        bindGridPenjualan();
        startSpinner('loading..', 0);
    });
    

});

function bindGrid() {
    _gvList = $("#gvList").kendoGrid({
        columns: [
          
            { field: "nomer", title: "Nomor", width: "30px" },
            { field: "nama_customer", title: "Nama Customer", width: "250px" },
            {
                field: "sisa"
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
                }, title: "Sisa Piutang", width: "60px", format: "{0:#,0.00}", attributes: { class: "text-right " }
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
                            tanggal: $("#tanggalfrom").val(),
                            tipe: $('input[name="tipe"]:checked').val(),
                            kd_cust: $("#Kd_Customer").val()
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
        selectable: true,
        noRecords: true,
        height: 350,
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

function searchPO() {
    startSpinner('loading..', 1);
    $('#gvList').kendoGrid('destroy').empty();
    bindGrid();
    detailds = [];
    $('#gvDetail').kendoGrid('destroy').empty();
    bindDetailGrid();
    penjualands = [];
    $('#gvPenjualan').kendoGrid('destroy').empty();
    bindGridPenjualan();

    startSpinner('loading..', 0);
}

function getCustomer() {
    return $.ajax({
        url: urlGetCustomer,
        success: function (result) {
            customerds = [];
            customerds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillCboCustomer() {
    $("#Kd_Customer").empty();
    $("#Kd_Customer").append('<option value="" selected >[Semua Data]</option>');
    var data = customerds;

    for (var i = 0; i < data.length; i++) {
        $("#Kd_Customer").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kd_Customer').selectpicker('refresh');
    $('#Kd_Customer').selectpicker('render');
}

function onChange() {
    var gridRowData = $("#gvList").data("kendoGrid");
    var selectedItem = gridRowData.dataItem(gridRowData.select());
    var kd_cust = selectedItem["kd_cust"];
    startSpinner('Loading..', 1);
    $.ajax({
        url: urlgetDetail + "/?kd_cust=" + kd_cust + "&tanggal=" + $("#tanggalfrom").val(),
        success: function (result) {
            detailds = result;
            $('#gvDetail').kendoGrid('destroy').empty();
            bindDetailGrid();

            penjualands = [];
            $('#gvPenjualan').kendoGrid('destroy').empty();
            bindGridPenjualan();
            startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindDetailGrid() {
    _gvDetail = $("#gvDetail").kendoGrid({
        scrollable: true,
        columns: [
            { field: "no_inv", title: "No Trans" },
            { field: "tgl_inv", title: "Tanggal", template: "#= kendo.toString(kendo.parseDate(tgl_inv, 'yyyy-MM-dd'), 'dd MMMM yyyy') #"},
            { field: "tgl_jth_tempo", title: "Tanggal Jatuh Tempo", template: "#= kendo.toString(kendo.parseDate(tgl_jth_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "no_jurnal", title: "No Jurnal" },
            { field: "tgl_posting", title: "Tanggal Posting", template: "#= kendo.toString(kendo.parseDate(tgl_posting, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "sts_ppn", title: "Status PPN" },
            { field: "keterangan", title: "Keterangan"},
            { field: "jml_tagihan", title: "Jumlah Piutang", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
            { field: "jml_bayar", title: "Jumlah Bayar", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
            { field: "jml_bayar_pending", title: "Jumlah Pending", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
            { field: "jml_akhir", title: "Sisa Piutang", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },

        ],
        dataSource: {
            data: detailds,
            schema: {
                model: {
                    fields: {
                        sts_ppn: { type: "string" },
                        no_inv: { type: "string" },
                        tgl_inv: { type: "date" },
                        no_jurnal: { type: "string" },
                        tgl_posting: { type: "date" },
                        keterangan: { type: "string" },
                        jml_tagihan: { type: "number" },
                        jml_bayar: { type: "number" },
                        jml_akhir: { type: "number" },
                        kd_cust: { type: "string" },
                        jml_bayar_pending: { type: "number" },
                        tgl_jth_tempo: { type: "date" }

                    }
                }
            },
            aggregate: [
                { field: "jml_tagihan", aggregate: "sum" },
                { field: "jml_bayar", aggregate: "sum" },
                { field: "jml_akhir", aggregate: "sum" },
                { field: "jml_bayar_pending", aggregate: "sum" }
            ]
        },
     
        noRecords: true,
        height: 300,
        change: onChangeDetail,
        selectable: true,

    }).data("kendoGrid");

}

function onChangeDetail() {
    var gridRowData = $("#gvDetail").data("kendoGrid");
    var selectedItem = gridRowData.dataItem(gridRowData.select());
    var no_inv = selectedItem["no_inv"];
    startSpinner('Loading..', 1);
    $.ajax({
        url: urlgetPenjualan + "/?no_inv=" + no_inv,
        success: function (result) {
            penjualands = result;
            $('#gvPenjualan').kendoGrid('destroy').empty();
            bindGridPenjualan();
            startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGridPenjualan() {
    _gvPenjualan = $("#gvPenjualan").kendoGrid({
        scrollable: true,
        columns: [
            { field: "no_inv", title: "No Trans" },
            { field: "no_sp", title: "No DO" },
            { field: "kd_stok", title: "Kode Barang" },
            { field: "nama_barang", title: "Nama Barang" },
            { field: "qty", title: "Qty", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "harga", title: "Harga", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "jml_ppn", title: "PPN", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "total", title: "Total", format: "{0:#,0.00}", attributes: { class: "text-right " }, footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
        ],
        dataSource: {
            data: penjualands,
            schema: {
                model: {
                    fields: {
                        no_sp: { type: "string" },
                        no_inv: { type: "string" },
                        kd_stok: { type: "string" },
                        nama_barang: { type: "string" },
                        qty: { type: "number" },
                        harga: { type: "number" },
                        jml_ppn: { type: "number" },
                        total: { type: "number" }
                    }
                }
            },
            aggregate: [
                { field: "total", aggregate: "sum" },
               
            ]
        },

        noRecords: true,
        height: 300,
        //change: onChangeDetail,
        //selectable: true,

    }).data("kendoGrid");
}

function searchByNotrans() {
    startSpinner('loading..', 1);
    $.ajax({
        url: urlgetDetail + "/?no_trans=" + $("#txtNoTrans").val(),
        success: function (result) {
            detailds = result;
            $('#gvDetail').kendoGrid('destroy').empty();
            bindDetailGrid();
            $.ajax({
                url: urlgetPenjualan + "/?no_inv=" + $("#txtNoTrans").val(),
                success: function (result) {
                    penjualands = result;
                    $('#gvPenjualan').kendoGrid('destroy').empty();
                    bindGridPenjualan();
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