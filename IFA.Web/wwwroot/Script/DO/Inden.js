var customerds = [];
var hargads = [];
var DOds = [];
var DOhdr = [];
var kasirds = [];
var _GridDO;

$(document).ready(function () {
    startSpinner('Loading..', 1);
    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })

    $("#tanggal").val(dateserver);
    $('#divtanggal').datepicker('remove');
    $('#tanggal').attr("disabled", "disabled");
    $.when(GetHargaBarang()).done(function () {
        $.when(getCustomer()).done(function () {
            //$.when(getKasir()).done(function () {
                fillCboCustomer();
                fillCboKasir();
                if (salesID != "") {
                    $("#kasir option[value='" + salesID + "']").attr("selected", "selected");
                    $('#kasir').selectpicker('refresh');
                    $('#kasir').selectpicker('render');
                    $('#kasir').attr("disabled", "disabled");
                }

                if (Mode != "NEW") {
                    $.when(getDataDO(idDO)).done(function () {
                        fillForm();
                        bindGrid();

                        if (Mode == "VIEW") {
                            var grid = $("#GridDO").data("kendoGrid");
                            grid.hideColumn(8);
                            $('#addKota').hide();

                        }

                        startSpinner('Loading..', 0);
                    });
                }
                else {
                    $("#dp").val(0);

                    bindGrid();
                    startSpinner('loading..', 0);
                }
           // });
        });
    });

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            return false;
        }
    });


});

