
var typeda = [];

$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(GetTahun()).done(function () {
        $.when(GetBulan()).done(function () {
            typeda.push({
                "type": "Rinci",
            },
                {
                    "type": "Rekap",
                });
            $("#typeRpt").empty();
            $("#typeRpt").append('<option value="" selected disabled>Pilih Tipe</option>');
            var data = typeda;

            for (var i = 0; i < data.length; i++) {
                $("#typeRpt").append('<option value="' + data[i].type + '">' + data[i].type + '</option>');
            }

            $('#typeRpt').selectpicker('refresh');
            $('#typeRpt').selectpicker('render');
            startSpinner('loading..', 0);
        });
    });
});

function printData() {

    var Bulan = $('#Bulan').val();
    var Tahun = $('#Tahun').val();
    var typeRpt = $('#typeRpt').val();

    validationMessage = '';

    if (!Bulan) {
        validationMessage = validationMessage + 'Bulan harus di pilih.' + '\n';
    }
    if (!Tahun) {
        validationMessage = validationMessage + 'Tahun harus di pilih.' + '\n';
    }
    if (!typeRpt) {
        validationMessage = validationMessage + 'Tipe harus di pilih.' + '\n';
    }
    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        if (typeRpt == "Rinci") {
            window.open(
                serverUrl + "Reports/WebFormRpt.aspx?type=aktivapasiva&thn=" + $("#Tahun").val() + "&bln=" + $("#Bulan").val() + "&val=IDR", "_blank");

        }
        else {
            window.open(
                serverUrl + "Reports/WebFormRpt.aspx?type=aktivapasivarekap&thn=" + $("#Tahun").val() + "&bln=" + $("#Bulan").val() + "&val=IDR", "_blank");

        }
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