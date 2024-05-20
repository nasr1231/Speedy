function validateForm(event) {
    var acceptTermsCheckbox = document.getElementById('AcceptTerms');
    if (!acceptTermsCheckbox.checked) {
        event.preventDefault();
        alert('يجب أن توافق على الشروط والأحكام');
    }
}

function updateHiddenGender() {
    var selectedGender = document.querySelector('input[name="gender"]:checked').value;
    document.getElementById('hidden-gender').value = selectedGender;
}


document.addEventListener('DOMContentLoaded', function () {
    var submitButton = document.getElementById('Submit-Btn');
    submitButton.addEventListener('click', validateForm);

    document.getElementById('radio-male').addEventListener('change', updateHiddenGender);
    document.getElementById('radio-female').addEventListener('change', updateHiddenGender);

    if (document.querySelector('input[name="gender"]:checked')) {
        updateHiddenGender();
    }    
});