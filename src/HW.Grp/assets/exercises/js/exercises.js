function getData(elements) {
  var data = [];
  $(elements).each(function(i, field) {
    data.push(field.value);
  });
  return data;
}

function getData2(elements) {
  var data = [];
  $(elements).each(function(i, field) {
    if ($(field).is(':checkbox')) {
      if ($(field).attr('checked')) {
        data.push({ ValueInt: field.value, Type: 3 });
      } else {
        data.push({ ValueInt: 0, Type: 3 });
      }
    } else {
      data.push({ ValueText: field.value, Type: 2 });
    }
  });
  return data;
}

var SponsorRepository = function() {};

SponsorRepository.saveSponsorAdminExercise = function(dataInputs, sponsorAdminID, exerciseVariantLangID, sponsorAdminExerciseID) {
  $('#save').text('Saving...');
  $.ajax({
    type: 'POST',
    url: 'ExerciseShow.aspx/SaveOrUpdateSponsorAdminExercise',
    data: JSON.stringify({
      dataInputs: dataInputs,
      sponsorAdminID: sponsorAdminID,
      exerciseVariantLangID: exerciseVariantLangID,
      sponsorAdminExerciseID: sponsorAdminExerciseID
    }),
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function(response) {
      $('#save').text('Save');
      alert(response.d);
    },
    error: function(req, textStatus, errorThrown) {
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown); // This is going to happen when you send something different from a 200 OK HTTP
    }
  });
};

SponsorRepository.saveSponsorAdminExercise2 = function(dataInputs, sponsorAdminID, exerciseVariantLangID, sponsorAdminExerciseID) {
  $('#save').text('Saving...');
  $.ajax({
    type: 'POST',
    url: 'ExerciseShow.aspx/SaveOrUpdateSponsorAdminExercise2',
    data: JSON.stringify({
      dataInputs: dataInputs,
      sponsorAdminID: sponsorAdminID,
      exerciseVariantLangID: exerciseVariantLangID,
      sponsorAdminExerciseID: sponsorAdminExerciseID
    }),
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function(response) {
      $('#save').text('Save');
      alert(response.d);
    },
    error: function(req, textStatus, errorThrown) {
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown); // This is going to happen when you send something different from a 200 OK HTTP
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
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown); // This is going to happen when you send something different from a 200 OK HTTP
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
