var pods = [];
var rcnkirimHeader = [];
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
var filterdate;
var editval = false;

$(document).ready(function () {
    $('#Tgl_rcnkirim').prop('disable', true);
    $('#tanggalfrom').prop('disable', true);
    $('#tanggalto').prop('disable', true);

    $('input[name=test]').change(function () {
        var from = $('tanggalfrom').val();
        var to = $("#tanggalto").val();
        if ($('input[name=test]').is(':checked')) {
            document.getElementById('tanggalfrom').readOnly = false;
            document.getElementById('tanggalto').readOnly = false;

           
            filterdate = [
                { field: "tgl_sp", operator: "gte", value: from },
                { field: "tgl_sp", operator: "lte", value: to }
            ];
            _GridPO.dataSource.filter(filterdate);
        } else {
            document.getElementById('tanggalfrom').readOnly = true;
            document.getElementById('tanggalto').readOnly = true;
            $('#GridPO').kendoGrid('destroy').empty();
            bindGrid();
        }

    });


    $('#divtanggalnow').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var datenow = new Date(selected.date.valueOf());
        $('#divtanggalnow').datepicker('setStartDate', datenow);
    });


    $('#tanggalfrom').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    });

    $('#tanggalto').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    });


    $('#tanggalfrom').datepicker("setDate", startdateserver);
    $('#tanggalto').datepicker("setDate", enddateserver);
    //$('#tanggalfrom').val(startdateserver);
    //$('#tanggalto').val(enddateserver);

    var cok = $('#tanggalfrom').datepicker('getDate');
    var cok2 = $('#tanggalto').datepicker('getDate');

    $('#Tgl_rcnkirim').val(dateserver);

    $("#set_petugas").kendoDropDownList({
        dataTextField: "Nama_Pegawai",
        dataValueField: "Kode_Pegawai",
        filter: "contains",
        dataSource: KenekList,
        optionLabel: "Please Select"
    });

    $("#set_kendaraan").kendoDropDownList({
        dataTextField: "Nama_Kendaraan",
        dataValueField: "Kode_Kendaraan",
        filter: "contains",
        dataSource: KendaraanList,
        optionLabel: "Please Select"
    });
   
    if (Mode !== "NEW")
    {
        GetDatarcnKirim(idrcnkirim);
    }
    else
    {
        getData();
        bindGrid();
    }
    
    //$('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
    //    if (e.key === "Enter") {
    //        alert('a');
    //        return false;
    //    }
    //});
});

function onFilterChanged() {
    var from = $('#tanggalfrom').datepicker('getDate');
    var to = $('#tanggalto').datepicker('getDate');
   
        filterdate = [
            { field: "tgl_sp", operator: "gte", value: from },
            { field: "tgl_sp", operator: "lte", value: to }
        ];
        _GridPO.dataSource.filter(filterdate);
   
     
}

function getData() {
    startSpinner('Loading..', 1);

    var urlLink = urlGetData;
    var filterdata = {
        id: $("#no_sp").val(),
        DateFrom:"",
        DateTo: "",
        status_po:"ENTRY"
    };
    
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
       
        success: function (result) {
            if (_GridPO) {
                $('#GridPO').kendoGrid('destroy').empty();
            }
            pods = result;
            for (var i = 0; i <= pods.length - 1; i++) {
                pods[i].jumlah = 0;
            }
            console.log(JSON.stringify(pods));
            bindGrid();
            startSpinner('loading..', 0);
           
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });

}

function readOnly(container, options) {
    container.removeClass("k-edit-cell");
    container.text(options.model.get(options.field));
}

