// var employees = 5;
// var exerciseRepo = new ExerciseRepo();

var newExericse = {
  inputs: [
    { valueText: 'Företagets vision' },
    { valueText: 'Gruppens uppdrag' },
    { components: [
        { valueText: "Medarbetare 1's beteende" },
        { valueText: "Medarbetare 2's beteende" },
        { valueText: "Medarbetare 3's beteende" },
        { valueText: "Medarbetare 4's beteende" },
        { valueText: "Medarbetare 5's beteende" },
      ]
    }
  ]
};

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  function init() {
    $('.data-input').click(function() {
      var content = $(this).data('content');
      if (content === $(this).val()) {
        $(this).val("");
      }
    });
    $('.data-input').blur(function() {
      if ($(this).val() === "") {
        $(this).val($(this).data('content'));
      }
    });
  }

  init();

  $('#btn-add').click(function() {
    for (var i = 0; i < 5; i++) {
      employees++;
      addDataInput(employees + 1, employees, "Employee " + employees + "'s behavior");
    }
    init();
  });

  function addDataInput(index, employees, value) {
    $('.employee-behaviors').append('<textarea name="data-input[]" class="data-input" id="input' + index + '" data-content="Employee ' + employees + '\'s behavior">' + value + '</textarea> ');
  }

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: html.getElementValues($('[id^="input"]')),
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
      // if (exercise !== null) {
      //   var index = 0;
      //   employees = -2;
      //   $.each(exercise.inputs, function(i, input) {
      //     if (index < 7) {
      //       $('[id="input' + index + '"]').val(input.valueText);
      //     } else {
      //       addDataInput(index, employees, input.valueText);
      //       init();
      //     }
      //     employees++;
      //     index++;
      //   });
      // } else {
      //   employees = 5;
      // }
      // if (exercise === null) {
      //   exercise = newExercise;
      // }
      $.each(exercise.inputs, function(i, input) {
        addDataInput(i, employees, input.valueText);
        employees++;
      });
    }
  );
});
