var kotads = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 20
};
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(getData()).done(function () {
        bindGrid();
        prepareActionGrid();
        startSpinner('loading..', 0);
    });
});

function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            kotads = [];
            kotads = result;
            console.log(kotads);
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

            { field: "kode_Kota", title: "Kode Kota", width: "100px" },
            { field: "nama_Kota", title: "Nama Kota", width: "160px" },
            { field: "keterangan", title: "Keterangan", width: "160px" },
            { field: "Action", width: "200px", template: '<center><a class="btn btn-danger btn-sm hapusData" href="javascript:void(0)" data-id="#=kode_Kota#"><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a> &nbsp; &nbsp; <a class="btn btn-info btn-sm editData" href="javascript:void(0)" data-id="#=kode_Kota#"><i class="glyphicon glyphicon-pencil" aria-hidden="true"></i></a></center>' },

        ],
        dataSource: {
            data: kotads,
            schema: {
                model: {
                    id: "kode_Kota",
                    fields: {
                        nama_Kota: { type: "string" },
                        keterangan: { type: "string" }
                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
                prepareActionGrid();
            }
        },
        noRecords: true
    }).data("kendoGrid");

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

function showForm(id) {

    var link = urlForm;
    if (typeof id !== "undefined") {
        link = link + "/" + id;
    }
    //window.location = link;

    $("#editForm").load(link, function () {
        //show spinner
        startSpinner('Loading..', 1);
        $("#wrapperList").css("display", "none");
        $("#editForm").css("display", "");
        if (typeof id !== "undefined") {
            $("#Kode_Kota").prop("readonly", true);
            $("#save").hide();
            $("#update").show();
        }
        else {
            $("#save").show();
            $("#update").hide();

        }
        //hide spinner
        startSpinner('Loading..', 0);
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

function SaveData() {
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
                data: $("#form1").serialize(),
                url: urlAction,
                success: function (result) {
                    if (result.Success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        Swal.fire({
                            type: 'success',
                            title: 'Success',
                            html: result.Message
                        });
                        $.when(getData()).done(function () {
                            $('#GridKota').kendoGrid('destroy').empty();
                            bindGrid();

                            showlist();
                            prepareActionGrid();
                            startSpinner('loading..', 0);
                        });
                    }

                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        } else {
            return false;
        }
    });
}

function deleteKota(id) {
    swal({
        type: 'warning',
        title: 'Are you sure?',
        html: 'You want to delete this data',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);
            $.when(deleteAjax(id)).done(function () {

                startSpinner('loading..', 0);
            });
        }
    });
}

function deleteAjax(id) {
    var urlDeleteLink = urlDelete + "/" + id;
    return $.ajax({
        type: "POST",
        url: urlDeleteLink,
        success: function (result) {
            $('#GridKota').kendoGrid('destroy').empty();
            $.when(getData()).done(function () {
                bindGrid();

                showlist();
                prepareActionGrid();
                startSpinner('loading..', 0);
            });
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}