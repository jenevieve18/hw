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
      if ($(field).is(':checked')) {
        data.push({ ValueInt: 1, Type: 3 });
      } else {
        data.push({ ValueInt: 0, Type: 3 });
      }
    } else if ($(field).is(':radio')) {
      if ($(field).is(':checked')) {
        data.push({ ValueInt: 1, Type: 1 });
      } else {
        data.push({ ValueInt: 0, Type: 1 });
      }
    } else {
      data.push({ ValueText: field.value, Type: 2 });
    }
  });
  return data;
}

var Exercise = (function() {
  return {
    save: function(dataInputs, sponsorAdminID, exerciseVariantLangID, sponsorAdminExerciseID) {
      $('#btnSaveSponsorAdminExercise').text('Saving...');
      $.ajax({
        type: 'POST',
        url: 'ExerciseShow.aspx/SaveOrUpdateSponsorAdminExercise3',
        data: JSON.stringify({
          dataInputs: dataInputs,
          sponsorAdminID: sponsorAdminID,
          exerciseVariantLangID: exerciseVariantLangID,
          sponsorAdminExerciseID: sponsorAdminExerciseID
        }),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function(response) {
          $('#btnSaveSponsorAdminExercise').text('Save');
          alert(response.d);
        },
        error: function(req, textStatus, errorThrown) {
          alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown); // This is going to happen when you send something different from a 200 OK HTTP
        }
      });
    },
    get: function(sponsorAdminExerciseID, callback) {
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
    }
  }
})();

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

SponsorRepository.findSponsorAdminExerciseDataInputs2 = function(sponsorAdminExerciseID, callback) {
  $.ajax({
    type: 'GET',
    url: 'ExerciseShow.aspx/FindSponsorAdminExerciseDataInputs2',
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

Exercise.get = function(sponsorAdminExerciseID, callback) {
  $.ajax({
    type: 'GET',
    url: 'ExerciseShow.aspx/FindSponsorAdminExerciseDataInputs2',
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
