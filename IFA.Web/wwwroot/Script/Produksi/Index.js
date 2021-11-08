var Gudangds = [];
var Gudangdetailds = [];
var urlAction = "";
var customer = "";
var optionsGrid = {
    pageSize: 10
};
var _GridTerima;
$(document).ready(function () {
    customer = false;
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
    $('#tanggalfrom').val(startdateserver);
    $('#tanggalto').val(enddateserver);
    startSpinner('Loading..', 1);
   // $.when(getData()).done(function () {
        //$('#GridTerima').kendoGrid('destroy').empty();

    $.when(getCustomer()).done(function () {
      
           
            fillCboCustomer();
            bindGrid();

            //  getData();

            startSpinner('loading..', 0);
       
   });
});

function getData() {
   // startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        no_sp: $("#SONo").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val()
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
            //console.log(Gudangds);
            


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
    $(".DeleteData").on("click", function () {
        var id = $(this).data("id");
        DeletePage(id);

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
function bindGrid() {


    _GridTerima = $("#GridTerima").kendoGrid({
        columns: [
            { field: "tanggal", title: "Tanggal", width: "120px", template: "#= kendo.toString(kendo.parseDate(tanggal, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "no_trans", title: "No Transaksi", width: "100px" },
            { field: "nama_Supir", title: "Nama Supir", width: "100px" },
            { field: "nama_kenek", title: "Nama Kenek", width: "100px" },
            { field: "last_created_by", title: "Create By", width: "50px" },
            { field: "Action", width: "120px", template: "<center style='display:inline;'><a class='btn btn-info btn-sm editData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-pencil' aria-hidden='true'></i></a></center>&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center><center style='display:inline;'></center>&nbsp<center style='display:inline;'><a class='btn btn-danger btn-sm DeleteData' href='javascript:void(0)' data-id='#=no_trans#'><i class='glyphicon glyphicon-trash' aria-hidden='true'></i></a></center>" }

        ],
        //dataSource: {
        //    data: Gudangds,
        //    schema: {
        //        model: {
        //            id: "no_trans",
        //            fields: {
        //                no_trans: { type: "string" },
        //                nama_Supir: { type: "string" },
        //                tanggal: { type: "date" },
        //                nama_kenek: { type: "string" },
        //                last_created_by: { type: "string" }
                      
        //            }
        //        }
        //    },
        //    pageSize: optionsGrid.pageSize
        //},
        resizable: true,
        sortable: true,
        dataSource: {
            transport: {
                read: function (option) {
                    if (customer == true) {
                        var data1 = {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            kd_customer: $("#Kd_Customer").val(),
                            no_sp: $("#SONo").val()
                            //DateFrom: $("#tanggalfrom").val(),
                            //DateTo: $("#tanggalto").val()
                        };
                    } else {
                        var data1 = {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            DateFrom: $("#tanggalfrom").val(),
                            DateTo: $("#tanggalto").val()

                        };
                    }


                    $.ajax({
                        url: urlGetData,
                       
                        data: data1,
                        //{
                        //    skip: option.data.skip,
                        //    take: option.data.take,
                        //    pageSize: option.data.pageSize,
                        //    page: option.data.page,
                        //    sorting: JSON.stringify(option.data.sort),
                        //    filter: JSON.stringify(option.data.filter),
                        //    DateFrom: $("#tanggalfrom").val(),
                        //    DateTo: $("#tanggalto").val(),
                        //    no_sp: $("#SONo").val()
                        //},
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
            pageSize: 100,
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

function detailInit(e) {
    
    startSpinner('loading..', 1);
    var detailRow = e.detailRow;
    detailRow.find(".tabstrip").kendoTabStrip({
        animation: {
            open: { effects: "fadeIn" }
        }
    });

    
    //$.when(getDetail(e.data.no_trans)).done(function () {
    
        detailRow.find(".detail").kendoGrid({
            //serverPaging: true,
            //serverSorting: true,
            //serverFiltering: true,
            //dataSource: {
            //    data: Gudangdetailds,
            //    schema: {
            //        model: {
            //            id: "no_trans",
            //            fields: {
            //                no_trans: { type: "string", editable: false },
            //                no_sp: { type: "string", editable: false },
            //                nama_Barang: { type: "string", editable: false },
            //                almt_pnrm: { type: "string", editable: false },
            //                atas_nama: { type: "string", editable: false }

            //            }
            //        }
            //    },
            //    sort: { field: "no_sp", dir: "asc" },
            //    //pageSize: 7,
            //    //filter: { field: "no_trans", operator: "eq", value: e.data.no_trans }
            //},
            resizable: true,
            sortable: true,
            dataSource: {
                transport: {
                    read: function (option) {
                       
                        var filterdata = {
                            no_trans: e.data.no_trans,
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter)


                        };

                        $.ajax({
                            url: urlGetDetailData,

                            data: filterdata,
                           
                            //{
                            //    skip: option.data.skip,
                            //    take: option.data.take,
                            //    pageSize: option.data.pageSize,
                            //    page: option.data.page,
                            //    sorting: JSON.stringify(option.data.sort),
                            //    filter: JSON.stringify(option.data.filter),
                            //    DateFrom: $("#tanggalfrom").val(),
                            //    DateTo: $("#tanggalto").val(),
                            //    no_sp: $("#SONo").val()
                            //},
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
                //serverFiltering: true,
                //serverSorting: true,
                //serverPaging: true,

               
                pageSize: 7,
            },
            scrollable: {
                endless: true,
            },

           
            pageable: {
                refresh: true,
                pageSizes: true,
                numeric: true,
                previousNext: true,
            },
            columns: [
                { field: "no_sp", title: "No DO", width: "30px" },
                { field: "nama_Barang", title: "Nama Barang", width: "110px" },
                { field: "atas_nama", title: "Customer", width: "80px" },
                { field: "almt_pnrm", title: "Alamat Pengiriman", width: "110px" }

                //    { field: "satuan", title: "Satuan", width: "50px" },
                //    { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "harga", title: "harga", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "prosen_diskon", title: "Disc % #1", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "diskon2", title: "Disc % #2", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "diskon3", title: "Disc % #3", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "diskon4", title: "Disc Rp #4", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                //    { field: "total", title: "total", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } }
            ]
             
        });
    startSpinner('loading..', 0);
    //});
    
}


function getDetail(id) {
    var filterdata = {
        no_trans: id
        //DateFrom: $("#tanggalfrom").val(),
        //DateTo: $("#tanggalto").val(),
        // kd_customer: id1


    };
    $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Gudangdetailds = [];
            Gudangdetailds = result;
          
            //bindGrid();
            //prepareActionGrid();
            //startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            //startSpinner('loading..', 0);
        }
    });
}
function getDetail1() {
    var filterdata = {
        //no_trans: id
        ////DateFrom: $("#tanggalfrom").val(),
        ////DateTo: $("#tanggalto").val(),
        //// kd_customer: id1


    };
    $.ajax({
        url: urlGetDetailData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            Gudangdetailds = [];
            Gudangdetailds = result;
         
            //bindGrid();
            //prepareActionGrid();
            //startSpinner('loading..', 0);

        },
        error: function (data) {
            alert('Something Went Wrong');
            //startSpinner('loading..', 0);
        }
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
    debugger;
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

function DeletePage(id) {

    swal({
        type: 'warning',
        title: 'Apakah Anda yakin akan menghapus Data?',
        html: '',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);

            $.ajax({
                type: "POST",
                url: urlDelete + '?id=' + id,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message

                        });
                    } else {
                        window.location.href = urlIndex;
                    }
                    startSpinner('loading..', 0);



                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        }
    });

}

function getByCust() {
    //var urlLink = urlGetData;
    //startSpinner('loading..', 1);

    //var filterdata = {
    //    no_sp: $("#SONo").val(),
    //    DateFrom: $("#tanggalfrom").val(),
    //    DateTo: $("#tanggalto").val(),
    //};
    //$.ajax({
    //    url: urlLink,
    //    type: "POST",
    //    data: filterdata,
    //    success: function (result) {
    //        if (_GridTerima) {
    //            $('#GridTerima').kendoGrid('destroy').empty();
    //        }


    //        Gudangds = result;
    //        //$.ajax({
    //        //    url: urlGetDetailData,
    //        //    type: "POST",
    //        //    data: filterdata,
    //        //    success: function (result) {
    //        //        Gudangdetailds = result;
    //        //        console.log(result);
    //        //        bindGrid();
    //        //        prepareActionGrid();
    //        //        startSpinner('loading..', 0);

    //        //    },
    //        //    error: function (data) {
    //        //        alert('Something Went Wrong');
    //        //        startSpinner('loading..', 0);
    //        //    }
    //        //});


    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});
    customer = true;
    startSpinner('loading..', 1);
    $('#GridTerima').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);

}

function searchPO() {
    //var urlLink = urlGetData;
    //startSpinner('loading..', 1);

    //var filterdata = {
    //    no_sp: $("#SONo").val(),
    //    DateFrom: $("#tanggalfrom").val(),
    //    DateTo: $("#tanggalto").val(),
    //};
    //$.ajax({
    //    url: urlLink,
    //    type: "POST",
    //    data: filterdata,
    //    success: function (result) {
    //        if (_GridTerima) {
    //            $('#GridTerima').kendoGrid('destroy').empty();
    //        }
            

    //        Gudangds = result;
    //        //$.ajax({
    //        //    url: urlGetDetailData,
    //        //    type: "POST",
    //        //    data: filterdata,
    //        //    success: function (result) {
    //        //        Gudangdetailds = result;
    //        //        console.log(result);
    //        //        bindGrid();
    //        //        prepareActionGrid();
    //        //        startSpinner('loading..', 0);

    //        //    },
    //        //    error: function (data) {
    //        //        alert('Something Went Wrong');
    //        //        startSpinner('loading..', 0);
    //        //    }
    //        //});
        

    //    },
    //    error: function (data) {
    //        alert('Something Went Wrong');
    //        startSpinner('loading..', 0);
    //    }
    //});
    customer = false;
    startSpinner('loading..', 1);
    $('#GridTerima').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);

}