
var GudangDs = [];
var pods = [];
var POds = [];
var urlAction = "";
var barangList = [];
var satuanList = [];
var _GridPO;
var columngrid = [];
var listBarang = [];
var SatuanList = [];
var GudangList = [];
var SatuanListParam = [];
var GUdangListParam = [];
var optionsGrid = {
    pageSize: 10
};
var HiddenAction;


$(document).ready(function () {
    
    if (Mode === "VIEW") {
        HiddenAction = true;
    }

    startSpinner('Loading..', 1);

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

   
        $.when(GetBarang()).done(function () {
            $.when(GetSatuan()).done(function () {
                $.when(GetGudang()).done(function () {
                bindGridDetail();   
                prepareActionGrid();
                    startSpinner('loading..', 0);
                });
            });
        });
  
    console.log(GudangList);

    if (Mode !== "NEW") {
        getDataTerima(id);
    }

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            return false;
        }
    });
});

$(document).on('keydown', function (event) {
    if (event.key == "F2") {
        onAddNewRow();
        return false;
    }
});

function prepareActionGrid() {
    $(".viewData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        showForm(id);

    });
    $(".editData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        showForm(id);

    });
}

function GetBarang() {
    return $.ajax({
        url: urlGetBarang,
        type: "POST",
        success: function (result) {

            listBarang = result;

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function GetGudang() {
    return $.ajax({
        url: UrlGetGudang,
        type: "POST",
        success: function (result) {
            GudangList = result;
            $("#IdGudang").empty();
            $("#IdGudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;
          
            for (var i = 0; i < data.length; i++) {
                $("#IdGudang").append('<option value="' + data[i].kode_Gudang + '">' + data[i].nama_Gudang + '</option>');
            }

            $('#IdGudang').selectpicker('refresh');
            $('#IdGudang').selectpicker('render');

            if (BranchUser != "") {
                $("#IdGudang option[value='" + BranchUser + "']").attr("selected", "selected");
                $('#IdGudang').selectpicker('refresh');
                $('#IdGudang').selectpicker('render');
                $('#IdGudang').attr("disabled", "disabled");
            }

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

function fillBarang() {
    $("#Kd_Stok").empty();
    $("#Kd_Stok").append('<option value="" selected disabled>Pilih Barang</option>');
    for (var i = 0; i < listBarang.length; i++) {
        $("#Kd_Stok").append('<option value="' + listBarang[i].kode_Barang + '">' + listBarang[i].nama_Barang + '</option>');
    }
    $('#Kd_Stok').selectpicker('refresh');
    $('#Kd_Stok').selectpicker('render');
}

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    var Tgl_Diperlukan = $('#tanggal').val();
    var Keterangan = $('#Keterangan').val();
    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';
    if (!Tgl_Diperlukan) {
        validationMessage = validationMessage + 'Tanggal Diperlukan harus di pilih.' + '\n';
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

function SaveData() {
    var dataterimabebas = {
        tgl_trans: $('#tanggal').val(),
        kode_gudang: $('#IdGudang').val(),
        Keterangan: $('#Keterangan').val(),
        gddetail: _GridPO.dataSource.data().toJSON()
    };
    // console.log(dataRequest);
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
                data: dataterimabebas,
                url: urlSave,
                success: function (result) {
                    if (result.Success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
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
        } else {
            return false;
        }
    });
}

function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

function bindGridDetail() {
    _GridPO = $("#GridPODetail").kendoGrid({
        columns: [
            { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
            { field: "kd_satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
            { field: "qty_in", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "harga", title: "Harga", width: "90px" ,hidden:true },
            { command: ["edit", "destroy"], title: "Actions", width: "110px", hidden:HiddenAction }
        
           

        ],
        dataSource: {
            data: POds,
            schema: {
                model: {
                    fields: {
                        //no: { type: "string" },
                        kd_stok: { type: "string" },
                        Kode_Barang: { type: "string" },
                        Nama_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        rek_persediaan: { type: "string" },
                        Nama_Satuan: { type: "string" },
                        kd_satuan: { type: "string" },
                        gudang_tujuan: { type: "string" },
                        nama_Gudang: { type: "string" },
                        satuan: { type: "string" },
                        qty_in: { type: "number", validation: { required: true, min: 1, defaultValue: 0 } },
                        harga: { type: "string" },
                        keterangan: { type: "string" },
                        no_dpm: { type: "string" }
                    }
                }
            },
            aggregate: [
                { field: "qty", aggregate: "sum" }
            ]
        },
        save: function (e) {

        },
        cancel: function (e) {
            $('#GridPODetail').data('kendoGrid').dataSource.cancelChanges();
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

function NamaGudang(gudang_tujuan) {
    for (var i = 0; i < GudangList.length; i++) {
        if (GudangList[i].kode_Gudang === gudang_tujuan) {
            return GudangList[i].nama_Gudang;
        }
    }
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

function barangDropDownEditor(container, options) {
    var input = $('<input required id="Kode_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Barang",
        dataValueField: "nama_Barang",
        dataSource: listBarang,
        optionLabel: "Select Barang...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.kode_Barang}' data-Barang='${data.nama_Barang}'>${data.nama_Barang} </span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            var barang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            barang.kd_stok = id;
            barang.nama_Barang = Barang;
            barang.Kode_Barang = id;
            barang.Nama_Barang = Barang;
            barang.nama_barang = Barang;

            var dataItem = e.sender.dataItem();

            SatuanListParam = [];
            var found = GetSatuanCode(id);

            var a = found[0].rek_persediaan;
            barang.rek_persediaan = found[0].rek_persediaan;
            //barang.last_price = found[0].last_price;
            for (var i = 0; i < SatuanList.length; i++) {
                if (SatuanList[i].kode_Satuan === found[0].kd_Satuan) {
                    var Key = SatuanList[i].kode_Satuan;
                    var Value = SatuanList[i].nama_Satuan;

                    SatuanListParam.push({
                        Kode_Satuan: Key,
                        Nama_Satuan: Value
                    });
                }
            }
            inputSatuan.setDataSource(SatuanListParam);
            inputSatuan.refresh();
            inputSatuan.enable(true);
            inputSatuan.select(1);

            $("#Kode_Satuan").data('kendoDropDownList').value(SatuanListParam[0].Kode_Satuan);

            var satuan = _GridPO.dataItem($(e.sender.element).closest("tr"));
            satuan.kd_satuan = SatuanListParam[0].Kode_Satuan;
            satuan.satuan = SatuanListParam[0].Nama_Satuan;
            satuan.Kode_satuan = SatuanListParam[0].Kode_Satuan;
            satuan.Kode_Satuan = SatuanListParam[0].Kode_Satuan;
            satuan.Nama_Satuan = SatuanListParam[0].Nama_Satuan;
        }
      
    }).appendTo(container);
}

function convertValues(value) {
    var data = {};

    value = $.isArray(value) ? value : [value];

    for (var idx = 0; idx < value.length; idx++) {
        data["values[" + idx + "]"] = value[idx];
    }

    return data;
}

function GetSatuanCode(code) {
    return listBarang.filter(
        function (listBarang) { return listBarang.kode_Barang === code; }
    );
}

function GetGudangCode(code) {
    return GudangList.filter(
        function (GudangList) { return GudangList.kode_Gudang === code; }
    );
}


function satuanDropDownEditor(container, options) {
    inputSatuan = $("<input required  id='Kode_Satuan' name='Nama_Satuan'  />")
        .attr("Kode_Satuan", "Nama_Satuan")
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Nama_Satuan",
            dataValueField: "Nama_Satuan",
            dataSource: SatuanListParam,
            //optionLabel: "Pilih Satuan",
            template: "<span data-id='${data.Kode_Satuan}' data-Satuan='${data.Nama_Satuan}'>${data.Nama_Satuan}</span>",
            select: function (e) {
                var id = e.item.find("span").attr("data-id");
                var Satuan = e.item.find("span").attr("data-Satuan");
                var satuan = _GridPO.dataItem($(e.sender.element).closest("tr"));
                satuan.kd_satuan = id;
                satuan.satuan = Satuan;
                satuan.Kode_satuan = id;
                satuan.Kode_Satuan = id;
                satuan.Nama_Satuan = Satuan;
            }
        }).data("kendoDropDownList");
}

function GudangDropDownEditor(container, options) {

    inputGudang = $('<input id="kode_Gudang" name="kode_Gudang">')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_Gudang",
            dataValueField: "kode_Gudang",
           
            dataSource: GetGudangCode(BranchUser),
            optionLabel: "Pilih Satuan",
            select: function (e) {
                var dataItem = this.dataItem(e.item.index());
                var bookId = dataItem.kode_Gudang;
                var id = e.dataitem.kode_Gudang;
                var Gudang = e.dataitem.nama_Gudang;
                var gudang = _GridPO.dataItem($(e.sender.element).closest("tr"));
               
                gudang.gudang = Gudang;
                gudang.gudang_tujuan = id;
                gudang.gudang_tujuan = id;
                gudang.Nama_Gudang = Gudang;
            }
        }).data("kendoDropDownList");
}
//oooo

function onAddNewRow() {
    startSpinner('Loading..', 1);
    var grid = $("#GridPODetail").data("kendoGrid");
    grid.addRow();
    $('#Kode_Barang').data("kendoDropDownList").open();
    startSpinner('Loading..', 0);
}


function getDataTerima(id) {
    startSpinner('loading..', 1);
    var urlLink = urlGetDataTerima;
    var filterdata = {
        id: id,
        Program_name : "FRM_TRMBEBAS",
        status_po: ""
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            POds = result;
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

                    POds = [];
                    for (var i = 0; i <= result.length - 1; i++) {
                        POds.push({

                            Kode_Barang: result[i].Kode_Barang,
                            kd_stok: result[i].kd_stok,
                            nama_Barang: result[i].nama_Barang,
                            qty_order: result[i].qty_order,
                            qty_in: result[i].qty_in,
                            kd_satuan: result[i].kd_satuan,
                            gudang_tujuan: result[i].gudang_tujuan,
                            harga: result[i].harga,
                            nama_Gudang: result[i].lokasi_simpan,
                            gudang_asal: result[i].gudang_asal,
                            nm_Gudang_asal: result[i].nm_Gudang_asal

                        });
                    }
                    console.log(POds);
                    $('#GridPODetail').kendoGrid('destroy').empty();
                    bindGridDetail();


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

     
        $("#no_ref").attr("disabled", "disabled");
     
       
        $("#Supplier").attr("disabled", "disabled");
        $("#nm_penyerah").attr("disabled", "disabled");
        $("#Keterangan").attr("disabled", "disabled");
        // $("#nm_Gudang_asal").data("kendoDropDownList").readonly();

    }

    if (Mode === "EDIT" || Mode === "VIEW") {
        if (terimads.length > 0) {
            //$("#no_qc").data("kendoDropDownList").value(GudangDs[0].no_qc);
            //$("#no_qc").val(POds[0].no_qc);
          
            $("#no_trans").val(terimads[0].no_trans);
            $("#no_ref").val(terimads[0].no_ref);
            $("#nm_penyerah").val(terimads[0].penyerah);
            $("#Keterangan").val(terimads[0].keterangan);
            $("#Supplier").val(terimads[0].nama_Supplier);
            $("#tanggal").val(terimads[0].tgl_transdesc);
          

        }
    }
}

