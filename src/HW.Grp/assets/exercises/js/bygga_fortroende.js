var exerciseRepo = new ExerciseRepo();

$(function () {

  var exerciseId = $('#sponsorAdminExerciseID').val() || 0;

  $('#btn-save').click(function () {
    healthwatch.grp(exerciseRepo).saveManagerExercise({
      id: exerciseId,
      sponsorAdmin: { id: $('#sponsorAdminID').val() || 0 },
      exerciseVariantLanguage: { exerciseVariantLanguage: $('#exerciseVariantLanguage').val() || 0 },
      inputs: [
        { id: $('.input0').data('id'), components: html.getElementTexts($('.input0 li')) },
        { id: $('.input1').data('id'), components: html.getElementTexts($('.input1 li')) },
        { id: $('.input2').data('id'), components: html.getElementTexts($('.input2 li')) },
        { id: $('.input3').data('id'), components: html.getElementTexts($('.input3 li')) },
        { id: $('.input4').data('id'), components: html.getElementTexts($('.input4 li')) },
        { id: $('.input5').data('id'), components: html.getElementTexts($('.input5 li')) }
      ]
    },
      function (message) {
        $('#message').text(message).fadeIn(1000).fadeOut(1000);
      },
      function (message, status, error) {
        $('#message').text(message).faceIn(1000).fadeOut(1000);
      }
    );
  });

  healthwatch.grp(exerciseRepo).readManagerExercise(
    exerciseId,
    function (exercise) {
      if (exercise === null) {
        exercise = newExercise;
      }
      // $(exercise.inputs[0].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input0').fadeIn();
      // });
      // $(exercise.inputs[1].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input1').fadeIn();
      // });
      // $(exercise.inputs[2].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input2').fadeIn();
      // });
      // $(exercise.inputs[3].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input3').fadeIn();
      // });
      // $(exercise.inputs[4].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input4').fadeIn();
      // });
      // $(exercise.inputs[5].components).each(function (i, component) {
      //   $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input5').fadeIn();
      // });
      setInputs(
        { input: exercise.inputs[0], container: '.input0' }, 
        { input: exercise.inputs[1], container: '.input1' }, 
        { input: exercise.inputs[2], container: '.input2' }
      );
      setInputs(
        { input: exercise.inputs[3], container: '.input3' }, 
        { input: exercise.inputs[4], container: '.input4' }, 
        { input: exercise.inputs[5], container: '.input5' }
      );
      // setExercise(exercise);
      // init();
    },
    function (message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  // function setExercise(exercise) {
  //   $('.input0').empty();
  //   $('.input1').empty();
  //   $('.input2').empty();
  //   $('.input3').empty();
  //   $('.input4').empty();
  //   $('.input5').empty();

  //   $(exercise.inputs[0].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input0').fadeIn();
  //   });
  //   $(exercise.inputs[1].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input1').fadeIn();
  //   });
  //   $(exercise.inputs[2].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input2').fadeIn();
  //   });
  //   $(exercise.inputs[3].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input3').fadeIn();
  //   });
  //   $(exercise.inputs[4].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input4').fadeIn();
  //   });
  //   $(exercise.inputs[5].components).each(function (i, component) {
  //     $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input5').fadeIn();
  //   });
  // }

  function setInputs(input1, input2, input3) {
    $(input1.container).empty();
    $(input2.container).empty();
    $(input3.container).empty();
    $(input1.input.components).each(function (i, component) {
      $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo(input1.container).fadeIn();
    });
    $(input2.input.components).each(function (i, component) {
      $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo(input2.container).fadeIn();
    });
    $(input3.input.components).each(function (i, component) {
      $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo(input3.container).fadeIn();
    });

    init();
  }

  function init() {
    var $gallery1 = $("#gallery1"),
      $gallery2 = $("#gallery2"),
      $trigger1 = $("#trigger1"),
      $impact1 = $("#impact1"),
      $trigger2 = $("#trigger2"),
      $impact2 = $("#impact2");

    $("li", $gallery1).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      appendTo: "body",
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $('li', $trigger1).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });
    $('li', $impact1).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $trigger1.droppable({
      accept: "#gallery1 > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find(".ui-icon-arrow-4")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .appendTo($trigger1)
            .fadeIn();
        });
      }
    });

    $impact1.droppable({
      accept: "#gallery1 > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
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
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($gallery1)
            .fadeIn();
        });
      }
    });

    $('#btn-notify1').click(function () {
      console.log('hello world');
      // for (var j = 1; j <= $("li", $trash).length; j++) {
      //   test($("ul li:nth-child(" + j + ")", $trash)[0].innerHTML, j);
      // }
      // for (var j = 1; j <= $("li", $trash2).length; j++) {
      //   testa($("ul li:nth-child(" + j + ")", $trash2)[0].innerHTML, j);
      // }
    });
    
    $('#btn-reset1').click(function() {
      setInputs(
        { input: newExercise.inputs[0], container: '.input0' }, 
        { input: newExercise.inputs[1], container: '.input1' }, 
        { input: newExercise.inputs[2], container: '.input2' }
      );
      // setExercise(newExercise);
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
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
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
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
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
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($gallery2)
            .fadeIn();
        });
      }
    });

    $('#btn-notify2').click(function () {
      console.log('world hello');
      // for (var j = 1; j <= $("li", $trash3).length; j++) {
      //   test1($("ul li:nth-child(" + j + ")", $trash3)[0].innerHTML, j);
      // }
      // for (var j = 1; j <= $("li", $trash4).length; j++) {
      //   testa1($("ul li:nth-child(" + j + ")", $trash4)[0].innerHTML, j);
      // }
    });
    
    $('#btn-reset2').click(function() {
      setInputs(
        { input: newExercise.inputs[3], container: '.input3' }, 
        { input: newExercise.inputs[4], container: '.input4' }, 
        { input: newExercise.inputs[5], container: '.input5' }
      );
      // setExercise(newExercise);
    });
  }

  // function test(str, ab) {
  //   var arr = ['Uppmana medarbetare att säga sin åsikt', 'Be medarbetarna förbereda någon punkt till mötet'];
  //   if (jQuery.inArray(str, arr) > -1) {
  //     $("ul li.item-" + ab, $trash).css('color', '#81BE23');
  //   } else {
  //     $("ul li.item-" + ab, $trash).css('color', '#ff0000');
  //   }
  // }

  // function testa(str, ab) {
  //   var arr2 = ['Tacka för synpunkterna', 'Tala om vad som händer härnäst i det av medarbetaren vidtalade ärendet', 'Be om fler synpunkter på det som någon sagt'];
  //   if (jQuery.inArray(str, arr2) > -1) {
  //     $("ul li.item-" + ab, $trash2).css('color', '#81BE23');
  //   } else {
  //     $("ul li.item-" + ab, $trash2).css('color', '#ff0000');
  //   }
  // }

  // function test1(str, ab) {
  //   var arr3 = ['Säga ”lägg det andra åt sidan och koncentrera dig på det här', 'Fråga om han/hon kan åta sig det här mot att bli avlastad i något annat'];
  //   if (jQuery.inArray(str, arr3) > -1) {
  //     $("ul li.item-" + ab, $trash3).css('color', '#81BE23');
  //   } else {
  //     $("ul li.item-" + ab, $trash3).css('color', '#ff0000');
  //   }
  // }

  // function testa1(str, ab) {
  //   var arr4 = ['Fråga hur det går och betona att det andra får ligga så länge', 'Tacka för utfört arbete och fråga om han/hon behöver hjälp att prioritera bland de ordinarie uppgifterna'];
  //   if (jQuery.inArray(str, arr4) > -1) {
  //     $("ul li.item-" + ab, $trash4).css('color', '#81BE23');
  //   } else {
  //     $("ul li.item-" + ab, $trash4).css('color', '#ff0000');
  //   }
  // }
});
