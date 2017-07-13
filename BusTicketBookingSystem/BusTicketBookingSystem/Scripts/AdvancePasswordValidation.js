$(document).ready(function () {

    $('input[type=password]').keyup(function () {
        var pswd = $(this).val();

        //validate the length
        if (pswd.length < 6) {
            $('#length').removeClass('isValid').addClass('isInValid');
        } else {
            $('#length').removeClass('isInValid').addClass('isValid');
        }

        //validate letter
        if (pswd.match(/[A-z]/)) {
            $('#letter').removeClass('isInValid').addClass('isValid');
        } else {
            $('#letter').removeClass('isValid').addClass('isInValid');
        }

        //validate capital letter
        if (pswd.match(/[A-Z]/)) {
            $('#capital').removeClass('isInValid').addClass('isValid');
        } else {
            $('#capital').removeClass('isValid').addClass('isInValid');
        }

        //validate number
        if (pswd.match(/\d/)) {
            $('#number').removeClass('isInValid').addClass('isValid');
        } else {
            $('#number').removeClass('isValid').addClass('isInValid');
        }

        //validate space
        if (pswd.match(/[^a-zA-Z0-9\-\/]/)) {
            $('#space').removeClass('isInValid').addClass('isValid');
        } else {
            $('#space').removeClass('isValid').addClass('isInValid');
        }

    }).focus(function () {
        $('#pswd_info').show();
    }).blur(function () {
        $('#pswd_info').hide();
    });

});