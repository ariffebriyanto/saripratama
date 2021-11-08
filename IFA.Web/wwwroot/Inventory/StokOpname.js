var _GridStokOpname;
var Opnameds = [];
var SatuanListParam = [];
var BarangList = [];
var GudangList = [];
var SatuanList = [];
var saldods = [];

function qty_datalbl(container, options) {
    var input = $('<label id="qty_datalabel" />');
    input.appendTo(container);
}

function qty_selisihlbl(container, options) {
    var input = $('<label id="qty_selisihlabel" />');
    input.appendTo(container);
}

function satuanlbl(container, options) {
    var input = $('<label id="satuanlabel" />');
    input.appendTo(container);
}

function keterangantxt(container, options) {
    var input = $('<input id="keterangantext" style="width:100%;" />');
    input.appendTo(container);
}

function valueValidation(container, options) {

    var input = $("<input name='" + options.field + "' id='qty_outnumeric'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
        min: 1,
        change: function () {
            var value = this.value();
            var qtydata = $("#qty_datalabel").text();
            $("#qty_selisihlabel").text((value * 1) - (qtydata * 1) );
            //if ((qtydata * 1) < (value * 1)) {
            //    $("#keterangantext").val("BARANG KURANG");
            //}
            //else {
            //    $("#keterangantext").val("BARANG LEBIH");

            //}

            if ((value * 1) > (qtydata * 1)) {
                $("#keterangantext").val("BARANG LEBIH");
            }
            else if ((qtydata * 1) == (value * 1)) {
                ds[i].keterangan = "";
            }
            else {
                $("#keterangantext").val("BARANG KURANG");
            }
        }
    });
}

