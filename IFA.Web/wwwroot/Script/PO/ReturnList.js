
var _GridPO;
var pods = [];
var returpods = [];
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
        bindGrid();
        startSpinner('loading..', 0);

   // });
});

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
            console.log(JSON.stringify(pods));
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
        //  alert(id);
        window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
    //printData
 
    $(".printData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        window.open(
            serverUrl + "Reports/WebFormRpt.aspx?type=returpo&id=" + id, "_blank");
    });
}
function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "no_retur", title: "No Retur", width: "120px" },
            { field: "no_po", title: "No PO", width: "120px" },
            { field: "Nama_Supplier", title: "Supplier", width: "240px" },
            { field: "tanggal", title: "Tanggal Retur", width: "80px", template: "#= kendo.toString(kendo.parseDate(tanggal, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: false },
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
                }, title: "Total", width: "110px", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                field: "Action", width: "50px",
                template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_retur#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true' title='Lihat PO'></i></a></center> &nbsp&nbsp <center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_retur#'><i class='glyphicon glyphicon-print' aria-hidden='true' title='Lihat PO'></i></a></center> "
            }
        ],
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
        sortable: true,
        noRecords: true,
        dataBound: function () {
            prepareActionGrid();
        },
        height: 550,
        scrollable: {
            endless: true
        },
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: false,
            numeric: false,
            previousNext: false,
        },
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
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
    $.when(GetRetur(e.data.no_retur)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: returpods,
                schema: {
                    model: {
                        fields: {
                            kd_satuan: { type: "string" },
                            nama_Barang: { type: "string" },
                            kd_satuan: { type: "string" },
                            qty: { type: "number" },
                            retur_total: { type: "number" },
                            harga: { type: "number" },
                        }
                    }
                },
                sort: { field: "no_seq", dir: "asc" },
            },
            scrollable: true,
            sortable: true,
            columns: [
                { field: "nama_Barang", title: "Nama Stok", width: "100px" },
                { field: "kd_satuan", title: "Satuan", width: "25px" },
                { field: "qty", title: "Qty PO", width: "25px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "harga", title: "Harga", width: "40px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "retur_total", title: "Total", width: "40px", format: "{0:#,0.00}", attributes: { class: "text-right " } }

            ]
        });

        startSpinner('loading..', 0);
    });
}

function addnewPO() {
    window.location.href = urlCreate;

}

function searchPO() {
    startSpinner('loading..', 1);
    $('#GridPO').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
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
                        "field": "no_retur",
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

function GetRetur(idDO) {
    return $.ajax({
        url: urlGetRetur + '?no_po=' + idDO,
        success: function (result) {
            returds = [];
            returds = result;
            returpods = [];

            for (var i = 0; i <= returds.detail.length - 1; i++) {
                returpods.push({
                    kd_satuan: returds.detail[i].satuan,
                    harga: returds.detail[i].harga,
                    qty: returds.detail[i].qty,
                    retur_total: returds.detail[i].total,
                    nama_Barang: returds.detail[i].nama_barang,
                })
            }
            //if ($('#GridDetails').hasClass("k-grid")) {
            //    $('#GridDetails').kendoGrid('destroy').empty();
            //}
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}