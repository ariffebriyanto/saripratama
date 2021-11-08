var Gudangds = [];
Gudangdetailds = [];
var BarangList = [];
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

    $.when(getData()).done(function () {

        bindGrid();
        startSpinner('loading..', 0);
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
            { field: "no_trans", title: "No Invoice", width: "50px" },
            { field: "tgl_trans", title: "Tanggal Inden", width: "50px", template: "#= kendo.toString(kendo.parseDate(tgl_trans, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "last_created_by", title: "Dibuat Oleh", width: "100px" },
            { field: "no_ref1", title: "No Referensi", width: "50px" },
            { field: "kd_kartu", title: "Customer", width: "120px" },
            { field: "jns_bayar", title: "Jenis Pembayaran", width: "90px" },
            { field: "kd_bank", title: "Bank", width: "90px" },
            { field: "kd_giro", title: "No Giro", width: "90px" },
            
            { field: "jml_bayar", title: "Jml. Bayar", editor: jmltagihanLabel, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "Action", width: "70px", template: "<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center><center style='display:inline;'></center><center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center> <center style='display:inline;'><a class='btn btn-danger btn-sm deleteData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-trash' title='Pembatalan Mutasi' aria-hidden='true'></i></a></center>" }

        ],
        dataSource: {
            data: Gudangds,
            schema: {
                model: {
                    id: "no_trans",
                    fields: {
                        no_trans: { type: "string" },
                        tgl_trans: { type: "date" },
                        last_created_by: { type: "string" },
                        no_ref1: { type: "string" },
                        jml_bayar: { type: "decimal" },
                        jns_bayar: { type: "string" },
                        kd_bank: { type: "string" },
                        kd_giro: { type: "string" },
                        // jml_diskon: { type: "decimal" },
                        // jml_pembulatan: { type: "decimal" },
                        // pendp_lain: { type: "decimal" },
                        kd_kartu: { type: "string" }

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

function jmltagihanLabel(container, options) {
    var input = $('<label id="tagihan" />');
    input.appendTo(container);
}

function potongan(container, options) {
    var input = $('<label id="potongan" />');
    input.appendTo(container);
}

function pembulatan(container, options) {
    var input = $('<label id="pembulatan" />');
    input.appendTo(container);
}

function pendapatanlain(container, options) {
    var input = $('<label id="pendapatanlain" />');
    input.appendTo(container);
}

function subtotal(container, options) {
    var input = $('<label id="subtotal" />');
    input.appendTo(container);
}

function bayarNumeric(container, options) {
    var input = $('<input id="jml_bayar" />');
    input.appendTo(container);

    input.kendoNumericTextBox({
        format: "{0:n2}",
        decimals: 2,
        min: 0,
        change: function (e) {
            var value = this.value();
            var invoice = _GridTerima.dataItem($(e.sender.element).closest("tr"));
            invoice.jml_bayar = value;
            invoice.no_trans = $("#no_inv").val();
            invoice.jml_tagihan = $("#tagihan").text();
            invoice.jml_diskon = $("#potongan").text();
            invoice.jml_pembulatan = $("#pembulatan").text();
            invoice.pendp_lain = $("#pendapatanlain").text();

            var tagihan = invoice.jml_tagihan;
            var potongan = invoice.jml_diskon;
            var pembulatan = invoice.jml_pembulatan;
            var pendapatanlain = invoice.pendp_lain;
            var subtotal = (tagihan - potongan - pembulatan - pendapatanlain) - value;
            $("#subtotal").text(value);
        }
    });
}

function GetPegawai() {
    return $.ajax({
        url: urlPegawai,
        type: "POST",
        success: function (result) {
            BarangList = result;
            //console.log(JSON.stringify(BarangList));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function PegawaiDropdown(container, options) {
    var input = $('<input required id="kode" name="nama">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama",
        dataValueField: "nama",
        dataSource: BarangList,
        optionLabel: "Pilih Pegawai",
        filter: "contains",
        template: "<span data-id='${data.kode}' data-pegawai='${data.nama}'>${data.nama}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Pegawai = e.item.find("span").attr("data-pegawai");
            var pegawai = _GridStokOpname.dataItem($(e.sender.element).closest("tr"));
            pegawai.kd_kartu = id;
            pegawai.nama = Pegawai;
            pegawai.kode = id;
            pegawai.NAMA = Pegawai;
            pegawai.Nama = Pegawai;

        }
    }).appendTo(container);

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
                    id: "no_trans",
                    fields: {
                        no_trans: { type: "string" },
                        prev_no_inv: { type: "string" },
                        jml_tagihan: { type: "decimal" },
                        jml_bayar: { type: "decimal" },
                        jml_diskon: { type: "decimal" },
                        jml_pembulatan: { type: "decimal" },
                        pendp_lain: { type: "decimal" },
                        subtotal: { type: "decimal" }

                    }
                }
            },
            pageSize: 7,
            filter: { field: "no_trans", operator: "eq", value: e.data.no_trans }
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "no_trans", title: "No Trans", width: "50px", hidden: true },
            { field: "prev_no_inv", title: "No Invoice", width: "100px" },
            { field: "jml_tagihan", title: "Jml. Tagihan",  attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "jml_bayar", title: "Jml. Bayar", editor: bayarNumeric, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "jml_diskon", title: "Jml. Potongan", editor: potongan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "jml_pembulatan", title: "Jml. Pembulatan", editor: pembulatan, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "pendp_lain", title: "Pendapatan Lain", editor: pendapatanlain, attributes: { class: "text-right ", 'style': 'background-color: darkseagreen; color:black;' }, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "subtotal", title: "Sub Total", editor: subtotal, width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },


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
        html: 'Pengajuan KasBon Akan dibatalkan ?',
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