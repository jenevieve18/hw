var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $("#btn-correct").click(function() {
    var ansArr = ['m2', 'm4'];
    $("input[name='monitorer[]']:checked").each(function() {
      if (jQuery.inArray($(this).val(), ansArr) > -1)
        $(this).parent('li').css("color", "green");
    });
    $('.description').fadeIn();
  });

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
      inputs: html.getElementValues($('[id^="input"]')),
      sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
      exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
      id: exerciseId
    }, function(message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }, function(message) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    });
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      if (exercise !== null) {
				$('#btn-correct').click();
        $.each(exercise.inputs, function(i, input) {
          if (input.type == 2) {
            $('[id="input' + i + '"]').val(input.valueText);
          } else if (input.type == 3 && input.valueInt == 1) {
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
