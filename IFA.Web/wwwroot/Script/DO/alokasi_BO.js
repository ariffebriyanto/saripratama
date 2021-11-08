var brgDS = [];
var urlAction = "";
var _Grid;
var detailIndends = [];
var indends = [];
var customerds = [];
var typesearch = false;

var levelList = [
    { text: "", value: "" },
    { text: "A", value: "A" },
    { text: "B", value: "B" },
    { text: "C", value: "C" },
    { text: "D", value: "D" }
];





var optionsGrid = {
    pageSize: 10
};
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

    $.when(getDataiNDEN()).done(function () {
    $.when(getCustomer()).done(function () {
        fillCboCustomer();
       
       
        
            bindGrid();
       
        startSpinner('Loading..', 0);
       // console.log(dataSource);
    });
    });

    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            //console.log(focusable.index(this) + 1);
            //if (focusable.index(this) == 20)
            //{
            //    alert("halo");
            //}
            return false;
        }
    });
});


function ListDropDownEditor(container, options) {
    var input = $('<input id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: tundalist,
        filter: "contains",
        //optionLabel: "Select Barang",

        template: "<span data-id='${data.value}' data-Approval='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var approval = _Grid.dataItem($(e.sender.element).closest("tr"));
            approval.tunda = id;
         
            
            
        }
    }).appendTo(container);
  
}
var tundalist = [
    { text: "", value: "" },
    { text: "No", value: "T" },
    { text: "Yes", value: "Y" }

];

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

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        max: options.model.Qty,
        min: 0
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

function GetDataSave() {
    var saveData = gridData.dataSource.data().toJSON();
    return saveData.filter(
        function (saveData) { return saveData.qty_Alokasi > 0; }
    );
}

function getDataiNDEN() {
    var urlLink = urlGetDataPartial;
    return $.ajax({
        url: urlLink,
        type: "GET",
        success: function (result) {
            brgDS = [];
            brgDS = result;
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

//function valueValidation(container, options) {
//    var input = $("<input name='" + options.field + "'/>");
//    input.appendTo(container);
//    input.kendoNumericTextBox({
//        max: options.model.qty,
//        min: 0
//    });
//}

function Satuandropdown(container, options) {
    $('<input required data-text-field="Kode_Satuan" data-value-field="Kode_Satuan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Kode_Satuan",
            dataValueField: "Kode_Satuan",
            dataSource: SatuanCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("kd_Satuan", dataItem.Kode_Satuan);
            }
        });
}
function rekPernjualandropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_penjualan1", dataItem.rek_persediaan);
            }
        });
}

function rekPernjualan2dropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_penjualan2", dataItem.rek_persediaan);
            }
        });
}

function rekPersediaandropdown(container, options) {

    $('<input data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_persediaan", dataItem.rek_persediaan);
            }
        });

}

function rekHppdropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_hpp", dataItem.rek_persediaan);
            }
        });
}

function rekReturdropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_retur1", dataItem.rek_persediaan);
            }
        });
}

function rekRetur2dropdown(container, options) {
    $('<input required data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_retur2", dataItem.rek_persediaan);
            }
        });
}

function rekBonusdropdown(container, options) {
    $('<input data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_bonus1", dataItem.rek_persediaan);
            }
        });
}

function rekBonus2dropdown(container, options) {
    $('<input data-text-field="nm_rek_persediaan" data-value-field="nm_rek_persediaan" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nm_rek_persediaan",
            dataValueField: "rek_persediaan",
            dataSource: PersediaanCbo,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("rek_bonus2", dataItem.rek_persediaan);
            }
        });
}





function onRequestEnd(e) {
    if (e.type === "create") {
        e.sender.read();
    }
    else if (e.type === "update") {
        e.sender.read();
    }
}


//startSpinner('Loading..', 1);
//$.when(getData()).done(function () {
//    bindGrid();
//    prepareActionGrid();
//    startSpinner('loading..', 0);
//});
//});
function getData1() {
    typesearch = false;
    startSpinner('loading..', 1);
    $('#gvList').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
}

