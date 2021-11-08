//import { request } from "https";

var InvoiceDS = [];
var InvoiceList = [];
var _GridDO1;
var namatoko = "";
var alamat = "";
var telp = "";
var wa = "";
var brgDS = [];
var total = 0;
var _GridDO; 
var _GridGiro; 
var selected = [];
var dsgiro = [];

var columnGrid = [
    { field: "no_trans", title: "No Invoice", width: "50px", editor: invoiceDropDownEditor },
    { field: "jml_tagihan", title: "Jml. Tagihan", editor: jmltagihanLabel, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "jml_bayar", title: "Jml. Bayar", editor: bayarNumeric, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "jml_diskon", title: "Jml. Potongan", editor: potongan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "jml_pembulatan", title: "Jml. Pembulatan", editor: pembulatan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "pendp_lain", title: "Pendapatan Lain", editor: pendapatanlain, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "subtotal", title: "Sub Total", editor: subtotal, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
];
var optionsGrid = {
    pageSize: 10
};

$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(
        getCustomer(),
        getValuta(),
        getJnsBayar(),
        getRekeningBank()
        //getGiro()
    ).done(function () {

        if (Mode != "NEW") {
            $.when(getDataAR(idAR)).done(function () {
                //fillForm();
                if (Mode == "VIEW") {
                    //  bindGrid();
                }
                // $('#GridStokOpname').kendoGrid('destroy').empty();



                //startSpinner('loading..', 0);
            });


        }

        bindGrid();
        startSpinner('Loading..', 0);
    });
    $("#STransferTunai").val(0);
    $("#TfTunai").val(0);
    $("#GT").val(0);

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        //var minDate = new Date(selected.date.valueOf());
        //$('#tanggalfrom').datepicker('setStartDate', minDate);
    });

    $(".decimalKendo").kendoNumericTextBox({
        decimals: 1
    });
    $("#tanggalfrom").val(kendo.toString(new Date(), "dd MMM yyyy"));
    hide();
    if (Mode == "New") {
        hide();
    }

    $('#chktransfer').change(function () {
        var isCheckedtf = $('#chktransfer').prop("checked") ? true : false;
        var isCheckedtt = $('#chktunai').prop("checked") ? true : false;
        var isCheckedgr = $('#chkgiro').prop("checked") ? true : false;

        if (isCheckedtf == false) {
            $('#TfTransfer').val(0);
            $('#STransfer').val(0);
            var numeric = $("#TfTransfer").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransfer").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedtt == false) {
            $('#TfTunai').val(0);
            $('#STransferTunai').val(0);
            var numeric = $("#TfTunai").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferTunai").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedgr == false) {
            $('#TfGiro').val(0);
            $('#STransferGiro').val(0);
            var numeric = $("#TfGiro").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferGiro").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedtf == true && isCheckedtt == false) {
            $("#DivTN").hide();
            $("#DivTF").show();
            $("#DivBank").show();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == false && isCheckedtt == true) {
            $("#DivTF").hide();
            $("#DivTN").show();
            $("#DivBank").hide();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == true && isCheckedtt == true) {
            $("#DivTF").show();
            $("#DivTN").show();
            $("#DivBank").show();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == false && isCheckedtt == false) {
            $("#DivTF").hide();
            $("#DivTN").hide();
            $("#DivBank").hide();
            if (Mode != "VIEW") {
                if (isCheckedgr == false) {
                    $('#addNew').hide();
                } else {
                    $('#addNew').show();
                }
            }
        }
    });

    $("#chktunai").change(function () {

        var isCheckedtf = $('#chktransfer').prop("checked") ? true : false;
        var isCheckedtt = $('#chktunai').prop("checked") ? true : false;
        var isCheckedgr = $('#chkgiro').prop("checked") ? true : false;

        if (isCheckedtf==false) {
            $('#TfTransfer').val(0);
            $('#STransfer').val(0);
            var numeric = $("#TfTransfer").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransfer").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedtt==false) {
            $('#TfTunai').val(0);
            $('#STransferTunai').val(0);
            var numeric = $("#TfTunai").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferTunai").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedgr==false) {
            $('#TfGiro').val(0);
            $('#STransferGiro').val(0);
            var numeric = $("#TfGiro").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferGiro").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedtf == true && isCheckedtt == false) {
            $("#DivTN").hide();
            $("#DivTF").show();
            $("#DivBank").show();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == false && isCheckedtt == true) {
            $("#DivTF").hide();
            $("#DivTN").show();
            $("#DivBank").hide();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == true && isCheckedtt == true) {
            $("#DivTF").show();
            $("#DivTN").show();
            $("#DivBank").show();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }
        } else if (isCheckedtf == false && isCheckedtt == false) {
            $("#DivTF").hide();
            $("#DivTN").hide();
            $("#DivBank").hide();
            if (Mode != "VIEW") {
                if (isCheckedgr == false) {
                    $('#addNew').hide();
                } else {
                    $('#addNew').show();
                }
            }
        }
    })

    $("#chkgiro").change(function () {
        var isCheckedgr = $('#chkgiro').prop("checked") ? true : false;
        var isCheckedtf = $('#chktransfer').prop("checked") ? true : false;
        var isCheckedtt = $('#chktunai').prop("checked") ? true : false;

        if (isCheckedtf == false) {
            $('#TfTransfer').val(0);
            $('#STransfer').val(0);
            var numeric = $("#TfTransfer").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransfer").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedtt == false) {
            $('#TfTunai').val(0);
            $('#STransferTunai').val(0);
            var numeric = $("#TfTunai").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferTunai").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedgr == false) {
            $('#TfGiro').val(0);
            $('#STransferGiro').val(0);
            var numeric = $("#TfGiro").getKendoNumericTextBox();
            numeric.focus();
            var numeric1 = $("#STransferGiro").getKendoNumericTextBox();
            numeric1.focus();
        }

        if (isCheckedgr == true) {
            $("#DivNoGiroCek").show();
            $("#DivGiroCek").show();
            if (Mode != "VIEW") {
                $('#addNew').show();
            }

        } else {
            $("#DivNoGiroCek").hide();
            $("#DivGiroCek").hide();

            if (Mode != "VIEW") {
                if (isCheckedtf == false && isCheckedtt == false) {
                    $('#addNew').hide();
                } else {
                    $('#addNew').show();
                }
            }

        }

    })

        $("#JnsPembayaran").change(function () {
            var val = this.value;
            hide();

            if (val == "04") { //Transfer
                $("#DivTF").show();
                $("#DivNoGiroCek").hide();
                $("#DivGiroCek").hide();
                $("#DivBank").show();
                if (Mode != "VIEW") {
                    $('#addNew').show();
                }
            } else if (val == "01") { //Giro Cek
                $("#DivNoGiroCek").show();
                $("#DivGiroCek").show();
                $("#DivBank").hide();
                if (Mode != "VIEW") {
                    $('#addNew').show();
                }
                $("#DivTF").hide();
            } else if (val == "03") {//Tunai
                $("#DivTF").show();
                $("#DivNoGiroCek").hide();
                if (Mode != "VIEW") {
                    $('#addNew').show();
                }
                $("#DivBank").hide();
                $("#DivGiroCek").hide();
            } else if (val == "") {
                $('#addNew').hide();
                $("#DivBank").hide();
                $("#DivNoGiroCek").hide();
            }
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

    $(document).on('keydown', function (event) {
        if (event.key == "F2") {
            onAddNewRow();
            return false;
        }
    });
});