var columnOpname = [
    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "90px", editor: satuanlbl },
    { field: "qty_data", title: "Qty Data", width: "80px", editor: qty_datalbl, format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "qty_opname", title: "Qty Manual", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation },
    { field: "qty_selisih", title: "Qty Selisih", width: "80px", editor: qty_selisihlbl, format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "keterangan", title: "Keterangan", width: "180px", editor: keterangantxt },
    { command: ["edit", "destroy"], title: "Actions", width: "80px" }
];
var editval = true;
$(document).ready(function () {
    startSpinner('Loading..', 1);

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    });
    $("#tanggal").val(dateserver);
    bindGrid();
    $.when(getGudang()).done(function () {
        $.when(GetSatuan()).done(function () {
            $.when(GetBarang()).done(function () {
                $.when(getData(idOpname)).done(function () {
                    startSpinner('loading..', 0);
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
            console.log(result);

            if (result.length > 0) {
                $("#NoTransaksi").val(result[0].no_trans);
                $("#tanggal").val(result[0].tgl_transdesc);
                $("#Keterangan").val(result[0].keterangan);
                $('#Keterangan').attr("disabled", "disabled");
                $('#tanggal').attr("disabled", "disabled");


                columnOpname = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px" },
                    { field: "qty_data", title: "Qty Data", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "qty_opname", title: "Qty Manual", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "qty_selisih", title: "Qty Selisih", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "keterangan", title: "Keterangan", width: "180px" }
                ];
                editval = false;

                $("#addNew").hide();
                $("#save").hide();
                $("#new").show();

                if (_GridStokOpname != undefined) {
                    $("#GridStokOpname").kendoGrid('destroy').empty();
                }
                for (var i = 0; i <= result[0].opnamedtl.length - 1; i++) {
                    $("#gudang option[value='" + result[0].opnamedtl[i].kode_gudang + "']").attr("selected", "selected");
                    $('#gudang').selectpicker('refresh');
                    $('#gudang').selectpicker('render');
                    $('#gudang').attr("disabled", "disabled");
                    Opnameds.push({
                        kd_stok: result[0].opnamedtl[i].kd_stok,
                        Kode_Barang: result[0].opnamedtl[i].kd_stok,
                        Nama_Barang: result[0].opnamedtl[i].nama_barang,
                        nama_Barang: result[0].opnamedtl[i].nama_barang,
                        Kode_satuan: result[0].opnamedtl[i].kd_satuan,
                        Nama_Satuan: result[0].opnamedtl[i].kd_satuan,
                        kd_satuan: result[0].opnamedtl[i].kd_satuan,
                        satuan: result[0].opnamedtl[i].kd_satuan,
                        qty_data: result[0].opnamedtl[i].qty_data,
                        qty_opname: result[0].opnamedtl[i].qty_opname,
                        qty_selisih: result[0].opnamedtl[i].qty_selisih,
                        keterangan: result[0].opnamedtl[i].keterangan
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
    var cb = $("#gudang").val();
    return $.ajax({
        url: urlBarang + "/?cb=" + cb,
        type: "POST",
        success: function (result) {
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
        url: urlGudang + "/?filter='cabang'",
        type: "POST",
        success: function (result) {
            $("#gudang").empty();
            $("#gudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#gudang").append('<option value="' + data[i].kode_Gudang + '">' + data[i].nama_Gudang + '</option>');
            }

            $("#gudang option[value='" + Gudang + "']").attr("selected", "selected");
            $('#gudang').selectpicker('refresh');
            $('#gudang').selectpicker('render');

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
                        kd_stok: { type: "string" },
                        Kode_Barang: { type: "string" },
                        Nama_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        Kode_satuan: { type: "string" },
                        Nama_Satuan: { type: "string" },
                        kd_satuan: { type: "string" },
                        satuan: { type: "string" },
                        qty_data: { type: "number" },
                        qty_opname: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        qty_selisih: { type: "number" },
                        rek_persediaan: { type: "string", editable: false },
                        keterangan: { type: "string" }
                    }
                }
            },
            aggregate: [
                { field: "qty_opname", aggregate: "sum" },
                { field: "qty_data", aggregate: "sum" },
                { field: "qty_selisih", aggregate: "sum" }
            ]
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        save: function (data) {
            var grid = this;

            setTimeout(function () {
                var ds = $('#GridStokOpname').data('kendoGrid').dataSource.data();
                Opnameds = [];
                for (var i = 0; i <= ds.length - 1; i++) {
                    ds[i].qty_selisih = ds[i].qty_opname - ds[i].qty_data;
                    if (ds[i].qty_opname > ds[i].qty_data) {
                        ds[i].keterangan = "BARANG LEBIH";
                    }
                    else if (ds[i].qty_opname == ds[i].qty_data) {
                        ds[i].keterangan = "";
                    }
                    else {
                        ds[i].keterangan = "BARANG KURANG";
                    }
                    Opnameds.push({
                        kd_stok: ds[i].kd_stok,
                        Kode_Barang: ds[i].Kode_Barang,
                        Nama_Barang: ds[i].Nama_Barang,
                        nama_Barang: ds[i].nama_Barang,
                        Kode_satuan: ds[i].Kode_satuan,
                        Nama_Satuan: ds[i].Nama_Satuan,
                        kd_satuan: ds[i].kd_satuan,
                        satuan: ds[i].satuan,
                        qty_data: ds[i].qty_data,
                        qty_opname: ds[i].qty_opname,
                        qty_selisih: ds[i].qty_selisih,
                        rek_persediaan: ds[i].rek_persediaan,
                        keterangan: ds[i].keterangan
                    });
                }
                $('#GridStokOpname').kendoGrid('destroy').empty();
                bindGrid();
            });
        },
        edit: function (e) {
            addCustomCssButtonCommand();

            if (e.model.kd_stok != null && e.model.kd_stok != "") {
                $("#qty_datalabel").text(e.model.qty_data);
                $("#satuanlabel").text(e.model.satuan);

                $("#qty_selisihlabel").text(e.model.qty_selisih);

                $("#keterangantext").val(e.model.keterangan);
            }

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
            barang.nama_barang = Barang;

            SatuanListParam = [];
            var found = GetSatuanCode(id);
            barang.Kode_satuan = found[0].kd_Satuan;
            barang.Nama_Satuan = found[0].kd_Satuan;
            barang.kd_satuan = found[0].kd_Satuan;
            barang.satuan = found[0].kd_Satuan;
            barang.qty_data = found[0].qty_data;
            barang.rek_persediaan = found[0].rek_persediaan;
            $("#satuanlabel").text(found[0].kd_Satuan);
            $("#qty_datalabel").text(found[0].qty_data);

            var numerictextbox = $("#qty_outnumeric").data("kendoNumericTextBox");
            numerictextbox.value(0);
            $("#qty_selisihlabel").text(found[0].qty_data * -1);
            //qty_outnumeric
            //qty_selisihlabel
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
    var tanggal = $('#tanggal').val();
    var ItemData = _GridStokOpname.dataSource.data();
    validationMessage = '';
    if (!gudang) {
        validationMessage = validationMessage + 'Gudang harus di pilih.' + '\n';
    }
    if (!tanggal) {
        validationMessage = validationMessage + 'Tanggal harus di isi.' + '\n';
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

function savedata() {
    var transno = "";
    if (Mode != "NEW") {
        transno = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        no_trans: transno,
        tgl_trans: $('#tanggal').val(),
        keterangan: $('#Keterangan').val(),
        gudang: $('#gudang').val(),
        opnamedtl: _GridStokOpname.dataSource.data().toJSON()
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