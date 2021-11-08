var customerds = [];
var paketds = [];
var hargads = [];
var DOds = [];
var DOhdr = [];
var DOpaket = []
var kasirds = [];
var ds = [];
var dsgiro = [];
var _GridDO;
var _GridDPM;
var _GridLimit;
var _GridRetur;
var dpmds = [];
var dpmlimit = [];
var DOhdrList = [];
var auth = [];
var StokGudangdetailds = [];
var kd;
var otp = "";
var otpe = true;
var piu;
var totaltran;
var piutangds = [];
var jnsotp = "";
var checkstok = false;
var chklimit = false;
var existdiskon = false;
var totpototongan = 0;


var columnGrid = [
    { field: "nama_Barang", title: "Nama Barang", width: "60px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
    { field: "vol", title: "Berat", width: "15px", editor: beratLabel, attributes: { class: "text-right " },hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "stok", title: "Stok", width: "15px", attributes: { class: "text-right " }, editor: stokLabel },
    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "tberat", title: "Tot Berat", width: "15px", editor: tberatLabel, attributes: { class: "text-right " }, hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric },
    { field: "harga1", title: "Harga asli", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric1, hidden: true },
    { field: "otp", title: "OTP", width: "15px", hidden: true },
    { field: "diskon", title: "Diskon 1 (%)", hidden: true, width: "20px", format: "{0:#,0}", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
   
    { field: "disc1", title: "Diskon 1 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric1, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc2", title: "Diskon 2 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric2, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc3", title: "Diskon 1 (Rp)", width: "40px", format: "{0:#,0}", editor: diskonNumeric3, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc4", title: "Diskon 2 (Rp)", width: "40px", format: "{0:#,0}", editor: diskonNumeric4, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "potongan_total", title: "Diskon Total", width: "40px", hidden: true, format: "{0:#,0}", editor: diskonNumericpot, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    {
        field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
        template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
    },
    { field: "harga_rupiah2", title: "harga_rupiah2", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
      { field: "harga_rupiah3", title: "harga_rupiah3", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
    { field: "batas1", title: "batas1", width: "10px", hidden: true },
    { field: "batas2", title: "batas2", width: "10px", hidden: true },
    { field: "batas3", title: "batas3", width: "10px", hidden: true },
    // { field: "no_booked", title: "no_booked", width: "30px" },

    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
    // { command: { text: "Stok", click: showStok }, title: "Cek", width: "20px" }

];

var columnGrid1 = [
    { field: "nama_Barang", title: "Nama Barang", width: "60px", editor: barangDropDownEditor },
    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
    { field: "jns_paket", title: "Jenis", width: "15px", editor: satuanLabel,hidden: true },
    { field: "vol", title: "Berat", width: "15px", editor: beratLabel, attributes: { class: "text-right " }, hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "stok", title: "Stok", width: "15px", attributes: { class: "text-right " }, editor: stokLabel },
    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "tberat", title: "Tot Berat", width: "15px", editor: tberatLabel, attributes: { class: "text-right " }, hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric },
    { field: "harga1", title: "Harga asli", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric1, hidden: true },
    { field: "otp", title: "OTP", width: "15px", hidden: true },
    { field: "diskon", title: "Diskon 1 (%)", hidden: true, width: "20px", format: "{0:#,0}", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    
    { field: "disc1", title: "Diskon 1 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric1, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc2", title: "Diskon 2 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric2, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc3", title: "Diskon 1 (Rp)", width: "40px", format: "{0:#,0}", editor: diskonNumeric3, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "disc4", title: "Diskon 2 (Rp)", width: "40px", format: "{0:#,0}", editor: diskonNumeric4, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "potongan_total", title: "Diskon Total", width: "40px", hidden: true, format: "{0:#,0}", editor: diskonNumericpot, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
    //{
    //    field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
    //    template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
    //},
    { field: "harga_rupiah2", title: "harga_rupiah2", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
    { field: "harga_rupiah3", title: "harga_rupiah3", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
    { field: "batas1", title: "batas1", width: "10px", hidden: true },
    { field: "batas2", title: "batas2", width: "10px", hidden: true },
    { field: "batas3", title: "batas3", width: "10px", hidden: true },
    // { field: "no_booked", title: "no_booked", width: "30px" },

    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
    // { command: { text: "Stok", click: showStok }, title: "Cek", width: "20px" }

];

//var columnGrid2 = [
//    { field: "nama_Barang", title: "Nama Barang", width: "60px", editor: barangDropDownEditor },
//    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
//    { field: "jns_paket", title: "Jenis", width: "15px", editor: satuanLabel, hidden: true },
//    { field: "vol", title: "Berat", width: "15px", editor: beratLabel, attributes: { class: "text-right " }, hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
//    { field: "stok", title: "Stok", width: "15px", attributes: { class: "text-right " }, editor: stokLabel },
//    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
//    { field: "tberat", title: "Tot Berat", width: "15px", editor: tberatLabel, attributes: { class: "text-right " }, hidden: true, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
//    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric },
//    { field: "harga1", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric1, hidden: true },
//    { field: "otp", title: "OTP", width: "15px", hidden: true },
//    { field: "diskon", title: "Diskon 1 (%)", hidden: true, width: "20px", format: "{0:#,0}", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    { field: "disc1", title: "Diskon 1 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric1, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    { field: "disc2", title: "Diskon 2 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric2, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    { field: "disc3", title: "Diskon 1 (Rp)", width: "20px", format: "{0:#,0}", editor: diskonNumeric3, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    { field: "disc4", title: "Diskon 2 (Rp)", width: "20px", format: "{0:#,0}", editor: diskonNumeric4, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
//    //{
//    //    field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
//    //    template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
//    //},
//    { field: "harga_rupiah2", title: "harga_rupiah2", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
//    { field: "harga_rupiah3", title: "harga_rupiah3", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
//    { field: "batas1", title: "batas1", width: "10px", hidden: true },
//    { field: "batas2", title: "batas2", width: "10px", hidden: true },
//    { field: "batas3", title: "batas3", width: "10px", hidden: true },
//    // { field: "no_booked", title: "no_booked", width: "30px" },

//    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
//    // { command: { text: "Stok", click: showStok }, title: "Cek", width: "20px" }

//];
var optionsGrid = {
    pageSize: 10
};
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $("#addKota").hide();
   

    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
        $("#divkirim").hide();
    }

    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })

    $('#divtanggalkirim').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })
    //tglkirimserver
    $("#tanggal").val(dateserver);
    $('#divtanggal').datepicker('remove');
    $('#tanggal').attr("disabled", "disabled");

    $("#tanggalkirim").val(tglkirimserver);
    $('#divtanggalkirim').datepicker('remove');
    $('#tanggalkirim').attr("disabled", "disabled");

    $.when(getPaket()).done(function () {

        $.when(getCustomer()).done(function () {
            $.when(GetHargaBarang()).done(function () {
                $.when(getKasir()).done(function () {
                    fillCboPaket();
                    fillCboCustomer();
                    fillCboKasir();

                    if (salesID != "") {
                        $("#kasir option[value='" + salesID + "']").attr("selected", "selected");
                        $('#kasir').selectpicker('refresh');
                        $('#kasir').selectpicker('render');
                        $('#kasir').attr("disabled", "disabled");
                    }
                    if (Mode != "NEW" && Mode != "RETUR") {
                        $.when(getDataDO(idDO)).done(function () {
                            fillForm();
                            if (Mode == "VIEW") {
                                columnGrid = [
                                    { field: "nama_Barang", title: "Nama Barang", width: "100px", editor: barangDropDownEditor },
                                    { field: "satuan", title: "Satuan", width: "20px", editor: satuanLabel },
                                    { field: "vol", title: "Berat", width: "20px", hidden: true, editor: beratLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                                    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                                    { field: "tberat", title: "Tot Berat", width: "15px", hidden: true, editor: tberatLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                                    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric },
                                    { field: "harga1", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaNumeric1, hidden: true },
                                    { field: "otp", title: "OTP", width: "15px", hidden: true },
                                    { field: "diskon", title: "Diskon 1 (%)", hidden: true, width: "20px", format: "{0:#,0}", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "disc1", title: "Diskon 1 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric1, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "disc2", title: "Diskon 2 (%)", width: "20px", format: "{0:#,0}", editor: diskonNumeric2, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "disc3", title: "Diskon 1 (Rp)", width: "20px", format: "{0:#,0}", editor: diskonNumeric3, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "disc4", title: "Diskon 2 (Rp)", width: "20px", format: "{0:#,0}", editor: diskonNumeric4, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "potongan_total", title: "Diskon Total", width: "40px", hidden: true, format: "{0:#,0}", editor: diskonNumericpot, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
                                    {
                                        field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
                                        template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
                                    },

                                    //{ field: "keterangan", title: "Keterangan", width: "30px" }

                                ];
                            }
                            var paket = $("#kategori").val();
                            if (paket == "BO PAKET" ) {
                                bindGrid1();
                            } else {
                                bindGrid();
                            }
                         


                            startSpinner('loading..', 0);
                        });
                    }
                    else if (Mode == "RETUR") {
                        $.when(getDataDORetur1(idDO)).done(function () {
                            fillForm();
                            var paket = $("#kategori").val();
                            if (paket == "BO PAKET") {
                                bindGrid1();
                            } else {
                                bindGrid();
                            }
                            startSpinner('loading..', 0);
                        });

                    }
                    else {
                        onjenisDOChanged();
                        DOds = [];
                        bindGrid();
                        startSpinner('loading..', 0);
                    }

                });
            });
        });
    });

    function getKasir() {
        return $.ajax({
            url: urlGetKasir,
            success: function (result) {
                kasirds = [];
                kasirds = result;
            },
            error: function (data) {
                alert('Something Went Wrong');
                startSpinner('loading..', 0);
            }
        });
    }

    function fillCboKasir() {
        $("#kasir").empty();
        $("#kasir").append('<option value="" selected disabled>Please select</option>');
        var data = kasirds;

        for (var i = 0; i < data.length; i++) {
            $("#kasir").append('<option value="' + data[i].kode_Sales + '">' + data[i].nama_Sales + '</option>');
        }

        $('#kasir').selectpicker('refresh');
        $('#kasir').selectpicker('render');
    }


    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            //console.log(focusable.index(this) + 1);
            //if (focusable.index(this) == 20)
            //{
            //    alert("halo");
            //}
            return false;
        }
    });

    var input = document.getElementById("ongkir");
    input.addEventListener("keyup", function (event) {
        // Number 13 is the "Enter" key on the keyboard
        if (event.keyCode === 13) {

            ItemCalc();
        }
    });

    //var input1 = document.getElementById("PPN");
    //input1.addeventlistener("keyup", function (event) {
    //    //number 13 is the "enter" key on the keyboard
    //    if (event.keycode === 13) {

    //        itemcalc();
    //    }
    //});


    $("#checkbox2").change(function () {
        //if ($('#checkbox2').is(":checked")) {
        //    var sbtl =  removeCommas($("#SubTotal").val());
        //    var ppn = parseFloat((0.1 * sbtl)).toFixed(2);
        //    $("#PPN").val(ppn.toString());

        //} else {
        //    $("#PPN").val(0);
        //}
        ItemCalc();
    });
    $("#divpaket").hide();

});



function changekat() {
    DOds = [];
    
    $("#save").show();
    $("#addKota").hide();

    $("#paket").val("");

    $("#paket").hide();
    var val = $("#kategori").val();
    var valcust = $("#Kd_Customer").val();

   
    
    if (val == "" || valcust == null || valcust == "") {
        $("#save").hide();
        $("#addKota").hide();
    } else {
        $("#addKota").show();
    }

    if (val == "BOOKING ORDER") {
        checkstok = false;
    }

    if (val == "BO PAKET") {
        
        $("#divpaket").show();
        $("#paket").show();
        $("#paket").val("");
        checkstok = false;


    } else {
        $("#divpaket").hide();
    }
    if (chklimit != true) {
        if (val == "BO PAKET") {
            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
            ItemCalc();
        }
       
    } else {
        $("#save").hide();
        $("#addKota").hide();
    }
}


