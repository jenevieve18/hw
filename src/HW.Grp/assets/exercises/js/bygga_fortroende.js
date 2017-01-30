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
      $(exercise.inputs[0].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input0').fadeIn();
      });
      $(exercise.inputs[1].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input1').fadeIn();
      });
      $(exercise.inputs[2].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input2').fadeIn();
      });
      $(exercise.inputs[3].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input3').fadeIn();
      });
      $(exercise.inputs[4].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-trash"></span>' + component.valueText + '</li>').appendTo('.input4').fadeIn();
      });
      $(exercise.inputs[5].components).each(function (i, component) {
        $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + component.valueText + '</li>').appendTo('.input5').fadeIn();
      });
      init();
    },
    function (message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  function init() {
    var $gallery = $("#gallery"),
      $gallery2 = $("#gallery2"),
      $trash = $("#trash"),
      $trash2 = $("#trash2"),
      $trash3 = $("#trash3"),
      $trash4 = $("#trash4");

    $("li", $gallery).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      appendTo: "body",
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $("li", $gallery2).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      appendTo: "body",
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $('li', $trash).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });
    $('li', trash2).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $trash.droppable({
      accept: "#gallery > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find(".ui-icon-arrow-4")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .appendTo($trash)
            .fadeIn();
        });
      }
    });

    $trash2.droppable({
      accept: "#gallery > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($trash2)
            .fadeIn();
        });
      }
    });

    $gallery.droppable({
      accept: "#trash li, #trash2 li",
      activeClass: "ui-state-default",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find(".ui-icon-trash")
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
            .appendTo($gallery)
            .fadeIn();
        });
      }
    });

    $('li', $trash3).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });
    $('li', trash4).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will revert back to its initial position
      helper: "clone",
      cursor: "move"
    });

    $trash3.droppable({
      accept: "#gallery2 > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($trash3)
            .fadeIn();
        });
      }
    });

    $trash4.droppable({
      accept: "#gallery2 > li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        var $item = ui.draggable;
        $item.fadeOut(function () {
          $item.find('.ui-icon-arrow-4')
            .remove()
            .end()
            .prepend('<span class="ui-icon ui-icon-trash"></span>')
            .prependTo($trash4)
            .fadeIn();
        });
      }
    });

    $gallery2.droppable({
      accept: "#trash3 li, #trash4 li",
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
          // resetclass();
        });
      }
    });

    // function deleteImage($item) {
    //   $item.fadeOut(function() {
    //     var $list = $("ul", $trash).length ?
    //       $("ul", $trash) :
    //       $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);

    //     $item.find("a.ui-icon-trash").remove();
    //     $item.append().appendTo($list).fadeIn(function() {
    //       $item
    //         .find("img")
    //         .animate({
    //           height: "36px"
    //         });
    //       resetclass();
    //     });
    //   });
    // }

    // function deleteImage2($item) {
    //   $item.fadeOut(function () {
    //     var $list = $("ul", $trash2).length ?
    //       $("ul", $trash2) :
    //       $("<ul class='gallery ui-helper-reset'/>").appendTo($trash2);

    //     $item.find("a.ui-icon-trash2").remove();
    //     $item.append().appendTo($list).fadeIn(function () {
    //       $item
    //         .find("img")
    //         .animate({
    //           height: "36px"
    //         });
    //       resetclass();
    //     });
    //   });
    // }

    // function deleteImage3($item) {
    //   $item.fadeOut(function () {
    //     var $list = $("ul", $trash3).length ?
    //       $("ul", $trash3) :
    //       $("<ul class='gallery ui-helper-reset'/>").appendTo($trash3);

    //     $item.find("a.ui-icon-trash3").remove();
    //     $item.append().appendTo($list).fadeIn(function () {
    //       $item
    //         .find("img")
    //         .animate({
    //           height: "36px"
    //         });
    //       resetclass2();
    //     });
    //   });
    // }

    // function deleteImage4($item) {
    //   $item.fadeOut(function () {
    //     var $list = $("ul", $trash4).length ?
    //       $("ul", $trash4) :
    //       $("<ul class='gallery ui-helper-reset'/>").appendTo($trash4);

    //     $item.find("a.ui-icon-trash4").remove();
    //     $item.append().appendTo($list).fadeIn(function () {
    //       $item
    //         .find("img")
    //         .animate({
    //           height: "36px"
    //         });
    //       resetclass2();
    //     });
    //   });
    // }

    // function recycleImage($item) {
    //   $item.fadeOut(function () {
    //     $item
    //       .find("a.ui-icon-refresh")
    //       .remove()
    //       .end()
    //       .appendTo($gallery)
    //       .fadeIn();
    //     resetclass();
    //   });
    // }

    // function recycleImage2($item) {
    //   $item.fadeOut(function () {
    //     $item
    //       .find("a.ui-icon-refresh")
    //       .remove()
    //       .end()
    //       .appendTo($gallery2)
    //       .fadeIn();
    //     resetclass2();
    //   });
    // }

    // function resetclass() {
    //   $("ul li", $trash).removeAttr('class');
    //   for (var j = 1; j <= $("li", $trash).length; j++) {
    //     $("ul li:nth-child(" + j + ")", $trash).addClass(function () {
    //       return "ui-draggable item-" + j;
    //     });
    //   }

    //   $("ul li", $trash2).removeAttr('class');
    //   for (var j = 1; j <= $("li", $trash2).length; j++) {
    //     $("ul li:nth-child(" + j + ")", $trash2).addClass(function () {
    //       return "ui-draggable item-" + j;
    //     });
    //   }
    //   $("li", $gallery).css('color', '#666');
    // }

    // function resetclass2() {
    //   $("ul li", $trash3).removeAttr('class');
    //   for (var j = 1; j <= $("li", $trash3).length; j++) {
    //     $("ul li:nth-child(" + j + ")", $trash3).addClass(function () {
    //       return "ui-draggable item-" + j;
    //     });
    //   }

    //   $("ul li", $trash4).removeAttr('class');
    //   for (var j = 1; j <= $("li", $trash4).length; j++) {
    //     $("ul li:nth-child(" + j + ")", $trash4).addClass(function () {
    //       return "ui-draggable item-" + j;
    //     });
    //   }
    //   $("li", $gallery2).css('color', '#666');
    // }
  }

  $('#submit').click(function () {
    for (var j = 1; j <= $("li", $trash).length; j++) {
      test($("ul li:nth-child(" + j + ")", $trash)[0].innerHTML, j);
    }
    for (var j = 1; j <= $("li", $trash2).length; j++) {
      testa($("ul li:nth-child(" + j + ")", $trash2)[0].innerHTML, j);
    }
  });

  $('#submit2').click(function () {
    for (var j = 1; j <= $("li", $trash3).length; j++) {
      test1($("ul li:nth-child(" + j + ")", $trash3)[0].innerHTML, j);
    }
    for (var j = 1; j <= $("li", $trash4).length; j++) {
      testa1($("ul li:nth-child(" + j + ")", $trash4)[0].innerHTML, j);
    }
  });

  function test(str, ab) {
    var arr = ['Uppmana medarbetare att säga sin åsikt', 'Be medarbetarna förbereda någon punkt till mötet'];
    if (jQuery.inArray(str, arr) > -1) {
      $("ul li.item-" + ab, $trash).css('color', '#81BE23');
    } else {
      $("ul li.item-" + ab, $trash).css('color', '#ff0000');
    }
  }

  function testa(str, ab) {
    var arr2 = ['Tacka för synpunkterna', 'Tala om vad som händer härnäst i det av medarbetaren vidtalade ärendet', 'Be om fler synpunkter på det som någon sagt'];
    if (jQuery.inArray(str, arr2) > -1) {
      $("ul li.item-" + ab, $trash2).css('color', '#81BE23');
    } else {
      $("ul li.item-" + ab, $trash2).css('color', '#ff0000');
    }
  }

  function test1(str, ab) {
    var arr3 = ['Säga ”lägg det andra åt sidan och koncentrera dig på det här', 'Fråga om han/hon kan åta sig det här mot att bli avlastad i något annat'];
    if (jQuery.inArray(str, arr3) > -1) {
      $("ul li.item-" + ab, $trash3).css('color', '#81BE23');
    } else {
      $("ul li.item-" + ab, $trash3).css('color', '#ff0000');
    }
  }

  function testa1(str, ab) {
    var arr4 = ['Fråga hur det går och betona att det andra får ligga så länge', 'Tacka för utfört arbete och fråga om han/hon behöver hjälp att prioritera bland de ordinarie uppgifterna'];
    if (jQuery.inArray(str, arr4) > -1) {
      $("ul li.item-" + ab, $trash4).css('color', '#81BE23');
    } else {
      $("ul li.item-" + ab, $trash4).css('color', '#ff0000');
    }
  }
});
