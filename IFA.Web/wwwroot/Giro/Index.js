var listds = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 20
};
var jenisGirods = [];
var divisids = [];
var bankAsalds = [];
var bankTujuands = [];
var valutads = [];
var savegirods = [];
var kartuds = [];
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(getData()).done(function () {
        $.when(getJenisGiro()).done(function () {
            $.when(getDivisi()).done(function () {
                $.when(getBankAsal()).done(function () {
                    $.when(getBankTujuan()).done(function () {
                        $.when(getValuta()).done(function () {
                            $.when(getKartu()).done(function () {
                                bindGrid();
                                //prepareActionGrid();
                                startSpinner('loading..', 0);
                            });
                        });
                    });
                });
            });
        });
    });
});

function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            listds = [];
            listds = result;
            console.log(JSON.stringify(listds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getJenisGiro() {
    var urlLink = urlGetJenisGiro;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            jenisGirods = [];
            jenisGirods = result;
            console.log(JSON.stringify(jenisGirods));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getDivisi() {
    var urlLink = urlGetDivisi;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            divisids = [];
            divisids = result;
            console.log(JSON.stringify(divisids));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getBankAsal() {
    var urlLink = urlGetBankAsal;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            bankAsalds = [];
            bankAsalds = result;
            console.log(JSON.stringify(bankAsalds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getBankTujuan() {
    var urlLink = urlGetBankTujuan;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            bankTujuands = [];
            bankTujuands = result;
            console.log("bankTujuands: " + JSON.stringify(bankTujuands));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getValuta() {
    var urlLink = urlGetValuta;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            valutads = [];
            valutads = result;
            console.log(JSON.stringify(valutads));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getKartu() {
    var urlLink = urlGetKartu;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            kartuds = [];
            kartuds = result;
            console.log(JSON.stringify(kartuds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function saveGiro() {
    var urlLink = urlSave;

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: savegirods,
        success: function (result) {
            //valutads = [];
            //valutads = result;
            //console.log(JSON.stringify(valutads));
            //startSpinner('Loading..', 1);
            //$.when(getData()).done(function () {
            //    bindGrid;
            //    startSpinner('Loading..', 0);

            //});
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGrid() {
    $("#GridGiro").kendoGrid({
        columns: [

            { field: "nomor", title: "Nomor Giro", width: "100px" },
            { field: "tgl_trans", title: "Tanggal", width: "100px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "tgl_jth_tempo", title: "Tanggal Jatuh Tempo", width: "100px", template: "#= kendo.toString(kendo.parseDate(tgl_jth_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "jenis_giro", title: "Jenis", width: "100px", editor: cboJenisGiro},
            { field: "nama_Departemen", title: "Divisi", width: "100px", editor: cboDivisi},
            { field: "nama_bank_asal", title: "Bank Asal", width: "100px", editor: cboBankAsal},
            { field: "nama_bank", title: "Bank Tujuan", width: "100px", editor: cboBankTujuan },
            { field: "nama_customer", title: "Nama Kartu", width: "100px", editor: cboKartu },
            { field: "nama_Valuta", title: "Valuta", width: "100px", editor: cboValuta },
            { field: "kurs_valuta", title: "Kurs", width: "100px"},
            { field: "jml_trans", title: "Jumlah", width: "100px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", width: "100px" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "150px" }
        ],
        dataSource: {
            data: listds,
            schema: {
                model: {
                    fields: {
                        nomor: { type: "string", validation: { required: true, nullable: false }, defaultValue: ""},
                        tgl_trans: { type: "date", validation: { required: true, nullable: false }},
                        tgl_jth_tempo: { type: "date", validation: { required: true, nullable: false }},
                        jenis_giro: { type: "string", validation: { required: true, nullable: false }},
                        nama_Departemen: { type: "string", validation: { required: true, nullable: false }},
                        nama_bank_asal: { type: "string", validation: { required: true, nullable: false }},
                        nama_bank: { type: "string", validation: { required: true, nullable: false }},
                        nama_Valuta: { type: "string", validation: { required: true, nullable: false }},
                        kurs_valuta: { type: "number", validation: { required: true, nullable: false }, defaultValue: "1"},
                        jml_trans: { type: "number", validation: { required: true, nullable: false }, defaultValue: ""},
                        keterangan: { type: "string"},
                        jns_giro: { type: "string", validation: { required: true, nullable: false }},
                        kd_bank: { type: "string", validation: { required: true, nullable: false }},
                        kd_valuta: { type: "string", validation: { required: true, nullable: false }},
                        divisi: { type: "string", validation: { required: true, nullable: false }},
                        bank_asal: { type: "string", validation: { required: true, nullable: false } },
                        kartu: { type: "string", validation: { required: true, nullable: false }, defaultValue: "CST00001" },
                        nama_customer: { type: "string", validation: { required: true, nullable: false }, defaultValue: "CST00001" }
                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
                //  prepareActionGrid();
            }
        },
        noRecords: true,
        editable: "inline",
        toolbar:
            [{
                name: "create",
                text: "Tambah"
            }],
      
        save: function (e) {
            savegirods = [];
            var datetrans = new Date(e.model.tgl_trans);
            var datetransJ = datetrans.toJSON();
            var datetJTempo = new Date(e.model.tgl_jth_tempo);
            var datetJTempoJ = datetJTempo.toJSON();
            savegirods = {
                "bank_asal": e.model.bank_asal,
                "nomor": e.model.nomor,
                "tgl_trans": datetransJ,
                "tgl_jth_tempo": datetJTempoJ,
                "jenis_giro": e.model.jenis_giro,
                "nama_Departemen": e.model.nama_Departemen,
                "nama_bank_asal": e.model.nama_bank_asal,
                "nama_bank": e.model.nama_bank,
                "nama_Valuta": e.model.nama_Valuta,
                "kurs_valuta": e.model.kurs_valuta,
                "jml_trans": e.model.jml_trans,
                "keterangan": e.model.keterangan,
                "jns_giro": e.model.jns_giro,
                "kd_bank": e.model.kd_bank,
                "kd_valuta": e.model.kd_valuta,
                "divisi": e.model.divisi,
                "bank_asal": e.model.bank_asal,
                "kartu": e.model.kartu,
            };
            startSpinner('loading..', 1);

            $.when(saveGiro()).done(function () {
                $.when(getData()).done(function () {
                    if ($('#GridGiro').hasClass("k-grid")) {
                        $('#GridGiro').kendoGrid('destroy').empty();
                    }
                    bindGrid();
                    startSpinner('loading..', 0);
                });
            });

        },
        edit: function (e) {
            var dropdownlist = $("#nama_Customer").data("kendoDropDownList");
            dropdownlist.list.width("400px");
        }
       
    }).data("kendoGrid");
}

function onTambah() {
    var grid = $("#GridGiro").data("kendoGrid");
    grid.addRow();
}

function cboJenisGiro(container, options) {
    $('<input required data-text-field="desc_Data" data-value-field="id_Data" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "desc_Data",
            dataValueField: "id_Data",
            dataSource: jenisGirods,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("jns_giro", dataItem.id_Data);
            }
        });
}

function cboDivisi(container, options) {
    $('<input required data-text-field="nama_Departemen" data-value-field="kd_Departemen" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_Departemen",
            dataValueField: "kd_Departemen",
            dataSource: divisids,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("divisi", dataItem.kd_Departemen);
            }
        });
}

function cboBankAsal(container, options) {
    $('<input required data-text-field="desc_Data" data-value-field="id_Data" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "desc_Data",
            dataValueField: "id_Data",
            dataSource: bankAsalds,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("bank_asal", dataItem.id_Data);
            }
        });
}

function cboBankTujuan(container, options) {
    $('<input required data-text-field="nama_bank" data-value-field="kd_bank" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_bank",
            dataValueField: "kd_bank",
            dataSource: bankTujuands,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kd_bank", dataItem.kd_bank);
            }
        });
}

function cboValuta(container, options) {
    $('<input required data-text-field="nama_Valuta" data-value-field="kode_Valuta" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_Valuta",
            dataValueField: "kode_Valuta",
            dataSource: valutads,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kd_valuta", dataItem.kode_Valuta);
            }
        });
}

function cboKartu(container, options) {
    $('<input required id="nama_Customer" name="nama_Customer" data-text-field="nama_Customer" data-value-field="kd_Customer" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_Customer",
            dataValueField: "kd_Customer",
            dataSource: kartuds,
            filter: "contains",
            virtual: {
                valueMapper: function (options) {
                    options.success([options.nama_Customer || 0]);
                }
            },
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kartu", dataItem.kd_Customer);
            }
        });
}