var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;
  var behaviors = 0;

  $('#btn-save').click(function() {
    var inputs = [];
    inputs.push({ valueText: $('#input0').val() });
    inputs.push({ valueText: $('#input1').val() });
    inputs.push({ components: html.getElementValues($('[name^="input2[]"]')) });
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: inputs
      },
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      },
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      });
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      if (exercise === null) {
        exercise = newExercise;
      }
      $('#input0').val(exercise.inputs[0].valueText).data('content', exercise.inputs[0].valueText);
      $('#input1').val(exercise.inputs[1].valueText).data('content', exercise.inputs[1].valueText);
      $.each(exercise.inputs[2].components, function(i, component) {
        $('.employee-behaviors').append('<textarea id="' + behaviors + '" name="input2[]" class="input" data-content="' + component.valueText + '">' + component.valueText + '</textarea> ');
        behaviors++;
      });
      init();
    }
  );

  function init() {
    $('.input').click(function() {
      var content = $(this).data('content');
      if (content === $(this).val()) {
        $(this).val("");
      }
    });
    $('.input').blur(function() {
      if ($(this).val() === "") {
        $(this).val($(this).data('content'));
      }
    });
  }

  init();

  $('#btn-add').click(function() {
    for (var i = 0; i < 5; i++) {
      addBehavior(behaviors, initBehavior(behaviors + 1));
      behaviors++;
    }
    init();
  });

  function addBehavior(behaviors, value) {
    $('.employee-behaviors').append('<textarea id="input' + behaviors + '" name="input2[]" class="input" data-content="' + value + '">' + value + '</textarea> ');
  }
});
