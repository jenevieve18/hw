var exerciseRepo = new ExerciseRepo();

$(function () {
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
        exerciseVariantLanguage: { exerciseVariantLanguage: $('#exerciseVariantLanguage').val() || 0 },
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
      if (exercise === null) {
        exercise = newExercise;
      }
      $(exercise.inputs).each(function(i, input) {
        $('<li><input type="checkbox" name="input[]" value="d' + (i + 1) + '">' + input.valueText + '</li>').appendTo($('.input0'));
        $('<li><span class="ui-icon ui-icon-arrow-4"></span>' + input.valueText + '</li>').appendTo($('#gallery'));
      });
      init();
    },
    function(message, status, error) {
      $('#message').text(message).fadeIn(1000).fadeOut(1000);
    }
  );

  $("#btn-clear1").click(function () {
    var ansArr = ['d1', 'd2', 'd4', 'd6', 'd9', 'd10'];
    $("input[name='input[]']").each(function () {
      if ($.inArray($(this).val(), ansArr) > -1)
        $(this).parent('li').css("color", "green");
    });
  });

  $("#btn-start").click(function () {
    $(".inputs input:checkbox").removeAttr("checked");
    $(".inputs input:checkbox").parent('li').removeAttr("style");
    $('#second-question').hide();
  });

  $("#btn-continue").click(function () {
    $("#second-question").fadeIn();
  });

  function init() {
    var $gallery = $("#gallery"),
      $trash1 = $("#trash1"),
      $trash2 = $("#trash2"),
      $trash3 = $("#trash3"),
      $trash4 = $("#trash4"),
      $trash5 = $("#trash5");

    $("li", $gallery).draggable({
      cancel: "a.ui-icon", // Clicking an icon won't initiate dragging
      revert: "invalid", // When not dropped, the item will back to initial position
      helper: "clone",
      cursor: "move"
    });

    $trash1.droppable({
      accept: "#gallery > li, #trash2 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        deleteImage(ui.draggable, $trash1);
      }
    });

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
    }

    $trash2.droppable({
      accept: "#gallery > li, #trash1 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        deleteImage(ui.draggable, $trash2);
      }
    });

    $trash3.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        deleteImage(ui.draggable, $trash3);
      }
    });

    $trash4.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash3 li, #trash5 li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        deleteImage(ui.draggable, $trash4);
      }
    });

    $trash5.droppable({
      accept: "#gallery > li, #trash1 li, #trash2 li, #trash3 li, #trash4 li",
      activeClass: "ui-state-highlight",
      drop: function (event, ui) {
        deleteImage(ui.draggable, $trash5);
      }
    });

    $gallery.droppable({
      accept: "#trash1 li, #trash2 li, #trash3 li, #trash4 li, #trash5 li",
      activeClass: "ui-state-default",
      drop: function (event, ui) {
        recycleImage(ui.draggable);
      }
    });

    function deleteImage($item, $trash) {
      $item.fadeOut(function () {
        // var $list = $("ul", $trash).length ?
        //   $("ul", $trash) :
        //   $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);

        $item.find("span.ui-icon").remove();
        // $item.append().appendTo($list).fadeIn(function () {
        $item
          .prepend('<span class="ui-icon ui-icon-trash"></span>')
          .appendTo($trash)
          .fadeIn(function () {
          // resetclass($trash);
        });
      });
    }

    function recycleImage($item) {
      $item.fadeOut(function () {
        $item.find('span.ui-icon').remove();
        $item
          .find(".ui-icon-refresh")
          .remove()
          .end()
          .prepend('<span class="ui-icon ui-icon-arrow-4"></span>')
          .appendTo($gallery)
          .fadeIn();
        // resetclass();
      });
    }

    // function resetclass($trash) {
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
  }

  $('#btn-clear2').click(function () {
    var arr = [
      ['Gå ut och titta på en vägg på ett bygge efter att personen som jobbat med den gått därifrån.', 'Läsa igenom en medarbetares rapport innan den är klar.'],
      ['Lyssna när ett par av dina medarbetare pratar om hur de ska lösa ett problem.', 'Titta på när en medarbetare leder ett möte.'],
      ['Fråga: “Skickade du iväg materialet som vi pratat om?”'],
      ['Fråga: ”Gick Kalle igenom projektplanen med er idag?”'],
      ['Säga: Ni har väl sett att vi fått nya rutiner för hur vi rapporterar frånvaro?', 'Fråga en av medarbetarna i ett stort projekt: ”Kommer vi hinna i tid?”', 'Prata med medarbetarna om vad de gjort i helgen?', 'Fråga: Varför har du inte lämnat in rapporten i tid?', 'Säga: ”Då kör vi hårt idag, eller hur!?”', 'Säga: Den här rapporten bör vara max 3 sidor och innehålla minst 2 figurer.']
    ];

    for (var t = 1; t <= 5; t++) {
      $("#trash" + t + " ul li").css('color', 'red');
      for (var j = 1; j <= $("#trash" + t + " li").length; j++) {
        var str = $("#trash" + t + " ul li:nth-child(" + j + ")")[0].innerHTML;
        if (jQuery.inArray(str, arr[t - 1]) > -1) {
          $("#trash" + t + " ul li.item-" + j).css('color', '#81BE23');
        }
      }
    }
    $("#body1").show(300);
  });

  $('#btn-notify').click(function () {
    var arr = [
      ['Gå ut och titta på en vägg på ett bygge efter att personen som jobbat med den gått därifrån.', 'Läsa igenom en medarbetares rapport innan den är klar.'],
      ['Lyssna när ett par av dina medarbetare pratar om hur de ska lösa ett problem.', 'Titta på när en medarbetare leder ett möte.'],
      ['Fråga: “Skickade du iväg materialet som vi pratat om?”'],
      ['Fråga: ”Gick Kalle igenom projektplanen med er idag?”'],
      ['Säga: Ni har väl sett att vi fått nya rutiner för hur vi rapporterar frånvaro?', 'Fråga en av medarbetarna i ett stort projekt: ”Kommer vi hinna i tid?”', 'Prata med medarbetarna om vad de gjort i helgen?', 'Fråga: Varför har du inte lämnat in rapporten i tid?', 'Säga: ”Då kör vi hårt idag, eller hur!?”', 'Säga: Den här rapporten bör vara max 3 sidor och innehålla minst 2 figurer.']
    ];

    var s = 0;
    for (var a = 0; a <= 4; a++) {
      s++;
      $("<ul class='gallery ui-helper-reset'>").appendTo("#trash" + s);
      for (var b = 0; b <= arr[a].length - 1; b++) {
        $("<li class='ui-draggable item-1' style='display: list-item;' >" + arr[a][b] + "</li>").appendTo("#trash" + s + " ul");
      }
      $("</ul>").appendTo("#trash" + s);
    }

    // $("#gallery").css("display", "none");
    // $('#btn-notify').css("display", "none");
    $('#btn-notify').hide();
  });
});
