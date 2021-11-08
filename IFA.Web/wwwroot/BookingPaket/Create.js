var _GridPaket;
var paketds = [];
var hargads = [];
var paketView = [];
var columnGrid = [
    { field: "nama_Barang", title: "Nama Barang", width: "100px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric },
    { field: "keterangan", title: "Keterangan", width: "80px" },
    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },

    { command: ["edit", "destroy"], title: "Actions", width: "30px" }
];
var optionsGrid = {
    pageSize: 10
};

$(document).ready(function () {
    startSpinner('Loading..', 1);

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })

    $('#divtanggalakhir').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })
    //tglkirimserver
    $("#tanggal").val(dateserver);
    $('#divtanggal').datepicker('remove');
    $('#tanggal').attr("disabled", "disabled");

    $("#tanggalakhir").val(setahun);
    $('#divtanggalakhir').attr("disabled", "disabled");

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            return false;
        }
    });

    $.when(GetHargaBarang()).done(function () {

        if (Mode != "NEW") {
            $.when(getDataPaket(idDO)).done(function () {
                fillForm();
                bindGrid();
                startSpinner('loading..', 0);
            });
        }
        else {
            bindGrid();
            startSpinner('Loading..', 0);
        }
    });
});

$(document).on('keydown', function (event) {
    var elementExist = document.getElementsByClassName("coverScreen");
    if (elementExist.length > 0 && elementExist[0].style.display == 'block') {
        if (event.key == "F2") {
            return false;
        }
        else if (event.key == "F4") {
            return false;
        }
        else if (event.key == "F7") {
            return false;
        }
        else if (event.key == "F8") {
            return false;
        }
    }
    else {
        if (event.key == "F2") {
            onAddNewRow();
            return false;
        }
        else if (event.key == "F4") {
            onSaveClicked();
            return false;
        }
        else if (event.key == "F7") {
            showCreate();
            return false;
        }
        else if (event.key == "F8") {
            showRetur();
            return false;
        }
    }

});

function showCreate() {
    window.location.href = urlCreate;
}

