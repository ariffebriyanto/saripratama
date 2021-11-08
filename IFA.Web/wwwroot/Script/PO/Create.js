var POds = [];
var barangList = [];
var satuanList = [];
var _GridPO;
var _GridDPM;
var columngrid = [];
var dpmds = [];
var selecteddpmds = [];
var acceptdpmds = [];
var selectRequest;
var vSubTotal;
var vTotalRupiah;
var vTotal;
var vPPN;
var alamatkirimds = [];
$(document).ready(function () {
    startSpinner('Loading..', 1);
    //  console.log(JenisBarangList);
    $('#tanggalBayar').datepicker({
        format: 'dd MM yyyy',
        //startDate: 'd',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        //$('#tanggal').datepicker('setEndDate', minDate);

        var diff = calcDate($('#tglBayar').val(), $('#tanggal').val());
        $("#Lama").val(diff);
    });

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        //startDate: 'd',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        // $('#tanggalBayar').datepicker('setStartDate', minDate);
        $('#divtanggalkirim').datepicker('setStartDate', minDate);

        var diff = calcDate($('#tglBayar').val(), $('#tanggal').val());
        $("#Lama").val(diff);
    });

    $("#tanggal").val(dateserver);
    //$('#divtanggal').datepicker('remove');
    //$('#tanggal').attr("disabled", "disabled");


    $('#divtanggalkirim').datepicker({
        format: 'dd MM yyyy',
        //startDate: 'd',
        todayBtn: 'linked',
        "autoclose": true
    });

    $("#tanggalkirim").val(seminggu);
    $('#tglBayar').val(seminggu);
    var diff = calcDate($('#tglBayar').val(), $('#tanggal').val());
    $("#Lama").val(diff);

    $.when(getAlamatKirim()).done(function () {
        $("#alamatkirim").empty();
        $("#alamatkirim").append('<option value="" selected disabled>Please select</option>');
        var data = alamatkirimds;

        for (var i = 0; i < data.length; i++) {
            $("#alamatkirim").append('<option value="' + data[i].id + '">' + data[i].nama + '</option>');
        }

        $('#alamatkirim').selectpicker('refresh');
        $('#alamatkirim').selectpicker('render');
    });

    //divtanggalkirim
    //fillForm();

    $("#checkbox2").change(function () {
        ItemCalc();
    });

    $.when(fillCboSupplier()).done(function () {
        if (Mode !== "NEW") {
            getDataPO(idPO);
        }
        else {
            $.when(getData()).done(function () {
                columngrid = [
                    { field: "Action", title: "Last Invoice", width: "80px", template:"<center style='display:inline;'>&nbsp&nbsp<a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=kd_stok#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>" },
                    { field: "nama_Barang", title: "Nama Stok", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                    { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "last_price", title: "Last Price", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: lastpriceLabel },
                    { field: "harga", title: "Harga", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: hargaNumeric },
                    { field: "prosen_diskon", title: "Disc %1", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                    { field: "diskon2", title: "Disc %2", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                    { field: "diskon3", title: "Disc %3", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                    { field: "diskon4", title: "Disc Rp.", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                    {
                        field: "Bonus", title: "Bonus", width: "50px", editor: customBoolEditor,
                        template: '#=dirtyField(data,"Bonus")#<center><input disabled type="checkbox" #= Bonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
                    },
                    { field: "total", title: "Total", width: "110px", format: "{0:#,0.00}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { field: "keterangan", title: "Keterangan", width: "160px" },
                    { command: ["edit", "destroy"], title: "Actions", width: "110px" }
                ];
                bindGrid();
                startSpinner('loading..', 0);
            });
        }
    });


    barangList = [];

    satuanList = [];


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

function getAlamatKirim() {
    return $.ajax({
        url: urlGetAlamatKirim,
        success: function (result) {
            alamatkirimds = [];
            alamatkirimds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function onalamatChanged() {
    var alamat = $("#alamatkirim").val();
    var alamatkirim = Getalamatkirimbyid(alamat);
    $("#Keterangan").val(alamatkirim[0].alamat_kirim);
}

function Getalamatkirimbyid(code) {
    return alamatkirimds.filter(
        function (alamatkirimds) { return alamatkirimds.id === code; }
    );
}

function customBoolEditor(container, options) {
    var guid = kendo.guid();
    $('<center><input id="chkStatus" type="checkbox" name="Bonus" data-type="boolean" data-bind="checked:Bonus" onchange="onChkChanged();"></center>').appendTo(container);
}

function dirtyField(data, fieldName) {
    var hasClass = $("[data-uid=" + data.uid + "]").find(".k-dirty-cell").length < 1;
    if (data.dirty && data.dirtyFields[fieldName] && hasClass) {
        return "<span class='k-dirty'></span>"
    }
    else {
        return "";
    }
}

function totalLabel(container, options) {
    var input = $('<label id="total" />');
    input.appendTo(container);
}

function lastpriceLabel(container, options) {
    var input = $('<label id="lastprice" />');
    input.appendTo(container);
}
function hargaNumeric(container, options) {
    var input = $('<input id="harga_num" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 2,
        change: function (e) {
            var value = this.value();
            var barang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            barang.harga = value;
          
        }
    });
}

function onChkChanged() {
    if ($("#harga_num").val() != 0) {
        harga = $("#harga_num").val();
    }
    if ($('#chkStatus').is(":checked")) {
        $("#harga_num").val(0);
    }
    else {
        $("#harga_num").val(harga);
    }
}


$(document).on('keydown', function (event) {
    if (event.key == "F2") {
        onAddNewRow();
        return false;
    }
});


function fillCboSupplier() {
  
    $("#kd_supplier").empty();
    $("#kd_supplier").append('<option value="" selected disabled>Please select</option>');
    var data = SupplierList;

    for (var i = 0; i < data.length; i++) {
        $("#kd_supplier").append('<option value="' + data[i].kd_supplier + '">' + data[i].Nama_Supplier + '</option>');
    }
    //var test = 'S04165';
    //$("#kd_supplier option[value='" + test + "']").attr("selected", "selected");

    $('#kd_supplier').selectpicker('refresh');
    $('#kd_supplier').selectpicker('render');
}

function getData() {

}

function calcDate(dtStart, dtEnd) {
    if (dtEnd !== null && dtStart !== null && dtEnd !== "" && dtStart !== "") {
        var diff = Math.abs(new Date(dtEnd).getTime() - new Date(dtStart).getTime());
        const diffDays = Math.ceil(diff / (1000 * 60 * 60 * 24));
        return diffDays;
    }
    else {
        return 0;
    }

}

function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: columngrid,
        dataSource: {
            data: POds,
            schema: {
                model: {
                    fields: {
                        //no: { type: "string" },
                        Action: { type: "string", editable: false },
                        kd_stok: { type: "string" },
                        Kode_Barang: { type: "string" },
                        Nama_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        Kode_satuan: { type: "string" },
                        Nama_Satuan: { type: "string" },
                        kd_satuan: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 0 } },
                        harga: { type: "number", validation: { required: true, min: 1, defaultValue: 0 } },
                        prosen_diskon: { type: "number", validation: { required: false, min: 0, max: 100, defaultValue: 0 } },
                        diskon2: { type: "number", validation: { required: false, min: 0, max: 100, defaultValue: 0 } },
                        diskon3: { type: "number", validation: { required: false, min: 0, max: 100, defaultValue: 0 } },
                        diskon4: { type: "number", validation: { required: false, min: 0, defaultValue: 0 } },
                        total: { type: "number", editable: false, nullable: false, validation: { required: true, min: 0, defaultValue: 0 } },
                        Bonus: { type: "boolean" },
                        //TglKedatangan: { type: "date" },
                        keterangan: { type: "string" },
                        pdm: { type: "string" },
                        last_price: { type: "number" }
                    }
                }
            },
            aggregate: [
                { field: "total", aggregate: "sum" },
                { field: "qty", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            addCustomCssButtonCommand();
            var dropdownlist = $("#Kode_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            var index = BarangList.findIndex(function (item, i) {
                return item.Kode_Barang === e.model.Kode_Barang;
            });
            dropdownlist.select(index);

            var found = GetSatuanCode(e.model.Kode_Barang);
            SatuanListParam = [];
            for (var i = 0; i < SatuanList.length; i++) {
                if (found[0] != undefined) {
                    if (SatuanList[i].Kode_Satuan === found[0].Kd_Satuan) {
                        var Key = SatuanList[i].Kode_Satuan;
                        var Value = SatuanList[i].Nama_Satuan;

                        SatuanListParam.push({
                            Kode_Satuan: Key,
                            Nama_Satuan: Value
                        });
                    }
                }

            }
            inputSatuan.setDataSource(SatuanListParam);

            inputSatuan.refresh();
            inputSatuan.enable(true);

            var indexSatuan = SatuanListParam.findIndex(function (item, i) {
                return item.Kode_Satuan === e.model.Kode_satuan;
            });
            if (indexSatuan == "-1") {
                indexSatuan = SatuanListParam.findIndex(function (item, i) {
                    return item.Kode_Satuan === e.model.Nama_Satuan;
                });
            }
            inputSatuan.select(indexSatuan);

            if (e.model.pdm != "" && e.model.pdm != undefined) {
                var qty = e.container.find("input[name=qty]").data("kendoNumericTextBox");
                qty.enable(false);

                var Nama_Barang = e.container.find("input[name=Nama_Barang]").data("kendoDropDownList");
                Nama_Barang.enable(false);

                var Nama_Satuan = e.container.find("input[name=Nama_Satuan]").data("kendoDropDownList");
                Nama_Satuan.enable(false);
            }
            //var numerictextboxharga = $("harga_num").data("kendoNumericTextBox");
            //numerictextboxharga.value(e.model.harga);
        },
        save: function (e) {
            var grid = $("#GridPO").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();
            var total;
            var totaldisc1;
            var totaldisc2;
            var totaldisc3;
            var totaldisc4;

            var temptotal;
            for (var i = 0; i <= ds.length - 1; i++) {
                if (ds[i].Bonus == true) {
                    ds[i].harga = 0;
                    ds[i].total = 0;
                    ds[i].prosen_diskon = 0;
                    ds[i].diskon2 = 0;
                    ds[i].diskon3 = 0;
                    ds[i].diskon4 = 0;

                }
                else {
                    total = (ds[i].qty * 1) * (ds[i].harga * 1);
                    totaldisc1 = total * ((ds[i].prosen_diskon * 1) / 100);
                    temptotal = total - totaldisc1;
                    totaldisc2 = temptotal * ((ds[i].diskon2 * 1) / 100);
                    temptotal = total - totaldisc1 - totaldisc2;
                    totaldisc3 = temptotal * ((ds[i].diskon3 * 1) / 100);
                    temptotal = total - totaldisc1 - totaldisc2 - totaldisc3;
                    // totaldisc4 = temptotal - (ds[i].diskon4 * 1);

                    // ds[i].total = (ds[i].qty * 1) * (ds[i].harga * 1);
                    ds[i].total = temptotal - (ds[i].diskon4 * 1);
                //ds[i].total = total-  (total * (ds[i].diskon4 * 0.01))
                }
               
            }
            POds = ds;

            $('#GridPO').kendoGrid('destroy').empty();
            bindGrid();
            ItemCalc();
        },
        cancel: function (e) {
            $('#GridPO').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBinding: function (e) {

            ItemCalc();
        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}

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

function ItemCalc() {
    if (_GridPO) {
        var requestData = _GridPO.dataSource.data();
        var value = 0;
        var SubTotal = $("#SubTotal").val();
        var ongkos = removeCommas($("#Ongkos").val());
        var PPN = removeCommas($("#PPN").val());
        var Total = $("#Total").val();
        var TotalRupiah = $("#TotalRupiah").val();
        var Kurs = removeCommas($("#Kurs").val());

        for (var i = 0; i < requestData.length; i++) {
            value += (requestData[i].total * 1);
        }
        // addCommas($('#txtAmountRequest').val());
        if (value != 0) {
            SubTotal = value;
            vSubTotal = value;
            $('#SubTotal').val(value.toLocaleString('id-ID', { minimumFractionDigits: 2 }));
        } else {
            $('#SubTotal').val(0);
        }
        if ($('#checkbox2').is(":checked")) {
            PPN = value * 0.1;
            vPPN = value * 0.1;
            //$('#PPN').val(addCommas(PPN.toString
            $('#PPN').val(PPN.toLocaleString('id-ID', { minimumFractionDigits: 2 }));
        }
        else {
            PPN = 0;
            vPPN = 0;
            $('#PPN').val(0);
        }

        Total = (SubTotal * 1) + (ongkos * 1) + (PPN * 1);
        vTotal = (SubTotal * 1) + (ongkos * 1) + (PPN * 1);
        if (Total != 0) {
            $('#Total').val(Total.toLocaleString('id-ID', { minimumFractionDigits: 2 }));
        } else {
            $('#Total').val(0);
        }

        if (!Kurs) {
            TotalRupiah = Total;
            if (TotalRupiah != 0) {
                $('#TotalRupiah').val(TotalRupiah.toLocaleString('id-ID', { minimumFractionDigits: 2 }));
            }
            else {
                $('#TotalRupiah').val(0);
            }
        } else {
            TotalRupiah = (Total * 1) * (Kurs * 1);
            vTotalRupiah = (Total * 1) * (Kurs * 1);
            if (TotalRupiah != 0) {
                $('#TotalRupiah').val(TotalRupiah.toLocaleString('id-ID', { minimumFractionDigits: 2 }));
            }
            else {
                $('#TotalRupiah').val(0);
            }
        }

        if (!ongkos) {
            $('#Ongkos').val(0);
        }
        else if (ongkos == 0) {
            $('#Ongkos').val(0);
        }
        else {
            document.getElementById('Ongkos').value = addCommas($('#Ongkos').val());
        }

        if (!Kurs) {
            $('#Kurs').val(1);

        }
        else if (Kurs == 0) {
            $('#Kurs').val(1);
        }
        else {
            document.getElementById('Kurs').value = addCommas($('#Kurs').val());
        }

    } else {
        $('#SubTotal').val(0);
        $('#TotalRupiah').val(0);
        $('#Total').val(0);
        $("#PPN").val(0);
    }
}

function removeCommas(str) {
    return str.replace(/,/g, '');
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function calcRequest(val) {
    var totRequest = 0;
    if (_gridReq !== undefined) {
        var requestData = _gridReq.dataSource.data();
        var reqAmt = 0;
        for (var i = 0; i < requestData.length; i++) {
            if (requestData[i].totalRequest === null) {
                reqAmt = 0;
                requestData[i].totalRequest = 0;
            }
            else {
                reqAmt = requestData[i].totalRequest;
            }
            totRequest = totRequest + (reqAmt * 1);
        }
    }
    return totRequest;
}

function onAddNewRow() {

    startSpinner('Loading..', 1);

    var grid = $("#GridPO").data("kendoGrid");
    grid.addRow();
    $('#Kode_Barang').data("kendoDropDownList").open();
    startSpinner('Loading..', 0);
}
var SatuanListParam = [];

function barangDropDownEditor(container, options) {
    var input = $('<input required id="Kode_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "Nama_Barang",
        dataValueField: "Nama_Barang",
        dataSource: BarangList,
        optionLabel: "Select Barang...",
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
           // barang.set("keterangan", barang.Nama_Barang);

            SatuanListParam = [];
            var found = GetSatuanCode(id);
            barang.last_price = found[0].last_price;
            if (found[0].last_price <= 0) {
                $("#lastprice").text(0);
            } else {
                $("#lastprice").text(addCommas(found[0].last_price.toString()));
            }
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
            inputSatuan.select(1);
            //  $("#Kode_Satuan").data('kendoDropDownList').value(SatuanListParam[0].Kode_Satuan);
            $("#Kode_Satuan").data('kendoDropDownList').value(Key);

            var satuan = _GridPO.dataItem($(e.sender.element).closest("tr"));
            //satuan.kd_satuan = SatuanListParam[0].Kode_Satuan;
            //satuan.satuan = SatuanListParam[0].Nama_Satuan;
            //satuan.Kode_satuan = SatuanListParam[0].Kode_Satuan;
            //satuan.Kode_Satuan = SatuanListParam[0].Kode_Satuan;
            //satuan.Nama_Satuan = SatuanListParam[0].Nama_Satuan;
            satuan.kd_satuan = Key;
            satuan.satuan = Value;
            satuan.Kode_satuan = Key;
            satuan.Kode_Satuan = Key;
            satuan.Nama_Satuan = Value;

            //Kode_Satuan
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
    return BarangList.filter(
        function (BarangList) { return BarangList.Kode_Barang === code; }
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

function opencustomerModal() {
    $('#NamaCust').val("");
    $('#AlamatCust').val("");
    $('#KotaCust').val("");
    $('#TelpCust').val("");
}

function tambahCustomer() {
    validationCustomer();
}

function validationCustomer() {
    var NamaCust = $('#NamaCust').val();
    var AlamatCust = $('#AlamatCust').val();
    var KotaCust = $('#KotaCust').val();
    var TelpCust = $('#TelpCust').val();

    validationMessage = '';
    if (!NamaCust) {
        validationMessage = validationMessage + 'Nama tidak boleh kosong.' + '\n';
    }
    if (!AlamatCust) {
        validationMessage = validationMessage + 'Alamat tidak boleh kosong.' + '\n';
    }
    if (!KotaCust) {
        validationMessage = validationMessage + 'Kota tidak boleh kosong.' + '\n';
    }
    if (!TelpCust) {
        validationMessage = validationMessage + 'Telp tidak boleh kosong.' + '\n';
    }


    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        SaveDataCustomer();
    }
}

function SaveDataCustomer() {
    var savedata = {
        Nama_Customer: $('#NamaCust').val(),
        Alamat1: $('#AlamatCust').val(),
        Kota1: $('#KotaCust').val(),
        No_Telepon1: $('#TelpCust').val()
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
                url: urlSaveCustomer,
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
                            html: "Save Successfully"
                        });
                        $.when(fillCboSupplier()).done(function () {
                            ///fillCboSupplier();
                            $("#customerModal").modal('hide');
                            startSpinner('loading..', 0);
                        });
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

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    var Supplier = $('#kd_supplier').val();
    var Keterangan = $('#Keterangan').val();
    var Lama = $('#Lama').val();

    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';
    if (!Supplier) {
        validationMessage = validationMessage + 'Supplier harus di pilih.' + '<br/>';
    }
    if (!Keterangan) {
        validationMessage = validationMessage + 'Alamat kirim harus di isi.' + '<br/>';
    }
    if (!Lama) {
        validationMessage = validationMessage + 'Lama Bayar harus di isi.' + '<br/>';
    }
    else {
        if ((Lama * 1) <= 0) {
            validationMessage = validationMessage + 'Lama Bayar harus di isi.' + '<br/>';
        }
    }
    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Tambahkan Item.' + '<br/>';
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
    $("#form1").validate();
    if (!$('#form1').valid()) {
        return false;
    }

    var ppnval;
    if ($('#checkbox2').is(":checked")) {
        ppnval = "Y";
    }
    else {
        ppnval = "T";
    }
    var no = "";
    if (Mode != "NEW") {
        no = $('#PONumber').val();
        Mode = "NEW";
    }
    var savedata = {
        no_po: no,
        tgl_po: $('#tanggal').val(),
        no_ref: $('#RefNo').val(),
        kd_supplier: $('#kd_supplier').val(),
        kd_valuta: $('#Valuta').val(),
        kurs_valuta: $('#Kurs').val(),
        tgl_kirim: $('#tanggalkirim').val(),
       // tgl_jth_tempo: $('#tglBayar').val(),
        qty_total: 0,
        //jml_val_trans: $('#Total').val(),
        //jml_rp_trans: $('#TotalRupiah').val(),
        jml_val_trans: vTotal,
        jml_rp_trans: vTotalRupiah,
        flag_ppn: ppnval,
        jml_ppn: vPPN,
        lama_bayar: $('#Lama').val(),
        ongkir: $('#Ongkos').val(),
        keterangan: $('#Keterangan').val(),
        term_bayar: $('#term_bayar').val(),
        atas_nama: $('#jenisPO').val(),
        kop_surat: $('#kop_surat').val(),
        podetail: _GridPO.dataSource.data().toJSON()
    };
    // console.log('savedata: ' + savedata);
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

                        //$.when(getDataPO(result.result)).done(function () {
                        //    Swal.fire({
                        //        type: 'success',
                        //        title: 'Success',
                        //        html: "Save Successfully"
                        //    });
                        //});
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
function fillForm() {
    if (Mode == "VIEW") {
        $('#tanggalBayar, #divtanggal, #divtanggalkirim').datepicker('remove');
        $("#tgl_kirimdesc").attr("disabled", "disabled");
        $('#tanggal').attr("disabled", "disabled");
        $('#tglBayar').attr("disabled", "disabled");
        $('#kd_supplier').attr("disabled", "disabled");
        $('#Valuta').attr("disabled", "disabled");
        $('#kurs_valuta').attr("disabled", "disabled");
        $('#keterangan').attr("disabled", "disabled");
        $('#addKota').hide();
        $('#tanggalkirim').attr("disabled", "disabled");
        $('#term_bayar').attr("disabled", "disabled");
        $('#Ongkos').attr("disabled", "disabled");
        $('#checkbox2').attr("disabled", "disabled");
        $('#Ongkos').attr("disabled", "disabled");
        $('#RefNo').attr("disabled", "disabled");
        $('#Kurs').attr("disabled", "disabled");
        $('#Keterangan').attr("disabled", "disabled");
    }

    if (Mode == "EDIT" || Mode == "VIEW") {
        if (POds.length > 0) {
            $("#PONumber").val(POds[0].no_po);
            $("#tanggal").val(POds[0].tgl_podesc);
            $("#tanggalkirim").val(POds[0].tgl_kirimdesc);
            $("#RefNo").val(POds[0].no_ref);

            $("#Valuta option[value='" + POds[0].kd_valuta + "']").attr("selected", "selected");
            $('#Valuta').selectpicker('refresh');
            $('#Valuta').selectpicker('render');
            $("#kd_supplier option[value='" + POds[0].kd_supplier + "']").attr("selected", "selected");
            $('#kd_supplier').selectpicker('refresh');
            $('#kd_supplier').selectpicker('render');
            $("#Kurs").val(POds[0].kurs_valuta);
            $("#kop_surat").val(POds[0].kop_surat);
            $("#atas_nama").val(POds[0].atas_nama);
            $("#Keterangan").val(POds[0].keterangan);
            $("#Status").val(POds[0].rec_stat);
            $("#tglBayar").val(POds[0].tgl_jth_tempodesc);
            $("#Lama").val(POds[0].lama_bayar);
            $("#term_bayar").val(POds[0].term_bayar);
            $("#SubTotal").val(POds[0].jml_rp_trans);
            $("#Ongkos").val(POds[0].ongkir);
            $("#PPN").val(POds[0].jml_ppn);
            $("#Total").val(POds[0].jml_val_trans);
            $("#TotalRupiah").val(POds[0].total);
            $("#ket_batal").val(POds[0].ket_batal);
            if (POds[0].rec_stat == "REJECT" || POds[0].rec_stat == "REVISE") {
                $("#ketreject").show();
            }


            document.getElementById('TotalRupiah').value = addCommas($('#TotalRupiah').val());
            document.getElementById('Total').value = addCommas($('#Total').val());
            document.getElementById('PPN').value = addCommas($('#PPN').val());
            document.getElementById('Ongkos').value = addCommas($('#Ongkos').val());
            document.getElementById('SubTotal').value = addCommas($('#SubTotal').val());
            document.getElementById('Kurs').value = addCommas($('#Kurs').val());

            if (POds[0].flag_ppn == "Y") {
                $('#checkbox2').attr('checked', 'checked');
            }

        }
    }
}

function showCreate() {
    window.location.href = urlCreate;
}
function showlist() {
    window.location.href = urlList;
}

function getDataPO(po) {
    var urlLink = urlGetData;
    var filterdata = {
        no_po: po,
        DateFrom: "",
        DateTo: "",
        status_po: ""
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            POds = result;
            if (Mode == "NEW") {
                Mode = "VIEW";
            }
            fillForm();
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {

                    POds = [];
                    for (var i = 0; i <= result.length - 1; i++) {
                        var flagbonus;
                        if (result[i].bonus == "true") {
                            Bonus = true;
                        }
                        else {
                            Bonus = false;

                        }
                        POds.push({
                            Kode_Barang: result[i].kd_stok,
                            kd_stok: result[i].kd_stok,
                            Nama_Barang: result[i].nama_barang,
                            nama_Barang: result[i].nama_barang,
                            Kode_satuan: result[i].kd_satuan,
                            kd_satuan: result[i].kd_satuan,
                            Nama_Satuan: result[i].satuan,
                            satuan: result[i].satuan,
                            qty: result[i].qty,
                            harga: result[i].harga,
                            prosen_diskon: result[i].prosen_diskon,
                            diskon2: result[i].diskon2,
                            diskon3: result[i].diskon3,
                            diskon4: result[i].diskon4,
                            total: result[i].total,
                            keterangan: result[i].keterangan,
                            last_price: result[i].last_price,
                            Bonus: result[i].bonus
                        });
                    }
                    console.log(JSON.stringify(result));
                    //$('#GridPO').kendoGrid('destroy').empty();
                    if (Mode == "VIEW") {
                       // alert('a');
                        columngrid = [
                            { field: "Action", title: "Last Invoice", width: "80px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=kd_stok#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>" },

                            { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                            { field: "satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                            { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                            { field: "last_price", title: "Last Price", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: lastpriceLabel },
                            { field: "harga", title: "Harga", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: hargaNumeric },
                            { field: "prosen_diskon", title: "Disc %1", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon2", title: "Disc %2", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon3", title: "Disc %3", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon4", title: "Disc Rp.", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            {
                                field: "Bonus", title: "Bonus", width: "50px", editor: customBoolEditor,
                                template: '#=dirtyField(data,"Bonus")#<center><input disabled type="checkbox" #= Bonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
                            },
                            { field: "total", title: "Total", width: "110px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                            { field: "keterangan", title: "Keterangan", width: "180px" },
                        ];
                    }
                    else if (Mode == "EDIT") {
                        columngrid = [
                            { field: "Action", title: "Last Invoice", width: "80px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=kd_stok#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>" },

                            { field: "Nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                            { field: "Nama_Satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                            { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                            { field: "last_price", title: "Last Price", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: lastpriceLabel},
                            { field: "harga", title: "Harga", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, editor: hargaNumeric},
                            { field: "prosen_diskon", title: "Disc %1", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon2", title: "Disc %2", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon3", title: "Disc %3", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            { field: "diskon4", title: "Disc Rp.", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                            {
                                field: "Bonus", title: "Bonus", width: "50px", editor: customBoolEditor,
                                template: '#=dirtyField(data,"Bonus")#<center><input disabled type="checkbox" #= Bonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
                            },
                            { field: "total", title: "Total", width: "110px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                            { field: "keterangan", title: "Keterangan", width: "180px" },
                            { command: ["edit", "destroy"], title: "Actions", width: "110px" }
                        ];
                    }
                    bindGrid();

                    ItemCalc();

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

function onPrintClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + idPO;
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

function onAddNewDPM() {
    startSpinner('loading..', 2);

    $.ajax({
        url: urlPORequest + "?status=Approve",
        type: "GET",
        success: function (result) {
            dpmds = result;
            if (_GridDPM != undefined) {
                $("#DPMGrid").kendoGrid('destroy').empty();
            }
            bindGridDPM();
            startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function bindGridDPM() {
    _GridDPM = $("#DPMGrid").kendoGrid({
        columns: [
            { selectable: true, width: "30px", headerAttributes: { style: "text-align: left;" } },
            { field: "cabang", title: "Cabang", width: "130px" },
            { field: "nama_Barang", title: "Nama Barang", width: "180px" },
            { field: "satuan", title: "Satuan", width: "80px" },
            { field: "tgl_Diperlukan", title: "Tanggal Diperlukan", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_Diperlukan, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "qty_PR", title: "Qty Request", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", width: "180px" }
        ],
        dataSource: {
            data: dpmds,
            schema: {
                model: {
                    id: "no_DPM",
                    fields: {
                        no_DPM: { type: "string" },
                        kd_Stok: { type: "string" },
                        satuan: { type: "string" },
                        qty_PR: { type: "number" },
                        tgl_Diperlukan: { type: "date" },
                        keterangan: { type: "string" },
                        nama_Barang: { type: "string" },
                        cabang: { type: "string" }
                    }
                }
            }
        },
        noRecords: true,
        change: onChange,
    }).data("kendoGrid");
}

function tambahDPM() {
    if (!selectRequest) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: 'Please select request'
        });
    }
    else {
        swal({
            type: 'warning',
            title: 'Are you sure?',
            html: 'You want to submit this data',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            selecteddpmds = [];
            acceptdpmds = [];
            if (isConfirm.value === true) {
                var array = selectRequest.split(";");
                for (var i = 0; i < dpmds.length; i++) {
                    for (var j = 0; j < array.length; j++) {
                        if (array[j] === dpmds[i].no_DPM) {
                            selecteddpmds.push(
                                {
                                    "no_DPM": dpmds[i].no_DPM,
                                    "kd_Stok": dpmds[i].kd_Stok,
                                    "satuan": dpmds[i].satuan,
                                    "qty": dpmds[i].qty_PR,
                                    "nama_Barang": dpmds[i].nama_Barang,
                                    "last_price": dpmds[i].last_price

                                }
                            );
                        }
                    }
                }

                var groupbyBarang = Object.values(selecteddpmds.reduce((a, { kd_Stok, nama_Barang, no_DPM, satuan, qty, last_price }) => {
                    if (!a[kd_Stok]) a[kd_Stok] = { kd_Stok, nama_Barang, no_DPM: [], satuan: [], qty: [], last_price: [] };
                    a[kd_Stok].no_DPM.push(no_DPM);
                    a[kd_Stok].satuan.push(satuan);
                    a[kd_Stok].qty.push(qty);
                    a[kd_Stok].last_price.push(last_price);
                    return a;
                }, {}));

                console.log(groupbyBarang);
                for (var x = 0; x < groupbyBarang.length; x++) {
                    var qtyBarang = 0;
                    var PDMBarang = "";
                    for (var b = 0; b < groupbyBarang[x].no_DPM.length; b++) {
                        qtyBarang += groupbyBarang[x].qty[b];
                        PDMBarang += groupbyBarang[x].no_DPM[b] + ";";
                    }
                    POds.push({
                        kd_Stok: groupbyBarang[x].kd_Stok,
                        Kode_Barang: groupbyBarang[x].kd_Stok,
                        Nama_Barang: groupbyBarang[x].nama_Barang,
                        nama_Barang: groupbyBarang[x].nama_Barang,
                        Kode_satuan: groupbyBarang[x].satuan[0],
                        kd_satuan: groupbyBarang[x].satuan[0],
                        Nama_Satuan: groupbyBarang[x].satuan[0],
                        satuan: groupbyBarang[x].satuan[0],
                        qty: qtyBarang,
                        harga: 0,
                        prosen_diskon: 0,
                        diskon2: 0,
                        diskon3: 0,
                        diskon4: 0,
                        total: 0,
                        keterangan: "",
                        pdm: PDMBarang,
                        last_price: groupbyBarang[x].last_price[0]
                    });
                }
                $('#GridPO').kendoGrid('destroy').empty();
                bindGrid();
                $("#requestModal").modal('hide');
            }
        });

    }
}

function onChange(arg) {
    selectRequest = this.selectedKeyNames().join(";");
}