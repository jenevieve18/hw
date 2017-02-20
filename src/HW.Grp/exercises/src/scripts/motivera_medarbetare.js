var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  var answers1 = ['m2', 'm4'];
  var answers2 = ['m1'];

  $('input[name="input0[]"]').change(function() {
    var checked = $('input[name="input0[]"]:checked').length;
    if (checked === answers1.length) {
      $('input[name="input0[]"]:not(:checked)').attr('disabled', true);
    } else {
      $('input[name="input0[]"]:disabled').attr('disabled', false);
    }
  });

  $("#btn-correct1").click(function() {
    $('#second-question').fadeIn();
    $("input[name='input0[]']").each(function() {
      if ($.inArray($(this).val(), answers1) > -1) {
        $(this).parent('li').css("color", "#81BE23");
      }
    });
  });

  $("#btn-correct2").click(function() {
    $("input[name='input1[]']").each(function() {
      if ($.inArray($(this).val(), answers2) > -1) {
        $(this).parent('li').css("color", "#81BE23");
      }
    });
    $(".description").fadeIn();
  });

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        inputs: html.getElementValues($('[id^="input"]')),
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
        $('#btn-correct1').click();
        $('#btn-correct2').click();
        $.each(exercise.inputs, function(i, input) {
          if ((input.type == 3 || input.type == 1) && input.valueInt == 1) {
            $('[id="input' + i + '"]').attr('checked', true);
          }
        });
        $('input[name="input0[]"]').change();
      }
    },
    function(message) {}
  );
});
