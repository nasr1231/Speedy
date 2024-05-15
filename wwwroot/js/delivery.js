$(document).ready(function () {
    $('body').on('click', '.delivery-toggle-status', function (e) {
        var btn = $(this);        
        var checkbox = $('#isChecked');
        var warningMessage = $('#checkboxWarning');
        
        if (!checkbox.is(':checked')) {
            e.preventDefault();
            checkbox.css('border', '2px solid red');
            warningMessage.show();
           
            return;
        }
        
        checkbox.css('border', '2px solid red');
        warningMessage.hide();

        bootbox.confirm({
            message: btn.data('message'),
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: btn.data('url'),
                        method: 'POST',
                        data: {
                            '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                        },
                        success: function (lastUpdatedOn) {                                                        
                            //ShowSuccessMessage();
                            OnModalToaster();
                            setTimeout(function () {
                                window.location.reload();
                            }, 2000);
                        },
                        error: function () {
                            ShowErrorMessage();
                        }
                    });
                }
            }
        });
    });
});