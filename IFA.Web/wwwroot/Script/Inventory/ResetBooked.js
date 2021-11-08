
var listBarang = [];

$(document).ready(function () {
    startSpinner('loading..', 1);
    $.when(GetBarang()).done(function () {
        // fillBarang();
        $("#barang").kendoDropDownList({
            dataTextField: "nama_Barang",
            dataValueField: "kode_Barang",
            filter: "contains",
            dataSource: listBarang,
            optionLabel: "ALL",
            virtual: {
                valueMapper: function (options) {
                    options.success([options.nama_Barang || 0]);
                }
            },

        }).closest(".k-widget");

        $("#barang").data("kendoDropDownList").list.width("400px");
        startSpinner('loading..', 0);
    });
});



function onReset() {
    var barang = $("#barang").val();

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
                url: urlPostKalkulasi + "/" + barang,
                type: "POST",
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
                            html: "Kalkulasi Sukses"
                        });
                        startSpinner('loading..', 0);
                    }
                    console.log(JSON.stringify(result));
                },
                error: function (data) {
                    alert('Something Went Wrong');
                    startSpinner('loading..', 0);
                }
            });
        }
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
