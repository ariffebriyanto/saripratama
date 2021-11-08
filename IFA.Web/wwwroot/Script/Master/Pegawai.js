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
                id: "kode_Pegawai",
                fields: {
                    kd_cabang: { type: "string" },
                    nama: { type: "string" },
                    kode_Pegawai: { type: "string", editable: false },
                    alamat_1: { type: "string" },
                    no_Telepon1: { type: "string" },
                    userlogin: { type: "string", validation: { required: true } },
                    idrole: { type: "string", validation: { required: true } },
                    nama_Pegawai: { type: "string", validation: { required: true } }



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
            text: "Tambah Pegawai"
        }],
        columns: [

            { field: "kode_Pegawai", title: "Kode Pegawai", width: "100px", readonly: true },
            { field: "nama_Pegawai", title: "Nama Lengkap", width: "160px" },

            { field: "userlogin", title: "UserLogin", width: "120px" },
            { field: "alamat_1", title: "Alamat", width: "180px" },
            { field: "no_Telepon1", title: "Telp/HP", width: "130px" },
            { field: "nama", title: "Cabang", width: "120px", editor: barangDropDownEditor },

            { field: "akses_penjualan", title: "Penjualan", attributes: { class: "text-right ", 'style': 'background-color: Beige; color:black;' }, width: "100px", editor: ListDropDownEditor },
            { field: "idrole", title: "ROLE", attributes: { class: "text-right ", 'style': 'background-color: Beige; color:black;' }, width: "100px", editor: RoleDropDownEditor },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "160px" }],
        editable: "inline"
    }).data("kendoGrid");
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
    var input = $('<input required id="value" name="text">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "text",
        dataValueField: "text",
        dataSource: Datalist,
        optionLabel: "Pilih...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.value || -1]);
            }
        },

        template: "<span data-id='${data.value}' data-akses_penjualan='${data.text}'>${data.text}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var approval = GridPeg.dataItem($(e.sender.element).closest("tr"));
            approval.akses_penjualan = id;
        }
    }).appendTo(container);
}

var Datalist = [
    //{ text: "", value: "" },
    { text: "CASH", value: "CASH" },

    { text: "NONCASH", value: "NONCASH" }
];

function RoleDropDownEditor(container, options) {
    var input = $('<input required idrole="value" name="nm_role">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nm_role",
        dataValueField: "nm_role",
        dataSource: Rolelist,
        optionLabel: "Pilih...",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.value || -1]);
            }
        },

        template: "<span data-idrole='${data.value}' data-idrole='${data.nm_role}'>${data.nm_role}</span>",
        select: function (e) {
            var idrole = e.item.find("span").attr("data-idrole");
            var approval = GridPeg.dataItem($(e.sender.element).closest("tr"));
            approval.idrole = idrole;
        }
    }).appendTo(container);
}

var Rolelist = [
    { nm_role: "PENJUALAN", value: "PENJUALAN" },
    { nm_role: "LOGISTIK", value: "LOGISTIK" },
    { nm_role: "SPV", value: "SPV" },
    { nm_role: "KASIR", value: "KASIR" },
    { nm_role: "FIN", value: "FIN" }
];

function barangDropDownEditor(container, options) {
    var input = $('<input required id="kd_cabang" name="nama">');
    input.appendTo(container);

    input.kendoDropDownList({
        valuePrimitive: true,
        dataTextField: "nama",
        dataValueField: "nama",
        dataSource: GudangList,
        optionLabel: "Pilih Cabang..",
        filter: "contains",
        virtual: {
            valueMapper: function (options) {
                options.success([options.nama || -1]);
            }
        },
        template: "<span data-id='${data.kd_cabang}' data-Barang='${data.nama}'>${data.nama}</span>",
        select: function (e) {
            var id = e.item.find("span").attr("data-id");
            var Barang = e.item.find("span").attr("data-Barang");
            kd = id;
            var barang = GridPeg.dataItem($(e.sender.element).closest("tr"));
            barang.kd_Cabang = id;
            barang.Kd_Cabang = id;
            barang.nama = Barang;

            //console.log(JSON.stringify(GudangList));
            //var found = GetBarangDetail(id);
            //barang.satuan = found[0].kd_Satuan;
            //barang.stok = found[0].stok;
            //barang.harga = found[0].harga_Rupiah;
            //barang.vol = found[0].vol;



        },
        change: function (e) {
            var dataItem = e.sender.dataItem();
            options.model.set("Kd_Cabang", dataItem.kd_cabang);
        }
    }).appendTo(container);
}


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
            console.log(brgDS);
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
    var akses = $('#akses_penjualan').val();
    var nama_Pegawai = $('#nama_Pegawai').val();
    var userlogin = $('#userlogin').val();
    var id_role = $('#idrole').val();
    var cbg = $('#nama').val();

    //validationMessage = '';
    if (id_role === "Pilih...") {
        validationMessage = validationMessage + 'Id Role Pegawai harus di isi.' + '\n';
    }
    if (userlogin === "Pilih...") {
        validationMessage = validationMessage + 'isi User Login.' + '\n';
    }
    if (!nama_Pegawai) {
        validationMessage = validationMessage + 'Nama Pegawai harus di isi.' + '\n';
    }

    if (!userlogin) {
        validationMessage = validationMessage + 'userlogin Kota harus di isi.' + '\n';
    }
    if (cbg === "Pilih Cabang..") {
        validationMessage = validationMessage + 'userlogin Kota harus di isi.' + '\n';
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