function getData2() {
    typesearch = true;
    startSpinner('loading..', 1);
    $('#gvList').kendoGrid('destroy').empty();
    bindGrid();
    startSpinner('loading..', 0);
}
function getData() {
    var urlLink = urlGetDataInden;
    var filterdata = {
        kd_cust: $("#Kd_Customer").val(),
        DateFrom: $("#tanggalfrom").val(),
        DateTo: $("#tanggalto").val(),

    };

    return $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            startSpinner('loading..', 1);
            brgDS = [];
            brgDS = result;
            //console.log(brgDS);
            $('#gvList').kendoGrid('destroy').empty();
            bindGrid();
            startSpinner('loading..', 0);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}
function stokLabel(container, options) {
    var input = $('<label id="pending" />');
    input.appendTo(container);
}

function bindGrid() {
   
    
    _Grid = $("#gvList").kendoGrid({
        columns: [

            { field: "kode_Barang", "filterable": false, editable: true,title: "kode Barang", width: "70px", hidden: true },
            { field: "jenis_so", "filterable": false, editable: true, title: "Jenis BO", width: "70px" },
            { field: "No_sp", "filterable": false, editable: true,title: "No DO", width: "70px" },
            { field: "nama_Sales", title: "Nama Sales", editable: false, width: "60px" },
            { field: "nama_Barang", title: "Nama Barang", editable: true, width: "120px" },
            { field: "nama_Customer", title: "Customer", editable: true, width: "120px" },
            { field: "tgl_inden", "filterable": false, title: "Tanggal BO", width: "70px", template: "#= kendo.toString(kendo.parseDate(tgl_inden, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "Qty", "filterable": false, title: "Qty BO", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "alokasi",  "filterable": false,   title: "Qty Alokasi", width: "50px", attributes: { style: "background-color: aquamarine", class: "text-right " }, editor: valueValidation, validation: { required: true, min: 0, defaultValue: 0 } },
            { field: "qty_available", "filterable": false, title: "Qty Available", width: "50px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "total_qty", "filterable": false, width: "50px", editable: true, title: "Total", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "stok", "filterable": false, editable: true, width: "50px", title: "Stok Gudang", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "harga", "filterable": false, editable: true, width: "50px", title: "Harga", format: "{0:#,0}", hidden: true, attributes: { class: "text-right " } },

            { field: "total", "filterable": false, editable: true, width: "60px", title: "Total", format: "{0:#,0}", hidden: true, attributes: { class: "text-right " } },
            //{
            //    field: "tunda", title: "Pending", editor: customBoolEditor, width: "50px", template: '<input id="tunda" type="checkbox" #= tunda  # class="chkbx" />', width: 110
            // }],
            //{ field: "tunda", "filterable": false, width: "80px", title: "Pending", editor: ListDropDownEditor }],
            //{ field: "pending", title: "pending", width: "15px", attributes: { class: "text-right " }, editor: stokLabel },
            {
                //field: "tunda", title: "Pending", width: "50px", editor: customBoolEditor,
                field: "tunda", title: "Pending", width: "50px",
                //template: '#=dirtyField(data,"tunda")#<center><input disabled name="tunda" data-type="boolean" data-bind="checked:tunda" onchange="onChkChanged()" type="checkbox" #= tunda ? \'checked="checked"\' : "" # class="chkbx"  /></center>'
                template: "<input name='tunda' class='checkbox' type='checkbox' data-bind='checked: tunda' #= tunda ? checked='checked' : '' #/>"
        }],
        
      editable: true,
        groupable: true,
        sortable: true,
       
        //filterable: {
        //    extra: false,
        //    operators: {
        //        string: { contains: "Contains" }
        //    }
        //},
        resizable: true,
        dataSource: {
            transport: {
                read: function (option) {
                    if(typesearch == false)
                    {
                        var dataku = {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            DateFrom: $("#tanggalfrom").val(),
                            DateTo: $("#tanggalto").val(),
                            kd_cust: null,
                        };
                    } else {
                        var dataku = {
                            skip: option.data.skip,
                            take: option.data.take,
                            pageSize: option.data.pageSize,
                            page: option.data.page,
                            sorting: JSON.stringify(option.data.sort),
                            filter: JSON.stringify(option.data.filter),
                            DateFrom: null,
                            DateTo: null,
                            kd_cust: $("#Kd_Customer").val(),
                        };
                    }
                    $.ajax({
                        url: urlGetDataPartial,
                        data: dataku,
                        //{
                        //    skip: option.data.skip,
                        //    take: option.data.take,
                        //    pageSize: option.data.pageSize,
                        //    page: option.data.page,
                        //    sorting: JSON.stringify(option.data.sort),
                        //    filter: JSON.stringify(option.data.filter),
                        //    DateFrom: $("#tanggalfrom").val(),
                        //    DateTo: $("#tanggalto").val(),
                        //    kd_cust: $("#Kd_Customer").val(),
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
            //    model: {
            //    id: "no_sp",
            //    fields: {
                  
            //        kode_Barang: { type: "string", editable: false },
            //        jenis_so: { type: "string", editable: false },
            //        no_sp: { type: "string", editable: false },
            //        nama_Customer: { type: "string", editable: false },
            //        id: { type: "string", editable: false },
            //        idDisplay: { type: "string", editable: false },
            //        kd_Stok: { type: "string", editable: false },
            //        satuan: { type: "string", editable: false },
            //        qty: { type: "number", editable: false },
            //        qty_available: { type: "number", editable: false },
            //        keterangan: { type: "string", editable: false },
            //        nama_Barang: { type: "string", editable: false },
            //        nama_Sales: { type: "string", editable: false },
            //        total_qty: { type: "number", editable: false },
            //        stok: { type: "number", editable: false },
            //        //totalBeratInden: { type: "number" },
            //        tunda: { type: "string" },
            //        status: { type: "string", editable: false },
            //        kd_sales: { type: "string", editable: false },
            //        tgl_inden: { type: "date", editable: false },
            //        qty_alokasi: { type: "number" }


            //    }
            //}

            },
            pageSize: 100,
        },
        dataBound: function (e) {
            $(".checkbox").bind("change", function (e) {
                
                var grid = $("#gvList").data("kendoGrid");
                var row = $(e.target).closest("tr");
                row.toggleClass("k-state-selected");
                var data = grid.dataItem(row);
                data.set("tunda", this.checked);
                //alert(data.tunda);
            });
        },
        noRecords: true,
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

function customBoolEditor(container, options) {
    var guid = kendo.guid();
    $('<center><input id="tunda" type="checkbox" name="tunda" data-type="boolean" data-bind="checked:tunda" onchange="onChkChanged();"></center>').appendTo(container);
   
    
    
}

//$("#gvList .k-grid-content").on("change", 'input.chkbx[name="tunda"]', function (e) {
//    var grid = $("#gvList").data("kendoGrid"),
//        dataItem = grid.dataItem($(e.target).closest("tr"));

//    dataItem.set("tunda", this.checked);
//});


function onChkChanged() {
    var grid = $("#gvList").data("kendoGrid");
    if ($('#tunda').is(":checked")) {
        

    }
   
 
    
    
}






function dirtyField(data, fieldName) {
    var hasClass = $("[data-uid=" + data.uid + "]").find(".k-dirty-cell").length < 1;
    if (data.dirty && data.dirtyFields[fieldName] && hasClass) {
        return "<span class='k-dirty'></span>"
    }
    else {
        return "";
    }
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


//function bindGrid() {
//    dataSource = new kendo.data.DataSource({
//        transport: {
//            read: {
//                url: urlGetDataInden
//            },
//            update: {
//                //deleteKota(id);
//                type: "POST",
//                url: urlUpdate,
//                dataType: "json",
//                contentType: "application/json",
//                complete: function (e) {
//                    $("#gvList").data("kendoGrid").dataSource.read();
//                }

//            },
//            //destroy: {
//            //    type: "POST",
//            //    url: urlDelete,
//            //    dataType: "json",
//            //    contentType: "application/json",
//            //    complete: function (e) {
//            //        $("#gvList").data("kendoGrid").dataSource.read();
//            //    }

//            //},
//            //create: {
//            //    type: "POST",
//            //    url: urlSave,
//            //    dataType: "json",
//            //    contentType: "application/json",
//            //    complete: function (e) {
//            //        $("#gvList").data("kendoGrid").dataSource.read();
//            //    }

//            //},
//            parameterMap: function (option, operation) {
//                if (operation !== "read") {
//                    return kendo.stringify(option.models);
//                }
//            }
//        },
//        batch: true,
//        pageSize: 20,
//        schema: {
//            model: {
//                id: "no_sp",
//                fields: {
//                  
//                    kode_Barang: { type: "string", editable: false },
//                    jenis_so: { type: "string", editable: false },
//                    no_sp: { type: "string", editable: false },
//                    nama_Customer: { type: "string", editable: false },
//                    id: { type: "string", editable: false },
//                    idDisplay: { type: "string", editable: false },
//                    kd_Stok: { type: "string", editable: false },
//                    satuan: { type: "string", editable: false },
//                    qty: { type: "number", editable: false },
//                    qty_available: { type: "number", editable: false },
//                    keterangan: { type: "string", editable: false },
//                    nama_Barang: { type: "string", editable: false },
//                    nama_Sales: { type: "string", editable: false },
//                    total_qty: { type: "number", editable: false },
//                    stok: { type: "number", editable: false },
//                    //totalBeratInden: { type: "number" },
//                    tunda: { type: "string" },
//                    status: { type: "string", editable: false },
//                    kd_sales: { type: "string", editable: false },
//                    tgl_inden: { type: "date", editable: false },
//                    qty_alokasi: { type: "number" }


//                }
//            }
//        }
//    });

//    _Grid = $("#gvList").kendoGrid({
//        dataSource: dataSource,
//        pageable: true,
//        groupable: true,
//        sortable: true,
//        height: 550,
//        filterable: true,
//        requestEnd: onRequestEnd,
//        //toolbar: ["excel"],
//        //excel: {
//        //    fileName: "ExportBarang.xlsx", allPages: true, Filterable: true
//        //},
//        //toolbar: [{
//        //    name: "create",
//        //    text: "Tambah Barang"

//        //}],
//        toolbar:
//            [
//                //{
//                //    name: "create",
//                //    text: "Tambah Barang"
//                //},
//                //{
//                //    name: "excel",
//                //    text: "Export Excels"
//                //}
//            ],
//        excel: {
//            fileName: "ExportBookingOrder.xlsx", allPages: true, Filterable: true
//        },
//        columns: [
//            { field: "kode_Barang", "filterable": false, title: "kode Barang", width: "70px", hidden: true },
//            { field: "jenis_so", "filterable": false, title: "Jenis BO", width: "70px" },
//            { field: "no_sp", "filterable": false, title: "No DO", width: "100px" },
//            { field: "nama_Sales", title: "Nama Sales", width: "60px" },
//            { field: "nama_Barang", title: "Nama Barang", width: "120px" },
//            { field: "nama_Customer", title: "Customer", width: "150px" },
//            { field: "tgl_inden", "filterable": false, title: "Tanggal BO", width: "70px", template: "#= kendo.toString(kendo.parseDate(tgl_inden, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
//            { field: "qty", "filterable": false, title: "Qty BO", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
//            { field: "alokasi", "filterable": false, title: "Qty Alokasi", width: "100px", attributes: { style: "background-color: aquamarine", class: "text-right " }, editor: valueValidation, validation: { required: true, min: 0, defaultValue: 0 } },
//            { field: "qty_available", "filterable": false, title: "Qty Available", width: "100px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
//            { field: "total_qty", "filterable": false, width: "120px", title: "Total", format: "{0:#,0.00}", attributes: { class: "text-right " } },
//            { field: "stok", "filterable": false, width: "100px", title: "Stok Gudang", format: "{0:#,0.00}", attributes: { class: "text-right " } },
//            { field: "harga", "filterable": false, width: "50px", title: "Harga", format: "{0:#,0}", hidden: true, attributes: { class: "text-right " } },
//            { field: "total", "filterable": false, width: "60px", title: "Total", format: "{0:#,0}", hidden: true, attributes: { class: "text-right " } },
//            { field: "tunda", "filterable": false, width: "80px", title: "Pending", editor: ListDropDownEditor },
//            { command: ["edit"], title: "&nbsp;", width: "250px" }],
//        editable: "inline"
//    }).data("kendoGrid");

//}
function prepareActionGrid() {
    //$(".hapusData").on("click", function () {
    //    var id = $(this).data("id");
    //    deleteKota(id);
    //});
    //$(".editData").on("click", function () {
    //    var id = $(this).data("id");
    //    showForm(id);
    //});
}

function showForm(id) {

    var link = urlForm;
    if (typeof id !== "undefined") {
        link = link + "/" + id;
    }
    //window.location = link;

    $("#editForm").load(link, function () {
        //show spinner
        startSpinner('Loading..', 1);
        $("#wrapperList").css("display", "none");
        $("#editForm").css("display", "");
        if (typeof id !== "undefined") {
            //$("#Kode_Kota").prop("readonly", true);
            $("#save").hide();
            $("#update").show();
        }
        else {
            $("#save").show();
            $("#update").hide();

        }
        //hide spinner
        startSpinner('Loading..', 0);
    });
}

function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

function onSaveClicked() {
    //urlAction = urlUpdate;
    //validationPage();
    save();
}

function onUpdateClicked() {
    urlAction = urlUpdate;
    validationPage();
}

function validationPage() {
    var reWhiteSpace = /\s/g;
    //var Kode_Kota = $('#Kode_Kota').val();
    //var Nama_Kota = $('#Nama_Kota').val();
    //var Keterangan = $('#Keterangan').val();

    //validationMessage = '';
    //if (!Kode_Kota) {
    //    validationMessage = validationMessage + 'Kode Kota harus di isi.' + '\n';
    //}
    //if (!Nama_Kota) {
    //    validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    //}

    //if (!Keterangan) {
    //    validationMessage = validationMessage + 'Nama Kota harus di isi.' + '\n';
    //}

    //if (Kode_Kota.match(reWhiteSpace)) {
    //    validationMessage = validationMessage + 'Kode Kota tidak boleh ada spasi.' + '\n';
    //}
    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        $.when(SaveData()).done(function (x) {
            if (typeof x !== "undefined") {
                $('#gvList').kendoGrid('destroy').empty();
                getData();
                showlist();
            }

        });
    }
}

function save() {
    gridData = $("#gvList").data("kendoGrid");
    //paramValue = gridData.dataSource.data().toJSON();

    //var models = [];
    //gridData.table.find("input[type=checkbox]:checked").each(function () {
    //    var row = $(this).closest("tr");
    //    var model = gridData.dataItem(row);
    //    models.push(model);
    //});

    var saveData = gridData.dataSource.data().toJSON();
    var dataSave = $.grep(saveData, function (v) {
        return v.alokasi > 0;
    });





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
                data: JSON.stringify(dataSave),
                dataType: "json",
                url: urlUpdate,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message

                        });
                    } else {
                        Swal.fire({
                            type: 'success',
                            title: 'Success',
                            html: result.message
                        });

                        $('#GridKota').kendoGrid('destroy').empty();

                        $.when(getDataiNDEN()).done(function () {
                            bindGrid();
                        });
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

function SaveData() {
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
                data: $("#form1").serialize(),
                url: urlAction,
                success: function (result) {
                    if (result.Success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.Message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        Swal.fire({
                            type: 'success',
                            title: 'Success',
                            html: result.Message
                        });
                        $.when(getData()).done(function () {
                            $('#gvList').kendoGrid('destroy').empty();
                            bindGrid();

                            showlist();
                            prepareActionGrid();
                            startSpinner('loading..', 0);
                        });
                    }

                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        } else {
            return false;
        }
    });
}

