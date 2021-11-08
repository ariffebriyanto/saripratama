var DOhdr = [];
var DOSaldo = [];
var indends = [];
var rekeningbankds = [];
var detailIndends = [];
var tipetransds = [];
var jnsjurnalds = [];


var optionsGrid = {
    pageSize: 10
};
var _GridDPM;
var _GridDO;
var detailds = [];
$(document).ready(function () {
    startSpinner('Loading..', 1);

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
    $.when(getRekeningBank()).done(function () {
        fillCboRek();
    });
   
       
            $.when(getjnsJurnal()).done(function () {
                $.when(getDataDO()).done(function () {

                    //fillTipeTrans();
                    fillCboJenisJurnal();
                    bindGrid();
                    startSpinner('loading..', 0);

                });
            });
        
   

    $.when(GetBarang()).done(function () {
        // fillBarang();
        $("#barang").kendoDropDownList({
            dataTextField: "nama_Barang",
            dataValueField: "kode_Barang",
            filter: "contains",
            dataSource: listBarang,
            optionLabel: "ALL",
            virtual: {
                valueMapper: function (options) {
                    options.success([options.nama_Barang || 0]);
                }
            },

        }).closest(".k-widget");

        $("#barang").data("kendoDropDownList").list.width("400px");
        startSpinner('loading..', 0);
    });
});

