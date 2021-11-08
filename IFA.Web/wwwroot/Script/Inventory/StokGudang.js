var StokGudangds = [];
var StokGudangdetailds = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 10
};
var _GridStokGudang;
var currentlySelectedItem;
$(document).ready(function () {


    startSpinner('loading..', 1);
    $.when(getPeriode()).done(function () {
        //$.when(GetBarang()).done(function () {
            startSpinner('loading..', 0);
       // });
    });
    bindGrid();
    bindDetailGrid();

    $("#fancy-checkbox-default").change(function () {
        var ds = $("#GridGudangStock").data("kendoGrid").dataSource;
        var status = "";
        if ($('#fancy-checkbox-default').is(":checked") && !$('#fancy-checkbox-primary').is(":checked")) {
            status = "Limit";
            console.log(status);
        }
        else if ($('#fancy-checkbox-default').is(":checked") && $('#fancy-checkbox-primary').is(":checked")) {
            status = "";
        }
        else if (!$('#fancy-checkbox-default').is(":checked") && $('#fancy-checkbox-primary').is(":checked")) {
            status = "Aman";
        }

        if (status) {
            ds.filter([
                {
                    "filters": [
                        {
                            "field": "status_Stok",
                            "operator": "eq",
                            "value": status
                        }
                    ]
                }
            ]);
        }
        else {
            $('#GridGudangStock').kendoGrid('destroy').empty();
            bindGrid();
        }
    });
    $("#fancy-checkbox-primary").change(function () {
        var ds = $("#GridGudangStock").data("kendoGrid").dataSource;
        var status = "";
        if (!$('#fancy-checkbox-default').is(":checked") && $('#fancy-checkbox-primary').is(":checked")) {
            status = "Aman";
            console.log(status);
        }
        else if ($('#fancy-checkbox-default').is(":checked") && $('#fancy-checkbox-primary').is(":checked")) {
            status = "";
        }
        else if ($('#fancy-checkbox-default').is(":checked") && !$('#fancy-checkbox-primary').is(":checked")) {
            status = "Limit";
        }

        if (status) {
            ds.filter([
                {
                    "filters": [
                        {
                            "field": "status_Stok",
                            "operator": "eq",
                            "value": status
                        }
                    ]
                }
            ]);
        }
        else {
            $('#GridGudangStock').kendoGrid('destroy').empty();
            bindGrid();
        }
    });
        
    //});
});

function GetBarang() {
    return $.ajax({
        url: urlGetBarang,
        type: "POST",
        success: function (result) {
            $("#barang").empty();
            $("#barang").append('<option value="" >Pilih Barang</option>');
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#barang").append('<option value="' + data[i].kode_Barang + '">' + data[i].nama_Barang + '</option>');
            }

            $('#barang').selectpicker('refresh');
            $('#barang').selectpicker('render');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function getPeriode() {
    return $.ajax({
        url: urlGetPeriode,
        type: "POST",
        success: function (result) {
            $("#periode").empty();
            $("#periode").append('<option value="" selected disabled>Please select</option>');
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#periode").append('<option value="' + data[i].thnbln + '">' + data[i].nama + '</option>');
            }


            $('#periode').selectpicker('refresh');
            $('#periode').selectpicker('render');

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

}


function bindGrid() {
    _GridStokGudang = $("#GridGudangStock").kendoGrid({
        columns: [
            { field: "bultah", title: "Periode", width: "20px", hidden: true, attributes: { class: "text-center borderStatus", 'style': "#if(Status_Stok == 'Limit'){#background-color: orangered;color: black;#}else if(Status_Stok == 'Aman'){#background-color: aquamarine ;color: black;#}#" }  },
            { field: "kd_stok", title: "Kode Barang", width: "20px", attributes: { class: "text-center borderStatus", 'style': "#if(Status_Stok == 'Limit'){#background-color: orangered;color: black;#}else if(Status_Stok == 'Aman'){#background-color: aquamarine ;color: black;#}#" } },
            { field: "Nama_Barang", title: "Nama Barang", width: "80px", attributes: { class: "text-left borderStatus", 'style': "#if(Status_Stok == 'Limit'){#background-color: orangered;color: black;#}else if(Status_Stok == 'Aman'){#background-color: aquamarine ;color: black;#}#" }  },
            { field: "Kd_Satuan", title: "Satuan", width: "25px", attributes: { class: "text-center borderStatus", 'style': "#if(Status_Stok == 'Limit'){#background-color: orangered;color: black;#}else if(Status_Stok == 'Aman'){#background-color: aquamarine ;color: black;#}#" } },
            { field: "kategori", title: "Kategori", width: "40px", hidden: true },
            { field: "sub_kategori", title: "Sub Kat", width: "25px", hidden: true },
            { field: "Nama_Merk", title: "Merk", width: "30px", hidden: true },
            {
                field: "stok_min", title: "Min Stock", width: "25px", attributes: { class: "text-center borderStatus", 'style': "#if(Status_Stok == 'Limit'){#background-color: orangered;color: black;#}else if(Status_Stok == 'Aman'){#background-color: aquamarine ;color: black;#}#" }
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
                }
            },
            {
                field: "awal_qty_onstok", title: "Awal Stock", width: "25px"
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
                }
            },
            {
                field: "akhir_qty_onstok", title: "Qty Fisik", width: "25px"
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
                }
            },
            {
                field: "qty_onstok_out", title: "Qty Out", width: "25px"
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
                }
            },
            {
                field: "qty_onstok_in", title: "Qty In", width: "25px"
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
                }
            },
            {
                field: "akhir_booked", title: "Booked", width: "25px"
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
                }
            },
            //{
            //    field: "qty_akhir_expedisi", title: "on Expedisi", width: "25px"
            //    , filterable: {
            //        extra: true,
            //        operators: {
            //            string: {
            //                gte: "Lebih dari sama dengan",
            //                gt: "Lebih dari",
            //                lte: "Kurang dari sama dengan",
            //                lt: "Kurang dari"
            //            }
            //        }
            //    }
            //},
            {
                field: "qty_available", title: "Available", width: "25px"
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
                }
            },
            { field: "bultah", title: "Bulan Tahun", hidden: true },
            { field: "Status_Stok", title: "Status Stok", width: "30px", hidden: true   }

        ],
        selectable: true,
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
                            blnthn: $('#periode').val(),
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
                schema: {
                model: {
                    fields: {
                        nama_Barang: { type: "string" },
                        kd_stok: { type: "string" },
                        kd_Satuan: { type: "string" },
                        nama_Merk: { type: "string" },
                        kategori: { type: "string" },
                        sub_kategori: { type: "string" },
                        stok_min: { type: "number" },
                        awal_qty_onstok: { type: "number" },
                        qty_onstok_in: { type: "number" },
                        qty_onstok_out: { type: "number" },
                        akhir_booked: { type: "number" },
                        akhir_qty_onstok: { type: "number" },
                        qty_available: { type: "number" },
                        bultah: { type: "string" },
                        status_Stok: { type: "string" }
                    }
                }
            },
            },
            pageSize: 100,
        },
        filterable: {
            extra: true,
            operators: {
                string: { contains: "Contains" }
            }
        },
        groupable: true,
        sortable: true,
        toolbar: ["excel"],
        excel: {
            fileName: "ExportSaldo.xlsx", allPages: true, Filterable: true
        },
        change: onChange,
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
        resizable: true,
        sortable: true,
        dataBound: function () {
        prepareActionGrid();
           
        }

    }).data("kendoGrid");

}

