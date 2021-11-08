
var pods = [];
var GudangList = [];
var userDs = NoTransList;
var kd_supp = null;
var terimads = [];
var ColumnGrid = [
    { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
    { field: "nama_Barang", title: "Nama Barang", width: "90px" },
    { field: "kd_satuan", title: "Satuan", width: "80px" },
    { field: "qty_out", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
    { field: "qty_in", title: "Qty In", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' },  editor: valueValidation },
    { field: "qty_sisa", title: "Qty Sisa", width: "110px", hidden:true, attributes: { class: "text-right " }, footerAttribute: { "id": "qty_sisa" }, "footerTemplate": "Total: #: data.qty_sisa ? data.qty_sisa.sum: 0 #" },
    { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
    { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
    //{ field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
    //{ field: "rp_trans", title: "Total", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    {
        field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
            $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
        }
    }
];
var editval = true;
$(document).ready(function () {

    //  console.log(JenisBarangList);
    //alert(dateserver);
    if (Mode != "NEW") {
        getData(idData);
    }
    //$.when(getData(idData)).done(function () {
    //    console.log(idData);
    //        startSpinner('loading..', 0);
    //    });

    bindGrid();
    fillCbonotrans();
});

function changePO() {
    var urlLink = urlGetData;
    return $.ajax({
        url: urlLink,
        dataType: "json",
        type: "POST",
        data: {
            id: $("#PONumber").val()
        },
        success: function (result) {
            //if (_GridPO) {
            $('#GridPO').kendoGrid('destroy').empty();
            //}
            pods = result;
            if (result.length > 0) {
                $('#Supplier').val(result[0].cb_asal);

            }
            //console.log(result);
            bindGrid();


        }

    });
    //getSup();
}

function getData(idData) {
    var urlLink = urlGetMts;
    var filterdata = {
        id: idData
        //no_trans: idData,
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            terimads = [];
            terimads = result;
            pods = [];
            for (var i = 0; i <= result.length - 1; i++) {
                pods.push({

                    kd_stok: result[i].kd_stok,
                    Kode_Barang: result[i].kd_stok,
                    nama_barang: result[i].nama_barang,
                    nama_Barang: result[i].nama_Barang,
                    kd_satuan: result[i].kd_satuan,
                    kd_satuan: result[i].kd_satuan,
                    qty_out: result[i].qty_order,
                    qty_in: result[i].qty_in,
                    qty_sisa: result[i].qty_sisa,
                    nama_Gudang: result[i].nama_Gudang

                });
            }


            if (result.length > 0) {
                $("#PONumber").val(result[0].no_ref);
                $("#NoTransaksi").val(result[0].no_trans);
                $("#Keterangan").val(result[0].keterangan);
                $("#sj_supplier").val(result[0].penyerah);
                $("#Supplier").val(result[0].cb_asal);
                $('#Keterangan').attr("disabled", "disabled");
                $('#sj_supplier').attr("disabled", "disabled");

                console.log(result);
                debugger;
                if (Mode === "VIEW") {
                    columns = [
                        { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                        { field: "nama_Barang", title: "Nama Barang", width: "90px" },
                        { field: "kd_satuan", title: "Satuan", width: "80px" },
                        { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
                        { field: "qty_in", title: "Qty In", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_in ? data.qty_in.sum: 0 #", footerAttribute: { "id": "qty_in" }, editor: valueValidation },
                        { field: "qty_sisa", title: "Qty sisa",hidden : true, width: "110px", attributes: { class: "text-right " } },
                        { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                        { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
                        //{ field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
                        //{ field: "rp_trans", title: "Total", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                        {
                            field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
                                $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
                            }
                        }
                    ];

                }
                else if (Mode === "EDIT") {
                    columns = [
                        { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                        { field: "nama_Barang", title: "Nama Barang", width: "90px" },
                        { field: "kd_satuan", title: "Satuan", width: "80px" },
                        { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
                        { field: "qty_in", title: "Qty In", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_in ? data.qty_in.sum: 0 #", footerAttribute: { "id": "qty_in" }, editor: valueValidation },
                        { field: "qty_sisa", title: "Sisa", hidden: true, width: "110px", attributes: { class: "text-right " } },
                        { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                        { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: false },
                        //{ field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
                        //{ field: "rp_trans", title: "Total", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                        {
                            field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
                                $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
                            }
                        }


                    ];
                }
                $('#GridPO').kendoGrid('destroy').empty();
                bindGrid();

            }


        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function onChange() {
    startSpinner('Loading..', 1);

    $.when(changePO()).done(function () {
        startSpinner('Loading..', 0);

    });
}

function fillCbonotrans() {
    $("#PONumber").empty();
    $("#PONumber").append('<option value="" selected disabled>Please select</option>');
    var data = NoTransList;


    for (var i = 0; i < data.length; i++) {
        $("#PONumber").append('<option value="' + data[i].no_trans + '">' + data[i].no_trans + '</option>');
    }
    //$("#tanggal").datepicker().datepicker("setDate", new Date());
    $('#tanggal').val(dateserver);
    $('#PONumber').selectpicker('refresh');
    $('#PONumber').selectpicker('render');
}
function changegudang() {
    var loksimpan = $("#lokasi").text();

    var dataItem = $("#GridPO").data("kendoGrid").dataSource.data();
    for (var i = 0; i < dataItem.length; i++) {
        dataItem[i].set("gudang_tujuan", loksimpan);
    }
}
//function getSup() {
//    console.log(pods);
//    //if (_GridPO) {
//    //    var requestData = _GridPO.dataSource.data();
//    //   // $('#Supplier').val(requestData[0].kd_supplier);
//    //    console.log(pods);
//    //}
//}
function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        max: options.model.qty_out
    });
}
function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: ColumnGrid,

        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        no: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        qty_out: { type: "string", editable: false },
                        qty_in: { type: "string", editable: true },
                        qty_sisa: { type: "string", editable: false },
                        gudang_tujuan: { type: "string", editable: false },
                        harga: { type: "number", editable: false },
                        rp_trans: { type: "number", editable: false },
                        nama_Supplier: { type: "string", editable: false },
                        nama_Gudang: { type: "string", editable: false }
                    }
                }
            },
            aggregate: [
                { field: "rp_trans", aggregate: "sum" },
                { field: "qty_in", aggregate: "sum" },
                { field: "qty_sisa", aggregate: "sum" }

            ]


        },
        save: function (data) {
            if (data.values.qty_in) {
                var test = data.model.set("rp_trans", data.values.qty_in * data.model.harga);
                data.model.set("qty_sisa", data.model.qty_order - data.values.qty_in);
                //data.model.sum("qty_sisa", data.model.qty_sisa.sum);
                //console.log(data.model.qty_sisa);
            }
            else {
                test = data.model.set("rp_trans", data.model.qty_in * data.values.harga);
            }
            var grid = this;
            setTimeout(function () {
                grid.refresh();
            });
        },

        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        },


        noRecords: true,
        editable: true
    }).data("kendoGrid");

}