function hide() {
    $('#addNew').hide();
    $("#DivNoGiroCek").hide();
    $("#DivGiroCek").hide();
    $("#DivTF").hide();
    $("#DivTN").hide();
    $("#DivBank").hide();

    $("#TfGiro").val(0);
    $("#STransferGiro").val(0);
    $("#NoGiro").val(0);
    $("#TfTunai").val(0);
    $("#STransferTunai").val(0);
    $("#DivBank").val('');
}
function closemodal() {
    $("#giroModal").hide();

}
function getDataGiro() {
    var urlLink = urlGetGiro;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            brgDS = [];
            brgDS = result;

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function openGiro() {
    $("#giroModal").show();
    if ($('#gvGiro').hasClass("k-grid")) {
        $('#gvGiro').kendoGrid('destroy').empty();
    }
   //' $("#btnSimpanGiro").hide();
    $.when(getDataGiro()).done(function () {
        
        bindGridGiro();
    });

}

function customBoolEditor(container, options) {
    var guid = kendo.guid();
    $('<center><input id="chkStatus" type="checkbox" name="isSelect" data-type="boolean" data-bind="checked:isSelect" onchange="onChkChanged();"></center>').appendTo(container);
}

function dirtyField(data, fieldName) {
    var hasClass = $("[data-uid=" + data.uid + "]").find(".k-dirty-cell").length < 1;
    if (data.dirty && data.dirtyFields[fieldName] && hasClass) {
        return "<span class='k-dirty'></span>"
    }
    else {
        return "";
    }
}
//function ItemCalc() {
//    if (_GridDO) {
//        var requestData = _GridDO.dataSource.data();
//        var value = 0;
//        for (var i = 0; i < requestData.length; i++) {
//            if (requestData[i].isSelect == 1) {
//                value += requestData[i].jml_trans * 1;
//            }
//        }
//    }
//}
var harga;
//function onChkChanged() {

//    var total = 0;
//    if ($('#chkStatus').is(":checked")) {
        
//        total = ItemCalc();
//        $("#TfGiro").text(total);
//        $("#TfGiro").readonly();
//    }
   
//}

function SaveDataGiro() {
    var checked = [];
    var total = 0;
    var allSelected = $("#gvGiro tr.k-state-selected");
   var allSelectedModels = [];
    $.each(allSelected, function (e) {
        var row = $(this);
        var grid = row.closest(".k-grid").data("kendoGrid");
        var dataItem = grid.dataItem(row);

        allSelectedModels.push(dataItem);

    });
   


for (var i1 = 0; i1 < allSelectedModels.length; i1++) {
    total += allSelectedModels[i1].jml_trans * 1;
    }
dsgiro = allSelectedModels;
    $("#TfGiro").val(total);
    
        bindGridGiro1();
    
   
    var numeric1 = $("#TfGiro").getKendoNumericTextBox();
    numeric1.focus();
    
    selected = [];
    closemodal();
}
function bindGridGiro() {
    _GridGiro = $("#gvGiro").kendoGrid({
        scrollable: true,
        columns: [
            { selectable: true,width: 80 },
            { field: "nomor", title: "Nomor Giro", filterable: true, width: "200px" },
            { field: "tgl_trans", title: "Tanggal", filterable: true, width: "200px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "tgl_jth_tempo", title: "Tgl Jatuh tempo", filterable: true, width: "200px", template: "#= kendo.toString(kendo.parseDate(tgl_jth_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "jns_giro_desc", title: "Jenis", filterable: true, width: "200px", editor: Jenisdropdown },
            { field: "nama_departemen", title: "Divisi", filterable: true, width: "200px", editor: Divisidropdown },
            { field: "desc_Data", title: "Bank Asal", filterable: true, width: "200px", editor: BankAsaldropdown },
            { field: "nama_bank", title: "Bank Tujuan", filterable: true, width: "200px", editor: BankTujuandropdown },
            { field: "nama_customer", title: "Nama Kartu", filterable: true, width: "200px", editor: Kartudropdown },
            { field: "kd_valuta", title: "Valuta", filterable: true, width: "200px" },
            { field: "kurs_valuta", title: "Kurs", filterable: true, width: "200px" },
            { field: "jml_trans", title: "Jumlah Trans", filterable: true, width: "200px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", filterable: true, width: "200px" },
           
        ],
        dataSource: {
            data: brgDS,
            schema: {
                model: {
                    fields: {
                        nomor: { validation: { required: true } },
                        jns_trans: { validation: { required: true } },
                        divisi: { validation: { required: true } },
                        kd_bank: { validation: { required: true } },
                        kartu: { validation: { required: true } },
                        tgl_trans: { type: "date", validation: { required: true } },
                        tgl_jth_tempo: { type: "date", validation: { required: true } },
                        desc_Data: { type: "string", validation: { required: true } },
                        Desc_Data: { type: "string", validation: { required: true } },
                        nama_bank: { type: "string", validation: { required: true } },
                        nama_customer: { type: "string", validation: { required: true } },
                        kd_valuta: { type: "string", defaultValue: "IDR", validation: { required: true } },
                        kurs_valuta: { type: "number", defaultValue: 1, validation: { required: true } },
                        jml_trans: { type: "number", validation: { required: true } },
                        keterangan: { type: "string" },
                        jns_giro_desc: { type: "string", validation: { required: true } },
                        nama_departemen: { type: "string", validation: { required: true } },
                        bank_asal: { type: "string", validation: { required: true } },
                    }
                }

            },
            aggregate: [
                { field: "jml_trans", aggregate: "sum" },
            ],
            pageSize: optionsGrid.pageSize
        },
        sortable: true,
        //change: onChange,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true

    }).data("kendoGrid");

}

function bindGridGiro1() {
    _GridDO1 = $("#gvGiro1").kendoGrid({
        scrollable: true,
        columns: [
            { selectable: true, width: "40px", title: "Pilih", headerTemplate: '<label style="vertical-align:bottom">Pilih</label>' },
            { field: "nomor", title: "Nomor Giro", filterable: true, width: "200px" },
            { field: "tgl_trans", title: "Tanggal", filterable: true, width: "200px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "tgl_jth_tempo", title: "Tgl Jatuh tempo", filterable: true, width: "200px", template: "#= kendo.toString(kendo.parseDate(tgl_jth_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "jns_giro_desc", title: "Jenis", filterable: true, width: "200px", editor: Jenisdropdown },
            { field: "nama_departemen", title: "Divisi", filterable: true, width: "200px", editor: Divisidropdown },
            { field: "desc_Data", title: "Bank Asal", filterable: true, width: "200px", editor: BankAsaldropdown },
            { field: "nama_bank", title: "Bank Tujuan", filterable: true, width: "200px", editor: BankTujuandropdown },
            { field: "nama_customer", title: "Nama Kartu", filterable: true, width: "200px", editor: Kartudropdown },
            { field: "kd_valuta", title: "Valuta", filterable: true, width: "200px" },
            { field: "kurs_valuta", title: "Kurs", filterable: true, width: "200px" },
            { field: "jml_trans", title: "Jumlah Trans", filterable: true, width: "200px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", filterable: true, width: "200px" },

        ],
        selectable: "multiple, row",
        editable: "inline",
        dataSource: {
            data: dsgiro,
            schema: {
                model: {
                    fields: {
                        nomor: { validation: { required: true } },
                        jns_trans: { validation: { required: true } },
                        divisi: { validation: { required: true } },
                        kd_bank: { validation: { required: true } },
                        kartu: { validation: { required: true } },
                        tgl_trans: { type: "date", validation: { required: true } },
                        tgl_jth_tempo: { type: "date", validation: { required: true } },
                        desc_Data: { type: "string", validation: { required: true } },
                        Desc_Data: { type: "string", validation: { required: true } },
                        nama_bank: { type: "string", validation: { required: true } },
                        nama_customer: { type: "string", validation: { required: true } },
                        kd_valuta: { type: "string", defaultValue: "IDR", validation: { required: true } },
                        kurs_valuta: { type: "number", defaultValue: 1, validation: { required: true } },
                        jml_trans: { type: "number", validation: { required: true } },
                        keterangan: { type: "string" },
                        jns_giro_desc: { type: "string", validation: { required: true } },
                        nama_departemen: { type: "string", validation: { required: true } },
                        bank_asal: { type: "string", validation: { required: true } },
                    }
                }

            },
            aggregate: [
                { field: "jml_trans", aggregate: "sum" },
            ],
            pageSize: optionsGrid.pageSize
        },
        sortable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true

    }).data("kendoGrid");

}
function onChange(e) {
//    var value;
    dsgiro = [];
    //selected = [];
    var rows = e.sender.select();
    total = 0;
    //value = 0;
   
    
    
//    rows.each(function (e) {
//        var grid = $("#gvGiro").data("kendoGrid");
//        var dataItem = grid.dataItem(this);

//        selected.push(dataItem);
//        value += dataItem.jml_trans * 1;
       
//    })
//    //var grid = $("#gvGiro").data("kendoGrid");
//    //var dataItem = grid.dataItem(this);
//    //grid.select().each(function () {
        
//    //    selected.push(grid.dataItem(this));
//    //});


   
   
//        //value += dataItem.jml_trans * 1;
        
//        //$("#TfGiro").text(total);
//        //$("#TfGiro").readonly();

//        //if (_GridDO) {
//        //    var requestData = _GridDO.dataSource.data();
//        console.log(selected);
//        //    var value = 0;
//    //for (var i1 = 0; i1 < selected.length; i1++) {
//    //    //if (dataItem.nomor == selected[i1].nomor) {
//    //    //    value -= selected[i1].jml_trans * 1;
//    //    //    selected.splice(i1, 1);
//    //    //} else {
//    //        value += selected[i1].jml_trans * 1;
//    //   // }
//    //     }
//        // total = value;
//        //}
//        total = value;
//    //})

//    dsgiro = selected;
};







function BankTujuandropdown(container, options) {
    $('<input required data-text-field="nama_bank" data-value-field="nama_bank" data-bind="value:' + options.field + '" id="kd_bank" name="kd_bank" />')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_bank",
            dataValueField: "nama_bank",
            optionLabel: "Please Select...",

            dataSource: BankTujuanCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kd_bank", dataItem.kd_bank);
            }
        });
}

