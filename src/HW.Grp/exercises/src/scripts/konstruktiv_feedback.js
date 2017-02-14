var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 81;

  $("#submit").click(function() {
    $(".green").css('color', 'green');
    $(".yellow").css('color', '#F5850B');
    $(".blue").css('color', '#3882AE');
    $('.msg').fadeIn();
  });

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        inputs: html.getInputs($('[id^="input"]')),
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
      if (exercise !== null) {
        $('#submit').click();
        $.each(exercise.inputs, function(i, input) {
          if (input.type === 2) {
            $('[id="input' + i + '"]').val(input.valueText);
          } else if (input.type === 3 && input.valueInt === 1) {
            $('[id="input' + i + '"]').attr('checked', true);
          } else if (input.type === 1 && input.valueInt === 1) {
            $('[id="input' + i + '"]').attr('checked', true);
          }
        });
      }
    },
    function(message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );
});
