function getData(elements) {
  var data = [];
  $(elements).each(function(i, field) {
    data.push(field.value);
  });
  return data;
}

var SponsorRepository = function() {};

SponsorRepository.saveSponsorAdminExercise = function(dataInputs, sponsorAdminID, exerciseVariantLangID) {
  $('#save').text('Saving...');
  $.ajax({
    type: 'POST',
    url: 'ExerciseShow.aspx/SaveSponsorAdminExercise',
    data: JSON.stringify({
      dataInputs: dataInputs,
      sponsorAdminID: sponsorAdminID,
      exerciseVariantLangID: exerciseVariantLangID
    }),
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function(response) {
      $('#save').text('Save');
      alert(response.d);
    },
    error: function(req, textStatus, errorThrown) {
      // This is going to happen when you send something different from a 200 OK HTTP
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
    }
  });
};

SponsorRepository.findSponsorAdminExerciseDataInputs = function(sponsorAdminExerciseID, callback) {
  $.ajax({
    type: 'GET',
    url: 'ExerciseShow.aspx/FindSponsorAdminExerciseDataInputs',
    data: {
      'sponsorAdminExerciseID': sponsorAdminExerciseID
    },
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function(response) {
      callback(response);
    },
    error: function(req, textStatus, errorThrown) {
      // This is going to happen when you send something different from a 200 OK HTTP
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
    }
  });
};

var HtmlHelper = function() {};

HtmlHelper.hasEmpty = function(elements) {
  var invalid = false;
  $(elements).each(function() {
    if (!$(this).val()) {
      invalid = true;
    }
  });
  return invalid;
};
