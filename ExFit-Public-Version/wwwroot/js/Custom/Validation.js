function validation() {
    var inputFields = $('input.required');
    inputFields.removeClass('is-invalid');
    inputFields.siblings('.invalid-feedback').remove();

    var hasEmptyFields = false;

    inputFields.each(function () {
        var input = $(this);
        var feedbackMessage = input.attr('feedback');
        var inputValue = input.val().trim();

        if (inputValue === '' && input.attr('type') !== 'email' && input.attr('type') !== 'tel') {
            hasEmptyFields = true;
            input.addClass('is-invalid');
            if (feedbackMessage) {
                input.after('<div class="invalid-feedback">' + feedbackMessage + '</div>');
            }
        } else if (input.attr('type') === 'email') {
            if (!isValidEmailFormat(inputValue)) {
                hasEmptyFields = true;
                input.addClass('is-invalid');
                input.after('<div class="invalid-feedback">Geçerli bir e-posta adresi giriniz.</div>');
            }
        } else if (input.attr('type') === 'tel') {
            if (inputValue === '' && input.attr('type') !== 'tel') {
                hasEmptyFields = true;
                input.addClass('is-invalid');
                input.after('<div class="invalid-feedback">Geçerli bir telefon numarası giriniz.</div>');
            }
            else {
                inputValue = inputValue.replace(/\s/g, '');
            }
        }
    });

    return !hasEmptyFields;
}

function isValidEmailFormat(email) {
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}

$(document).ready(function () {
    $('input[type="tel"]').mask("+99 999 999 9999", { placeholder: "+__ ___ ___ ____" });

    $('input.required').on('input', function () {
        var input = $(this);
        if (input.val().trim() !== '') {
            input.removeClass('is-invalid');
            input.siblings('.invalid-feedback').remove();
        }
    });
});
