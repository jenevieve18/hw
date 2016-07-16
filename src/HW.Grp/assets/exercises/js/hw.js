function getData(elements) {
  var data = [];
  $(elements).each(function(i, field) {
    data.push(field.value);
  });
  return data;
}

var hw = function() {};

hw.saveSponsorAdminExercise = function(dataInputs, sponsorAdminID, exerciseVariantLangID) {
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
      //this is going to happen when you send something different from a 200 OK HTTP
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
    }
  });
};

// hw.save = function(dataInputs, sponsorID, exerciseVariantLangID) {
//   $('#save').text('Saving...');
//   $.ajax({
//     type: 'POST',
//     url: 'ExerciseShow.aspx/Save',
//     data: JSON.stringify({
//       dataInputs: dataInputs,
//       sponsorID: sponsorID,
//       exerciseVariantLangID: exerciseVariantLangID
//     }),
//     contentType: "application/json;charset=utf-8",
//     dataType: "json",
//     success: function(response) {
//       $('#save').text('Save');
//       alert(response.d);
//     },
//     error: function(req, textStatus, errorThrown) {
//       //this is going to happen when you send something different from a 200 OK HTTP
//       alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
//     }
//   });
// };
//
// hw.save2 = function(dataInputs, sponsorAdminID, exerciseVariantLangID, savingText, saveText) {
//   $('#save').text(savingText);
//   $.ajax({
//     type: 'POST',
//     url: 'ExerciseShow.aspx/Save2',
//     data: JSON.stringify({
//       dataInputs: dataInputs,
//       sponsorAdminID: sponsorAdminID,
//       exerciseVariantLangID: exerciseVariantLangID
//     }),
//     contentType: "application/json;charset=utf-8",
//     dataType: "json",
//     success: function(response) {
//       $('#save').text(saveText);
//       alert(response.d);
//     },
//     error: function(req, textStatus, errorThrown) {
//       // This is going to happen when you send something different from a 200 OK HTTP
//       alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
//     }
//   });
// };
//
// hw.save3 = function(dataInputs, sponsorAdminID, exerciseVariantLangID) {
//   $('#save').text('Saving...');
//   $.ajax({
//     type: 'POST',
//     url: 'ExerciseShow.aspx/Save2',
//     data: JSON.stringify({
//       dataInputs: dataInputs,
//       sponsorAdminID: sponsorAdminID,
//       exerciseVariantLangID: exerciseVariantLangID
//     }),
//     contentType: "application/json;charset=utf-8",
//     dataType: "json",
//     success: function(response) {
//       $('#save').text('Save');
//       alert(response.d);
//     },
//     error: function(req, textStatus, errorThrown) {
//       // This is going to happen when you send something different from a 200 OK HTTP
//       alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
//     }
//   });
// };

hw.findSponsorAdminExerciseDataInputs = function(sponsorAdminID, exerciseVariantLangID, f) {
  $.ajax({
    type: 'GET',
    url: 'ExerciseShow.aspx/FindSponsorAdminExerciseDataInputs',
    data: {
      'sponsorAdminID': sponsorAdminID,
      'exerciseVariantLangID': exerciseVariantLangID
    },
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    success: function(response) {
      f(response);
    },
    error: function(req, textStatus, errorThrown) {
      //this is going to happen when you send something different from a 200 OK HTTP
      alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
    }
  });
};

// hw.read = function(sponsorAdminID, exerciseVariantLangID, f) {
//   $.ajax({
//     type: 'GET',
//     url: 'ExerciseShow.aspx/Read',
//     data: {
//       'sponsorAdminID': sponsorAdminID,
//       'exerciseVariantLangID': exerciseVariantLangID
//     },
//     contentType: "application/json;charset=utf-8",
//     dataType: "json",
//     success: function(response) {
//       f(response);
//     },
//     error: function(req, textStatus, errorThrown) {
//       //this is going to happen when you send something different from a 200 OK HTTP
//       alert('Ooops, something happened: ' + textStatus + ' ' + errorThrown);
//     }
//   });
// };
//
// hw.hasEmpty = function(elements) {
//   var invalid = false;
//   $(elements).each(function() {
//     if (!$(this).val()) {
//       invalid = true;
//     }
//   });
//   return invalid;
// };
