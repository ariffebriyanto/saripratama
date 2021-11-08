$(document).ready(function () {

    //  console.log(JenisBarangList);
    //alert(dateserver);
    //if (Mode != "NEW") {
    //    getData(idData);
    //}
    getGudang()
});
function onSaveClicked() {
    validationCustomer();
}

function getGudang() {
    return $.ajax({
        url: urlGudang,
        type: "POST",
        success: function (result) {
            $("#gudang").empty();
            $("#gudang").append('<option value="" selected disabled>Please select</option>');
            var data = result;
            GudangTujuan = result;
            for (var i = 0; i < data.length; i++) {
                $("#gudang").append('<option value="' + data[i].kode_Gudang + '">' + data[i].nama_Gudang + '</option>');
            }
            $('#gudang').selectpicker('refresh');
            $('#gudang').selectpicker('render');

        },
        error: function (data) {
            alert('Something Went Wrong');
            startSpinner('loading..', 0);
        }
    });
}

function validationCustomer() {
    var password = $('#password').val();
  
    var gd  = $('#gudang').val();
    validationMessage = '';
    if (!password) {
        validationMessage = validationMessage + 'Password tidak boleh kosong.' + '\n';
    }
    if (!gd) {
        validationMessage = validationMessage + 'Cabang/Gudang Baru blm di set!!' + '\n';
    }

    //if (validationMessage == "") {
    //    if (confirmpassword.length < 6 || newpassword.length < 6) {
    //        validationMessage = validationMessage + 'Password harus lebih dari 6 karakter.' + '\n';
    //    }
    //}
    //if (validationMessage == "") {
    //    if (confirmpassword != newpassword) {
    //        validationMessage = validationMessage + 'Kombinasi New Password dan Confirm Password harus sama.' + '\n';
    //    }
    //}

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
    var savedata = {
        cabang_new: $('#gudang').val(),
        oldPassword: $('#password').val()
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
                url: urlUpdateCabang,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {

                        //Swal.fire({
                        //    type: 'success',
                        //    title: 'Success',
                        //    html: "Save Successfully"
                        //});
                        //startSpinner('loading..', 0);
                        //$('#confirmpassword').val("");
                        //$('#newpassword').val("");
                        //$('#password').val("");
                        window.location.href = urlLogout;
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

