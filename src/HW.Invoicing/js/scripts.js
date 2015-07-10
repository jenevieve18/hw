
function addMonth(startDate, months) {
    months = parseFloat(months);
    var d = new Date(startDate);
    var currentMonth = d.getMonth();
    var newMonth = currentMonth + months;
    d = new Date(d.setMonth(newMonth));
    d = new Date(d.setDate(d.getDate() - 1));
    return d;
}

function getDaysInAMonth(month, year) {
    var isLeapYear = year % 4 == 0 && (year % 100 != 0 && year % 400 == 0);
    var febDays = isLeapYear ? 29 : 28;
    var days = [
        31,
        febDays,
        31,
        30,
        31,
        30,
        31,
        31,
        30,
        31,
        30,
        31
    ];
    var d = days[month];
    return d;
}

function monthDiff(d1, d2) {
    var months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth();
    months += d2.getMonth();
    months--;
    
    var daysOfMonth1 = getDaysInAMonth(d1.getMonth(), d1.getFullYear());
    months += (daysOfMonth1-d1.getDate()+1) / daysOfMonth1;
    
    var daysOfMonth2 = getDaysInAMonth(d2.getMonth(), d2.getFullYear());
    months += d2.getDate() / daysOfMonth2;  

    return Math.round(months*100)/100; 
}

function monthDiffX(d1, d2) {
    var months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth();
    months += d2.getMonth();

    var daysOfMonth1 = getDaysInAMonth(d1.getMonth(), d1.getFullYear());
    var daysOfMonth2 = getDaysInAMonth(d2.getMonth(), d2.getFullYear());

    var days1 = daysOfMonth1 - d1.getDate() + 1;
    var extraMonth1 = parseFloat(days1) / daysOfMonth1;

    var days2 = daysOfMonth2 - d2.getDate();
    var extraMonth2 = parseFloat(days2) / daysOfMonth2;

    months += (extraMonth1 + extraMonth2);
    return months;
}

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