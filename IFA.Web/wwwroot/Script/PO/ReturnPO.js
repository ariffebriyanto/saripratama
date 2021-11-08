var returinvds = [];
var detailsds = [];
var returpods = [];
var returds = [];
var columngrid = [
    { field: "nama_Barang", title: "Nama Barang", width: "60px" },
    { field: "kd_satuan", title: "Satuan", width: "15px" },
    { field: "harga", title: "Harga", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "qty", title: "Qty PO", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { field: "qty_retur", title: "Qty", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " }, editor: valueValidation },
    { field: "retur_total", title: "Total", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
    { command: ["destroy"], title: "Actions", width: "20px" }
];

$(document).ready(function () {
    startSpinner('Loading..', 1);
    $.when(GetReturInv()).done(function () {
        bindGrid();
        fillNoDO();

        if (Mode == "VIEW") {
            columngrid = [
                { field: "nama_Barang", title: "Nama Barang", width: "60px" },
                { field: "kd_satuan", title: "Satuan", width: "15px" },
                { field: "harga", title: "Harga", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "qty", title: "Qty", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
                { field: "retur_total", title: "Total", width: "20px", format: "{0:#,0}", attributes: { class: "text-right " } },
            ];
            $.when(GetRetur()).done(function () {
                fillform();
                startSpinner('Loading..', 0);
            });
        }
        else {
            startSpinner('Loading..', 0);
        }
       
    });
    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    })

    $("#tanggal").val(dateserver);
    $('#divtanggal').datepicker('remove');
    $('#tanggal').attr("disabled", "disabled");

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {
            return false;
        }
    });
});

function GetReturInv() {
    return $.ajax({
        url: urlGetReturInv,
        success: function (result) {
            returinvds = [];
            returinvds = result;
            console.log(JSON.stringify(returinvds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillNoDO() {
    $("#noDO").empty();
    $("#noDO").append('<option value="" selected disabled>Please select</option>');
    var data = returinvds;

    for (var i = 0; i < data.length; i++) {
        $("#noDO").append('<option value="' + data[i].no_po + '">' + data[i].no_po + '</option>');
    }

    $('#noDO').selectpicker('refresh');
    $('#noDO').selectpicker('render');
}

function onNoDOChanged() {
    var id = $("#noDO").val();
    var found = GetDODetailByInv(id);
    //$("#NoInv").val(found[0].no_inv);
    $("#Supplier").val(found[0].nama_Supplier);
    $("#NoInv").val(found[0].no_jur);
    //$("#JenisDO").val(found[0].jenis_sp);
    $.when(GetPORetur()).done(function () {
        if ($('#GridDetails').hasClass("k-grid")) {
            $('#GridDetails').kendoGrid('destroy').empty();
        }
        bindGrid();
        startSpinner('Loading..', 0);
    });
}

function GetDODetailByInv(code) {
    return returinvds.filter(
        function (returinvds) { return returinvds.no_po === code; }
    );
}

function bindGrid() {
    _GridDetails = $("#GridDetails").kendoGrid({
        columns: columngrid,
        dataSource: {
            data: returpods,
            schema: {
                model: {
                    fields: {
                        no_po: { type: "string", editable: false},
                        kd_stok: { type: "string", editable: false},
                        kd_satuan: { type: "string", editable: false},
                        harga: { type: "number", editable: false},
                        qty: { type: "number", editable: false},
                        qty_retur: { type: "number", validation: { required: true, min: 0, defaultValue: 0 }},
                        retur_total: { type: "number", editable: false},
                        nama_Barang: { type: "string", editable: false},
                    }
                }
            }
        },
        cancel: function (e) {
            $('#GridDO').data('kendoGrid').dataSource.cancelChanges();
        },
        save: function (data) {
            setTimeout(function () {
                var ds = $('#GridDetails').data('kendoGrid').dataSource.data();
                returpods = [];
                for (var i = 0; i <= ds.length - 1; i++) {

                    returpods.push({
                        no_po: ds[i].no_po,
                        kd_stok: ds[i].kd_stok,
                        kd_satuan: ds[i].kd_satuan,
                        harga: ds[i].harga,
                        qty: ds[i].qty,
                        qty_retur: ds[i].qty_retur,
                        retur_total: ds[i].harga * ds[i].qty_retur,
                        qty_total: ds[i].qty_total,
                        nama_Barang: ds[i].nama_Barang
                    });
                }
                $('#GridDetails').kendoGrid('destroy').empty();
                bindGrid();
            });
        },
        noRecords: true,
        editable: true
    }).data("kendoGrid");

}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        decimals: 0,
        max: options.model.qty,
        min: 1
    });
}

function GetPORetur() {
    return $.ajax({
        url: urlGetReturPO + '?no_po=' + $("#noDO").val(),
        success: function (result) {
            returpods = [];
            returpods = result;
            console.log(JSON.stringify(returpods));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function onSaveClicked() {
    SaveData();
}

function SaveData() {
    var found = GetDODetailByInv($('#noDO').val());
    var savedata = {

        tanggal: $('#tanggal').val(),
        no_po: $('#noDO').val(),
        no_ref1: $('#NoInv').val(),
        kd_supplier: found[0].kd_supplier,
        keterangan: $('#Keterangan').val(),
        no_ref: $('#NoReferensi').val(),
        detail: _GridDetails.dataSource.data().toJSON()
    }
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
                        window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
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

function showCreate() {
    window.location.href = urlCreate;
}

function GetRetur() {
    return $.ajax({
        url: urlGetRetur + '?no_po=' + idDO,
        success: function (result) {
            returds = [];
            returds = result;
            returpods = [];

            for (var i = 0; i <= returds.detail.length - 1; i++) {
                returpods.push({
                    kd_satuan: returds.detail[i].satuan,
                    harga: returds.detail[i].harga,
                    qty: returds.detail[i].qty,
                    retur_total: returds.detail[i].total,
                    nama_Barang: returds.detail[i].nama_barang,
                })
            }
            if ($('#GridDetails').hasClass("k-grid")) {
                $('#GridDetails').kendoGrid('destroy').empty();
            }
            bindGrid();
            console.log(JSON.stringify(returds));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillform() {
    $("#NoRetur").val(returds.no_retur);
    $("#NoInv").val(returds.no_ref1);
    $("#Status").val(returds.rec_stat);
    $("#NoReferensi").val(returds.no_ref);
    $("#Keterangan").val(returds.keterangan);
    $('#NoReferensi').attr("disabled", "disabled");
    $('#Keterangan').attr("disabled", "disabled");
    $("#noDO option[value='" + returds.no_po + "']").attr("selected", "selected");
    $('#noDO').selectpicker('refresh');
    $('#noDO').selectpicker('render');
    $('#noDO').attr("disabled", "disabled");
    var found = GetDODetailByInv($('#noDO').val());
    $('#Supplier').val(found[0].nama_Supplier);
    console.log(JSON.stringify(found));

}


function onPrintClicked() {
    window.open(
        serverUrl + "Reports/WebFormRpt.aspx?type=returpo&id=" + idDO, "_blank");
}