var pods = [];
var urlAction = "";
var _GridPO;
var approvallist = [
    { text: "", value: "" },
    { text: "APPROVE", value: "APPROVE" },
    { text: "REVISE", value: "REVISE" },
    { text: "REJECT", value: "REJECT" }
];

$(document).ready(function () {
    startSpinner('Loading..', 1);
   
    $.when(getData()).done(function () {
        bindGrid();
        startSpinner('loading..', 0);
    });
});

function getData() {
    var urlLink = urlGetData;
    return $.ajax({
        url: urlLink + "?status=Entry",
        success: function (result) {
            pods = [];
            pods = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "rec_stat", title: "Status", width: "80px", attributes: { class: "text-center borderStatus", 'style': "#if(rec_stat == 'OUTSTANDING'){#background-color: cornflowerblue;color: white;#}else if(rec_stat == 'BATAL'){#background-color: orangered;color: white;#}else if(rec_stat == 'CLOSE'){#background-color: green;color: white;#}else if(rec_stat == 'OPEN'){#background-color: deepskyblue;color: black;#}else if(rec_stat == 'ENTRY'){#background-color: aquamarine;color: black;#}else if(rec_stat == 'REVISE'){#background-color: lightpink;color: black;#}#" } },
            { field: "no_DPM", title: "Request Number", width: "130px" },
            { field: "no_po", title: "PO Number", width: "130px" },
            { field: "nama_Barang", title: "Nama Barang", width: "180px" },
            { field: "satuan", title: "Satuan", width: "80px" },
            { field: "tgl_Diperlukan", title: "Tanggal Diperlukan", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_Diperlukan, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "qty", title: "Qty Request", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "status_approve", title: "Status Approval", attributes: { class: "text-right ", 'style': 'background-color: Aquamarine; color:black;' }, width: "80px", editor: approvalDropDownEditor },
            { field: "qty_approve", title: "Qty Approve", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_DPM",
                    fields: {
                        no_DPM: { type: "string", editable: false },
                        no_po: { type: "string", editable: false },
                        kd_Stok: { type: "string", editable: false },
                        satuan: { type: "string", editable: false },
                        qty: { type: "number", editable: false },
                        qty_PR: { type: "number", editable: false },
                        qty_received: { type: "number", editable: false },
                        qty_sisa: { type: "number", editable: false },
                        rec_stat: { type: "string", editable: false },
                        tgl_Diperlukan: { type: "date", editable: false },
                        keterangan: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        qty_approve: { type: "number" },
                        status_approve: { type: "string" }

                    }
                }
            }
        },
        edit: function (e) {
            if (e.model.rec_stat != "ENTRY") {

                var qty_approveInput = e.container.find("input[name=qty_approve]").data("kendoNumericTextBox");
                if (qty_approveInput != undefined) {
                    qty_approveInput.enable(false);
                }

                var status_approveInput = e.container.find("input[name=text]").data("kendoDropDownList");
                if (status_approveInput != undefined) {
                    status_approveInput.enable(false);
                }

                //alert('a');

            }
        },
        noRecords: true,
        editable:true
    }).data("kendoGrid");

}

function approvalDropDownEditor(container, options) {
    var input = $('<input id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: approvallist,
        filter: "contains",
        template: "<span data-id='${data.value}' data-Approval='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var approval = _GridPO.dataItem($(e.sender.element).closest("tr"));
            approval.status_approve = id;
        }
    }).appendTo(container);
}

function onsaveApprovalPO() {
    var saveData = [];
    var approvalData = _GridPO.dataSource.data().toJSON();
    for (var i = 0; i <= approvalData.length - 1; i++) {
        if (approvalData[i].status_approve != "" && approvalData[i].status_approve != approvalData[i].rec_stat) {
            saveData.push(
                {
                    qty_approve: approvalData[i].qty_approve,
                    status_approve: approvalData[i].status_approve,
                    no_DPM: approvalData[i].no_DPM
                }
            );
        }
    }

    if (saveData.length > 0) {
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
                    contentType: "application/json",
                    url: urlSave,
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
                            $.when(getData()).done(function () {
                                $('#GridPO').kendoGrid('destroy').empty();

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
            html: "Please update status approval the record"
        });
    }
    
}