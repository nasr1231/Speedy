//Variables Definition
var table;
var UpdatedRow;
var datatable;
var exported_Columns = [];

function disableSubmitButton() {
    $('.body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

function OnModalBegin() {
    disableSubmitButton();
}

// Messages
function ShowSuccessMessage(message = 'Updated Successfully!') {
    Swal.fire({
        position: 'center-center',
        icon: 'success',
        title: 'Success',
        text: message, // Use the parameter message here
        showConfirmButton: false,
        timer: 2500
    });
}

function showUserForm(form) {
    var modal = $('#Modal');
    modal.find('.modal-title').text("إضافة مستخدم");
    modal.find('.modal-body').html(form);
    $.validator.unobtrusive.parse(modal);
    select2func()
    $("#kt_datepicker_1").flatpickr({});
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
    ShowSuccessMessage();
    $('#model-window').modal('hide');

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

// Datatable Configuration
var KTDatatables = function () {
    // Private functions
    var initDatatable = function () {
        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            'pageLength': 10,
        });
    }

    // Hook export buttons
    var exportButtons = () => {
        const documentTitle = $('.js-datatables').data('doc-title');
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
            table = document.querySelector('.js-datatables');

            if (!table) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        }
    };
}();


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

    //Date Picker
    $('.js-datepicker').daterangepicker({
        singleDatePicker: true,
        autoApply: true,
        showDropdowns: true,
        drops: 'up',
        maxDate: new Date()
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
            UpdatedRow = btn.parents('tr');
        }

        $.ajax({
            url: btn.data('url'),
            method: 'GET',
            dataType: 'html', // Expect HTML content
            success: function (form) {
                ShowModel.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(ShowModel);
            },
            error: function () {
                ShowErrorMessage();
            }
        });

        $('#model-window').modal('show');
    });
    //Handle Sign Out
    $('.js-signout').on('click', function () {
        $('#SignOut').submit();
    });
});