function BankAsaldropdown(container, options) {
    $('<input required data-text-field="Desc_Data" data-value-field="kd_bank" data-bind="value:' + options.field + '" id="bank_asal" name="bank_asal" />')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Desc_Data",
            dataValueField: "Desc_Data",
            optionLabel: "Please Select...",

            dataSource: BankAsalCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("bank_asal", dataItem.Id_Data);
            }
        });
}

function Kartudropdown(container, options) {
    $('<input required data-text-field="nama" data-value-field="nama" data-bind="value:' + options.field + '" id="kartu" name="kartu" />')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama",
            dataValueField: "nama",
            optionLabel: "Please Select...",
            filter: "contains",
            dataSource: KartuCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kartu", dataItem.kode);
            }
        });
}


function Divisidropdown(container, options) {
    $('<input required data-text-field="Nama_Departemen" data-value-field="Nama_Departemen" data-bind="value:' + options.field + '" id="divisi" name="divisi" />')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Nama_Departemen",
            dataValueField: "Nama_Departemen",
            optionLabel: "Please Select...",

            dataSource: DivisiCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("divisi", dataItem.Kd_Departemen);
            }
        });
}

function Jenisdropdown(container, options) {
    $('<input required data-text-field="jns_giro" data-value-field="kd_jns_giro" data-bind="value:' + options.field + '" id="jns_giro" name="jns_giro" />')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "jns_giro",
            dataValueField: "kd_jns_giro",
            optionLabel: "Please Select...",

            dataSource: JenisGiroCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                console.log(dataItem);
                options.model.set("jns_giro", dataItem.kd_jns_giro);
            }
        });
}

