var _GridStokOpname;
var _GridStokOpname2;
var Opnameds = [];
var SatuanListParam = [];
var InvoiceList = [];
var GudangList = [];
var SatuanList = [];
var saldods = [];
var pods = [];
var customerds = [];
var valutads = [];
var jnsbayards = [];
var rekeningbankds = [];
var girods = [];

var columnOpname = [
    { field: "no_trans", title: "No Invoice", width: "80px", editor: invoiceDropDownEditor },
    { field: "jml_tagihan", title: "Jml. Tagihan", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    { field: "jml_bayar", title: "Jml. Bayar", editor: JmlBayarNumeric, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    { field: "jml_diskon", title: "Jml. Potongan", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    { field: "jml_pembulatan", title: "Jml. Pembulatan", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0.00}", attributes: { class: "text-right " }},
    { field: "pendp_lain", title: "Pendapatan Lain", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    { field: "subtotal", title: "Sub Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { command: ["edit", "destroy"], title: "Actions", width: "30px" }
];
var editval = true;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    $.when(getCustomer()).done(function () {
        $.when(fillCboCustomer()).done(function () {

            $.when(getValuta()).done(function () {
                $.when(fillCboValuta()).done(function () {

                    $.when(getJnsBayar()).done(function () {
                        $.when(fillCboJnsBayar()).done(function () {

                            $.when(getRekeningBank()).done(function () {
                                $.when(fillCboBank()).done(function () {

                                    $.when(getGiro()).done(function () {
                                        $.when(fillCboGiro()).done(function () {
                                            bindGrid();
                                            startSpinner('loading..', 0);
                                        });
                                    });
                                });
                            });
                        });
                    });
                });
            });

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


    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalto').datepicker('setStartDate', minDate);
    });

    $('#tanggal').val(dateserver);

    $(".decimalKendo").kendoNumericTextBox({
        decimals: 1
    });


    //Begin GRID
    _GridStokOpname2 = $("#GridStokOpname2").kendoGrid({
        columns: [
            { field: "no_inv", title: "No Invoice", editor: categoryDropDownEditor },
            { field: "jml_tagihan", title: "Jml. Tagihan" },
            { field: "jml_bayar", title: "Jml. Bayar" },
            { field: "jml_diskon", title: "Jml. Potongan" },
            { field: "jml_pembulatan", title: "Jml. Pembulatan" },
            { field: "pendp_lain", title: "Pendapatan Lain" },
            { field: "subtotal", title: "Sub Total" },
            { command: ["edit", "destroy"], title: "Action" },
        ],
        noRecords: true,
        dataSource: {
            data: InvoiceList,
            aggregate: [
                { field: "subtotal", aggregate: "sum", attributes: { style: "text-align: right;" } },
            ],
            schema: {
                model: {
                    id: "no_inv",
                    fields: {
                        no_inv: { type: "string" },
                        jml_tagihan: { type: "number", editable: false, nullable: false },
                        jml_bayar: { type: "number", editable: true, nullable: false, validation: { min: 0, required: true } },
                        jml_diskon: { type: "number", editable: false, nullable: false, validation: { min: 0, required: true } },
                        jml_pembulatan: { type: "number", editable: false, nullable: false, validation: { min: 0, required: true } },
                        pendp_lain: { type: "number", editable: false, nullable: false, validation: { min: 0, required: true } },
                        subtotal: { type: "number", editable: false, nullable: false, validation: { min: 0, required: true } },
                    }
                }
            },
            change: onChange,
        },
        edit: function (e) {
            debugger;
            //var DataGrid = _GridStokOpname2.data().kendoGrid.dataSource.data()[0];
            //DataGrid.set('no_inv', e.model.no_inv);
            //$('input[name=no_inv_input]').val(e.model.no_inv);
        },
        //save: function (e) {
        //    debugger;
        //    //console.log(e.model)
        //},
        editable: {
            createAt: "top",
            mode: "inline",
        },
        toolbar: ["create"]
    });
    //End GRID
        
});

function onChange(e) {
    if (e.action == "itemchange" && e.field == "jml_bayar") {
        var Tagihan = e.items[0].jml_tagihan;
        var Bayar = e.items[0].jml_bayar;
        var Total = Tagihan - Bayar;
        e.items[0].subtotal = Total;
        $('#GridStokOpname2').data('kendoGrid').refresh(); 
    }
}

function GetInvoice() {
    var filterdata = {
        kdcustomer: $("#Kd_Customer").val(),
        kdvaluta: $("#valuta").val(),
    };
    return $.ajax({
        url: urlInvoice,
        type: "POST",
        data: filterdata,
        success: function (result) {
            InvoiceList = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function categoryDropDownEditor(container, options) {
    $.when(GetInvoice()).done(function () {
    $('<input required name="' + options.field + '"/>')
        .appendTo(container)
        .kendoMultiColumnComboBox({
            //autoBind: true,
            dataTextField: "no_inv",
            dataValueField: "no_inv",
            height: 400,
            columns: [
                {
                    field: "no_inv",
                    title: "No Invoice",
                    width: 200
                },
                {
                    field: "tgl_inv",
                    title: "Tgl. Invoice",
                    template: "#= dateTime(data.tgl_inv)#",
                    width: 200
                },
                {
                    field: "tgl_jth_tempo",
                    title: "Tgl. Jatuh Tempo",
                    template: "#= dateTime(data.tgl_jth_tempo)#",
                    width: 200
                },
                {
                    field: "jml_tagihan",
                    title: "Jml. Tagihan",
                    template: "#= currency(data.jml_tagihan)#",
                    width: 200
                },
            ],
            footerTemplate: 'Total #: instance.dataSource.total() # items found',
            filter: "contains",
            filterFields: ["no_inv"],
            dataSource: InvoiceList,
            select: function (e) {
                var DataGrid = _GridStokOpname2.data().kendoGrid.dataSource.data()[0];
                //DataGrid.set('jml_tagihan', e.dataItem.jml_tagihan);
                DataGrid.jml_tagihan = e.dataItem.jml_tagihan;
                DataGrid.no_inv = e.dataItem.no_inv;
                //DataGrid["jml_tagihan"] = e.dataItem.jml_tagihan; 
                //_GridStokOpname2.data('kendoGrid').refresh();
            },
            //valuePrimitive: true
        })
    });
}

function currency(data) {
    return kendo.toString(data, "Rp ##,##.00");
}

function dateTime(data) {
    return kendo.toString(kendo.parseDate(data.substring(0, 10)), "dd/MM/yyyy");
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

function fillCboValuta() {
    $("#valuta").empty();
    $("#valuta").append('<option value="" selected disabled>Please select</option>');
    var data = valutads;
  
    for (var i = 0; i < data.length; i++) {
        $("#valuta").append('<option value="' + data[i].kode_Valuta + '">' + data[i].nama_Valuta + '</option>');
    }

    $('#valuta').selectpicker('refresh');
    $('#valuta').selectpicker('render');
}

function fillCboJnsBayar() {
    $("#JnsPembayaran").empty();
    $("#JnsPembayaran").append('<option value="" selected disabled>Please select</option>');
    var data = jnsbayards;

    for (var i = 0; i < data.length; i++) {
        $("#JnsPembayaran").append('<option value="' + data[i].id_Data + '">' + data[i].desc_Data + '</option>');
    }

    $('#JnsPembayaran').selectpicker('refresh');
    $('#JnsPembayaran').selectpicker('render');
}

function fillCboBank() {
    $("#Bank").empty();
    $("#Bank").append('<option value="" selected disabled>Please select</option>');
    var data = rekeningbankds;
    
    for (var i = 0; i < data.length; i++) {
        $("#Bank").append('<option value="' + data[i].kd_bank + '">' + data[i].nama_bank + '</option>');
    }

    $('#Bank').selectpicker('refresh');
    $('#Bank').selectpicker('render');
}

function fillCboGiro() {
    $("#NoGiro").empty();
    $("#NoGiro").append('<option value="" selected disabled>Please select</option>');
    var data = girods;

    for (var i = 0; i < data.length; i++) {
        $("#NoGiro").append('<option value="' + data[i].kd_bank + '">' + data[i].nama_bank + '</option>');
    }

    $('#NoGiro').selectpicker('refresh');
    $('#NoGiro').selectpicker('render');
}


function totalLabel(container, options) {
    var input = $('<label id="total" />');
    input.appendTo(container);
}

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
function getValuta() {
    return $.ajax({
        url: urlGetValuta,
        success: function (result) {
            valutads = [];
            valutads = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getJnsBayar() {
    return $.ajax({
        url: urlGetJnsBayar,
        success: function (result) {
            jnsbayards = [];
            jnsbayards = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getRekeningBank() {
    return $.ajax({
        url: urlGetRekBank,
        success: function (result) {
            rekeningbankds = [];
            rekeningbankds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getGiro() {
    var KodeCabang = null;
    var KodeCustomer = null;
    var Nomor = null;
    return $.ajax({
        url: urlGetGiro,
        data: { kdcb: KodeCabang, kdcust: KodeCustomer, nomor: Nomor},
        success: function (result) {
            girods = [];
            girods = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function onCustomerChanged() {
    var customer = $("#Kd_Customer").val();

    //var found = FindCustomer(customer);

    //var customer = $("#AlamatKirim").val(found[0].alamat1);
    //startSpinner('Loading..', 1);
    //getPiutangCustomer();

}



function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}

function bindGrid() {
    _GridStokOpname = $("#GridStokOpname").kendoGrid({
        columns: columnOpname,
        dataSource: {
            data: Opnameds,
            schema: {
                model: {
                    fields: {
                        no_trans: { type: "string", editable: true},
                        jml_tagihan: { type: "string", editable: false},
                        jml_bayar: { type: "string", editable: true },
                        jml_diskon: { type: "string", editable: false},
                        jml_pembulatan: { type: "string", editable: false},
                        pendp_lain: { type: "string", editable: false},
                        subtotal: { type: "string", editable: false}
                    }
                }
            },
            aggregate: [
                { field: "subtotal", aggregate: "sum" }

            ]
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        edit: function (e) {
            var dropdownlist = $("#no_trans").data("kendoDropDownList");
            if (e.model.no_trans != "") {
                var found = GetInvoiceCode(e.model.no_trans);
                var index = InvoiceList.findIndex(function (item, i) {
                    return item.no_trans === e.model.no_trans;
                });
                dropdownlist.select(index + 1);

                $("#jml_tagihan").text(found[0].jml_tagihan);
                $("#jml_diskon").text(found[0].jml_diskon);
                $("#jml_pembulatan").text(found[0].jml_pembulatan);
                $("#pendp_lain").text(found[0].pendp_lain);
                $("#jml_bayar").text(e.model.jml_bayar)
               
                var numerictextbox = $("#jml_bayar").data("kendoNumericTextBox");
                numerictextbox.value(e.model.jml_bayar);

                var total = calcTotal();
                $("#total").text(total);
            }
            addCustomCssButtonCommand();
        },
        save: function (data) {
            var grid = this;

            setTimeout(function () {
                var ds = $('#GridStokOpname').data('kendoGrid').dataSource.data();
                Opnameds = [];
                for (var i = 0; i <= ds.length - 1; i++) {
                    Opnameds.push({
                        no_trans: ds[i].no_inv,
                        jml_tagihan: formatNumber(ds[i].jml_tagihan + ".00"),
                        jml_bayar: ds[i].jml_bayar,
                        jml_diskon: ds[i].jml_diskon,
                        jml_pembulatan: ds[i].jml_pembulatan,
                        pendp_lain: ds[i].pendp_lain,
                        subtotal: ds[i].subtotal
                    });
                }
                $('#GridStokOpname').kendoGrid('destroy').empty();
                bindGrid();
            });
        },

        noRecords: true,
        editable: editval,
        dataBound: onDataBound
    }).data("kendoGrid");
}

function calcTotal() {
    debugger;
    var JmlTagihan = $("#jml_tagihan").val();
    var JmlBayar = $("#jml_bayar").text();
   
    var total = JmlTagihan - JmlBayar;
    return total;
}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
        max: options.model.qty_data,
        min: 0.1
    });
}

function invoiceDropDownEditor(container, options) {
    var input = $('<input required id="no_inv" name="no_inv">');
    input.appendTo(container);
    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "no_inv",
        dataValueField: "no_inv",
        dataSource: InvoiceList,
        filter: "contains",
        optionLabel: "Pilih Invoice",
        virtual: {
            valueMapper: function (options) {
                options.success([options.success || 0]);
            }
        },
        template: "<span data-id='${data.no_inv}' data-inv='${data.no_inv}'>${data.no_inv}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Invoice = e.item.find("span").attr("data-inv");
            var invoice = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            invoice.no_trans = Invoice;

            SatuanListParam = [];
            var found = GetInvoiceCode(id);
            invoice.jml_tagihan = found[0].jml_tagihan;
            invoice.jml_bayar = found[0].jml_bayar;
            invoice.jml_diskon = found[0].jml_diskon;
            invoice.jml_pembulatan = found[0].jml_pembulatan;
            invoice.pendp_lain = found[0].pendp_lain;
            invoice.subtotal = found[0].subtotal;
        }
    }).appendTo(container);
}


function JmlBayarNumeric(container, options) {
    var input = $('<input id="jml_bayar" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "{0:n2}",
        decimals: 2,
        min: 0,
        change: function (e) {
            var value = this.value();
            var invoice = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            invoice.jml_bayar = value;
            invoice.subtotal = value;
            $("#total").text(value);
        }
    });
}

function onAddNewRow() {
    $.when(GetInvoice()).done(function () {
        $('#GridStokOpname').kendoGrid('destroy').empty();
        bindGrid();
        var grid = $("#GridStokOpname").data("kendoGrid");
        grid.addRow();
    });
}

function GetInvoiceCode(code) {
    return InvoiceList.filter(
        function (InvoiceList) { return InvoiceList.no_inv === code; }
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
        validationMessage = validationMessage + 'Penerima/Nopo/Sopir harus di isi.' + '\n';
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

function onPrintTokoClicked() {
    //console.log(JSON.stringify(Opnameds));
    // //console.log(JSON.stringify(pods));

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
        cmds += '            TOKO 88'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)
        //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
        //cmds += newLine;
        //cmds += 'Telp:081216327988 WA:081216327988';
        //cmds += newLine;

        cmds += ' ' + Opnameds[0].alamat;
        cmds += newLine;
        cmds += 'Telp:' + Opnameds[0].fax1 + ' WA:' + Opnameds[0].fax1;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '         MUTASI KELUAR'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
        cmds += 'NO TRANS   : ' + Opnameds[0].no_trans;
        cmds += newLine;
        cmds += 'TANGGAL : ' + Opnameds[0].last_Create_Date;
        cmds += newLine;
        cmds += 'PENERIMA   : ' + Opnameds[0].penerima;
        cmds += newLine;
        cmds += 'USER       : ' + Opnameds[0].last_Created_By;
        cmds += newLine;
        cmds += 'GUDANG TUJUAN: ' + Opnameds[0].nama_gdtujuan;
        cmds += newLine;
        cmds += 'Keterangan: ' + Opnameds[0].keterangan;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        var totQty = 0;
        var totharga = 0;

        for (var i = 1; i <= Opnameds.length - 1; i++) {
            cmds += Opnameds[i].nama_Barang;
            cmds += newLine;
            var qtyIn = Opnameds[i].qty_out.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            totQty += Opnameds[i].qty_out * 1;
            cmds += qtyIn + " " + Opnameds[i].kd_satuan;
            //var countchar = 33 - (qtyIn.length + terimadtlds[i].kd_satuan.length + 1);
            //for (var j = 0; j <= countchar - 1; j++) {
            //    cmds += " ";
            //}
            cmds += newLine;
        }
        var jmlitem = Opnameds.length - 1;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Tot. Item : ' + jmlitem.toString();
        cmds += newLine;
        cmds += 'Tot.Qty   : ' + totQty.toLocaleString('id-ID', { maximumFractionDigits: 2 });
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Diperiksa  Pengirim   Penerima';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += ' (      )  (      )   (       )';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;


        cpj.printerCommands = cmds;
        //Send print job to printer!
        cpj.sendToClient();
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
        gudang_tujuan: $('#gudang').val(),
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

