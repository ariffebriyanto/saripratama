var paketDS = [];

var columnGrid = [
    { field: "no_Paket", title: "Nomor Paket", width: "25px" },
    { field: "nama_Paket", title: "Nama Paket", width: "100px" },
    { field: "tgl_Paketdesc", title: "Tanggal Paket", width: "50px" },
    { field: "tgl_Akhir_Paketdesc", title: "Tanggal Akhir", width: "50px" },
    { field: "total_qty", title: "Total Qty", width: "25px", format: "{0:#,00}", attributes: { class: "text-right " }},
    { field: "harga_Paket", title: "Harga Paket", width: "50px", format: "{0:#,00}", attributes: { class: "text-right " } },
    { field: "Action", width: "20px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_Paket#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center> "}

];
var optionsGrid = {
    pageSize: 10
};

$(document).ready(function () {
    startSpinner('Loading..', 1);

    $.when(getDataPaket()).done(function () {
        bindGrid();
        startSpinner('Loading..', 0);
    });
});

function getDataPaket() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            paketDS = result;
            console.log(JSON.stringify(paketDS));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    $("#GridPaket").kendoGrid({
        columns: columnGrid,
        dataSource: {
            data: paketDS,
            schema: {
                model: {
                    fields: {
                        no_Paket: { type: "string" },
                        nama_Paket: { type: "string" },
                        tgl_Paketdesc: { type: "string" },
                        tgl_Akhir_Paketdesc: { type: "string" },
                        total_qty: { type: "number" },
                        harga_Paket: { type: "number" },
                    }
                }
            },
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        dataBound: function () {
            prepareActionGrid();
        },
        noRecords: true,
       
    }).data("kendoGrid");

}

function prepareActionGrid() {
    $(".viewData").on("click", function () {
        var id = $(this).data("id");
        window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
    
}