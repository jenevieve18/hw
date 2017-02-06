var exerciseRepo = new ExerciseRepo();
var inputs = 0;

$(function () {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-add').click(function () {
    if ($('#input' + (inputs - 3)).val() !== "" || $('#input' + (inputs - 2)).val() !== "" || $('#input' + (inputs - 1)).val() !== "") {
      inputs += 3;
      $('#tbl').append('<tr>\
					<td>' + getEnterText() + '</td>\
					<td><textarea name="input[]" id="input' + (inputs - 3) + '" style="width:90%;height:50px;padding:3px"></textarea></td>\
					<td><textarea name="input[]" id="input' + (inputs - 2) + '"style="width:90%;height:50px;padding:3px"></textarea></td>\
					<td><textarea name="input[]" id="input' + (inputs - 1) + '"style="width:90%;height:50px;padding:3px"></textarea></td>\
				</tr>');
    }
  });

  $('#btn-save').click(function () {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
      inputs: html.getInputs($('[id^="input"]')),
      sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
      exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
      id: exerciseId,
    }, function (message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    },
      function (message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function (exercise) {
      if (exercise === null) {
        exercise = newExercise;
      }
      for (inputs = 0; inputs < exercise.inputs.length;) {
        $('#tbl').append('<tr>\
            <td>' + getEnterText() + '</td>\
            <td><textarea name="input[]" id="input' + (inputs) + '" style="width:90%;height:50px;padding:3px">' + exercise.inputs[(inputs)].valueText + '</textarea></td>\
            <td><textarea name="input[]" id="input' + (inputs + 1) + '" style="width:90%;height:50px;padding:3px">' + exercise.inputs[(inputs + 1)].valueText + '</textarea></td>\
            <td><textarea name="input[]" id="input' + (inputs + 2) + '" style="width:90%;height:50px;padding:3px">' + exercise.inputs[(inputs + 2)].valueText + '</textarea></td>\
          </tr>');
        inputs += 3;
      }
    });
});
