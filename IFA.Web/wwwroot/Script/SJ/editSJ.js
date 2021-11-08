
var pods = [];
var GudangList = [];
var userDs = NoTransList;
var kd = null;
var ColumnGrid = [
    { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
    { field: "no_sj", title: "Surat Jalan", width: "80px" },
    { field: "no_sp", title: "No SO", width: "80px" },
    { field: "nama_barang", title: "Nama Barang", attributes: { class: "text-right ", 'style': 'background-color: Aquamarine; color:black;' }, width: "100px" },
    { field: "kd_satuan", title: "Satuan", width: "40px" },
    { field: "qty_kirim", title: "Qty Kirim", width: "40px", attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_kirim ? data.qty_kirim.sum: 0 #", footerAttribute: { "id": "qty_kirim" } },
    //{ field: "qty_balik", title: "Qty Kembali", width: "40px", validation: { required: true, min: 0, defaultValue: 0 }, attributes: { class: "text-right ", 'style': 'background-color: Beige; color:black;' }, "footerTemplate": "Total: #: data.qty_balik ? data.qty_balik.sum: 0 #", footerAttribute: { "id": "qty_balik" }, editor: valueValidation },
   // { field: "qty_out", title: "Qty Terkirim", width: "40px", attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_out ? data.qty_out.sum: 0 #", footerAttribute: { "id": "qty_out" } },
    { field: "keterangan", title: "Keterangan", attributes: { class: "text-right ", 'style': 'background-color: Aquamarine; color:black;' }, width: "100px" }

];
var editval = true;
$(document).ready(function () {

    //  console.log(JenisBarangList);
    //alert(dateserver);
    if (Mode != "NEW") {
        getData(idData);
    }


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
            console.log(JSON.stringify(pods));
            //console.log(pods);
            if (result.length > 0) {
                $('#nama_cust').val(result[0].nama_cust);
                $('#alamat_cust').val(result[0].alamat_cust);
                $('#no_dpb').val(result[0].no_dpb);
                $('#no_sj').val(result[0].no_sj2);
                $('#no_sj2').val(result[0].no_sj2);
                $('#no_so').val(result[0].no_sp);
                $('#No_Gudang_Out').val(result[0].no_Gudang_Out);

            }

            bindGrid();


        }

    });
    //getSup();
}

