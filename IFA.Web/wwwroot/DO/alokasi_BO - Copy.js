
var _Grid;
var detailIndends = [];
var indends = [];
var customerds = [];

var levelList = [
    { text: "", value: "" },
    { text: "A", value: "A" },
    { text: "B", value: "B" },
    { text: "C", value: "C" },
    { text: "D", value: "D" }
];
$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(getDataiNDEN()).done(function () {
        $.when(getCustomer()).done(function () {
            fillCboCustomer();
        bindGrid();
            startSpinner('loading..', 0);

        });
    });
});

function onSaveClicked() {
    gridData = $("#gvList").data("kendoGrid");
    var saveData = gridData.dataSource.data().toJSON();
    var found = GetDataSave();
    console.log(JSON.stringify(found));
    if (found.length > 0) {
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
                    data: JSON.stringify(found),
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
                            startSpinner('loading..', 0);

                        } else {
                            $.when(getDataiNDEN()).done(function () {
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
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: "Masukan Qty Alokasi"
        });
    }
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

function GetDataSave() {
    var saveData = gridData.dataSource.data().toJSON();
    return saveData.filter(
        function (saveData) { return saveData.qty_Alokasi > 0; }
    );
}

function getDataiNDEN() {
    var urlLink = urlGetDataInden;
    return $.ajax({
        url: urlLink,
        type: "GET",
        success: function (result) {
            indends = [];
            indends = result;
            //console.log(JSON.stringify(indends));
            if (_Grid != undefined) {
                $("#gvList").kendoGrid('destroy').empty();
            }
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        max: options.model.qty,
        min: 0
    });
}

function bindGrid() {
    _Grid = $("#gvList").kendoGrid({
        columns: [
            { field: "jenis_so", "filterable": false, title: "Jenis BO", width: "70px" },
            { field: "no_sp", "filterable": false, title: "No DO", width: "70px" },
            { field: "nama_Sales", title: "Nama Sales", width: "60px" },
            { field: "nama_Barang", title: "Nama Barang", width: "120px" },
            { field: "nama_Customer", title: "Customer", width: "150px" },
            { field: "tgl_inden", "filterable": false, title: "Tanggal BO", width: "70px", template: "#= kendo.toString(kendo.parseDate(tgl_inden, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "qty", "filterable": false, title: "Qty BO", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "qty_Alokasi", "filterable": false, title: "Qty Alokasi", width: "50px", attributes: { style: "background-color: aquamarine", class: "text-right " }, editor: valueValidation, validation: { required: true, min: 0, defaultValue: 0 } },
            { field: "qty_available", "filterable": false, title: "Qty Available", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "total_qty", "filterable": false, width: "50px", title: "Total", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "stok", "filterable": false, width: "50px", title: "Stok Gudang", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "totalBerat", "filterable": false, width: "50px", title: "Total Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "harga", "filterable": false, width: "50px", title: "Harga", format: "{0:#,0}", visible:false, attributes: { class: "text-right " } },
            { field: "total", "filterable": false, width: "60px", title: "Total", format: "{0:#,0}", attributes: { class: "text-right " } },
            { template: '#=dirtyField(data,"tunda")#<input type="checkbox" #= tunda ? \'checked="checked"\' : "" # class="chkbx k-checkbox" />', width: 110 },
        ],
        dataSource: {
            data: indends,
            schema: {
                model: {
                    id: "id",
                    fields: {
                        jenis_so: { type: "string" },
                        no_sp: { type: "string" },
                        nama_Customer: { type: "string" },
                        id: { type: "string" },
                        idDisplay: { type: "string" },
                        kd_Stok: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number" },
                        qty_available: { type: "number" },
                        keterangan: { type: "string" },
                        nama_Barang: { type: "string" },
                        nama_Sales: { type: "string" },
                        total_qty: { type: "number" },
                        stok: { type: "number" },
                        //totalBeratInden: { type: "number" },
                        tunda: { type: "boolean" },
                        status: { type: "string" },
                        kd_sales: { type: "string" },
                        tgl_inden: { type: "date" },
                        qty_Alokasi: { type: "string" }
                    }
                }
            }
        },
        noRecords: true,
        filterable: true,
        //detailTemplate: kendo.template($("#template").html()),
        //detailInit: detailInitInden,
        editable: true,
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
                { field: "nama_Barang", title: "Nama Barang", width: "120px" },
                { field: "satuan", title: "Satuan", width: "40px" },
                { field: "qty", title: "Qty Request", width: "50px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "berat", width: "50px", title: "Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "tBerat", width: "50px", title: "Total Berat", format: "{0:#,0.00}", attributes: { class: "text-right " } },
                { field: "harga", width: "50px", title: "Harga", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "total", width: "50px", title: "Total", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "keterangan", title: "Keterangan", width: "180px" },
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
                    "tBerat": result[i].qty * result[i].berat
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

function barangDropDownEditor(container, options) {
    var input = $('<input id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: levelList,
        filter: "contains",
        template: "<span data-id='${data.value}' data-Approval='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var grid = _Grid.dataItem($(e.sender.element).closest("tr"));
            grid.alokasiLevel = id;
        }
    }).appendTo(container);
}