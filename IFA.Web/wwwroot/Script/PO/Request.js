var pods = [];
var POds = [];
var urlAction = "";
var barangList = [];
var satuanList = [];
var _GridPO;
var columngrid = [];
var listBarang = [];
var SatuanList = [];
var SatuanListParam = [];
var optionsGrid = {
    pageSize: 10
};
var _GridPO;

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
    $('#tanggalfrom').val(startdateserver);
    $('#tanggalto').val(enddateserver);

    $.when(getData()).done(function () {
        $.when(GetBarang()).done(function () {
            $.when(GetSatuan()).done(function () {
                bindGrid();
                columngrid = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                    { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { command: ["edit", "destroy"], title: "Actions", width: "110px" }

                ];
                prepareActionGrid();
                startSpinner('loading..', 0);
            });
        });
    });

    getData();
    $('body').on('keydown', 'input, select, span, .k-dropdown', function (e) {
        if (e.key === "Enter") {

            var self = $(this), form = self.parents('form:eq(0)'), focusable, next;
            focusable = form.find('input,a,select,button,textarea, .k-dropdown').filter(':visible');
            next = focusable.eq(focusable.index(this) + 1);
            next.focus();
            return false;
        }
    });
});

$(document).on('keydown', function (event) {
    if (event.key == "F2") {
        onAddNewRow();
        return false;
    }
});