function onSaveClicked() {
    //SaveData();
    validationPage();
}

function getGudang() {
    return $.ajax({
        url: urlGetGudang,
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

function validationPage() {
    //var gudang = $('#gudang').val();
    var sj_supplier = $('#sj_supplier').val();
    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';
    if (!sj_supplier) {
        validationMessage = validationMessage + 'Lengkapi Data sebelum Simpan' + '\n';
    }
    //if (!penerima) {
    //    validationMessage = validationMessage + 'Penerima harus di isi.' + '\n';
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
        SaveData();
    }
}

function Gudangdropdown(container, options) {
    var input = $('<input required id="Kode_Gudang" name="Nama_Gudang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "Nama_Gudang", //Nama_Gudang
        dataValueField: "Kode_Gudang",
        dataSource: GudangList,
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.Nama_Gudang || 0]);
            }
        },
        template: "<span data-id='${data.Kode_Gudang}' data-Nama='${data.Nama_Gudang}'>${data.Nama_Gudang}</span>",
        select: function (e) {
            var gd_id = e.item.find("span").attr("data-id");
            var nmGudang = e.item.find("span").attr("data-Nama");
            var gudang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            gudang.gudang_tujuan = gd_id;
            //gudang.lokasi = nmGudang;
            gudang.nama_Gudang = nmGudang;
            gudang.gudang_tujuan = gd_id;
            gudang.Nama_Gudang = nmGudang;
            gudang.nama_Gudang = nmGudang;

        }
    });
}


