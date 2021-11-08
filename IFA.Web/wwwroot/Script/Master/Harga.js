var Hargads = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 15
};
$(document).ready(function () {

    $("#files").kendoUpload({
        async: {
            saveUrl: urlSaveUpload,
            autoUpload: false
        }
    });



    startSpinner('Loading..', 1);
    $.when(getData()).done(function () {
        bindGrid();
        startSpinner('loading..', 0);
    });
});

function uploadSelected() {
    $(".k-upload-selected").click();
}


function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            Hargads = [];
            Hargads = result;
            console.log(Hargads);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    $("#GridKota").kendoGrid({
        columns: [

            { field: "kode_Barang", title: "Kode Barang", width: "100px",filterable: false, },
            { field: "nama_Barang", title: "Nama Barang", width: "300px" },
            { field: "harga_Rupiah", title: "Harga 1", width: "100px", filterable: false, },
            { field: "harga_Rupiah2", title: "Harga 2", width: "160px", filterable: false, },
            { field: "harga_Rupiah3", title: "Harga 3", width: "100px", filterable: false, },
            { field: "harga_Rupiah4", title: "Harga 4", width: "100px", filterable: false, },
            { field: "qty_harga1_min", title: "Min Qty Harga 1", width: "100px", filterable: false, },
            { field: "qty_harga2_min", title: "Min Qty Harga 2", width: "100px", filterable: false, },
            { field: "qty_harga3_min", title: "Min Qty Harga 3", width: "100px", filterable: false, },
            { field: "harga_RupiahOld", title: "Copy Harga 1", width: "100px",hidden:true},
            { field: "harga_RupiahOld2", title: "Copy Harga 1", width: "100px",hidden: true },
            { field: "harga_RupiahOld3", title: "Copy Harga 1", width: "100px",hidden: true },
            { field: "selisih", title: "selisih", width: "100px",hidden: true },
            { field: "selisih2", title: "selisih2", width: "100px",hidden: true },
            { field: "selisih3", title: "selisih2", width: "100px", hidden: true },
            { field: "updateStatus", title: "selisih2", width: "100px", hidden: true },

        
        ],
        dataSource: {
            data: Hargads,
            schema: {
                model: {
                    id: "Kode_Barang",
                    fields: {
                        kode_Barang: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        harga_Rupiah: { type: "number", editable: true },
                        harga_Rupiah2: { type: "number", editable: true },
                        harga_Rupiah3: { type: "number", editable: true },
                        harga_Rupiah4: { type: "number", editable: true },
                        qty_harga1_min: { type: "number", editable: true },
                        qty_harga2_min: { type: "number", editable: true },
                        qty_harga3_min: { type: "number", editable: true },
                        selisih: { type: "string", editable: true },
                        selisih2: { type: "string", editable: true },
                        selisih3: { type: "string", editable: true },
                        selisih4: { type: "string", editable: true },
                        harga_RupiahOld: { type: "number", editable: true },
                        harga_RupiahOld2: { type: "number", editable: true },
                        harga_RupiahOld3: { type: "number", editable: true },
                        harga_RupiahOld4: { type: "number", editable: true },
                        updateStatus: { type: "string", editable: true }
                      
                    }
                }
            },
            change: function (e) {
                if (e.field === "harga_Rupiah") {
                    // The total is Prix (price) * Quantite (Quantity)
                    var newTotal = e.items[0].harga_Rupiah - e.items[0].harga_RupiahOld;
                    var Status = "";
                    if (newTotal > 0) {
                        Status = "Harga Naik Rp " + newTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    else {
                        Status = "Harga Turun Rp " + Math.abs(newTotal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    console.log("Nouveau total : " + newTotal);
                    e.items[0].set("selisih", Status);
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "harga_Rupiah2") {
                    // The total is Prix (price) * Quantite (Quantity)
                    var newTotal = e.items[0].harga_Rupiah2 - e.items[0].harga_RupiahOld2;
                    var Status = "";
                    if (newTotal > 0) {
                        Status = "Harga Naik Rp " + newTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    else {
                        Status = "Harga Turun Rp " + Math.abs(newTotal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    console.log("Nouveau total : " + newTotal);
                    e.items[0].set("selisih2", Status);
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "harga_Rupiah3") {
                    // The total is Prix (price) * Quantite (Quantity)
                    var newTotal = e.items[0].harga_Rupiah3 - e.items[0].harga_RupiahOld3;
                    var Status = "";
                    if (newTotal > 0) {
                        Status = "Harga Naik Rp " + newTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    else {
                        Status = "Harga Turun Rp " + Math.abs(newTotal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    console.log("Nouveau total : " + newTotal);
                    e.items[0].set("selisih3", Status);
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "harga_Rupiah4") {
                    // The total is Prix (price) * Quantite (Quantity)
                    var newTotal = e.items[0].harga_Rupiah4 - e.items[0].harga_RupiahOld4;
                    var Status = "";
                    if (newTotal > 0) {
                        Status = "Harga Naik Rp " + newTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    else {
                        Status = "Harga Turun Rp " + Math.abs(newTotal).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    }
                    console.log("Nouveau total : " + newTotal);
                    e.items[0].set("selisih4", Status);
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "qty_harga1_min") {
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "qty_harga2_min") {
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "qty_harga3_min") {
                    e.items[0].set("updateStatus", "Y");

                }
                else if (e.field === "qty_harga4_min") {
                    e.items[0].set("updateStatus", "Y");

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
        editable: true,
        groupable: true,
        sortable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
                prepareActionGrid();
            }
        },
        toolbar: ["excel"],
        excel: {
            fileName: "ExportHarga.xlsx", allPages: true, Filterable:true
        },
        noRecords: true
    }).data("kendoGrid");

}

function onSaveClicked() {
    save();
}

function save() {
    gridData = $("#GridKota").data("kendoGrid");
    //paramValue = gridData.dataSource.data().toJSON();

    //var models = [];
    //gridData.table.find("input[type=checkbox]:checked").each(function () {
    //    var row = $(this).closest("tr");
    //    var model = gridData.dataItem(row);
    //    models.push(model);
    //});
  
    var saveData = gridData.dataSource.data().toJSON();
    var dataSave = $.grep(saveData, function (v) {
        return v.updateStatus === "Y" ;
    });

   
 
 

    //var savedata = {

    //};
    //  console.log('savedata: ' + JSON.stringify(models));
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
                data: JSON.stringify(dataSave),
                dataType: "json",
                url: urlSave,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message

                        });
                    } else {
                        Swal.fire({
                            type: 'success',
                            title: 'Success',
                            html: result.message
                        });
                        
                        $('#GridKota').kendoGrid('destroy').empty();
                      
                        $.when(getData()).done(function () {
                            bindGrid();
                        });
                    }
                    startSpinner('loading..', 0);



                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        }
    });
}