function rekPernjualan2dropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '" id="rek_penjualan2" name="rek_penjualan2"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            optionLabel: "Please Select...",
            dataSource: PersediaanCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_penjualan2", dataItem.rek_persediaan);
            }
        });
}

function getCustomer() {
    return $.ajax({
        type: "POST",
        cache: false,
        url: urlGetCustomer,
        success: function (data) {
            $("#Kd_Customer").empty();
            $("#Kd_Customer").append('<option value="" selected disabled>Please select</option>');
            for (var i = 0; i < data.length; i++) {
                $("#Kd_Customer").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
            }
            $('#Kd_Customer').selectpicker('refresh');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getValuta() {
    return $.ajax({
        type: "POST",
        cache: false,
        url: urlGetValuta,
        success: function (data) {
            $("#valuta").empty();
            $("#valuta").append('<option value="" selected disabled>Please select</option>');
            for (var i = 0; i < data.length; i++) {
                $("#valuta").append('<option value="' + data[i].kode_Valuta + '">' + data[i].nama_Valuta + '</option>');
            }
            $('select[name=valuta]').val('IDR');
            $('#valuta').selectpicker('refresh');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getJnsBayar() {
    return $.ajax({
        type: "POST",
        cache: false,
        url: urlGetJnsBayar,
        success: function (data) {
            console.log(data);
            $("#JnsPembayaran").empty();
            $("#JnsPembayaran").append('<option value="" selected disabled>Please select</option>');
            for (var i = 0; i < data.length; i++) {
                $("#JnsPembayaran").append('<option value="' + data[i].id_Data + '">' + data[i].desc_Data + '</option>');
            }

            $('#JnsPembayaran').selectpicker('refresh');
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
        success: function (data) {
            $("#Bank").empty();
            $("#Bank").append('<option value="" selected disabled>Please select</option>');
            for (var i = 0; i < data.length; i++) {
                $("#Bank").append('<option value="' + data[i].kd_bank + '">' + data[i].nama_bank + '</option>');
            }
            $('#Bank').selectpicker('refresh');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getDataAR(ar) {
    var urlLink = urlGetData;
    var filterdata = {
        id: ar
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            InvoiceDS = [];
            //InvoiceDS = result;
            nama = result[0].nama;
            alamat = result[0].alamat;
            wa = result[0].wa;
            telp = result[0].telp;
            //console.log(JSON.stringify(InvoiceDS));

            hide();
            if (result[0].jml_transfer > 0) { //Transfer
                $("#DivTF").show();
                $("#DivNoGiroCek").hide();
                $("#DivGiroCek").hide();
                $("#DivBank").show();
              
                $('#chktransfer').prop("checked", true);
                var numeric = $("#STransfer").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#TfTransfer").getKendoNumericTextBox();
                $("#TfTransfer").val(result[0].jml_transfer);
                numeric1.focus();
            }

            if (result[0].jml_giro > 0) { //Giro Cek
                $("#DivNoGiroCek").hide();
                $("#DivGiroCek").show();
                if (result[0].jml_transfer == 0) {
                    $("#DivBank").hide();

                    $("#DivTF").hide();
                }
               
                $('#chkgiro').prop("checked", true);
                var numeric = $("#STransferGiro").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#TfGiro").getKendoNumericTextBox();
                $("#TfGiro").val(result[0].jml_giro);
                numeric1.focus();
            }

            if (result[0].jml_tunai > 0) {//Tunai
                var numeric = $("#STransferTunai").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#TfTunai").getKendoNumericTextBox();
                $("#TfTunai").val(result[0].jml_tunai);
                numeric1.focus();
                
                $('#chktunai').prop("checked", true);
                $("#DivTN").show();
                //$("#DivTF").s();
                //$("#DivNoGiroCek").hide();
                //$('#addNew').show();
                //$("#DivGiroCek").hide();
                //$("#DivBank").hide();
            } 

            $('#addNew').hide();
            $("#NoTransaksi").val(ar);
            $("#Keterangan").val(result[0].keterangan);
            $("#tanggalfrom").val(result[0].tgl_trans);
            $("#NoReferensi").val(result[0].no_ref1);
           // $("#valuta").val(result[0].kd_valuta);
           // $("#kurs").val(result[0].kurs_valuta);
           // $("#JnsPembayaran").val(result[0].Jns_bayar);
            $("#NoGiro").val(result[0].kd_giro);
            $("#GT").val(result[0].jml_val_trans);
            $("#SGrand").val(0);
            //$("#TfGiro").val(result[0].jml_bayar);
            $("#STransferGiro").val(result[0].jml_bayar - result[0].jml_val_trans);
            $("#TfTunai").val(result[0].jml_tunai);
            $("#STransferTunai").val(result[0].jml_bayar - result[0].jml_val_trans);
            $("#valuta option[value='" + result[0].kd_valuta + "']").attr("selected", "selected");
            $('#valuta').selectpicker('refresh');
            $('#valuta').selectpicker('render');
            $("#valuta").attr("disabled", "disabled");
            $("#kurs option[value='" + result[0].kurs_valuta + "']").attr("selected", "selected");
            $('#kurs').selectpicker('refresh');
            $('#kurs').selectpicker('render');
            $("#kurs").attr("disabled", "disabled");
            $("#Bank option[value='" + result[0].kd_bank + "']").attr("selected", "selected");
            $('#Bank').selectpicker('refresh');
            $('#Bank').selectpicker('render');
            $("#Bank").attr("disabled", "disabled");
            $("#Kd_Customer option[value='" + result[0].kd_kartu + "']").attr("selected", "selected");
            $('#Kd_Customer').selectpicker('refresh');
            $('#Kd_Customer').selectpicker('render');
            $("#Kd_Customer").attr("disabled", "disabled");
            $("#JnsPembayaran option[value='" + result[0].jns_bayar + "']").attr("selected", "selected");
            $('#JnsPembayaran').selectpicker('refresh');
            $('#JnsPembayaran').selectpicker('render');
            $("#JnsPembayaran").attr("disabled", "disabled");
          //  $("#Kd_Customer").attr("disabled", "disabled");


            //$("#penerima").val(result[0].penerima);
            $("#Keterangan").attr("disabled", "disabled");
            $("#tanggalfrom").attr("disabled", "disabled");
            $("#NoReferensi").attr("disabled", "disabled");
           // $("#valuta").attr("disabled", "disabled");
            //$("#kurs").attr("disabled", "disabled");
            $("#STransferGiro").attr("disabled", "disabled");
            
            $("#STransferTunai").attr("disabled", "disabled");
            $("#TfTunai").attr("disabled", "disabled");
            $("#TfTransfer").attr("disabled", "disabled");
            $("#TfGiro").attr("disabled", "disabled");

            $("#addNew").hide();
            $("#save").hide();
            $("#new").show();

            if (_GridDO != undefined) {
            $("#GridStokOpname").kendoGrid('destroy').empty();
            }
            ///if ($('#GridStokOpname').hasClass("k-grid")) {
             
           // }

            columnGrid = [
                { field: "no_trans", title: "No Invoice", width: "50px", hidden: true },
                { field: "prev_no_inv", title: "No Invoice", width: "100px", editor: invoiceDropDownEditor },
                { field: "jml_tagihan", title: "Jml. Tagihan", editor: jmltagihanLabel, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "jml_bayar", title: "Jml. Bayar", editor: bayarNumeric, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "jml_diskon", title: "Jml. Potongan", editor: potongan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "jml_pembulatan", title: "Jml. Pembulatan", editor: pembulatan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "pendp_lain", title: "Pendapatan Lain", editor: pendapatanlain, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "subtotal", title: "Sub Total", editor: subtotal, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" }

                    //(tagihan - potongan - pembulatan - pendapatanlain) - value
                //{ field: "keterangan", title: "Keterangan", width: "30px" }

            ];
         

            for (var i = 0; i <= result[0].detail.length - 1; i++) {
                var subtotaljumlah = (result[0].detail[i].jml_tagihan - result[0].detail[i].jml_diskon - result[0].detail[i].jml_pembulatan - result[0].detail[i].pendp_lain) - result[0].detail[i].jml_bayar
                 //(tagihan - potongan - pembulatan - pendapatanlain) - value
                InvoiceDS.push({
                    no_trans: result[0].detail[i].no_trans,
                    prev_no_inv: result[0].detail[i].prev_no_inv,
                    jml_tagihan: result[0].detail[i].jml_tagihan,
                    jml_bayar: result[0].detail[i].jml_bayar,
                    jml_diskon: result[0].detail[i].jml_diskon,
                    jml_pembulatan: result[0].detail[i].jml_pembulatan,
                    pendp_lain: result[0].detail[i].pendp_lain,
                    subtotal: subtotaljumlah

                });
            }
            bindGrid();
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function newCreate() {
    window.location.href = urlCreate;
}

function getGiro() {
    var KodeCabang = null;
    var KodeCustomer = null;
    var Nomor = null;
    return $.ajax({
        url: urlGetGiro,
        data: { kdcb: KodeCabang, kdcust: KodeCustomer, nomor: Nomor },
        success: function (data) {
            $("#NoGiro").empty();
            $("#NoGiro").append('<option value="" selected disabled>Please select</option>');
            for (var i = 0; i < data.length; i++) {
                $("#NoGiro").append('<option value="' + data[i].kd_bank + '">' + data[i].nama_bank + '</option>');
            }

            $('#NoGiro').selectpicker('refresh');
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    _GridDO = $("#GridStokOpname").kendoGrid({
        columns: columnGrid,
        dataSource: {
            data: InvoiceDS,
            schema: {
                model: {
                    fields: {
                        no_trans: { type: "string", editable: true },
                        jml_tagihan: { type: "number", editable: true },
                        jml_bayar: { type: "number", editable: true },
                        jml_diskon: { type: "number", editable: true },
                        jml_pembulatan: { type: "number", editable: true },
                        pendp_lain: { type: "number", editable: true },
                        subtotal: { type: "number", editable: true }
                    }
                }
            },
            aggregate: [
                { field: "subtotal", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            $("#tagihan").text(e.model.jml_tagihan);
            var jmlbayar = $("#jml_bayar").data("kendoNumericTextBox");
            jmlbayar.value(e.model.jml_bayar);
            $("#potongan").text(e.model.jml_diskon);
            $("#pembulatan").text(e.model.jml_pembulatan);
            $("#pendapatanlain").text(e.model.pendp_lain);

            if (e.model.no_trans != "") {
                var explode = function () {
                    var noinv = $("#no_inv").data("kendoDropDownList");
                    var invIndex = InvoiceList.findIndex(x => x.no_inv == e.model.no_inv);
                    noinv.select(invIndex + 1);
                };

                setTimeout(explode, 500);
            }
            addCustomCssButtonCommand();
        },
        save: function (e) {
            var grid = $("#GridStokOpname").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();
            //var TotalTF = $("#TfTunai").val() > 0 ? $("#TfTunai").val() : $("#TfGiro").val();
            var TotalTF = $("#TfTransfer").val() + $("#TfTunai").val()  + $("#TfGiro").val();
            var GrandTotal = 0;
            var GrandTagihan = 0;
            for (var i = 0; i <= ds.length - 1; i++) {
                if (ds[i].jml_bayar > ds[i].jml_tagihan) {
                    alert("jumlah pembayaran melebihi jumlah tagihan");
                    ds[i].jml_bayar = 0;
                    return false;
                }
                ds[i].subtotal = ds[i].jml_bayar;
                GrandTotal += ds[i].subtotal;
                GrandTagihan += ds[i].jml_tagihan;

            }
            InvoiceDS = ds;

            $("#JumlahTotalBayar").val(ds.length);
            $("#JumlahNominalBayar").val(GrandTotal);
            $("#JumlahNominalTagihan").val(GrandTagihan);
            $("#GT").val(GrandTotal);
            $("#SGrand").val(TotalTF - GrandTotal);
            if ($("#TfTunai").val() > 0) {
                $("#STransferTunai").val(TotalTF - GrandTotal);
                var numeric = $("#STransferTunai").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#GT").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#SGrand").getKendoNumericTextBox();
                numeric2.focus();
            } else if ($("#TfTransfer").val() > 0) {
                $("#STransferTunai").val(TotalTF - GrandTotal);
                var numeric = $("#STransfer").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#GT").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#SGrand").getKendoNumericTextBox();
                numeric2.focus();
            } else if ($("#TfTransferGiro").val() > 0) {
                $("#STransferGiro").val(TotalTF - GrandTotal);
                var numeric = $("#STransferGiro").getKendoNumericTextBox();
                numeric.focus();
                var numeric1 = $("#GT").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#SGrand").getKendoNumericTextBox();
                numeric2.focus();
            } else {

                var numeric1 = $("#GT").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#SGrand").getKendoNumericTextBox();
                numeric2.focus();
            }

            $('#GridStokOpname').kendoGrid('destroy').empty();
            bindGrid();
            //ItemCalc();
            //onAddNewRow();
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBinding: function (e) {

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

function setsisatransfer() {

    var giro = parseFloat($("#TfGiro").val());
    var tunai = parseFloat($("#TfTunai").val());
    var transfer = parseFloat($("#TfTransfer").val());
    var tottunaigrid = parseFloat($("#GT").val());
    var sisa = tottunaigrid - (giro + tunai + transfer);
    $("#STransfer").val(sisa);
    var numeric = $("#STransfer").getKendoNumericTextBox();
    numeric.focus();
    $("#SGrand").val(sisa);
    var numeric1 = $("#SGrand").getKendoNumericTextBox();
    numeric1.focus();
}

function setsisa() {
  
    var giro = parseFloat($("#TfGiro").val());
    var tunai = parseFloat($("#TfTunai").val());
    var transfer = parseFloat($("#TfTransfer").val());
    var tottunaigrid = parseFloat($("#GT").val());
    var sisa = tottunaigrid - (giro + tunai + transfer);
    $("#STransferTunai").val(sisa);
    var numeric = $("#STransferTunai").getKendoNumericTextBox();
    numeric.focus();
    $("#SGrand").val(sisa);
    var numeric1 = $("#SGrand").getKendoNumericTextBox();
    numeric1.focus();
}
function setsisagiro() {
    var giro = parseFloat($("#TfGiro").val());
    var tunai = parseFloat($("#TfTunai").val());
    var transfer = parseFloat($("#TfTransfer").val());
    var tottunaigrid = parseFloat($("#GT").val());
    var sisa = tottunaigrid - (giro + tunai + transfer);
    $("#STransferGiro").val(sisa);
    var numeric = $("#STransferGiro").getKendoNumericTextBox();
    numeric.focus();
    $("#SGrand").val(sisa);
    var numeric1 = $("#SGrand").getKendoNumericTextBox();
    numeric1.focus();
}

function onAddNewRow() {
    $.when(GetInvoice()).done(function () {
        var grid = $("#GridStokOpname").data("kendoGrid");
        grid.addRow();
        //$('#no_inv').data("kendoDropDownList").open();

    });
}

function jmltagihanLabel(container, options) {
    var input = $('<label id="tagihan" />');
    input.appendTo(container);
}

function potongan(container, options) {
    var input = $('<label id="potongan" />');
    input.appendTo(container);
}

function pembulatan(container, options) {
    var input = $('<label id="pembulatan" />');
    input.appendTo(container);
}

function pendapatanlain(container, options) {
    var input = $('<label id="pendapatanlain" />');
    input.appendTo(container);
}

function subtotal(container, options) {
    var input = $('<label id="subtotal" />');
    input.appendTo(container);
}

function bayarNumeric(container, options) {
    var input = $('<input id="jml_bayar" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "{0:n2}",
        decimals: 2,
        min: 0,
        max: parseFloat($("#tagihan").text()),
        change: function (e) {
            var value = this.value();
            var invoice = _GridDO.dataItem($(e.sender.element).closest("tr"));
            invoice.jml_bayar = value;
            invoice.no_trans = $("#no_inv").val();
            invoice.jml_tagihan = $("#tagihan").text();
            invoice.jml_diskon = $("#potongan").text();
            invoice.jml_pembulatan = $("#pembulatan").text();
            invoice.pendp_lain = $("#pendapatanlain").text();

            var tagihan = invoice.jml_tagihan;
            var potongan = invoice.jml_diskon;
            var pembulatan = invoice.jml_pembulatan;
            var pendapatanlain = invoice.pendp_lain;
            var subtotal = (tagihan - potongan - pembulatan - pendapatanlain) - value;
            $("#subtotal").text(value);
        }
    });
}


function onCustomerChanged() {
    if (Mode != "VIEW") {
     

            $("#GridStokOpname").data("kendoGrid").dataSource.data([]);
        
       
    }
}


function GetInvoice() {
    InvoiceList = [];
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
            //console.log(InvoiceList);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function invoiceDropDownEditor(container, options) {
    var input = $('<input required id="no_inv" name="no_inv">');
   
    input.appendTo(container);
    input.kendoMultiColumnComboBox({
        valuePrimitive: true,
        dataTextField: "no_inv",
        dataValueField: "no_inv",
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
                template: "#= currency(data.jml_akhir)#",
                width: 200
            },
        ],
        dataSource: InvoiceList,
        filter: "contains",
        optionLabel: "Pilih Invoice",
        virtual: {
            valueMapper: function (options) {
                options.success([options.success || 0]);
            }
        },
        template: "<span data-id='${data.no_inv}' data-inv='${data.no_inv}' data-tagihan='${data.jml_tagihan}' data-diskon='${data.jml_diskon}' data-pembulatan='0' data-pendapatanlain='${data.jml_pend_lain}'>${data.no_inv}</span>",
        select: function (e) {
            var id = e.dataItem.no_inv;
            var Invoice = e.dataItem.no_inv;
            var JmlTagihan = e.dataItem.jml_tagihan;
            var Diskon = e.dataItem.Diskon;
            var Pembulatan = e.dataItem.jml_diskon;
            var PendapatanLain = e.dataItem.PendapatanLain;
           
          
            var invoice = _GridDO.dataItem($(e.sender.element).closest("tr"));
            invoice.no_trans = Invoice;
            invoice.prev_no_inv = Invoice;
            invoice.jml_tagihan = JmlTagihan >= 0 ? JmlTagihan : 0;
            invoice.jml_diskon = Diskon >= 0 ? Diskon : 0;
            invoice.jml_pembulatan = Pembulatan >= 0 ? Pembulatan : 0;
            invoice.pendp_lain = PendapatanLain >= 0 ? PendapatanLain : 0;

            JmlTagihan >= 0 ? $("#tagihan").text(formatNumber(JmlTagihan)) : $("#tagihan").text('0');
            Diskon >= 0 ? $("#potongan").text(Diskon) : $("#potongan").text('0');
            Pembulatan >= 0 ? $("#pembulatan").text(Pembulatan) : $("#pembulatan").text('0')
            PendapatanLain >= 0 ? $("#pendapatanlain").text(PendapatanLain) : $("#pendapatanlain").text('0');
            var subtotal = (invoice.jml_tagihan - invoice.jml_diskon - invoice.jml_pembulatan - invoice.pendp_lain);
            invoice.subtotal = subtotal >= 0 ? subtotal : 0;

            //subtotal >= 0 ? $("#subtotal").text(subtotal) : $("#subtotal").text('0');
            $("#subtotal").text(0);

        }
    });
}

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,')
}

function currency(data) {
    return kendo.toString(data, "Rp ##,##.00");
}

function dateTime(data) {
    return kendo.toString(kendo.parseDate(data.substring(0, 10)), "dd/MM/yyyy");
}

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    //console.log("GT: " + $("#GT").val());
    //console.log("JNS: " + $("#JnsPembayaran").val());
    //console.log("TF: " + $("#TfTunai").val());
    //console.log("SISA: " + $("#STransferTunai").val());


    if ($("#GT").val() > 0 && $("#TfTransfer").val() > 0 && $("#Bank").val() != "" && $("#SGrand").val() == 0 ) {
    SaveData();

    //} else if ($("#GT").val() > 0  && $("#TfTransfer").val() > 0 && $("#STransfer").val() == 0 && $("Bank").val() != "") {
    //    SaveData();

    //} else if ($("#GT").val() > 0  && $("#TfGiro").val() > 0 && $("#STransferGiro").val() == 0) {
    //    SaveData();

    }
    else if ($("#GT").val() > 0 && $("#SGrand").val() == 0) {
        SaveData();
    }

    else {
        


        if ($("#GT").val() > 0 && $("#TfTransfer").val() > 0 && $("#Bank").val() == "") {
            swal({
                type: 'warning',
                title: 'Error Bank',
                html: 'Bank Harus Dipilih',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d9534f'
            })
        } else {
            swal({
                type: 'warning',
                title: 'Error Sisa Transfer',
                html: 'Sisa Transfer Tidak Boleh Lebih atau Kurang dari 0',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d9534f'
            })
        }
    }
}

function onPrintTokoClicked() {
    //console.log(JSON.stringify(InvoiceDS));
    var notrans = $("#NoTransaksi").val();
    var keteangantxt = $("#Keterangan").val();
    var tgltrans = $("#tanggalfrom").val();
    var noref = $("#NoReferensi").val();
    var namacustomer = $("#Kd_Customer option:selected").text();
    var valuta = $("#valuta option:selected").text();
    var kurs = 1;
    var kdjnis = $("#JnsPembayaran option:selected").val();
    var jnspembayaran = $("#JnsPembayaran option:selected").text();
    var Bank = $("#Bank option:selected").text();
   
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
        cmds += nama; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)
        cmds += alamat;
        cmds += newLine;
        cmds += 'Telp:' + telp + 'WA:' + wa;
        cmds += newLine;

        //cmds += ' ' + Opnameds[0].alamat;
        //cmds += newLine;
        //cmds += 'Telp:' + Opnameds[0].fax1 + ' WA:' + Opnameds[0].fax1;
        //cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '         Pelunasan AR'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
        cmds += 'NO TRANS   : ' + notrans;
        cmds += newLine;
        cmds += 'TANGGAL : ' + kendo.toString(tgltrans, "dd MM yyyy");
        cmds += newLine;
        cmds += 'No Referensi : ' + noref;
        cmds += newLine;
        cmds += 'Customer : ' + namacustomer;
        cmds += newLine;
        cmds += 'Valuta : ' + valuta;
        cmds += newLine;
        cmds += 'Kurs : ' + kurs;
        cmds += newLine;
        cmds += 'Jenis Pembayaran : ' + jnspembayaran;
        cmds += newLine;
        if (kdjnis == "01") {
            cmds += 'No Giro : ' + $("#NoGiro").val();;
            cmds += newLine;
            cmds += 'Jumlah Bayar Giro : ' + $("#TfGiro").val();
            cmds += newLine;
            cmds += 'Jumlah Sisa Giro : ' + $("#STransferGiro").val();
            cmds += newLine;
        } else if (kdjnis == "04") {
            cmds += 'Bank : ' + Bank;
            cmds += newLine;
            cmds += 'Jumlah Bayar Transfer : ' + $("#TfTunai").val();
            cmds += newLine;
            cmds += 'Jumlah Sisa Transfer : ' + $("#STransferTunai").val();
            cmds += newLine;
        } else if (kdjnis == "03") {
            cmds += 'Jumlah Bayar Tunai : ' + $("#TfTunai").val();
            cmds += newLine;
            cmds += 'Jumlah Sisa Tunai : ' + $("#STransferTunai").val();
            cmds += newLine;
        }

        cmds += 'Keterangan: ' + keteangantxt;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;

        var totjmltrans = 0;
        var jmlitem = 0;

        for (var i = 0; i <= InvoiceDS.length - 1; i++) {
            cmds += 'No Invoice : ' + InvoiceDS[i].prev_no_inv;
            cmds += newLine;
            cmds += 'Jumlah Tagihan : ' + InvoiceDS[i].jml_tagihan.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;
            cmds += 'Jumlah Bayar : ' + InvoiceDS[i].jml_bayar.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;
            cmds += 'Jumlah Potongan : ' + InvoiceDS[i].jml_diskon.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;
            cmds += 'Jumlah Pembulatan : ' + InvoiceDS[i].jml_pembulatan;
            cmds += newLine;
            cmds += 'Pendapatan Lain : ' + InvoiceDS[i].pendp_lain.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;
            cmds += 'Sub Total : ' + InvoiceDS[i].subtotal.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;


            //var qtyIn = Opnameds[i].qty_out.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            totjmltrans += InvoiceDS[i].subtotal * 1;
            //cmds += 'Keterangan : ' + Opnameds[i].keterangan;
            //cmds += newLine;


        }
        var jmlitem = InvoiceDS.length;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Tot. Item : ' + jmlitem.toString();
        cmds += newLine;
        cmds += 'Tot.Trans   : ' + totjmltrans.toLocaleString('id-ID', { maximumFractionDigits: 2 });
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Diperiksa Penerima';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += ' (      )  (      )';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;


        cpj.printerCommands = cmds;
        //Send print job to printer!
        cpj.sendToClient();
    }
}

function SaveData() {

    var SP_REFFval;
    var myJsonString = JSON.stringify(dsgiro);
    var json;
    var totalbayar = parseFloat($("#TfGiro").val()) + parseFloat($("#TfTransfer").val()) + parseFloat($("#TfTunai").val());
    if (dsgiro.length > 0) {
        json = _GridDO1.dataSource.data().toJSON()
    } else {
        json = [];
    }
    urlSaveData = urlSave;
    var savedata = {
        Kd_cabang: "",
        tgl_trans: $("#tanggalfrom").val(),

        no_ref1: $("#NoReferensi").val(),
        no_ref2: "",
        no_ref3: "",
        thnbln: "",
        jml_giro: $("#TfGiro").val(),
        jml_transfer: $("#TfTransfer").val(),
        jml_tunai: $("#TfTunai").val(),
        kd_kartu: $("#Kd_Customer").val(),
        kd_valuta: $("#valuta").val(),
        kurs_valuta: $("#kurs").val(),
        jml_val_trans: totalbayar,
        jml_rp_trans: totalbayar,
        jml_tagihan: $("#JumlahNominalTagihan").val(),
        jml_bayar: totalbayar,
        jml_bayargiro: totalbayar,
        jml_bayartf: totalbayar,
        Jns_bayar: $("#JnsPembayaran").val(),
        jns_giro_trans: "",
        no_giro: $("#NoGiro").val(),
        kd_bank: $("#Bank").val(),
        tgl_posting: "",
        no_posting: "",
        tgl_batal: "",
        keterangan: $("#Keterangan").val(),
        status: "",
        kd_buku_besar: "",
        Last_create_date: "",
        Last_created_by: "",
        Last_update_date: "",
        Last_updated_by: "",
        Program_name: "",
        no_do: "",
        jml_titipantr: $("#STransfer").val(),
        jml_titipangr: $("#STransferGiro").val(),
        jml_titipan: $("#STransferTunai").val(),
        no_batal: "",
        giro: json,
        detail: _GridDO.dataSource.data().toJSON()
       
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
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        //swal({
                        //    type: 'warning',
                        //    title: 'Apakah anda akan mencetak AR?',
                        //    html: '',
                        //    showCancelButton: true,
                        //    confirmButtonColor: '#3085d6',
                        //    cancelButtonColor: '#d9534f'
                        //}).then(function (isConfirm) {
                        //    if (isConfirm.value === true) {
                        //        startSpinner('loading..', 1);

                        //        $.when(printSJNew(result.result)).done(function () {
                        //            startSpinner('loading..', 0);
                        //            window.location.href = urlCreate;
                        //        });
                        //    }
                        //    else {
                        //        window.location.href = urlCreate;
                        //    }
                        //});
                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
                    }
                }
            });
        }
    });



    /*var ppnval;
    if ($('#checkbox2').is(":checked")) {
        ppnval = "Y";
    }
    else {
        ppnval = "T";
    }
    if ($('#cb_ongkir').is(":checked")) {
        ex_jasa = "Y";
    }
    else {
        ex_jasa = "N";
    }
    var no = "";
    var urlSaveData
    if (Mode == "RETUR") {
        no = $("#DONumber").val();
        SP_REFFval = $("#RefNo1").val();
        urlSaveData = urlSaveRetur;
    }
    else if (Mode == "EDIT") {
        no = $("#DONumber").val();
        urlSaveData = urlEdit;
    }
    else {
        urlSaveData = urlSave;
    }
    var savedata = {
        No_sp: no,
        Jenis_sp: $("#jenisDO").val(),
        Kd_Customer: $("#Kd_Customer").val(),
        Atas_Nama: $("#Kd_Customer option:selected").text(),
        Tgl_sp: $("#tanggal").val(),
        Tgl_Kirim_Marketing: $("#tanggalkirim").val(),
        Kd_sales: $("#kasir").val(),
        Keterangan: $("#Keterangan").val(),
        Almt_pnrm: $("#AlamatKirim").val(),
        SP_REFF: $("#RefNo1").val(),
        SP_REFF2: $("#RefNo2").val(),
        JML_RP_TRANS: $("#TotalRupiah").val(),
        JML_VALAS_TRANS: $("#TotalRupiah").val(),
        Jatuh_Tempo: 0,
        Flag_Ppn: ppnval,
        inc_ongkir: ex_jasa,
        PPn: 0,
        dp: $("#dp").val(),
        Discount: $("#Diskon").val(),
        Biaya: $("#ongkir").val(),
        details: _GridDO.dataSource.data().toJSON()
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

                        //Swal.fire({
                        //    type: 'success',
                        //    title: 'Success',
                        //    html: "Save Successfully"
                        //});
                        if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                            $.when(printNotaNew(result.result)).done(function () {
                                startSpinner('loading..', 0);
                                if (Mode == "RETUR") {
                                    $.when(printNotaNew(SP_REFFval)).done(function () {
                                        swal({
                                            type: 'warning',
                                            title: 'Apakah anda akan mencetak SJ?',
                                            html: '',
                                            showCancelButton: true,
                                            confirmButtonColor: '#3085d6',
                                            cancelButtonColor: '#d9534f'
                                        }).then(function (isConfirm) {
                                            if (isConfirm.value === true) {
                                                startSpinner('loading..', 1);

                                                $.when(printSJNew(result.result)).done(function () {
                                                    startSpinner('loading..', 0);
                                                    window.location.href = urlCreate;
                                                });
                                            }
                                            else {
                                                window.location.href = urlCreate;
                                            }
                                        });
                                    });
                                }
                                else {
                                    swal({
                                        type: 'warning',
                                        title: 'Apakah anda akan mencetak SJ?',
                                        html: '',
                                        showCancelButton: true,
                                        confirmButtonColor: '#3085d6',
                                        cancelButtonColor: '#d9534f'
                                    }).then(function (isConfirm) {
                                        if (isConfirm.value === true) {
                                            startSpinner('loading..', 1);

                                            $.when(printSJNew(result.result)).done(function () {
                                                startSpinner('loading..', 0);
                                                window.location.href = urlCreate;
                                            });
                                        }
                                        else {
                                            window.location.href = urlCreate;
                                        }
                                    });
                                }

                            });
                        }
                        else {
                            startSpinner('loading..', 0);
                            window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
                        }


                    }
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });

        }
    });*/
}
