var brgDS = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 20
};
$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: urlGetData
            },
            update: {

                type: "POST",
                url: urlUpdate,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            destroy: {
                type: "POST",
                url: urlDelete,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            create: {
                type: "POST",
                url: urlSave,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            parameterMap: function (option, operation) {
                if (operation !== "read") {
                    return kendo.stringify(option.models);
                }
            }
        },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "kd_Customer",
                fields: {
                    kode_Supplier: { validation: { required: true } },
                    nama_Supplier: { validation: { required: true } },
                    alamat1: { type: "string" },
                    no_Telepon1: { type: "string" }



                }
            }
        }
    });

    $("#GridBrg").kendoGrid({
        dataSource: dataSource,
        groupable: true,
        sortable: true,
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        toolbar: ["excel"],
        excel: {
            fileName: "ExportHarga.xlsx", allPages: true, Filterable: true
        },
        pageable: true,
        height: 550,
        requestEnd: onRequestEnd,
        toolbar: [{
            name: "create",
            text: "Tambah Supplier"
        }],
        columns: [

            { field: "kode_Supplier", title: "Kode Supplier", width: "100px", editable: false },
            { field: "nama_Supplier", title: "Nama Supplier", width: "200px" },
            { field: "alamat1", title: "Alamat", width: "200px" },
            { field: "no_Telepon1", title: "Telepon", width: "120px" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "inline"
    });
    console.log(dataSource);
});


function onRequestEnd(e) {
    if (e.type === "create") {
        e.sender.read();
    }
    else if (e.type === "update") {
        e.sender.read();
    }
}

function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            brgDS = [];
            brgDS = result;
            console.log(brgDS);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function prepareActionGrid() {
    $(".hapusData").on("click", function () {
        var id = $(this).data("id");
        deleteKota(id);
    });
    $(".editData").on("click", function () {
        var id = $(this).data("id");
        showForm(id);
    });
}


function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

function onSaveClicked() {
    urlAction = urlSave;
    validationPage();
}

function onUpdateClicked() {
    urlAction = urlUpdate;
    validationPage();
}

function validationPage() {
    var reWhiteSpace = /\s/g;
    var Kode_Kota = $('#Kode_Kota').val();
    var Nama_Kota = $('#Nama_Kota').val();
    var Keterangan = $('#Keterangan').val();

    validationMessage = '';
    if (!Kode_Kota) {
        validationMessage = validationMessage + 'Kode Kota harus di isi.' + '\n';
    }
    if (!Nama_Kota) {
        validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    }

    if (!Keterangan) {
        validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    }

    if (Kode_Kota.match(reWhiteSpace)) {
        validationMessage = validationMessage + 'Kode Kota tidak boleh ada spasi.' + '\n';
    }
    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        $.when(SaveData()).done(function (x) {
            if (typeof x !== "undefined") {
                $('#GridKota').kendoGrid('destroy').empty();
                getData();
                showlist();
            }

        });
    }
}