function bindDetailGrid() {
    _GridDetailStokGudang = $("#GridDetailGudangStock").kendoGrid({
        scrollable: true,
        columns: [
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
            { field: "nama_Gudang", title: "Nama Gudang", width: "80px" },
            { field: "bultah", title: "Periode", width: "80px" },
            { field: "nama_Barang", title: "Nama Barang", width: "150px" },
            { field: "awal_qty", title: "Qty Awal", width: "30px" },
            { field: "qty_in", title: "Masuk", width: "50px" },
            { field: "qty_out", title: "Keluar", width: "50px" },
            { field: "akhir_qty", title: "Qty Akhir", width: "30px"  },
            { field: "kd_Satuan", title: "Satuan", width: "30px" }
            //{ field: "nama_Merk", title: "Merk", width: "30px" },
            //{ field: "nama_Tipe", title: "Tipe", width: "50px" }
            //{ field: "Action", width: "50px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center><center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center>" }

        ],
        selectable: true,
        dataSource: {
            data: StokGudangdetailds,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        nama_Barang: { type: "string" },
                      
                        nama_Gudang: { type: "string" },
                        kd_stok: { type: "string" },
                        kd_Satuan: { type: "string" },
                        //nama_Merk: { type: "string" },
                        //nama_Tipe: { type: "string" },
                        stok_min: { type: "number" },
                        awal_qty: { type: "number" },
                        akhir_qty: { type: "number" },
                        qty_onstok_in: { type: "number" },
                        qty_onstok_out: { type: "number" },
                        akhir_booked: { type: "number" },
                        bultah: { type: "string" },
                        qty_in: { type: "number" },
                        qty_out: { type: "number" }
         
                    }
                }
            },
           // pageSize: optionsGrid.pageSize
        },
        //pageable: {
        //    pageSizes: [5, 10, 20, 100],
        //    change: function () {

        //    }
        //},
        noRecords: true,
        dataBound: function () {
            prepareActionGrid();

        }

    }).data("kendoGrid");

}

function onChange(args) {
   
    var gridRowData = $("#GridGudangStock").data("kendoGrid");
    var selectedItem = gridRowData.dataItem(gridRowData.select());
    var quote = selectedItem["kd_stok"];
    var blthn = selectedItem["bultah"];
    var urlLink = urlGetDetailData;
    var filterdata = {
        Kode_Barang: quote,
        blnthn : blthn
    };
    startSpinner('Loading..', 1);
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
       
        success: function (result) {
            if (_GridDetailStokGudang) {
                $('#GridDetailGudangStock').kendoGrid('destroy').empty();
            }
            StokGudangdetailds = result;
            bindDetailGrid();
            startSpinner('loading..', 0);
            console.log(result);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

    //var asu = $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_in.sum;
   // console.log(quote + blthn);
}

function searchBarang() {
    StokGudangdetailds = []

    startSpinner('Loading..', 1);

    if (_GridStokGudang) {
        $('#GridGudangStock').kendoGrid('destroy').empty();
        $('#GridDetailGudangStock').kendoGrid('destroy').empty();
    }

    bindGrid();
    bindDetailGrid();
    startSpinner('loading..', 0);
}

function oncboFilterChanged() {
    var ds = $("#GridGudangStock").data("kendoGrid").dataSource;
    var status = "";
    if ($('#checkbox2').checked) {
        status: "Limit";
    }
    console.log(status);
    if (status) {
        ds.filter([
            {
                "filters": [
                    {
                        "field": "status_Stok",
                        "operator": "eq",
                        "value": status
                    }
                ]
            }
        ]);
    }
    else {
        $('#GridGudangStock').kendoGrid('destroy').empty();
        bindGrid();
    }
}
