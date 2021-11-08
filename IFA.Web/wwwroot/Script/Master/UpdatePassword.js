
function onSaveClicked() {
    validationCustomer();
}

function validationCustomer() {
    var password = $('#password').val();
    var newpassword = $('#newpassword').val();
    var confirmpassword = $('#confirmpassword').val();

    validationMessage = '';
    if (!password) {
        validationMessage = validationMessage + 'Password tidak boleh kosong.' + '\n';
    }
    if (!newpassword) {
        validationMessage = validationMessage + 'New Password tidak boleh kosong.' + '\n';
    }
    if (!confirmpassword) {
        validationMessage = validationMessage + 'Confirm Password tidak boleh kosong.' + '\n';
    }

    if (validationMessage == "") {
        if (confirmpassword.length < 6 || newpassword.length < 6) {
            validationMessage = validationMessage + 'Password harus lebih dari 6 karakter.' + '\n';
        }
    }
    if (validationMessage == "") {
        if (confirmpassword != newpassword) {
            validationMessage = validationMessage + 'Kombinasi New Password dan Confirm Password harus sama.' + '\n';
        }
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
    var savedata = {
        passwd: $('#confirmpassword').val(),
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
                url: urlUpdatePassword,
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

