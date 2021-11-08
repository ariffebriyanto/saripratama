

$(document).ready(function () {
    startSpinner('Loading..', 1);

    selectedInput();
 //   console.log(JSON.stringify(TipeTransds));

    startSpinner('Loading..', 0);

});

function selectedInput() {
    var seq = 0;
    for (var i = 0; i <= TipeTransds.length - 1;i++) {
        $("input[name='" + TipeTransds[i].tipe_trans + "'][value='" + TipeTransds[i].auto_posting + "']").prop('checked', true);
    }
}

function onSaveClicked() {
    var savedata = [];
    for (var i = 0; i <= TipeTransds.length - 1; i++) {
        correctValue = 'Nothing selected',
            selected = document.querySelector('input[name="' + TipeTransds[i].tipe_trans + '"]:checked'),
            selection = document.querySelector('#selection');
        correctValue = document.querySelector('label[for="' + selected.id + '"]').innerHTML;

        if (correctValue == "AUTO") {
            correctValue = "A"
        }
        else {
            correctValue = "M"
        }

        savedata.push({
            "tipe_trans": TipeTransds[i].tipe_trans,
            "auto_posting": correctValue,
        });
    }

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
            var d = {
                "mode": "New",
                "details": savedata
            }
            $.ajax({
                type: "POST",
                data: d,
                url: urlSaveData,
                success: function (result) {
                    if (result.success === false) {
                        Swal.fire({
                            type: 'error',
                            title: 'Warning',
                            html: result.message
                        });
                        startSpinner('loading..', 0);
                    } else {
                        startSpinner('loading..', 0);
                        window.location.href = urlCreate;

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