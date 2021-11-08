var Gudangds = [];
Gudangdetailds = [];
var urlAction = "";
var optionsGrid = {
    pageSize: 10
};
var _GridTerima;
$(document).ready(function () {

    $('#divtanggalfrom').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalto').datepicker('setStartDate', minDate);
    });

    $('#divtanggalto').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var minDate = new Date(selected.date.valueOf());
        $('#divtanggalfrom').datepicker('setEndDate', minDate);
    });
    $('#tanggalfrom').val(dateserver);
    $('#tanggalto').val(dateserver);

    getData();
    console.log(Gudangds);
});

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        id: $("#no_sp").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_GridTerima) {
                $('#GridTerima').kendoGrid('destroy').empty();
            }
            Gudangds = result;
            //console.log(JSON.stringify(result));// console.log(result);
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    Gudangdetailds = result;
                    bindGrid();
                    prepareActionGrid();
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


function prepareActionGrid() {
    $(".viewData").on("click", function () {
        var id = $(this).data("id");
        window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
    $(".editData").on("click", function () {
        var id = $(this).data("id");

        window.location.href = urlCreate + '?id=' + id + '&mode=EDIT';
    });
    $(".printData").on("click", function () {
        var id = $(this).data("id");
        printpage(id);

    });
    $(".printSJKairosNew").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        if (BranchID == "TOKO") {
            printSJNew(id);
        } else {
            printSJKairosNew(id);
        }
     

    });
    $(".printNotaSM").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        if (BranchID == "TOKO") {
            printNotaNew(id);
        } else {
            printNotaSM(id);
        }

    });

    $(".deleteData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        pembatalan(id);
    });

    $(".printOngkir").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        printOngkir(id);
    });


}
function bindGrid() {
    _GridTerima = $("#GridTerima").kendoGrid({
        columns: [

            { field: "no_sj", title: "No SJ", width: "60px" },
            { field: "tglSJ", title: "Tanggal SJ", width: "40px", template: "#= kendo.toString(kendo.parseDate(tglSJ, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "no_dpb", title: "No Renc Kirim", width: "50px", hidden: true }, //Last_created_by

            { field: "no_sp", title: "No DO", width: "50px" },
            { field: "nama_customer", title: "Pelanggan", width: "80px" },
            { field: "alamat1", title: "Alamat", width: "150px" },
            { field: "last_created_by", title: "User", width: "40px" }, //Last_created_by
            { field: "inc_ongkir", title: "Ongkir", width: "20px" ,hidden:true}, //Last_created_by
            {
                field: "Action", width: "60px",
                template: "<center style='display:inline;'><a class='btn btn-success btn-sm printSJKairosNew' href='javascript:void(0)' data-id='#=no_sj#'><i class='glyphicon glyphicon-list-alt' title='Surat Jalan' aria-hidden='true'></i></a></center><center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printNotaSM' href='javascript:void(0)' data-id='#=no_sj#'><i class='glyphicon glyphicon-usd' title='Invoice Sementara' aria-hidden='true'></i></a></center><center style='display:inline;'><a class='btn btn-danger btn-sm deleteData' href='javascript:void(0)' data-id='#=no_sj#'><i class='glyphicon glyphicon-trash' title='Pembatalan SJK' aria-hidden='true'></i></a></center>" +
                "#if(inc_ongkir == 'Y'){#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm printOngkir' href='javascript:void(0)' data-id='#=no_sj#'><i class='glyphicon glyphicon-bed' aria-hidden='true' title='Cetak Ongkir'></i></a></center>#}#"
            }

        ],
        dataSource: {
            data: Gudangds,
            schema: {
                model: {
                    id: "no_sj",
                    fields: {
                        no_sj: { type: "string" },
                        no_dpb: { type: "string" },
                        tglSJ: { type: "date" },
                        nama_customer: { type: "string" },
                        no_sp: { type: "string" },
                        alamat1: { type: "string" },
                        inc_ongkir: { type: "string" }

                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        groupable: true,
        sortable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {

            }
        },
        change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
            prepareActionGrid();
            //this.expandRow(this.tbody.find("tr.k-master-row").first());
        }

    }).data("kendoGrid");

}

function detailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    detailRow.find(".detail").kendoGrid({
        dataSource: {
            data: Gudangdetailds,
            schema: {
                model: {
                    id: "no_seq",
                    fields: {
                        no_seq: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        nama_barang: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        qty_balik: { type: "number", editable: false },
                        qty_kirim: { type: "number", editable: false },
                        qty_out: { type: "number", editable: false }

                    }
                }
            },
            pageSize: 7,
            filter: { field: "no_sj", operator: "eq", value: e.data.no_sj }
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "no_seq", title: "No", width: "30px" },
            { field: "nama_barang", title: "Nama Barang", width: "120px" },
            { field: "kd_satuan", title: "Satuan", width: "50px" },
            { field: "qty_kirim", title: "Qty Kirim", width: "70px" },
            { field: "qty_balik", title: "Qty Kembali", width: "70px" },
            { field: "qty_out", title: "Qty Terkirim", width: "70px" }



        ]
    });
}
function onChange(arg) {
    var key = this.selectedKeyNames().join(", ");
    if (key) {
        $('#btnPrint').show();
    }
    else {
        $('#btnPrint').hide();

    }
}


function searchPO() {
    var urlLink = urlGetData;
    startSpinner('loading..', 1);

    var filterdata = {
        no_po: $("#PONo").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        status_po: $("#status").val()
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            //console.log(result);
            $('#GridPO').kendoGrid('destroy').empty();

            pods = result;
            bindGrid();
            startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function oncboFilterChanged() {
    var ds = $("#GridPO").data("kendoGrid").dataSource;
    var status = $('#status').val();

    if (status) {
        ds.filter([
            {
                "filters": [
                    {
                        "field": "status_po",
                        "operator": "eq",
                        "value": status
                    }
                ]
            }
        ]);
    }
    else {
        $('#GridPO').kendoGrid('destroy').empty();
        bindGrid();
    }
}

function addnewPO() {
    window.location.href = urlCreate;

}

function printpage(id) {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + id;
    //wrapperList
    debugger;
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


    return false;

}

function printSJKairosNew(id) {
    //var notads = [];
    //if (typeof id == "undefined") {
    //    id = idDO;
    //}
    //return $.ajax({
    //    // url: urlPrintNotaNew + "?id=" + id,
    //    url: urlPrintNotaSM + "?id=" + id,
    //    type: "POST",
    //    success: function (result) {
    //        startSpinner('loading..', 0);
    //        notads = result;
    //        // console.log(JSON.stringify(notads));

    //        if (jspmWSStatus()) {
    //            //Create a ClientPrintJob
    //            var cpj = new JSPM.ClientPrintJob();
    //            //Set Printer type (Refer to the help, there many of them!)
    //            if ($('#useDefaultPrinter').prop('checked')) {
    //                cpj.clientPrinter = new JSPM.DefaultPrinter();
    //            } else {
    //                cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
    //            }
    //            //Set content to print...
    //            //Create ESP/POS commands for sample label
    //            var esc = '\x1B'; //ESC byte in hex notation
    //            var newLine = '\x0A'; //LF byte in hex notation

    //            var cmds = esc + "@"; //Initializes the printer (ESC @)
    //            cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
    //            cmds += '                           KAIROS'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            cmds += '                     ' + notads[0].alamat;
    //            cmds += newLine;
    //            cmds += '                   Telp: ' + notads[0].telp + ' WA: ' + notads[0].wa;
    //            cmds += newLine;
    //            cmds += '------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += '                       SURAT JALAN KEMBALI'; //text to print
    //            cmds += newLine;
    //            cmds += 'NO        :' + notads[0].no_sp + '          *TANGGAL :' + notads[0].tanggaldesc;
    //            cmds += newLine;

    //            cmds += 'GUDANG    :' + notads[0].branch;
    //            cmds += newLine;
    //            cmds += 'PELANGGAN :' + notads[0].atas_Nama;
    //            cmds += newLine;
    //            cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
    //            cmds += newLine;
    //            cmds += 'KETERANGAN:' + notads[0].keterangan;
    //            cmds += newLine;
    //            cmds += '------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += '|NO|          NAMA BARANG                  |       QTY      |   BERAT  |';
    //            cmds += newLine;
    //            cmds += '------------------------------------------------------------------------';
    //            cmds += newLine;
    //            var totBerat = 0;
    //            var beratStr = 0;
    //            var seqNo;
    //            var totBerat = 0;
    //            for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
    //                seqNo = i + 1;
    //                cmds += "|";
    //                if (seqNo.toString().length == 1) {
    //                    cmds += seqNo + " |";
    //                }
    //                else {
    //                    cmds += seqNo + "|";
    //                }

    //                var nama_barang = "";
    //                var spaceBarang;
    //                if (notads[0].detailsvm[i].nama_Barang.length <= 38) {
    //                    nama_barang = notads[0].detailsvm[i].nama_Barang;
    //                }
    //                else {
    //                    nama_barang = notads[0].detailsvm[i].nama_Barang.substring(0, 37);
    //                }

    //                cmds += nama_barang;
    //                spaceBarang = 38 - nama_barang.length;
    //                for (var x = 0; x <= spaceBarang; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";
    //                var qtyStr = notads[0].detailsvm[i].qty_out + " " + notads[0].detailsvm[i].satuan;
    //                var spaceQty = 15 - (qtyStr.length + 1);
    //                cmds += " ";
    //                cmds += qtyStr;

    //                for (var x = 0; x <= spaceQty; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";

    //                beratStr = (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty_out).toLocaleString('id-ID', { maximumFractionDigits: 2 });

    //                var spaceBerat = 6 - beratStr.length;

    //                for (var x = 0; x <= spaceBerat; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += beratStr.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + " kg";

    //                cmds += "|";
    //                totBerat += (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty_out);
    //                cmds += newLine;
    //            }
    //            cmds += '------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += 'Tot Berat :' + totBerat.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + " kg";
    //            cmds += newLine;
    //            cmds += "Barang Yang Sudah Dibeli tidak dapat ditukar atau dikembalikan"
    //            cmds += newLine;
    //            cmds += '------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += ' Hormat Kami     Mengetahui      Pengirim     Security       Customer';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '   ' + notads[0].sales + '         (      )        (       )    (       )      (        )';
    //            cmds += newLine;

    //            //notads[0].sales

    //            cpj.printerCommands = cmds;
    //            //Send print job to printer!
    //            cpj.sendToClient();
    //        }

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});
   
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=suratjalan&id=" + id, "_blank");

}


function printNotaSM(id) {
    //var notads = [];
    //if (typeof id == "undefined") {
    //    id = idDO;
    //}
    //return $.ajax({
    //    url: urlPrintNotaSM + "?id=" + id,
    //    type: "POST",
    //    success: function (result) {
    //        startSpinner('loading..', 0);
    //        notads = result;
    //        // console.log(JSON.stringify(notads));

    //        if (jspmWSStatus()) {
    //            //Create a ClientPrintJob
    //            var cpj = new JSPM.ClientPrintJob();
    //            //Set Printer type (Refer to the help, there many of them!)
    //            if ($('#useDefaultPrinter').prop('checked')) {
    //                cpj.clientPrinter = new JSPM.DefaultPrinter();
    //            } else {
    //                cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
    //            }
    //            //Set content to print...
    //            //Create ESP/POS commands for sample label
    //            var esc = '\x1B'; //ESC byte in hex notation
    //            var newLine = '\x0A'; //LF byte in hex notation

    //            var cmds = esc + "@"; //Initializes the printer (ESC @)
    //            cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
    //            cmds += '                                 KAIROS'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            cmds += '                     ' + notads[0].alamat;
    //            cmds += newLine;
    //            cmds += '                   Telp: ' + notads[0].telp + ' WA: ' + notads[0].wa;
    //            cmds += newLine;
    //            cmds += '----------------------------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
    //            cmds += '                              FAKTUR TAGIHAN'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            var nospSTR = notads[0].no_sp;
    //            cmds += 'NO        :' + notads[0].no_sp;
    //            var spacetgl = 37 - nospSTR.length;
    //            for (var x = 0; x <= spacetgl; x++) {
    //                cmds += " ";
    //            }
    //            cmds += '*TANGGAL :' + notads[0].tanggaldesc;
    //            cmds += newLine;
    //            cmds += 'GUDANG    :' + notads[0].branch;
    //            cmds += newLine;
    //            cmds += 'PELANGGAN :' + notads[0].atas_Nama;
    //            var namaSTR = notads[0].atas_Nama;
    //            var spacetgl = 37 - namaSTR.length;
    //            for (var x = 0; x <= spacetgl; x++) {
    //                cmds += " ";
    //            }
    //            cmds += '*No TELP :' + notads[0].telp;
    //            cmds += newLine;
    //            cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
    //            cmds += newLine;
    //            cmds += '-----------------------------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += '|NO|          NAMA BARANG                  |   QTY   |   HARGA   |  DISKON  |        TOTAL    |';
    //            cmds += newLine;
    //            cmds += '-----------------------------------------------------------------------------------------------';
    //            cmds += newLine;
    //            var T_diskon = 0;
    //            var seqNo;
    //            var totalhargaString = 0;
    //            var harga = 0;
    //            var subTot = 0;
    //            var grandval = 0;
    //            for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
    //                seqNo = i + 1;
    //                cmds += "|";
    //                if (seqNo.toString().length == 1) {
    //                    cmds += seqNo + " |";
    //                }
    //                else {
    //                    cmds += seqNo + "|";
    //                }

    //                var nama_barang = "";
    //                var spaceBarang;
    //                if (notads[0].detailsvm[i].nama_Barang.length <= 38) {
    //                    nama_barang = notads[0].detailsvm[i].nama_Barang;
    //                }
    //                else {
    //                    nama_barang = notads[0].detailsvm[i].nama_Barang.substring(0, 37);
    //                }

    //                cmds += nama_barang;
    //                spaceBarang = 38 - nama_barang.length;
    //                for (var x = 0; x <= spaceBarang; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";
    //                var qtyStr = notads[0].detailsvm[i].qty_out + " " + notads[0].detailsvm[i].satuan;
    //                var spaceQty = 8 - (qtyStr.length + 1);
    //                cmds += " ";
    //                cmds += qtyStr;

    //                for (var x = 0; x <= spaceQty; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";
    //                //
    //                var diskonsatuan = (notads[0].detailsvm[i].diskon * 1) / (notads[0].detailsvm[i].qty_out * 1)
    //                var hargasatuan = (notads[0].detailsvm[i].harga)

    //                if (hargasatuan != "0") {
    //                    harga = hargasatuan.toLocaleString('id-ID', { maximumFractionDigits: 2 });

    //                }
    //                else {
    //                    harga = 0;
    //                }
    //                var spaceHrg = 10 - harga.length;
    //                var totalharga = ((hargasatuan * 1) * (notads[0].detailsvm[i].qty_out * 1) - (notads[0].detailsvm[i].diskon * 1) * (notads[0].detailsvm[i].qty_out * 1));

    //                if (totalharga != "0") {
    //                    if (notads[0].detailsvm[i].qty_out < 0) {
    //                        totalhargaString = "-" + totalharga.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                    }
    //                    else {
    //                        totalhargaString = totalharga.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                    }
    //                }
    //                else {
    //                    totalhargaString = 0;
    //                }

    //                //
    //                //var beratStr = (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty).toFixed(2).toString() + "kg";

    //                var spaceTotHrg = 8 - totalhargaString.length;


    //                cmds += harga.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                for (var x = 0; x <= spaceHrg; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";
    //                var strDisc = notads[0].detailsvm[i].diskon;
    //                var strdisc1 = strDisc.toLocaleString('id-ID', { maximumFractionDigits: 2 });

    //                cmds += " ";
    //                cmds += strdisc1;

    //                var spaceDisc = 8 - (strdisc1.length);
    //                for (var x = 0; x <= spaceDisc; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";

    //                var RpStr = notads[0].detailsvm[i].qty_out + " " + notads[0].detailsvm[i].satuan;
    //                var spaceRp = 16 - (totalhargaString.length + 1);
    //                cmds += " ";
    //                cmds += totalhargaString.toLocaleString('id-ID', { maximumFractionDigits: 2 });

    //                for (var x = 0; x <= spaceRp; x++) {
    //                    cmds += " ";
    //                }
    //                cmds += "|";
    //                subTot += ((hargasatuan * 1) * (notads[0].detailsvm[i].qty_out * 1) - (notads[0].detailsvm[i].diskon * 1) * (notads[0].detailsvm[i].qty_out * 1));
    //                T_diskon += notads[0].detailsvm[i].diskon;
    //                // totBerat += (notads[0].detailsvm[i].vol * notads[0].detailsvm[i].qty);
    //                cmds += newLine;
    //            }

    //            cmds += '-----------------------------------------------------------------------------------------------';
    //            var grandval = subTot + (notads[0].biaya * 1) - (notads[0].dp * 1);
    //            var grandtotalLength = 19 - grandval.toString().length;
    //            var SubTotal = (grandval * 1) - (notads[0].biaya * 1);
    //            var ongkir = notads[0].biaya * 1
    //            cmds += newLine;
    //            cmds += 'Sub Total     :                                                        ' + T_diskon.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + "         " + subTot.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            cmds += newLine;
    //            if (notads[0].inc_ongkir == 'Y')
    //            {
    //                //cmds += 'Ongkos Kirim  : ' + ongkir.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                //cmds += newLine;
    //                var total_new = grandval - ongkir;
    //                if (grandval < 0) {
                       
    //                    cmds += 'Grand Total   : -' + total_new.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                }
    //                else {
    //                    cmds += 'Grand Total   : ' + total_new.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                }
    //            } else
    //            {
    //                cmds += 'Ongkos Kirim  : ' + ongkir.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                cmds += newLine;
    //                if (grandval < 0) {
    //                    cmds += 'Grand Total   : -' + grandval.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                }
    //                else {
    //                    cmds += 'Grand Total   : ' + grandval.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                }
    //            }
                
                

    //            cmds += newLine;
    //            cmds += "Barang Yang Sudah Dibeli tidak dapat ditukar atau dikembalikan"
    //            cmds += newLine;
    //            cmds += '-----------------------------------------------------------------------------------------------';
    //            cmds += newLine;
    //            cmds += "-BCA : 018-2087-589 a/n ACHAZIO KYNAN       -BRI: 0086-0100-0196-562 a/n NATALIA"
    //            cmds += newLine;
    //            cmds += "-MANDIRI : 141-00-1159 0007 a/n NATALIA    -BNI: 1199-8118-88 a/n NATALIA"
    //            cmds += newLine;
    //            cmds += ' Hormat Kami         Mengetahui                              Customer';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '   ' + notads[0].sales + '            (         )                           (             )';
    //            cmds += newLine;

    //            //notads[0].sales

    //            cpj.printerCommands = cmds;
    //            //Send print job to printer!
    //            cpj.sendToClient();
    //        }

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});

    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=invoice&id=" + id, "_blank");
}

