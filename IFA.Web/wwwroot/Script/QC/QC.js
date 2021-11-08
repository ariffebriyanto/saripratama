
var pods = [];
//var GudangList = [];
var userDs = NoTransList;
var kd_supp = null;
var ColumnGrid = [
    { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
    { field: "nama_barang", title: "Nama Barang", width: "90px" },
    { field: "kd_satuan", title: "Satuan", width: "80px" },
    { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
    { field: "qty_qc_pass", title: "Qty Good", width: "110px", footerTemplate: "Sum: #= sum # ", validation: { required: true, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_qc_pass ? data.qty_qc_pass.sum: 0 #", footerAttribute: { "id": "qty_qc_pass" }, editor: valueValidation },
    { field: "qty_sisa", title: "Qty Sisa", width: "110px", attributes: { class: "text-right " } },
    { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown, hidden: true },
    { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
    { field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
    //{ field: "rp_trans", title: "Total", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
    {
        field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
            $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
        }
    }
];
var editval = true;
$(document).ready(function () {

    $("#lok_simpan").kendoDropDownList({
        dataTextField: "Nama_Gudang",
        dataValueField: "Kode_Gudang",
        filter: "contains",
        dataSource: GudangList,
        //value: GudangUser,
      //  change: changegudang,
        enable: true,
        optionLabel: "Please Select"
    });

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

    $('#Supplier').attr("disabled", "disabled");
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
                $('#Supplier').val(result[0].nama_Supplier);
                $("#p_np").val(result[0].flag_ppn);
            }
           
            bindGrid();


        }

    });
    //getSup();
}

function getData(idData) {
    var urlLink = urlGetQC;
    var filterdata = {
        id: idData
        //no_trans: idData,
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            pods = [];
            for (var i = 0; i <= result.length - 1; i++) {
                pods.push({

                    kd_stok: result[i].kd_stok,
                    Kode_Barang: result[i].kd_stok,
                    nama_barang: result[i].nama_barang,
                    nama_Barang: result[i].nama_Barang,
                    kd_satuan: result[i].kd_satuan,
                    kd_satuan: result[i].kd_satuan,
                    qty_order: result[i].qty_order,
                    qty_sisa: result[i].qty_sisa,
                    qty_qc_pass: result[i].qty_qc_pass,
                    qty_sisa: result[i].qty_sisa,
                    nama_Gudang: result[i].nama_Gudang

                });
            }
            //console.log(result[0].no_ref);

            if (result.length > 0) {
                $("#PONumber").val(result[0].no_ref);
                $("#NoTransaksi").val(result[0].no_trans);
                $("#Keterangan").val(result[0].keterangan);
                $("#sj_supplier").val(result[0].sj_supplier);
                $("#Supplier").val(result[0].nama_Supplier);
                $('#Keterangan').attr("disabled", "disabled");
                $('#sj_supplier').attr("disabled", "disabled");

             
                
                debugger;
                if (Mode === "VIEW") {
                    console.log(result[0].no_ref);
                    $("#vPONumber").val(result[0].no_ref);
                    //$("#v_noqc").val(GudangDs[0].no_qc);
                    columns = [
                    { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                    { field: "nama_barang", title: "Nama Barang", width: "90px" },
                    { field: "kd_satuan", title: "Satuan", width: "80px" },
                    { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
                    { field: "qty_qc_pass", title: "Qty Good", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_qc_pass ? data.qty_qc_pass.sum: 0 #", footerAttribute: { "id": "qty_qc_pass" }, editor: valueValidation },
                    { field: "qty_sisa", title: "Qty Reject", width: "110px", attributes: { class: "text-right " } },
                    { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                    { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
                    { field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
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
                        { field: "nama_barang", title: "Nama Barang", width: "90px" },
                        { field: "kd_satuan", title: "Satuan", width: "80px" },
                        { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
                        { field: "qty_qc_pass", title: "Qty Good", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_qc_pass ? data.qty_qc_pass.sum: 0 #", footerAttribute: { "id": "qty_qc_pass" }, editor: valueValidation },
                        { field: "qty_sisa", title: "Qty Reject", width: "110px", attributes: { class: "text-right " } },
                        { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                        { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
                        { field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
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

function showCreate() {
    window.location.href = urlCreate;
}
function fillCbonotrans() {
    $("#PONumber").empty();
    $("#PONumber").append('<option value="" selected disabled>Please select</option>');
    var data = NoTransList;


    for (var i = 0; i < data.length; i++) {
        $("#PONumber").append('<option value="' + data[i].no_po + '">' + data[i].no_po + '</option>');
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
        dataItem[i].set("lokasi", loksimpan);
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
        max: options.model.qty,
        min: 0
    });
}
function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: ColumnGrid,
        
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_po",
                    fields: {
                        no: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        nama_barang: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        qty_order: { type: "string", editable: false },
                        qty_qc_pass: { type: "number", editable: true },
                        qty_sisa: { type: "string", editable: true },
                        lokasi: { type: "string", editable: false },
                        harga: { type: "number", editable: false },
                        kd_supplier: { type: "string", editable: false },
                        rp_trans: { type: "number", editable: false },
                        nama_Supplier: { type: "string", editable: false },
                        nama_Gudang: { type: "string", editable: false }
                    }
                }
            },
            aggregate: [
                { field: "rp_trans", aggregate: "sum" },
                { field: "qty_qc_pass", aggregate: "sum" }
               
            ]


        },
        save: function (data) {
            if (data.values.qty_qc_pass) {
                data.model.set("rp_trans", data.values.qty_qc_pass * data.model.harga);
                data.model.set("qty_sisa", data.model.qty_order - data.values.qty_qc_pass);
                console.log(data.model.qty_sisa);
            }
            else if (data.values.qty_sisa) {
                data.model.set("qty_qc_pass", data.model.qty_order - data.values.qty_sisa);
                data.model.set("rp_trans", data.values.qty_qc_pass * data.model.harga);
                console.log(data.model.qty_sisa);
            }
            else if (data.values.qty_qc_pass==0) {
                data.model.set("rp_trans", data.values.qty_qc_pass * data.model.harga);
                data.model.set("qty_sisa", data.model.qty_order - data.values.qty_qc_pass);
                console.log(data.model.qty_sisa);
            }
            else {
                test = data.model.set("rp_trans", data.model.qty_qc_pass * data.values.harga);
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
    var lok_simpan = $('#lok_simpan').val();
    var PONumber = $('#PONumber').val();
    validationMessage = '';

    if (!PONumber) {
        validationMessage = validationMessage + 'PO harus dipilih.' + '<br/>';
    }
    if (!sj_supplier) {
        validationMessage = validationMessage + 'SJ Supplier tidak boleh kosong.' + '<br/>';
    }
    if (!lok_simpan) {
        validationMessage = validationMessage + 'Lokasi Simpan harus dipilih.' + '<br/>';
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
        SaveData();
    }
}
function onPrintClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + idData;
    //wrapperList
    //var wrapper = document.getElementById("wrapperList");

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
            gudang.lokasi = gd_id;
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
        sj_supplier: $('#sj_supplier').val(),
        tgl_trans: $('#tanggal').val(),
        no_trans: $('#no_trans').val(),
        keterangan: $('#Keterangan').val(),
        lokasi: $('#lokasi').val(),
        tgl_po: $('#tanggal').val(),
        kd_supplier: $('#Supplier').val(),
        lok_simpan: $('#lok_simpan').val(),
        p_np: $('#p_np').val(),
        kode_gudang: $('#lok_simpan').val(),
        penyerah: $('#nm_penyerah').val(),
        podetail: _GridPO.dataSource.data().toJSON()
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
                        Swal.fire({
                            type: 'success',
                            title: 'Success',
                            html: result.message
                        });
                        startSpinner('loading..', 0); //window.location.href = urlCreate 
                        //sleep(2000);
                        //window.location.href = urlCreate 
                        window.location.href = urlInventory + '?id=' + result.result + '&mode=VIEW';
                        //window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';;
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