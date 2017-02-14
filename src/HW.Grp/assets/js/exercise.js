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
    });

    $(this).find('.exercise-comment-text').hide();
    $(this).find('.spinner').hide();
    //$('.exercise-comments').click(function () {
    //    console.log($(this).find('.test'));
    //    $(this).find('.hw-icon-exercise').hide();
    //    $(this).find('.exercise-comment-text').show();
    //});

    $('.exercise-comments').click(function () {
        var text = $(this).find('.exercise-comment-text');
        $(this).find('.exercise-comment-label').hide();
        text.show();
        text.focus();
        $(this).find('.hw-icon-exercise').hide();
    });
    $('.exercise-comment-text').focusout(function () {
        var comments = $(this).val();
        var id = $(this).data('id') || 0;
        var icon = $(this).closest('td').find('.hw-icon-exercise');
        var label = $(this).closest('td').find('.exercise-comment-label');
        label.text(comments);

        var spinner = $(this).closest('td').find('.spinner');
        spinner.show();
        $.ajax({
            type: 'POST',
            url: 'Service.aspx/UpdateManagerExerciseComments',
            data: JSON.stringify({ sponsorAdminExerciseID: id, comments: comments }),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (msg) {
                spinner.hide();
            },
            error: function (msg) {
                spinner.hide();
            }
        });
        $(this).hide();
        icon.show();
        label.show();
    });
})