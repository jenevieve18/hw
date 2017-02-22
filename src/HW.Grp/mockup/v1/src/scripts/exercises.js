$(document).ready(function() {
  $(".dropdown dt a").click(function() {
    if ($(this).parent().parent().find("dd ul").hasClass("activated")) {
      $(".dropdown dd ul").hide();
      $(".activated").removeClass("activated");
    } else {
      $(".activated").removeClass("activated");
      $(".dropdown dd ul").hide();
      $(this).parent().parent().find("dd ul").toggle();
      $(this).parent().parent().find("dd ul").addClass("activated");
    }
  });
  $(".dropdown dd ul li a").click(function() {
    var text = $(this).html();
    $(this).parent().parent().parent().parent().find("dt a span").html(text);
    $(".dropdown dd ul").hide();
  });
  $(document).bind('click', function(e) {
    var $clicked = $(e.target);
    if (!$clicked.parents().hasClass("dropdown")) {
      $(".dropdown dd ul").hide();
      $(".activated").removeClass("activated");
    }
  });
  $(".bar .overview").toggle(function() {
    $(this).parent().find(".detail").slideDown('fast', function() {
      $(this).parent().addClass("active");
    });
  }, function() {
    $(this).parent().find(".detail").slideUp('fast', function() {
      $(this).parent().removeClass("active");
    });
  });
});
