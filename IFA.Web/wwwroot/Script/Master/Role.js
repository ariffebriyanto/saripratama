var brgDS = [];
var urlAction = "";
var GridPeg;
var optionsGrid = {
    pageSize: 20
};
$(document).ready(function () {
    dataSource = new kendo.data.DataSource({
        transport: {
            read: {
                url: urlGetData
            },
            update: {

                type: "POST",
                url: urlUpdate,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPeg").data("kendoGrid").dataSource.read();
                }

            },
            destroy: {
                type: "POST",
                url: urlDelete,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPeg").data("kendoGrid").dataSource.read();
                }

            },
            create: {
                type: "POST",
                url: urlSave,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPeg").data("kendoGrid").dataSource.read();
                }

            },
            parameterMap: function (option, operation) {
                if (operation !== "read") {
                    return kendo.stringify(option.models);
                }
            }
        },
        batch: true,
        pageSize: 20,
        schema: {
            model: {
                id: "IDUSER",
                fields: {
                    iduser: { type: "string", validation: { required: true } },
                    iduser: { type: "string", validation: { required: true } }



                }
            }
        }
    });

    GridPeg = $("#GridPeg").kendoGrid({
        dataSource: dataSource,
        groupable: true,
        sortable: true,
        filterable: {
            extra: false,
            operators: {
                string: { contains: "Contains" }
            }
        },
        toolbar: ["excel"],
        excel: {
            fileName: "ExportHarga.xlsx", allPages: true, Filterable: true
        },
        pageable: true,
        height: 550,
        requestEnd: onRequestEnd,
        toolbar: [{
            name: "create",
            text: "Tambah Role"
        }],
        columns: [
            { field: "iduser", title: "USER", width: "100px", readonly: true  }, //, editor: barangDropDownEditor
            { field: "idrole", title: "Role", width: "100px", editor: ListDropDownEditor },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "70px" }],
        editable: "inline"
    }).data("kendoGrid");
    //console.log(JSON.stringify(dataSource));
    //console.log(dataSource);

    //$("#cabang_gudang").kendoDropDownList({
    //    dataTextField: "nama",
    //    dataValueField: "kd_cabang",
    //    filter: "contains",
    //    dataSource: GudangList,
    //    value: GudangUser,
    //    change: changegudang,
    //    enable: false,
    //    optionLabel: "Please Select"
    //});
});

function ListDropDownEditor(container, options) {
    var input = $('<input id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: Datalist,
        filter: "contains",
        //optionLabel: "Select Barang",

        template: "<span data-id='${data.value}' data-Approval='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var approval = GridPeg.dataItem($(e.sender.element).closest("tr"));
            approval.idrole = id;
        }
    }).appendTo(container);
}

var Datalist = [
    { text: "", value: "" },
    { text: "PENJUALAN", value: "PENJUALAN" },
    { text: "UAT", value: "UAT" },
    { text: "SPV", value: "SPV" },
    { text: "SPV1", value: "SPV1" }
];

//function barangDropDownEditor(container, options) {
//    var input = $('<input required id="nama" name="nama">');
//    input.appendTo(container);

//    input.kendoDropDownList({
//        valuePrimitive: true,
//        dataTextField: "nama",
//        dataValueField: "nama",
//        dataSource: GudangList,
//        optionLabel: "Select Cabang...",
//        filter: "contains",
//        virtual: {
//            valueMapper: function (options) {
//                options.success([options.nama || 0]);
//            }
//        },
//        template: "<span data-id='${data.kd_Cabang}' data-Barang='${data.nama}'>${data.nama}</span>",
//        select: function (e) {
//            var id = e.item.find("span").attr("data-id");
//            var Barang = e.item.find("span").attr("data-Barang");
//            kd = id;
//            var barang = GridPeg.dataItem($(e.sender.element).closest("tr"));
//            barang.kd_Cabang = id;
//            barang.nama = Barang;
//            console.log(JSON.stringify(GudangList));
//            //var found = GetBarangDetail(id);
//            //barang.satuan = found[0].kd_Satuan;
//            //barang.stok = found[0].stok;
//            //barang.harga = found[0].harga_Rupiah;
//            //barang.vol = found[0].vol;



//        }
//    }).appendTo(container);
//}


function onRequestEnd(e) {
    if (e.type === "create") {
        e.sender.read();
    }
    else if (e.type === "update") {
        e.sender.read();
    }
}

function getData() {
    var urlLink = urlGetData;

    return $.ajax({
        url: urlLink,
        success: function (result) {
            brgDS = [];
            brgDS = result;
            console.log(JSON.stringify(brgDS));
            console.log(JSON.stringify(result));
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function prepareActionGrid() {
    $(".hapusData").on("click", function () {
        var id = $(this).data("id");
        deleteKota(id);
    });
    $(".editData").on("click", function () {
        var id = $(this).data("id");
        showForm(id);
    });
}


function showlist() {
    $("#editForm").css("display", "none");
    $("#wrapperList").css("display", "");
}

function onSaveClicked() {
    urlAction = urlSave;
    validationPage();
}

function onUpdateClicked() {
    urlAction = urlUpdate;
    validationPage();
}

function validationPage() {
    var reWhiteSpace = /\s/g;
    // var kode_Pegawai = $('#kode_Pegawai').val();
    var id_role = $('#idrole').val();
    var id_user = $('#iduser').val();
    // var Keterangan = $('#Keterangan').val();

    //validationMessage = '';
    //if (!kode_Pegawai) {
    //    validationMessage = validationMessage + 'Kode Pegawai harus di isi.' + '\n';
    //}
    if (!id_role) {
        validationMessage = validationMessage + 'id_user  harus di isi.' + '\n';
    }

    if (!id_user) {
        validationMessage = validationMessage + 'id_user harus di isi.' + '\n';
    }

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
                $('#GridPeg').kendoGrid('destroy').empty();
                Swal.fire({
                    type: 'success',
                    title: 'Success',
                    html: "Data Tersimpan.."
                });
                getData();
                showlist();
            }

        });
    }
}
