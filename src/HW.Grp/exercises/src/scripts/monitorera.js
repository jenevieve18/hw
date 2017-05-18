var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  html.accordion('.accordion');

  $('#btn-save').click(function() {
    var inputs = [];
    inputs.push({ id: $('[name^="input0[]"]').data('id'), components: html.getElementValues($('[name^="input0[]"]')) });
    inputs.push({ id: $('[name^="input1[]"]').data('id'), components: html.getElementValues($('[name^="input1[]"]')) });
    inputs.push({ id: $('[name^="input2[]"]').data('id'), components: html.getElementValues($('[name^="input2[]"]')) });
    inputs.push({ id: $('[name^="input3[]"]').data('id'), components: html.getElementValues($('[name^="input3[]"]')) });
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        inputs: inputs,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        id: exerciseId,
      },
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      },
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      if (exercise === null) {
        exercise = {
          inputs: [
            { components: [{ valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }] },
            { components: [{ valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }] },
            { components: [{ valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }] },
            { components: [{ valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }, { valueText: '' }] },
          ]
        };
      } else {
        $('.accordion h3').click();
      }
      var str = '';
      for (var i = 0; i < exercise.inputs.length; i++) {
        var input = exercise.inputs[i];
        for (j = 0; j < input.components.length;) {
          // $('#tbl' + (i)).append("<tr>\
          //       <td><input type='text' name='input" + (i) + "[]' value='" + input.components[(j)].valueText + "' id='inputComponent" + (j) + "' /></td>\
          //       <td><input type='text' name='input" + (i) + "[]' value='" + input.components[(j + 1)].valueText + "' id='inputComponent" + (j + 1) + "' /></td>\
          //       <td><input type='text' name='input" + (i) + "[]' value='" + input.components[(j + 2)].valueText + "' id='inputComponent" + (j + 2) + "'/></td>\
    			// 		</tr>");
          $('#tbl' + (i)).append("<tr>\
                <td><textarea style='width:90%' name='input" + (i) + "[]' value='" + input.components[(j)].valueText + "' id='inputComponent" + (j) + "' /></td>\
                <td><textarea style='width:90%' name='input" + (i) + "[]' value='" + input.components[(j + 1)].valueText + "' id='inputComponent" + (j + 1) + "' /></td>\
                <td><textarea style='width:90%' name='input" + (i) + "[]' value='" + input.components[(j + 2)].valueText + "' id='inputComponent" + (j + 2) + "'/></td>\
    					</tr>");
          j += 3;
        }
      }
    },
    function(message) {}
  );
});

function addInput(tbl, input) {
  var fields = $("#" + tbl + " input:text").length / 3;
  // $('#' + tbl).append("\
  //   <tr>\
  //     <td><input type='text' name='input" + input + "[]' value='' id='inputComponent" + "' /></td>\
  //     <td><input type='text' name='input" + input + "[]' value='' id='inputComponent" + "' /></td>\
  //     <td><input type='text' name='input" + input + "[]' value='' id='inputComponent" + "'/></td>\
  //   </tr>");
  $('#' + tbl).append("\
    <tr>\
      <td><textarea style='width:90%' name='input" + input + "[]' value='' id='inputComponent" + "' /></td>\
      <td><textarea style='width:90%' name='input" + input + "[]' value='' id='inputComponent" + "' /></td>\
      <td><textarea style='width:90%' name='input" + input + "[]' value='' id='inputComponent" + "'/></td>\
    </tr>");
  $("." + tbl + " input[name=text" + fields + "1]").focus();
}
