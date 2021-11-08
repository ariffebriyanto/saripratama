var SiapkirimDs = [];

$(document).ready(function () {

    //fillCbonotrans();
    $('#divtanggal').datepicker({
        format: 'dd MM yyyy',
        todayBtn: 'linked',
        "autoclose": true
    }).on('changeDate', function (selected) {
        var datenow = new Date(selected.date.valueOf());
        $('#divtanggal').datepicker('getDate', datenow);
        });
    $('#tanggal').val(dateserver);

    if (Mode === "NEW") {
        $("#no_rcnkirim").kendoDropDownList({
            dataTextField: "no_trans",
            dataValueField: "no_trans",
            filter: "contains",
            dataSource: Norcnkirim,
            change: onChange,
            optionLabel: "Please Select"

        }).closest(".k-widget");
    }
  

    if (Mode !== "NEW") {
        GetDataSiapKirim(idSiapKirim);
    }


    //console.log([SiapkirimDs]);
    bindGrid();
   



});

function onChange(e) {
    var dataItem = e.sender.dataItem();
    if (dataItem) {
        //output selected dataItem
        $("#Supplier").val(dataItem.Nama_Supplier);
        $("#p_np").val(dataItem.p_np);
        $("#no_ref").val(dataItem.no_po);
    }
    var urlLink = urlGetDataDs;
    startSpinner('Loading..', 1);
    $.ajax({
        url: urlLink,
        dataType: "json",
        type: "POST",
        data: {
            no_trans: $("#no_rcnkirim").val()
        },
        success: function (result) {
     
            $('#GridSiapKirim').kendoGrid('destroy').empty();
    
            SiapkirimDs = result;
            //console.log(SiapkirimDs);
            bindGrid();
            startSpinner('Loading..', 0);
        }

    });
}

function bindGrid() {

    _GridSiapKirim = $("#GridSiapKirim").kendoGrid({
        columns: [
            { field: "no", title: "No", width: "30px", template: "<span class='row-number'></span>" },
            { field: "no_sp", title: "No SO", width: "90px" },
            { field: "atas_nama", title: "Nama Customer", width: "220px" },
            { field: "deskripsi", title: "Nama Barang", width: "250px" },
            { field: "kd_satuan", title: "Satuan ", width: "50px" },
            { field: "jumlah", title: "Qty SPM", width: "50px", aggregates: ["sum"], footerTemplate: "<div align=right>#= kendo.toString(sum, \"n2\") #</div>"},
            { field: "qty_out", title: "Qty SJ", width: "50px", footerTemplate: "Sum: #= sum # ", validation: { required: true, min: 1, defaultValue: 0 }, attributes: { class: "text-right " }, "footerTemplate": "Total: #: data.qty_out ? data.qty_out.sum: 0 #", footerAttribute: { "id": "qty_out" }, editor: valueValidation },
            { field: "sisa", title: "Sisa", hidden: true }
        ],
        dataSource: {
            data: SiapkirimDs,
            schema: {
                model: {
                    id: "no_po",
                    fields: {
                        no: { type: "string", editable: false },
                       
                        atas_nama: { type: "string", editable: false },
                        no_sp: { type: "string", editable: false },
                        no_sp_dtl: { type: "string", editable: false },
                        kd_stok: { type: "string", editable: false },
                        kd_satuan: { type: "string", editable: false },
                        deskripsi: { type: "string", editable: false },
                        kd_Barang: { type: "string", editable: false },
                      
                        jumlah: { type: "number", editable: false }, 
                        qty_out: { type: "number", editable: true },
                        sisa: { type: "number", editable: true },
                        harga: { type: "number", editable: true },
                        rek_persediaan: { type: "string", editable: false }

                    }
                }
            },

            change: function (e) {
                //if (e.field === "qty_in") {
                //    // The total is Prix (price) * Quantite (Quantity)
                //    var newTotal = e.items[0].harga * e.items[0].qty_in;
                //    console.log("Nouveau total : " + newTotal);
                //    e.items[0].set("rp_trans", newTotal);

                //}
            },
            aggregate: [
                { field: "rp_trans", aggregate: "sum" },
                { field: "qty_out", aggregate: "sum" },
                { field: "jumlah", aggregate: "sum" }
            ]


        },

        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        },


        noRecords: true,


        editable: false
    }).data("kendoGrid");

}

function onSaveClicked() {
   // SaveData();
    validationpage();
}

function validationpage() {
    var no_rcnkirim = $("#no_rcnkirim").val();
    var ItemData = _GridSiapKirim.dataSource.data();
    var validationMessage = '';
    if (!no_rcnkirim) {
        validationMessage = validationMessage + 'Rencana Kirim harus di pilih.' + '\n';
    }

    if (ItemData.length <= 0) {
        validationMessage = validationMessage + 'Item Tidak booleh kosong.' + '\n';
    }

    if (validationMessage) {
        Swal.fire({
            type: 'error',
            title: 'Warning',
            html: validationMessage
        });
    } else {
        SaveData();
    }
}

function valueValidation(container, options) {
    var input = $("<input name='" + options.field + "'/>");
    input.appendTo(container);
    input.kendoNumericTextBox({
        max: options.model.jumlah,
        min: 1
    });
}

