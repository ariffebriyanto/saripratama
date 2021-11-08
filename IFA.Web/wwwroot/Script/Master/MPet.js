var brgDS = [];
var jnsDS = [];
var urlAction = "";
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
                //deleteKota(id);
                type: "POST",
                url: urlUpdate,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPet").data("kendoGrid").dataSource.read();
                }

            },
            destroy: {
                type: "POST",
                url: urlDelete,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPet").data("kendoGrid").dataSource.read();
                }

            },
            create: {
                type: "POST",
                url: urlSave,
                dataType: "json",
                contentType: "application/json",
                complete: function (e) {
                    $("#GridPet").data("kendoGrid").dataSource.read();
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
                id: "kd_Pet",
                fields: {
                    kd_Pet: { editable: false, nullable: true },
                    nama_Pet: { validation: { required: true } },
                    jenisPet: { type: "string" },
                    harga_Pet: { type: "string" },
                    namaOwner: { type: "string" }
                }
            }
        }
    });

    $("#GridPet").kendoGrid({
        dataSource: dataSource,
        pageable: true,
        groupable: true,
        sortable: true,
        height: 550,
        filterable: true,
        requestEnd: onRequestEnd,
        //toolbar: ["excel"],
        //excel: {
        //    fileName: "ExportBarang.xlsx", allPages: true, Filterable: true
        //},
        //toolbar: [{
        //    name: "create",
        //    text: "Tambah Barang"

        //}],
        toolbar:
            [{
                name: "create",
                text: "Tambah Pet"
            },
            {
                name: "excel",
                text: "Export Excels"
            }],
        excel: {
            fileName: "ExportOwner.xlsx", allPages: true, Filterable: true
        },
        columns: [
            "kd_Pet",
            { field: "nama_Pet", title: "Nama Pet", filterable: true, width: "250px" },
            { field: "jenisPet", title: "Jenis Pet", filterable: true, width: "250px", editor: Jenisdropdown },
            { field: "harga_Pet", title: "Harga Pet", filterable: true, width: "250px", attributes: { class: "text-right " }, format: "{0:#,0}" },
            { field: "namaOwner", title: "Owner", width: "250px", filterable: true, editor: ownerDropdown },
            ////{ field: "berat", title: "Berat", width: "50px" },


            //{ field: "nm_rek_penjualan1", title: "Rekening Penjualan 1", width: "120px", editor: rekPernjualandropdown },
            //{ field: "nm_rek_penjualan2", title: "Rekening Penjualan 2", width: "120px", editor: rekPernjualan2dropdown },
            //{ field: "nm_rek_persediaan", title: "Rekening Persediaan", width: "120px", editor: rekPersediaandropdown },
            //{ field: "nm_rek_hpp", title: "Rekening Hpp", width: "120px", editor: rekHppdropdown },
            //{ field: "nm_rek_retur1", title: "Rekening Retur 1", width: "120px", editor: rekReturdropdown },
            //{ field: "nm_rek_retur2", title: "Rekening Retur 2", width: "120px", editor: rekRetur2dropdown },
            //{ field: "nm_rek_bonus1", title: "Rekening Bonus 1", width: "120px", editor: rekBonusdropdown },
            //{ field: "nm_rek_bonus2", title: "Rekening Bonus 2", width: "120px", editor: rekBonus2dropdown },
            { command: ["edit", "destroy"], title: "&nbsp;", width: "250px" }],
        editable: "inline"
    });
    console.log(dataSource);
});

function textAreaEditor(container, options) {
    $('<textarea class="k-textbox" name="' + options.field + '" style="width:100%;height:100%;" />').appendTo(container);
}

