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
                id: "kd_buku_besar",
                fields: {
                    kd_buku_besar: { type: "string" },
                    nm_buku_besar: { type: "string" },
                    tipe_rek: { type: "string" },
                    grup_header: { type: "string" },
                    nama_Valuta: { type: "string" },
                    grup_level1: { type: "string" },
                    grup_level2: { type: "string" },
                    grup_level3: { type: "string" },
                    rec_Stat: { type: "string" },
                    div1: { type: "string" }



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
            text: "Tambah Buku Besar"
        }],
        columns: [

            { field: "kd_buku_besar", title: "Kode COA", width: "100px" },
            { field: "nm_buku_besar", title: "Nama COA", width: "300px" },
            { field: "tipe_rek", title: "Tipe Rekening", width: "100px" },
            { field: "grup_header", title: "Group Header", width: "160px" },
            { field: "nama_Valuta", title: "Kurs", width: "100px" },
            { field: "grup_level1", title: "Group Level 1", width: "100px" },
            { field: "grup_level2", title: "Group Level 2", width: "160px" },
            { field: "grup_level3", title: "Group Level 3", width: "160px" },
            { field: "div1", title: "Flag AK", template: '<input type="checkbox" #if(div1 === "Y"){#= checked="checked" #}else{}#= class="chkbx" />' },
            { field: "rec_Stat", title: "Status", template: '<input type="checkbox" #if(rec_Stat === "Y"){#= checked="checked" #}else{}#= class="chkbx" />' },
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
