var pods = [];
podetailds = [];
var ds = [];
var grid = [];
var paramValue;
var gridData;
var urlAction = "";
var optionsGrid = {
    pageSize: 10
};
var _GridPO;

var approvallist = [
    { text: "", value: "" },
    { text: "APPROVE", value: "APPROVE" },
    { text: "REVISE", value: "REVISE" },
    { text: "REJECT", value: "REJECT" }
];

$(document).ready(function () {
    getData();
});

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        no_po: $("#PONo").val(),
        DateFrom:"",
        DateTo: "",
        status_po:"ENTRY"
    };
    console.log("console log" + filterdata);
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
       
        success: function (result) {
            if (_GridPO) {
                $('#GridPO').kendoGrid('destroy').empty();
            }
            pods = result;

            $.ajax({
                url: urlGetDetailData,
                type: "POST",
                data: filterdata,
                success: function (result) {
                    podetailds = result;
                    bindGrid();
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
function barangDropDownEditor(container, options) {
    var input = $('<input id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: approvallist,
        filter: "contains",
        //optionLabel: "Select Barang",
       
        template: "<span data-id='${data.value}' data-Approval='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var approval = _GridPO.dataItem($(e.sender.element).closest("tr"));
            approval.rec_stat = id;
        }
    }).appendTo(container);
}


function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "no_po", title: "PO Number", width: "130px" },
            { field: "tgl_po", title: "Tanggal PO", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_po, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "nama_Supplier", title: "Supplier", width: "230px" },
            { field: "atas_nama", title: "Atas Nama", width: "130px" },
            { field: "kop_surat", title: "Kop Surat", width: "120px" },
            { field: "tgl_kirim", title: "Tanggal Kirim", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_kirim, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "term_bayar", title: "Keterangan", width: "230px" },
            { field: "rec_stat", title: "Approve", attributes: { class: "text-right ", 'style': 'background-color: Aquamarine; color:black;' }, width: "80px", editor: barangDropDownEditor },
            { field: "ket_batal", title: "Alasan Reject", attributes: { class: "text-right ", 'style': 'background-color: Aquamarine; color:black;' }, width: "230px" } //, attributes: { 'style': 'background-color: #C7E8E3; color:black;'}}
           

        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_po",
                    fields: {
                        no_po: { type: "string", editable: false },
                        tgl_po: { type: "date", editable: false},
                        kd_supplier: { type: "string", editable: false },
                        atas_nama: { type: "string", editable: false },
                        kop_surat: { type: "string", editable: false },
                        nama_Supplier: { type: "string", editable: false },
                        tgl_kirim: { type: "date", editable: false},
                        keterangan: { type: "string", editable: false },
                        term_bayar: { type: "string", editable: false },
                        rec_stat: { type: "string", editable: true },
                        ket_batal: { type: "string", editable: true }

                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        editable: true,
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
                //  prepareActionGrid();
            }
        },
        change: onChange,
        noRecords: true,
        detailTemplate: kendo.template($("#template").html()),
        detailInit: detailInit,
        dataBound: function () {
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
            data: podetailds,
            schema: {
                model: {
                    id: "no_seq",
                    fields: {
                        no_seq: { type: "string" },
                        nama_barang: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number" },
                        prosen_diskon: { type: "number" },
                        diskon2: { type: "number" },
                        diskon3: { type: "number" },
                        diskon4: { type: "number" },
                        harga: { type: "number" },
                        total: { type: "number" },
                        last_price: { type: "number" }
                    }
                }
            },
            pageSize: 7,
            filter: { field: "no_po", operator: "eq", value: e.data.no_po }
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            { field: "no_seq", title: "No", width: "30px" },
            { field: "nama_barang", title: "Nama Stok", width: "110px" },
            { field: "keterangan", title: "Nama PO", width: "110px" },
            { field: "satuan", title: "Satuan", width: "50px" },
            { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "last_price", title: "Last Price", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "harga", title: "harga", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "prosen_diskon", title: "Disc % #1", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "diskon2", title: "Disc % #2", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "diskon3", title: "Disc % #3", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "diskon4", title: "Disc %", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "total", title: "total", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } }
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
    var rec_stat = $('#rec_stat').val();

    if (rec_stat) {
        ds.filter([
            {
                "filters": [
                    {
                        "field": "rec_stat",
                        "operator": "eq",
                        "value": rec_stat
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


function onSaveClicked() {
    save();
}

function save() {
    gridData = $("#GridPO").data("kendoGrid");
    //paramValue = gridData.dataSource.data().toJSON();

    //var models = [];
    //gridData.table.find("input[type=checkbox]:checked").each(function () {
    //    var row = $(this).closest("tr");
    //    var model = gridData.dataItem(row);
    //    models.push(model);
    //});

    var saveData = gridData.dataSource.data().toJSON();
   
   
    //var savedata = {
       
    //};
  //  console.log('savedata: ' + JSON.stringify(models));
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
                data: JSON.stringify(saveData), 
                dataType: "json",
                url: urlSave,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message

                        });
                    } else {
                        //Swal.fire({
                        //    type: 'success',
                        //    title: 'Success',
                        //    html: result.message
                        //});
                        getData();
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