function getData(idData) {
    var urlLink = urlGetsavedSJ;
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
                    no_sj: result[i].no_sj,
                    no_sj2: result[i].no_sj2,
                    no_sp: result[i].no_sp,
                    kd_stok: result[i].kd_stok,
                    Kode_Barang: result[i].kd_stok,
                    nama_barang: result[i].nama_barang,
                    nama_Barang: result[i].nama_Barang,
                    kd_satuan: result[i].kd_satuan,
                    kd_satuan: result[i].kd_satuan,
                    qty_kirim: result[i].qty_kirim,
                   // qty_out: result[i].qty_out,
                    keterangan: result[i].keterangan
                    //qty_balik: result[i].qty_balik



                });
            }
            //console.log(result);

            if (result.length > 0) {
                $("#PONumber").val(result[0].no_ref);
                $("#NoTransaksi").val(result[0].no_trans);
                $("#nm_agen").val(result[0].nm_agen);
                $("#almt_agen").val(result[0].Almt_agen);
                //$("#Supplier").val(result[0].nama_Supplier);
                //$('#Keterangan').attr("disabled", "disabled");
                //$('#sj_supplier').attr("disabled", "disabled");

                //ColumnGrid = [
                //    { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                //    { field: "nama_Barang", title: "Nama Barang", width: "90px" },
                //    { field: "kd_satuan", title: "Satuan", width: "80px" },
                //    { field: "qty_order", title: "Qty Order", width: "110px", attributes: { class: "text-right " } },
                //    { field: "qty_qc_pass", title: "Qty Good", width: "110px", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_qc_pass ? data.qty_qc_pass.sum: 0 #", footerAttribute: { "id": "qty_qc_pass" }, editor: valueValidation },
                //    { field: "qty_sisa", title: "Qty Reject", width: "110px", attributes: { class: "text-right " } },
                //    { field: "nama_Gudang", title: "Lokasi Simpan", width: "180px", editor: Gudangdropdown },
                //    { field: "harga", title: "Harga", format: "{0:#,0.00}", width: "180px", hidden: true },
                //    { field: "nama_Supplier", title: "kd_supplier", width: "80px", hidden: true },
                //    //{ field: "rp_trans", title: "Total", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    {
                //        field: "rp_trans", title: "Total", hidden: true, "footerTemplate": "Total: #: data.rp_trans ? data.rp_trans.sum: 0 #", editor: function (cont, options) {
                //            $("<span>" + options.model.rp_trans + "</span>").appendTo(cont);
                //        }
                //    }
                //];

                debugger;
                if (Mode === "VIEW") {
                    columns = [
                        { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                        { field: "no_sj", title: "Surat Jalan", width: "80px" },
                        { field: "no_sp", title: "No SO", width: "80px" },
                        { field: "nama_barang", title: "Nama Barang", width: "100px" },
                        { field: "kd_satuan", title: "Satuan", width: "40px" },
                        { field: "qty_kirim", title: "Qty Kirim", width: "40px", attributes: { class: "text-right " } },
                       
                        { field: "keterangan", title: "Keterangan", width: "120px" }
                    ];

                }
                else if (Mode === "EDIT") {
                    columns = [
                        { field: "no", title: "No", width: "20px", template: "<span class='row-number'></span>" },
                        { field: "no_sj", title: "Surat Jalan", width: "80px" },
                        { field: "no_sp", title: "No SO", width: "80px" },
                        { field: "nama_barang", title: "Nama Barang", width: "120px" },
                        { field: "kd_satuan", title: "Satuan", width: "30px" },
                        { field: "qty_kirim", title: "Qty Kirim", width: "40px", attributes: { class: "text-right " } },
                       
                        { field: "keterangan", title: "Keterangan", width: "120px" }

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

    console.log(NoTransList);
    for (var i = 0; i < data.length; i++) {
        $("#PONumber").append('<option value="' + data[i].no_sj + '">' + data[i].no_sj + '</option>');
    }
    //$("#tanggal").datepicker().datepicker("setDate", new Date());
    // $('#tanggal').val(dateserver);
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

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        max: options.model.qty_kirim,
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
                    id: "no_sj",
                    fields: {
                        no: { type: "string", editable: false },
                        no_sj: { type: "string", editable: false },
                        no_sj2: { type: "string", editable: false },
                        no_sp: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        nama_barang: { type: "string", editable: true },
                        kd_satuan: { type: "string", editable: false },
                        //qty_balik: { type: "string", editable: true },
                        qty_kirim: { type: "string", editable: false },
                        //qty_out: { type: "string", editable: true },
                        keterangan: { type: "string", editable: true },
                        lokasi: { type: "string", editable: true },
                        harga: { type: "number", editable: false },

                        rp_trans: { type: "number", editable: true }
                        //nama_Supplier: { type: "string", editable: false },
                        //nama_Gudang: { type: "string", editable: true }
                    }
                }
            },
            aggregate: [
                { field: "rp_trans", aggregate: "sum" },
                { field: "qty_kirim", aggregate: "sum" }
               // { field: "qty_out", aggregate: "sum" },
                //{ field: "qty_balik", aggregate: "sum" }

            ]


        },
        save: function (data) {
            if (data.values.qty_balik) {

                data.model.set("qty_out", (data.model.qty_kirim * 1) - (data.values.qty_balik * 1));
                console.log(data.model.qty_out);
            }
            //else {
            //    test = data.model.set("rp_trans", data.model.qty_balik * data.values.harga);
            //}
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

    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';


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

    var no = "";
    if (Mode !== "NEW") {
        no = $('#PONumber').val();
        kd = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        no_sj: $('#PONumber').val(),
        no_sj2: $('#no_sj2').val(),
        no_sp: $('#no_so').val(),
        nama_agent: $('#nama_cust').val(),
        //tgl_trans: dateserver,
        // ongkir: $('#ongkir').val(),
        Almt_agen: $('#alamat_cust').val(),
        no_dpb: $('#no_dpb').val(),
        No_Gudang_Out: $('#No_Gudang_Out').val(),

        sjdetail: _GridPO.dataSource.data().toJSON()
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
                            html: result.Message
                        });
                        startSpinner('loading..', 4); //window.location.href = urlCreate 
                        //sleep(2000);
                        window.location.href = urlCreate
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