var _GridStokOpname;
var Opnameds = [];
var SatuanListParam = [];
var RekGlList = [];
var RekeningList = [];
var BukuPusatList = [];
var saldods = [];
var pods = [];
var columnOpname = [];
var rekbank = [];
var saldodebet1 = 0;
var saldokredit1 = 0;

var customerds = [];



var editval = true;
$(document).ready(function () {
    startSpinner('Loading..', 1);
    bindGrid();
    
    JenisChanged1();

  

    if (idOpname != null && idOpname != "") {
        $.when(getCustomer()).done(function () {
            $.when(fillCboCustomer()).done(function () {
                $.when(getRekBank()).done(function () {
                    $.when(getRekKas()).done(function () {
                        $.when(getRekGl()).done(function () {
                            $.when(getBukuPusat()).done(function () {
                                $.when(getData(idOpname)).done(function () {
                                    startSpinner('loading..', 0);
                                });
                            });
                        });
                    });
                });
            });
        });
    } else {
        $.when(getCustomer()).done(function () {
            $.when(fillCboCustomer()).done(function () {
                $.when(getRekBank()).done(function () {
                    $.when(getRekKas()).done(function () {
                        $.when(getRekGl()).done(function () {
                            $.when(getBukuPusat()).done(function () {
                                //$.when(getData(idOpname)).done(function () {
                                    startSpinner('loading..', 0);
                               // });
                            });
                        });
                    });
                });
            });
        });
    }


    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
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

function JenisChanged() {

    var valjenis = $("#Jenis option:selected").val();
    //penerimaan
    if (valjenis.indexOf("JKM") >= 0) {
        columnOpname = [
            { field: "no_jur", title: "Nomer", width: "100px", hidden: true },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown },
            { field: "nm_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown },
            { field: "kd_buku_besar", title: "Rekening", width: "120px", hidden: true },
            { field: "kd_buku_pusat", title: "Pusat Biaya", width: "120px", hidden: true },
            { field: "kartu", title: "Rekening", width: "120px", hidden: true },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "saldo_rp_debet", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_debet", title: "Val Debet", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation, hidden: true },
            { field: "saldo_rp_kredit", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_kredit", title: "Val Kredit", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation1, hidden: false },
            { command: ["destroy"], title: "Actions", width: "40px" }
        ];
    } else {
        columnOpname = [
            { field: "no_jur", title: "Nomer", width: "100px", hidden: true },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown },
            { field: "nm_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown },
            { field: "kd_buku_besar", title: "Rekening", width: "120px", hidden: true },
            { field: "kd_buku_pusat", title: "Pusat Biaya", width: "120px", hidden: true },
            { field: "kartu", title: "Rekening", width: "120px", hidden: true },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "saldo_rp_debet", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_debet", title: "Val Debet", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation, hidden: false },
            { field: "saldo_rp_kredit", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_kredit", title: "Val Kredit", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation1, hidden: true },
            { command: ["destroy"], title: "Actions", width: "40px" }
        ];
    }

}

function JenisChanged1() {
    var valjenis = $("#Jenis option:selected").val();
    var textjenis = $("#Jenis option:selected").text();

   
    if (textjenis.indexOf("Kas") >= 0) {
        getRekKas();
    } else {
        getRekBank();
    }

    //penerimaan
    if (valjenis.indexOf("JKM") >= 0) {
        columnOpname = [
            { field: "no_jur", title: "Nomer", width: "100px", hidden: true },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown },
            { field: "nm_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown },
            { field: "kd_buku_besar", title: "Rekening", width: "120px", hidden: true },
            { field: "kd_buku_pusat", title: "Pusat Biaya", width: "120px", hidden: true },
            { field: "kartu", title: "Rekening", width: "120px", hidden: true},
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "saldo_rp_debet", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_debet", title: "Val Debet", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation, hidden: true },
            { field: "saldo_rp_kredit", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_kredit", title: "Val Kredit", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation1, hidden: false },
            { command: ["destroy"], title: "Actions", width: "40px" }
        ];
    } else {
        columnOpname = [
            { field: "no_jur", title: "Nomer", width: "100px", hidden: true },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown },
            { field: "nm_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown },
            { field: "kd_buku_besar", title: "Rekening", width: "120px", hidden: true },
            { field: "kd_buku_pusat", title: "Pusat Biaya", width: "120px", hidden: true },
            { field: "kartu", title: "Rekening", width: "120px", hidden: true },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "saldo_rp_debet", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_debet", title: "Val Debet", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation, hidden: false },
            { field: "saldo_rp_kredit", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_kredit", title: "Val Kredit", width: "120px", format: "{0:n2}", decimals: 2, min: 0, attributes: { class: "text-right ", 'style': 'background-color: aquamarine; color:black;' }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>", editor: valueValidation1, hidden: true },
            { command: ["destroy"], title: "Actions", width: "40px" }
        ];
    }
    $('#GridStokOpname').kendoGrid('destroy').empty();
    Opnameds = [];
    bindGrid();
    
}

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
        no_jur: id
    };
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Opnameds = [];
            // //console.log(result);
           // Opnameds = result;
           // //console.log(JSON.stringify(Opnameds));

            if (result.length > 0) {
                $("#NoTransaksi").val(id);   
                $("#Keterangan").val(result[0].keterangan);
               
                $("#NoReferensi").val(result[0].no_ref1);
                $("#Alamat").val(result[0].alamat);
         
                $('#Keterangan').attr("disabled", "disabled");
               
                $("#NoReferensi").attr("disabled", "disabled");
                $("#Alamat").attr("disabled", "disabled");
                

                $("#Jenis option[value='" + result[0].tipe_trans + "']").attr("selected", "selected");
                $('#Jenis').selectpicker('refresh');
                $('#Jenis').selectpicker('render');
                $('#Jenis').attr("disabled", "disabled");

                $("#Kepada option[value='" + result[0].kd_kartu + "']").attr("selected", "selected");
                $('#Kepada').selectpicker('refresh');
                $('#Kepada').selectpicker('render');
                $('#Kepada').attr("disabled", "disabled");

                $("#Rekening option[value='" + result[0].detail[0].kd_buku_besar + "']").attr("selected", "selected");
                $('#Rekening').selectpicker('refresh');
                $('#Rekening').selectpicker('render');
                $('#Rekening').attr("disabled", "disabled");

              

                //var strjenis = $("#Jenis").val();
                JenisChanged();

               
                
                editval = false;

                $("#addNew").hide();
                $("#save").hide();
                $("#new").show();

                if (_GridStokOpname != undefined) {
                    $("#GridStokOpname").kendoGrid('destroy').empty();
                }
                if (id != null && id != "") {
                    for (var i = 0; i <= result[0].detail.length - 1; i++) {
                        var rslbb = RekGlList.filter(p => p.kd_buku_besar == result[0].detail[i].kartu);
                        var rslbp = BukuPusatList.filter(p => p.kd_buku_pusat == result[0].detail[i].kd_buku_pusat);

                        Opnameds.push({

                            kd_buku_besar: result[0].detail[i].kd_buku_besar,
                            nm_buku_besar: rslbb[0].nm_buku_besar,
                            kd_buku_pusat: result[0].detail[i].kd_buku_pusat,
                            nm_buku_pusat: rslbp[0].nm_buku_pusat,
                            Nm_Buku_Pusat: result[0].detail[i].Nm_Buku_Pusat,
                            kartu: result[0].detail[i].kartu,
                            saldo_rp_debet: result[0].detail[i].saldo_rp_debet,
                            saldo_val_debet: result[0].detail[i].saldo_val_debet,
                            saldo_rp_kredit: result[0].detail[i].saldo_rp_kredit,
                            saldo_val_kredit: result[0].detail[i].saldo_val_kredit,
                            keterangan: result[0].detail[i].keterangan

                        });

                    }
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
function removeCommas(str) {
    return str.replace(/,/g, '');
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function getCustomer() {
    return $.ajax({
        url: urlGetCustomer,
        success: function (result) {
            customerds = [];
            customerds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getRekGl() {
    return $.ajax({
        url: urlRekGl,
        type: "POST",
        success: function (result) {
            RekGlList = result;
            //console.log('rgl:' + JSON.stringify(RekGlList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillCboCustomer() {
    $("#Kepada").empty();
    $("#Kepada").append('<option value="" selected disabled>Please select</option>');
    var data = customerds;

    for (var i = 0; i < data.length; i++) {
        $("#Kepada").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kepada').selectpicker('refresh');
    $('#Kepada').selectpicker('render');
}

function onCustomerChanged() {
    var customer = $("#Kepada").val();

    //var found = FindCustomer(customer);

    //var customer = $("#AlamatKirim").val(found[0].alamat1);
    //startSpinner('Loading..', 1);
    //getPiutangCustomer();

}

function getBukuPusat() {
    //var filterdata = {
    //    kd_buku_besar : code
    //};
    return $.ajax({
        url: urlBukuPusat,
        type: "POST",
        //data: filterdata,
        success: function (result) {
            BukuPusatList = result;
            //console.log(JSON.stringify(BukuPusatList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getRekBank() {
    return $.ajax({
        url: urlRekeningBank,
        type: "POST",
        success: function (result) {
            $("#Rekening").empty();
            $("#Rekening").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            RekeningList = result;
            //console.log(JSON.stringify(RekeningList));
            for (var i = 0; i < data.length; i++) {
                $("#Rekening").append('<option value="' + data[i].kd_bank + '">' + data[i].nama_bank + '</option>');
            }
            $('#Rekening').selectpicker('refresh');
            $('#Rekening').selectpicker('render');

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getRekKas() {
    return $.ajax({
        url: urlRekeningKas,
        type: "POST",
        success: function (result) {
            $("#Rekening").empty();
            $("#Rekening").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            RekeningList = result;
            //console.log(JSON.stringify(RekeningList));
            for (var i = 0; i < data.length; i++) {
                $("#Rekening").append('<option value="' + data[i].kd_buku_besar + '">' + data[i].nm_buku_besar + '</option>');
            }
            $('#Rekening').selectpicker('refresh');
            $('#Rekening').selectpicker('render');

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
                        kd_buku_besar: { type: "string", editable: false },
                        kartu: { type: "string", editable: false },
                        nm_buku_besar: { type: "string" },
                        kd_buku_pusat: { type: "string", editable: false },
                        nm_buku_pusat: { type: "string" },
                        keterangan: { type: "string" },
                        saldo_rp_debet: { type: "decimal", editable: false, defaultValue: 0 },
                        saldo_val_debet: { type: "decimal", defaultValue: 0 },
                        saldo_rp_kredit: { type: "decimal", editable: false, defaultValue: 0 },
                        saldo_val_kredit: { type: "decimal", defaultValue: 0 }
                    }
                }
            },
            aggregate: [
                { field: "saldo_val_debet", aggregate: "sum" },
                { field: "saldo_val_kredit", aggregate: "sum" }
            ]
        },
        cancel: function (e) {
            $('#GridStokOpname').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBound: function (e) {
            var gridData = $("#GridStokOpname").data('kendoGrid').dataSource.view();
            let total_price = 0;


            gridData.forEach(element => {
              
                if (parseInt(element.saldo_val_debet) > 0) {
                    //console.log(parseInt(element.saldo_val_debet));
                    total_price = total_price + element.saldo_val_debet;
                } else if (parseInt(element.saldo_val_kredit) > 0) {
                    total_price = total_price + element.saldo_val_kredit;
                } else {
                    total_price = total_price + 0;
                }

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
                        kd_buku_besar: ds[i].kd_buku_besar,
                        nm_buku_besar: ds[i].nm_buku_besar,
                        kd_buku_pusat: ds[i].kd_buku_pusat,
                        kartu: ds[i].kartu,
                        nm_buku_pusat: ds[i].nm_buku_pusat,
                        keterangan: ds[i].keterangan,
                        saldo_rp_debet: ds[i].saldo_rp_debet,
                        saldo_val_debet: ds[i].saldo_val_debet,
                        saldo_rp_kredit: ds[i].saldo_rp_kredit,
                        saldo_val_kredit: ds[i].saldo_val_kredit
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

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
        max: 9999999999999999999999999999999.99,
        min: 0.1
    });
}

function valueValidation1(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 2,
        max: 999999999999999999999999999999.99,
        min: 0.1
    });
}



function RekGlDropDown(container, options) {
   
    var input = $('<input required id="kd_buku_besar" name="nm_buku_besar">');
    input.appendTo(container);


    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nm_buku_besar",
        dataValueField: "nm_buku_besar",
        dataSource: RekGlList,
        optionLabel: "Pilih Rekening",
        filter: "contains",
        template: "<span data-id='${data.kd_buku_besar}' data-rekgl='${data.nm_buku_besar}'>${data.nm_buku_besar}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Rekgldp = e.item.find("span").attr("data-rekgl");
            var rekgldp = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            rekgldp.kartu = id;
            rekgldp.nm_buku_besar = Rekgldp;
            rekgldp.kd_buku_besar = id;
            rekgldp.Nm_Buku_Besar = Rekgldp;
            rekgldp.NM_BUKU_BESAR = Rekgldp;

            //getBukuPusat(id);

        }
    }).appendTo(container);
    
}

function BukuPusatDropDown(container, options) {
    var input = $('<input required id="kd_buku_pusat" name="nm_buku_pusat">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nm_buku_pusat",
        dataValueField: "nm_buku_pusat",
        dataSource: BukuPusatList,
        optionLabel: "Pilih Buku Pusat",
        filter: "contains",
        template: "<span data-id='${data.kd_buku_pusat}' data-bukupusat='${data.nm_buku_pusat}'>${data.nm_buku_pusat}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var BukuPusatdp = e.item.find("span").attr("data-bukupusat");
            var bukupusatdp = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            bukupusatdp.kd_buku_pusat = id;
            bukupusatdp.nm_buku_pusat = BukuPusatdp;
            bukupusatdp.kd_buku_pusat = id;
            bukupusatdp.Nm_Buku_Pusat = BukuPusatdp;
            bukupusatdp.NM_BUKU_PUSAT = BukuPusatdp;

        }
    }).appendTo(container);
   
}

function getKartu() {
    return $.ajax({
        url: urlRekeningBank,
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
            barang.harga = found[0].harga;

        }
    }).appendTo(container);
}
function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

//function gudangDropDownEditor(container, options) {
//    var input = $('<input required id="kode_Gudang" name="nama_Gudang">');
//    input.appendTo(container);

//    input.kendoDropDownList({
//        valuePrimitive: true,
//        dataTextField: "nama_Gudang",
//        dataValueField: "nama_Gudang",
//        dataSource: GudangList,
//        filter: "contains",
//        template: "<span data-id='${data.kode_Gudang}' data-Barang='${data.nama_Gudang}'>${data.nama_Gudang}</span>",
//        select: function (e) {
//            var id = e.item.find("span").attr("data-id");
//            var Barang = e.item.find("span").attr("data-Barang");
//            var gudang = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
//            gudang.gudang_tujuan = id;

//        }
//    }).appendTo(container);
//}



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
    var gudang = $('#Rekening').val();
    //var penerima = $('#penerima').val();
    var ItemData = _GridStokOpname.dataSource.data();
    validationMessage = '';
    if (!gudang) {
        validationMessage = validationMessage + 'Rekening harus di pilih.' + '\n';
    }
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

function savedata() {
    var transno = "";
 
    if (Mode != "NEW") {
        transno = $('#NoTransaksi').val();
        Mode = "NEW";
    }
    var savedata = {

        no_referensi: $('NoReferensi').val(),
        rek_attribute1: $('#Jenis').val(),
        rek_attribute2: $('#Rekening').val(),
        keterangan: $('#Keterangan').val(),
        nama: $("#Kepada option:selected").text(),
        kd_kartu: $('#Kepada').val(),
        alamat: $('#Alamat').val(),
        no_ref1: $('#NoReferensi').val(),
        kd_buku_besar: $('#Rekening').val(),
        JML_RP_TRANS: removeCommas($("#TotalRupiah").val()),
        JML_VAL_TRANS: removeCommas($("#TotalRupiah").val()),
        //gudang_tujuan: $('#gudang').val(),
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
                    //console.log(urlSave);
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

function onPrintTokoClicked() {
    //console.log(JSON.stringify(Opnameds));
    var nama = $("#Kepada option:selected").text();
   
    // //console.log(JSON.stringify(pods));

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
        cmds += '---------------------------------';

        //cmds += ' ' + Opnameds[0].alamat;
        //cmds += newLine;
        //cmds += 'Telp:' + Opnameds[0].fax1 + ' WA:' + Opnameds[0].fax1;
        //cmds += newLine;
        //cmds += '---------------------------------';
        cmds += newLine;
        cmds += esc + '!' + '\x18';
        cmds += '         Kas Masuk Keluar'; //text to print
        cmds += newLine;
        cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
        cmds += 'NO TRANS   : ' + Opnameds[0].no_jur;
        cmds += newLine;
        cmds += 'TANGGAL : ' + Opnameds[0].last_Create_Date;
        cmds += newLine;
        cmds += 'No Referensi   : ' + Opnameds[0].no_ref1;
        cmds += newLine;
        cmds += 'Kepada       : ' + Opnameds[0].nama;
        cmds += newLine;
        cmds += 'Alamat: ' + Opnameds[0].alamat;
        cmds += newLine;
        cmds += 'Keterangan: ' + Opnameds[0].keterangan;
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        var totQty = 0;
        var totharga = 0;

        for (var i = 1; i <= Opnameds.length - 1; i++) {
            cmds += Opnameds[i].kartu;
            cmds += newLine;
            //var qtyIn = Opnameds[i].qty_out.toLocaleString('id-ID', { maximumFractionDigits: 2 });
            totQty += Opnameds[i].jml_trans * 1;
            //cmds += qtyIn + " " + Opnameds[i].kd_satuan;
            //var countchar = 33 - (qtyIn.length + terimadtlds[i].kd_satuan.length + 1);
            //for (var j = 0; j <= countchar - 1; j++) {
            //    cmds += " ";
            //}
            cmds += newLine;
        }
        var jmlitem = Opnameds.length - 1;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += ' Total Data: ' + jmlitem.toString();
        cmds += newLine;
        cmds += 'Tot. Transaksi   : ' + totQty.toLocaleString('id-ID', { maximumFractionDigits: 2 });
        cmds += newLine;
        cmds += '---------------------------------';
        cmds += newLine;
        cmds += 'Diperiksa';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;
        cmds += ' (      )';
        cmds += newLine;
        cmds += newLine;
        cmds += newLine;


        cpj.printerCommands = cmds;
        //Send print job to printer!
        cpj.sendToClient();
    }
}