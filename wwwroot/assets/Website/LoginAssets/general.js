"use strict";

// Create a module named KTSignupGeneral
var KTSignupGeneral = function () {
    // Define variables
    var form, submitBtn, passwordMeter, validator;

    // Function to check if the password score is 100
    var isPasswordStrong = function () {
        return 100 === passwordMeter.getScore();
    };

    return {
        // Initialize function
        init: function () {
            // Get DOM elements
            form = document.querySelector("#kt_sign_up_form");
            submitBtn = document.querySelector("#kt_sign_up_submit"); // Define submitBtn variable

            // Initialize password meter
            passwordMeter = KTPasswordMeter.getInstance(form.querySelector('[data-kt-password-meter="true"]'));

            // Initialize form validation
            validator = FormValidation.formValidation(form, {
                fields: {
                    password: {
                        validators: {
                            notEmpty: {
                                message: "حقل كلمة المرور مطلوب!"
                            },
                            callback: {
                                message: "من فضلك أدخل كلمة مرور صحيحة",
                                callback: function (value) {
                                    // Check if password is strong
                                    if (value.length > 0) {
                                        return isPasswordStrong();
                                    }
                                    return true;
                                }
                            }
                        }
                    },
                    "confirm-password": {
                        validators: {
                            notEmpty: {
                                message: "حقل كلمة المرور مطلوب!"
                            },
                            identical: {
                                compare: function () {
                                    return form.querySelector('[name="password"]').value;
                                },
                                message: "كلمتين المرور غير متطابقين"
                            }
                        }
                    },
                    toc: {
                        validators: {
                            notEmpty: {
                                message: "يجب الموافقة على البنود والحالات"
                            }
                        }
                    }
                }
            });            

            // Event listener for password input
            form.querySelector('input[name="password"]').addEventListener("input", function () {
                // Reset password field status on input
                if (this.value.length > 0) {
                    validator.updateFieldStatus("password", "NotValidated");
                }
            });
        }
    };
}();
