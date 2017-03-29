
String.prototype.format = function () {
    var formatted = this;
    for (var arg in arguments) {
        formatted = formatted.replace("{" + arg + "}", arguments[arg]);
    }
    return formatted;
}
function getPlot(plotType) {
    if (plotType == 1) {
        return { title: 'Line Chart', description: 'Chart displaying mean values.' };
    } else if (plotType == 2) {
        return { title: 'Line Chart with Standard Deviation', description: 'chart displaying mean values with Standard Deviation whiskers. The SD is a theoretical statistical measure that illustrates the range (variation from the average) in which approximately 67 % of the responses are. A low standard deviation indicates that the responses tend to be very close to the mean (lower variation); a high standard deviation indicates that the responses are spread out over a large range of values.' };
    } else if (plotType == 3) {
        return { title: 'Line Chart with Standard Deviation and Confidence Interval', description: 'chart displaying mean values, including whiskers that in average covers 1.96 SD, i.e. a theoretical distribution of approximately 95% of observations.' };
    } else if (plotType == 4) {
        return { title: 'Box Plot Min/Max', description: 'median value chart, including one set of whiskers that covers 50% of observations, and another set of whiskers that captures min and max values' };
    } else if (plotType == 5) {
        return { title: 'Box Plot', description: 'median value chart, similar to the min/max BloxPlot but removes outlying extreme values' };
    } else if (plotType == 6) {
        return { title: 'Verbose', description: 'Verbose' };
    }
}
function f(obj, plotType, all) {
    obj.closest('.report-part').find('.selected-plot-type').text(plotType);
    var plot = getPlot(plotType);
    if (all) {
        $('.chart-description1').text("{0} - {1}".format(plot.title, plot.description));
    } else {
        obj.closest('.report-part').find('.chart-description').find('p').text(plot.description);
    }
}
$(document).ready(function () {
    $('#accordion').accordion({ collapsible: true, active: true, heightStyle: 'content' });
    f($('.report-part'), 3, true);
    $('.report-part-header').hide();
    $('.chart-descriptions').dialog({ autoOpen: false, width: 600, height: 480 });
    $('.chart-descriptions-info').click(function () {
        $('.chart-descriptions').dialog('open');
    });
    $('.report-part-subject').mouseover(function () {
        $(this).closest('.report-part').find('.toggle-right').removeClass('toggle-active').addClass('toggle-active-hover');
    });
    $('.report-part-subject').mouseleave(function () {
        $(this).closest('.report-part').find('.toggle-right').removeClass('toggle-active-hover').addClass('toggle-active-');
    });
    $('.toggle-chart-description').mouseover(function () {
        $(this).removeClass('toggle-active').addClass('toggle-active-hover');
    });
    $('.toggle-chart-description').mouseleave(function () {
        $(this).removeClass('toggle-active-hover').addClass('toggle-active');
    });
    $('.report-part-subject').click(function () {
        $(this).closest('.report-part').find('.report-part-header').slideToggle();
    });
    $('.report-part .action .plot-types').change(function () {
        var partContent = $(this).closest('.report-part-content');
        var plotType = $(this).val();
        //f($(this), plotType, false);

        var img = partContent.find('img.report-part-graph');
        var imageUrl = partContent.find('.hidden-image-url').text();
        console.log(imageUrl);
        img.attr('src', imageUrl + '&PLOT=' + plotType);

        var exportDocXUrl = partContent.find('.hidden-export-docx-url').text();
        partContent.find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + plotType);

        var exportXlsXUrl = partContent.find('.hidden-export-xls-url').text();
        partContent.find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + plotType);

        var exportPptXUrl = partContent.find('.hidden-export-pptx-url').text();
        partContent.find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + plotType);
    });

    $('.report-parts > .action .plot-types').change(function () {
        var plotType = $(this).val();

        var exportAllDocXUrl = $('.hidden-exportall-docx-url').text();
        $('.exportall-docx-url').attr('href', exportAllDocXUrl + '&PLOT=' + plotType);

        var exportAllXlsUrl = $('.hidden-exportall-xls-url').text();
        $('.exportall-xls-url').attr('href', exportAllXlsUrl + '&PLOT=' + plotType);

        var exportAllPptxUrl = $('.hidden-exportall-pptx-url').text();
        $('.exportall-pptx-url').attr('href', exportAllPptxUrl + '&PLOT=' + plotType);

        $.each($('.report-part-content'), function () {
            var p = $(this).closest('.report-part').find('.action .plot-types');
            p.val(plotType);
            p.change();


        });
    });

});