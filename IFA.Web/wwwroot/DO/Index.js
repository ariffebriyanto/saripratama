var DOhdr = [];
var indends = [];
var detailIndends = [];
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

    $.when(getCustomer()).done(function () {
    $.when(getDataDO()).done(function () {
        $.when(getDataiNDEN()).done(function () {
            fillCboCustomer();
            bindGrid();
            startSpinner('loading..', 0);
        });
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

function getDataDO() {
    var urlLink = urlGetData;
    var filterdata = {
        no_po: "",
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
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
function getDataiNDEN() {
    var urlLink = urlGetDataInden;
    return $.ajax({
        url: urlLink + "?status=Entry,Alokasi",
        type: "GET",
        success: function (result) {
            indends = result;
            if (_GridDPM != undefined) {
                $("#DPMGrid").kendoGrid('destroy').empty();
            }
            //console.log(JSON.stringify(indends));
            bindGridDPM();
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
            //{ field: "status", title: "Status", width: "50px" },
            { field: "tgl_inden", title: "Tanggal Inden", width: "50px", template: "#= kendo.toString(kendo.parseDate(tgl_inden, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "nama_Customer", title: "Customer", width: "150px" },
            //{ field: "nama_Barang", title: "Nama Barang", width: "120px" },
            //{ field: "kd_satuan", title: "Satuan", width: "40px" },
            { field: "nama_Sales", title: "Nama Sales", width: "80px" },
            { field: "qty", title: "Total Qty", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },

            //{ field: "berat", width: "50px", title: "Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "totalBeratInden", width: "60px", title: "Total Berat Inden", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "total", width: "60px", title: "Grand Total", format: "{0:#,0}", attributes: { class: "text-right " } },
            { field: "dp_inden", width: "60px", title: "DP Inden", format: "{0:#,0}", attributes: { class: "text-right " } },
            {
                field: "Action", width: "60px",
                template: "<center ># if(kd_sales == salesID){#<a class='btn btn-info btn-sm editDataInden' href='javascript:void(0)' data-id='#=idDisplay#'><i class='glyphicon glyphicon-pencil' aria-hidden='true'></i></a> &nbsp&nbsp <a class='btn btn-danger btn-sm deleteDataInden' href='javascript:void(0)' data-id='#=idDisplay#'><i class='glyphicon glyphicon-trash' aria-hidden='true'></i></a>#}#</center>"
            }
        ],
        dataSource: {
            data: indends,
            schema: {
                model: {
                    id: "idDisplay",
                    fields: {
                        nama_Customer: { type: "string" },
                        idDisplay: { type: "string" },
                        kd_Stok: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number" },
                        keterangan: { type: "string" },
                        nama_Barang: { type: "string" },
                        nama_Sales: { type: "string" },
                        berat: { type: "number" },
                        totalBerat: { type: "number" },
                        totalBeratInden: { type: "number" },
                        dp_inden: { type: "number" },
                        status: { type: "string" },
                        kd_sales: { type: "string" },
                        tgl_inden: { type: "date" }
                    }
                }
            }
        },
        dataBound: function () {
            prepareActionGridInden();
        },
        noRecords: true,
        detailTemplate: kendo.template($("#templateInden").html()),
        detailInit: detailInitInden,
    }).data("kendoGrid");
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
                        id: "id",
                        fields: {
                            nama_Customer: { type: "string" },
                            id: { type: "string" },
                            idDisplay: { type: "string" },
                            kd_Stok: { type: "string" },
                            satuan: { type: "string" },
                            qty: { type: "number" },
                            keterangan: { type: "string" },
                            nama_Barang: { type: "string" },
                            nama_Sales: { type: "string" },
                            berat: { type: "number" },
                            tBerat: { type: "number" },
                            totalBeratInden: { type: "number" },
                            status: { type: "string" },
                            kd_sales: { type: "string" },
                            tgl_inden: { type: "date" }
                        }
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "status", title: "Status", width: "50px" },
                { field: "nama_Barang", title: "Nama Barang", width: "120px" },
                { field: "satuan", title: "Satuan", width: "40px" },
                { field: "qty", title: "Qty Request", width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "qty_Alokasi", title: "Qty Alokasi", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "berat", width: "50px", title: "Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "tBerat", width: "50px", title: "Total Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "harga", width: "50px", title: "Harga", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "total", width: "50px", title: "Total", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "Keterangan", width: "180px" },

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
        no_po: $("#no_sp").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        kd_cust: $("#Kd_Customer").val()
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
function getByCust() {
    var urlLink = urlGetByCust;
    var filterdata = {
        no_po: $("#no_sp").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        kd_cust: $("#Kd_Customer").val()
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
           
            { field: "STATUS_DO", title: "Status" },
            { field: "nama", title: "Cabang" },
            { field: "No_sp", title: "DO Number" }, //aggregates: ["count"], footerTemplate: "<div align=right>#= kendo.toString(count, \"n0\") #</div>" },
            { field: "Jenis_sp", title: "Jenis DO" },
            { field: "Tgl_sp", title: "Tanggal DO" },// template: "#= kendo.toString(kendo.parseDate(tgl_sp, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: true },
            { field: "Tgl_Kirim", title: "Tanggal Terkirim" },// template: "#= kendo.toString(kendo.parseDate(tgl_kirim, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: true },
            { field: "Atas_Nama", title: "Customer" },
            { field: "sales", title: "Sales" },
            { field: "subtotal", title: "Subtotal", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "pPn", title: "PPN", format: "{0:#,0.00}", hidden: true, attributes: { class: "text-right " } },
            { field: "JML_RP_TRANS", title: "Grand Total (Rp)", format: "{0:#,0.00}", attributes: { class: "text-right " } }, // aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
            { field: "Action", width: "120px", template: "&nbsp# if(STATUS_DO == 'PERSIAPAN BARANG'){#<center style='display:inline;'><a class='btn btn-info btn-sm editData' href='javascript:void(0)' data-id='#=No_sp#'><i class='glyphicon glyphicon-pencil' aria-hidden='true'></i></a></center> &nbsp <center style='display:inline;'><a class='btn btn-danger btn-sm deleteData' href='javascript:void(0)' data-id='#=No_sp#'><i class='glyphicon glyphicon-trash' aria-hidden='true'></i></a></center>#}#&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=No_sp#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center></center>&nbsp&nbsp<center style='display:inline;'><a class='btn btn-info btn-sm printData' href='javascript:void(0)' data-id='#=No_sp#'><i class='glyphicon glyphicon-print' aria-hidden='true'></i></a></center>" }

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
function prepareActionGrid() {
    $(".viewData").on("click", function () {
        var id = $(this).data("id");
        window.location.href = urlCreate + '?id=' + id + '&mode=VIEW';
    });
    $(".editData").on("click", function () {
        var id = $(this).data("id");

        window.location.href = urlCreate + '?id=' + id + '&mode=EDIT';
    });
    $(".printData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        window.open(
            serverUrl + "Reports/WebFormRpt.aspx?q=" + id, "_blank");
    });

    $(".deleteData").on("click", function () {
        var id = $(this).data("id");

        var savedata = {
            No_sp: id
        };

        swal({
            type: 'warning',
            title: 'Are you sure?',
            html: 'You want to delete this data',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d9534f'
        }).then(function (isConfirm) {
            if (isConfirm.value === true) {
                startSpinner('loading..', 1);
                $.ajax({
                    type: "POST",
                    data: savedata,
                    url: urlDeleteData,
                    success: function (result) {
                        if (result.success === false) {
                            Swal.fire({
                                type: 'error',
                                title: 'Warning',
                                html: result.message
                            });
                            startSpinner('loading..', 0);
                        } else {

                            $.when(getDataDO()).done(function () {
                                $('#GridDO').kendoGrid('destroy').empty();
                                bindGrid();
                                startSpinner('loading..', 0);
                            });
                            startSpinner('loading..', 0);
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
}

function detailInit(e) {
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });
    startSpinner('loading..', 1);
    $.when(getdetailList(e.data.No_sp)).done(function () {
        detailRow.find(".detail").kendoGrid({
            dataSource: {
                data: detailds,
                schema: {
                    model: {
                        id: "No_sp",
                        fields: {
                            nama_Barang: { type: "string" },
                            satuan: { type: "string" },
                            vol: { type: "number" },
                            qty: { type: "number" },
                            harga: { type: "number" },
                            diskon: { type: "number" },
                            total: { type: "number" }
                        }
                    }
                },
                pageSize: 10
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "nama_Barang", title: "Nama Barang", width: "60px" },
                { field: "satuan", title: "Satuan", width: "15px" },
                { field: "vol", title: "Berat", width: "15px", attributes: { class: "text-right " } },
                { field: "qty", title: "Qty", width: "20px", attributes: { class: "text-right " } },
                { field: "harga", title: "Harga", width: "25px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "total", title: "Total", width: "30px", format: "{0:#,0}", attributes: { class: "text-right " } },
            ]
        });
        startSpinner('loading..', 0);
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

function getdetailList(id) {
    var urlLink = urlGetDetailData;
    var filterdata = {
        no_po: id,
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        status_po: ""
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