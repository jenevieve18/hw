$(function () {
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

  var $gallery = $("#gallery"),
    $trash1 = $("#trash1"),
    $trash2 = $("#trash2"),
    $trash3 = $("#trash3"),
    $trash4 = $("#trash4"),
    $trash5 = $("#trash5");

  $("li", $gallery).draggable({
    cancel: "a.ui-icon", // clicking an icon won't initiate dragging
    revert: "invalid", // when not dropped, the item will back to initial position
    helper: "clone",
    cursor: "move"
  });

  $trash1.droppable({
    accept: "#gallery > li",
    activeClass: "ui-state-highlight",
    drop: function (event, ui) {
      deleteImage(ui.draggable, $trash1);
    }
  });

  $trash2.droppable({
    accept: "#gallery > li",
    activeClass: "ui-state-highlight",
    drop: function (event, ui) {
      deleteImage(ui.draggable, $trash2);
    }
  });

  $trash3.droppable({
    accept: "#gallery > li",
    activeClass: "ui-state-highlight",
    drop: function (event, ui) {
      deleteImage(ui.draggable, $trash3);
    }
  });

  $trash4.droppable({
    accept: "#gallery > li",
    activeClass: "ui-state-highlight",
    drop: function (event, ui) {
      deleteImage(ui.draggable, $trash4);
    }
  });

  $trash5.droppable({
    accept: "#gallery > li",
    activeClass: "ui-state-highlight",
    drop: function (event, ui) {
      deleteImage(ui.draggable, $trash5);
    }
  });

  $gallery.droppable({
    accept: "#trash1 li, #trash2 li, #trash3 li, #trash4 li, #trash5 li",
    activeClass: "custom-state-active",
    drop: function (event, ui) {
      recycleImage(ui.draggable);
    }
  });

  function deleteImage($item, $trash) {
    $item.fadeOut(function () {
      var $list = $("ul", $trash).length ?
        $("ul", $trash) :
        $("<ul class='gallery ui-helper-reset'/>").appendTo($trash);

      $item.find("a.ui-icon-trash").remove();
      $item.append().appendTo($list).fadeIn(function () {
        resetclass($trash);
      });
    });
  }

  function recycleImage($item) {
    $item.fadeOut(function () {
      $item
        .find("a.ui-icon-refresh")
        .remove()
        .end()
        .appendTo($gallery)
        .fadeIn();
      resetclass();
    });
  }

  function resetclass($trash) {
    $("ul li", $trash).removeAttr('class');
    for (var j = 1; j <= $("li", $trash).length; j++) {
      $("ul li:nth-child(" + j + ")", $trash).addClass(function () {
        return "ui-draggable item-" + j;
      });
    }

    $("ul li", $trash2).removeAttr('class');
    for (var j = 1; j <= $("li", $trash2).length; j++) {
      $("ul li:nth-child(" + j + ")", $trash2).addClass(function () {
        return "ui-draggable item-" + j;
      });
    }
    $("li", $gallery).css('color', '#666');
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

    $("#gallery").css("display", "none");
    // $('#btn-notify').css("display", "none");
    $('#btn-notify').hide();
  });
});
