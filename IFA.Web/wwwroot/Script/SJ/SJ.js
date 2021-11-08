var pods = [];
var urlAction = "";
var _GridPO;
var approvallist = [];
   

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
        url: urlLink ,
        success: function (result) {
            
            pods = [];
            pods = result;
            //console.log(result);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}
//bind click event to the checkbox
//GridPO.table.on("click", ".checkbox", selectRow);


function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "no_krm", title: "NO Kirim", width: "80px" },
            { field: "no_sp", title: "No DO", width: "130px" },
            { field: "jenis_sp", title: "Jenis DO", width: "130px" },
            { field: "nama_customer", title: "Nama Customer", width: "180px" },
            { field: "kd_sopir", title: "Sopir", width: "80px" },
            { field: "tgl_Cetak", title: "Tanggal Cetak", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_Cetak, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "cetak", title: "", width: "80px", template: "<input type='checkbox' class='checkbox' />" }
           
          
         
          
        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_DPM",
                    fields: {
                        no_krm: { type: "string", editable: false },
                        no_sp: { type: "string", editable: false },
                        jenis_sp: { type: "string", editable: false },
                        nama_customer: { type: "string", editable: false },
                        kd_sopir: { type: "number", editable: false },
                        tgl_cetak: { type: "number", editable: false },
                        cetak: { type: "number", editable: false }
                      

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
        editable: true
    }).data("kendoGrid");


}

var checkedIds = {};

//on click of the checkbox:
function selectRow() {
    var checked = this.checked,
        row = $(this).closest("tr"),
        grid = $("#GridPO").data("kendoGrid"),
        dataItem = grid.dataItem(row);

    checkedIds[dataItem.id] = checked;
    if (checked) {
        //-select the row
        row.addClass("k-state-selected");
    } else {
        //-remove selection
        row.removeClass("k-state-selected");
    }
}

//on dataBound event restore previous selected rows:
function onDataBound(e) {
    var view = this.dataSource.view();
    console.log(view);
    for (var i = 0; i < view.length; i++) {
        if (checkedIds[view[i].id]) {
            this.tbody.find("tr[data-uid='" + view[i].no_krm + "']")
                .addClass("k-state-selected")
                .find(".checkbox")
                .attr("checked", "checked");
        }
    }
}

function onsaveSJ() {
    var sel = $("input:checked", _GridPO.tbody).closest("tr");
    var saveData = [];
    $.each(sel, function (idx, row) {
        var item = _GridPO.dataItem(row);
        saveData.push(item);
    });
    console.log("selected: " + JSON.stringify(saveData));

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
            html: "Please Check Surat Jalan Dilivery Note"
        });
    }
 }