function barangDropDownEditor(container, options) {
    var input = $('<input required id="Kode_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "Nama_Barang",
        dataValueField: "Nama_Barang",
        dataSource: BarangList,
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.Nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.Kode_Barang}' data-Barang='${data.Nama_Barang}'>${data.Nama_Barang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            var barang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            barang.kd_stok = id;
            barang.nama_Barang = Barang;
            barang.Kode_Barang = id;
            barang.Nama_Barang = Barang;
            barang.nama_barang = Barang;

            SatuanListParam = [];
            var found = GetSatuanCode(id);

            for (var i = 0; i < SatuanList.length; i++) {
                if (SatuanList[i].Kode_Satuan === found[0].Kd_Satuan) {
                    var Key = SatuanList[i].Kode_Satuan;
                    var Value = SatuanList[i].Nama_Satuan;

                    SatuanListParam.push({
                        Kode_Satuan: Key,
                        Nama_Satuan: Value
                    });
                }
            }
            inputSatuan.setDataSource(SatuanListParam);
            ////$("#grid").data("kendoGrid").setData(ds);
            ////inputChannel.setd(channelListParam);
            inputSatuan.refresh();
            inputSatuan.enable(true);
        }
    }).appendTo(container);
}
function SaveData() {
    //$("#form1").validate();
    //if (!$('#form1').valid()) {
    //    return false;
    //}

    var no = "";
    if (Mode !== "NEW") {
        no = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        no_ref: $('#PONumber').val(),
        penyerah: $('#sj_supplier').val(),
        tgl_trans: $('#tanggal').val(),
        no_trans: $('#no_trans').val(),
        keterangan: $('#Keterangan').val(),
        //lokasi: $('#lokasi').val(),
        //tgl_po: $('#tanggal').val(),
        //kd_supplier: $('#Supplier').val(),
        gddetail: _GridPO.dataSource.data().toJSON()
    };
    console.log('savedata: ' + savedata);
    swal({
        type: 'warning',
        title: 'Anda yakin?',
        html: 'data akan di simpan?',
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

function onPrintTokoClicked() {
    //console.log(JSON.stringify(terimads));
    //console.log(JSON.stringify(pods));

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

        cmds += ' ' + terimads[0].alamat;
        cmds += newLine;
        cmds += 'Telp:' + terimads[0].fax1 + ' WA:' + terimads[0].fax1;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '         PENERIMAAN'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x00'; //Character font A selected (ESC ! 0)
        cmds += 'NO TRANS   : ' + terimads[0].no_trans;
        cmds += newLine;
        cmds += 'TGL : ' + terimads[0].last_Create_Date;
        cmds += newLine;
        cmds += 'PENYERAH   : ' + terimads[0].penyerah;
        cmds += newLine;
        cmds += 'USER       : ' + terimads[0].last_Created_By;
        cmds += newLine;
        cmds += 'GUDANG ASAL: ' + terimads[0].cb_asal;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        var totQty = 0;
        var totharga = 0;

        for (var i = 0; i <= pods.length - 1; i++) {
            cmds += pods[i].nama_Barang;
            cmds += newLine;
            var qtyIn = addCommas(pods[i].qty_in.toString());
            totQty += pods[i].qty_in * 1;
            cmds += qtyIn + " " + pods[i].kd_satuan;
            //var countchar = 33 - (qtyIn.length + terimadtlds[i].kd_satuan.length + 1);
            //for (var j = 0; j <= countchar - 1; j++) {
            //    cmds += " ";
            //}
            cmds += newLine;
        }
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Tot. Item : ' + addCommas(pods.length.toString());
        cmds += newLine;
        cmds += 'Tot.Qty   : ' + addCommas(totQty.toString());
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Disiapkan   Disetujui   Diketahui';
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
function showCreate() {
    window.location.href = urlCreate;
}
function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}


function onPrintClicked() {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=mutasimasukcabang&id=" + idData, "_blank");
}