var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $("#btn-correct").click(function() {
    var answers = ['1', '1', '1', '3'];
    $.each(answers, function(i, a) {
      $("input[name='input" + i + "']").each(function() {
        if ($(this).val() === a) {
          $(this).parent('label').css('color', 'green');
        }
      });
    })
    $('.description').fadeIn();
  });

  $('#btn-save').click(function() {
    var exercise = {
      inputs: [
        { valueInt: $("input[name='input0']:checked").val() },
        { valueInt: $("input[name='input1']:checked").val() },
        { valueInt: $("input[name='input2']:checked").val() },
        { valueInt: $("input[name='input3']:checked").val() },
      ],
      sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
      exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
      id: exerciseId
    };
    healthwatch.grp(exerciseRepo).saveManagerExercise(
      exercise,
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
      if (exercise !== null) {
				$('#btn-correct').click();
        $.each(exercise.inputs, function(i, input) {
          $("input[name='input" + i + "']").each(function() {
            if ($(this).val() == input.valueInt) {
              $(this).attr('checked', 'checked');
            }
          });
        });
      }
    },
    function(message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );
});