function getRekeningBank() {
    return $.ajax({
        url: urlGetRekBank,
        success: function (result) {
            rekeningbankds = [];
            rekeningbankds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function getDataDO() {
    var urlLink = urlGetData;
    var filterdata = {
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        jurnal: $("#jns_jurnal").val(),
        tipe_trans: $("#tipe_transaksi").val(),
        kd_valuta: $("#valuta").val()
        // status_po: ""
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            console.log(JSON.stringify(DOhdr));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function getSaldoAwal() {
    var urlLink = urlGetSaldo;
    var jsDate = $('#divtanggalfrom').datepicker('getDate');


    if (jsDate !== null) { // if any date selected in datepicker

        jsDate instanceof Date;
        var date1 = jsDate.getDate();
        var bulan1 = jsDate.getMonth();
        var tahun1 = jsDate.getFullYear();
    }



    var filterdata = {
        kd_rekening: $('#Kd_Rekening').val(),
        kd_valuta: "IDR",
        tahun: tahun1.toString(),
        bulan: bulan1.toString()
        // status_po: ""
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOSaldo = result;
            console.log(JSON.stringify(DOSaldo));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
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




function detailInitInden(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });
    startSpinner('loading..', 1);
    $.when(getdetailIndenList(e.data.id)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: detailIndends,
                schema: {
                    model: {
                        id: "no_jur",
                        fields: {
                            no_jur: { type: "string" },
                            nm_buku_besar: { type: "string" },
                            nm_buku_pusat: { type: "string" },
                            saldo_val_debet: { type: "number" },
                            saldo_val_kredit: { type: "number" },
                            saldo_rp_debet: { type: "number" },
                            saldo_rp_kredit: { type: "number" },
                            nama_Barang: { type: "string" },
                            val_ref1: { type: "string" },
                            harga: { type: "number" },
                            keterangan: { type: "number" },
                            status: { type: "string" }
                           
                        }
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "no_jur", title: "No Jurnal", width: "70px" },
                { field: "nm_buku_besar", title: "Nama Barang", width: "120px" },
                { field: "nm_buku_pusat", title: "Satuan", width: "100px" },
                { field: "saldo_val_debet", title: "Saldo val Debet", width: "100px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "saldo_val_kredit", title: "Saldo Val Kredit", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_debet", width: "100px", title: "Saldo Rp Debet", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_kredit", width: "100px", title: "Saldo Rp Kredit", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "nama_Barang", width: "120px", title: "kd obyek", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "val_ref1", width: "80px", title: "Ref1", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "harga", width: "100px", title: "Harga", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "Keterangan", width: "180px" },
                { field: "status", title: "Status", width: "60px" }

                //{
                //    field: "Action", width: "60px",
                //    template: "<center ># if(kd_sales == salesID){#<a class='btn btn-danger btn-sm deleteDataInden' href='javascript:void(0)' data-id='#=id#'><i class='glyphicon glyphicon-trash' aria-hidden='true'></i></a>#}#</center>"
                //}
            ],
        });
        startSpinner('loading..', 0);
    });

}

function getdetailIndenList(id) {
    var urlLink = urlGetDetailInden + "/" + id;

    return $.ajax({
        url: urlLink,
        type: "GET",
        success: function (result) {
            detailIndends = [];
            // console.log(JSON.stringify(result));
            for (var i = 0; i <= result.length - 1; i++) {
                detailIndends.push({
                    "kode_Barang": result[i].kode_Barang,
                    "nama_Barang": result[i].nama_Barang,
                    "satuan": result[i].kd_satuan,
                    "berat": result[i].berat,
                    "qty": result[i].qty,
                    "harga": result[i].harga,
                    "total": result[i].total,
                    "keterangan": result[i].keterangan,
                    "tBerat": result[i].qty * result[i].berat,
                    "status": result[i].status,
                    "qty_Alokasi": result[i].qty_Alokasi,
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

function prepareActionGridInden() {
    $(".deleteDataInden").on("click", function () {
        var id = $(this).data("id");
        swal({
            type: 'warning',
            title: 'Are you sure?',
            html: 'You want delete this data',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            if (isConfirm.value === true) {
                startSpinner('loading..', 1);
                $.ajax({
                    type: "POST",
                    data: { id: id },
                    url: urlDeleteInden,
                    success: function (result) {
                        if (result.success === false) {
                            Swal.fire({
                                type: 'error',
                                title: 'Warning',
                                html: result.message
                            });
                            startSpinner('loading..', 0);
                        } else {
                            $.when(getDataiNDEN()).done(function () {
                                $('#DPMGrid').kendoGrid('destroy').empty();
                                bindGridDPM();
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
    });

    $(".editDataInden").on("click", function () {
        var id = $(this).data("id");
        window.location.href = urlInden + '?id=' + id + '&mode=EDIT';
    });
}

function addnewDO() {
    window.location.href = urlCreate;
}

function searchDO() {
    startSpinner('loading..', 1);

    $.when(getDataDO()).done(function () {
        $('#GridDO').kendoGrid('destroy').empty();
        bindGrid();
        startSpinner('loading..', 0);
    });
}

function oncboFilterChanged() {
    alert('b');
}

function getByDO() {
    startSpinner('loading..', 1);
    var urlLink = urlGetByDO;
    // var urlLink = urlGetByCust
    var filterdata = {

        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        jurnal: $("#jns_jurnal").val(),
        tipe_trans: $("#tipe_transaksi").val(),
        kd_valuta: $("#valuta").val()
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            
            
            DOhdr = result;
            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
            startSpinner('loading..', 0);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getRekening() {
    var urlLink = urlGetRekBank;
    var filterdata = {
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        kd_buku_besar: $("#Kd_Rekening").val(),
        kd_valuta: $("#valuta").val()
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function getByCust() {
    var urlLink = urlGetByCust;
    var filterdata = {
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        kd_buku_besar: $("#Kd_Rekening").val(),
        kd_valuta: $("#valuta").val()
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function getByStok() {
    var urlLink = urlGetByStok;
    var filterdata = {
        no_po: $("#no_sp").val()
    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            DOhdr = result;
            $('#GridDO').kendoGrid('destroy').empty();
            bindGrid();
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function oncboFilterChanged() {
    //alert('b');
}

function bindGrid() {
    _GridDO = $("#GridDO").kendoGrid({

        columns: [

            { field: "no", title: "No" },
            { field: "tipe_trans", title: "Tipe Trans " },
            { field: "no_jur", title: "No. Jurnal" },
            //aggregates: ["count"], footerTemplate: "<div align=right>#= kendo.toString(count, \"n0\") #</div>" },
            { field: "tgl_trans", title: "Tgl. Trans" },
            { field: "tgl_posting", title: "Tgl. Posting" },// template: "#= kendo.toString(kendo.parseDate(tgl_sp, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: true },
            { field: "no_ref1", title: "No. ref1" },// template: "#= kendo.toString(kendo.parseDate(tgl_kirim, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: true },
            { field: "no_ref3", title: "No. Ref3" },
            { field: "kartu", title: "Kartu" },
            { field: "nama", title: "Nama" },
            { field: "keterangan", title: "Keterangan" },
            {
                //field: "saldo_val_debet", title: "Saldo Debet", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>"
                field: "jml_val_trans", title: "Jml. Val", format: "{0:#,0.00}", attributes: { class: "text-right " }
            },
            {
                //field: "saldo_val_kredit", title: "Saldo Kredit", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n0\") #</div>"
                field: "jml_rp_trans", title: "Jml. Rp", format: "{0:#,0.00}", attributes: { class: "text-right " }
            }
        ],
        dataSource: {
            transport: {
                read: function (option) {
                    $.ajax({
                        url: urlGetData,
                        data:
                        {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            DateFrom: $("#tanggalfrom").val(),
                            DateTo: $("#tanggalto").val(),
                            jurnal: $("#jns_jurnal").val(),
                            tipe_trans: $("#tipe_transaksi").val(),
                            kd_valuta: $("#valuta").val(),
                            cek_post: $("#radioposting").val()

                            // barang: $("#barang").val()
                        },
                        dataType: 'json',
                        success: function (result) {
                            option.success(result);
                        },
                        error: function (result) {
                            alert("error");

                        }
                    });
                }
            },
            serverFiltering: true,
            serverSorting: true,
            serverPaging: true,

            schema: {
                data: "data",
                total: "total",
            },
            pageSize: 30,
            aggregate: [
                { field: "saldo_val_debet", aggregate: "sum" },
                { field: "saldo_val_kredit", aggregate: "sum" }

            ],
        },

        change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
            prepareActionGrid();
            //  this.expandRow(this.tbody.find("tr.k-master-row"));
        },
        height: 650,
        scrollable: {
            endless: true
        },

        pageable: {
            refresh: true,
            pageSizes: false,
            numeric: false,
            previousNext: false,
        },

    }).data("kendoGrid");
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

function removeCommas(str) {
    return str.replace(/,/g, '');
}

function removeDots(str) {
    return str.replaceAll(".", "");
}

function addCommas(str) {
    return str.replace(/^0+/, '').replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function prepareActionGrid() {
    var gridData = $("#GridDO").data().kendoGrid.dataSource.view();

    //let total_price_debet = 0;
    //let total_price_kredit = 0;
    //let total_price = 0;
    //gridData.forEach(element => {
    //    total_price_debet = total_price_debet + element.saldo_val_debet;
    //    total_price_kredit = total_price_kredit + element.saldo_val_kredit;
    //});
    //total_price = total_price_debet - total_price_kredit;
    //$('#DebetKredit').val(addCommas(total_price.toString()));
    //var saldoku = $('#saldo_awal').val();
    //var saldoawal = saldoku;
    //var dk = total_price;

    //var akhir = parseInt(saldoawal) + dk;

    //// $('#saldo_awal').val(saldoawal);
    //if (akhir < 0) {
    //    $('#SaldoAkhir').val('-' + addCommas(akhir.toString()));
    //} else {
    //    $('#SaldoAkhir').val(addCommas(akhir.toString()));
    //}


    //$("#totalPrice").data("kendoNumericTextBox").value(total_price);


    //$(".viewData").on("click", function () {
    //    var id = $(this).data("id");
    //    window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    //});
    //$(".editData").on("click", function () {
    //    var id = $(this).data("id");

    //    window.location.href = urlCreate + '?id=' + id + '&mode=EDIT';
    //});
    //$(".printData").on("click", function (event) {
    //    event.stopPropagation();
    //    event.stopImmediatePropagation();
    //    var id = $(this).data("id");
    //    window.open(
    //        serverUrl + "Reports/WebFormRpt.aspx?q=" + id, "_blank");
    //});

    //$(".deleteData").on("click", function () {
    //    var id = $(this).data("id");

    //    var savedata = {
    //        No_sp: id
    //    };

    //    swal({
    //        type: 'warning',
    //        title: 'Are you sure?',
    //        html: 'You want to delete this data',
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d9534f'
    //    }).then(function (isConfirm) {
    //        if (isConfirm.value === true) {
    //            startSpinner('loading..', 1);
    //            $.ajax({
    //                type: "POST",
    //                data: savedata,
    //                url: urlDeleteData,
    //                success: function (result) {
    //                    if (result.success === false) {
    //                        Swal.fire({
    //                            type: 'error',
    //                            title: 'Warning',
    //                            html: result.message
    //                        });
    //                        startSpinner('loading..', 0);
    //                    } else {

    //                        $.when(getDataDO()).done(function () {
    //                            $('#GridDO').kendoGrid('destroy').empty();
    //                            bindGrid();
    //                            startSpinner('loading..', 0);
    //                        });
    //                        startSpinner('loading..', 0);
    //                    }
    //                },
    //                error: function (data) {
    //                    alert('Something Went Wrong');
    //                    startSpinner('loading..', 0);
    //                }
    //            });

    //        }
    //    });
    //});
}

function detailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });
    startSpinner('loading..', 1);
    $.when(getdetailList(e.data.no_jur)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: detailds,
                schema: {
                    model: {
                        id: "no_jur",
                        fields: {
                            no_jur: { type: "string" },
                            nm_buku_besar: { type: "string" },
                            nm_buku_pusat: { type: "string" },
                            saldo_val_debet: { type: "number" },
                            saldo_val_kredit: { type: "number" },
                            saldo_rp_debet: { type: "number" },
                            saldo_rp_kredit: { type: "number" },
                            nama_Barang: { type: "string" },
                            val_ref1: { type: "string" },
                            harga: { type: "number" },
                            keterangan: { type: "number" },
                            status: { type: "string" }
                        }
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "no_jur", title: "No Jurnal", width: "70px" },
                { field: "nm_buku_besar", title: "Nama Barang", width: "120px" },
                { field: "nm_buku_pusat", title: "Satuan", width: "100px" },
                { field: "saldo_val_debet", title: "Saldo val Debet", width: "100px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "saldo_val_kredit", title: "Saldo Val Kredit", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_debet", width: "100px", title: "Saldo Rp Debet", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "saldo_rp_kredit", width: "100px", title: "Saldo Rp Kredit", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "nama_Barang", width: "120px", title: "kd obyek", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "val_ref1", width: "80px", title: "Val Ref1", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "harga", width: "100px", title: "Harga", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "Keterangan", width: "180px" },
                { field: "status", title: "Status", width: "60px" }
            ]
        });
        startSpinner('loading..', 0);
    });

}

function getTipeTrans(id) {
    var filterdata = {
        kd_jurnal: id

    };
    return $.ajax({
        url: urlGetTipe,
        data: filterdata,
        success: function (result) {
            tipetransds = [];
            tipetransds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillTipeTrans() {
    $("#tipe_transaksi").empty();
    $("#tipe_transaksi").append('<option value="" selected disabled>Please select</option>');
    var data = tipetransds;

    for (var i = 0; i < data.length; i++) {
        $("#tipe_transaksi").append('<option value="' + data[i].tipe_trans + '">' + data[i].tipe_desc + '</option>');
    }

    $('#tipe_transaksi').selectpicker('refresh');
    $('#tipe_transaksi').selectpicker('render');
}

function rpostchange() {

    var hasil = $("#rpost").val();
    $("#rnopost").prop("checked", false);
    $("#rpost").prop("checked", true);
    $("#radioposting").val(hasil);
   

}


function rnopostchange() {

    var hasil = $("#rnopost").val();
    $("#rpost").prop("checked", false);
    $("#rnopost").prop("checked", true);
    $("#radioposting").val(hasil);


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
        $("#Kd_Customer").append('<option value="' + data[i].k + '">' + data[i].nama_Customer + '</option>');
    }

    $('#Kd_Customer').selectpicker('refresh');
    $('#Kd_Customer').selectpicker('render');
}

function getjnsJurnal() {
    return $.ajax({
        url: urlGetJenis,
        success: function (result) {
            jnsjurnalds = [];
            jnsjurnalds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function JenisJurnalChange() {
    startSpinner('loading..', 1);
    var id = $("#jns_jurnal").val();
    $.when(getTipeTrans(id)).done(function () {
        fillTipeTrans();
    });
    startSpinner('loading..', 0);
}


function fillCboJenisJurnal() {
    $("#jns_jurnal").empty();
    $("#jns_jurnal").append('<option value="" selected disabled>Please select</option>');
    var data = jnsjurnalds;

    for (var i = 0; i < data.length; i++) {
        $("#jns_jurnal").append('<option value="' + data[i].id_Data + '">' + data[i].desc_Data + '</option>');
    }

    $('#jns_jurnal').selectpicker('refresh');
    $('#jns_jurnal').selectpicker('render');
}

function fillCboRek() {
    $("#Kd_Rekening").empty();
    $("#Kd_Rekening").append('<option value="" selected disabled>Please select</option>');
    var data = rekeningbankds;

    for (var i = 0; i < data.length; i++) {
        $("#Kd_Rekening").append('<option value="' + data[i].kd_buku_besar + '">' + data[i].nm_buku_besar + '</option>');
    }

    $('#Kd_Rekening').selectpicker('refresh');
    $('#Kd_Rekening').selectpicker('render');
}

function getdetailList(id) {
    var urlLink = urlGetDetailData;
    var filterdata = {
        no_jur: id
        
    };
    return $.ajax({
        url: urlLink,
        data: filterdata,
        type: "POST",
        success: function (result) {
            detailds = [];
            detailds = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}