function getDataDO(po) {
    var urlLink = urlGetData;
    var filterdata = {
        no_po: po,
        DateFrom: "",
        DateTo: "",
        status_po: ""
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            //console.log(JSON.stringify(DOhdr));
            var total = 0;
            var total1 = 0;
            var totdiscp1 = 0;
            var totdiscp2 = 0;
            var totdiscr1 = 0;
            var totdiscr2 = 0;
            if (DOhdr.length > 0) {
                for (var i = 0; i <= DOhdr[0].detailsvm.length - 1; i++) {
                    var diskon = DOhdr[0].detailsvm[i].diskon;
                    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                        diskon = (DOhdr[0].detailsvm[i].diskon * 1) / (DOhdr[0].detailsvm[i].qty * 1);
                        total = DOhdr[0].detailsvm[i].total + DOhdr[0].detailsvm[i].diskon;
                    }
                    else {
                        total = DOhdr[0].detailsvm[i].total - (DOhdr[0].detailsvm[i].diskon * DOhdr[0].detailsvm[i].qty);
                    }

                    //check diskon 
                    totdiscp1 = DOhdr[0].detailsvm[i].disc1;
                    totdiscp2 = DOhdr[0].detailsvm[i].disc2;
                    totdiscr1 = DOhdr[0].detailsvm[i].disc3;
                    totdiscr2 = DOhdr[0].detailsvm[i].disc4;


                    if (totdiscp1 > 0) {
                        totdiscp1 = total - (totdiscp1 * (total) / 100);

                        if (totdiscp2 >= 0) {

                            if (totdiscr1 <= 0 && totdiscr2 <= 0) {
                                totdiscp2 = totdiscp1 - ((totdiscp2 * totdiscp1) / 100);
                                total1 = totdiscp2.toFixed(1);
                            } else {

                                totdiscp1 = 0;
                                totdiscp2 = 0;
                                totdiscr1 = 0;
                                totdiscr2 = 0;


                                total1 = total;

                            }
                        }
                        else if (totdiscp2 >= 0) {
                            total1 = totdiscp1;
                        }

                    } else if (totdiscp2 > 0 && totdiscp1 <= 0) {

                        totdiscp1 = 0;
                        totdiscp2 = 0;
                        totdiscr1 = 0;
                        totdiscr2 = 0;

                        total1 = total;

                    }

                    else if (totdiscr2 > 0 && totdiscr1 <= 0) {

                        totdiscp1 = 0;
                        totdiscp2 = 0;
                        totdiscr1 = 0;
                        totdiscr2 = 0;

                        total1 = total;


                    }

                    else if (totdiscr1 > 0) { // total diskon rupiah
                        totdiscr1 = total - totdiscr1;

                        if (totdiscr2 >= 0) {

                            if (totdiscp1 <= 0 && totdiscp2 <= 0) {
                                totdiscr2 = totdiscr1 - totdiscr2;
                                total1 = totdiscr2;
                            } else {

                                totdiscp1 = 0;
                                totdiscp2 = 0;
                                totdiscr1 = 0;
                                totdiscr2 = 0;

                                total1 = total;


                            }
                        } else if (totdiscp2 >= 0) {
                            total1 = totdiscr1.toFixed(1);
                        }
                    } else {
                        total1 = total;
                    }

                    DOds.push({
                        "kode_Barang": DOhdr[0].detailsvm[i].kode_Barang,
                        "nama_Barang": DOhdr[0].detailsvm[i].nama_Barang,
                        "satuan": DOhdr[0].detailsvm[i].satuan,
                        "stok": DOhdr[0].detailsvm[i].stok,
                        "qty": DOhdr[0].detailsvm[i].qty,
                        "qty_awal": DOhdr[0].detailsvm[i].qty_awal,
                        "harga": DOhdr[0].detailsvm[i].harga,
                        "harga1": DOhdr[0].detailsvm[i].harga,
                        "diskon": diskon,
                        "disc1": DOhdr[0].detailsvm[i].disc1,
                        "disc2": DOhdr[0].detailsvm[i].disc2,
                        "disc3": DOhdr[0].detailsvm[i].disc3,
                        "disc4": DOhdr[0].detailsvm[i].disc4,
                        "total": total1,
                        "keterangan": DOhdr[0].detailsvm[i].keterangan,
                        "flagbonus": DOhdr[0].detailsvm[i].flagbonus,
                        "vol": DOhdr[0].detailsvm[i].vol,
                        "tberat": (DOhdr[0].detailsvm[i].vol * DOhdr[0].detailsvm[i].qty).toFixed(2)

                    });
                }
                // console.log(JSON.stringify(DOhdr));
            }
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function getDataDORetur1(po) {
    var urlLink = urlGetDataRetur;
    var filterdata = {
        no_po: po,
        DateFrom: "",
        DateTo: "",
        status_po: ""
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            for (var i = 0; i <= DOhdr[0].detailsvm.length - 1; i++) {
                var diskon = DOhdr[0].detailsvm[i].diskon;
                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    diskon = (DOhdr[0].detailsvm[i].diskon * 1) / (DOhdr[0].detailsvm[i].qty * 1);
                }
                DOds.push({
                    "kode_Barang": DOhdr[0].detailsvm[i].kode_Barang,
                    "nama_Barang": DOhdr[0].detailsvm[i].nama_Barang,
                    "satuan": DOhdr[0].detailsvm[i].satuan,
                    "stok": DOhdr[0].detailsvm[i].stok,
                    "qty": DOhdr[0].detailsvm[i].qty,
                    "qty_awal": DOhdr[0].detailsvm[i].qty_awal,
                    "harga": DOhdr[0].detailsvm[i].harga,
                    "harga1": DOhdr[0].detailsvm[i].harga,
                    "diskon": diskon,
                    "disc1": DOhdr[0].detailsvm[i].disc1,
                    "disc2": DOhdr[0].detailsvm[i].disc2,
                    "disc3": DOhdr[0].detailsvm[i].disc3,
                    "disc4": DOhdr[0].detailsvm[i].disc4,
                    "total": DOhdr[0].detailsvm[i].total + DOhdr[0].detailsvm[i].diskon,
                    "keterangan": DOhdr[0].detailsvm[i].keterangan,
                    "flagbonus": DOhdr[0].detailsvm[i].flagbonus,
                    "vol": DOhdr[0].detailsvm[i].vol,
                    "tberat": (DOhdr[0].detailsvm[i].vol * DOhdr[0].detailsvm[i].qty).toFixed(2)
                });
            }
            //  console.log(JSON.stringify(DOhdr));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function fillForm() {
    //console.log(JSON.stringify(DOhdr));
    if (Mode == "VIEW" || Mode == "RETUR") {
        $('#addKota').hide();

        $('#divtanggal, #divtanggalkirim').datepicker('remove');
        $('#tanggal').attr("disabled", "disabled");
        $('#tanggalkirim').attr("disabled", "disabled");
        $('#RefNo1').attr("disabled", "disabled");
        $('#RefNo2').attr("disabled", "disabled");
        $('#Kd_Customer').attr("disabled", "disabled");
        $('#jt').attr("disabled", "disabled");
        $('#Keterangan').attr("disabled", "disabled");
        $('#checkbox2').attr("disabled", "disabled");
        $('#Keterangan').attr("disabled", "disabled");
        $('#kasir').attr("disabled", "disabled");
        $('#jenisDO').attr("disabled", "disabled");
        $('#kategori').attr("disabled", "disabled");
        $('#lim_piu').attr("disabled", "disabled");
        $('#paket').attr("disabled", "disabled");
        $('#dp').attr("disabled", "disabled");
    }
    else if (Mode == "EDIT") {
        $('#Kd_Customer').attr("disabled", "disabled");
        $('#addKota').hide();
    }
    else {
        $('#addKota').show();
    }

    if (DOhdr.length > 0) {
        $("#DONumber").val(DOhdr[0].no_sp);
        $("#dp").val(DOhdr[0].dp);
        $("#tanggal").val(DOhdr[0].tgl_spdesc);
        var jk = parseInt(DOhdr[0].jatuh_Tempo);
        $("#jt").val(jk);
        $("#tanggalkirim").val(DOhdr[0].tgl_Kirim_Marketingdesc);
        $("#jenisDO option[value='" + DOhdr[0].jenis_sp + "']").attr("selected", "selected");
        $('#jenisDO').selectpicker('refresh');
        $('#jenisDO').selectpicker('render');

        $("#kasir option[value='" + DOhdr[0].kd_sales + "']").attr("selected", "selected");
        $('#kasir').selectpicker('refresh');
        $('#kasir').selectpicker('render');

        
        $("#kategori option[value='" + DOhdr[0].jenis_so + "']").attr("selected", "selected");
        $('#kategori').selectpicker('refresh');
        $('#kategori').selectpicker('render');

        if (DOhdr[0].jenis_so == "BO PAKET") {
            $('#divpaket').show();
            $("#paket option[value='" + DOhdr[0].no_paket + "']").attr("selected", "selected");
            $('#paket').selectpicker('refresh');
            $('#paket').selectpicker('render');
        } else {
            $('#divpaket').hide();
        }
       

        $("#RefNo1").val(DOhdr[0].sP_REFF);
        $("#RefNo2").val(DOhdr[0].sP_REFF2);

        $("#Kd_Customer option[value='" + DOhdr[0].kd_Customer + "']").attr("selected", "selected");
        $('#Kd_Customer').selectpicker('refresh');
        $('#Kd_Customer').selectpicker('render');

        onCustomerChanged();

        $("#AlamatKirim").val(DOhdr[0].almt_pnrm);
        $("#Status").val(DOhdr[0].statuS_DO);
        $("#Keterangan").val(DOhdr[0].keterangan);

      //  $("#PPN").val(0);
        if (Mode != "RETUR" && DOhdr[0].pPn >= 0) {
            //document.getElementById('PPN').value = addCommas(DOhdr[0].ppn);
            $("#PPN").val(addCommas1(DOhdr[0].pPn));
        }

        $("#SubTotal").val(DOhdr[0].subtotal + DOhdr[0].discount - DOhdr[0].biaya);
      
        if (Mode != "RETUR" && DOhdr[0].subtotal >= 0) {
            document.getElementById('SubTotal').value = addCommas($('#SubTotal').val());
           
        }

        $("#SubTotal").val(addCommas($("#SubTotal").val()));

        $("#Diskon").val(DOhdr[0].discount);
        if (Mode != "RETUR" && DOhdr[0].subtotal >= 0) {
            document.getElementById('Diskon').value = addCommas1($('#Diskon').val());
        }
        //var torp = DOhdr[0].jmL_RP_TRANS - DOhdr[0].dp;
        //var tt = addCommas1(torp.toString());

        $("#TotalRupiah").val(DOhdr[0].jmL_RP_TRANS - DOhdr[0].dp);
       
        if (Mode != "RETUR" && DOhdr[0].jmL_RP_TRANS >= 0) {
            document.getElementById('TotalRupiah').value = addCommas1($('#TotalRupiah').val());
        }

        if (DOhdr[0].flag_Ppn == "Y") {
            $('#checkbox2').attr('checked', 'checked');
        }
        if (DOhdr[0].inc_ongkir == "Y") {
            $('#cb_ongkir').attr('checked', 'checked');
        }
        if (Mode == "RETUR") {
            var no_sp = DOhdr[0].no_sp;
            if (!no_sp.includes("R")) {
                $("#DONumber").val(DOhdr[0].no_sp + "R1");
            }
            else {
                var numret = no_sp.charAt(no_sp.length - 1);
                numret = (numret * 1) + 1;
                no_sp = no_sp.substring(0, no_sp.length - 1);
                no_sp = no_sp + numret;
                $("#DONumber").val(no_sp);
            }
            $("#RefNo1").val(DOhdr[0].no_sp);
        }
        $("#ongkir").val(DOhdr[0].biaya);
    }
    //onjenisDOChanged();
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
       
    }

});

