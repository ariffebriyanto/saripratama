
var _gvList;
var Listds = [];
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
    $('#tanggalfrom').val(startdateserver);
    $('#tanggalto').val(enddateserver);
    startSpinner('Loading..', 1);

    $("#status option[value='Belum Cetak']").attr("selected", "selected");
    $('#status').selectpicker('refresh');
    $('#status').selectpicker('render');

    $.when(getData()).done(function () {
        bindGrid();
        oncboFilterChanged();
        startSpinner('loading..', 0);
    });
});


function onClickPrint() {
    var listPrint = [];

    //GetListPrint

    var status = $('#status').val();
    if (status != "ALL") {
        listPrint = GetListPrint(status);
    }
    else {
        listPrint = Listds;
    }

    console.log(JSON.stringify(listPrint));

    var savedata = {
        "details": listPrint
    }
    if (listPrint.length > 0) {
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
                    url: urlSaveCetakDPB,
                    success: function (result) {
                        if (result.success === false) {
                            Swal.fire({
                                type: 'error',
                                title: 'Warning',
                                html: result.message
                            });
                            startSpinner('loading..', 0);
                        } else {
                            // alert('a');
                            window.open(
                                serverUrl + "Reports/WebFormRpt.aspx?type=rencanakirim&id=" + result.message, "_blank");

                            $.when(getData()).done(function () {
                                bindGrid();
                                oncboFilterChanged();
                                startSpinner('loading..', 0);

                            });
                            //  window.location.href = urlCreate;

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
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: "Item tidak boleh kosong"
        });
    }
  
}

function GetListPrint(status) {
    return Listds.filter(
        function (Listds) { return Listds.status_cetak == status; }
    );
}

function getData() {
    var filterdata = {
        no_po: $("#PONo").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),
        status_po: $("#status").val()
    };
    return $.ajax({
        url: urlGetData,
        type: "POST",
        data: filterdata,
        success: function (result) {
            if (_gvList) {
                $('#gvList').kendoGrid('destroy').empty();
            }
            Listds = result;
            console.log(JSON.stringify(Listds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}


function onClickFilter() {
    startSpinner('loading..', 1);

    $.when(getData()).done(function () {
        bindGrid();
        oncboFilterChanged();
        startSpinner('loading..', 0);
    });
}

function bindGrid() {
    _gvList = $("#gvList").kendoGrid({
        columns: [
            { field: "no_sp", title: "No DO", width: "100px" },
            { field: "tgl_sp", title: "Tanggal DO", width: "100px", template: "#= kendo.toString(kendo.parseDate(tgl_sp, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", filterable: false },
            { field: "nama_Sales", title: "Nama Sales", width: "110px" },
            { field: "jenis_sp", title: "Jenis DO", width: "100px" },
            { field: "nama_Customer", title: "Pelanggan", width: "150px" },
            { field: "nama_Barang", title: "Nama Barang", width: "180px" },
            { field: "sisa", title: "Sisa", width: "60px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "status_cetak", title: "Status Cetak", width: "80px",  filterable: false},
        ],
        dataSource: {
            data: Listds,
            schema: {
                model: {
                    id: "no_sp",
                    fields: {
                        no_sp: { type: "string" },
                        tgl_sp: { type: "date" },
                        nama_Sales: { type: "string" },
                        jenis_sp: { type: "string" },
                        nama_Customer: { type: "string" },
                        nama_Barang: { type: "string" },
                        sisa: { type: "number" },
                        status_cetak: { type: "string" }
                    }
                }
            },
            pageSize: 50
        },
        filterable: true,
       // groupable: true,
        sortable: true,
        // change: onChange,
        noRecords: true,
        //detailTemplate: kendo.template($("#template").html()),
        //detailInit: detailInit,
        //dataBound: function () {
        //    prepareActionGrid();
        //    this.expandRow(this.tbody.find("tr.k-master-row"));
        //},
        height: 550,
        scrollable: {
            virtual: true
        },
        resizable: true,

    }).data("kendoGrid");

}

function oncboFilterChanged() {
    var ds = $("#gvList").data("kendoGrid").dataSource;
    var status = $('#status').val();
    startSpinner('loading..', 1);
    if (status && status != "ALL") {
        ds.filter([
            {
                "filters": [
                    {
                        "field": "status_cetak",
                        "operator": "eq",
                        "value": status
                    }
                ]
            }
        ]);
        startSpinner('loading..', 0);

    }
    else {
        startSpinner('loading..', 0);

        $('#gvList').kendoGrid('destroy').empty();
        bindGrid();
    }
}