function Jenisdropdown(container, options) {
    jnsDS = [];
    const jeniss = ["Mamalia", "Unggas", "Reptil"];
    for (i = 0; i < jeniss.length; i++) {
       // console.log(numbers[i]);
        jnsDS.push({
            kode_jenis: i+1,
            nama_jenis: jeniss[i]
            

        });
    }
    

    $('<input required data-text-field="nama_jenis" data-value-field="kode_jenis" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "nama_jenis",
            dataValueField: "kode_jenis",
            dataSource:jnsDS,
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("jenis_Pet", dataItem.kode_jenis);
            }
        });
}



function ownerDropdown(container, options) {



    $('<input data-text-field="Nama_Owner" data-value-field="Nama_Owner" data-bind="value:' + options.field + '"/>')
        .appendTo(container)
        .kendoDropDownList({
            dataTextField: "Nama_Owner",
            dataValueField: "Kd_Owner",
            dataSource: CboOwner,
            optionLabel: "Please Select",
            change: function (e) {
                var dataItem = e.sender.dataItem();
                options.model.set("Kd_Owner", dataItem.Kd_Owner);
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
function customBoolEditor(container, options) {
    var guid = kendo.guid();
    $('<input class="k-checkbox" id="' + guid + '" type="checkbox" name="Discontinued" data-type="boolean" data-bind="checked:Discontinued">').appendTo(container);
    $('<label class="k-checkbox-label" for="' + guid + '">&#8203;</label>').appendTo(container);
}

//startSpinner('Loading..', 1);
//$.when(getData()).done(function () {
//    bindGrid();
//    prepareActionGrid();
//    startSpinner('loading..', 0);
//});
//});


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

function bindGrid() {
    $("#GridPet").kendoGrid({
        columns: [

            { field: "kd_Pet", title: "Kode Pet", width: "100px" },
            { field: "nama_Pet", title: "Nama Pet", width: "250px" },
            { field: "jenisPet", title: "Jenis Pet", width: "250px" },
            { field: "harga_Pet", title: "harga Pet", width: "250px" },
            { field: "namaOwner", title: "Owner", width: "250px" },
            { field: "Action", width: "200px", template: '<center><a class="btn btn-danger btn-sm hapusData" href="javascript:void(0)" data-id="#=kd_Pet#"><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a> &nbsp; &nbsp; <a class="btn btn-info btn-sm editData" href="javascript:void(0)" data-id="#=kd_Pet#"><i class="glyphicon glyphicon-pencil" aria-hidden="true"></i></a></center>' },

        ],
        dataSource: {
            data: brgDS,
            schema: {
                model: {
                    id: "kd_Pet",
                    fields: {
                        kd_Pet: { type: "string" },
                        nama_Pet: { type: "string" },
                        harga_Pet: { type: "string" },
                        namaOwner: { type: "string" },
                        kd_Owner: { type: "string" }
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
        noRecords: true
    }).data("kendoGrid");

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
            $("#Kd_Owner").prop("readonly", true);
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
    urlAction = urlSave;
    validationPage();
}

function onUpdateClicked() {
    urlAction = urlUpdate;
    validationPage();
}

function validationPage() {
    var reWhiteSpace = /\s/g;
    
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
                $('#GridKota').kendoGrid('destroy').empty();
                getData();
                showlist();
            }

        });
    }
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
                            $('#GridKota').kendoGrid('destroy').empty();
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

function deleteKota(id) {
    swal({
        type: 'warning',
        title: 'Are you sure?',
        html: 'You want to delete this data',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d9534f'
    }).then(function (isConfirm) {
        if (isConfirm.value === true) {
            startSpinner('loading..', 1);
            $.when(deleteAjax(id)).done(function () {

                startSpinner('loading..', 0);
            });
        }
    });
}

function deleteAjax(id) {
    var urlDeleteLink = urlDelete + "/" + id;
    return $.ajax({
        type: "POST",
        url: urlDeleteLink,
        success: function (result) {
            $('#GridKota').kendoGrid('destroy').empty();
            $.when(getData()).done(function () {
                bindGrid();

                showlist();
                prepareActionGrid();
                startSpinner('loading..', 0);
            });
        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}