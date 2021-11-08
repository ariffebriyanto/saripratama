
var GudangDs = [];
var userDs = NoTransList;
var terimads = [];
var terimadtlds = [];
$(document).ready(function () {

    //fillCbonotrans();


    $("#no_qc").kendoDropDownList({
        dataTextField: "no_trans",
        dataValueField: "no_trans",
        filter: "contains",
        dataSource: NoTransList,
        change: onChange,
        optionLabel: "Please Select"

    }).closest(".k-widget");

    $("#lok_simpan").kendoDropDownList({
        dataTextField: "Nama_Gudang",
        dataValueField: "Kode_Gudang",
        filter: "contains",
        dataSource: GudangList,
        value: GudangUser,
        change: changegudang,
        enable: true,
        optionLabel: "Please Select"
    });


    var dropdownGudang = $("#lok_simpan").data("kendoDropDownList");
    //dropdownGudang.readonly();


    // console.log([NoTransList]);

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        //startDate: 'd',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        //var minDate = new Date(selected.date.valueOf());
        //// $('#tanggalBayar').datepicker('setStartDate', minDate);
        //$('#divtanggalkirim').datepicker('setStartDate', minDate);

        //var diff = calcDate($('#tglBayar').val(), $('#tanggal').val());
        //$("#Lama").val(diff);
    });


    if (Mode !== "NEW") {
        $.when(getGudang()).done(function () {
            getDataTerima(idterima);
        });


    }

    $('#tanggal').val(dateserver);
    bindGrid();


    function onChange(e) {
        var dataItem = e.sender.dataItem();
        if (dataItem) {
            //output selected dataItem
            $("#Supplier").val(dataItem.Nama_Supplier);
            $("#p_np").val(dataItem.p_np);
            $("#no_ref").val(dataItem.no_po);
        }

        var urlLink = urlGetData;
        startSpinner('Loading..', 1);
        $.ajax({
            url: urlLink,
            dataType: "json",
            type: "POST",
            data: {
                id: $("#no_qc").val()
            },
            success: function (result) {
                //if (_GridPO) {
                $('#GridPO').kendoGrid('destroy').empty();
                //}
                GudangDs = result;
                console.log(GudangDs);
                bindGrid();
                startSpinner('Loading..', 0);
            }

        });
    }

    barangList = [];

    satuanList = [];



});
//var GudangList = [
//    { text: "Gudang Bangil", value: "00002" },
//    { text: "Gudang Lamongan", value: "00003" },
//    { text: "Gudang Pusat", value: "00001" }
//];


function getGudang() {
    return $.ajax({
        url: urlGudang,
        type: "POST",
        success: function (result) {
            $("#gudang").empty();
            $("#gudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            GudangTujuan = result;
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

function Gudangdropdown(container, options) {
    var input = $('<input required id="Kode_Gudang" name="Nama_Gudang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "Nama_Gudang",
        dataValueField: "Kode_Gudang",
        dataSource: GudangList,
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.Nama_Gudang || 0]);
            }
        },
        template: "<span data-id='${data.Kode_Gudang}' data-Gudang='${data.Nama_Gudang}'>${data.Nama_Gudang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Gudang");
            var barang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            barang.lokasi_simpan = Barang;
            //barang.nama_Barang = Barang;
            barang.gudang_tujuan = id;
            barang.nm_Gudang_asal = Barang;
            barang.gudang_asal = id;
            //barang.Nama_Barang = Barang;
            //barang.nama_barang = Barang;

        }
    });
}



function changegudang() {
    var loksimpan = $("#lok_simpan").data("kendoDropDownList").text();
    var loksimpanid = $("#lok_simpan").val();

    for (var i = 0; i <= GudangDs.length - 1; i++) {
        GudangDs[i].lokasi_simpan = loksimpan;
        GudangDs[i].gudang_tujuan = loksimpanid;
        GudangDs[i].gudang_asal = '00000';
        GudangDs[i].nm_Gudang_asal = 'GUDANG SEMENTARA';
    }
    $('#GridPO').kendoGrid('destroy').empty();

    bindGrid();
}

