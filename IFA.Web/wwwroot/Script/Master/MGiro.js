var brgDS = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 20
};
$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: urlGetData,
                complete: function (e) {
                    console.log(e);
                }
            },
            update: {
                type: "POST",
                url: urlUpdate,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            destroy: {
                type: "POST",
                url: urlDelete,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            create: {
                type: "POST",
                url: urlSave,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridBrg").data("kendoGrid").dataSource.read();
                }

            },
            parameterMap: function (option, operation) {
                if (operation !== "read") {
                    return kendo.stringify(option.models);
                }
            }
        },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "no_jur",
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
                    kd_valuta: { type: "string", defaultValue: "IDR", validation: { required: true }},
                    kurs_valuta: { type: "number", defaultValue: 1, validation: { required: true } },
                    jml_trans: { type: "number", validation: { required: true } },
                    keterangan: { type: "string" },
                    jns_giro_desc: { type: "string", validation: { required: true } },
                    nama_departemen: { type: "string", validation: { required: true } },
                    bank_asal: { type: "string", validation: { required: true } },


                }
            }
        }
    });

    $("#GridBrg").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        groupable: true,
        sortable: true,
        height: 800,
        filterable: true,
        requestEnd: onRequestEnd,
        //toolbar: ["excel"],
        //excel: {
        //    fileName: "ExportBarang.xlsx", allPages: true, Filterable: true
        //},
        toolbar:
            [{
                name: "create",
                text: "Tambah Giro"      
            },
            {
                name: "excel",
                text: "Export Excels"
            }],
        excel: {
            fileName: "ExportBarang.xlsx", allPages: true, Filterable: true
        },
        columns: [
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
            { field: "jml_trans", title: "Jumlah", filterable: true, width: "200px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", filterable: true, width: "200px" },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "inline",
    });
    
});




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







function onRequestEnd(e) {
    if (e.type === "create") {
        e.sender.read();
    }
    else if (e.type === "update") {
        e.sender.read();
    }
}

//startSpinner('Loading..', 1);
//$.when(getData()).done(function () {
//    bindGrid();
//    prepareActionGrid();
//    startSpinner('loading..', 0);
//});
//});


function getData() {
    var urlLink = urlGetData;

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

function bindGrid() {
    $("#GridBrg").kendoGrid({
        columns: [

            { field: "nomor", title: "Nomor Giro", filterable: true, width: "200px" },
            { field: "tgl_trans", title: "Tanggal", filterable: true, width: "200px" },
            { field: "tgl_jth_tempo", title: "Tgl Jatuh tempo", filterable: true, width: "200px" },
            { field: "jns_giro", title: "Jenis", filterable: true, width: "200px", editor: Jenisdropdown },
            { field: "Nama_Departemen", title: "Divisi", filterable: true, width: "200px", editor: Divisidropdown },
            { field: "Desc_Data", title: "Bank Asal", filterable: true, width: "200px", editor: BankAsaldropdown },
            { field: "nama_bank", title: "Bank Tujuan", filterable: true, width: "200px", editor: BankTujuandropdown },
            { field: "nama", title: "Nama Kartu", filterable: true, width: "200px", editor: Kartudropdown },
            { field: "kd_valuta", title: "Valuta", filterable: true, width: "200px" },
            { field: "kurs_valuta", title: "Kurs", filterable: true, width: "200px" },
            { field: "jml_trans", title: "Jumlah", filterable: true, width: "200px" },
            { field: "keterangan", title: "Keterangan", filterable: true, width: "200px" },
            { field: "Action", width: "200px", template: '<center><a class="btn btn-danger btn-sm hapusData" href="javascript:void(0)" data-id="#=nomor#"><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a> &nbsp; &nbsp; <a class="btn btn-info btn-sm editData" href="javascript:void(0)" data-id="#=nomor#"><i class="glyphicon glyphicon-pencil" aria-hidden="true"></i></a></center>' },

        ],
        dataSource: {
            data: brgDS,
            schema: {
                model: {
                    id: "no_jur",
                    fields: {
                        nomor: { validation: { required: true } },
                        jns_trans: { validation: { required: true } },
                        divisi: { validation: { required: true } },
                        kd_bank: { validation: { required: true } },
                        kartu: { validation: { required: true } },
                        tgl_trans: { type: "date", validation: { required: true } },
                        tgl_jth_tempo: { type: "date", validation: { required: true } },
                        Desc_Data: { type: "string", validation: { required: true } },
                        Nama_Departemen: { type: "string", validation: { required: true } },
                        Desc_Data: { type: "string", validation: { required: true } },
                        nama_bank: { type: "string", validation: { required: true } },
                        nama: { type: "string", validation: { required: true } },
                        kd_valuta: { type: "string", defaultValue: "IDR", validation: { required: true } },
                        kurs_valuta: { type: "number", defaultValue: 1, validation: { required: true } },
                        jml_trans: { type: "number", validation: { required: true } },
                        keterangan: { type: "string", validation: { required: true } }

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
           // $("#Kode_Kota").prop("readonly", true);
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
    //var Kode_Kota = $('#Kode_Kota').val();
    //var Nama_Kota = $('#Nama_Kota').val();
    //var Keterangan = $('#Keterangan').val();

    //validationMessage = '';
    //if (!Kode_Kota) {
    //    validationMessage = validationMessage + 'Kode Kota harus di isi.' + '\n';
    //}
    //if (!Nama_Kota) {
    //    validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    //}

    //if (!Keterangan) {
    //    validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    //}

    //if (Kode_Kota.match(reWhiteSpace)) {
    //    validationMessage = validationMessage + 'Kode Kota tidak boleh ada spasi.' + '\n';
    //}
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
                $('#GridBrg').kendoGrid('destroy').empty();
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
                            $('#GridBrg').kendoGrid('destroy').empty();
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
            $('#GridBrg').kendoGrid('destroy').empty();
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