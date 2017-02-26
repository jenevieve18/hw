var exerciseRepo = new ExerciseRepo();

$(function() {
  var $suggestedThings = $("#suggested-things");
  var $ranking = $("#ranking");

  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: [
          { id: $('.input0').data('id'), components: html.getElementTexts($('.input0 li')) },
          { id: $('.input1').data('id'), components: html.getElementTexts($('.input1 li')) }
        ]
      },
      function(message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      },
      function(message, status, error) {
        $('#message').text(message).faceIn(1000).fadeOut(1000);
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function(exercise) {
      if (exercise === null) {
        exercise = newExercise;
      }
      setExercise(exercise);
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  $('#btn-add').click(function() {
    var $txtInput = $("#text-input");
    if ($txtInput.val() !== "") {
      $("<li><span class='ui-icon ui-icon-arrow-4'></span>" + $txtInput.val() + "</li>").appendTo($suggestedThings).fadeIn();
      $txtInput.val('');
      init();
    }
  });

  $('#btn-reset').click(function() {
    setExercise(newExercise);
  });

  $("#btn-subtext").click(function() {
    $("#sub-text").fadeIn();
  });

  function setExercise(exercise) {
    $suggestedThings.empty();
    $ranking.empty();
    $(exercise.inputs[0].components).each(function(i, component) {
      $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input0').fadeIn();
    });
    $(exercise.inputs[1].components).each(function(i, component) {
      $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input1').fadeIn();
    });
    init();
  }

  function init() {
    // $ranking.sortable().disableSelection();
    $("li", $suggestedThings).draggable({
      cancel: "a.ui-icon",
      appendTo: "body",
      revert: "invalid",
      containment: $("#demo-frame").length ? "#demo-frame" : "document",
      helper: "clone",
      cursor: "move"
    });

    $ranking.droppable({
      accept: "#suggested-things > li",
      activeClass: "ui-state-default",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            // .prependTo($ranking)
            .appendTo($ranking)
            .fadeIn();
        });
      }
    });

    $suggestedThings.droppable({
      accept: "#ranking li",
      activeClass: "ui-state-default",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.removeClass('ui-state-default');
        $item.fadeOut(function() {
          $item.find("span.ui-icon").remove();
          $item
            .find(".ui-icon-refresh")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($suggestedThings)
            .fadeIn();
        });
      }
    });
  }

  $('ul#ranking').click(function(e) {
    var $target = $(e.target);
    if ($target.is('.ui-icon-trash')) {
      var $item = $target.parent();
      $item
        .find('.ui-icon-trash')
        .remove()
        .end()
        .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
        .end();
      $suggestedThings.append($item);
    }
  });
});
