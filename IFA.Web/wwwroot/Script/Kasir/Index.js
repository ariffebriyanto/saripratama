var _GridStokOpname;
var Opnameds = [];
var SatuanListParam = [];
var BarangList = [];
var GudangList = [];
var SatuanList = [];
var saldods = [];
var pods = [];
var columnOpname = [
    { field: "nomor", title: "Nomer", width: "100px", hidden: true },
    {
        field: "tgl_trans", title: "Tanggal Trans", width: "120px", format: "{0:dd MMM yyyy}", editor: dateTimeEditor2, hidden: true
    },
    { field: "nama", title: "Kartu", width: "120px", editor: PegawaiDropdown},
    { field: "kd_valuta", title: "Valuta", width: "40px" },
    { field: "kurs_valuta", title: "Kurs", width: "30px"},
    { field: "jml_trans", title: "Jml. Trans", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "keterangan", title: "Keterangan", width: "120px" },
    { command: ["destroy"], title: "Actions", width: "40px" }
];
var editval = true;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    bindGrid();
    $.when(GetPegawai()).done(function () {
        $.when(getData(idOpname)).done(function () {
            startSpinner('loading..', 0);
        });
    });
     

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,textarea,.k-dropdown,button').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            ////console.log(focusable.index(this) + 1);
            //if (focusable.index(this) == 20)
            //{
            //alert("halo");
            //}
            return false;
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

