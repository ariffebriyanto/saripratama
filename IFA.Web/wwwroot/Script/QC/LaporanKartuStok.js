var kartustokds = [];
var _kartustokGrid;
var optionsGrid = {
    pageSize: 10
};
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(GetBarang()).done(function () {
        $.when(GetTahun()).done(function () {
            $.when(GetBulan()).done(function () {
                startSpinner('loading..', 0);
            });
        });
    });
});

function GetBarang() {
    return $.ajax({
        url: urlBarang,
        type: "POST",
        success: function (result) {
            //$("#Barang").empty();
            //$("#Barang").append('<option value="" selected disabled>Pilih Barang</option>');
            //var data = result;

            //for (var i = 0; i < data.length; i++) {
            //    $("#Barang").append('<option value="' + data[i].kode_Barang + '">' + data[i].nama_Barang + '</option>');
            //}

            //$('#Barang').selectpicker('refresh');
            //$('#Barang').selectpicker('render');
            $("#Barang").kendoDropDownList({
                dataTextField: "nama_Barang",
                dataValueField: "kode_Barang",
                filter: "contains",
                dataSource: result,
                optionLabel: "ALL",
                virtual: {
                    valueMapper: function (options) {
                        options.success([options.nama_Barang || 0]);
                    }
                },

            }).closest(".k-widget");

            $("#Barang").data("kendoDropDownList").list.width("400px");
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
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

function getData() {
    validationPage("getdata");
   
}

function getDataHeader() {
    var barang = $('#Barang').val();
    var Bulan = $('#Bulan').val();
    var Tahun = $('#Tahun').val();
    if ($('#checkbox2').is(":checked")) {
        Bulan = "";
    }
   
    return $.ajax({
        url: urlGetDataStokHeader + '?kd_stok=' + barang + "&bulan=" + Bulan + "&tahun=" + Tahun,
        type: "GET",
        success: function (result) {
            console.log(result);
            if (result.length > 0) {
                kartustokds = result[0].listSaldo;
                if (_kartustokGrid != undefined) {
                    $('#kartustokGrid').kendoGrid('destroy').empty();
                }

                $('#lblPeriod').text("Periode: " + result[0].bultah);
                $('#lblNamaBarang').text(result[0].nama_Barang);
                $('#lblKodeBarang').text(result[0].kode_Barang);
                $('#lblSaldoAwal').text(result[0].awal_qty_onstok.toFixed(2));
                $("#divHeader").show();
            }
          

            bindGrid();
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    _kartustokGrid = $("#kartustokGrid").kendoGrid({
        columns: [
            { field: "tanggal", title: "Tanggal", width: "120px", template: "#= kendo.toString(kendo.parseDate(tanggal, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "no_trans", title: "No Bukti", width: "130px" },
            { field: "atas_Nama", title: "Nama Toko/Gudang", width: "180px" },
            { field: "keterangan", title: "Keterangan", width: "180px", footerTemplate: "<div align=right>Total</div>" },
            { field: "qty_in", title: "Qty Masuk", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>"  },
            { field: "qty_out", title: "Qty Keluar", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>"  },
            { field: "qty_sisa", title: "Qty Sisa", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(data.qty_in.sum - data.qty_out.sum + data.awal_qty_onstok.max, \"n2\") #</div>"  }
        ],
        dataSource: {
            data: kartustokds,
            schema: {
                model: {
                    fields: {
                        tanggal: { type: "date" },
                        no_trans: { type: "string" },
                        atas_Nama: { type: "string" },
                        keterangan: { type: "string" },
                        qty_in: { type: "number" },
                        qty_out: { type: "number" },
                        qty_sisa: { type: "number" },
                        awal_qty_onstok: { type: "number" }


                    }
                }
            },
            aggregate: [
                { field: "qty_in", aggregate: "sum" },
                { field: "qty_out", aggregate: "sum" },
                { field: "qty_sisa", aggregate: "sum" },
                { field: "awal_qty_onstok", aggregate: "max" }
            ]
        },
        noRecords: true,
        height:450
    }).data("kendoGrid");

}

function printData() {
    validationPage("print");
}

function getDataPrint() {
    var barang = $('#Barang').val();
    var Bulan = $('#Bulan').val();
    var Tahun = $('#Tahun').val();
    if ($('#checkbox2').is(":checked")) {
        Bulan = "";
    }
    return $.ajax({
        url: urlGetPrintKartuStok + '?kd_stok=' + barang + "&bulan=" + Bulan + "&tahun=" + Tahun,
        type: "POST",
        success: function (result) {
            var MainWindow = window.open('', '', 'height=500,width=800');
            MainWindow.document.write(result);
            MainWindow.document.close();
            setTimeout(function () {
                MainWindow.print();
            }, 500);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function validationPage(action) {
    var barang = $('#Barang').val();
    var Bulan = $('#Bulan').val();
    var Tahun = $('#Tahun').val();
  
    validationMessage = '';
    if (!barang) {
        validationMessage = validationMessage + 'Barang harus di pilih.' + '\n';
    }
    if ($('#checkbox2').is(":unchecked")) {
        if (!Bulan) {
            validationMessage = validationMessage + 'Bulan harus di pilih.' + '\n';
        }
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
        startSpinner('loading..', 1);

        if (action == "print") {
            $.when(getDataPrint()).done(function () {
                startSpinner('loading..', 0);
            });
        }
        else {
            $.when(getDataHeader()).done(function () {
                startSpinner('loading..', 0);
            });
        }
    }
}