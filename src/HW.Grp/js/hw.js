function getData(elements) {
    var data = [];
    $(elements).each(function(i, field) {
        data.push(field.value);
    });
    return data;
}

var HW = function() {};

HW.save = function (dataInputs, sponsorID, exerciseVariantLangID) {
    $('#save').text('Saving...');
    $.ajax({
        type: 'POST',
        url: 'ExerciseShow.aspx/Save',
        data: JSON.stringify({ dataInputs: dataInputs, sponsorID: sponsorID, exerciseVariantLangID: exerciseVariantLangID }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#save').text('Save');
            alert(response.d);
        },
        error: function(req, textStatus, errorThrown) {
            //this is going to happen when you send something different from a 200 OK HTTP
            alert('Ooops, something happened: ' + textStatus + ' ' +errorThrown);
        }
    });
}

HW.read = function(sponsorID, exerciseVariantLangID, f) {
    $.ajax({
        type: 'GET',
        url: 'ExerciseShow.aspx/Read',
        data: { 'sponsorID': sponsorID, 'exerciseVariantLangID': exerciseVariantLangID },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (response) {
            f(response);
        },
        error: function(req, textStatus, errorThrown) {
            //this is going to happen when you send something different from a 200 OK HTTP
            alert('Ooops, something happened: ' + textStatus + ' ' +errorThrown);
        }
    });
}

HW.hasEmpty = function (elements) {
    var invalid = false;
    $(elements).each(function() {
        if (!$(this).val()) {
            invalid = true;
        }
    });
    return invalid;
}
