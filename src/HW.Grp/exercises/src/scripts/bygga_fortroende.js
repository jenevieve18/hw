var exerciseRepo = new ExerciseRepo();

$(function() {

  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  var $gallery1 = $("#gallery1"),
    $gallery2 = $("#gallery2"),
    $trigger1 = $("#trigger1"),
    $impact1 = $("#impact1"),
    $trigger2 = $("#trigger2"),
    $impact2 = $("#impact2");

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: [
          { id: $('.input0').data('id'), components: html.getElementTexts($('.input0 li')) },
          { id: $('.input1').data('id'), components: html.getElementTexts($('.input1 li')) },
          { id: $('.input2').data('id'), components: html.getElementTexts($('.input2 li')) },
          { id: $('.input3').data('id'), components: html.getElementTexts($('.input3 li')) },
          { id: $('.input4').data('id'), components: html.getElementTexts($('.input4 li')) },
          { id: $('.input5').data('id'), components: html.getElementTexts($('.input5 li')) }
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
      setInputs({ input: exercise.inputs[0], container: '.input0' }, { input: exercise.inputs[1], container: '.input1' }, { input: exercise.inputs[2], container: '.input2' });
      setInputs({ input: exercise.inputs[3], container: '.input3' }, { input: exercise.inputs[4], container: '.input4' }, { input: exercise.inputs[5], container: '.input5' });
      init();
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  function setInputs(input1, input2, input3) {
    $(input1.container).empty();
    $(input2.container).empty();
    $(input3.container).empty();
    $(input1.input.components).each(function(i, component) {
      $('<li class="' + component.class + '"><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo(input1.container).fadeIn();
    });
    $(input2.input.components).each(function(i, component) {
      $('<li class="' + component.class + '"><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo(input2.container).fadeIn();
    });
    $(input3.input.components).each(function(i, component) {
      $('<li class="' + component.class + '"><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo(input3.container).fadeIn();
    });
    // init();
  }

  function init() {
    $("li", $gallery1).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      appendTo: "body",
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    // $('li', $trigger1).draggable({
    //   cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
    //   revert: "invalid", // When not dropped, the item will revert back to its initial position
    //   helper: "clone",
    //   cursor: "move"
    // });
    // $('li', $impact1).draggable({
    //   cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
    //   revert: "invalid", // When not dropped, the item will revert back to its initial position
    //   helper: "clone",
    //   cursor: "move"
    // });

    $impact1.droppable({
      accept: "#gallery1 > li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($impact1)
            .fadeIn();
        });
      }
    });

    $gallery1.droppable({
      accept: "#trigger1 li, #impact1 li",
      activeClass: "ui-state-default",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.removeAttr('style');
        $item.fadeOut(function() {
          $item.find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($gallery1)
            .fadeIn();
        });
      },
      out: function(event, ui) {
        console.log('hello world');
        $(this).droppable('option', 'accept', '.drag-item');
      }
    });

    $('#btn-notify1').click(function() {
      var triggers = [
        newExercise.inputs[2].components[0].valueText,
        newExercise.inputs[2].components[11].valueText
      ];
      $('.input0 li').each(function(i, component) {
        if ($.inArray($(this).text(), triggers) > -1) {
          $(this).css('color', '#81BE23');
        } else {
          $(this).css('color', '#ff0000');
        }
      });
      var impacts = [
        newExercise.inputs[2].components[3].valueText,
        newExercise.inputs[2].components[4].valueText
      ];
      $('.input1 li').each(function(i, component) {
        if ($.inArray($(this).text(), impacts) > -1) {
          $(this).css('color', '#81BE23');
        } else {
          $(this).css('color', '#ff0000');
        }
      });
    });

    $('#btn-reset1').click(function() {
      setInputs({ input: newExercise.inputs[0], container: '.input0' }, { input: newExercise.inputs[1], container: '.input1' }, { input: newExercise.inputs[2], container: '.input2' });
      init();
    });

    // $('li', $trigger2).draggable({
    //   cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
    //   revert: "invalid", // When not dropped, the item will revert back to its initial position
    //   helper: "clone",
    //   cursor: "move"
    // });
    // $('li', $impact2).draggable({
    //   cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
    //   revert: "invalid", // When not dropped, the item will revert back to its initial position
    //   helper: "clone",
    //   cursor: "move"
    // });

    $("li", $gallery2).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      appendTo: "body",
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $trigger2.droppable({
      accept: "#gallery2 > li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($trigger2)
            .fadeIn();
        });
      }
    });

    $impact2.droppable({
      accept: "#gallery2 > li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($impact2)
            .fadeIn();
        });
      }
    });

    $gallery2.droppable({
      accept: "#trigger2 li, #impact2 li",
      activeClass: "ui-state-default",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.removeAttr('style');
        $item.fadeOut(function() {
          $item.find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($gallery2)
            .fadeIn();
        });
      }
    });

    $('#btn-notify2').click(function() {
      var triggers = [
        newExercise.inputs[5].components[0].valueText,
        newExercise.inputs[5].components[1].valueText
      ];
      $('.input3 li').each(function(i, component) {
        if ($.inArray($(this).text(), triggers) > -1) {
          $(this).css('color', '#81BE23');
        } else {
          $(this).css('color', '#ff0000');
        }
      });
      var impacts = [
        newExercise.inputs[5].components[5].valueText,
        newExercise.inputs[5].components[6].valueText
      ];
      $('.input4 li').each(function(i, component) {
        if ($.inArray($(this).text(), impacts) > -1) {
          $(this).css('color', '#81BE23');
        } else {
          $(this).css('color', '#ff0000');
        }
      });
    });

    $('#btn-reset2').click(function() {
      setInputs({ input: newExercise.inputs[3], container: '.input3' }, { input: newExercise.inputs[4], container: '.input4' }, { input: newExercise.inputs[5], container: '.input5' });
      init();
    });
  }

  var deleteTrigger1Item = function(e) {
    var $target = $(e.target);
    if ($target.is('.ui-icon-trash')) {
      var $item = $target.parent();
      $item.removeAttr('style');
      $item
        .find('.ui-icon-trash')
        .remove()
        .end()
        .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
        .end();
      $gallery1.append($item);
    }
    init();
  }

  $trigger1.click(deleteTrigger1Item);
  $impact1.click(deleteTrigger1Item);

  var deleteTrigger2Item = function(e) {
    var $target = $(e.target);
    if ($target.is('.ui-icon-trash')) {
      var $item = $target.parent();
      $item.removeAttr('style');
      $item
        .find('.ui-icon-trash')
        .remove()
        .end()
        .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
        .end();
      $gallery2.append($item);
    }
    init();
  }

  $trigger2.click(deleteTrigger2Item);
  $impact2.click(deleteTrigger2Item);

  // $impact1.click(function(e) {
  //   var $target = $(e.target);
  //   if ($target.is('.ui-icon-trash')) {
  //     var $item = $target.parent();
  //     $item.removeAttr('style');
  //     $item
  //       .find('.ui-icon-trash')
  //       .remove()
  //       .end()
  //       .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
  //       .end();
  //     $gallery1.append($item);
  //   }
  // });
  //
  // $impact2.click(function(e) {
  //   var $target = $(e.target);
  //   if ($target.is('.ui-icon-trash')) {
  //     var $item = $target.parent();
  //     $item.removeAttr('style');
  //     $item
  //       .find('.ui-icon-trash')
  //       .remove()
  //       .end()
  //       .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
  //       .end();
  //     $gallery2.append($item);
  //   }
  // });

  $trigger1.droppable({
    accept: "#gallery1 > li",
    activeClass: "ui-state-highlight",
    drop: function(event, ui) {
      var $item = ui.draggable;
      $item.fadeOut(function() {
        $item.find(".ui-icon-arrow-4")
          .remove()
          .end()
          .prepend('<span class="ui-icon ui-icon-trash"></span>')
          .appendTo($trigger1)
          .fadeIn();
      });
    }
  });
});