function fillCbonotrans() {
    $("#no_qc").empty();
    $("#no_qc").append('<option value="" selected disabled>Please select</option>');
    var data = NoTransList;


    for (var i = 0; i < data.length; i++) {
        $("#no_qc").append('<option value="' + data[i].no_trans + '">' + data[i].no_trans + '</option>');
    }
    //var test = 'S04165';
    //$("#kd_supplier option[value='" + test + "']").attr("selected", "selected");

    $('#no_qc').selectpicker('refresh');
    $('#no_qc').selectpicker('render');
}

function bindGrid() {

    _GridPO = $("#GridPO").kendoGrid({
        columns: [

            { field: "no", title: "No", width: "30px", template: "<span class='row-number'></span>" },
            { field: "nama_Barang", title: "Nama Barang", width: "150px" },
            { field: "kd_satuan", title: "Satuan", width: "80px" },
            { field: "qty_order", title: "Qty PO", width: "110px", attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_order ? data.qty_order.sum: 0 #", footerAttribute: { "id": "qty_order" } },
            { field: "qty_qc_pass", title: "Qty Terima", width: "110px", attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_qc_pass ? data.qty_qc_pass.sum: 0 #", footerAttribute: { "id": "qty_qc_pass" } },
            { field: "nm_Gudang_asal", title: "Lokasi Asal", width: "180px", editor: Gudangdropdown, hidden: true },
            { field: "lokasi_simpan", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown, hidden: true },
            { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },

            {
                field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
                    $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
                }
            }
        ],



        save: function () {


            var grid = this;
            setTimeout(function () {
                // grid.refresh();


            });

        },
        dataSource: {
            data: GudangDs,
            schema: {
                model: {
                    id: "no_po",
                    fields: {

                        no: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        kd_Barang: { type: "string", editable: false },
                        no_seq: { type: "number", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        qty_order: { type: "number", editable: false },
                        qty_qc_pass: { type: "number", editable: true },
                        gudang_asal: { type: "string", editable: true },
                        nm_Gudang_asal: { type: "string", editable: true },
                        gudang_tujuan: { type: "string", editable: true },
                        lokasi_simpan: { type: "string", editable: true },
                        harga: { type: "number", editable: true },
                        rp_trans: { type: "number", editable: true }
                    }
                }
            },

            change: function (e) {

            },
            aggregate: [
                { field: "rp_trans", aggregate: "sum" },
                { field: "qty_qc_pass", aggregate: "sum", type: "number" },
                { field: "qty_order", aggregate: "sum" }
            ]


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


        editable: false
    }).data("kendoGrid");

}
function onSaveClicked() {
    validationPage();
}
function validationPage() {
    var no_qc = $('#no_qc').val();
    var lok_simpan = $('#lok_simpan').val();
    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';
    if (!no_qc) {
        validationMessage = validationMessage + 'No QC blm di pilih!' + '\n';
    }
    if (!lok_simpan) {
        validationMessage = validationMessage + 'Lokasi Simpan blm di tentukan!' + '\n';
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
function showlist() {
    window.location.href = urlList;
}
function showCreate() {
    window.location.href = urlCreate;
}


function getDataTerima(terima) {
    startSpinner('loading..', 1);
    var urlLink = urlGetDataTerima;
    var filterdata = {
        id: terima,
        //DateFrom: "",
        //DateTo: "",
        status_po: ""
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            GudangDs = result;
            terimads = result;
            if (Mode === "NEW") {
                Mode = "VIEW";
            }
            fillForm();
            $.ajax({
                url: urlGetDetailTerima,
                type: "POST",
                data: filterdata,
                success: function (result) {

                    GudangDs = [];
                    for (var i = 0; i <= result.length - 1; i++) {
                        GudangDs.push({

                            Kode_Barang: result[i].kd_satuan,
                            kd_stok: result[i].kd_stok,
                            nama_Barang: result[i].nama_Barang,
                            qty_order: result[i].qty_order,
                            qty_qc_pass: result[i].qty_in,
                            kd_satuan: result[i].kd_satuan,
                            gudang_tujuan: result[i].gudang_tujuan,
                            lokasi_simpan: result[i].lokasi_simpan,
                            gudang_asal: result[i].gudang_asal,
                            nm_Gudang_asal: result[i].nm_Gudang_asal,
                            harga: result[i].harga

                        });
                    }
                    console.log(GudangDs);

                    if (Mode === "VIEW") {
                        columns = [
                            { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                            { field: "nama_Barang", title: "Nama Barang", width: "90px" },
                            { field: "kd_satuan", title: "Satuan", width: "80px" },
                            { field: "qty_order", title: "qty PO", width: "110px", attributes: { class: "text-right " }, footerTemplate: "Jumlah: #: sum #", footerAttribute: { "id": "qty_order" } },
                            { field: "qty_qc_pass", title: "qty QC", width: "110px", attributes: { class: "text-right " }, footerTemplate: "Jumlah: #: sum #" },
                            { field: "nm_Gudang_asal", title: "Lokasi Asal", width: "180px", readonly: true },
                            { field: "lokasi_simpan", title: "Lokasi Simpan", width: "180px" },
                            { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },

                            {
                                field: "rp_trans", title: "Total", "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
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
                            { field: "qty_order", title: "qty QC", width: "110px", attributes: { class: "text-right " }, footerTemplate: "Jumlah: #: sum #", footerAttribute: { "id": "qty_order" } },
                            { field: "qty_qc_pass", title: "qty IN", width: "110px", attributes: { class: "text-right " }, footerTemplate: "Jumlah: #: sum #", footerAttribute: { "id": "qty_qc_pass" } },
                            { field: "gudang_asal", title: "Lokasi Asal", width: "180px" },
                            { field: "lokasi_simpan", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                            { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },

                            {
                                field: "rp_trans", title: "Total", "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
                                    $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
                                }
                            }


                        ];
                    }
                    $('#GridPO').kendoGrid('destroy').empty();
                    bindGrid();


                    startSpinner('loading..', 0);
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


}


function fillForm() {
    if (Mode === "VIEW") {


        $('[name="qty_qc_pass"]').attr("readonly", true);
        $('#divtanggal').datepicker('remove');

        //$("#no_trans").attr("disabled", "disabled");
        $("#no_ref").attr("disabled", "disabled");
        //$("#no_qc").data("kendoDropDownList").readonly();
        //$("#lok_simpan").data("kendoDropDownList").readonly();
        $("#Supplier").attr("disabled", "disabled");
        $("#nm_penyerah").attr("disabled", "disabled");
        $("#Keterangan").attr("disabled", "disabled");
        // $("#nm_Gudang_asal").data("kendoDropDownList").readonly();

    }

    if (Mode === "EDIT" || Mode === "VIEW") {
        if (GudangDs.length > 0) {
            //$("#no_qc").data("kendoDropDownList").value(GudangDs[0].no_qc);
            //$("#no_qc").val(POds[0].no_qc);
            $("#v_noqc").val(GudangDs[0].no_qc);
            $("#no_trans").val(GudangDs[0].no_trans);
            $("#no_ref").val(GudangDs[0].no_ref);
            $("#nm_penyerah").val(GudangDs[0].penyerah);
            $("#Keterangan").val(GudangDs[0].keterangan);
            $("#Supplier").val(GudangDs[0].nama_Supplier);
            $("#tanggal").val(GudangDs[0].tgl_transdesc);
            $("#lok_simpan").data("kendoDropDownList").value(GudangDs[0].kode_gudang);

        }
    }
}

function SaveData() {
    var asu = $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_qc_pass.sum;
    var su = $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_order.sum;
    console.log(asu + su);
    //$("#form1").validate();
    //if (!$('#form1').valid()) {
    //    return false;
    //}
    //var totalText = $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_po.sum;
    //console.log("Tes ini" + asu - su);
    var no = "";
    if (Mode !== "NEW") {
        no = $('#no_qc').val();
        Mode = "NEW";
    }
    var savedata = {
        no_ref: $('#no_ref').val(),
        no_qc: $('#no_qc').val(),
        penyerah: $('#nm_penyerah').val(),
        tgl_trans: $('#tanggal').val(),
        no_trans: no,
        keterangan: $('#Keterangan').val(),
        lok_simpan: $('#lok_simpan').val(),
        p_np: $('#p_np').val(),
        kode_gudang: $('#lok_simpan').val(),
        jml_qtypo: $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_order.sum,
        jml_qtyin: $("#GridPO").data().kendoGrid.dataSource.aggregates().qty_qc_pass.sum,
        jml_rp_trans: $("#GridPO").data().kendoGrid.dataSource.aggregates().rp_trans.sum,
        gddetail: _GridPO.dataSource.data().toJSON()
    };
    console.log('savedata: ' + _GridPO.dataSource.data().toJSON());
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

function onPrintTokoClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlGetDataTerima;
    var filterdata = {
        id: idterima,
        status_po: ""
    };

    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            terimads = result;

            $.ajax({
                url: urlGetDetailTerima,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    terimadtlds = result;

                    console.log(JSON.stringify(terimads));
                    console.log(JSON.stringify(terimadtlds));

                    if (jspmWSStatus()) {
                        //Create a ClientPrintJob
                        var cpj = new JSPM.ClientPrintJob();
                        // Set Printer type (Refer to the help, there many of them!)
                        if ($('#useDefaultPrinter').prop('checked')) {
                            cpj.clientPrinter = new JSPM.DefaultPrinter();
                        } else {
                            cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
                        }
                        // Set content to print...
                        //Create ESP/POS commands for sample label
                        var esc = '\x1B'; //ESC byte in hex notation
                        var newLine = '\x0A'; //LF byte in hex notation

                        var cmds = esc + "@"; //Initializes the printer (ESC @)
                        cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                        cmds += '            TOKO 88'; //text to print
                        cmds += newLine;
                        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                        //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
                        //cmds += newLine;
                        //cmds += 'Telp:081216327988 WA:081216327988';
                        //cmds += newLine;

                        cmds += ' ' + terimads[0].alamat;
                        cmds += newLine;
                        cmds += 'Telp:' + terimads[0].telp + ' WA:' + terimads[0].wa;
                        cmds += newLine;
                        cmds += '---------------------------------';
                        cmds += newLine;
                        cmds += esc + '!' + '\x18';
                        cmds += '         PENERIMAAN'; //text to print
                        cmds += newLine;
                        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                        cmds += 'NO Tran : ' + terimads[0].no_trans;
                        cmds += newLine;
                        cmds += 'NO PO   : ' + terimads[0].no_ref;
                        cmds += newLine;
                        cmds += 'SUPPLIER: ' + terimads[0].nama_Supplier;
                        cmds += newLine;
                        cmds += 'TANGGAL : ' + terimads[0].tgl_transdesc;
                        cmds += newLine;
                        cmds += 'PENYERAH: ' + terimads[0].penyerah;
                        cmds += newLine;
                        cmds += 'USER    : ' + terimads[0].last_Created_By;
                        cmds += newLine;
                        cmds += 'GUDANG  : ' + terimads[0].nama_Gudang;
                        cmds += newLine;
                        cmds += '---------------------------------';
                        cmds += newLine;
                        var totQty = 0;
                        var totharga = 0;

                        for (var i = 0; i <= terimadtlds.length - 1; i++) {
                            cmds += terimadtlds[i].nama_Barang;
                            cmds += newLine;
                            var qtyIn = addCommas(terimadtlds[i].qty_in.toString());
                            var berat = addCommas(terimadtlds[i].harga.toString());
                            // var harga = terimadtlds[i].harga * terimadtlds[i].qty_in;
                            var harga = ((terimadtlds[i].qty_in) * (terimadtlds[i].harga * 100).toFixed(0)) / 100;
                            var hargastr = addCommas(harga.toString());
                            totharga += harga;
                            totQty += terimadtlds[i].qty_in;
                            cmds += qtyIn + " " + terimadtlds[i].kd_satuan + "      " + berat.toString();
                            var countchar = 24 - (qtyIn.length + terimadtlds[i].kd_satuan.length + berat.length + hargastr.length);
                            for (var j = 0; j <= countchar - 1; j++) {
                                cmds += " ";
                            }
                            cmds += "  " + hargastr;
                            cmds += newLine;
                        }

                        var totItem = terimadtlds.length.toString();
                        var totalQty = addCommas(totQty.toString());
                        var totalHarga = addCommas(totharga.toString());
                        cmds += '---------------------------------';
                        cmds += newLine;
                        cmds += 'Tot. Item : ' + totItem.toString();
                        cmds += newLine;
                        cmds += 'Tot.Qty   : ' + totalQty.toString();
                        cmds += newLine;
                        cmds += 'Total     : ' + totalHarga.toString();
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


                    startSpinner('loading..', 0);
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

    //test
}

function addCommas(str) {
    var components = str.toString().split(".");
    components[0] = components[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return components.join(".");
    // return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function onPrintClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + idterima;
    //wrapperList
    //debugger;
    var wrapper = document.getElementById("wrapperList");

    $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            var MainWindow = window.open('', '', 'height=500,width=800');

            //  MainWindow.document.write('<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style="margin-bottom: 20px;"><tr ><td style="width: 40%;border: 0px solid #dddddd;" ><h2>IFA Company</h2><p>Jalan Diponego 21</p><p>031-9992190</p><p>Surabaya - Jawa Timur</p></td><td style="width: 20%;border: 0px solid #dddddd;"></td><td style="width: 40%;border: 0px solid #dddddd;"><h2>PURCHASE ORDER</h2><p>PO No: 00002/POM/1/20151207</p><p>03 September 2019</p><p>PO Status: ENTRY</p></td></tr></table> <table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">SUPPLIER</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;">ALAMAT PENGIRIMAN</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SUMBER REJEKI TEKNIK-PENGHELA , UD.</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. DIPONEGORO 21, SURABAYA</td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. RAYA BAMBE KM.19, DRIYOREJO GRESIK</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Telp No: 7590102</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"> </td></tr><tr > <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Jatuh Tempo: 02 Oktober 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">TANGGAL KIRIM</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">REQUESTED BY</th> <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">APPROVED BY</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">20 September 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Ricardo Kaka</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Rui Costa</td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">NOTES</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SEGERA DIKIRIM, STOK HABIS</td></tr></table> <table> <tr class="headerTable"> <th>Barang</th><th>Satuan</th> <th>Qty</th> <th>Harga</th><th>Disc Rp</th> <th>Total</th> </tr><tr> <td>HYDRAULIC CRIMPING TOOL YQK 300</td><td>PCS</td><td style="text-align: right;padding: 2px;">100.00</td><td style="text-align: right;padding: 2px;">10,000,000.00</td><td style="text-align: right;padding: 2px;">288,650,000.00</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">SUBTOTAL</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">ONGKOS KIRIM</td><td style="text-align: right;padding: 2px;">10,000,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">PPN</td><td style="text-align: right;padding: 2px;">71,135,000.00</td></tr><tr> <th colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">GRAND TOTAL (Rp)</th> <th style="text-align: right;padding: 2px;">792,485,000.00</th> </tr></table></body></html>');
            MainWindow.document.write(result);
            MainWindow.document.close();
            setTimeout(function () {
                MainWindow.print();
            }, 500);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}