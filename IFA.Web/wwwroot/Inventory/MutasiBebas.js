var _GridStokOpname;
var Opnameds = [];
var SatuanListParam = [];
var BarangList = [];
var GudangList = [];
var SatuanList = [];
var saldods = [];
var columnOpname = [
    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "90px" },
    { field: "qty_data", title: "Stok Barang", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "qty_out", title: "Jumlah", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation },
    { field: "keterangan", title: "Keterangan", width: "180px" },
    { command: ["destroy"], title: "Actions", width: "40px" }
];
var editval = true;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    bindGrid();
    $.when(GetBarang()).done(function () {
        $.when(GetSatuan()).done(function () {
            $.when(getGudang()).done(function () {
                $.when(getData(idOpname)).done(function () {
                    startSpinner('loading..', 0);
                });
            });
        });
    });


});

function getData(id) {
    var urlLink = urlGetData;
    var filterdata = {
        no_trans: id,
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Opnameds = [];
            console.log(result);

            if (result.length > 0) {
                $("#NoTransaksi").val(result[0].no_trans);
                $("#Keterangan").val(result[0].keterangan);
                $("#penerima").val(result[0].penerima);
                $('#Keterangan').attr("disabled", "disabled");
                $('#penerima').attr("disabled", "disabled");

                columnOpname = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px" },
                    { field: "qty_out", title: "Jumlah", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "keterangan", title: "Keterangan", width: "180px" }
                ];
                editval = false;

                $("#addNew").hide();
                $("#save").hide();
                $("#new").show();
                
                if (_GridStokOpname != undefined) {
                    $("#GridStokOpname").kendoGrid('destroy').empty();
                }
                for (var i = 0; i <= result[0].detail.length - 1; i++) {
                    $("#gudang option[value='" + result[0].detail[i].gudang_asal + "']").attr("selected", "selected");
                    $('#gudang').selectpicker('refresh');
                    $('#gudang').selectpicker('render');
                    $('#gudang').attr("disabled", "disabled");
                    Opnameds.push({
                        kd_stok: result[0].detail[i].kd_stok,
                        Kode_Barang: result[0].detail[i].kd_stok,
                        Nama_Barang: result[0].detail[i].nama_Barang,
                        nama_Barang: result[0].detail[i].nama_Barang,
                        Kode_satuan: result[0].detail[i].kd_satuan,
                        Nama_Satuan: result[0].detail[i].kd_satuan,
                        kd_satuan: result[0].detail[i].kd_satuan,
                        satuan: result[0].detail[i].kd_satuan,
                        qty_data: result[0].detail[i].qty_out + result[0].detail[i].qty_sisa,
                        keterangan: result[0].detail[i].keterangan,
                        qty_out: result[0].detail[i].qty_out,
                        nama_Gudang: result[0].detail[i].nama_Gudang

                    });
                }
                bindGrid();

            }


        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetBarang() {
    return $.ajax({
        url: urlBarang,
        type: "POST",
        success: function (result) {
            BarangList = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getGudang() {
    return $.ajax({
        url: urlGudang,
        type: "POST",
        success: function (result) {
            $("#gudang").empty();
            $("#gudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            GudangList = result;
            for (var i = 0; i < data.length; i++) {
                $("#gudang").append('<option value="' + data[i].kode_Gudang + '">' + data[i].nama_Gudang + '</option>');
            }
            $('#gudang').selectpicker('refresh');
            $('#gudang').selectpicker('render');

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetSatuan() {
    return $.ajax({
        url: urlSatuan,
        type: "POST",
        success: function (result) {
            SatuanList = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    _GridStokOpname = $("#GridStokOpname").kendoGrid({
        columns: columnOpname,
        dataSource: {
            data: Opnameds,
            schema: {
                model: {
                    fields: {
                        kd_stok: { type: "string", editable: false },
                        Kode_Barang: { type: "string", editable: false },
                        Nama_Barang: { type: "string", editable: false },
                        nama_Barang: { type: "string" },
                        Kode_satuan: { type: "string", editable: false },
                        Nama_Satuan: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        satuan: { type: "string", editable: false },
                        qty_data: { type: "number", editable: false },
                        keterangan: { type: "string" },
                        gudang_tujuan: { type: "string" },
                        qty_out: { type: "string" },
                        nama_Gudang: { type: "string" },
                        rek_persediaan: { type: "string", editable: false },
                        harga: { type: "string", editable: false }
                    }
                }
            },
            aggregate: [
                { field: "qty_data", aggregate: "sum" },
                { field: "qty_out", aggregate: "sum" }

            ]
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        save: function (data) {
            var grid = this;

            setTimeout(function () {
                var ds = $('#GridStokOpname').data('kendoGrid').dataSource.data();
                Opnameds = [];
                for (var i = 0; i <= ds.length - 1; i++) {
                    if (ds[i].keterangan == "" && ds[i].nama_Gudang != "") {
                        ds[i].keterangan = "MUTASI ke " + ds[i].nama_Gudang;
                    }
                    Opnameds.push({
                        kd_stok: ds[i].kd_stok,
                        Kode_Barang: ds[i].Kode_Barang,
                        Nama_Barang: ds[i].Nama_Barang,
                        nama_Barang: ds[i].nama_Barang,
                        Kode_satuan: ds[i].Kode_satuan,
                        Nama_Satuan: ds[i].Nama_Satuan,
                        kd_satuan: ds[i].kd_satuan,
                        satuan: ds[i].satuan,
                        qty_data: ds[i].qty_data,
                        keterangan: ds[i].keterangan,
                        gudang_tujuan: ds[i].gudang_tujuan,
                        qty_out: ds[i].qty_out,
                        nama_Gudang: ds[i].nama_Gudang,
                        rek_persediaan: ds[i].rek_persediaan,
                        harga: ds[i].harga

                    });
                }
                $('#GridStokOpname').kendoGrid('destroy').empty();
                bindGrid();
            });
        },

        noRecords: true,
        editable: editval
    }).data("kendoGrid");
}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 4,
        max: options.model.qty_data,
        min: 1
    });
}

function barangDropDownEditor(container, options) {
    var input = $('<input required id="kode_Barang" name="nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Barang",
        dataValueField: "nama_Barang",
        dataSource: BarangList,
        filter: "contains",
        optionLabel: "Pilih Barang",
        virtual: {
            valueMapper: function (options) {
                options.success([options.nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.kode_Barang}' data-Barang='${data.nama_Barang}'>${data.nama_Barang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            var barang = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            barang.kd_stok = id;
            barang.nama_Barang = Barang;
            barang.Kode_Barang = id;
            barang.Nama_Barang = Barang;
            barang.nama_barang = Barang;

            SatuanListParam = [];
            var found = GetSatuanCode(id);
            barang.Kode_satuan = found[0].kd_Satuan;
            barang.Nama_Satuan = found[0].kd_Satuan;
            barang.kd_satuan = found[0].kd_Satuan;
            barang.satuan = found[0].kd_Satuan;
            barang.qty_data = found[0].qty_data;
            barang.rek_persediaan = found[0].rek_persediaan;
            barang.harga = found[0].harga;

        }
    }).appendTo(container);
}

function gudangDropDownEditor(container, options) {
    var input = $('<input required id="kode_Gudang" name="nama_Gudang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Gudang",
        dataValueField: "nama_Gudang",
        dataSource: GudangList,
        filter: "contains",
        template: "<span data-id='${data.kode_Gudang}' data-Barang='${data.nama_Gudang}'>${data.nama_Gudang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            var gudang = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            gudang.gudang_tujuan = id;

        }
    }).appendTo(container);
}

function satuanDropDownEditor(container, options) {
    inputSatuan = $("<input required  id='Kode_Satuan' name='Nama_Satuan'  />")
        .attr("Kode_Satuan", "Nama_Satuan")
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Nama_Satuan",
            dataValueField: "Nama_Satuan",
            dataSource: SatuanListParam,
            template: "<span data-id='${data.Kode_Satuan}' data-Satuan='${data.Nama_Satuan}'>${data.Nama_Satuan}</span>",
            select: function (e) {
                var id = e.item.find("span").attr("data-id");
                var Satuan = e.item.find("span").attr("data-Satuan");
                var satuan = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
                satuan.kd_satuan = id;
                satuan.satuan = Satuan;
                satuan.Kode_satuan = id;
                satuan.Kode_Satuan = id;
                satuan.Nama_Satuan = Satuan;
            }
        }).data("kendoDropDownList");
}

function onAddNewRow() {
    var grid = $("#GridStokOpname").data("kendoGrid");
    grid.addRow();
}

function GetSatuanCode(code) {
    return BarangList.filter(
        function (BarangList) { return BarangList.kode_Barang === code; }
    );
}

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    var gudang = $('#gudang').val();
    var penerima = $('#penerima').val();
    var ItemData = _GridStokOpname.dataSource.data();
    validationMessage = '';
    if (!gudang) {
        validationMessage = validationMessage + 'Gudang harus di pilih.' + '\n';
    }
    if (!penerima) {
        validationMessage = validationMessage + 'Penerima harus di isi.' + '\n';
    }

    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Tambahkan Item.' + '\n';
    }

    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        savedata();
    }
}

function savedata() {
    var transno = "";
    if (Mode != "NEW") {
        transno = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        no_trans: transno,
        penerima: $('#penerima').val(),
        keterangan: $('#Keterangan').val(),
        gudang: $('#gudang').val(),
        detail: _GridStokOpname.dataSource.data().toJSON()
    };

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
                data: savedata,
                url: urlSave,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        window.location.href = urlOpname + '?id=' + result.result + '&mode=VIEW';;
                        // startSpinner('loading..', 0);
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

function newCreate() {
    window.location.href = urlOpname;
}