//Variables Definition
var table;
var UpdatedRow;
var datatable;
var exported_Columns = [];
var model = $('#model-window');

var KTDatatables = function () {
    // Private functions
    var initDatatable = function () {
        datatable = $(table).DataTable({
            "info": false,
            'pageLength': 10,
        });
    }

    // Hook export buttons
    var exportButtons = () => {
        const documentTitle = $('.js-datatable').data('export-title');
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                }
            ]
        }).container().appendTo($('#kt_datatable_example_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = $('.js-datatable');

            if (!table) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        },
    };
}();

function showSuccessMessage(message = 'Your Entry is added successfully.') {

    Swal.fire({
        title: "Done Successfully.....",
        text: message,
        icon: 'success',        
        buttonsStyling: false,
        showConfirmButton: false,
        timer: 2500        
    }).then((result) => {
        if (result.isConfirmed) {
            $('.js-status').parents('tr').removeClass('animate__animated animate__flash');
            $('.js-edition-status').parents('tr').removeClass('animate__animated animate__flash');
            $('tbody').find('.js-new-row').removeClass('animate__animated animate__flash');
            $('tbody').find('.js-new-row').removeClass('js-new-row');
        }
    });
}
function disableSubmitButton() {
    $('.body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function onModalBegin() {
    disableSubmitButton();
}

function showErrorMessage(message) {

    if (message.responseText == '') {
        message.responseText = 'Something Went Wrong!';
    }

    Swal.fire({
        title: "Oooooooops!",
        text: message.responseText,
        icon: "error",
        buttonsStyling: false,
        confirmButtonText: "Accept",
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function modalSubmitSuccess(row) {
    $(model).modal('hide');

    addNewRow(row);
    $('tbody').find('.js-new-row').addClass('animate__animated animate__flash');
    showSuccessMessage();

    KTMenu.init();
    KTMenu.initHandlers();    
}


// Messages
//function ShowSuccessMessage(message = 'Updated Successfully!') {
//    Swal.fire({
//        position: 'center-center',
//        icon: 'success',
//        title: 'Success',
//        text: message, // Use the parameter message here
//        showConfirmButton: false,
//        timer: 2500
//    });
//}

function showUserForm(form) {
    var modal = $('#Modal');
    modal.find('.modal-title').text("إضافة مستخدم");
    modal.find('.modal-body').html(form);
    $.validator.unobtrusive.parse(modal);
    select2func()    
    modal.modal('show');
}
function ShowErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message, // Use the parameter message here
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function onModalComplete() {
    $('body :submit').removeAttr('data-kt-indicator', 'on').removeAttr('disabled', 'disabled');
}
function OnModalSuccess(row) {
    $(model).modal('hide');
    ShowSuccessMessage();

    if (UpdatedRow !== undefined) {
        datatable.row(UpdatedRow).remove().draw();
        UpdatedRow = undefined;
    }

    var newRow = $(row);
    datatable.row.add(newRow).draw();

    KTMenu.init();
    KTMenu.initGlobalHandlers();
}

var headers = $('th');
$.each(headers, function (i) {
    if (!$(this).hasClass('js-no-export'))
        exported_Columns.push(i);
});

function OnModalToaster() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toastr-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.success("Updated Successfully!");
}

//Select2 Function
function select2func() {
    $('.js-select2').select2();
    $('.js-select2').on('select2:select', function (e) {

        $('form').not('.js-logOutForm').validate().element('#' + $(this).attr('id'));
    });
}

function addNewRow(row) {

    var newRow = $(row);

    datatable.row.add(newRow).draw();

    //if (rowUpdated !== undefined) {
    //    datatable.row(rowUpdated).remove().draw()

    //    rowUpdated = undefined;
    //}
}

// Bootstrap Modal
$(document).ready(function () {
    // Sweet Alert
    var message = $('#Message').text();
    if (message !== '') {
        ShowSuccessMessage(message);
    }

    // Disable Button
    $('form').not('#SignOut').on('submit', function () {
        var isValid = $(this).valid();
        if (isValid) disableSubmitButton();
    });

    // Select2
    select2func();

    // Date picker
    $(".js-datepicker").flatpickr({
        singleDatePicker: true,
        autoApply: true,
        showDropdowns: true,
        drops: 'up',               
        minDate: "1-1-1955",        
    });    

    // Tinymce Editor
    if ($('.js-tinymce').length > 0) {
        var options = { selector: ".js-tinymce", height: "537" };

        if (KTThemeMode.getMode() === "dark") {
            options["skins"] = "oxide-dark";
            options["content_css"] = "dark";
        }
        tinymce.init(options);
    }


    // Data Table Setting
    KTUtil.onDOMContentLoaded(function () {
        KTDatatables.init();
    });

    $('.js-save-btn').on('click', function () {
        //var btn = $(this);
        ShowSuccessMessage();
    });

    //Toggle Status Request

    $('body').on('click', '.js-toggle-status', function () {
        var btn = $(this);

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
                            var row = btn.parents('tr');
                            var status = row.find('.js-status');
                            var newStatus = status.text().trim() === 'Deleted' ? 'Available' : 'Deleted';
                            status.text(newStatus).toggleClass('badge-light-success badge-light-danger');
                            row.find('.js-updated-on').html(lastUpdatedOn);
                            //ShowSuccessMessage();
                            OnModalToaster();
                        },
                        error: function () {
                            ShowErrorMessage();
                        }
                    });
                }
            }
        });
    });

    $(document).on('click', '.js-render-modal', function (e) {
        e.preventDefault(); // Prevent the default anchor behavior
        var btn = $(this);
        var ShowModel = $('#model-window');
        ShowModel.find('#modalLabel').text(btn.data('title'));

        if (btn.data('update') !== undefined) {
            rowUpdated = btn.parents('tr');
        }

        $.ajax({
            url: btn.data('url'),
            method: 'GET',
            dataType: 'html', // Expect HTML content
            success: function (form) {
                ShowModel.find('.modal-title').text(btn.data('title'));                

                ShowModel.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(ShowModel);
                ShowModel.modal('show');
            },
            error: function (message) {
                ShowErrorMessage(message);
            }
        });

       
    });
    //Handle Sign Out
    $('.js-signout').on('click', function () {
        $('#SignOut').submit();
    });

    $('body').delegate('.js-change-btn', 'click', function () {

        var btn = $(this);

        bootbox.confirm({
            title: btn.data('title'),
            message: btn.data('message'),
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel',
                    className: 'btn-primary'

                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {

                    $.post
                        (
                            {
                                url: btn.data('url'),
                                data: {
                                    '__RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
                                },
                                success: function (Data) {
                                    rowUpdated = btn.parents('tr');
                                    btn.parents('tr').removeClass('animate__animated animate__flash');

                                    modalSubmitSuccess(Data)
                                    showSuccessMessage();

                                },
                                error: function (message) {
                                    showErorrMessage(message);
                                }

                            }
                        )
                }
            }
        });

    });
});