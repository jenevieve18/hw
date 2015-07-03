

Object.size = function (obj) {
    var size = 0, key;
    for (key in obj) {
        if (obj.hasOwnProperty(key)) size++;
    }
    return size;
};

function findAndRemove(array, property, value) {
    $.each(array, function (index, result) {
        if (result[property] == value) {
            //Remove from array
            array.splice(index, 1);
            return false;
        }
    });
}

function turnEditable(labelId, textBoxId) {
    var label = $(labelId);
    var textBox = $(textBoxId);
    textBox.hide();

    label.click(function () {
        $(this).hide();
        textBox.show();
        textBox.focus();
    });
    textBox.focusout(function () {
        $(this).hide();
        label.show();
    });
}

function addErrorIf(errors, condition, message) {
    if (condition) {
        errors.push(message);
    }
}

function displayMessage(errors, box) {
    if (errors.length > 0) {
        s = '';
        for (var i = 0; i < errors.length; i++) {
            s += '<li>' + errors[i] + '</li>';
        }
        var message = '<div class="alert alert-warning">' +
                            '<ul>' + s + '</ul>' +
                            '</div>';
        $(box).html(message);
        return false;
    }
    return true;
}