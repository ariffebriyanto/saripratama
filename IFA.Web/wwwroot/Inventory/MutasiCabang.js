var _GridStokOpname;
var Opnameds = [];
var SatuanListParam = [];
var BarangList = [];
var GudangList = [];
var SatuanList = [];
var saldods = [];
var pods = [];
function qty_datalbl(container, options) {
    var input = $('<label id="qty_datalabel" />');
    input.appendTo(container);
}

function satuanlbl(container, options) {
    var input = $('<label id="satuanlabel" />');
    input.appendTo(container);
}
var columnOpname = [
    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "50px", editor:satuanlbl },
 //   { field: "nama_Gudang", title: "Gudang Tujuan", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "90px", editor: gudangDropDownEditor, hidden: true  },

    { field: "qty_data", title: "Stok Barang", attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: qty_datalbl, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
  //  { field: "qty_data", title: "Last Price", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: qty_datalbl },

    { field: "qty_out", title: "Jumlah", width: "50px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, editor: valueValidation, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
   // { field: "keterangan", title: "Keterangan", width: "180px" },
    { command: ["edit", "destroy"], title: "Actions", width: "40px" }
];
var editval = "inline";

$(document).on('keydown', function (event) {
    if (event.key == "F2") {
        onAddNewRow();
        return false;
    }
});

function prepareActionGrid() {
    $(".viewData").on("click", function () {
        var id = $(this).data("id");

        if (id == "") {
            id = _GridPO.dataSource._data[0].Kode_Barang;
        }
        $("#invoiceModal").modal('show');

        startSpinner('loading..', 1);
        //var urlLink = urlPrint + '?id=' + idPO;
        var urlLink = urlPrint + '?kd_barang=' + id;

        $.ajax({
            url: urlLink,
            type: "POST",
            success: function (result) {
                startSpinner('loading..', 0);
                //var element = document.getElementById("divInvoice");
                //element.appendChild(result);
                $("#divInvoice").empty()
                $("#divInvoice").append(result);
            },
            error: function (data) {
                alert('Something Went Wrong');
                startSpinner('loading..', 0);
            }
        });

        //   window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
}

function onDataBound(e) {
    addCustomCssButtonCommand();
    prepareActionGrid();
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

$(document).ready(function () {
    startSpinner('Loading..', 1);

    bindGrid();
   $.when(GetBarang()).done(function () {
       $.when(GetSatuan()).done(function () {
          $.when(getGudang_asal()).done(function () {
            $.when(getGudang()).done(function () {
                $.when(getData(idOpname)).done(function () {
                    startSpinner('loading..', 0);
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
            //console.log(focusable.index(this) + 1);
            //if (focusable.index(this) == 20)
            //{
               //alert("halo");
            //}
            return false;
        }
    });

});

function onGDChange() {
    startSpinner('Loading..', 1);
    $("#GridStokOpname").data('kendoGrid').dataSource.data([]);   
   // $("#GridStokOpname").kendoGrid('destroy').empty();
   // bindGrid();
   
    $.when(GetBarang()).done(function () {
        startSpinner('Loading..', 0);

    });
}
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
           // console.log(result);
            Opnameds = result;
            console.log(JSON.stringify(Opnameds));

            if (result.length > 0) {
                $("#NoTransaksi").val(result[0].no_trans);
                $("#Keterangan").val(result[0].keterangan);
                $("#penerima").val(result[0].penerima);
                $('#Keterangan').attr("disabled", "disabled");
                $('#penerima').attr("disabled", "disabled");
                $("#gudang").val(result[0].gudang_tujuan);
                $("#gudang_asal").val(result[0].gudang_asal);

                columnOpname = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px" },
                    { field: "nama_Gudang1", title: "Gudang Asal", width: "90px", hidden: true },
                    { field: "nama_Gudang", title: "Gudang Tujuan", width: "90px", hidden: true },
                    { field: "qty_out", title: "Jumlah", width: "80px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                   // { field: "qty_out", title: "Jumlah", width: "80px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right " }},

                    //  { field: "keterangan", title: "Keterangan", width: "180px" }
                ];
                editval = false;

                $("#addNew").hide();
                $("#simpan").hide();
                $("#new").show();
                
                if (_GridStokOpname != undefined) {
                    $("#GridStokOpname").kendoGrid('destroy').empty();
                }
                for (var i = 0; i <= result[0].detail.length - 1; i++) {
                    $("#gudang option[value='" + result[0].detail[i].gudang_tujuan + "']").attr("selected", "selected");
                    $('#gudang').selectpicker('refresh');
                    $('#gudang').selectpicker('render');
                    $('#gudang').attr("disabled", "disabled");

                    $("#gudang_asal option[value='" + result[0].detail[i].gudang_asal + "']").attr("selected", "selected");
                    $('#gudang_asal').selectpicker('refresh');
                    $('#gudang_asal').selectpicker('render');
                    $('#gudang_asal').attr("disabled", "disabled");

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
    ////var filterdata = {
    ////    cb: cb,
    ////};
    return $.ajax({
        url: urlBarang,
        type: "POST",
        data: {
            cb: $("#gudang_asal").val()
        },
        success: function (result) {
            //$('#GridStokOpname').kendoGrid('destroy').empty();
            //$('#GridStokOpname').empty();
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
function getGudang_asal() {
    return $.ajax({
        url: urlGudang + "/?filter='cabang'",
        type: "POST",
        success: function (result) {
            $("#gudang_asal").empty();
            $("#gudang_asal").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            GudangList = result;
            for (var i = 0; i < data.length; i++) {
                $("#gudang_asal").append('<option value="' + data[i].kode_Gudang + '">' + data[i].nama_Gudang + '</option>');
            }
            $('#gudang_asal').selectpicker('refresh');
            $('#gudang_asal').selectpicker('render');

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
                        Action: { type: "string", editable: false },

                        kd_stok: { type: "string" },
                        Kode_Barang: { type: "string" },
                        Nama_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        Kode_satuan: { type: "string" },
                        Nama_Satuan: { type: "string" },
                        kd_satuan: { type: "string" },
                        satuan: { type: "string" },
                        qty_data: { type: "number"},
                       // keterangan: { type: "string" },
                        gudang_tujuan: { type: "string" },
                        qty_out: { type: "number" },
                        nama_Gudang: { type: "string" },
                        rek_persediaan: { type: "string" },
                        harga: { type: "number" }
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
        save: function (e) {
            //alert('a');
          //  var grid = this;

            //setTimeout(function () {
            //    var ds = $('#GridStokOpname').data('kendoGrid').dataSource.data();
            //    Opnameds = [];
            //    for (var i = 0; i <= ds.length - 1; i++) {
            //        if (ds[i].keterangan == "" && ds[i].nama_Gudang != "") {
            //            ds[i].keterangan = "MUTASI ke " + ds[i].nama_Gudang;
            //        }
            //        Opnameds.push({
            //            kd_stok: ds[i].kd_stok,
            //            Kode_Barang: ds[i].Kode_Barang,
            //            Nama_Barang: ds[i].Nama_Barang,
            //            nama_Barang: ds[i].nama_Barang,
            //            Kode_satuan: ds[i].Kode_satuan,
            //            Nama_Satuan: ds[i].Nama_Satuan,
            //            kd_satuan: ds[i].kd_satuan,
            //            satuan: ds[i].satuan,
            //            qty_data: ds[i].qty_data,
            //           // keterangan: ds[i].keterangan,
            //            gudang_tujuan: ds[i].gudang_tujuan,
            //            qty_out: ds[i].qty_out,
            //            nama_Gudang1: ds[i].nama_Gudang1,
            //            nama_Gudang: ds[i].nama_Gudang,
            //            rek_persediaan: ds[i].rek_persediaan,
            //            harga: ds[i].harga

            //        });
            //    }
            //    $('#GridStokOpname').kendoGrid('destroy').empty();
            //    bindGrid();
            //});
        },
        edit: function (e) {
            addCustomCssButtonCommand();

            if (e.model.kd_stok != null && e.model.kd_stok != "") {
                var found = GetSatuanCode(e.model.kd_stok);

                $("#qty_datalabel").text(found[0].qty_data);
                $("#satuanlabel").text(found[0].kd_Satuan);
            }
           
        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");
}

function valueValidation(container, options) {

    var input = $("<input name='" + options.field + "' id='qty_outnumeric'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
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
            
            SatuanListParam = [];
            var found = GetSatuanCode(id);
            barang.Kode_satuan = found[0].kd_Satuan;
            barang.Nama_Satuan = found[0].kd_Satuan;
            barang.kd_satuan = found[0].kd_Satuan;
            barang.satuan = found[0].kd_Satuan;
            barang.qty_data = found[0].qty_data;
            barang.rek_persediaan = found[0].rek_persediaan;
            barang.harga = found[0].harga;
            $("#qty_datalabel").text(found[0].qty_data);
            $("#satuanlabel").text(found[0].kd_Satuan);
            var numerictextbox = $("#qty_outnumeric").data("kendoNumericTextBox");
            numerictextbox.max(found[0].qty_data);
            numerictextbox.value(0);
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
    $('#kode_Barang').data("kendoDropDownList").open();

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
    var gudang_asal = $('#gudang_asal').val();
    var penerima = $('#penerima').val();
    var ItemData = _GridStokOpname.dataSource.data();
    validationMessage = '';
    if (!gudang || !gudang_asal ) {
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
    console.log(JSON.stringify(Opnameds));
   // console.log(JSON.stringify(pods));

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

        for (var i = 1; i <= Opnameds.length -1 ; i++) {
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
        gudang_asal: $('#gudang_asal').val(),
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

function onPrintClicked() {
    var id = $(this).data("id");
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=mutasicabang&id=" + idOpname, "_blank");
}