function getDataDO(po) {
    var urlLink = urlGetData + "/" + po;

    return $.ajax({
        url: urlLink,
        type: "GET",
        success: function (result) {
            DOhdr = result;
            for (var i = 0; i <= DOhdr.length - 1; i++) {
                DOds.push({
                    "kode_Barang": DOhdr[i].kd_Stok,
                    "nama_Barang": DOhdr[i].nama_Barang,
                    "satuan": DOhdr[i].kd_satuan,
                    "berat": DOhdr[i].berat,
                    //"tBerat": DOhdr[i].tBerat,
                    "qty": DOhdr[i].qty,
                    "harga": DOhdr[i].harga,
                    "total": DOhdr[i].total,
                    "keterangan": DOhdr[i].keterangan,
                    "tBerat": DOhdr[i].qty * DOhdr[i].berat
                });
            }
            console.log(JSON.stringify(DOhdr));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function GetHargaBarang() {
    return $.ajax({
        url: urlGetHargaBarang,
        success: function (result) {
            hargads = [];
            hargads = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

$(document).on('keydown', function (event) {
    if (event.key == "F2") {
        onAddNewRow();
        return false;
    }
});

function getCustomer() {
    return $.ajax({
        url: urlGetCustomer,
        success: function (result) {
            customerds = [];
            customerds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getKasir() {
    return $.ajax({
        url: urlGetKasir,
        success: function (result) {
            kasirds = [];
            kasirds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillForm() {
    $('#Kd_Customer').attr("disabled", "disabled");
    $('#kasir').attr("disabled", "disabled");

    if (DOhdr.length > 0) {
        console.log(JSON.stringify(DOhdr));
        $("#Status").val(DOhdr[0].status);
        $("#Kd_Customer option[value='" + DOhdr[0].kd_Customer + "']").attr("selected", "selected");
        $('#Kd_Customer').selectpicker('refresh');
        $('#Kd_Customer').selectpicker('render');
        $("#tanggal").val(DOhdr[0].tgl_indendesc);
        $("#DONumber").val(idDO);
        $("#dp").val(DOhdr[0].dp_inden);


    }
}
function fillCboCustomer() {
    $("#Kd_Customer").empty();
    $("#Kd_Customer").append('<option value="" selected disabled>Please select</option>');
    var data = customerds;

    for (var i = 0; i < data.length; i++) {
        $("#Kd_Customer").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kd_Customer').selectpicker('refresh');
    $('#Kd_Customer').selectpicker('render');
}

function fillCboKasir() {
    $("#kasir").empty();
    $("#kasir").append('<option value="" selected disabled>Please select</option>');
    var data = kasirds;

    for (var i = 0; i < data.length; i++) {
        $("#kasir").append('<option value="' + data[i].kode_Sales + '">' + data[i].nama_Sales + '</option>');
    }

    $('#kasir').selectpicker('refresh');
    $('#kasir').selectpicker('render');
}

function bindGrid() {
    _GridDO = $("#GridDO").kendoGrid({
        columns: [
            { field: "nama_Barang", title: "Nama Barang", editor: barangDropDownEditor },
            { field: "satuan", title: "Satuan", editor: satuanLabel },
            { field: "berat", title: "Berat", editor: BeratLabel },
            { field: "qty", title: "Qty", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },

            { field: "harga", title: "Harga", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaLabel },
            { field: "tBerat", title: "Total Berat", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: tBeratLabel, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
            { field: "total", title: "Total", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
            { field: "keterangan", title: "Keterangan" },

            { command: ["edit", "destroy"], title: "Actions" },
            { field: "kode_Barang", title: "kode_Barang", hidden: true },


        ],
        dataSource: {
            data: DOds,
            schema: {
                model: {
                    fields: {
                        kode_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        satuan: { type: "string" },
                        berat: { type: "number" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        harga: { type: "number" },
                        total: { type: "number" },
                        keterangan: { type: "string" },
                        tBerat: { type: "number" },
                        flagbonus: { type: "boolean" }
                    }
                }
            },
            aggregate: [
                { field: "total", aggregate: "sum" },
                { field: "tBerat", aggregate: "sum" },
                { field: "qty", aggregate: "sum" }

            ]
        },
        edit: function (e) {
            addCustomCssButtonCommand();
            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");

            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            if (e.model.kode_Barang != "") {
                var found = GetBarangDetail(e.model.kode_Barang);
                var index = hargads.findIndex(function (item, i) {
                    return item.kode_Barang === e.model.kode_Barang;
                });
                dropdownlist.select(index + 1);

                $("#satuan").text(found[0].kd_Satuan);
                $("#stok").text(found[0].stok);
                $("#harga").text(e.model.harga);
                $("#berat").text(e.model.berat);
                var numerictextbox = $("#qty").data("kendoNumericTextBox");
                numerictextbox.value(e.model.qty);
                var total = calcTotal();
                $("#total").text(total);
                $("#tBerat").text(e.model.berat * e.model.qty);

            }

            addCustomCssButtonCommand();
        },
        save: function (e) {
        },
        cancel: function (e) {
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBinding: function (e) {

        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}

function onAddNewRow() {
    var grid = $("#GridDO").data("kendoGrid");
    grid.addRow();
    $('#Nama_Barang').data("kendoDropDownList").open();
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
        optionLabel: "Select Barang...",
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
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.kode_Barang = id;
            barang.nama_Barang = Barang.split("|", 1);

            var found = GetBarangDetail(id);
            console.log(JSON.stringify(found));
            barang.satuan = found[0].kd_Satuan;
            barang.berat = found[0].vol;
            barang.stok = found[0].stok;
            barang.harga = found[0].harga_Rupiah;

            $("#satuan").text(found[0].kd_Satuan);
            $("#berat").text(found[0].vol);
            $("#stok").text(found[0].stok);
            $("#harga").text(found[0].harga_Rupiah);
            var total = calcTotal();
            var tBerat = calcBerat();
            barang.tBerat = tBerat;

            barang.total = total;
            $("#tBerat").text(tBerat);
            $("#total").text(total);
        }
    }).appendTo(container);
}

function GetBarangDetail(code) {
    return hargads.filter(
        function (hargads) { return hargads.kode_Barang === code; }
    );
}

function satuanLabel(container, options) {
    var input = $('<label id="satuan" />');
    input.appendTo(container);
}

function stokLabel(container, options) {
    var input = $('<label id="stok" />');
    input.appendTo(container);
}

function hargaLabel(container, options) {
    var input = $('<label id="harga" />');
    input.appendTo(container);
}


function BeratLabel(container, options) {
    var input = $('<label id="berat" />');
    input.appendTo(container);
}
function tBeratLabel(container, options) {
    var input = $('<label id="tBerat" />');
    input.appendTo(container);
}
function totalLabel(container, options) {
    var input = $('<label id="total" />');
    input.appendTo(container);
}

function qtyNumeric(container, options) {
    var input = $('<input required id="qty" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.qty = value;
            var total = calcTotal();
            var tBerat = calcBerat();
            barang.tBerat = tBerat;

            barang.total = total;
            $("#tBerat").text(tBerat);
            $("#total").text(total);
        }
    });
}

function calcTotal() {
    var qty = $("#qty").val();
    var harga = $("#harga").text();

    return qty * harga;
}

function calcBerat() {
    var qty = $("#qty").val();
    var berat = $("#berat").text();

    return qty * berat;
}

function onSaveClicked() {
    //SaveData();
    validationPage();
}

function validationPage() {
    var Kd_Customer = $('#Kd_Customer').val();
    var kasir = $("#kasir").val();
    var dp = $("#dp").val();

    validationMessage = '';

    if (!Kd_Customer) {
        validationMessage = validationMessage + 'Customer harus di pilih.' + '\n';
    }
    if (!kasir) {
        validationMessage = validationMessage + 'Sales harus di pilih.' + '\n';
    }
    if (!dp) {
        validationMessage = validationMessage + 'DP tidak boleh kosong.' + '\n';
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

function showCreate() {
    window.location.href = urlCreate;
}


function SaveData() {
    $("#form1").validate();
    if (!$('#form1').valid()) {
        return false;
    }

    var no = "";
    if (Mode == "EDIT") {
        no = $("#DONumber").val();
        urlSaveData = urlEdit;
    } else {
        urlSaveData = urlSave;
    }
    var savedata = {
        No_sp: no,
        Kd_Customer: $("#Kd_Customer").val(),
        Tgl_sp: $("#tanggal").val(),
        Kd_sales: $("#kasir").val(),
        dp_inden: $("#dp").val(),
        details: _GridDO.dataSource.data().toJSON()
    };
    console.log(savedata);
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
                url: urlSaveData,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {

                        startSpinner('loading..', 0);

                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';;
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

function onPrintClicked() {
    if (jspmWSStatus()) {
        //Create a ClientPrintJob
        var cpj = new JSPM.ClientPrintJob();
        //Set Printer type (Refer to the help, there many of them!)
        if ($('#useDefaultPrinter').prop('checked')) {
            cpj.clientPrinter = new JSPM.DefaultPrinter();
        } else {
            cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
        }
        //Set content to print...
        //Create ESP/POS commands for sample label
        var esc = '\x1B'; //ESC byte in hex notation
        var newLine = '\x0A'; //LF byte in hex notation

        var cmds = esc + "@"; //Initializes the printer (ESC @)
        cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
        cmds += '            ' + DOhdr[0].nama; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)
        cmds += ' ' + DOhdr[0].alamat;
        cmds += newLine;
        cmds += 'Telp:' + DOhdr[0].telp + ' WA:' + DOhdr[0].wa;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '          INDEN'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)
        cmds += 'TANGGAL   :' + DOhdr[0].tgl_indendesc;
        cmds += newLine;
        cmds += 'KASIR     :' + DOhdr[0].last_Created_By;
        cmds += newLine;
        cmds += 'PELANGGAN :' + DOhdr[0].nama_Customer;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        var totQty = 0;
        var totharga = 0;
        for (var i = 0; i <= DOds.length - 1; i++) {
            cmds += DOds[i].nama_Barang;
            cmds += newLine;

            var qty = addCommas(DOds[i].qty.toString());
            var harga = addCommas(DOds[i].harga.toString());
            var total = DOds[i].harga * DOds[i].qty;
            var hargastr = addCommas(total.toString());

            totharga += total;
            totQty += DOds[i].qty;

            cmds += qty + " " + DOds[i].satuan + " X " + harga;

            var countchar = 33 - (qty.length + DOds[i].satuan.length + harga.length + hargastr.length + 4);
            for (var j = 0; j <= countchar - 1; j++) {
                cmds += " ";
            }
            cmds += hargastr;
            cmds += newLine;
        }

        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Tot. Item : ' + addCommas(DOds.length.toString());
        cmds += newLine;
        cmds += 'Tot.Qty   : ' + addCommas(totQty.toString());
        cmds += newLine;
        cmds += 'Total     : ' + addCommas(totharga.toString());
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cpj.printerCommands = cmds;
        //Send print job to printer!
        cpj.sendToClient();
    }

}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}