function getPaket() {
    return $.ajax({
        url: urlGetPaket,
        success: function (result) {
            paketds = [];
            paketds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getauth() {
    var saveotp = {
        password: otp,

    };
    return $.ajax({
        data: saveotp,
        url: urlGetAuth,
        success: function (result) {
            auth = [];
            auth = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function GetHargaBarang() {
    return $.ajax({
        url: urlGetHargaBarang,
        success: function (result) {
            hargads = [];
            hargads = result;
            //console.log(hargads);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillCboPaket() {
    $("#paket").empty();
    $("#paket").append('<option value="" selected disabled>Please select</option>');
    var data = paketds;

    for (var i = 0; i < data.length; i++) {
        $("#paket").append('<option value="' + data[i].no_paket + '">' + data[i].nama_paket + '</option>');
    }

    $('#paket').selectpicker('refresh');
    $('#paket').selectpicker('render');
}

function fillCboCustomer() {
    $("#Kd_Customer").empty();
    $("#Kd_Customer").append('<option value="" selected disabled>Please select</option>');
    var data = customerds;

    for (var i = 0; i < data.length; i++) {
        $("#Kd_Customer").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kd_Customer').selectpicker('refresh');
    $('#Kd_Customer').selectpicker('render');
}

function fillCboKasir() {
    $("#kasir").empty();
    $("#kasir").append('<option value="" selected disabled>Please select</option>');
    var data = kasirds;

    for (var i = 0; i < data.length; i++) {
        $("#kasir").append('<option value="' + data[i].kode_Sales + '">' + data[i].nama_Sales + '</option>');
    }

    $('#kasir').selectpicker('refresh');
    $('#kasir').selectpicker('render');
}

function customBoolEditor(container, options) {
    var guid = kendo.guid();
    $('<center><input id="chkStatus" type="checkbox" name="flagbonus" data-type="boolean" data-bind="checked:flagbonus" onchange="onChkChanged();"></center>').appendTo(container);
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
var harga;
function onChkChanged() {

    if ($("#harga").text() != 0) {
        harga = $("#harga").text();
    }
    if ($('#chkStatus').is(":checked")) {
        $("#harga").text(0);
        var total = calcTotal();
        $("#total").text(total);
    }
    else {
        $("#harga").text(harga);
        var total = calcTotal();
        $("#total").text(total);
    }
}

function bindGrid() {
    _GridDO = $("#GridDO").kendoGrid({
        columns: columnGrid,
        dataSource: {
            data: DOds,
            schema: {
                model: {
                    fields: {
                        kode_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        satuan: { type: "string" },
                        stok: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        qty_awal: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        harga: { type: "number" },
                        harga1: { type: "number" },
                        otp: { type: "string" },
                        diskon: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        potongan_total: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc1: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc2: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc3: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc4: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        total: { type: "number" },
                        keterangan: { type: "string" },
                        flagbonus: { type: "boolean" },
                        no_booked: { type: "string" },
                        vol: { type: "number" },
                        harga_rupiah2: { type: "number" },
                        harga_rupiah3: { type: "number" },
                        batas1: { type: "number" },
                        batas2: { type: "number" },
                        batas3: { type: "number" },
                        tberat: { type: "number" },

                    }
                }
            },
            aggregate: [
                { field: "vol", aggregate: "sum" },
                { field: "total", aggregate: "sum" },
                { field: "qty", aggregate: "sum" },
                { field: "diskon", aggregate: "sum" },
                { field: "potongan_total", aggregate: "sum" },
                { field: "disc1", aggregate: "sum" },
                { field: "disc2", aggregate: "sum" },
                { field: "disc3", aggregate: "sum" },
                { field: "disc4", aggregate: "sum" },
                { field: "tberat", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            if (e.model.kode_Barang != "") {
                var found = GetBarangDetail(e.model.kode_Barang);
                var index = hargads.findIndex(function (item, i) {
                    return item.kode_Barang === e.model.kode_Barang;
                });
                dropdownlist.select(index + 1);

                $("#satuan").text(found[0].kd_Satuan);
                $("#stok").text(found[0].stok);
                $("#harga").text(e.model.harga);
                $("#harga1").text(e.model.harga1);
                $("#otp").text(e.model.otp);
                $("#berat").text(e.model.vol);
                $("#batas1").text(e.model.batas1);
                $("#batas2").text(e.model.batas2);
                $("#batas3").text(e.model.batas3);

                var numerictextbox = $("#qty").data("kendoNumericTextBox");
                numerictextbox.value(e.model.qty);

                var numerictextboxdiskon = $("#diskon").data("kendoNumericTextBox");
                numerictextboxdiskon.value(e.model.diskon);

                var numerictextboxdiskon1 = $("#diskon1").data("kendoNumericTextBox");
                numerictextboxdiskon1.value(e.model.disc1);
                var numerictextboxdiskon2 = $("#diskon2").data("kendoNumericTextBox");
                numerictextboxdiskon2.value(e.model.disc2);
                var numerictextboxdiskon3 = $("#diskon3").data("kendoNumericTextBox");
                numerictextboxdiskon3.value(e.model.disc3);
                var numerictextboxdiskon4 = $("#diskon4").data("kendoNumericTextBox");
                numerictextboxdiskon4.value(e.model.disc4);

                var numerictextboxharga = $("#harga").data("kendoNumericTextBox");
                numerictextboxharga.value(e.model.harga);

                var numerictextboxpotongan = $("#potongan_total").data("kendoNumericTextBox");
                numerictextboxpotongan.value(e.model.potongan_total);

                var numerictextboxharga1 = $("#harga1").data("kendoNumericTextBox");
                numerictextboxharga1.value(e.model.harga1);


                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    numerictextboxdiskon.readonly();
                    //numerictextboxharga.readonly();
                }

                var total = calcTotal();
                $("#total").text(total);

            }

            addCustomCssButtonCommand();

        },
        save: function (e) {
            var grid = $("#GridDO").data("kendoGrid");
            
            ds = grid.dataSource.data().toJSON();
            if (Mode == "NEW") {
                if (ds.length > 0) {

                    ////check diskon 
                    //var totdiscp1 = 0;
                    //var totdiscp2 = 0;
                    //var totdiscr1 = 0;
                    //var totdiscr2 = 0;

                    //if (e.model.disc1 >= 0) {
                    //    //totdiscp1 = (e.model.harga * e.model.qty) - (e.model.disc1 * (e.model.harga * e.model.qty)/100);
                    //    //e.model.total = totdiscp1;

                    //    if (e.model.disc2 >= 0) {

                    //        if (e.model.disc3 <= 0 && e.model.disc4 <= 0) {
                    //            totdiscp2 = totdiscp1 - ((e.model.disc2 * totdiscp1) / 100)
                    //            e.model.total = totdiscp2;
                    //        } else {
                    //            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
                    //            return;
                    //        }
                    //    }

                    //}

                    if (existdiskon == true) {
                        e.model.disc1 = 0;
                        e.model.disc2 = 0;
                        e.model.disc3 = 0;
                        e.model.disc4 = 0;
                    }


                    if (e.model.qty == 0 & e.model.flagbonus != true) {
                        $.when(validateSave()).done(function () {

                        });
                    } else {
                        $("#save").show();
                    }
                    var totrp = $('#TotalRupiah').val();
                    var gtotal = removeCommas(totrp.toString());
                    var sumtot = e.model.harga * e.model.qty;
                    var sumtot1 = sumtot;
                   
                    var limitpiut = $("#lim_piu").val();
                    if (typeof limitpiut == "undefined") {
                        limitpiut = 0;
                    }
                    var h = removeDots(limitpiut.toString());
                    var j = parseInt(h).toFixed(0);
                  
                    //h = h.replace(",", ".");
                    if (parseInt(e.model.harga) < parseInt(e.model.harga1) ) {
                        otp = "";
                        
                            $.when(ProsesOtp()).done(function () {
                                if (otp !="" && typeof otp != "undefined") {
                                    otpe = false;

                                    $.when(showOtp()).done(function () {
                                        $.when(ProsesOtp()).done(function () {
                                            if (otp != "" && typeof otp != "undefined") {
                                                otpe = false;

                                            } else {
                                                return;
                                            }
                                        });
                                    });

                                } else {
                                    return;
                                }
                            });
                       

                    }
                    else if (parseInt(sumtot1).toFixed(0) > parseInt(j))
                    {


                        otp = "";

                        $.when(ProsesOtpLimit()).done(function () {
                            return;
                        });
                    }
                    else {
                        $("#save").show();
                        $("#addKota").show();
                        chklimit = false;
                        for (var i = 0; i <= ds.length - 1; i++) {
                           
                            if (ds[i].flagbonus == true) {
                                ds[i].harga = 0;
                                ds[i].harga1 = 0;
                                ds[i].total = 0;
                            }
                            if (ds[i].otp == "gagal") {
                                ds[i].harga = ds[i].harga1;

                            } else if (ds[i].otp == "sukses") {
                                ds[i].harga = ds[i].harga;

                            }

                        }

                        DOds = ds;
                        $('#GridDO').kendoGrid('destroy').empty();
                        bindGrid();
                        ItemCalc();
                        onAddNewRow();
                    }

                }
            }
        },
        cancel: function (e) {
            $('#save').show();
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
            var grid = $("#GridDO").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();

            for (var i = 0; i <= ds.length - 1; i++) {
                if (ds[i].flagbonus == true) {
                    ds[i].harga = 0;
                    ds[i].total = 0;
                }
            }

            DOds = ds;

            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
            ItemCalc();
        },
        dataBinding: function (e) {
            var g = $("#kategori").val();
            checkstok = false;
            if (g != "BO PAKET")
            {
                if (e.action == "rebind") {
                    if (e.items.length > 0)
                    {
                        
                            for (var i = 0; i < e.items.length; i++) {
                                var found = GetBarangDetail(e.items[i].kode_Barang);
                                e.items[i].satuan = found[0].kd_Satuan;
                                e.items[i].stok = found[0].stok;
                                //e.items[i].tberat = (e.items[i].qty * e.items[i].vol).toFixed(2);
                                var qty_asli = found[0].qty
                                if (e.items[i].qty <= found[0].stok) {
                                    if (e.items[i].flagbonus != true) {
                                        //  e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * 1);
                                        if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                                            if (e.items[i].qty >= found[0].batas1 && e.items[i].qty < found[0].batas2) {
                                                //alert("harga 1")
                                                e.items[i].harga = found[0].harga_Rupiah;
                                                e.items[i].harga1 = found[0].harga_Rupiah;
                                            }
                                            else if (e.items[i].qty >= found[0].batas2 && e.items[i].qty < found[0].batas3) {
                                                //harga_rupiah3
                                                e.items[i].harga = found[0].harga_rupiah2;
                                                e.items[i].harga1 = found[0].harga_Rupiah2;
                                                //alert("harga 2")
                                            }
                                            else if (e.items[i].qty >= found[0].batas3) {
                                                //alert("harga 3")
                                                e.items[i].harga = found[0].harga_rupiah3;
                                                e.items[i].harga1 = found[0].harga_Rupiah3;
                                                //alert("harga 2")
                                            }
                                            else {
                                                e.items[i].harga = found[0].harga_Rupiah;
                                                e.items[i].harga1 = found[0].harga_Rupiah;
                                            }
                                            if (e.items[i].disc1 == 0 && e.items[i].disc2 == 0 && e.items[i].disc3 == 0 && e.items[i].disc4 == 0) {
                                                e.items[i].total = (e.items[i].harga * e.items[i].qty) + (e.items[i].diskon * e.items[i].qty);
                                            } else {
                                                e.items[i].total = e.items[i].total;
                                            }

                                        } else {
                                            e.items[i].harga = e.items[i].harga;
                                            e.items[i].harga1 = e.items[i].harga;
                                            if (e.items[i].disc1 == 0 && e.items[i].disc2 == 0 && e.items[i].disc3 == 0 && e.items[i].disc4 == 0) {
                                                e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * e.items[i].qty);
                                            } else {
                                               
                                                e.items[i].total = e.items[i].total;
                                            }
                                        }
                                    }
                                }

                                else {
                                    if (Mode == "NEW") {
                                        if (g != "BO PAKET" && g != "BOOKING ORDER") {
                                            checkstok = true;
                                        } else {
                                            checkstok = false;
                                        }

                                        //if (e.items.qty != 0 && typeof e.items.qty != 'undefined') {
                                        //   // alert("Qty Jual Melebihi Stok !!! ");

                                        //}
                                        //    e.items[i].diskon = 0;
                                        //    e.items[i].qty = 0;
                                        //    e.items[i].total = 0;
                                        //    e.items[i].harga = 0; //tberat//tberat
                                        //    e.items[i].vol = 0; //tberat//tberat
                                        //    e.items[i].tberat = 0; //tberat//tberat//diskon
                                        //    e.items[i].diskon = 0; //tberat//tberat//diskon//total
                                        //    e.items[i].total = 0; //tberat//tberat//diskon//total



                                    }
                                    //else if (Mode == "RETUR") {
                                    //    //alert(qty_asli);

                                    //}
                                }

                            }
                       
                    }

                }
                else if (e.action == "sync") {
                    ItemCalc();
                }
            }
            
        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}
function bindGrid1() {
    _GridDO = $("#GridDO").kendoGrid({
        columns: columnGrid,
        dataSource: {
            data: DOds,
            schema: {
                model: {
                    fields: {
                        kode_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        satuan: { type: "string" },
                        stok: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        qty_awal: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        harga: { type: "number" },
                        harga1: { type: "number" },
                        otp: { type: "string" },
                        diskon: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        potongan_total: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc1: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc2: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc3: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        disc4: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        total: { type: "number" },
                        keterangan: { type: "string" },
                        flagbonus: { type: "boolean" },
                        no_booked: { type: "string" },
                        vol: { type: "number" },
                        harga_rupiah2: { type: "number" },
                        harga_rupiah3: { type: "number" },
                        batas1: { type: "number" },
                        batas2: { type: "number" },
                        batas3: { type: "number" },
                        tberat: { type: "number" },

                    }
                }
            },
            aggregate: [
                { field: "vol", aggregate: "sum" },
                { field: "total", aggregate: "sum" },
                { field: "qty", aggregate: "sum" },
                { field: "diskon", aggregate: "sum" },
                { field: "potongan_total", aggregate: "sum" },
                { field: "disc1", aggregate: "sum" },
                { field: "disc2", aggregate: "sum" },
                { field: "disc3", aggregate: "sum" },
                { field: "disc4", aggregate: "sum" },
                { field: "tberat", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            if (e.model.kode_Barang != "") {
                var found = GetBarangDetail(e.model.kode_Barang);
                var index = hargads.findIndex(function (item, i) {
                    return item.kode_Barang === e.model.kode_Barang;
                });
                dropdownlist.select(index + 1);

                $("#satuan").text(found[0].kd_Satuan);
                $("#stok").text(found[0].stok);
                $("#harga").text(e.model.harga);
                $("#harga1").text(e.model.harga1);
                $("#otp").text(e.model.otp);
                $("#berat").text(e.model.vol);
                $("#batas1").text(e.model.batas1);
                $("#batas2").text(e.model.batas2);
                $("#batas3").text(e.model.batas3);

                var numerictextbox = $("#qty").data("kendoNumericTextBox");
                numerictextbox.value(e.model.qty);

                var numerictextboxdiskon = $("#diskon").data("kendoNumericTextBox");
                numerictextboxdiskon.value(e.model.diskon);

                var numerictextboxdiskon1 = $("#diskon1").data("kendoNumericTextBox");
                numerictextboxdiskon1.value(e.model.disc1);
                var numerictextboxdiskon2 = $("#diskon2").data("kendoNumericTextBox");
                numerictextboxdiskon2.value(e.model.disc2);
                var numerictextboxdiskon3 = $("#diskon3").data("kendoNumericTextBox");
                numerictextboxdiskon3.value(e.model.disc3);
                var numerictextboxdiskon4 = $("#diskon4").data("kendoNumericTextBox");
                numerictextboxdiskon4.value(e.model.disc4);

                var numerictextboxharga = $("#harga").data("kendoNumericTextBox");
                numerictextboxharga.value(e.model.harga);

                var numerictextboxpotongan = $("#potongan_total").data("kendoNumericTextBox");
                numerictextboxpotongan.value(e.model.potongan_total);

                var numerictextboxharga1 = $("#harga1").data("kendoNumericTextBox");
                numerictextboxharga1.value(e.model.harga1);


                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    numerictextboxdiskon.readonly();
                    //numerictextboxharga.readonly();
                }

                var total = calcTotal();
                $("#total").text(total);

            }

            addCustomCssButtonCommand();

        },
        save: function (e) {
            var grid = $("#GridDO").data("kendoGrid");

            ds = grid.dataSource.data().toJSON();
            if (Mode == "NEW") {
                if (ds.length > 0) {

                    ////check diskon 
                    //var totdiscp1 = 0;
                    //var totdiscp2 = 0;
                    //var totdiscr1 = 0;
                    //var totdiscr2 = 0;

                    //if (e.model.disc1 >= 0) {
                    //    //totdiscp1 = (e.model.harga * e.model.qty) - (e.model.disc1 * (e.model.harga * e.model.qty)/100);
                    //    //e.model.total = totdiscp1;

                    //    if (e.model.disc2 >= 0) {

                    //        if (e.model.disc3 <= 0 && e.model.disc4 <= 0) {
                    //            totdiscp2 = totdiscp1 - ((e.model.disc2 * totdiscp1) / 100)
                    //            e.model.total = totdiscp2;
                    //        } else {
                    //            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
                    //            return;
                    //        }
                    //    }

                    //}

                    if (existdiskon == true) {
                        e.model.disc1 = 0;
                        e.model.disc2 = 0;
                        e.model.disc3 = 0;
                        e.model.disc4 = 0;
                    }


                    if (e.model.qty == 0 & e.model.flagbonus != true) {
                        $.when(validateSave()).done(function () {

                        });
                    } else {
                        $("#save").show();
                    }
                    var totrp = $('#TotalRupiah').val();
                    var gtotal = removeCommas(totrp.toString());
                    var sumtot = parseInt(gtotal) + (e.model.harga * e.model.qty);
                    var sumtot1 = (e.model.harga * e.model.qty);

                    var limitpiut = $("#lim_piu").val();
                    if (typeof limitpiut == "undefined") {
                        limitpiut = 0;
                    }
                    var h = removeDots(limitpiut.toString());
                    //h = h.replace(",", ".");
                    if (parseInt(e.model.harga) < parseInt(e.model.harga1)) {
                        otp = "";

                        $.when(ProsesOtp()).done(function () {
                            if (otp != "" && typeof otp != "undefined") {
                                otpe = false;

                                $.when(showOtp()).done(function () {
                                    $.when(ProsesOtp()).done(function () {
                                        if (otp != "" && typeof otp != "undefined") {
                                            otpe = false;

                                        } else {
                                            return;
                                        }
                                    });
                                });

                            } else {
                                return;
                            }
                        });


                    }
                    else if (sumtot1.toFixed(0) > parseInt(h).toFixed(0)) {


                        otp = "";

                        $.when(ProsesOtpLimit()).done(function () {
                            return;
                        });
                    }


                    else {

                        for (var i = 0; i <= ds.length - 1; i++) {

                            if (ds[i].flagbonus == true) {
                                ds[i].harga = 0;
                                ds[i].harga1 = 0;
                                ds[i].total = 0;
                            }
                            if (ds[i].otp == "gagal") {
                                ds[i].harga = ds[i].harga1;

                            } else if (ds[i].otp == "sukses") {
                                ds[i].harga = ds[i].harga;

                            }

                        }

                        DOds = ds;
                        $('#GridDO').kendoGrid('destroy').empty();
                        bindGrid();
                        ItemCalc();
                        onAddNewRow();
                    }

                }
            }
        },
        cancel: function (e) {
            $('#save').show();
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
            var grid = $("#GridDO").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();

            for (var i = 0; i <= ds.length - 1; i++) {
                if (ds[i].flagbonus == true) {
                    ds[i].harga = 0;
                    ds[i].total = 0;
                }
            }

            DOds = ds;

            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
            ItemCalc();
        },
        dataBinding: function (e) {
            var g = $("#kategori").val();
            checkstok = false;
            if (g != "BO PAKET") {
                if (e.action == "rebind") {
                    if (e.items.length > 0) {

                        for (var i = 0; i < e.items.length; i++) {
                            var found = GetBarangDetail(e.items[i].kode_Barang);
                            e.items[i].satuan = found[0].kd_Satuan;
                            e.items[i].stok = found[0].stok;
                            //e.items[i].tberat = (e.items[i].qty * e.items[i].vol).toFixed(2);
                            var qty_asli = found[0].qty
                            if (e.items[i].qty <= found[0].stok) {
                                if (e.items[i].flagbonus != true) {
                                    //  e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * 1);
                                    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                                        if (e.items[i].qty >= found[0].batas1 && e.items[i].qty < found[0].batas2) {
                                            //alert("harga 1")
                                            e.items[i].harga = found[0].harga_Rupiah;
                                            e.items[i].harga1 = found[0].harga_Rupiah;
                                        }
                                        else if (e.items[i].qty >= found[0].batas2 && e.items[i].qty < found[0].batas3) {
                                            //harga_rupiah3
                                            e.items[i].harga = found[0].harga_rupiah2;
                                            e.items[i].harga1 = found[0].harga_Rupiah2;
                                            //alert("harga 2")
                                        }
                                        else if (e.items[i].qty >= found[0].batas3) {
                                            //alert("harga 3")
                                            e.items[i].harga = found[0].harga_rupiah3;
                                            e.items[i].harga1 = found[0].harga_Rupiah3;
                                            //alert("harga 2")
                                        }
                                        else {
                                            e.items[i].harga = found[0].harga_Rupiah;
                                            e.items[i].harga1 = found[0].harga_Rupiah;
                                        }
                                        if (e.items[i].disc1 == 0 && e.items[i].disc2 == 0 && e.items[i].disc3 == 0 && e.items[i].disc4 == 0) {
                                            e.items[i].total = (e.items[i].harga * e.items[i].qty) + (e.items[i].diskon * e.items[i].qty);
                                        } else {
                                            e.items[i].total = e.items[i].total;
                                        }

                                    } else {
                                        e.items[i].harga = e.items[i].harga;
                                        e.items[i].harga1 = e.items[i].harga;
                                        if (e.items[i].disc1 == 0 && e.items[i].disc2 == 0 && e.items[i].disc3 == 0 && e.items[i].disc4 == 0) {
                                            e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * e.items[i].qty);
                                        } else {

                                            e.items[i].total = e.items[i].total;
                                        }
                                    }
                                }
                            }

                            else {
                                if (Mode == "NEW") {
                                    if (g != "BO PAKET" && g != "BOOKING ORDER") {
                                        checkstok = true;
                                    } else {
                                        checkstok = false;
                                    }

                                    //if (e.items.qty != 0 && typeof e.items.qty != 'undefined') {
                                    //   // alert("Qty Jual Melebihi Stok !!! ");

                                    //}
                                    //    e.items[i].diskon = 0;
                                    //    e.items[i].qty = 0;
                                    //    e.items[i].total = 0;
                                    //    e.items[i].harga = 0; //tberat//tberat
                                    //    e.items[i].vol = 0; //tberat//tberat
                                    //    e.items[i].tberat = 0; //tberat//tberat//diskon
                                    //    e.items[i].diskon = 0; //tberat//tberat//diskon//total
                                    //    e.items[i].total = 0; //tberat//tberat//diskon//total



                                }
                                //else if (Mode == "RETUR") {
                                //    //alert(qty_asli);

                                //}
                            }

                        }

                    }

                }
                else if (e.action == "sync") {
                    ItemCalc();
                }
            }

        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}

function bindGrid2() {
    _GridDO = $("#GridDO").kendoGrid({
        columns: columnGrid2,
        dataSource: {
            data: DOds,
            schema: {
                model: {
                    fields: {
                        kode_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        satuan: { type: "string" },
                        stok: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        qty_awal: { type: "number", validation: { required: true, min: 1, defaultValue: 1 } },
                        harga: { type: "number" },
                        harga1: { type: "number" },
                        otp: { type: "string" },
                        diskon: { type: "number", validation: { required: true, min: 0, defaultValue: 0 } },
                        total: { type: "number" },
                        keterangan: { type: "string" },
                        flagbonus: { type: "boolean" },
                        no_booked: { type: "string" },
                        vol: { type: "number" },
                        harga_rupiah2: { type: "number" },
                        harga_rupiah3: { type: "number" },
                        batas1: { type: "number" },
                        batas2: { type: "number" },
                        batas3: { type: "number" },
                        tberat: { type: "number" },

                    }
                }
            },
            aggregate: [
                { field: "vol", aggregate: "sum" },
                { field: "total", aggregate: "sum" },
                { field: "qty", aggregate: "sum" },
                { field: "diskon", aggregate: "sum" },
                { field: "tberat", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            var dropdownlist = $("#Nama_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            if (e.model.kode_Barang != "") {
                var found = GetBarangDetail(e.model.kode_Barang);
                var index = hargads.findIndex(function (item, i) {
                    return item.kode_Barang === e.model.kode_Barang;
                });
                dropdownlist.select(index + 1);

                $("#satuan").text(found[0].kd_Satuan);
                $("#stok").text(found[0].stok);
                $("#harga").text(e.model.harga);
                $("#harga1").text(e.model.harga1);
                $("#otp").text(e.model.otp);
                $("#berat").text(e.model.vol);
                $("#batas1").text(e.model.batas1);
                $("#batas2").text(e.model.batas2);
                $("#batas3").text(e.model.batas3);

                var numerictextbox = $("#qty").data("kendoNumericTextBox");
                numerictextbox.value(e.model.qty);

                var numerictextboxdiskon = $("#diskon").data("kendoNumericTextBox");
                numerictextboxdiskon.value(e.model.diskon);

                var numerictextboxharga = $("#harga").data("kendoNumericTextBox");
                numerictextboxharga.value(e.model.harga);

                var numerictextboxharga1 = $("#harga1").data("kendoNumericTextBox");
                numerictextboxharga1.value(e.model.harga1);


                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    numerictextboxdiskon.readonly();
                    //numerictextboxharga.readonly();
                }

                var total = calcTotal();
                $("#total").text(total);

            }

            addCustomCssButtonCommand();

        },
        save: function (e) {
            var grid = $("#GridDO").data("kendoGrid");

            ds = grid.dataSource.data().toJSON();
            if (Mode == "NEW") {
                if (ds.length > 0) {

                    if (e.model.qty == 0 & e.model.flagbonus != true) {
                        $.when(validateSave()).done(function () {

                        });
                    } else {
                        $("#save").show();
                    }
                    var totrp = $('#TotalRupiah').val();
                    var gtotal = removeCommas(totrp.toString());
                    var sumtot = parseInt(gtotal) + (e.model.harga * e.model.qty);

                    var limitpiut = $("#lim_piu").val();
                    if (typeof limitpiut == "undefined") {
                        limitpiut = 0;
                    }
                    var h = removeDots(limitpiut.toString());
                    if (e.model.harga < e.model.harga1 || sumtot > parseInt(h)) {
                        otp = "";
                        $.when(showOtp()).done(function () {
                            $.when(ProsesOtp()).done(function () {
                                if (otp != "" && typeof otp != "undefined") {
                                    otpe = false;

                                    $.when(showOtp()).done(function () {
                                        $.when(ProsesOtp()).done(function () {
                                            if (otp != "" && typeof otp != "undefined") {
                                                otpe = false;

                                            } else {
                                                return;
                                            }
                                        });
                                    });

                                } else {
                                    return;
                                }
                            });
                        });

                    } else {

                        for (var i = 0; i <= ds.length - 1; i++) {
                            if (ds[i].flagbonus == true) {
                                ds[i].harga = 0;
                                ds[i].harga1 = 0;
                                ds[i].total = 0;
                            }
                            if (ds[i].otp == "gagal") {
                                ds[i].harga = ds[i].harga1;

                            } else if (ds[i].otp == "sukses") {
                                ds[i].harga = ds[i].harga;

                            }

                        }

                        DOds = ds;
                        $('#GridDO').kendoGrid('destroy').empty();
                        bindGrid2();
                        ItemCalc();
                        onAddNewRow();
                    }

                }
            }
        },
        cancel: function (e) {
            $('#save').show();
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
            var grid = $("#GridDO").data("kendoGrid");
            var ds = grid.dataSource.data().toJSON();

            for (var i = 0; i <= ds.length - 1; i++) {
                if (ds[i].flagbonus == true) {
                    ds[i].harga = 0;
                    ds[i].total = 0;
                }
            }

            DOds = ds;

            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid2();
            ItemCalc();
        },

        dataBinding: function (e) {


            //Selects all edit buttons



            var g = $("#kategori").val();
            if (g != "BO PAKET") {
                if (e.action == "rebind") {
                    if (e.items.length > 0) {
                        for (var i = 0; i < e.items.length; i++) {
                            var found = GetBarangDetail(e.items[i].kode_Barang);
                            e.items[i].satuan = found[0].kd_Satuan;
                            e.items[i].stok = found[0].stok;
                            //e.items[i].tberat = (e.items[i].qty * e.items[i].vol).toFixed(2);
                            var qty_asli = found[0].qty
                            if (e.items[i].qty <= found[0].stok) {
                                if (e.items[i].flagbonus != true) {
                                    //  e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * 1);
                                    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                                        if (e.items[i].qty >= found[0].batas1 && e.items[i].qty < found[0].batas2) {
                                            //alert("harga 1")
                                            e.items[i].harga = found[0].harga_Rupiah;
                                            e.items[i].harga1 = found[0].harga_Rupiah;
                                        }
                                        else if (e.items[i].qty >= found[0].batas2 && e.items[i].qty < found[0].batas3) {
                                            //harga_rupiah3
                                            e.items[i].harga = found[0].harga_rupiah2;
                                            e.items[i].harga1 = found[0].harga_Rupiah2;
                                            //alert("harga 2")
                                        }
                                        else if (e.items[i].qty >= found[0].batas3) {
                                            //alert("harga 3")
                                            e.items[i].harga = found[0].harga_rupiah3;
                                            e.items[i].harga1 = found[0].harga_Rupiah3;
                                            //alert("harga 2")
                                        }
                                        else {
                                            e.items[i].harga = found[0].harga_Rupiah;
                                            e.items[i].harga1 = found[0].harga_Rupiah;
                                        }
                                        e.items[i].total = (e.items[i].harga * e.items[i].qty) + (e.items[i].diskon * e.items[i].qty);
                                    } else {
                                        e.items[i].harga = e.items[i].harga;
                                        e.items[i].harga1 = e.items[i].harga;
                                        e.items[i].total = (e.items[i].harga * e.items[i].qty) - (e.items[i].diskon * e.items[i].qty);
                                    }
                                }
                            }
                            else {
                                if (Mode == "NEW") {
                                    if (e.items.qty != 0 && typeof e.items.qty != 'undefined') {
                                        // alert("Qty Jual Melebihi Stok !!! ");

                                    }
                                    //    e.items[i].diskon = 0;
                                    //    e.items[i].qty = 0;
                                    //    e.items[i].total = 0;
                                    //    e.items[i].harga = 0; //tberat//tberat
                                    //    e.items[i].vol = 0; //tberat//tberat
                                    //    e.items[i].tberat = 0; //tberat//tberat//diskon
                                    //    e.items[i].diskon = 0; //tberat//tberat//diskon//total
                                    //    e.items[i].total = 0; //tberat//tberat//diskon//total



                                }
                                //else if (Mode == "RETUR") {
                                //    //alert(qty_asli);

                                //}
                            }

                        }

                    }
                }
                else if (e.action == "sync") {
                    ItemCalc();
                }
            }

        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound2
    }).data("kendoGrid");

}

function onDataBound2(e) {
    addCustomCssButtonCommand();
    
}

function onDataBound(e) {
    addCustomCssButtonCommand();
    $("#GridDO tbody tr .k-grid-edit").each(function () {
        var currentDataItem = $("#GridDO").data("kendoGrid").dataItem($(this).closest("tr"));

        //Check in the current dataItem if the row is editable
        if (currentDataItem.jns_paket == "datapaket") {
            $(this).remove();
        }

        if (Mode == "VIEW") {
            $(this).remove();

        }


    })

    //Selects all delete buttons
    $("#GridDO tbody tr .k-grid-delete").each(function () {
        var currentDataItem = $("#GridDO").data("kendoGrid").dataItem($(this).closest("tr"));

        //Check in the current dataItem if the row is deletable
        if (currentDataItem.jns_paket == "datapaket") {
            $(this).remove();
        }

        if (Mode == "VIEW") {
            $(this).remove();

        }
    })

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

function onCustomerChanged() {

    var customer = $("#Kd_Customer").val();

    var found = FindCustomer(customer);

    var customer = $("#AlamatKirim").val(found[0].alamat1);
    startSpinner('Loading..', 1);
    getPiutangCustomer();
    $("#save").show();
    $("#addKota").show();
    if (Mode != "VIEW") {
        $.when(onAddNewLimit()).done(function () {


        });
    }
   
    

}

function closemodal() {
    $("#requestModal1").hide();

}

function getPiutangCustomer() {
    return $.ajax({
        url: urlGetPiutangCustomer + "?kd_cust=" + $("#Kd_Customer").val(),
        success: function (result) {
            piutangds = [];
            piutangds = result;
            //console.log(JSON.stringify(piutangds));
            piu = piutangds[0].saldo_limit;
            $("#lim_piu").val(piutangds[0].saldo_limit.toLocaleString('id-ID', { maximumFractionDigits: 2 }))
            startSpinner('Loading..', 0);
            console.log(piu);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function FindCustomer(code) {
    return customerds.filter(
        function (customerds) { return customerds.kd_Customer === code; }
    );
}

function onAddNewRow() {
   
    var grid = $("#GridDO").data("kendoGrid");
    grid.addRow();
    
    $('#Nama_Barang').data("kendoDropDownList").open();
}

function barangDropDownEditor(container, options) {
    var input = $('<input required id="Nama_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Barang",
        dataValueField: "nama_Barang",
        dataSource: hargads,
        optionLabel: "Select Barang...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.kode_Barang}' data-Barang='${data.nama_Barang}'>${data.nama_Barang}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            kd = id;
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.kode_Barang = id;
            barang.nama_Barang = Barang.split("| ", 1)[0];

            var found = GetBarangDetail(id);
            barang.satuan = found[0].kd_Satuan;
            barang.stok = found[0].stok;
            barang.harga = found[0].harga_Rupiah;
            barang.harga1 = found[0].harga_Rupiah;
            barang.vol = found[0].vol;


            $("#satuan").text(found[0].kd_Satuan);
            $("#stok").text(found[0].stok);
            $("#berat").text(found[0].vol);

            if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                if (barang.qty >= found[0].batas1 && barang.qty < found[0].batas2) {
                    barang.harga = found[0].harga_Rupiah;
                    barang.harga1 = found[0].harga_Rupiah;
                }
                else if (barang.qty >= found[0].batas2 && barang.qty < found[0].batas3) {
                    barang.harga = found[0].harga_rupiah2;
                    barang.harga1 = found[0].harga_Rupiah2;
                }
                else if (barang.qty >= found[0].batas3) {
                    barang.harga = found[0].harga_rupiah3;
                    barang.harga1 = found[0].harga_Rupiah3;
                }
                else {
                    barang.harga = found[0].harga_Rupiah;
                    barang.harga1 = found[0].harga_Rupiah;
                }
                $("#harga").text(barang.harga);
                $("#harga").val(barang.harga);
                $("#harga1").text(barang.harga);
                $("#harga1").val(barang.harga);
            } else {
                $("#harga").text(found[0].harga_Rupiah);
                $("#harga").val(found[0].harga_Rupiah);
                $('#harga').data('kendoNumericTextBox').options.min = 0;
                $("#harga1").text(found[0].harga_Rupiah);
                $("#harga1").val(found[0].harga_Rupiah);
                $('#harga1').data('kendoNumericTextBox').options.min = 0;
            }

            var total = calcTotal();
            barang.total = total;

            $("#total").text(total);
        }
    }).appendTo(container);
}

function GetBarangDetail(code) {
    return hargads.filter(
        function (hargads) { return hargads.kode_Barang === code; }
    );
}

function satuanLabel(container, options) {
    var input = $('<label id="satuan" />');
    input.appendTo(container);
}

function stokLabel(container, options) {
    var input = $('<label id="stok" />');
    input.appendTo(container);
}
function stokLabelhari(container, options) {
    var input = $('<label id="lama_hari" />');
    input.appendTo(container);
}


function hargaLabel(container, options) {
    var input = $('<label id="harga" />');
    input.appendTo(container);
}

function totalLabel(container, options) {
    var input = $('<label id="total" />');
    input.appendTo(container);
}

function totalLabelpiutang(container, options) {
    var input = $('<label id="jml_akhir" />');
    input.appendTo(container);
}
function beratLabel(container, options) {
    var input = $('<label id="berat" />');
    input.appendTo(container);
}

function tberatLabel(container, options) {
    var input = $('<label id="tberat" />');
    input.appendTo(container);
}

function qtyNumeric(container, options) {
    var input = $('<input id="qty" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "{0:n2}",
        decimals: 2,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.qty = value;



            var found = GetBarangDetail(barang.kode_Barang);
            if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                var hargadiskon = 0;
                if (barang.qty >= found[0].batas1 && barang.qty < found[0].batas2) {
                    if (barang.otp != "") {
                        barang.harga = barang.harga;
                    } else {
                        if (barang.harga == "") {
                            barang.harga = found[0].harga_Rupiah;
                        } else {
                            barang.harga = barang.harga;
                        }
                    }

                }
                else if (barang.qty >= found[0].batas2 && barang.qty < found[0].batas3) {

                    if (barang.otp != "") {
                        barang.harga = barang.harga;
                    } else {
                        if (barang.harga == "") {
                            barang.harga = found[0].harga_Rupiah2;
                        } else {
                            barang.harga = barang.harga;
                        }
                    }
                }
                else if (barang.qty >= found[0].batas3) {
                    if (barang.otp != "") {
                        barang.harga = barang.harga;
                    } else {
                        if (barang.harga == "") {
                            barang.harga = found[0].harga_Rupiah3;
                        } else {
                            barang.harga = barang.harga;
                        }
                    }
                }
                else {
                    if (barang.otp != "") {
                        barang.harga = barang.harga;
                    } else {
                        if (barang.harga == "") {
                            barang.harga = found[0].harga_Rupiah;
                        } else {
                            barang.harga = barang.harga;
                        }
                    }
                }
                $("#harga").text(barang.harga);
                $("#harga").val(barang.harga);
                hargadiskon = (found[0].harga_Rupiah * 1) - (barang.harga * 1);

                barang.diskon = hargadiskon;
                $("#diskon").val(hargadiskon);
                $("#diskon").text(hargadiskon);
            } else {
                if (barang.otp != "") {
                    $("#harga").text(barang.harga);
                    $("#harga").val(barang.harga);
                    $('#harga').data('kendoNumericTextBox').options.min = 0;
                } else {
                    if (barang.harga != "") {
                        $("#harga").text(barang.harga);
                        $("#harga").val(barang.harga);
                    } else {
                        $("#harga").text(found[0].harga_Rupiah);
                        $("#harga").val(found[0].harga_Rupiah);
                    }
                   
                    $('#harga').data('kendoNumericTextBox').options.min = 0;
                }


            }
            var total = calcTotal();
            barang.total = total;
            barang.tberat = (found[0].vol * value).toFixed(2);
            $("#total").text(total);
        }
    });
}

function hargaNumeric(container, options) {
    var input = $('<input id="harga" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.harga = value;
            var total = calcTotal();
            barang.total = total;

            $("#total").text(total);
        }
    });

    var numerictextboxharga = $("#harga").data("kendoNumericTextBox");



    // if (BranchID == "TOKO" || akses_penjualan == "CASH") {
    // //    numerictextboxdiskon.readonly();
    // }
}

function hargaNumeric1(container, options) {
    var input = $('<input id="harga1" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: 0

    });

    //var numerictextboxdiskon = $("#harga1").data("kendoNumericTextBox");
    //if (BranchID == "TOKO" || akses_penjualan == "CASH") {
    //    numerictextboxdiskon.readonly();
    //}
}

function diskonNumeric(container, options) {
    var input = $('<input id="diskon" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.diskon = value;
            var total = calcTotal();
            barang.total = total;

            $("#total").text(total);
        }
    });
    var numerictextboxdiskon = $("#diskon").data("kendoNumericTextBox");
    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
        //  numerictextboxdiskon.readonly();
    }
}

function diskonNumericpot(container, options) {
    var input = $('<input id="potongan_total" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.potongan_total = value;
           
        }
    });
    var numerictextboxdiskon = $("#potongan_total").data("kendoNumericTextBox");
    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
        //  numerictextboxdiskon.readonly();
    }
    numerictextboxdiskon.readonly();
   
}

    function diskonNumeric1(container, options) {
        var input = $('<input id="diskon1" />');
        input.appendTo(container);

        input.kendoNumericTextBox({
            format: "#",
            decimals: 0,
            min: 0,
            max: 100,
            change: function (e) {
                var value = this.value();
                var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
                barang.disc1 = value;
                var total = calcTotal();
                barang.total = total;

                $("#total").text(total);
            }
        });
        var numerictextboxdiskon = $("#diskon1").data("kendoNumericTextBox");
        if (BranchID == "TOKO" || akses_penjualan == "CASH") {
            //  numerictextboxdiskon.readonly();
        }
    }


        function diskonNumeric2(container, options) {
            var input = $('<input id="diskon2" />');
            input.appendTo(container);

            input.kendoNumericTextBox({
                format: "#",
                decimals: 0,
                min: 0,
                max: 100,
                change: function (e) {
                    var value = this.value();
                    var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
                    barang.disc2 = value;
                    var total = calcTotal();
                    barang.total = total;

                    $("#total").text(total);
                }
            });
            var numerictextboxdiskon = $("#diskon2").data("kendoNumericTextBox");
            if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                //  numerictextboxdiskon.readonly();
            }
}


            function diskonNumeric3(container, options) {
                var input = $('<input id="diskon3" />');
                input.appendTo(container);

                input.kendoNumericTextBox({
                    format: "#",
                    decimals: 0,
                    min: 0,
                    change: function (e) {
                        var value = this.value();
                        var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
                        barang.disc3 = value;
                        var total = calcTotal();
                        barang.total = total;

                        $("#total").text(total);
                    }
                });
                var numerictextboxdiskon = $("#diskon3").data("kendoNumericTextBox");
                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    //  numerictextboxdiskon.readonly();
                }
}

function diskonNumeric4(container, options) {
    var input = $('<input id="diskon4" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "#",
        decimals: 0,
        min: 0,
        change: function (e) {
            var value = this.value();
            var barang = _GridDO.dataItem($(e.sender.element).closest("tr"));
            barang.disc4 = value;
            var total = calcTotal();
            barang.total = total;

            $("#total").text(total);
        }
    });
    var numerictextboxdiskon = $("#diskon4").data("kendoNumericTextBox");
    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
        //  numerictextboxdiskon.readonly();
    }
}


    
function calcTotal() {


    existdiskon = false;
    var qty = $("#qty").val();
    var harga = $("#harga").val();
    var diskon = $("#diskon").val();
    var berat = $("#berat").text();
    // return (qty * harga) - (diskon * 1);
    var total = 0;
    var tberat = 0;

    //check diskon 
    var totdiscp1 = $("#diskon1").val();
    var totdiscp2 = $("#diskon2").val();
    var totdiscr1 = $("#diskon3").val();
    var totdiscr2 = $("#diskon4").val();

    if (typeof totdiscp1 == "undefined") {
        totdiscp1 = 0;
    }
    if (typeof totdiscp2 == "undefined") {
        totdiscp1 = 0;
    }
    if (typeof totdiscr1 == "undefined") {
        totdiscr1 = 0;
    }
    if (typeof totdiscr2 == "undefined") {
        totdiscr2 = 0;
    }
    totpototongan = 0;

    ////if (typeof qty != "undefined") {
    //   // if (BranchID == "TOKO" || akses_penjualan == "CASH") {
    //        //total = ((qty * harga) * 1) + ((diskon * qty) * 1)
    //       // if (totdiscp1 > 0)
    //        //{
    //            totpototongan = totdiscp1 * (qty * harga) / 100;
    //            totdiscp1 = (qty * harga) - (totdiscp1 * (qty * harga)/100);
               
    //            if (totdiscp2 > 0) {

    //                if (totdiscr1 <= 0 && totdiscr2 <= 0) {
    //                    totpototongan += (totdiscp2 * totdiscp1) / 100;
    //                    totdiscp2 = totdiscp1 - ((totdiscp2 * totdiscp1) / 100);
                       
    //                    total = totdiscp2.toFixed(1);
    //                } else {
                       
    //                    totdiscp1 = 0;
    //                    totdiscp2 = 0;
    //                    totdiscr1 = 0;
    //                    totdiscr2 = 0;
    //                    $("#diskon1").val(0);
    //                    $("#diskon2").val(0);
    //                    $("#diskon3").val(0);
    //                    $("#diskon4").val(0);
    //                    var numeric1 = $("#diskon1").getKendoNumericTextBox();
    //                    numeric1.focus();
    //                    var numeric2 = $("#diskon2").getKendoNumericTextBox();
    //                    numeric2.focus();
    //                    var numeric3 = $("#diskon3").getKendoNumericTextBox();
    //                    numeric3.focus();
    //                    var numeric4 = $("#diskon4").getKendoNumericTextBox();
    //                    numeric4.focus();
    //                    total = (qty * harga);
    //                    Swal.fire({
    //                        type: 'error',
    //                        title: 'Error DIskon',
    //                        html: "Diskon Harus Satu Tipe"
    //                    });
    //                    existdiskon = true;
    //                    totpototongan = 0;
    //                }
    //            }
    //            else if (totdiscp2 == 0) {
    //                total = totdiscp1.toFixed(1);
    //                totpototongan = totpototongan.toFixed(1);
    //            }

    //        }
    //        else if (totdiscp2 > 0 && totdiscp1 <= 0) {

    //            totdiscp1 = 0;
    //            totdiscp2 = 0;
    //            totdiscr1 = 0;
    //            totdiscr2 = 0;
    //            $("#diskon1").val(0);
    //            $("#diskon2").val(0);
    //            $("#diskon3").val(0);
    //            $("#diskon4").val(0);
    //            var numeric1 = $("#diskon1").getKendoNumericTextBox();
    //            numeric1.focus();
    //            var numeric2 = $("#diskon2").getKendoNumericTextBox();
    //            numeric2.focus();
    //            var numeric3 = $("#diskon3").getKendoNumericTextBox();
    //            numeric3.focus();
    //            var numeric4 = $("#diskon4").getKendoNumericTextBox();
    //            numeric4.focus();
    //            total = (qty * harga);
    //            Swal.fire({
    //                type: 'error',
    //                title: 'Error DIskon',
    //                html: "Diskon Persen Harus Pilih Diskon 1 Dulu"
    //            });
    //            existdiskon = true;
    //            totpototongan = 0;

    //        }

    //        else if (totdiscr2 > 0 && totdiscr1 <= 0) {
    //            Swal.fire({
    //                type: 'error',
    //                title: 'Error DIskon',
    //                html: "Diskon Rupiah Harus Pilih Diskon  1 Dulu"
    //            });
    //            totdiscp1 = 0;
    //            totdiscp2 = 0;
    //            totdiscr1 = 0;
    //            totdiscr2 = 0;
    //            $("#diskon1").val(0);
    //            $("#diskon2").val(0);
    //            $("#diskon3").val(0);
    //            $("#diskon4").val(0);
    //            total = (qty * harga);
    //            existdiskon = true;
    //            var numeric1 = $("#diskon1").getKendoNumericTextBox();
    //            numeric1.focus();
    //            var numeric2 = $("#diskon2").getKendoNumericTextBox();
    //            numeric2.focus();
    //            var numeric3 = $("#diskon3").getKendoNumericTextBox();
    //            numeric3.focus();
    //            var numeric4 = $("#diskon4").getKendoNumericTextBox();
    //            numeric4.focus();
    //            totpototongan = 0;
    //        }

    //        else if (totdiscr1 > 0) { // total diskon rupiah
    //            totpototongan = qty * totdiscr1;
    //            totdiscr1 = (qty * harga) - (qty * totdiscr1);
                
    //            if (totdiscr2 > 0) {

    //                if (totdiscp1 <= 0 && totdiscp2 <= 0) {
    //                    totpototongan += qty * totdiscr2;
    //                    totdiscr2 = totdiscr1 - (qty * totdiscr2);
    //                    total = totdiscr2;
                       
    //                } else {
    //                    Swal.fire({
    //                        type: 'error',
    //                        title: 'Error DIskon',
    //                        html: "Diskon Harus Satu Tipe Persen atau Rupiah"
    //                    });
    //                    totdiscp1 = 0;
    //                    totdiscp2 = 0;
    //                    totdiscr1 = 0;
    //                    totdiscr2 = 0;
    //                    $("#diskon1").val(0);
    //                    $("#diskon2").val(0);
    //                    $("#diskon3").val(0);
    //                    $("#diskon4").val(0);
    //                    total = (qty * harga);
    //                    existdiskon = true;
    //                    var numeric1 = $("#diskon1").getKendoNumericTextBox();
    //                    numeric1.focus();
    //                    var numeric2 = $("#diskon2").getKendoNumericTextBox();
    //                    numeric2.focus();
    //                    var numeric3 = $("#diskon3").getKendoNumericTextBox();
    //                    numeric3.focus();
    //                    var numeric4 = $("#diskon4").getKendoNumericTextBox();
    //                    numeric4.focus();
    //                    totpototongan = 0;
                       
    //                }
    //            } else if (totdiscr2 == 0) {
    //                totpototongan = totpototongan.toFixed(1);
    //                total = totdiscr1;
                
    //            } 
    //        } else {
    //            total = (qty * harga);
    //            totpototongan = 0;
    //        }
    //   // }
    //   // else {
    //        //total = (qty * harga) - (qty * diskon);




    if (totdiscp1 > 0 && totdiscr1 == 0)
    {
                totpototongan = totdiscp1 * (qty * harga) / 100;
                totdiscp1 = (qty * harga) - (totdiscp1 * (qty * harga) / 100);
               
                if (totdiscp2 > 0) {

                    if (totdiscr1 <= 0 && totdiscr2 <= 0) {
                        totpototongan += (totdiscp2 * totdiscp1) / 100;
                        totdiscp2 = totdiscp1 - ((totdiscp2 * totdiscp1) / 100);
                        total = totdiscp2.toFixed(0);
                       
                    } else {
                        Swal.fire({
                            type: 'error',
                            title: 'Error DIskon',
                            html: "Diskon Harus Satu Tipe Persen atau Rupiah"
                        });
                        totdiscp1 = 0;
                        totdiscp2 = 0;
                        totdiscr1 = 0;
                        totdiscr2 = 0;
                        $("#diskon1").val(0);
                        $("#diskon2").val(0);
                        $("#diskon3").val(0);
                        $("#diskon4").val(0);
                        total = (qty * harga);
                        var numeric1 = $("#diskon1").getKendoNumericTextBox();
                        numeric1.focus();
                        var numeric2 = $("#diskon2").getKendoNumericTextBox();
                        numeric2.focus();
                        var numeric3 = $("#diskon3").getKendoNumericTextBox();
                        numeric3.focus();
                        var numeric4 = $("#diskon4").getKendoNumericTextBox();
                        numeric4.focus();
                        existdiskon = true;
                        totpototongan = 0;
                    }
                } else if (totdiscp2 == 0) {
                    totpototongan = totpototongan.toFixed(0);
                    total = totdiscp1.toFixed(0);
                }




    }

    else if (totdiscp2 > 0 && totdiscp1 <= 0) {

                totdiscp1 = 0;
                totdiscp2 = 0;
                totdiscr1 = 0;
                totdiscr2 = 0;
                $("#diskon1").val(0);
                $("#diskon2").val(0);
                $("#diskon3").val(0);
                $("#diskon4").val(0);
                var numeric1 = $("#diskon1").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#diskon2").getKendoNumericTextBox();
                numeric2.focus();
                var numeric3 = $("#diskon3").getKendoNumericTextBox();
                numeric3.focus();
                var numeric4 = $("#diskon4").getKendoNumericTextBox();
                numeric4.focus();
                total = (qty * harga);
                Swal.fire({
                    type: 'error',
                    title: 'Error DIskon',
                    html: "Diskon Persen Harus Pilih Diskon 1 Dulu"
                });
                existdiskon = true;
                totpototongan = 0;
            }


    else if (totdiscr2 > 0 && totdiscr1 <= 0) {
                Swal.fire({
                    type: 'error',
                    title: 'Error DIskon',
                    html: "Diskon Rupiah Harus Pilih Diskon  1 Dulu"
                });
                totdiscp1 = 0;
                totdiscp2 = 0;
                totdiscr1 = 0;
                totdiscr2 = 0;
                $("#diskon1").val(0);
                $("#diskon2").val(0);
                $("#diskon3").val(0);
                $("#diskon4").val(0);
                total = (qty * harga);
                existdiskon = true;
                var numeric1 = $("#diskon1").getKendoNumericTextBox();
                numeric1.focus();
                var numeric2 = $("#diskon2").getKendoNumericTextBox();
                numeric2.focus();
                var numeric3 = $("#diskon3").getKendoNumericTextBox();
                numeric3.focus();
                var numeric4 = $("#diskon4").getKendoNumericTextBox();
                numeric4.focus();
                totpototongan = 0;

            }

    else if (totdiscr1 > 0 && totdiscp1 == 0) { // total diskon rupiah
                totpototongan = qty * totdiscr1;
                totdiscr1 = (qty * harga) - (qty * totdiscr1);

                if (totdiscr2 >= 0) {

                    if (totdiscp1 <= 0 && totdiscp2 <= 0) {
                        totpototongan += qty * totdiscr2;
                        totdiscr2 = totdiscr1 - (totdiscr2 * qty);
                        total = totdiscr2.toFixed(0);
                    } else {
                        Swal.fire({
                            type: 'error',
                            title: 'Error DIskon',
                            html: "Diskon Harus Satu Tipe Persen atau Rupiah"
                        });
                        totdiscp1 = 0;
                        totdiscp2 = 0;
                        totdiscr1 = 0;
                        totdiscr2 = 0;
                        $("#diskon1").val(0);
                        $("#diskon2").val(0);
                        $("#diskon3").val(0);
                        $("#diskon4").val(0);
                        total = (qty * harga);
                        existdiskon = true;

                        var numeric1 = $("#diskon1").getKendoNumericTextBox();
                        numeric1.focus();
                        var numeric2 = $("#diskon2").getKendoNumericTextBox();
                        numeric2.focus();
                        var numeric3 = $("#diskon3").getKendoNumericTextBox();
                        numeric3.focus();
                        var numeric4 = $("#diskon4").getKendoNumericTextBox();
                        numeric4.focus();
                        totpototongan = 0;
                    }
                } else if (totdiscr2 == 0) {
                    totpototongan = totpototongan.toFixed(0);
                    total = totdiscr1.toFixed(0);
                } 
    }
    else
    {
        if (totdiscp1 != totdiscr1 || totdiscp1 != totdiscr2 || totdiscp1 != totdiscr1 || totdiscp2 != totdiscr2) {
            Swal.fire({
                type: 'error',
                title: 'Error DIskon',
                html: "Diskon Harus Satu Tipe Persen atau Rupiah"
            });
            totdiscp1 = 0;
            totdiscp2 = 0;
            totdiscr1 = 0;
            totdiscr2 = 0;
            $("#diskon1").val(0);
            $("#diskon2").val(0);
            $("#diskon3").val(0);
            $("#diskon4").val(0);
            existdiskon = true;
            var numeric1 = $("#diskon1").getKendoNumericTextBox();
            numeric1.focus();
            var numeric2 = $("#diskon2").getKendoNumericTextBox();
            numeric2.focus();
            var numeric3 = $("#diskon3").getKendoNumericTextBox();
            numeric3.focus();
            var numeric4 = $("#diskon4").getKendoNumericTextBox();
            numeric4.focus();
        }
               
                total = (qty * harga);
              
               
                totpototongan = 0;                                                                                                                                                                                                                                                                         
    }
        
        tberat = (qty * berat);
        $("#tberat").text(tberat.toFixed(2));
    //}

    var numericpot = $("#potongan_total").getKendoNumericTextBox();
    $("#potongan_total").val(totpototongan);
    $("#potongan_total").text(totpototongan);
    numericpot.focus();
    return total;
  
    //return (qty * harga);
}

function ItemCalc() {
    if (_GridDO) {
        var requestData = _GridDO.dataSource.data();
        var value = 0;
        var _ongkir = $("#ongkir").val();
        var diskon = 0;
        var SubTotal = $("#SubTotal").val();
        var PPN = removeCommas($("#PPN").val());
        var TotalRupiah = $("#TotalRupiah").val();
        var Diskon = $("#Diskon").val();

        for (var i = 0; i < requestData.length; i++) {
            value += (requestData[i].total * 1);
            if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                diskon += (requestData[i].diskon * 1) * (requestData[i].qty * 1);
            }
            else {
                //  diskon += (requestData[i].diskon * 1) * (requestData[i].qty * 1);
                diskon = 0;
            }

        }
        if (value != 0) {
            SubTotal = value;
            $('#SubTotal').val(addCommas1(value.toString()));
        } else {
            $('#SubTotal').val(0);
        }

        if (value != 0) {
            Diskon = diskon;
            $('#Diskon').val(addCommas(diskon.toString()));
        } else {
            $('#Diskon').val(0);
        }
        if ($('#checkbox2').is(":checked")) {
            var ppn1 = value * 0.1;
            PPN = ppn1.toFixed(1);
            $('#PPN').val(addCommas1(PPN.toString()));
        }
        else {
            PPN = 0;
            $('#PPN').val(0);
        }

        Total = ((SubTotal * 1) + (_ongkir * 1) + (PPN * 1)) - (diskon * 1);
        totaltran = Total;
        //Total = (SubTotal * 1) - (diskon * 1);
        if (Total != 0) {
            $('#TotalRupiah').val(addCommas1(Total.toString()));
            //$('#TotalRupiah').val(Total.toString());
        } else {
            totaltran = 0;
            $('#TotalRupiah').val(0);
        }
    } else {
        $('#SubTotal').val(0);
        $('#TotalRupiah').val(0);
        $("#PPN").val(0);

    }
}

function removeCommas(str) {
    return str.replace(/,/g, '');
}

function removeDots(str) {
    return str.replaceAll(".","");
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function addCommas1(bilangan) {
    var number_string = bilangan.toString(),
        split = number_string.split('.'),
        sisa = split[0].length % 3,
        rupiah = split[0].substr(0, sisa),
        ribuan = split[0].substr(sisa).match(/\d{1,3}/gi);

    if (ribuan) {
        separator = sisa ? ',' : '';
        rupiah += separator + ribuan.join(',');
    }
    rupiah = split[1] != undefined ? rupiah + '.' + split[1] : rupiah;
    return rupiah;
}
function onSaveClicked() {

    ItemCalc();
    validationPage();
}

function validationPage() {
    var tanggalkirim = $('#tanggalkirim').val();
    //var totaltrans = $("#TotalRupiah").val();
    var Kd_Customer = $('#Kd_Customer').val();
    var jt = $('#jt').val();
    var jenisDO = $('#jenisDO').val();
    var keterangan = $('#Keterangan').val();
    var dp = $("#dp").val();
    var kategori = $("#kategori").val(); 
    var paket = $("#paket").val(); 
    var kasir = $("#kasir").val();
    var ItemData = _GridDO.dataSource.data();
    validationMessage = '';
    if (jenisDO != "CASH") {
        if (totaltran > piu) {
            validationMessage = validationMessage + 'Transaksi Melebihi Limit Piutang!' + '\n';
        }
        if (!tanggalkirim) {
            validationMessage = validationMessage + 'Rencana kirim tidak boleh kosong.' + '\n';
        }
        if (!jt) {
            validationMessage = validationMessage + 'Jatuh Tempo tidak boleh kosong.' + '\n';
        }
        if (!dp) {
            validationMessage = validationMessage + 'DP tidak boleh kosong.' + '\n';
        }
        if (!Kd_Customer) {
            validationMessage = validationMessage + 'Customer harus di pilih.' + '\n';
        }

    }
    //if (!kasir) {
    //    validationMessage = validationMessage + 'Sales harus di pilih.' + '\n';
    //}


    if (!keterangan) {
        validationMessage = validationMessage + 'keterangan tidak boleh kosong.' + '\n';
    }

    //}
    if (!kategori) {
        validationMessage = validationMessage + 'kategori harus di pilih.' + '\n';
    } else {
        if (kategori == "BO PAKET") {
            if (!paket) {
                validationMessage = validationMessage + 'paket harus di pilih.' + '\n';
            }
        }
        else if (kategori == "BOOKING ORDER") {
            checkstok = false;
        }
    }
    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Tambahkan Item.' + '\n';
    }
    //if (piu > totaltran) {
    //    validationMessage = validationMessage + 'Transaksi Melebihi Limit Piutang!' + '\n';
    //}

    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        var cust = GetCustomerDetail($("#Kd_Customer").val());
        if (piutangds.length > 0 && piutangds[0].total > cust[0].creditLimit) {
            //customerPiutangModal
            if ($('#gvPiutangCustomer').hasClass("k-grid")) {
                $('#gvPiutangCustomer').kendoGrid('destroy').empty();
            }
            $("#btnSimpanPiutang").show();
            bindGridPiutang();
            $('#customerPiutangModal').modal('show');

        } else {
            //  startSpinner('loading..', 1);


            SaveData();
        }
    }
}

function GetCustomerDetail(code) {
    return customerds.filter(
        function (customerds) { return customerds.kd_Customer === code; }
    );
}

function validateSave() {
    var bok = $("#kategori").val();
    $("#save").show();
    if (bok != "Booking Order") {
       
        swal({
            type: 'warning',
            title: 'Error Qty Cannot 0',
            html: 'Qty Cannot 0',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        })
        $("#save").hide();
        var grid = $('#GridDO').data("kendoGrid");
        grid.cancel();
    
        
        
    } 

}

function SaveData() {
  
    var jns_so1 = $("#kategori").val();
    var SP_REFFval;
    var ppnval;
    if ($('#checkbox2').is(":checked")) {
        ppnval = "Y";
    }
    else {
        ppnval = "T";
    }
    if ($('#cb_ongkir').is(":checked")) {
        ex_jasa = "Y";
    }
    else {
        ex_jasa = "N";
    }
    var no = "";

    var urlSaveData
    if (Mode == "RETUR") {
        no = $("#DONumber").val();
        SP_REFFval = $("#RefNo1").val();
        urlSaveData = urlSaveRetur;
    }
    else if (Mode == "EDIT") {
        no = $("#DONumber").val();
        urlSaveData = urlEdit;
    }
    else {
        urlSaveData = urlSave;
    }
    var savedata = {

        No_sp: no,
        Jenis_sp: $("#jenisDO").val(),
        Kd_Customer: $("#Kd_Customer").val(),
        Atas_Nama: $("#Kd_Customer option:selected").text(),
        Tgl_sp: $("#tanggal").val(),
        Tgl_Kirim_Marketing: $("#tanggalkirim").val(),
        Kd_sales: $("#kasir").val(),
        Keterangan: $("#Keterangan").val(),
        Almt_pnrm: $("#AlamatKirim").val(),
        SP_REFF: $("#RefNo1").val(),
        SP_REFF2: $("#RefNo2").val(),
        JML_RP_TRANS: $("#TotalRupiah").val(),
        JML_VALAS_TRANS: $("#TotalRupiah").val(),
        Jatuh_Tempo: $("#jt").val(),
        Flag_Ppn: ppnval,
        inc_ongkir: ex_jasa,
        PPn: $("#PPN").val(),
        potongan_total: totpototongan,
        dp: $("#dp").val(),
        Discount: $("#Diskon").val(),
        Biaya: $("#ongkir").val(),
        stat_save: 1,
        jenis_so: $("#kategori").val(),
        no_paket: $("#paket").val(),
        details: _GridDO.dataSource.data().toJSON()
    };
    //console.log(JSON.stringify(savedata));
    swal({
        type: 'warning',
        title: 'Are you sure?',
        html: 'You want to submit this data',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {

            if (checkstok == true) {

                swal({
                    type: 'warning',
                    title: 'Are you sure?',
                    html: 'Qty is not enough can be replace to BOOKING ORDER, You want to submit this data',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d9534f'
                }).then(function (isConfirm) {
                    if (isConfirm.value === true) {

                        startSpinner('loading..', 1);
                        $.ajax({
                            type: "POST",
                            data: savedata,
                            url: urlSaveData,
                            success: function (result) {
                                if (result.success === false) {
                                    Swal.fire({
                                        type: 'error',
                                        title: 'Warning',
                                        html: result.message
                                    });
                                    startSpinner('loading..', 0);
                                } else {

                                    //Swal.fire({
                                    //    type: 'success',
                                    //    title: 'Success',
                                    //    html: "Save Successfully"
                                    //});
                                    if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                                        $.when(printNotaNew(result.result)).done(function () {
                                            startSpinner('loading..', 0);
                                            if (Mode == "RETUR") {
                                                $.when(printNotaNew(SP_REFFval)).done(function () {
                                                    swal({
                                                        type: 'warning',
                                                        title: 'Apakah anda akan mencetak SJ?',
                                                        html: '',
                                                        showCancelButton: true,
                                                        confirmButtonColor: '#3085d6',
                                                        cancelButtonColor: '#d9534f'
                                                    }).then(function (isConfirm) {
                                                        if (isConfirm.value === true) {
                                                            startSpinner('loading..', 1);

                                                            $.when(printSJNew(result.result)).done(function () {
                                                                startSpinner('loading..', 0);
                                                                window.location.href = urlCreate;
                                                            });
                                                        }
                                                        else {
                                                            window.location.href = urlCreate;
                                                        }
                                                    });
                                                });
                                            }
                                            else {
                                                swal({
                                                    type: 'warning',
                                                    title: 'Apakah anda akan mencetak SJ?',
                                                    html: '',
                                                    showCancelButton: true,
                                                    confirmButtonColor: '#3085d6',
                                                    cancelButtonColor: '#d9534f'
                                                }).then(function (isConfirm) {
                                                    if (isConfirm.value === true) {
                                                        startSpinner('loading..', 1);

                                                        $.when(printSJNew(result.result)).done(function () {
                                                            startSpinner('loading..', 0);
                                                            window.location.href = urlCreate;
                                                        });
                                                    }
                                                    else {
                                                        window.location.href = urlCreate;
                                                    }
                                                });
                                            }

                                        });
                                    }
                                    else {
                                        startSpinner('loading..', 0);
                                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
                                    }


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

            else {
                //if (jns_so1 == "BOOKING ORDER" || jns_so1 == "BO PAKET")
                //{
                    startSpinner('loading..', 1);
                    $.ajax({
                        type: "POST",
                        data: savedata,
                        url: urlSaveData,
                        success: function (result) {
                            if (result.success === false) {
                                Swal.fire({
                                    type: 'error',
                                    title: 'Warning',
                                    html: result.message
                                });
                                startSpinner('loading..', 0);
                            } else {

                                //Swal.fire({
                                //    type: 'success',
                                //    title: 'Success',
                                //    html: "Save Successfully"
                                //});
                                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                                    $.when(printNotaNew(result.result)).done(function () {
                                        startSpinner('loading..', 0);
                                        if (Mode == "RETUR") {
                                            $.when(printNotaNew(SP_REFFval)).done(function () {
                                                swal({
                                                    type: 'warning',
                                                    title: 'Apakah anda akan mencetak SJ?',
                                                    html: '',
                                                    showCancelButton: true,
                                                    confirmButtonColor: '#3085d6',
                                                    cancelButtonColor: '#d9534f'
                                                }).then(function (isConfirm) {
                                                    if (isConfirm.value === true) {
                                                        startSpinner('loading..', 1);

                                                        $.when(printSJNew(result.result)).done(function () {
                                                            startSpinner('loading..', 0);
                                                            window.location.href = urlCreate;
                                                        });
                                                    }
                                                    else {
                                                        window.location.href = urlCreate;
                                                    }
                                                });
                                            });
                                        }
                                        else {
                                            swal({
                                                type: 'warning',
                                                title: 'Apakah anda akan mencetak SJ?',
                                                html: '',
                                                showCancelButton: true,
                                                confirmButtonColor: '#3085d6',
                                                cancelButtonColor: '#d9534f'
                                            }).then(function (isConfirm) {
                                                if (isConfirm.value === true) {
                                                    startSpinner('loading..', 1);

                                                    $.when(printSJNew(result.result)).done(function () {
                                                        startSpinner('loading..', 0);
                                                        window.location.href = urlCreate;
                                                    });
                                                }
                                                else {
                                                    window.location.href = urlCreate;
                                                }
                                            });
                                        }

                                    });
                                }
                                else {
                                    startSpinner('loading..', 0);
                                    window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
                                }


                            }
                        },
                        error: function (data) {
                            alert('Something Went Wrong');
                            startSpinner('loading..', 0);
                        }
                    });
               // }


//
            }

            

        }
    });
}

function showCreate() {
    window.location.href = urlCreate;
}

function onPrintClicked() {
    // alert(idDO);

    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?q=" + idDO, "_blank");
}

function onAddNewDPM() {
    startSpinner('loading..', 2);

    $.ajax({
        url: urlGetIndenDO + "?status=Alokasi&kd_customer=" + $("#Kd_Customer").val() + "&Kd_sales=" + salesID,
        type: "GET",
        success: function (result) {
            dpmds = result;
            console.log(JSON.stringify(dpmds));
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


function onAddNewLimit() {
   // startSpinner('loading..', 2);
    
    return $.ajax({
        url: urlGetLimit + "?kd_customer=" + $("#Kd_Customer").val(),
        type: "GET",
        success: function (result) {
            dpmlimit = result;
            
            if (_GridLimit != undefined) {
                $("#DPMLimit").kendoGrid('destroy').empty();
            }
            bindGridLimit();
           // startSpinner('loading..', 0);

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
            { field: "nama_Barang", title: "Nama Barang", width: "180px" },
            { field: "kd_satuan", title: "Satuan", width: "80px" },
            { field: "berat", title: "Berat", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "totalBerat", title: "Total Berat", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "qty", title: "Qty Request", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "keterangan", title: "Keterangan", width: "180px" },
            { field: "nama_Sales", title: "Sales", width: "180px" }
            
           
        ],
        dataSource: {
            data: dpmds,
            schema: {
                model: {
                    id: "id",
                    fields: {
                        id: { type: "string" },
                        kd_Stok: { type: "string" },
                        satuan: { type: "string" },
                        berat: { type: "number" },
                        totalBerat: { type: "number" },

                        qty: { type: "number" },
                        keterangan: { type: "string" },
                        nama_Sales: { type: "string" },
                        nama_Barang: { type: "string" }
                    }
                }
            }
        },
        noRecords: true,
        change: onChange,
    }).data("kendoGrid");
}

function bindGridLimit() {
    
    var customer = $("#Kd_Customer option:selected").text();
    _GridLimit = $("#DPMLimit").kendoGrid({
        columns: [
            { field: "no_inv", title: "No Invoice", width: "180px" },
            { field: "tgl_jatuh_tempo", title: "Tanggal Jatuh Tempo", width: "160px", template: "#= kendo.toString(kendo.parseDate(tgl_jatuh_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "hari_jatuh_tempo", title: "Hari Jatuh Tempo", width: "160px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "lama_hari", title: "Keterlambatan", width: "15px", attributes: { class: "text-right " },hidden:true, editor: stokLabelhari },
            { field: "jml_akhir", title: "Sisa Piutang", width: "100px", format: "{0:#,0}", editor: totalLabelpiutang, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" }
        ],
        toolbar: ["excel"],
        excel: {
            fileName: "LimitPiutang" + customer + ".xlsx"
        },
        excelExport: function (e) {
            var rows = e.workbook.sheets[0].rows;
            var sheet = e.workbook.sheets[0];
            sheet.frozenRows = 2;
            sheet.mergedCells = ["A1:D1"];
            sheet.name = "Limit";

            var myHeaders = [ {
                value: "PIUTANG LEBIH LIMIT / PIUTANG JATUH TEMPO" + customer,
                fontSize: 12,
                textAlign: "center"
            }];
            sheet.rows.splice(0, 0, { cells: myHeaders, type: "header", height: 20});
            for (var ri = 0; ri < rows.length; ri++) {
                var row = rows[ri];

                if (row.type == "header") {
                    for (var ci = 0; ci < row.cells.length; ci++) {
                        var cell = row.cells[ci];

                        // Use jQuery.fn.text to remove the HTML and get only the text

                        // Set the alignment
                        cell.hAlign = "center";

                    }
                }

                if (row.type == "data") {
                    for (var ci = 0; ci < row.cells.length; ci++) {
                        
                        var cell = row.cells[ci];
                       
                        if (ci == 3) {
                            // Set the alignment
                            cell.format = "#,##0";
                            cell.hAlign = "right";
                        }

                        if (ci == 1) {
                            cell.format = "dd MMMM yyyy";
                            cell.hAlign = "left";
                        }

                        if (row.cells[2].value > 40) {
                            
                            row.cells[ci].background = "#FF0000"                            
                        }
                    }
                }

                if (row.type == "group-footer" || row.type == "footer") {
                    for (var ci = 0; ci < row.cells.length; ci++) {
                        var cell = row.cells[ci];
                        if (cell.value) {
                            // Use jQuery.fn.text to remove the HTML and get only the text
                            cell.value = $(cell.value).text();
                            // Set the alignment
                            cell.hAlign = "right";
                        }
                    }
                }
            }
        },
        dataSource: {
            data: dpmlimit,
            schema: {
                model: {
                    id: "id",
                    fields: {
                        no_inv: { type: "string" },
                        tgl_jatuh_tempo: { type: "date" },
                        hari_jatuh_tempo: { type: "int" }

                    }
                }
            },
            aggregate: [
                { field: "jml_akhir", aggregate: "sum" }
               
            ]
        },
        noRecords: true,
        dataBound: setColors
        
        //rowTemplate: '<tr class="red1" data-uid="#= uid #"><td>#: no_inv #</td><td>#:tgl_jatuh_tempo #</td><td>#:hari_jatuh_tempo #</td></tr>'
       
    }).data("kendoGrid");

    var totalku = totallimit();
    if (dpmlimit.length > 0) {
      
        for (i = 0; i < dpmlimit.length; i++) {
            if (dpmlimit[i].lama_hari > 40 || totalku > piu) {
                $("#save").hide();
                $("#addKota").hide();
                $("#requestModal1").show();
                chklimit = true;
                if (totalku > piu) {
                    $("#teksjudul").text("Limit Piutang Tidak Boleh melebihi " + piu.toString());
                    $("#teksjudul").css('color', 'red');
                } else {
                    $("#teksjudul").text("");
                }
            }
        }
      

       // alert('a');
        //$.when(ProsesOtp1()).done(function () {


        //});



    }

}

function totallimit() {
    var tot = 0;
    if (dpmlimit.length > 0) {
        for (i = 0; i < dpmlimit.length; i++) {
            tot += dpmlimit[i].jml_akhir;

        }
    }
    return tot;
}

function setColors(e) {
    var grid = $("#DPMLimit").data("kendoGrid");
    var data = grid.dataSource.data();

    grid.tbody.find('>tr').each(function () {
        var dataItem = grid.dataItem(this);
      

        if (dataItem.lama_hari != null && dataItem.lama_hari > 40) {
            $(this).css('color', 'black');
            $(this).css('background-color', 'red');
        }
    });
}

function onChange(arg) {
    selectRequest = this.selectedKeyNames().join(";");
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
            html: 'You want to Add this data',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            selecteddpmds = [];
            acceptdpmds = [];
            if (isConfirm.value === true) {
                startSpinner('Loading..', 1);

                var array = selectRequest.split(";");
                for (var i = 0; i < dpmds.length; i++) {
                    for (var j = 0; j < array.length; j++) {
                        if (array[j] === dpmds[i].id) {
                            DOds.push(
                                {
                                    "no_booked": dpmds[i].id,
                                    "kode_Barang": dpmds[i].kd_Stok,
                                    "satuan": dpmds[i].kd_satuan,
                                    "qty": dpmds[i].qty,
                                    "vol": dpmds[i].berat,
                                    "tberat": dpmds[i].totalBerat,
                                    "nama_Sales": dpmds[i].nama_Sales,
                                    "nama_Barang": dpmds[i].nama_Barang,
                                    "total": dpmds[i].total,
                                    "harga": dpmds[i].harga
                                }
                            );
                        }
                    }
                }

                $('#GridDO').kendoGrid('destroy').empty();
                bindGrid();
                var grid = $("#GridDO").data("kendoGrid");
                var ds = grid.dataSource;
                ds.sync();
                ds.read();
                $.when(getDPInden()).done(function () {
                    startSpinner('Loading..', 0);
                });
                $("#requestModal").modal('hide');
            }
        });

    }
}

function getDPInden() {
    var idx = "";
    if (DOds != null && DOds.length > 0) {
        for (var i = 0; i < DOds.length; i++) {
            idx += "'" + DOds[i].no_booked + "',";
        }
        idx = idx.slice(0, -1);
    }
    return $.ajax({
        url: urlGetDPInden + "/?id=" + idx,
        success: function (result) {
            $("#dp_inden").val(result[0].dp_inden);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function onjenisDOChanged() {
    if ($("#jenisDO").val() == "CASH") {
        $("#divkirim").hide();
        $("#addDPM").hide();
        $("#divdp").hide();

        //columnGrid = [
        //    { field: "nama_Barang", title: "Nama Barang", width: "60px", editor: barangDropDownEditor },
        //    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
        //    { field: "vol", title: "Berat", width: "15px", editor: beratLabel, attributes: { class: "text-right " } },
        //    { field: "stok", title: "Stok", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
        //    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaLabel },
        //    { field: "diskon", title: "Diskon", hidden: true, width: "20px", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    {
        //        field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
        //        template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
        //    },
        //    //{ field: "keterangan", title: "Keterangan", width: "30px" },
        //    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
        //]
    } else {
        $("#divkirim").show();
        $("#divdp").hide();
        $("#addDPM").hide();
        //columnGrid = [
        //    { field: "nama_Barang", title: "Nama Barang", width: "60px", editor: barangDropDownEditor },
        //    { field: "satuan", title: "Satuan", width: "15px", editor: satuanLabel },
        //    { field: "vol", title: "Berat", width: "15px", editor: beratLabel, attributes: { class: "text-right " } },
        //    { field: "stok", title: "Stok", width: "15px", attributes: { class: "text-right " }, editor: stokLabel, hidden: true },
        //    { field: "qty", title: "Qty", width: "20px", editor: qtyNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: hargaLabel },
        //    { field: "diskon", title: "Diskon", width: "20px", editor: diskonNumeric, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    { field: "total", title: "Total", width: "30px", format: "{0:#,0}", editor: totalLabel, attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        //    {
        //        field: "flagbonus", title: "Bonus", width: "15px", editor: customBoolEditor,
        //        template: '#=dirtyField(data,"flagbonus")#<center><input disabled type="checkbox" #= flagbonus ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
        //    },
        //    //{ field: "keterangan", title: "Keterangan", width: "30px" },
        //    { command: ["edit", "destroy"], title: "Actions", width: "20px" }
        //]
    }
    $("#dp").val("0");
    //if ($('#GridDO').hasClass("k-grid")) {
    //    $('#GridDO').kendoGrid('destroy').empty();
    //}
    //DOds = [];
    //bindGrid();
}

function showOtp() {
    startSpinner('loading..', 1);
    $('#otpModal').modal('show');
    startSpinner('loading..', 0);
}

function showOtpLimit() {
    startSpinner('loading..', 1);
    $('#otpModalLimit').modal('show');
    startSpinner('loading..', 0);
}


function onChangePaket() {
    startSpinner('Loading..', 1);

    $.when(changePaket()).done(function () {
        startSpinner('Loading..', 0);
       

    });
}

function changePaket() {

    var urlLink = urlGetPaketList;
    return $.ajax({
        url: urlLink,
        dataType: "json",
        type: "POST",
        data: {
            no_paket: $("#paket").val()
        },
        success: function (result) {
            DOds = [];
            DOpaket = result;
            var total = 0;
            for (var i = 0; i <= DOpaket.length - 1; i++) {
                var diskon = DOpaket[i].potongan_total;
                if (BranchID == "TOKO" || akses_penjualan == "CASH") {
                    diskon = (DOpaket[i].potongan_total * 1) / (DOpaket[i].qty * 1);
                    total = DOpaket[i].total + DOpaket[i].potongan_total;
                }
                else {
                    total = DOpaket[i].total - (DOpaket[i].potongan_total * DOpaket[i].qty);
                }
                var dropdownlist = $("#nama_Barang").data("kendoDropDownList");
                if (DOpaket[i].kd_stok != "") {
                    var found = GetBarangDetail(DOpaket[i].kd_stok);
                    var index = hargads.findIndex(function (item, i2) {
                        return item.kode_Barang === DOpaket[i].kd_stok;
                    });
                    if (index >= 1) {
                        var brg = found[0].nama_Barang.split("| ", 1);;
                        var stok1 = found[0].stok;

                    } else {
                        var brg = "";
                        var stok1 = 0;
                    }
                   
                    
                }
                DOds.push({
                    "kode_Barang": DOpaket[i].kd_stok,
                    "nama_Barang": brg,
                    "satuan": DOpaket[i].kd_satuan,
                    "stok": stok1,
                    "qty": DOpaket[i].qty,
                    "qty_awal": DOpaket[i].qty_awal,
                    "harga": DOpaket[i].harga,
                    "harga1": DOpaket[i].harga,
                    "diskon": diskon,
                    "disc1": DOpaket[i].disc1,
                    "disc2": DOpaket[i].disc2,
                    "disc3": DOpaket[i].disc3,
                    "disc4": DOpaket[i].disc4,
                    "total": total,
                    "keterangan": DOpaket[i].keterangan,
                    "vol": 0,
                    "jns_paket": "datapaket",
                    "tberat": 0

                });
            }
            
            $('#GridDO').kendoGrid('destroy').empty();
            $("#Grid tbody tr .k-grid-edit").each(function () {
                var currentDataItem = $("#Grid").data("kendoGrid").dataItem($(this).closest("tr"));

                //Check in the current dataItem if the row is editable
                //if (currentDataItem.isEditable == true) {
                    $(this).remove();
               // }


            })

            //Selects all delete buttons
            $("#Grid tbody tr .k-grid-delete").each(function () {
                var currentDataItem = $("#Grid").data("kendoGrid").dataItem($(this).closest("tr"));

                //Check in the current dataItem if the row is deletable
               // if (currentDataItem.isDeletable == true) {
                    $(this).remove();
               // }
            })
            bindGrid1();
            ItemCalc();
        }

    });


    
   
    
    //getSup();
}

function ValidasiOtp() {

    otp = $('#otp').val();
    if (typeof otp != "undefined") {

        if (jnsotp != "otplimit") {
            $.when(getauth()).done(function () {
                if (auth.length > 0) {
                    ds[0].otp = "sukses";
                    
                } else {
                    ds[0].otp = "gagal";
                    
                }

                for (var i = 0; i <= ds.length - 1; i++) {
                    if (ds[i].flagbonus == true) {
                        ds[i].harga = 0;
                        ds[i].harga1 = 0;
                        ds[i].total = 0;
                    }
                    if (ds[i].otp == "gagal") {
                        //ds[i].harga = ds[i].harga1;
                        $("#save").hide();
                        $("#addKota").hide();
                        chklimit = true;
                        swal({
                            type: 'warning',
                            title: 'Error Password',
                            html: 'Password Salah',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d9534f'
                        });

                        return;

                    } else if (ds[i].otp == "sukses") {
                       
                        $("#save").show();
                        $("#addKota").show();
                        startSpinner('loading..', 1);
                        $('#otpModal').modal('hide');
                        startSpinner('loading..', 0);
                       
                        chklimit = false;
                        ds[i].harga = ds[i].harga;
                        ds[i].total = ds[i].harga * ds[i].qty;

                    }

                }


                DOds = ds;

                $('#GridDO').kendoGrid('destroy').empty();
                bindGrid();

                ItemCalc();

                onAddNewRow();


            });
        }
        else {
            $.when(getauth()).done(function () {
                if (auth.length > 0) {
                    //ds[0].otp = "sukses";
                    $("#save").show();
                    $("#addKota").show();
                    $("#requestModal1").hide();
                    chklimit = false;
                } else {
                    //ds[0].otp = "gagal
                    $("#save").hide();
                    $("#addKota").hide();
                    chklimit = true;
                    swal({
                        type: 'warning',
                        title: 'Error Password',
                        html: 'Password Salah',
                        showCancelButton: false,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d9534f'
                    });

                    return;
                }
                $('#GridDO').kendoGrid('destroy').empty();
                bindGrid();
                ItemCalc();
                //onAddNewRow();
            });
        }

    } else {
        return;
    }

    $('#otp').val("");
    $('#otpModal').modal('hide');
}

function ProsesOtpLimit() {

    otp = "";
 

    $("#save").hide();
    $("#addKota").hide();
    chklimit = true;
    jnsotp = "otpharga";

    swal({
        type: 'warning',
        title: 'Error Limit Piutang',
        html: 'Harga Tidak Boleh Lebih Dari Limit Piutang',
        showCancelButton: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            //$("#save").hide();
            //$("#addKota").hide();
         
            //$.when(showOtp()).done(function () {
            //});
        }

    });



}

function ProsesOtp() {

   otp = "";

    $("#save").hide();
    $("#addKota").hide();
    jnsotp = "otpharga";
 
        swal({
            type: 'warning',
            title: 'Error Harga',
            html: 'Harga Tidak Boleh Kurang dari Harga Bottom',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            if (isConfirm.value === true) {
                $.when(showOtp()).done(function () {                 
                });
            }

         });
    
   

}

function ProsesOtp1() {

    otp = "";

    otp = $('#otp').val();

    $("#save").hide();
    $("#addKota").hide();
    jnsotp = "otplimit";

    $.when(showOtp()).done(function () {
        
    });
}


function showRetur() {
    startSpinner('loading..', 1);

    $.when(getDataDORetur()).done(function () {
        if ($('#GridRetur').hasClass("k-grid")) {
            $('#GridRetur').kendoGrid('destroy').empty();
        }
        bindGridRetur();
        $('#returModal').modal('show');
        startSpinner('loading..', 0);
    });

}

function getDataDORetur() {
    var urlLink = urlGetData;
    var filterdata = {
        no_po: "",
        DateFrom: "",
        DateTo: "",
        status_po: "TERKIRIM"
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdrList = result;
            // console.log(JSON.stringify(DOhdrList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function showStok() {
    // e.preventDefault();
    startSpinner('loading..', 1);

    $.when(getDataStokGudang()).done(function () {
        if ($('#GridDetailGudangStock').hasClass("k-grid")) {
            $('#GridDetailGudangStock').kendoGrid('destroy').empty();
        }
        bindGridStok();
        $('#StokModal').modal('show');
        startSpinner('loading..', 0);
    });

}
function getDataStokGudang() {
    //console.log(kd);
    // var gridRowData = $("#GridDO").data("kendoGrid");
    //var selectedItem = gridRowData.dataItem(gridRowData.select());
    // var quote = selectedItem["stok"];
    var blthn = null;
    var urlLink = urlGetDetailStok;
    var filterdata = {
        Kode_Barang: kd,
        blnthn: blthn
    };
    startSpinner('Loading..', 1);
    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,

        success: function (result) {
            //if (_GridDetailStokGudang) {
            //    $('#GridDetailGudangStock').kendoGrid('destroy').empty();
            //}
            StokGudangdetailds = result;
            //  bindGridStok();
            startSpinner('loading..', 0);
            console.log(result);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


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

function fillCboCustomer() {
    $("#Kd_Customer").empty();
    $("#Kd_Customer").append('<option value="" selected disabled>Please select</option>');
    var data = customerds;

    for (var i = 0; i < data.length; i++) {
        $("#Kd_Customer").append('<option value="' + data[i].kd_Customer + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kd_Customer').selectpicker('refresh');
    $('#Kd_Customer').selectpicker('render');
}

function bindGridStok() {

    _GridDetailStokGudang = $("#GridDetailGudangStock").kendoGrid({
        scrollable: true,
        columns: [
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
            { field: "nama_Gudang", title: "Nama Gudang", filterable: true, width: "80px" },
            { field: "bultah", title: "Periode", filterable: true, width: "80px" },
            { field: "nama_Barang", title: "Nama Barang", filterable: true, width: "150px" },
            { field: "awal_qty", title: "Qty Awal", width: "30px" },
            { field: "qty_in", title: "Masuk", width: "50px" },
            { field: "qty_out", title: "Keluar", width: "50px" },
            { field: "akhir_qty", title: "Qty Akhir", width: "30px" },
            { field: "kd_Satuan", title: "Satuan", width: "30px" }


        ],
        selectable: true,
        dataSource: {
            data: StokGudangdetailds,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        nama_Barang: { type: "string" },

                        nama_Gudang: { type: "string" },
                        kd_stok: { type: "string" },
                        kd_Satuan: { type: "string" },
                        //nama_Merk: { type: "string" },
                        //nama_Tipe: { type: "string" },
                        stok_min: { type: "number" },
                        awal_qty: { type: "number" },
                        akhir_qty: { type: "number" },
                        qty_onstok_in: { type: "number" },
                        qty_onstok_out: { type: "number" },
                        akhir_booked: { type: "number" },
                        bultah: { type: "string" },
                        qty_in: { type: "number" },
                        qty_out: { type: "number" }

                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        filterable: true,
        groupable: true,
        sortable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true

    }).data("kendoGrid");


}


function bindGridRetur() {
    _GridRetur = $("#GridRetur").kendoGrid({
        columns: [
            {
                selectable: true,
                width: "50px",
                headerTemplate: ' '
            },
            { field: "statuS_DO", title: "Status" },
            { field: "no_sp", title: "PO Number" },
            { field: "jenis_sp", title: "Jenis DO" },
            { field: "tgl_sp", title: "Tanggal DO", template: "#= kendo.toString(kendo.parseDate(tgl_sp, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "atas_Nama", title: "Customer" },
            { field: "sales", title: "Sales" },
            { field: "subtotal", title: "Subtotal", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "pPn", title: "PPN", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "jmL_RP_TRANS", title: "Grand Total (Rp)", format: "{0:#,0}", attributes: { class: "text-right " } }

        ],
        dataSource: {
            data: DOhdrList,
            schema: {
                model: {
                    id: "no_sp",
                    fields: {
                        no_sp: { type: "string" },
                        statuS_DO: { type: "string" },
                        jenis_sp: { type: "string" },
                        atas_Nama: { type: "string" },
                        tgl_sp: { type: "date" },
                        sales: { type: "string" },
                        subtotal: { type: "number" },
                        pPn: { type: "number" },
                        jmL_RP_TRANS: { type: "number" }
                    }
                }
            },
            pageSize: 10
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true,
        dataBound: function () {
            prepareActionGridSearch();
        },
        scrollable: true

    }).data("kendoGrid");

}

function prepareActionGridSearch() {
    var grid = $("#GridRetur").data("kendoGrid");
    grid.tbody.on("click", ".k-checkbox", onClick);


}
var idCN;
function onClick(e) {
    var grid = $("#GridRetur").data("kendoGrid");
    var row = $(e.target).closest("tr");
    var dataItem = grid.dataItem(row);

    idCN = dataItem.no_sp;
    if (row.hasClass("k-state-selected")) {
        setTimeout(function (e) {
            var grid = $("#GridRetur").data("kendoGrid");
            grid.clearSelection();
        })
    } else {
        grid.clearSelection();
    };
};

function tambahRetur() {
    window.location.href = urlCreate + '?id=' + idCN + '&mode=RETUR';
}

function filterChanged() {
    var ds = $("#GridRetur").data("kendoGrid").dataSource;
    var filter = $('#txtNo').val();

    if (filter) {

        ds.filter([
            {
                logic: "or",
                "filters": [
                    {
                        "field": "no_sp",
                        "operator": "contains",
                        "value": filter
                    }
                ]
            }
        ]);
    }
    else {
        $('#GridRetur').kendoGrid('destroy').empty();
        bindGridRetur();
    }

}

function onPrintDOTokoClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlPrintDOToko;

    $.ajax({
        url: urlLink + "?id=" + idDO,
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

function onPrintClickedDO() {
    startSpinner('loading..', 1);
    var urlLink = urlPrintDOKairos;

    $.ajax({
        url: urlLink + "?id=" + idDO,
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

function onPrintClickedNota() {
    startSpinner('loading..', 1);
    var urlLink = urlPrintNota;

    $.ajax({
        url: urlLink + "?id=" + idDO,
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

function oncheckGudang() {
    window.open(urlCheckGudang, '_blank');
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
                        $.when(getCustomer()).done(function () {
                            fillCboCustomer();
                            $("#customerModal").modal('hide');
                            startSpinner('loading..', 0);
                        });
                        $.when(getPaket()).done(function () {
                            fillCboPaket();
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

function opencustomerModal() {
    $('#NamaCust').val("");
    $('#AlamatCust').val("");
    $('#KotaCust').val("");
    $('#TelpCust').val("");
}

function onPrintClickedFaktur() {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?f=" + idDO, "_blank");
}

function printNotaNew(id) {
    if (typeof id == "undefined") {
        id = idDO;
    }
    var notads = [];
    return $.ajax({
        url: urlPrintNotaNew + "?id=" + id,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            notads = result;
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
                if (notads != null && notads.length > 0) {
                    console.log(notads);
                    cmds += '            ' + notads[0].nama; //text to print
                    cmds += newLine;
                    cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                    //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
                    //cmds += newLine;
                    //cmds += 'Telp:081216327988 WA:081216327988';
                    cmds += ' ' + notads[0].alamat;
                    cmds += newLine;
                    cmds += 'Telp:' + notads[0].telp + ' WA:' + notads[0].wa;
                    cmds += newLine;
                    cmds += '---------------------------------';
                    cmds += newLine;
                    cmds += esc + '!' + '\x18';
                    cmds += '              NOTA'; //text to print
                    cmds += newLine;
                    cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                    cmds += 'TIPE      : ' + notads[0].jenis_sp;
                    cmds += newLine;
                    cmds += 'PELANGGAN : ' + notads[0].atas_Nama;
                    cmds += newLine;
                    cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
                    cmds += newLine;
                    cmds += 'NO        : ' + notads[0].no_sp;
                    cmds += newLine;
                    cmds += 'TGL : ' + notads[0].last_Create_Date;
                    cmds += newLine;
                    cmds += 'GUDANG    : ' + notads[0].branch;
                    cmds += newLine;
                    cmds += 'KETERANGAN: ' + notads[0].keterangan;
                    cmds += newLine;
                    cmds += '---------------------------------';
                    cmds += newLine;
                    for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
                        //+ (notads[0].detailsvm[i].diskon * 1)
                        var diskonsatuan = 0;
                        if (notads[0].detailsvm[i].qty < 0) {
                            diskonsatuan = notads[0].detailsvm[i].qty * -1;
                        }
                        else {
                            diskonsatuan = notads[0].detailsvm[i].qty * 1;
                        }
                        var hargasatuan = (notads[0].detailsvm[i].harga) + ((notads[0].detailsvm[i].diskon * 1) / (diskonsatuan))
                        var harga = 0;
                        if (hargasatuan != "0") {
                            harga = addCommas(hargasatuan.toString());

                        }
                        else {
                            harga = 0;
                        }
                        var totalharga = ((hargasatuan * 1) * (notads[0].detailsvm[i].qty * 1));
                        var totalhargaString;
                        if (totalharga != "0") {
                            if (notads[0].detailsvm[i].qty < 0) {
                                totalhargaString = "-" + addCommas(totalharga.toString());
                            }
                            else {
                                totalhargaString = addCommas(totalharga.toString());
                            }
                        }
                        else {
                            totalhargaString = 0;
                        }

                        //var totalhargaString = addCommas(totalharga.toString());
                        var sLength = 32 - (notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].satuan.toString().length + harga.toString().length + totalhargaString.toString().length + 4);

                        cmds += notads[0].detailsvm[i].nama_Barang;
                        cmds += newLine;
                        cmds += notads[0].detailsvm[i].qty + " " + notads[0].detailsvm[i].satuan + " X " + harga;
                        for (var j = 0; j <= sLength - 1; j++) {
                            cmds += " ";
                        }
                        cmds += totalhargaString;
                        cmds += newLine;

                        if (BranchID == "TOKO" && notads[0].detailsvm[i].diskon > 0) {
                            var diskonsatuan = (notads[0].detailsvm[i].diskon * 1) / (notads[0].detailsvm[i].qty * 1)
                            var sLengthdis = 31 - (notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].diskon.toString().length + diskonsatuan.toString().length + 17);

                            cmds += "Pot Harga : " + notads[0].detailsvm[i].qty + " X " + addCommas(diskonsatuan.toString());
                            for (var j = 0; j <= sLengthdis - 1; j++) {
                                cmds += " ";
                            }

                            if (notads[0].detailsvm[i].qty < 0) {
                                cmds += "(-" + addCommas(notads[0].detailsvm[i].diskon.toString()) + ")";
                            }
                            else {
                                cmds += "(" + addCommas(notads[0].detailsvm[i].diskon.toString()) + ")";
                            }
                            cmds += newLine;
                        }
                    }
                    cmds += '---------------------------------';
                    cmds += newLine;
                    var subtotalLength = 21 - notads[0].subtotal.toString().length;
                    cmds += 'SUB TOTAL';
                    for (var j = 0; j <= subtotalLength - 1; j++) {
                        cmds += " ";
                    }
                    var diskontotal = 0;
                    if (notads[0].total_qty < 0) {
                        diskontotal = (notads[0].discount * -1)
                    }
                    else {
                        diskontotal = (notads[0].discount * 1)
                    }
                    var subtotalDis = ((notads[0].subtotal * 1) + (diskontotal) - (notads[0].biaya * 1));
                    if (subtotalDis < 0) {
                        cmds += "-" + addCommas(subtotalDis.toString());
                    } else {
                        cmds += addCommas(subtotalDis.toString());
                    }
                    cmds += newLine;
                    if (notads[0].discount == null || notads[0].discount == "") {
                        notads[0].discount = "0";
                    }
                    var PPNLength = 21 - notads[0].discount.toString().length;
                    cmds += 'DISKON  ';
                    for (var j = 0; j <= PPNLength - 1; j++) {
                        cmds += " ";
                    }
                    if (notads[0].discount != "0") {
                        if (notads[0].total_qty < 0) {
                            cmds += "(-" + addCommas(notads[0].discount.toString()) + ")";
                        } else {
                            cmds += "(" + addCommas(notads[0].discount.toString()) + ")";
                        }
                    }
                    else {
                        cmds += "(0)";
                    }

                    cmds += newLine;
                    var dpval = notads[0].dp;
                    var DPLength = 27 - notads[0].dp.toString().length;
                    cmds += 'DP ';
                    for (var j = 0; j <= DPLength - 1; j++) {
                        cmds += " ";
                    }
                    if (dpval != "0") {
                        cmds += "(" + addCommas(notads[0].dp.toString()) + ")";
                    }
                    else {
                        cmds += "(0)";
                    }
                    //ongkir
                    cmds += newLine;
                    cmds += 'BIAYA KIRIM';
                    var ongkir = (notads[0].biaya * 1);
                    var ongkirLength = 19 - ongkir.toString().length;

                    for (var j = 0; j <= ongkirLength - 1; j++) {
                        cmds += " ";
                    }
                    cmds += addCommas(ongkir.toString());
                    cmds += newLine;
                    //ongkir
                    cmds += newLine;
                    cmds += 'GRAND TOTAL';
                    var grandval = (notads[0].jmL_RP_TRANS * 1) - (notads[0].dp * 1);
                    var grandtotalLength = 19 - grandval.toString().length;

                    for (var j = 0; j <= grandtotalLength - 1; j++) {
                        cmds += " ";
                    }

                    if (grandval < 0) {
                        cmds += "-" + addCommas(grandval.toString());
                    }
                    else {
                        cmds += addCommas(grandval.toString());
                    }

                    cmds += newLine;

                    cmds += '---------------------------------';
                    cmds += newLine;
                    cmds += '                   Hormat Kami';
                    cmds += newLine;
                    cmds += newLine;
                    cmds += newLine;
                    cmds += '                      ' + notads[0].sales;
                }
                cmds += newLine;
                cmds += '         TERIMA KASIH';
                cmds += newLine;
                cmds += '    Barang Yang Sudah Dibeli';
                cmds += newLine;
                cmds += '  Tidak Bisa Ditukar/Dikembalikan';
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

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


}

function printSJNew(id) {
    var notads = [];
    if (typeof id == "undefined") {
        id = idDO;
    }
    return $.ajax({
        url: urlPrintNotaNew + "?id=" + id,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            notads = result;
            //console.log(JSON.stringify(notads));

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
                cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
                //cmds += newLine;
                //cmds += 'Telp:081216327988 WA:081216327988';
                //cmds += newLine;
                //cmds += '---------------------------------';
                if (notads != null && notads.length > 0) {
                    cmds += ' ' + notads[0].alamat;
                    cmds += newLine;
                    cmds += 'Telp:' + notads[0].telp + ' WA:' + notads[0].wa;
                    cmds += newLine;
                    cmds += '---------------------------------';
                    cmds += newLine;
                    cmds += esc + '!' + '\x18';
                    cmds += '          SURAT JALAN'; //text to print
                    cmds += newLine;
                    cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                    cmds += 'NO        :' + notads[0].no_sp;
                    cmds += newLine;
                    cmds += 'TANGGAL   :' + notads[0].tanggaldesc;
                    cmds += newLine;
                    cmds += 'GUDANG    :' + notads[0].branch;
                    cmds += newLine;
                    cmds += 'PELANGGAN :' + notads[0].atas_Nama;
                    cmds += newLine;
                    cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
                    cmds += newLine;
                    cmds += 'KETERANGAN:' + notads[0].keterangan;
                    cmds += newLine;
                    cmds += '---------------------------------';
                    cmds += newLine;
                    var totBerat = 0;
                    for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {

                        var sLength = 32 - ((notads[0].detailsvm[i].vol).toFixed(2).toString().length + notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].satuan.toString().length + 5);

                        cmds += notads[0].detailsvm[i].nama_Barang;
                        cmds += newLine;
                        cmds += "(" + (notads[0].detailsvm[i].vol).toFixed(2).toString() + " kg)";
                        for (var j = 0; j <= sLength - 1; j++) {
                            cmds += " ";
                        }
                        cmds += notads[0].detailsvm[i].qty.toString() + " " + notads[0].detailsvm[i].satuan.toString();
                        cmds += newLine;
                        totBerat += (notads[0].detailsvm[i].vol * 1);
                    }
                    cmds += '---------------------------------';
                    cmds += newLine;
                    cmds += 'Tot Berat :' + totBerat.toFixed(2).toString() + " kg";
                    cmds += newLine;
                    cmds += '---------------------------------';
                    cmds += newLine;
                    cmds += ' Tanda Terima        Hormat Kami';
                    cmds += newLine;
                    cmds += newLine;
                    cmds += newLine;
                    cmds += '   Customer            ' + notads[0].sales;

                }
                cmds += newLine;
                cmds += newLine;
                cmds += '  Mengetahui           Dikirim';
                cmds += newLine;
                cmds += newLine;
                cmds += newLine;
                cmds += '   Checker           (          )';
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

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


}

function printDONew() {
    var notads = [];
    $.ajax({
        url: urlPrintNotaNew + "?id=" + idDO,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            notads = result;
            // console.log(JSON.stringify(notads));

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
                cmds += '            ' + notads[0].nama; //text to print
                cmds += newLine;
                cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                cmds += '  Jl Kombespol M Duryat 7-9 Sda';
                cmds += newLine;
                cmds += 'Telp:081216327988 WA:081216327988';
                cmds += newLine;
                cmds += '---------------------------------';
                cmds += newLine;
                cmds += esc + '!' + '\x18';
                cmds += '           DO BARANG'; //text to print
                cmds += newLine;
                cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                cmds += 'NO        :' + notads[0].no_sp;
                cmds += newLine;
                cmds += 'TANGGAL   :' + notads[0].last_Create_Date;
                cmds += newLine;
                cmds += 'KASIR     :' + notads[0].sales;
                cmds += newLine;
                cmds += 'GUDANG    :' + notads[0].branch;
                cmds += newLine;
                cmds += 'PELANGGAN :' + notads[0].atas_Nama;
                cmds += newLine;
                cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
                cmds += newLine;
                cmds += 'KETERANGAN:' + notads[0].keterangan;
                cmds += newLine;
                cmds += '---------------------------------';
                cmds += newLine;
                var totBerat = 0;
                var totQty = 0;
                var totItem = 0;

                for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {

                    var sLength = 32 - ((notads[0].detailsvm[i].vol).toFixed(2).toString().length + notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].satuan.toString().length + 5);

                    cmds += notads[0].detailsvm[i].nama_Barang;
                    cmds += newLine;
                    cmds += "(" + (notads[0].detailsvm[i].vol).toFixed(2).toString() + " kg)";
                    for (var j = 0; j <= sLength - 1; j++) {
                        cmds += " ";
                    }
                    cmds += notads[0].detailsvm[i].qty.toString() + " " + notads[0].detailsvm[i].satuan.toString();
                    cmds += newLine;
                    totBerat += ((notads[0].detailsvm[i].vol * 1) * (notads[0].detailsvm[i].qty * 1));
                    totQty += (notads[0].detailsvm[i].qty * 1);

                    totItem += 1;
                }
                cmds += '---------------------------------';
                cmds += newLine;
                cmds += 'Tot Berat :' + totBerat.toFixed(2).toString() + " kg";
                cmds += newLine;
                cmds += 'Tot Qty   :' + totQty.toString();
                cmds += newLine;
                cmds += 'Tot Item  :' + totItem.toString();
                cmds += newLine;
                cmds += '---------------------------------';
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

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


}

function printSJKairosNew(id) {
    var notads = [];
    if (typeof id == "undefined") {
        id = idDO;
    }
    return $.ajax({
        url: urlPrintNotaNew + "?id=" + id,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            notads = result;
            // console.log(JSON.stringify(notads));

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
                cmds += '                                 KAIROS'; //text to print
                cmds += newLine;
                cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                cmds += '                     ' + notads[0].alamat;
                cmds += newLine;
                cmds += '                   Telp: ' + notads[0].telp + ' WA: ' + notads[0].wa;
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += '                              SURAT JALAN'; //text to print
                cmds += newLine;
                cmds += 'NO        :' + notads[0].no_sp;
                cmds += newLine;
                cmds += 'TANGGAL   :' + notads[0].tanggaldesc;
                cmds += newLine;
                cmds += 'GUDANG    :' + notads[0].branch;
                cmds += newLine;
                cmds += 'PELANGGAN :' + notads[0].atas_Nama;
                cmds += newLine;
                cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
                cmds += newLine;
                cmds += 'KETERANGAN:' + notads[0].keterangan;
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += '|NO|          NAMA BARANG                  |       QTY      |   BERAT  |';
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                var totBerat = 0;
                var seqNo;
                var totBerat = 0;
                for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
                    seqNo = i + 1;
                    cmds += "|";
                    if (seqNo.toString().length == 1) {
                        cmds += seqNo + " |";
                    }
                    else {
                        cmds += seqNo + "|";
                    }

                    var nama_barang = "";
                    var spaceBarang;
                    if (notads[0].detailsvm[i].nama_Barang.length <= 38) {
                        nama_barang = notads[0].detailsvm[i].nama_Barang;
                    }
                    else {
                        nama_barang = notads[0].detailsvm[i].nama_Barang.substring(0, 37);
                    }

                    cmds += nama_barang;
                    spaceBarang = 38 - nama_barang.length;
                    for (var x = 0; x <= spaceBarang; x++) {
                        cmds += " ";
                    }
                    cmds += "|";
                    var qtyStr = notads[0].detailsvm[i].qty + " " + notads[0].detailsvm[i].satuan;
                    var spaceQty = 15 - (qtyStr.length + 1);
                    cmds += " ";
                    cmds += qtyStr;

                    for (var x = 0; x <= spaceQty; x++) {
                        cmds += " ";
                    }
                    cmds += "|";

                    var beratStr = (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty).toFixed(2).toString() + "kg";

                    var spaceBerat = 9 - beratStr.length;

                    for (var x = 0; x <= spaceBerat; x++) {
                        cmds += " ";
                    }
                    cmds += beratStr;

                    cmds += "|";
                    totBerat += (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty);
                    cmds += newLine;
                }
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += 'Tot Berat :' + totBerat.toFixed(2).toString() + " kg";
                cmds += newLine;
                cmds += "Barang Yang Sudah Dibeli tidak dapat ditukar atau dikembalikan"
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += ' Hormat Kami        Mengetahui           Pengirim            Customer';
                cmds += newLine;
                cmds += newLine;
                cmds += newLine;
                cmds += '   ' + notads[0].sales + '             Checker              Sopir            (          )';
                cmds += newLine;

                //notads[0].sales

                cpj.printerCommands = cmds;
                //Send print job to printer!
                cpj.sendToClient();
            }

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });


}

function opencustomerPiutangModal() {
    if ($('#gvPiutangCustomer').hasClass("k-grid")) {
        $('#gvPiutangCustomer').kendoGrid('destroy').empty();
    }
    $("#btnSimpanPiutang").hide();
    bindGridPiutang();

}

function bindGridPiutang() {
    $("#gvPiutangCustomer").kendoGrid({
        scrollable: true,
        columns: [
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
            { field: "no_inv", title: "No Invoice" },
            { field: "tgl_jatuh_tempo", title: "Jatuh Tempo", template: "#= kendo.toString(kendo.parseDate(tgl_jatuh_tempo, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "hari_jatuh_tempo", title: "Lama Hari", attributes: { class: "text-right " } },
            { field: "parameter_date", title: "Termin", attributes: { class: "text-right " } },
            { field: "jml_akhir", title: "Sisa Piutang", format: "{0:#,0}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>" },
        ],
        selectable: true,
        dataSource: {
            data: piutangds,
            schema: {
                model: {
                    fields: {
                        no_inv: { type: "string" },
                        tgl_jatuh_tempo: { type: "date" },
                        hari_jatuh_tempo: { type: "number" },
                        parameter_date: { type: "number" },
                        jml_akhir: { type: "number" },
                        total: { type: "number" },
                    }
                }

            },
            aggregate: [
                { field: "jml_akhir", aggregate: "sum" },
            ],
            pageSize: optionsGrid.pageSize
        },
        sortable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        noRecords: true

    }).data("kendoGrid");

}