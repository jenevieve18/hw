var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  var answers = [];
  for (var i = 0; i < newExercise.inputs[0].components.length; i++) {
    var s1 = newExercise.inputs[0].components[i].valueText;
    var s2 = newExercise.inputs[1].components[i].valueText;
    answers.push(s1 + ' ' + s2);
  }

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: [
          { id: $('#input0').data('id'), components: html.getElementTexts($('#input0 li')) },
          { id: $('#input1').data('id'), components: html.getElementTexts($('#input1 li')) }
        ]
      },
      function(message) {
        $('#message').text(message).fadeIn().fadeOut();
      },
      function(message, status, error) {
        $('#message').text(message).faceIn().fadeOut();
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      var isNewExercise = false;
      if (exercise === null) {
        exercise = newExercise;
        isNewExercise = true;
      }
      $(exercise.inputs).each(function(i, input) {
        var shuffledComponents = input.components;
        if (isNewExercise) {
          shuffledComponents = arrayShuffle(input.components);
        }
        $(shuffledComponents).each(function(j, component) {
          $('#input' + i).append('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>');
        });
      });
      init();
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  function arrayShuffle(oldArray) {
    var newArray = oldArray.slice();
    var len = newArray.length;
    var i = len;
    while (i--) {
      var p = parseInt(Math.random() * len);
      var t = newArray[i];
      newArray[i] = newArray[p];
      newArray[p] = t;
    }
    return newArray;
  }

  function init() {
    // $('#input0').sortable().disableSelection();
    $('#input1').sortable().disableSelection();
  }

  $('#btn-clear').click(function() {
    $('#input0 li').removeAttr('style');
    $('#input1 li').removeAttr('style');
  });

  $('#btn-correct').click(function() {
    var correctAnswers = [];
    var answers = [];
    for (var i = 0; i < newExercise.inputs[0].components.length; i++) {
      var correctAnswer = newExercise.inputs[0].components[i].valueText + ' ' + newExercise.inputs[1].components[i].valueText;
      correctAnswers.push(correctAnswer.trim());
      var answer = $('#input0 li:nth-child(' + (i + 1) + ')').text() + ' ' + $('#input1 li:nth-child(' + (i + 1) + ')').text();
      answers.push(answer.trim());
    }
    // console.log(correctAnswers);
    $(answers).each(function(i, a) {
      if ($.inArray(a, correctAnswers) > -1) {
        $('#input0 li:nth-child(' + (i + 1) + ')').css('color', 'green');
        $('#input1 li:nth-child(' + (i + 1) + ')').css('color', 'green');
      } else {
        $('#input0 li:nth-child(' + (i + 1) + ')').css('color', 'red');
        $('#input1 li:nth-child(' + (i + 1) + ')').css('color', 'red');
      }
    });
  });
});