function prepareActionGrid() {
    $(".viewData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        showForm(id);
       
    });
    $(".editData").on("click", function (event) {
        event.stopPropagation();
        event.stopImmediatePropagation();
        var id = $(this).data("id");
        showForm(id);

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

function GetSatuan() {
   return $.ajax({
        url: urlSatuan,
        type: "POST",
        success: function (result) {
            SatuanList = result;
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function fillBarang() {
    $("#Kd_Stok").empty();
    $("#Kd_Stok").append('<option value="" selected disabled>Pilih Barang</option>');
    for (var i = 0; i < listBarang.length; i++) {
        $("#Kd_Stok").append('<option value="' + listBarang[i].kode_Barang + '">' + listBarang[i].nama_Barang + '</option>');
    }
    $('#Kd_Stok').selectpicker('refresh');
    $('#Kd_Stok').selectpicker('render');
}

function onCboBarangOnChange() {
    startSpinner('loading..', 1);
    var barangval = {
        kode_barang: $("#Kd_Stok").val()
    }
    $.ajax({
        url: urlSatuan,
        type: "POST",
        data: barangval,
        success: function (result) {
            console.log(result);
            $("#Satuan").empty();
            var data = result;

            for (var i = 0; i < data.length; i++) {
                $("#Satuan").append('<option value="' + data[i].kode_Satuan + '">' + data[i].nama_Satuan + '</option>');
            }
          
            $('#Satuan').selectpicker('refresh');
            $('#Satuan').selectpicker('render');
            startSpinner('loading..', 0);
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function getData() {

    var urlLink = urlGetData;
    
  return  $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            //if (_GridPO) {
            //    $('#GridPO').kendoGrid('destroy').empty();
            //}
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
            // { selectable: true, width: "40px", title: "Print", headerTemplate: '<label style="vertical-align:bottom">Cetak</label>' },
            { field: "rec_stat", title: "Status", width: "80px", attributes: { class: "text-center borderStatus", 'style': "#if(rec_stat == 'OUTSTANDING'){#background-color: cornflowerblue;color: white;#}else if(rec_stat == 'BATAL'){#background-color: orangered;color: white;#}else if(rec_stat == 'CLOSE'){#background-color: green;color: white;#}else if(rec_stat == 'OPEN'){#background-color: deepskyblue;color: black;#}else if(rec_stat == 'ENTRY'){#background-color: aquamarine;color: black;#}else if(rec_stat == 'REVISE'){#background-color: lightpink;color: black;#}#" } },
            { field: "no_DPM", title: "Request Number", width: "130px" },
            { field: "no_po", title: "PO Number", width: "130px" },
            { field: "nama_Barang", title: "Nama Barang", width: "180px" },
            { field: "satuan", title: "Satuan", width: "80px" },
            { field: "tgl_Diperlukan", title: "Tanggal Diperlukan", width: "120px", template: "#= kendo.toString(kendo.parseDate(tgl_Diperlukan, 'yyyy-MM-dd'), 'dd MMMM yyyy') #" },
            { field: "qty", title: "Qty Request", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            { field: "qty_PR", title: "Qty Approve", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "qty_received", title: "Qty Diterima", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } },
            //{ field: "qty_sisa", title: "Qty Sisa", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " } }
            { field: "Action", width: "100px", template: "#if(rec_stat != 'REVISE'){#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-success btn-sm viewData' href='javascript:void(0)' data-id='#=no_DPM#'><i class='glyphicon glyphicon-eye-open' aria-hidden='true'></i></a></center>#}else{#&nbsp&nbsp<center style='display:inline;'><a class='btn btn-info btn-sm editData' href='javascript:void(0)' data-id='#=no_DPM#'><i class='glyphicon glyphicon-pencil' aria-hidden='true'></i></a></center>#}#&nbsp&nbsp<center style='display:inline;'></center>" }

        ],
        dataSource: {
            data: pods,
            schema: {
                model: {
                    id: "no_DPM",
                    fields: {
                        no_DPM: { type: "string" },
                        no_po: { type: "string" },
                        kd_Stok: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number" },
                        qty_PR: { type: "number" },
                        qty_received: { type: "number" },
                        qty_sisa: { type: "number" },
                        rec_stat: { type: "string" },
                        tgl_Diperlukan: { type: "date" },
                        keterangan: { type: "string" },
                        nama_Barang: { type: "string" }

                    }
                }
            },
            pageSize: optionsGrid.pageSize
        },
        pageable: {
            pageSizes: [5, 10, 20, 100],
            change: function () {
                  prepareActionGrid();
            }
        },
        dataBound: function () {
            prepareActionGrid();
        },
        noRecords: true,
        height: 350,
        scrollable:true
    }).data("kendoGrid");

}



function showForm(id) {
    startSpinner('Loading..', 1);

    var link = urlForm;
    if (typeof id !== "undefined") {
        link = link + "/" + id;
    }
    $("#editForm").load(link, function () {
        //show spinner
        $("#wrapperList").css("display", "none");
        $("#editForm").css("display", "");

        $('.selectpicker').selectpicker({
            liveSearch: true
        });

        $(".selectpicker").selectpicker('refresh');
        $(".selectpicker").selectpicker('render');

        $('#divTgl_Diperlukan').datepicker({
            format: 'dd MM yyyy',
            todayBtn: 'linked',
            "autoclose": true
        });
        $('#Tgl_Diperlukan').val(dateserver);
        POds = [];

        if (typeof id !== "undefined") {
            if (Mode != "REVISE") {
                columngrid = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                    { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" }
                ];
                $('#Tgl_Diperlukan').attr("disabled", "disabled");
                $('#Keterangan').attr("disabled", "disabled");
            } else {
                columngrid = [
                    { field: "nama_Barang", title: "Nama Barang", width: "160px", editor: barangDropDownEditor },
                    { field: "satuan", title: "Satuan", width: "90px", editor: satuanDropDownEditor },
                    { field: "qty", title: "Qty", width: "80px", format: "{0:#,0.00}", attributes: { class: "text-right " }, aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>" },
                    { command: ["edit", "destroy"], title: "Actions", width: "110px" }
                ];
            }

            if (DetailList != null) {
                for (var i = 0; i <= DetailList.length - 1; i++) {
                    POds.push({
                        kd_stok:DetailList[i].kd_Stok,
                        Kode_Barang:DetailList[i].kd_Stok,
                        Nama_Barang:DetailList[i].nama_barang,
                        nama_Barang:DetailList[i].nama_barang,
                        Nama_Satuan:DetailList[i].Satuan,
                        kd_satuan:DetailList[i].kd_satuan,
                        satuan:DetailList[i].Satuan,
                        qty: DetailList[i].Qty,
                        no_dpm: DetailList[i].no_dpm

                    })
                    $('#Tgl_Diperlukan').val(DetailList[i].Tgl_Diperlukan);

                }
            }
        }
        bindGridDetail();

        //hide spinner
        startSpinner('Loading..', 0);
    });
}

function onSaveClicked() {
    validationPage();
}

function validationPage() {
    var Tgl_Diperlukan = $('#Tgl_Diperlukan').val();
    var Keterangan = $('#Keterangan').val();
    var ItemData = _GridPO.dataSource.data();
    validationMessage = '';
    if (!Tgl_Diperlukan) {
        validationMessage = validationMessage + 'Tanggal Diperlukan harus di pilih.' + '\n';
    }

    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Tambahkan Item.' + '\n';
    }

    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    }
    else {
        SaveData();
    }
}

function SaveData() {
    var dataRequest = {
        Tgl_Diperlukan: $('#Tgl_Diperlukan').val(),
        Keterangan: $('#Keterangan').val(),
        podetail: _GridPO.dataSource.data().toJSON()
    };
   // console.log(dataRequest);
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
                data: dataRequest,
                url: urlSave,
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
                        $('#GridPO').kendoGrid('destroy').empty();
                        $.when(getData()).done(function () {
                            bindGrid();
                            showlist();
                          //  prepareActionGrid();
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

function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

function bindGridDetail() {
    _GridPO = $("#GridPODetail").kendoGrid({
        columns: columngrid,
        dataSource: {
            data: POds,
            schema: {
                model: {
                    fields: {
                        //no: { type: "string" },
                        kd_stok: { type: "string" },
                        Kode_Barang: { type: "string" },
                        Nama_Barang: { type: "string" },
                        nama_Barang: { type: "string" },
                        Kode_satuan: { type: "string" },
                        Nama_Satuan: { type: "string" },
                        kd_satuan: { type: "string" },
                        satuan: { type: "string" },
                        qty: { type: "number", validation: { required: true, min: 1, defaultValue: 0 } },
                        keterangan: { type: "string" },
                        no_dpm: { type: "string" }
                    }
                }
            },
            aggregate: [
                { field: "qty", aggregate: "sum" }
            ]
        },
        edit: function (e) {
            addCustomCssButtonCommand();
            var dropdownlist = $("#Kode_Barang").data("kendoDropDownList");
            dropdownlist.list.width("400px");
            var index = listBarang.findIndex(function (item, i) {
                return item.kode_Barang === e.model.Kode_Barang;
            });
            dropdownlist.select(index);

            var found = GetSatuanCode(e.model.Kode_Barang);
            SatuanListParam = [];
            for (var i = 0; i < SatuanList.length; i++) {
                if (found[0] != undefined) {
                    if (SatuanList[i].kode_Satuan === found[0].kd_Satuan) {
                        var Key = SatuanList[i].kode_Satuan;
                        var Value = SatuanList[i].nama_Satuan;

                        SatuanListParam.push({
                            Kode_Satuan: Key,
                            Nama_Satuan: Value
                        });
                    }
                }
            }
            inputSatuan.setDataSource(SatuanListParam);
            inputSatuan.refresh();
            inputSatuan.enable(true);
            var indexSatuan = SatuanListParam.findIndex(function (item, i) {
                return item.Kode_Satuan === e.model.Kode_satuan;
            });
            if (indexSatuan == "-1") {
                indexSatuan = SatuanListParam.findIndex(function (item, i) {
                    return item.Kode_Satuan === e.model.Nama_Satuan;
                });
            }
            inputSatuan.select(indexSatuan);

            if (e.model.pdm != "" && e.model.pdm != undefined) {
                var qty = e.container.find("input[name=qty]").data("kendoNumericTextBox");
                qty.enable(false);
            }
        },
        save: function (e) {
            
        },
        cancel: function (e) {
            $('#GridPODetail').data('kendoGrid').dataSource.cancelChanges();
        },
        dataBinding: function (e) {

        },
        noRecords: true,
        editable: "inline",
        dataBound: onDataBound
    }).data("kendoGrid");

}

function onDataBound(e) {
    addCustomCssButtonCommand();
}

function addCustomCssButtonCommand() {
    $(".k-grid-edit").removeClass("k-button k-button-icontext ");
    $(".k-grid-edit").addClass("btn btn-info colorWhite marginRight10 font10 padding79");
    $('.k-grid-edit').find('span').remove();

    $(".k-grid-delete").removeClass("k-button k-button-icontext");
    $(".k-grid-delete").addClass("btn btn-danger colorWhite  font10 padding79");
    $('.k-grid-delete').find('span').remove();

    $(".k-grid-update").removeClass("k-button k-primary k-button-icontext");
    $(".k-grid-update").addClass("btn btn-info colorWhite marginRight10 font10 padding79");
    $('.k-grid-update').find('span').remove();

    $(".k-grid-cancel").removeClass("k-button k-button-icontext");
    $(".k-grid-cancel").addClass("btn btn-danger colorWhite  font10 padding79");
    $('.k-grid-cancel').find('span').remove();
}

function barangDropDownEditor(container, options) {
    var input = $('<input required id="Kode_Barang" name="Nama_Barang">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama_Barang",
        dataValueField: "nama_Barang",
        dataSource: listBarang,
        optionLabel: "Select Barang...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.nama_Barang || 0]);
            }
        },
        template: "<span data-id='${data.kode_Barang}' data-Barang='${data.nama_Barang}'>${data.nama_Barang} </span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            var barang = _GridPO.dataItem($(e.sender.element).closest("tr"));
            barang.kd_stok = id;
            barang.nama_Barang = Barang;
            barang.Kode_Barang = id;
            barang.Nama_Barang = Barang;
            barang.nama_barang = Barang;

            SatuanListParam = [];
            var found = GetSatuanCode(id);
            //barang.last_price = found[0].last_price;
            for (var i = 0; i < SatuanList.length; i++) {
                if (SatuanList[i].kode_Satuan === found[0].kd_Satuan) {
                    var Key = SatuanList[i].kode_Satuan;
                    var Value = SatuanList[i].nama_Satuan;

                    SatuanListParam.push({
                        Kode_Satuan: Key,
                        Nama_Satuan: Value
                    });
                }
            }
            inputSatuan.setDataSource(SatuanListParam);
            ////$("#grid").data("kendoGrid").setData(ds);
            ////inputChannel.setd(channelListParam);
            inputSatuan.refresh();
            inputSatuan.enable(true);
            inputSatuan.select(1);
            $("#Kode_Satuan").data('kendoDropDownList').value(SatuanListParam[0].Kode_Satuan);

            var satuan = _GridPO.dataItem($(e.sender.element).closest("tr"));
            satuan.kd_satuan = SatuanListParam[0].Kode_Satuan;
            satuan.satuan = SatuanListParam[0].Nama_Satuan;
            satuan.Kode_satuan = SatuanListParam[0].Kode_Satuan;
            satuan.Kode_Satuan = SatuanListParam[0].Kode_Satuan;
            satuan.Nama_Satuan = SatuanListParam[0].Nama_Satuan;

            //Kode_Satuan
        }
    }).appendTo(container);
}

function convertValues(value) {
    var data = {};

    value = $.isArray(value) ? value : [value];

    for (var idx = 0; idx < value.length; idx++) {
        data["values[" + idx + "]"] = value[idx];
    }

    return data;
}

function GetSatuanCode(code) {
    return listBarang.filter(
        function (listBarang) { return listBarang.kode_Barang === code; }
    );
}

function satuanDropDownEditor(container, options) {
    inputSatuan = $("<input required  id='Kode_Satuan' name='Nama_Satuan'  />")
        .attr("Kode_Satuan", "Nama_Satuan")
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Nama_Satuan",
            dataValueField: "Nama_Satuan",
            dataSource: SatuanListParam,
            //optionLabel: "Pilih Satuan",
            template: "<span data-id='${data.Kode_Satuan}' data-Satuan='${data.Nama_Satuan}'>${data.Nama_Satuan}</span>",
            select: function (e) {
                var id = e.item.find("span").attr("data-id");
                var Satuan = e.item.find("span").attr("data-Satuan");
                var satuan = _GridPO.dataItem($(e.sender.element).closest("tr"));
                satuan.kd_satuan = id;
                satuan.satuan = Satuan;
                satuan.Kode_satuan = id;
                satuan.Kode_Satuan = id;
                satuan.Nama_Satuan = Satuan;
            }
        }).data("kendoDropDownList");
}

function onAddNewRow() {
    startSpinner('Loading..', 1);
    var grid = $("#GridPODetail").data("kendoGrid");
    grid.addRow();
    $('#Kode_Barang').data("kendoDropDownList").open();
    startSpinner('Loading..', 0);
}