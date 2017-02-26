var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-clock').click(function() {
    var count = 15;
    countdown = setInterval(function() {
      $('#clock').text(count + ' sekunder terstr');
      if (count > 0) {
        $('#clock').show();
        count--;
      } else {
        $('#clock').hide();
        clearInterval(countdown);
      }
    }, 1000);
  });

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        inputs: html.getInputs($('textarea[id^="input"]')),
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
        $.each(exercise.inputs, function(i, input) {
          $('textarea[id="input' + i + '"]').val(input.valueText);
        });
      }
    });
});
