
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(GetTahun()).done(function () {
        $.when(GetBulan()).done(function () {
            startSpinner('loading..', 0);
        });
    });
});

function printData() {

    var Bulan = $('#Bulan').val();
    var Tahun = $('#Tahun').val();

    validationMessage = '';

    if (!Bulan) {
        validationMessage = validationMessage + 'Bulan harus di pilih.' + '\n';
    }
    if (!Tahun) {
        validationMessage = validationMessage + 'Tahun harus di pilih.' + '\n';
    }
    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        window.open(
            serverUrl + "Reports/WebFormRpt.aspx?type=labarugi&thn=" + $("#Tahun").val() + "&bln=" + $("#Bulan").val() + "&val=IDR", "_blank");
    }


}


function GetTahun() {
    return $.ajax({
        url: urlTahun,
        type: "POST",
        success: function (result) {
            $("#Tahun").empty();
            $("#Tahun").append('<option value="" selected disabled>Pilih Tahun</option>');
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#Tahun").append('<option value="' + data[i].key + '">' + data[i].value + '</option>');
            }

            $('#Tahun').selectpicker('refresh');
            $('#Tahun').selectpicker('render');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetBulan() {
    return $.ajax({
        url: urlBulan,
        type: "POST",
        success: function (result) {
            $("#Bulan").empty();
            $("#Bulan").append('<option value="" selected disabled>Pilih Bulan</option>');
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#Bulan").append('<option value="' + data[i].key + '">' + data[i].value + '</option>');
            }

            $('#Bulan').selectpicker('refresh');
            $('#Bulan').selectpicker('render');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}