function getDataPaket(id) {
    return $.ajax({
        url: urlGetData + "/?id=" + id,
        success: function (result) {
            paketView = [];
            paketView = result;
            paketds = [];
            for (var i = 0; i <= paketView.details.length - 1; i++) {
                var totHarga = 0;
                totHarga = (paketView.details[i].harga * 1) * (paketView.details[i].qty * 1);
                paketds.push({
                    kode_Barang: paketView.details[i].kd_Stok,
                    nama_Barang: paketView.details[i].deskripsi,
                    satuan: paketView.details[i].kd_satuan,
                    qty: paketView.details[i].qty,
                    harga: paketView.details[i].harga,
                    total: totHarga,
                    keterangan: paketView.details[i].keterangan,
                });
            }
            console.log(JSON.stringify(paketView));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillForm() {
    $("#DONumber").val(paketView.no_Paket);
    $("#id").val(paketView.no_Paket);
    $("#Nama_Paket").val(paketView.nama_Paket);
    $("#Status option[value='" + paketView.status_Aktif + "']").attr("selected", "selected");
    $('#Status').selectpicker('refresh');
    $('#Status').selectpicker('render');
    $("#tanggal").val(paketView.tgl_Paketdesc);
    $("#tanggalakhir").val(paketView.tgl_Akhir_Paketdesc);
}

function bindGrid() {
    _GridPaket = $("#GridPaket").kendoGrid({
        columns: columnGrid,
        dataSource: {
            data: paketds,
            schema: {
                model: {
                    fields: {
                        kode_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        harga: { type: "number" },
                        total: { type: "number" },
                        keterangan: { type: "string" },
                    }
                }
            },
            aggregate: [
                { field: "total", aggregate: "sum" },
                { field: "qty", aggregate: "sum" },
            ]
        },
        edit: function (e) {
            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            if (e.model.kode_Barang != "") {
                var found = GetBarangDetail(e.model.kode_Barang);
                var index = hargads.findIndex(function (item, i) {
                    return item.kode_Barang === e.model.kode_Barang;
                });
                dropdownlist.select(index + 1);

                $("#satuan").text(found[0].kd_Satuan);
                $("#harga").text(e.model.harga);

                var numerictextbox = $("#qty").data("kendoNumericTextBox");
                numerictextbox.value(e.model.qty);


                var numerictextboxharga = $("#harga").data("kendoNumericTextBox");
                numerictextboxharga.value(e.model.harga);

                var total = calcTotal();
                $("#total").text(total);
            }

            addCustomCssButtonCommand();
        },
        save: function (e) {
            var grid = $("#GridPaket").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();

            paketds = ds;

            $('#GridPaket').kendoGrid('destroy').empty();
            bindGrid();
            onAddNewRow();

        },
        cancel: function (e) {
            $('#GridPaket').data('kendoGrid').dataSource.cancelChanges();
            var grid = $("#GridPaket").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();

            DOds = ds;

            $('#GridPaket').kendoGrid('destroy').empty();
            bindGrid();
        },
        dataBinding: function (e) {

            if (e.action == "rebind") {
                if (e.items.length > 0) {

                }
            }
        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}

function onDataBound(e) {
    addCustomCssButtonCommand();
}

function addCustomCssButtonCommand() {
    $(".k-grid-edit").removeClass("k-button k-button-icontext ");
    $(".k-grid-edit").addClass("btn btn-info colorWhite marginRight10 font10 padding79");
    $('.k-grid-edit').find('span').remove();

    $(".k-grid-delete").removeClass("k-button k-button-icontext");
    $(".k-grid-delete").addClass("btn btn-danger colorWhite  font10 padding79");
    $('.k-grid-delete').find('span').remove();

    $(".k-grid-update").removeClass("k-button k-primary k-button-icontext");
    $(".k-grid-update").addClass("btn btn-info colorWhite marginRight10 font10 padding79");
    $('.k-grid-update').find('span').remove();

    $(".k-grid-cancel").removeClass("k-button k-button-icontext");
    $(".k-grid-cancel").addClass("btn btn-danger colorWhite  font10 padding79");
    $('.k-grid-cancel').find('span').remove();
}

function barangDropDownEditor(container, options) {
    var input = $('<input required id="Nama_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Barang",
        dataValueField: "nama_Barang",
        dataSource: hargads,
        optionLabel: "Pilih Barang...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.Nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.kode_Barang}' data-Barang='${data.nama_Barang}'>${data.nama_Barang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");

            var barang = _GridPaket.dataItem($(e.sender.element).closest("tr"));
            barang.kode_Barang = id;
            barang.nama_Barang = Barang.split("| ", 1);

            var found = GetBarangDetail(id);
            barang.satuan = found[0].kd_Satuan;
            barang.stok = found[0].stok;
            barang.harga = found[0].harga_Rupiah;
            barang.vol = found[0].vol;

            $("#satuan").text(found[0].kd_Satuan);
            $("#stok").text(found[0].stok);
            $("#berat").text(found[0].vol);

            $("#harga").text(found[0].harga_Rupiah);
            $("#harga").val(found[0].harga_Rupiah);

            var total = calcTotal();
            barang.total = total;

            $("#total").text(total);
        }
    }).appendTo(container);
}

function GetHargaBarang() {
    return $.ajax({
        url: urlGetHargaBarang,
        success: function (result) {
            hargads = [];
            hargads = result;
            // console.log(JSON.stringify(hargads));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function satuanLabel(container, options) {
    var input = $('<label id="satuan" />');
    input.appendTo(container);
}

function qtyNumeric(container, options) {
    var input = $('<input id="qty" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "{0:n2}",
        decimals: 2,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridPaket.dataItem($(e.sender.element).closest("tr"));
            barang.qty = value;
            var found = GetBarangDetail(barang.kode_Barang);
            $("#harga").text(found[0].harga_Rupiah);
            $("#harga").val(found[0].harga_Rupiah);

            var total = calcTotal();
            barang.total = total;
            $("#total").text(total);

        }
    });
}

function hargaNumeric(container, options) {
    var input = $('<input id="harga" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: options.model.harga,
        change: function (e) {
            var value = this.value();
            var barang = _GridPaket.dataItem($(e.sender.element).closest("tr"));
            barang.harga = value;
            var qty = $("#qty").val();
            var total = ((qty * value) * 1)
            barang.total = total;
            $("#total").text(total);
        }
    });
}

function totalLabel(container, options) {
    var input = $('<label id="total" />');
    input.appendTo(container);
}

function onAddNewRow() {
    var grid = $("#GridPaket").data("kendoGrid");
    grid.addRow();
    $('#Nama_Barang').data("kendoDropDownList").open();
}

function GetBarangDetail(code) {
    return hargads.filter(
        function (hargads) { return hargads.kode_Barang === code; }
    );
}

function calcTotal() {
    var qty = $("#qty").val();
    var harga = $("#harga").text();
    var total = 0;
    total = ((qty * harga) * 1)

    return total;
    //return (qty * harga);
}

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    var tanggalakhir = $('#tanggalakhir').val();
    var Nama_Paket = $('#Nama_Paket').val();

    var ItemData = _GridPaket.dataSource.data();
    validationMessage = '';

    if (!tanggalakhir) {
        validationMessage = validationMessage + 'Tanggal Berakhir di pilih.' + '<br/>';
    }
    if (!Nama_Paket) {
        validationMessage = validationMessage + 'Nama Paket tidak boleh kosong.' + '<br/>';
    }

    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Tambahkan Item.' + '<br/>';
    }

    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        SaveData();
    }
}

function SaveData() {
    var urlSaveData = "";

    var detailtemp = _GridPaket.dataSource.data().toJSON();
    var detailds = [];
    for (var i = 0; i <= detailtemp.length - 1; i++) {
        detailds.push({
            Kd_Stok: detailtemp[i].kode_Barang,
            Kd_satuan: detailtemp[i].satuan,
            Deskripsi: detailtemp[i].nama_Barang,
            Qty: detailtemp[i].qty,
            harga: detailtemp[i].harga,

        });
    }


    var savedata = {
        No_Paket: $("#id").val(),
        Tgl_Paket: $("#tanggal").val(),
        Nama_Paket: $("#Nama_Paket").val(),
        Tgl_Akhir_Paket: $("#tanggalakhir").val(),
        Status_Aktif: $("#Status").val(),
        details: detailds
    };

    console.log(JSON.stringify(savedata));

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
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';

                    }
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });

        }
    });

}