function getData(id) {
    var urlLink = urlGetData;
    var filterdata = {
        nomor: id
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Opnameds = [];
            // ////console.log(result);
           //Opnameds = result;
            //////console.log(JSON.stringify(Opnameds));

            if (result.length > 0) {
                $("#NoTransaksi").val(result[0].nomor);
                $("#Keterangan").val(result[0].keterangan);
                //$("#penerima").val(result[0].penerima);
                $("#Keterangan").attr("disabled", "disabled");
               
                //$("#gudang").val(result[0].gudang_tujuan);

                columnOpname = [
                    { field: "nomor", title: "Nomer", width: "100px", hidden: true },
                    { field: "tgl_trans", title: "Tanggal", width: "160px", hidden: true },
                    { field: "nama", title: "Kartu", width: "120px", editor: PegawaiDropdown },
                    { field: "kd_valuta", title: "Valuta", width: "40px" },
                    { field: "kurs_valuta", title: "Kurs", width: "30px" },
                    { field: "jml_trans", title: "Jml. Trans", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "keterangan", title: "Keterangan", width: "120px" }
                ];
                editval = false;

                $("#addNew").hide();
                $("#save").hide();
                $("#new").show();

                if (_GridStokOpname != undefined) {
                    $("#GridStokOpname").kendoGrid('destroy').empty();
                }

                //Opnameds.push({
                //        tgl_trans: result[0].tgl_trans,
                //        kd_kartu: result[0].kd_kartu,
                //        nama: result[0].nama,
                //        kd_valuta: result[0].kd_valuta,
                //        kurs_valuta: result[0].kurs_valuta,
                //        keterangan: result[0].keterangan,
                //        jml_trans: result[0].jml_trans
                //});

                for (var i = 0; i <= result[0].detail.length - 1; i++) {
                    var rslbb = BarangList.filter(p => p.kode == result[0].detail[i].kd_kartu);
                    Opnameds.push({
                        //kd_stok: result[0].detail[i].kd_stok,
                        kd_kartu: result[0].detail[i].kd_kartu,
                        kode: result[0].detail[i].kode,
                        nama: rslbb[0].nama,
                        tgl_trans: result[0].detail[i].tgl_trans,
                        kd_valuta: result[0].detail[i].kd_valuta,
                        kurs_valuta: result[0].detail[i].kurs_valuta,
                        keterangan: result[0].detail[i].keterangan,
                        jml_trans: result[0].detail[i].jml_trans

                    });
                }
                bindGrid();
                $("#TotalRupiah").val(addCommas(result[0].total_trans.toString()));
            }


        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetPegawai() {
    return $.ajax({
        url: urlPegawai,
        type: "POST",
        success: function (result) {
            BarangList = result;
            ////console.log(JSON.stringify(BarangList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getGudang() {
    return $.ajax({
        url: urlPegawai,
        type: "POST",
        success: function (result) {
            $("#gudang").empty();
            $("#gudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            GudangList = result;
            for (var i = 0; i < data.length; i++) {
                $("#gudang").append('<option value="' + data[i].kode_Pegawai + '">' + data[i].nama_Pegawai + '</option>');
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
function removeCommas(str) {
    return str.replace(/,/g, '');
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function bindGrid() {
    var todaydate = new Date();
    _GridStokOpname = $("#GridStokOpname").kendoGrid({
        columns: columnOpname,
        dataSource: {
            data: Opnameds,
            schema: {
                model: {
                    fields: {
                        kd_kartu: { type: "string", editable: false },
                        kode: { type: "string", editable: false },
                        nama: { type: "string"},
                        tgl_trans: { type: "datetime", editable: false, defaultValue: new Date()},
                        kd_valuta: { type: "string", editable: false, defaultValue: "IDR" },
                        kurs_valuta: { type: "decimal", editable: false, defaultValue: 1 },
                        keterangan: { type: "string" }, 
                        jml_trans: { type: "decimal", editable: true, defaultValue: 0 }
                    }
                }
            },
            aggregate: [
                { field: "jml_trans", aggregate: "sum" }
            ]
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBound: function (e) {
            var gridData = $("#GridStokOpname").data('kendoGrid').dataSource.view();
            let total_price = 0;


            gridData.forEach(element => {

                total_price = total_price + element.jml_trans;
               

            });
            $("#TotalRupiah").val(addCommas(total_price.toString()));
        },
        save: function (data) {
            var grid = this;

            setTimeout(function () {
                var ds = $('#GridStokOpname').data('kendoGrid').dataSource.data();
                Opnameds = [];
                for (var i = 0; i <= ds.length - 1; i++) {
                    Opnameds.push({
                        kd_kartu: ds[i].kd_kartu,
                        kode: ds[i].kode,
                        nama: ds[i].nama,
                        tgl_trans: ds[i].tgl_trans,
                        kd_valuta: ds[i].kd_valuta,
                        kurs_valuta: ds[i].kurs_valuta,
                        keterangan: ds[i].keterangan,
                        jml_trans: ds[i].jml_trans

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


function dateTimeEditor2(container, options) {

    var input = $("<input/>");
    input.attr("name", options.field);

    input.appendTo(container);

    input.kendoDateTimePicker({
        value: new Date()

    });
    
        
}
function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
        max: options.model.jml_trans,
        min: 0
    });
}

function PegawaiDropdown(container, options) {
    var input = $('<input required id="kode" name="nama">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama",
        dataValueField: "nama",
        dataSource: BarangList,
        optionLabel: "Pilih Pegawai",
        filter: "contains",
        template: "<span data-id='${data.kode}' data-pegawai='${data.nama}'>${data.nama}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Pegawai = e.item.find("span").attr("data-pegawai");
            var pegawai = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            pegawai.kd_kartu = id;
            pegawai.nama = Pegawai;
            pegawai.kode = id;
            pegawai.NAMA = Pegawai;
            pegawai.Nama = Pegawai;

        }
    }).appendTo(container);
    
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
    //var gudang = $('#gudang').val();
    //var penerima = $('#penerima').val();
    var ItemData = _GridStokOpname.dataSource.data();
    validationMessage = '';
    //if (!gudang) {
    //    validationMessage = validationMessage + 'Gudang harus di pilih.' + '\n';
    //}
    //if (!penerima) {
    //    validationMessage = validationMessage + 'Penerima/Nopo/Sopir harus di isi.' + '\n';
    //}

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
    ////console.log(JSON.stringify(Opnameds));
    var notrans = $("#NoTransaksi").val();
    var keteangantxt = $("#Keterangan").val();
    // ////console.log(JSON.stringify(pods));

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
        cmds += '  Jl Kombespol M Duryat 7-9 Sda';
        cmds += newLine;
        cmds += 'Telp:081216327988 WA:081216327988';
        cmds += newLine;

        //cmds += ' ' + Opnameds[0].alamat;
        //cmds += newLine;
        //cmds += 'Telp:' + Opnameds[0].fax1 + ' WA:' + Opnameds[0].fax1;
        //cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '         KASBON'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
        cmds += 'NO TRANS   : ' + notrans;
        cmds += newLine;
        cmds += 'TANGGAL : ' + kendo.toString(Opnameds[0].tgl_trans, "dd MM yyyy");
        cmds += newLine;
        cmds += 'Keterangan: ' + keteangantxt;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        
        var totjmltrans = 0;
        var jmlitem = 0;

        for (var i = 0; i <= Opnameds.length - 1; i++) {
            cmds += 'Kartu : ' + Opnameds[i].nama;
            cmds += newLine;
            cmds += 'Valuta : ' + Opnameds[i].kd_valuta;
            cmds += newLine;
            cmds += 'Kurs : ' + Opnameds[i].kurs_valuta;
            cmds += newLine;


            //var qtyIn = Opnameds[i].qty_out.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            totjmltrans += Opnameds[i].jml_trans * 1;
            cmds += 'Jml. Trans : ' + Opnameds[i].jml_trans.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            cmds += newLine;

            cmds += 'Keterangan : ' + Opnameds[i].keterangan;
            cmds += newLine;

         
        }
        var jmlitem = Opnameds.length ;
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


function savedata() {
    var transno = "";
    if (Mode != "NEW") {
        //transno = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        
      //nomor: trasno,
        //penerima: $('#penerima').val(),
        keterangan: $('#Keterangan').val(),
        total_trans: removeCommas($("#TotalRupiah").val()),
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
                    ////console.log(urlSave);
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        window.location.href = urlOpname + '?id=' + result.result + '&mode=VIEW';
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

