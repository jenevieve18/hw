var count = 0;
var exerciseRepo = new ExerciseRepo();

$(function() {
  $('.sortable-inputs').sortable().disableSelection();
  $('.inputs input').keyup(inputKeyUp);

  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-add').click(function() {
    if (!html.hasEmpty($('.inputs input'))) {
      addInput('');
    }
  });

  $('#btn-save').click(function() {
    var exercise = {
      id: exerciseId,
      sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
      exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
      inputs: html.getElementValues($('.sortable-inputs input[id^="text"]')),
    };
    healthwatch.grp(exerciseRepo).saveManagerExercise(
      exercise,
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      },
      function(message, status, error) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      if (exercise !== null) {
        var hasInputs = false;
        $.each(exercise.inputs, function(i, input) {
          addInput(input.valueText);
          hasInputs = true;
        });
        if (!hasInputs) {
          addInput('');
        }
      } else {
        addInput('');
      }
    }
  );

  function addInput(text) {
    $('.inputs').append('<li><input type="text" id="input' + count + '" value="' + text + '" name="input[]"></li>');
    $('.sortable-inputs').append('<li><span class="ui-icon ui-icon-arrow-4"></span><input type="text" id="input' + count + '" value="' + text + '" name="input[]"><span class="ui-icon ui-icon-trash pointer btn-delete" data-id=""></span></li>');
    $('.btn-delete').click(function() {
      $(this).parent().remove();
      var id = $(this).parent().find('input').attr('id');
      $('.inputs input[id="' + id + '"]').parent().remove();
    });
    $('.inputs input').keyup(inputKeyUp);
    count++;
  }

  function inputKeyUp() {
    $('.sortable-inputs input[id="' + this.id + '"]').val(this.value);
  }
});
