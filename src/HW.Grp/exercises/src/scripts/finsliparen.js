var exerciseRepo = new ExerciseRepo();

$(function() {
  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-save').click(function() {
    var inputs = [];
    inputs.push({ id: $('.input0').data('id'), components: html.getElementValues($('[name^="input[]')) });
    inputs.push({ id: $('.input1').data('id'), components: html.getElementTexts($('.input1 li')) });
    inputs.push({ id: $('.input2').data('id'), components: html.getElementTexts($('.input2 li')) });
    inputs.push({ id: $('.input3').data('id'), components: html.getElementTexts($('.input3 li')) });
    inputs.push({ id: $('.input4').data('id'), components: html.getElementTexts($('.input4 li')) });
    inputs.push({ id: $('.input5').data('id'), components: html.getElementTexts($('.input5 li')) });
    inputs.push({ id: $('.input6').data('id'), components: html.getElementTexts($('.input6 li')) });
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: inputs
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
      var isNewExercise = false;
      if (exercise === null) {
        isNewExercise = true;
        exercise = newExercise;
      }
      setExercise(exercise, isNewExercise);
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  function setExercise(exercise, isNewExercise) {
    $('.input0').empty();
    $(exercise.inputs[0].components).each(function(i, c) {
      var checked = (c.valueInt == 1 ? 'checked' : '');
      $('<li><input type="checkbox" name="input[]" value="d' + (i + 1) + '" ' + checked + '>' + components[i].valueText + '</li>').appendTo($('.input0'));
    });
    for (i = 1; i <= 5; i++) {
      $('.input' + i).empty();
      $(exercise.inputs[i].components).each(function(j, c) {
        $('<li><span class="' + c.class + ' ui-icon ui-icon-trash"></span>' + c.valueText + '</li>').appendTo($('.input' + i));
      });
    }
    $('.input6').empty();
    $(exercise.inputs[6].components).each(function(i, c) {
      $('<li><span class="' + c.class + ' ui-icon ui-icon-arrow-4"></span>' + c.valueText + '</li>').appendTo($('.input6'));
    });
    if (!isNewExercise) {
      $('#btn-continue').click();
    }
    init();
  }

  $("#btn-start").click(function() {
    setExercise(newExercise, true);
    $('#second-question').hide();
  });

  $("#btn-continue").click(function() {
    $("#second-question").fadeIn();
  });

  var $gallery = $("#gallery"),
    $trash1 = $("#trash1"),
    $trash2 = $("#trash2"),
    $trash3 = $("#trash3"),
    $trash4 = $("#trash4"),
    $trash5 = $("#trash5");

  function init() {
    $("li", $gallery).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will back to initial position
      helper: "clone",
      cursor: "move"
    });

    $trash1.droppable({
      accept: "#gallery > li, #trash2 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        dropItem(ui.draggable, $trash1);
      }
    });

    $trash2.droppable({
      accept: "#gallery > li, #trash1 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        dropItem(ui.draggable, $trash2);
      }
    });

    $trash3.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        dropItem(ui.draggable, $trash3);
      }
    });

    $trash4.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash3 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        dropItem(ui.draggable, $trash4);
      }
    });

    $trash5.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash3 li, #trash4 li",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        dropItem(ui.draggable, $trash5);
      }
    });

    $gallery.droppable({
      accept: "#trash1 li, #trash2 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-default",
      drop: function(event, ui) {
        recycleItem(ui.draggable);
      }
    });

    function dropItem($item, $trash) {
      $item.fadeOut(function() {
        $item.find("span.ui-icon").remove();
        $item
          .prepend('<span class="ui-icon ui-icon-trash"></span>')
          .appendTo($trash)
          .fadeIn();
      });
    }

    function recycleItem($item) {
      $item.fadeOut(function() {
        $item.find('span.ui-icon').remove();
        $item
          .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
          .appendTo($gallery)
          .fadeIn();
      });
    }
  }

  $trash1.click(function(e) {
    removeItem($gallery, e);
  });

  $trash2.click(function(e) {
    removeItem($gallery, e);
  });

  $trash3.click(function(e) {
    removeItem($gallery, e);
  });

  $trash4.click(function(e) {
    removeItem($gallery, e);
  });

  $trash5.click(function(e) {
    removeItem($gallery, e);
  });

  function removeItem($gallery, e) {
    var $target = $(e.target);
    if ($target.is('.ui-icon-trash')) {
      var $item = $target.parent();
      $item
        .find('.ui-icon-trash')
        .remove()
        .end()
        .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
        .end();
      $gallery.append($item);
    }
    init();
  }

  $("#btn-clear1").click(function() {
    var answers = ['d1', 'd2', 'd4', 'd6', 'd9', 'd10'];
    $("input[name='input[]']").each(function() {
      if ($.inArray($(this).val(), answers) > -1)
        $(this).parent('li').css("color", "green");
    });
  });

  $('#btn-clear2').click(function() {
    var answers = [
      [components[3].valueText, components[9].valueText],
      [components[0].valueText, components[1].valueText],
      [components[5].valueText],
      [components[8].valueText],
      [components[2].valueText, components[4].valueText]
    ];
    for (i = 1; i <= 5; i++) {
      $('#trash' + i + ' li').css('color', 'red');
      for (j = 1; j <= $('#trash' + i + ' li').length; j++) {
        var element = $("#trash" + i + " li:nth-child(" + j + ")");
        var str = element.text();
        if (jQuery.inArray(str, answers[i - 1]) > -1) {
          $('#trash' + i + ' li:nth-child(' + j + ")").css('color', '#81BE23');
        }
      }
    }
    $("#body1").show(300);
  });

  $('#btn-notify').click(function() {
    $('.input6').empty();
    $(components).each(function(i, c) {
      $('<li>' + c.valueText + '</li>').appendTo($('.input6'));
    });

    var answers = [
      [components[3].valueText, components[9].valueText],
      [components[0].valueText, components[1].valueText],
      [components[5].valueText],
      [components[8].valueText],
      [components[2].valueText, components[4].valueText]
    ];
    for (i = 1; i <= 5; i++) {
      $('.input' + i).empty();
      $(answers[i -1]).each(function(j, c) {
        $('<li>' + c + '</li>').appendTo($('.input' + i));
      });
    }

    $('#btn-notify').hide();
  });
});
