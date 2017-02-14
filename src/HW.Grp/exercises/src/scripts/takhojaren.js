var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  var fixedValueText = "Vi ger varandra återkoppling\nVi uppmuntrar andra när det går bra för dem\nVi tackar för förslag från andra\nVi kommer med synpunkter och idéer\nVi ser även ”dåliga” förslag och idéer som viktiga grogrunder för utveckling.";

  html.accordion('.accordion');

  $('#input1').val(fixedValueText);

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        inputs: html.getElementValues($('textarea[id^="input"]')),
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        id: exerciseId
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
        $('.accordion h3').click();
        $.each(exercise.inputs, function(i, input) {
          $('textarea[id="input' + i + '"]').val(input.valueText);
        });
      }
    },
    function(message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );
});
