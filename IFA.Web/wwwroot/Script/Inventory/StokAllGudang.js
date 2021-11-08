var StokGudangds = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 10
};
var _GridStokGudang;
var currentlySelectedItem;
$(document).ready(function () {


    startSpinner('loading..', 1);
    $.when(GetBarang()).done(function () {
        $.when(getPeriode()).done(function () {
            bindGrid();
             startSpinner('loading..', 0);
            
         });
    });
  
    $("#fancy-checkbox-default").change(function () {
        var ds = $("#GridGudangStock").data("kendoGrid").dataSource;
        var status = "";
        if ($('#fancy-checkbox-default').is(":checked") && !$('#fancy-checkbox-primary').is(":checked")) {
            status = "Limit";
            //console.log(status);
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
           // console.log(status);
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
            //console.log("ikilho" + data);
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
        scrollable: true,
        columns: [
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
            
            { field: "nomor", title: "No", width: "10px", hidden: true },
            { field: "bultah", title: "Periode", width: "20px", hidden: true},
            { field: "kd_stok", title: "Kode Barang", width: "30px"},
            { field: "nama_Barang", title: "Nama Barang", width: "100px" },
            { field: "kd_satuan", title: "Satuan", width: "25px" },
            { field: "kairos", title: "Kairos", width: "25px" },
            { field: "kombes_Sidoarjo", title: "Kombes", width: "25px" },
            { field: "lingkar_timur", title: "Lingkar Timur", width: "30px" },
            { field: "bangil", title: "Bangil", width: "25px" },
            { field: "lamongan", title: "Lamongan", width: "25px" }
         
        ],
        selectable: true,
        dataSource: {
            data: StokGudangds,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        nama_Barang: { type: "string" },
                        kd_stok: { type: "string" },
                        kd_satuan: { type: "string" },
                        bangil: { type: "number" },
                        kombes_Sidoarjo: { type: "number" },
                        lamongan: { type: "number" },
                        lingkar_timur: { type: "number" },
                        kairos: { type: "number" }
                       
                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        groupable: true,
        sortable: true,
        toolbar: ["excel"],
        excel: {
            fileName: "ExportAllCbg.xlsx", allPages: true, Filterable: true
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
              
            }
        },
        noRecords: true,
        dataBound: function () {
        prepareActionGrid();
           
        }

    }).data("kendoGrid");

}


function GetData() {
    var urlLink = urlGetData;
    $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            if (_GridStokGudang) {
                $('#GridGudangStock').kendoGrid('destroy').empty();
              
            }
            StokGudangds = result;
            bindGrid();
           
            startSpinner('loading..', 0);
          
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function searchBarang() {
    var kode_barang = "";
    if ($('#barang').val() === "") {
        kode_barang = "";
    }
    else {
        kode_barang = $('#barang').val();
    }
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        Kode_Barang: kode_barang,
        blnthn: $('#periode').val()

    };

    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_GridStokGudang) {
                $('#GridGudangStock').kendoGrid('destroy').empty();
            }
            StokGudangds = result;
            bindGrid();
            startSpinner('loading..', 0);
           // console.log(result);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


   // console.log(kode_barang);
}