function bindGrid() {
    _GridPO = $("#GridPO").kendoGrid({
        columns: [
            { field: "no_sp", title: "No DO", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "tgl_sp", filterable: false, title: "Tanggal DO", width: "100px", template: "#= kendo.toString(kendo.parseDate(tgl_sp, 'yyyy-MM-dd'), 'dd MMMM yyyy') #", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "nama_Sales", title: "Nama Sales", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "jenis_so", title: "Jenis DO", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" }},
            { field: "nama_Customer", title: "Pelanggan", width: "100px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "nama_Kota", title: "Kota", width: "100px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "nama_Barang", title: "Nama Barang", width: "250px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "sisa", filterable: false, title: "Jumlah Order", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            //{ field: "sisa", title: "Sisa Order", width: "80px" },
            { field: "jumlah", filterable: false, title: "Jumlah Kirim", width: "80px", attributes: {style:"background-color: silver"}},
            { field: "berat", filterable: false, title: "Berat", hidden: true, width: "80px", hidden: true, attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "totberat", filterable: false, title: "Total Berat", hidden: true, width: "80px", hidden: true, editor: readOnly, attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" }},
            { field: "nama_Gudang", title: "Nama Gudang", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "akhir_qty", filterable: false, title: "Qty Gudang", width: "80px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } },
            { field: "keterangan", title: "Keterangan", width: "200px", attributes: { 'style': "#=akhir_qty<sisa?'background-color: lightpink; color:black;':''#" } }
        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                   // id: "no_po",
                    fields: {
                        no_sp: { type: "string", editable: false },
                        tgl_sp: { type: "date", editable: false},
                        jenis_sp: { type: "string", editable: false },
                        nama_Sales: { type: "string", editable: false },
                        nama_Customer: { type: "string", editable: false },
                        nama_Barang: { type: "string", editable: false },
                        qty_order: { type: "number", editable: false },
                        sisa: { type: "number", editable: false },
                        jumlah: { type: "number", editable: true, validation: { min: 0 } },
                        rec_stat: { type: "string", editable: true },
                        keterangan: { type: "string", editable: true },
                        kd_sales: { type: "string", editable: true },
                        no_seq_d: { type: "string", editable: true },
                        no_sp_box: { type: "string", editable: true },
                        nama_kenek: { type: "string", editable: true },
                        nama_Kota: { type: "string", editable: false },
                        Kd_Customer: { type: "string", editable: false },
                        berat: { type: "number", editable: false },
                        totberat: { type: "number" },
                        nama_Gudang: { type: "string", editable: false },
                        akhir_qty: { type: "number", editable: false },
                    }
                }
            },
            change: function (e) {
                if (e.field === "jumlah") {
                    // The total is Prix (price) * Quantite (Quantity)
                    var newTotal = (e.items[0].berat * e.items[0].jumlah).toFixed(2);
                    e.items[0].set("totberat", newTotal);
                    var validation = e.items[0].sisa;
                    var vakhir_qty = e.items[0].akhir_qty;

                    if (vakhir_qty < validation) {
                        if (e.items[0].jumlah > vakhir_qty) {
                            e.items[0].set("jumlah", vakhir_qty);
                        }
                    }
                    else {
                        if (e.items[0].jumlah > validation) {
                            e.items[0].set("jumlah", validation);
                        }
                    }

                   
                    return true;
                }
                
            },
            pageSize: 50,
            //group: {
            //    field: "no_sp",
            //    dir: "asc"
            //}
        },
        editable: true,
        //pageable: {
        //    pageSizes: [20, 40, 100],
        //    change: function () {
        //        //  prepareActionGrid();
        //    }
        //},
        dataBound: function (e) {
            //var grid = this;
            //$(".k-grouping-row").each(function (e) {
            //    grid.collapseGroup(this);
            //});
        },
        //groupable: true,
        //save: function (e) {
        //    alert('s');
        //},
        noRecords: true,
        height: 550,
        scrollable: {
            virtual: true
        },
        resizable: true,
        filterable: {
            extra: true,
            operators: {
                string: { contains: "Contains" }
            }
        },

    }).data("kendoGrid");

}

function GetDatarcnKirim(idrcnkirim) {
    startSpinner('loading..', 1);
    var urlLink = urlGetDatarcnKirim;
    var filterdata = {
        id: idrcnkirim,
        //DateFrom: "",
        //DateTo: "",
        status_po: ""
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            rcnkirimHeader = result;
            if (Mode === "NEW") {
                Mode = "VIEW";
            }
            fillForm();
            $.ajax({
                url: urlGetDatarcnKirimDetail,
                type: "POST",
                data: filterdata,
                success: function (result) {

                    pods = [];
                    for (var i = 0; i <= result.length - 1; i++) {
                        pods.push({
                            no_sp: result[i].no_sp,
                            jenis_sp: result[i].jenis_sp,
                            jenis_so: result[i].jenis_sp,
                            nama_Sales: result[i].nama_Sales,
                            tgl_sp: result[i].tgl_sp,
                            nama_Customer: result[i].nama_Customer,
                            qty_order: result[i].qty_order,
                            nama_Barang: result[i].nama_Barang,
                            sisa: result[i].sisa,
                            jumlah: result[i].jumlah,
                            berat: result[i].berat,
                            totberat: result[i].totberat
                        });
                    }
                   // console.log(pods);
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

function fillForm() {
    if (Mode === "EDIT" || Mode === "VIEW") {


        //$('[name="qty_qc_pass"]').attr("readonly", true);
        $("#Tgl_rcnkirim").datepicker().attr('readonly', 'readonly');
        $("#divtanggalnow").datepicker().attr('readonly', 'readonly');

        $("#divtanggalnow").datepicker("option", "disabled", true);
        ////$("#no_trans").attr("disabled", "disabled");
        //$("#no_ref").attr("disabled", "disabled");
        ////$("#no_qc").data("kendoDropDownList").readonly();
        $("#set_petugas").data("kendoDropDownList").readonly();
        $("#set_kendaraan").data("kendoDropDownList").readonly();
        //$("#nm_penyerah").attr("disabled", "disabled");
        //$("#Keterangan").attr("disabled", "disabled");
        //// $("#nm_Gudang_asal").data("kendoDropDownList").readonly();

    }

    if (Mode === "EDIT" || Mode === "VIEW") {
        if (rcnkirimHeader.length > 0) {
            //$("#no_qc").data("kendoDropDownList").value(GudangDs[0].no_qc);
            //$("#no_qc").val(POds[0].no_qc);
        
            $("#no_trans").val(rcnkirimHeader[0].no_trans);
            $("#Tgl_rcnkirim").val(rcnkirimHeader[0].tgl_transdesc);
            $("#set_petugas").data("kendoDropDownList").value(rcnkirimHeader[0].kd_kenek);
            $("#set_kendaraan").data("kendoDropDownList").value(rcnkirimHeader[0].kd_kendaraan);

           

        }
    }
}


function onSaveClicked() {
    var supir = $('#set_petugas').val();
    var kendaraan = $('#set_kendaraan').val();
    gridData = $("#GridPO").data("kendoGrid");

    var BeforeSave = gridData.dataSource.data().toJSON();
    var detail = [];

    for (var i = 0, len = BeforeSave.length; i < len; i++) {
        if (BeforeSave[i].jumlah !== 0) { detail.push(BeforeSave[i]); }
    }

    validationMessage = '';
    //if (!supir && kendaraan != '009')
    //{
    //    validationMessage = validationMessage + 'Supir harus di pilih.' + '\n';
    //}
    //if (!kendaraan) {
    //    validationMessage = validationMessage + 'Kendaraan harus di pilih.' + '\n';
    //}
    if (detail.length <= 0) {
        validationMessage = validationMessage + 'Data Tidak Boleh Kosong' + '\n';
    }


    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {

        save();
    }
}

function save() {
    gridData = $("#GridPO").data("kendoGrid");
   
    var BeforeSave = gridData.dataSource.data().toJSON();
    var detail = [];

    for (var i = 0, len = BeforeSave.length; i < len; i++) {
        if (BeforeSave[i].jumlah > 0) { detail.push(BeforeSave[i]); }
    }
    
    var no = "";
    var urlSaveData

    if (Mode == "EDIT") {
        no = $("#no_trans").val();
        urlSaveData = urlEdit;
    }
    else {
        urlSaveData = urlSave;
    }
    var saveData = {
        no_trans:no,
        kd_kenek: $('#set_petugas').val(),
        kd_kendaraan: $('#set_kendaraan').val(),
        rcnkrmDetail: detail,
        rcnkrmDetailSO:detail
    };
    console.log(saveData);
 
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
                data: saveData, 
                url: urlSaveData,
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
                        //getData();
                        //fillForm();
                        window.location.href = urlCreate;
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


function showlist() {
    window.location.href = urlList;
}


