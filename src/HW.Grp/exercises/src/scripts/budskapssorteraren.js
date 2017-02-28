var exerciseRepo = new ExerciseRepo();

$(function() {
  $('#div2').hide();

  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-save').click(function() {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
        id: exerciseId,
        sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
        exerciseVariantLanguage: { id: $('#exerciseVariantLangID').val() || 0 },
        inputs: [
          { id: $('.input0').data('id'), components: html.getElementTexts($('.input0 li')) },
          { id: $('.input1').data('id'), components: html.getElementTexts($('.input1 li')) },
          { id: $('.input2').data('id'), components: html.getElementTexts($('.input2 li')) }
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
      $(exercise.inputs[0].components).each(function(i, component) {
        $('<li class="' + component.class + '"><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input0').fadeIn();
      });
      $(exercise.inputs[1].components).each(function(i, component) {
        $('<li class="' + component.class + '"><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input1').fadeIn();
      });
      $(exercise.inputs[2].components).each(function(i, component) {
        $('<li class="' + component.class + '"><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input2').fadeIn();
      });
      init();
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  var $choices = $("#choices"),
    $behavior = $("#behavior"),
    $nonBehavior = $("#non-behavior");

  init();

  function init() {
    $("li", $choices).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });
		console.log('test');

    $behavior.droppable({
      accept: "#choices > li.beteende",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .appendTo($behavior)
            .fadeIn();
        });
      }
    });

    $nonBehavior.droppable({
      accept: "#choices > li.ickebeteende",
      activeClass: "ui-state-highlight",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .appendTo($nonBehavior)
            .fadeIn();
        });
      }
    });

    $choices.droppable({
      accept: "#behavior li, #non-behavior li",
      activeClass: "ui-state-default",
      // hoverClass: "ui-state-hover",
      drop: function(event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function() {
          $item
            .find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($choices)
            .fadeIn();
        });
      }
    });
  }

	var deleteItem = function(e) {
		var $target = $(e.target);
		if ($target.is('.ui-icon-trash')) {
			var $item = $target.parent();
			$item
				.find('.ui-icon-trash')
				.remove()
				.end()
				.prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
				.end();
			$choices.append($item);
		}
		init();
	};

	$behavior.click(deleteItem);
	$nonBehavior.click(deleteItem);

  $('#btn-back').hide();

  $('#btn-cheat').click(function() {
    $("#div1").hide();
    $("#div2").show();
    $('#btn-cheat').hide();
    $('#btn-back').show();
  });

  $('#btn-back').click(function() {
    $('#div1').show();
    $('#div2').hide();
    $('#btn-back').hide();
    $('#btn-cheat').show();
  });
});