function printOngkir(id) {
    var notads = [];
    if (typeof id == "undefined") {
        id = idDO;
    }
    return $.ajax({
        // url: urlPrintNotaNew + "?id=" + id,
        url: urlPrintNotaSM + "?id=" + id,
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
                cmds += '                           KAIROS'; //text to print
                cmds += newLine;
                cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
                cmds += '                     ' + notads[0].alamat;
                cmds += newLine;
                cmds += '                   Telp: ' + notads[0].telp + ' WA: ' + notads[0].wa;
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += '                       TAGIHAN JASA KIRIM'; //text to print
                cmds += newLine;
                cmds += 'NO        :' + notads[0].no_sp + '          *TANGGAL :' + notads[0].tanggaldesc;
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
                cmds += '|NO|          NAMA BARANG                  |    QTY    |        BIAYA  |';
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                var totBerat = 0;
                var beratStr = 0;
                var seqNo;
                var totBerat = 0;
                //for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
                    seqNo = 1;
                    cmds += "|";
                    if (seqNo.toString().length == 1) {
                        cmds += seqNo + " |";
                    }
                    else {
                        cmds += seqNo + "|";
                    }

                    var nama_barang = "JASA KIRIM";
                    var spaceBarang;
                    //if (notads[0].detailsvm[0].nama_Barang.length <= 38) {
                    //    nama_barang = notads[0].detailsvm[i].nama_Barang;
                    //}
                    //else {
                    //    nama_barang = notads[0].detailsvm[0].nama_Barang.substring(0, 37);
                    //}

                    cmds += nama_barang;
                    spaceBarang = 38 - nama_barang.length;
                    for (var x = 0; x <= spaceBarang; x++) {
                        cmds += " ";
                    }
                    cmds += "|";
                    var qtyStr = "1";
                    var spaceQty = 10 - (qtyStr.length + 1);
                    cmds += " ";
                    cmds += qtyStr;

                    for (var x = 0; x <= spaceQty; x++) {
                        cmds += " ";
                    }
                    cmds += "|";

                    //beratStr = (notads[0].detailsvm[i].vol * notads[0].detailsvm[0].qty_out).toLocaleString('id-ID', { maximumFractionDigits: 2 });
                    beratStr = notads[0].biaya.toLocaleString('id-ID', { maximumFractionDigits: 2 });

                    var spaceBerat = 14 - beratStr.length;

                    for (var x = 0; x <= spaceBerat; x++) {
                        cmds += " ";
                    }
                    cmds += beratStr.toLocaleString('id-ID', { maximumFractionDigits: 2 });

                    cmds += "|";
                    //totBerat += (notads[0].detailsvm[0].vol * notads[0].detailsvm[0].qty_out);
                    cmds += newLine;
                //}
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                //cmds += 'Tot Berat :' + totBerat.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + " kg";
                //cmds += newLine;
                cmds += "Barang Yang Sudah Dibeli tidak dapat ditukar atau dikembalikan"
                cmds += newLine;
                cmds += '------------------------------------------------------------------------';
                cmds += newLine;
                cmds += ' Hormat Kami     Mengetahui      Pengirim           Customer';
                cmds += newLine;
                cmds += newLine;
                cmds += newLine;
                cmds += '   ' + notads[0].sales + '         (      )           (       )      (        )';
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

function pembatalan(id) {

    var urlLink = urlPembatalan + '?id=' + id;

    swal({
        type: 'warning',
        title: 'Anda Yakin',
        html: 'Data di DELETE dari SO, kembali dari AWAL??',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);

            $.ajax({
                url: urlLink,
                type: "POST",
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        $.when(getData()).done(function () {
                            bindGrid();
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
    return false;

}

function printNotaNew(id) {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=invoice&id=" + id, "_blank");

    //if (typeof id == "undefined") {
    //    id = idDO;
    //}
    //var notads = [];
    //return $.ajax({
    //    //url: urlPrintNotaNew + "?id=" + id, urlPrintNotaSM
    //     url: urlPrintNotaSM + "?id=" + id, //urlPrintNotaSM
    //    type: "POST",
    //    success: function (result) {
    //        startSpinner('loading..', 0);
    //        notads = result;
    //        if (jspmWSStatus()) {
    //            //Create a ClientPrintJob
    //            var cpj = new JSPM.ClientPrintJob();
    //            //Set Printer type (Refer to the help, there many of them!)
    //            if ($('#useDefaultPrinter').prop('checked')) {
    //                cpj.clientPrinter = new JSPM.DefaultPrinter();
    //            } else {
    //                cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
    //            }
    //            //Set content to print...
    //            //Create ESP/POS commands for sample label
    //            var esc = '\x1B'; //ESC byte in hex notation
    //            var newLine = '\x0A'; //LF byte in hex notation

    //            var cmds = esc + "@"; //Initializes the printer (ESC @)
    //            cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
    //            cmds += '            TOKO 88'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
    //            //cmds += newLine;
    //            //cmds += 'Telp:081216327988 WA:081216327988';
    //            cmds += ' ' + notads[0].alamat;
    //            cmds += newLine;
    //            cmds += 'Telp:' + notads[0].telp + ' WA:' + notads[0].wa;
    //            cmds += newLine;
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x18';
    //            cmds += '              NOTA'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            cmds += 'TIPE      : ' + notads[0].jenis_sp;
    //            cmds += newLine;
    //            cmds += 'PELANGGAN : ' + notads[0].atas_Nama;
    //            cmds += newLine;
    //            cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
    //            cmds += newLine;
    //            cmds += 'NO        : ' + notads[0].no_sp;
    //            cmds += newLine;
    //            cmds += 'TGL : ' + notads[0].last_Create_Date;
    //            cmds += newLine;
    //            cmds += 'GUDANG    : ' + notads[0].branch;
    //            cmds += newLine;
    //            cmds += 'KETERANGAN: ' + notads[0].keterangan;
    //            cmds += newLine;
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //             for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {
    //            //for (var i = 0; i <= notads[0].detailsvm[i].length - 1; i++) {
    //                //+ (notads[0].detailsvm[i].diskon * 1)
    //                var diskonsatuan = 0;
    //                if (notads[0].detailsvm[i].qty < 0) {
    //                    diskonsatuan = notads[0].detailsvm[i].qty * -1;
    //                }
    //                else {
    //                    diskonsatuan = notads[0].detailsvm[i].qty * 1;
    //                }
    //                var hargasatuan = (notads[0].detailsvm[i].harga) + ((notads[0].detailsvm[i].diskon * 1) / (diskonsatuan))
    //                var harga = 0;
    //                if (hargasatuan != "0") {
    //                    harga = hargasatuan.toLocaleString('id-ID', { maximumFractionDigits: 2 });

    //                }
    //                else {
    //                    harga = 0;
    //                }
    //                var totalharga = ((hargasatuan * 1) * (notads[0].detailsvm[i].qty * 1));
    //                var totalhargaString;
    //                if (totalharga != "0") {
    //                    if (notads[0].detailsvm[i].qty < 0) {
    //                        totalhargaString = "-" + totalharga.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                    }
    //                    else {
    //                        totalhargaString = totalharga.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                    }
    //                }
    //                else {
    //                    totalhargaString = 0;
    //                }

    //                //var totalhargaString = addCommas(totalharga.toString());
    //                var sLength = 32 - (notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].satuan.toString().length + harga.toString().length + totalhargaString.toString().length + 4);

    //                cmds += notads[0].detailsvm[i].nama_Barang;
    //                cmds += newLine;
    //                cmds += notads[0].detailsvm[i].qty + " " + notads[0].detailsvm[i].satuan + " X " + harga;
    //                for (var j = 0; j <= sLength - 1; j++) {
    //                    cmds += " ";
    //                }
    //                cmds += totalhargaString;
    //                cmds += newLine;

    //                if (BranchID == "TOKO" && notads[0].detailsvm[i].diskon > 0) {
    //                    var diskonsatuan = (notads[0].detailsvm[i].diskon * 1) / (notads[0].detailsvm[i].qty * 1)
    //                    var sLengthdis = 31 - (notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].diskon.toString().length + diskonsatuan.toString().length + 17);

    //                    cmds += "Pot Harga : " + notads[0].detailsvm[i].qty + " X " + diskonsatuan.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //                    for (var j = 0; j <= sLengthdis - 1; j++) {
    //                        cmds += " ";
    //                    }

    //                    if (notads[0].detailsvm[i].qty < 0) {
    //                        cmds += "(-" + notads[0].detailsvm[i].diskon.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + ")";
    //                    }
    //                    else {
    //                        cmds += "(" + notads[0].detailsvm[i].diskon.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + ")";
    //                    }
    //                    cmds += newLine;
    //                }
    //            }
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            var subtotalLength = 21 - notads[0].subtotal.toString().length;
    //            cmds += 'SUB TOTAL';
    //            for (var j = 0; j <= subtotalLength - 1; j++) {
    //                cmds += " ";
    //            }
    //            var diskontotal = 0;
    //            if (notads[0].total_qty < 0) {
    //                diskontotal = (notads[0].discount * -1)
    //            }
    //            else {
    //                diskontotal = (notads[0].discount * 1)
    //            }
    //            var subtotalDis = ((notads[0].subtotal * 1) + (diskontotal) - (notads[0].biaya * 1));
    //            if (subtotalDis < 0) {
    //                cmds += "-" + subtotalDis.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            } else {
    //                cmds += subtotalDis.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            }
    //            cmds += newLine;
    //            if (notads[0].discount == null || notads[0].discount == "") {
    //                notads[0].discount = "0";
    //            }
    //            var PPNLength = 21 - notads[0].discount.toString().length;
    //            cmds += 'DISKON  ';
    //            for (var j = 0; j <= PPNLength - 1; j++) {
    //                cmds += " ";
    //            }
    //            if (notads[0].discount != "0") {
    //                if (notads[0].total_qty < 0) {
    //                    cmds += "(-" + notads[0].discount.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + ")";
    //                } else {
    //                    cmds += "(" + notads[0].discount.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + ")";
    //                }
    //            }
    //            else {
    //                cmds += "(0)";
    //            }

    //            cmds += newLine;
    //            var dpval = notads[0].dp;
    //            var DPLength = 27 - notads[0].dp.toString().length;
    //            cmds += 'DP ';
    //            for (var j = 0; j <= DPLength - 1; j++) {
    //                cmds += " ";
    //            }
    //            if (dpval != "0") {
    //                cmds += "(" + notads[0].dp.toLocaleString('id-ID', { maximumFractionDigits: 2 }) + ")";
    //            }
    //            else {
    //                cmds += "(0)";
    //            }
    //            //ongkir
    //            cmds += newLine;
    //            cmds += 'BIAYA KIRIM';
    //            var ongkir = (notads[0].biaya * 1);
    //            var ongkirLength = 19 - ongkir.toString().length;

    //            for (var j = 0; j <= ongkirLength - 1; j++) {
    //                cmds += " ";
    //            }
    //            cmds += ongkir.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            cmds += newLine;
    //            //ongkir
    //            cmds += newLine;
    //            cmds += 'GRAND TOTAL';
    //            var grandval = (notads[0].jmL_RP_TRANS * 1) - (notads[0].dp * 1);
    //            var grandtotalLength = 19 - grandval.toString().length;

    //            for (var j = 0; j <= grandtotalLength - 1; j++) {
    //                cmds += " ";
    //            }

    //            if (grandval < 0) {
    //                cmds += "-" + grandval.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            }
    //            else {
    //                cmds += grandval.toLocaleString('id-ID', { maximumFractionDigits: 2 });
    //            }

    //            cmds += newLine;

    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            cmds += '                   Hormat Kami';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '                      ' + notads[0].sales;
    //            cmds += newLine;
    //            cmds += '         TERIMA KASIH';
    //            cmds += newLine;
    //            cmds += '    Barang Yang Sudah Dibeli';
    //            cmds += newLine;
    //            cmds += '  Tidak Bisa Ditukar/Dikembalikan';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;


    //            cpj.printerCommands = cmds;
    //            //Send print job to printer!
    //            cpj.sendToClient();
    //        }

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});


}

function printSJNew(id) {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=suratjalan&id=" + id, "_blank");
    //var notads = [];
    //if (typeof id == "undefined") {
    //    id = idDO;
    //}
    //return $.ajax({
    //    url: urlPrintNotaSM + "?id=" + id,
    //    type: "POST",
    //    success: function (result) {
    //        startSpinner('loading..', 0);
    //        notads = result;
    //        console.log(JSON.stringify(notads));

    //        if (jspmWSStatus()) {
    //            //Create a ClientPrintJob
    //            var cpj = new JSPM.ClientPrintJob();
    //            //Set Printer type (Refer to the help, there many of them!)
    //            if ($('#useDefaultPrinter').prop('checked')) {
    //                cpj.clientPrinter = new JSPM.DefaultPrinter();
    //            } else {
    //                cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
    //            }
    //            //Set content to print...
    //            //Create ESP/POS commands for sample label
    //            var esc = '\x1B'; //ESC byte in hex notation
    //            var newLine = '\x0A'; //LF byte in hex notation

    //            var cmds = esc + "@"; //Initializes the printer (ESC @)
    //            cmds += esc + '!' + '\x18'; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
    //            cmds += '            TOKO 88'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            //cmds += '  Jl Kombespol M Duryat 7-9 Sda';
    //            //cmds += newLine;
    //            //cmds += 'Telp:081216327988 WA:081216327988';
    //            //cmds += newLine;
    //            //cmds += '---------------------------------';
    //            cmds += ' ' + notads[0].alamat;
    //            cmds += newLine;
    //            cmds += 'Telp:' + notads[0].telp + ' WA:' + notads[0].wa;
    //            cmds += newLine;
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x18';
    //            cmds += '          SURAT JALAN'; //text to print
    //            cmds += newLine;
    //            cmds += esc + '!' + '\x01'; //Character font A selected (ESC ! 0)
    //            cmds += 'NO        :' + notads[0].no_sp;
    //            cmds += newLine;
    //            cmds += 'TANGGAL   :' + notads[0].tanggaldesc;
    //            cmds += newLine;
    //            cmds += 'GUDANG    :' + notads[0].branch;
    //            cmds += newLine;
    //            cmds += 'PELANGGAN :' + notads[0].atas_Nama;
    //            cmds += newLine;
    //            cmds += 'ALAMAT    :' + notads[0].almt_pnrm;
    //            cmds += newLine;
    //            cmds += 'KETERANGAN:' + notads[0].keterangan;
    //            cmds += newLine;
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            var totBerat = 0;
    //            for (var i = 0; i <= notads[0].detailsvm.length - 1; i++) {

    //                var sLength = 32 - ((notads[0].detailsvm[i].vol).toFixed(2).toString().length + notads[0].detailsvm[i].qty.toString().length + notads[0].detailsvm[i].satuan.toString().length + 5);

    //                cmds += notads[0].detailsvm[i].nama_Barang;
    //                cmds += newLine;
    //                cmds += "(" + (notads[0].detailsvm[i].vol).toFixed(2).toString() + " kg)";
    //                for (var j = 0; j <= sLength - 1; j++) {
    //                    cmds += " ";
    //                }
    //                cmds += notads[0].detailsvm[i].qty.toString() + " " + notads[0].detailsvm[i].satuan.toString();
    //                cmds += newLine;
    //                totBerat += (notads[0].detailsvm[i].vol * 1);
    //            }
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            cmds += 'Tot Berat :' + totBerat.toFixed(2).toString() + " kg";
    //            cmds += newLine;
    //            cmds += '---------------------------------';
    //            cmds += newLine;
    //            cmds += ' Tanda Terima        Hormat Kami';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '   Customer            ' + notads[0].sales;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '  Mengetahui           Dikirim';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += '   Checker           (          )';
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;
    //            cmds += newLine;

    //            cpj.printerCommands = cmds;
    //            //Send print job to printer!
    //            cpj.sendToClient();
    //        }

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});


}


function onPrintClickedFaktur(id) {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?f=" + id, "_blank");
}