function SaveData() {
  
    var savedata = {
        no_ref: $('#no_rcnkirim').val(),
        detail: _GridSiapKirim.dataSource.data().toJSON()
    };

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

                        //window.location.href = urlCreate + '?id=' + result.result + '&mode=VIEW';
                        window.location.href= '/ViewSalesSJ/CetakSJ';
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


function GetDataSiapKirim(id) {
    startSpinner('loading..', 1);
    var urlLink = urlGetDataSiapKirim;
    var filterdata = {
        id: id
    };
    $.ajax({
        url: urlLink,
        type: "POST",
        data: filterdata,
        success: function (result) {
            SiapkirimDs = result;
            if (Mode === "NEW") {
                Mode = "VIEW";
            }
            fillForm();
            $.ajax({
                url: urlGetDetailSiapKirim,
                type: "POST",
                data: filterdata,
                success: function (result) {

                    SiapkirimDs = [];
                    for (var i = 0; i <= result.length - 1; i++) {
                        SiapkirimDs.push({
                            no_sp: result[i].no_sp,
                            atas_nama: result[i].nama_Customer,
                            kd_stok: result[i].kd_stok,
                            deskripsi: result[i].nama_Barang,
                            qty_out: result[i].qty_out,
                            jumlah: result[i].jumlah,
                            kd_satuan: result[i].kd_satuan
                            

                        });
                    }
                    console.log(SiapkirimDs);

                  
                    $('#GridSiapKirim').kendoGrid('destroy').empty();
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
    if (Mode === "VIEW") {


        $('[name="qty_qc_pass"]').attr("readonly", true);
        $('#divtanggal').datepicker('remove');

        $("#no_trans").attr("disabled", "disabled");
        $("#no_ref").attr("disabled", "disabled");
        //$("#no_qc").data("kendoDropDownList").readonly();
       

    }

    if (Mode === "EDIT" || Mode === "VIEW") {
        if (SiapkirimDs.length > 0) {
            //$("#no_qc").data("kendoDropDownList").value(GudangDs[0].no_qc);
            $("#no_rcnkirim").val(SiapkirimDs[0].no_ref);
        
            $("#no_trans").val(SiapkirimDs[0].no_trans);
           
            $("#tanggal").val(SiapkirimDs[0].tgl_transdesc);
           

        }
    }
}

function onPrintClicked() {
    startSpinner('loading..', 1);
    var urlLink = urlPrint + '?id=' + idSiapKirim;
    //wrapperList
    //var wrapper = document.getElementById("wrapperList");

    $.ajax({
        url: urlLink,
        type: "POST",
        success: function (result) {
            startSpinner('loading..', 0);
            var MainWindow = window.open('', '', 'height=500,width=800');

            //  MainWindow.document.write('<!DOCTYPE html><html><head> <style>table{font-family: tahoma, sans-serif;font-size: 10px; border-collapse: collapse; width: 100%;}td, th{border: 1px solid #dddddd; text-align: left; padding: 8px;}p{margin-block-start: 0em;margin-block-end: 0em;margin-bottom:7px;}@media print{.headerTable{background-color: #eae8e8 !important;-webkit-print-color-adjust: exact;}}</style></head><body><table style="margin-bottom: 20px;"><tr ><td style="width: 40%;border: 0px solid #dddddd;" ><h2>IFA Company</h2><p>Jalan Diponego 21</p><p>031-9992190</p><p>Surabaya - Jawa Timur</p></td><td style="width: 20%;border: 0px solid #dddddd;"></td><td style="width: 40%;border: 0px solid #dddddd;"><h2>PURCHASE ORDER</h2><p>PO No: 00002/POM/1/20151207</p><p>03 September 2019</p><p>PO Status: ENTRY</p></td></tr></table> <table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">SUPPLIER</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;">ALAMAT PENGIRIMAN</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SUMBER REJEKI TEKNIK-PENGHELA , UD.</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. DIPONEGORO 21, SURABAYA</td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">JL. RAYA BAMBE KM.19, DRIYOREJO GRESIK</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Telp No: 7590102</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"> </td></tr><tr > <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Jatuh Tempo: 02 Oktober 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;"></td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">TANGGAL KIRIM</th><th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">REQUESTED BY</th> <th style="border: 0px solid #dddddd;padding-bottom: 8px;padding-left: 8px;">APPROVED BY</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">20 September 2019</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Ricardo Kaka</td><td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">Rui Costa</td></tr></table><table style="margin-bottom: 20px;"> <tr class="headerTable" > <th style="border: 0px solid #dddddd;padding-bottom: 8px;">NOTES</th> </tr><tr> <td style="border: 0px solid #dddddd;padding: 2px;padding-left: 8px;">SEGERA DIKIRIM, STOK HABIS</td></tr></table> <table> <tr class="headerTable"> <th>Barang</th><th>Satuan</th> <th>Qty</th> <th>Harga</th><th>Disc Rp</th> <th>Total</th> </tr><tr> <td>HYDRAULIC CRIMPING TOOL YQK 300</td><td>PCS</td><td style="text-align: right;padding: 2px;">100.00</td><td style="text-align: right;padding: 2px;">10,000,000.00</td><td style="text-align: right;padding: 2px;">288,650,000.00</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">SUBTOTAL</td><td style="text-align: right;padding: 2px;">711,350,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">ONGKOS KIRIM</td><td style="text-align: right;padding: 2px;">10,000,000.00</td></tr><tr> <td colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">PPN</td><td style="text-align: right;padding: 2px;">71,135,000.00</td></tr><tr> <th colspan="5" style="text-align: right;padding: 2px;border: 0px solid #dddddd;">GRAND TOTAL (Rp)</th> <th style="text-align: right;padding: 2px;">792,485,000.00</th> </tr></table></body></html>');
            MainWindow.document.write(result);
            MainWindow.document.close();
            setTimeout(function () {
                MainWindow.print();
            }, 500);

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}


function showCreate() {
    window.location.href = urlCreate;
}