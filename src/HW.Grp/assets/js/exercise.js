$(document).ready(function () {
    /** controls the dropdowns **/
    $(".dropdown dt a").click(function () {
        if ($(this).parent().parent().find("dd ul").hasClass("activated")) {
            $(".dropdown dd ul").hide();
            $(".activated").removeClass("activated")
        } else {
            $(".activated").removeClass("activated")
            $(".dropdown dd ul").hide();
            $(this).parent().parent().find("dd ul").toggle();
            $(this).parent().parent().find("dd ul").addClass("activated")
        }
    });

    $(".dropdown dd ul li a").click(function () {
        var text = $(this).html();
        $(this).parent().parent().parent().parent().find("dt a span").html(text);
        $(".dropdown dd ul").hide();
        //the id field of parent has the control hook, although I'd use a hrefs if you don't want to ajax
        //alert($(this).parent().attr('id'))
    });

    $(document).bind('click', function (e) {
        var $clicked = $(e.target);
        if (!$clicked.parents().hasClass("dropdown")) {
            $(".dropdown dd ul").hide();
            $(".activated").removeClass("activated")
        }
    });

    /** controls bar details **/
    /*$(".bar .detailtoggle").click(function() {
    $(this).parent().find(".detail").slideUp('fast', function() {
    $(this).parent().removeClass("active");
    })
          
    });
    $(".bar:not(.active)").click(function() {
    if(!$(this).hasClass("active")) {
    $(this).find(".detail").slideDown('fast', function() {
    $(this).parent().addClass("active");
    })
    }
    });*/
    $(".bar .overview").toggle(function () {
        $(this).parent().find(".detail").slideDown('fast', function () {
            $(this).parent().addClass("active");
        })
    }, function () {
        $(this).parent().find(".detail").slideUp('fast', function () {
            $(this).parent().removeClass("active");
        })
    })
})