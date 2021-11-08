var Gudangds = [];
Gudangdetailds = [];
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

    $.when(getBukuPusat()).done(function () {
        getData();
    });

});

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        id: $("#no_trans").val(),
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
            ////console.log(result);
            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    Gudangdetailds = result;
                    ////console.log(Gudangdetailds);
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

    $(".deleteData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        pembatalan(id);
    });

}
function bindGrid() {
    _GridTerima = $("#GridTerima").kendoGrid({
        columns: [
            { field: "no_jur", title: "Nomer Jurnal", width: "100px" },
            { field: "tgl_trans", title: "Tanggal Trans", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown, hidden: true },
            { field: "tipe_trans", title: "Tipe Trans", width: "120px" },
            { field: "alamat", title: "Alamat", width: "120px" },
            { field: "nama", title: "Kepada", width: "120px" },
            { field: "no_ref1", title: "No Ref", width: "120px" },
            { field: "jml_rp_trans", title: "Jumlah Transaksi", width: "120px", format: "{0:n2}", decimals: 2, min: 0 },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "Action", width: "70px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_jur#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center><center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_jur#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center> <center style='display:inline;'><a class='btn btn-danger btn-sm deleteData' href='javascript:void(0)' data-id='#=no_jur#'><i class='glyphicon glyphicon-trash' title='Pembatalan Mutasi' aria-hidden='true'></i></a></center>" }

        ],
        dataSource: {
            data: Gudangds,
            schema: {
                model: {
                    id: "no_jur",
                    fields: {
                        no_jur: { type: "string" },
                        tgl_trans: { type: "date" },
                        nm_buku_besar: { type: "string" },
                        last_Created_By: { type: "string" },
                        tipe_trans: { type: "string" },
                        alamat: { type: "string" },
                        nama: { type: "string" },
                        no_ref1: { type: "string" },
                        keterangan: { type: "string" },
                        jml_rp_trans: { type: "decimal" },
                        tgl_trans: { type: "date" }
                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
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
                        kd_buku_besar: { type: "string", editable: false },
                        kartu: { type: "string", editable: false },
                        nm_buku_besar: { type: "string" },
                        kd_buku_pusat: { type: "string" },
                        nm_buku_pusat: { type: "string"},
                        keterangan: { type: "string" },
                        saldo_rp_debet: { type: "decimal", editable: false },
                        saldo_val_debet: { type: "decimal" },
                        saldo_rp_kredit: { type: "decimal", editable: false },
                        saldo_val_kredit: { type: "decimal" }

                    }
                }
            },
            pageSize: 7,
            filter: { field: "no_jur", operator: "eq", value: e.data.no_jur }
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "no_seq", title: "No", width: "30px" },
            { field: "no_jur", title: "Nomer Jurnal", width: "100px" },
            { field: "nm_buku_besar", title: "Rekening", width: "120px", editor: RekGlDropDown, hidden: true },
            { field: "nm_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown, hidden: true },
            { field: "kd_buku_besar", title: "Rekening", width: "120px", hidden: true },
            { field: "kd_buku_pusat", title: "Pusat Biaya", width: "120px", editor: BukuPusatDropDown, hidden: true },
            { field: "kartu", title: "Rekening", width: "120px", hidden: true },
            { field: "keterangan", title: "Keterangan", width: "120px" },
            { field: "saldo_rp_debet", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_debet", title: "Val Debet", width: "120px", format: "{0:n2}", decimals: 2, min: 0 },
            { field: "saldo_rp_kredit", title: "Kurs", width: "30px", hidden: true },
            { field: "saldo_val_kredit", title: "Val Kredit", width: "120px", format: "{0:n2}", decimals: 2, min: 0 },

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

function pembatalan(id) {

    var urlLink = urlPembatalan + '?id=' + id;

    swal({
        type: 'warning',
        title: 'Anda Yakin',
        html: 'Mutasi Brg Keluar akan di batalkan, stok di kembalikan?',
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

function getRekGl() {
    return $.ajax({
        url: urlRekGl,
        type: "POST",
        success: function (result) {
            RekGlList = result;
            //console.log(JSON.stringify(RekGlList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
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
            var rekgldp = _GridTerima.dataItem($(e.sender.element).closest("tr"));
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
            var bukupusatdp = _GridTerima.dataItem($(e.sender.element).closest("tr"));
            bukupusatdp.kd_buku_pusat = id;
            bukupusatdp.nm_buku_pusat = BukuPusatdp;
            bukupusatdp.kd_buku_pusat = id;
            bukupusatdp.Nm_Buku_Pusat = BukuPusatdp;
            bukupusatdp.NM_BUKU_PUSAT = BukuPusatdp;

            //var dataItem = e.sender.dataItem();
            //SatuanListParam = [];
            //var found = GetSatuanCode(id);

            //var a = found[0].kd_buku_pusat;
            //var dropdownlist = $("#kd_buku_pusat").data("kendoDropDownList");

            //dropdownlist.select(a);
        }
    }).appendTo(container);

    
}

function GetSatuanCode(code) {
    return BukuPusatList.filter(
        function (BukuPusatList) { return BukuPusatList.kd_buku_pusat === code; }
    );
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
            ////console.log(result);
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