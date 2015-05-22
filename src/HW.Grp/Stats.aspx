<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" href="css/smoothness/jquery-ui-1.9.2.custom.min.css"/>
<style type="text/css">

.ui-widget {
    font-family:Arial;
    font-size:13px;
}
.button {
	display: inline-block;
	outline: none;
	cursor: pointer;
	text-align: center;
	text-decoration: none;
	/*font: 14px/100% Arial, Helvetica, sans-serif;*/
	padding: .5em 2em .55em;
	text-shadow: 0 1px 1px rgba(0, 0, 0, .3);
	-webkit-border-radius: .4em;
	-moz-border-radius: .4em;
	border-radius: .4em;
	-webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
	-moz-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
	box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
}
.button:hover {
	text-decoration: none;
}
.button:active {
	position: relative;
	top: 1px;
}
.white {
	color: #606060;
	border: solid 1px #b7b7b7;
}
.white:hover {
	background: -webkit-gradient(linear, left top, left bottom, from(#fff), to(#dcdcdc));
}
.small {
	font-size: 11px;
	padding: .2em 1em .275em;
}
.report-part {
	border:1px solid #e7e7e7;
}
.report-part-subject, .report-part-header, .report-part-content {
	padding:10px;
}
/*.report-part:hover {
	background:#f1f9fd;
}*/
.report-part-subject {
    background:#e7e7e7;
	cursor:pointer;
}
.report-part-header {
	font-size:13px;
}
.report-part-content {
	background:white;
	padding:10px;
	margin-top:5px;
}
/*.report-part-subject {
    height:16px;
    clear:both;
    cursor:pointer;
}
.report-part-subject h3 {
    float:left;
}
.report-part-subject span {
    float:right;
}*/
.chart-description1 {
    border:1px solid #efefef;
    padding:5px;
    background-color:#fcfcfc;
    margin-bottom:5px;
    font-size:9pt;
}
.chart-description {
    background:url(img/myhealth_statistics_bar_detail_toggle.png);
}
.chart-descriptions-info {
    width: 16px;
    height: 16px;
	background:url(images/information.png);
	display:inline-block;
    cursor:pointer;
}
.toggle {
    width: 32px;
    height: 16px;
    background:url(img/myhealth_statistics_bar_detail_toggle.png);
    display:inline-block;
    cursor:pointer;
}
.toggle-right {
    float:right;
}
.toggle-active {
}
.toggle-active-hover {
    background:url(img/myhealth_statistics_bar_detail_toggle.png);
    background-position:0 -16px;
}
</style>

<script type="text/javascript" src="assets/js/jquery-ui-1.9.2.custom.min.js"></script>
<script type="text/javascript">
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
        f($('.report-part'), 1, true);
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
            img.attr('src', imageUrl + '&PLOT=' + plotType);

            var exportDocXUrl = partContent.find('.hidden-export-docx-url').text();
            partContent.find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + plotType);

            var exportXlsXUrl = partContent.find('.hidden-export-xls-url').text();
            partContent.find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + plotType);

            var exportPptXUrl = partContent.find('.hidden-export-pptx-url').text();
            partContent.find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + plotType);
        });
        /*$('.report-part .action .graph').click(function () {
            var partContent = $(this).closest('.report-part-content');
            var plotType = $(this).find('.plot-type').text();
            f($(this), plotType, false);

            var img = partContent.find('img.report-part-graph');
            var imageUrl = partContent.find('.hidden-image-url').text();
            img.attr('src', imageUrl + '&PLOT=' + plotType);

            var exportDocXUrl = partContent.find('.hidden-export-docx-url').text();
            partContent.find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + plotType);

            var exportXlsXUrl = partContent.find('.hidden-export-xls-url').text();
            partContent.find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + plotType);

            var exportPptXUrl = partContent.find('.hidden-export-pptx-url').text();
            partContent.find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + plotType);
        });*/
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
                //f($(this), plotType, false);
                /*var img = $(this).find('img.report-part-graph');
                var imageUrl = $(this).find('.hidden-image-url').text();
                img.attr('src', imageUrl + '&PLOT=' + plotType);

                var exportDocXUrl = $(this).find('.hidden-export-docx-url').text();
                $(this).find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + plotType);

                var exportXlsXUrl = $(this).find('.hidden-export-xls-url').text();
                $(this).find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + plotType);

                var exportPptXUrl = $(this).find('.hidden-export-pptx-url').text();
                $(this).find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + plotType);*/
            });
        });
        /*$('.report-parts > .action .graph').click(function () {
            var plotType = $(this).find('.plot-type').text();

            var exportAllDocXUrl = $('.hidden-exportall-docx-url').text();
            $('.exportall-docx-url').attr('href', exportAllDocXUrl + '&PLOT=' + plotType);

            var exportAllXlsUrl = $('.hidden-exportall-xls-url').text();
            $('.exportall-xls-url').attr('href', exportAllXlsUrl + '&PLOT=' + plotType);

            var exportAllPptxUrl = $('.hidden-exportall-pptx-url').text();
            $('.exportall-pptx-url').attr('href', exportAllPptxUrl + '&PLOT=' + plotType);

            $.each($('.report-part-content'), function () {
                f($(this), plotType, false);
                img = $(this).find('img.report-part-graph');
                imageUrl = $(this).find('.hidden-image-url').text();
                img.attr('src', imageUrl + '&PLOT=' + plotType);

                var exportDocXUrl = $(this).find('.hidden-export-docx-url').text();
                $(this).find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + plotType);

                var exportXlsXUrl = $(this).find('.hidden-export-xls-url').text();
                $(this).find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + plotType);

                var exportPptXUrl = $(this).find('.hidden-export-pptx-url').text();
                $(this).find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + plotType);
            });
        });*/
    });
</script>

<link href="bootstrap-datepicker/css/bootstrap-combined.min.css" rel="stylesheet"/>
<link href="bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet"/>

<script type="text/javascript" src="bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
<script type="text/javascript" src="bootstrap-datepicker/locales/bootstrap-datepicker.sv.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
	            <span class="desc"><%= R.Str(lid, "timeframe", "Timeframe")%></span>

                <!--<div class="input-daterange" id="datepicker">-->
                <span class="input-daterange">
                    <input readonly style="cursor:pointer" type="text" class="input-small date" name="startDate" value="<%= startDate.ToString("yyyy MMM") %>" />
                    <span>--</span>
                    <input readonly style="cursor:pointer" type="text" class="input-small date" name="endDate" value="<%= endDate.ToString("yyyy MMM") %>" />
                </span>
                <!--</div>-->

                <!--<asp:DropDownList ID="FromYear" runat="server" />--<asp:DropDownList ID="ToYear" runat="server" />-->
                <%= R.Str(lid, "survey", "Survey")%>
                <asp:DropDownList AutoPostBack="true" ID="ProjectRoundUnitID" runat="server" />
			    <%= R.Str(lid, "aggregation", "Aggregation")%>
				<asp:DropDownList AutoPostBack="true" ID="GroupBy" runat="server">
					<%--<asp:ListItem Value="1" Text="One week" />
					<asp:ListItem Value="7" Text="Two weeks, start with even" />
					<asp:ListItem Value="2" Text="Two weeks, start with odd" />
					<asp:ListItem Value="3" Text="One month" />
					<asp:ListItem Value="4" Text="Three months" />
					<asp:ListItem Value="5" Text="Six months" />
					<asp:ListItem Value="6" Text="One year" />--%>
				</asp:DropDownList><br />
			    <span class="desc"><%= R.Str(lid, "grouping", "Grouping")%></span>
				<asp:DropDownList AutoPostBack="true" ID="Grouping" runat="server">
					<%--<asp:ListItem Value="0" Text="< none >" />
					<asp:ListItem Value="1" Text="Users on unit" />
					<asp:ListItem Value="2" Text="Users on unit+subunits" />
					<asp:ListItem Value="3" Text="Background variable" />--%>
				</asp:DropDownList>
                <%--<%= R.Str(lid, "language", "Language")%>
                <asp:DropDownList ID="LangID" runat="server" AutoPostBack="true" />--%>
                <br />
                <asp:PlaceHolder ID="Org" runat="server" Visible="false" />
    	        <asp:CheckBoxList RepeatDirection="Vertical" RepeatLayout="table" CellPadding="0" CellSpacing="0" ID="BQ" runat="server" Visible="false" />
                <asp:Button ID="Execute" CssClass="btn" runat="server" Text="Execute" />
			</div>
        </div>
		<% if (reportParts != null) { %>
			<% Q additionalQuery = GetGID(urlModels); %>
            <% bool supportsBoxPlot = SelectedDepartments.Count == 1 || Grouping.SelectedValue == "0"; %>
            <% bool forSingleSeries = SelectedDepartments.Count == 1 || Grouping.SelectedValue == "0"; %>
			<div class="report-parts">
				<div class="action">
                    <div class="chart-descriptions" title="Chart Descriptions">
                        <div>
                        <% foreach (var p in plotTypes) { %>
	       			        <div>&nbsp;<br /></div>
                            <div class="report-part">
                                <div class="report-part-subject">
							        <span><%= p.ShortName %> - <%= p.Name %></span>
                                    <span class="toggle toggle-right toggle-active"></span>
						        </div>
                                <div class="report-part-header"><%= p.Description %></div>
                            </div>
                        <% } %>
                        </div>
                    </div>
					<span class="small"><%= R.Str(lid, "graphs.change.all", "Change all graphs to:")%></span>
                    <select class="plot-types small">
                        <% foreach (var p in plotTypes) { %>
                            <% if (!p.SupportsMultipleSeries && !forSingleSeries) {} %>
                            <% else { %><option value="<%= p.PlotType.Id %>"><%= p.ShortName %></option><% } %>
                        <% } %>
                    </select>
					<!--<span id="modal" class="button white small graph"><span class="hidden plot-type"><%= PlotType.Line%></span><%= PlotType.GetString(PlotType.Line)%></span>
					<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.LineSD %></span><%= PlotType.GetString(PlotType.LineSD)%></span>
					<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.LineSDWithCI %></span><%= PlotType.GetString(PlotType.LineSDWithCI)%></span>
                    <% if (supportsBoxPlot) { %>
						<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.BoxPlotMinMax %></span><%= PlotType.GetString(PlotType.BoxPlotMinMax)%></span>
						<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.BoxPlot %></span><%= PlotType.GetString(PlotType.BoxPlot)%></span>
					<% } %>-->
                    <!--<span class="toggle toggle-chart-description"></span>-->
                    <span class="chart-descriptions-info"></span>
					<span class="small"><%= R.Str(lid, "graphs.export.all", "Export all graphs to:")%></span>
					<span class="button white small export">
                        <% string exportAllDocXUrl = GetExportAllUrl("docx", additionalQuery); %>
                        <span class="hidden hidden-exportall-docx-url"><%= exportAllDocXUrl%></span>
						<%= HtmlHelper.Anchor("docx", exportAllDocXUrl, "class='exportall-docx-url' target='_blank'") %>
                    </span>
					<span class="button white small export">
                        <% string exportAllPptxUrl = GetExportAllUrl("pptx", additionalQuery); %>
                        <span class="hidden hidden-exportall-pptx-url"><%= exportAllPptxUrl%></span>
						<%= HtmlHelper.Anchor("pptx", exportAllPptxUrl, "class='exportall-pptx-url' target='_blank'") %>
                    </span>
                    <% string exportAllXlsUrl = GetExportAllUrl("xls", additionalQuery); %>
					<span class="button white small export">
                        <span class="hidden hidden-exportall-xls-url"><%= exportAllXlsUrl%></span>
						<%= HtmlHelper.Anchor("xls", exportAllXlsUrl, "class='exportall-xls-url' target='_blank'")%>
                    </span>
					<span class="button white small export">
						<%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportAllXlsUrl + "&PLOT=" + PlotType.Verbose, "class='exportall-xls-verbose-url' target='_blank'")%>
                    </span>

				</div>
	        	<% foreach (var r in reportParts) { %>
	       			<div>&nbsp;<br /></div>
					<div class="report-part">
                        <div class="hidden selected-plot-type"><%= PlotType.Line%></div>
						<div class="report-part-subject">
							<span><%= r.Subject %></span>
                            <span class="toggle toggle-right toggle-active"></span>
						</div>
						<div class="report-part-header"><%= r.Header %></div>
						<div class="report-part-content">
                            <% string imageUrl = GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery); %>
							<span class="hidden hidden-image-url"><%= imageUrl %></span>
                            <img class="report-part-graph" src="<%= imageUrl %>" alt="" />
                            <!--<div class="chart-description1" style="display:none"></div>-->
							<div class="action">
								<span class="small"><%= R.Str(lid, "graphs.change", "Change this graph to:")%></span>
                                <select class="plot-types small">
                                    <% foreach (var p in plotTypes) { %>
                                        <% if (!p.SupportsMultipleSeries && !forSingleSeries) {} %>
                                        <% else { %><option value="<%= p.PlotType.Id %>"><%= p.ShortName %></option><% } %>
                                    <% } %>
                                </select>
								<!--<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.Line %></span><%= PlotType.GetString(PlotType.Line)%></span>
								<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.LineSD%></span><%= PlotType.GetString(PlotType.LineSD)%></span>
								<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.LineSDWithCI%></span><%= PlotType.GetString(PlotType.LineSDWithCI)%></span>
                                <% if (supportsBoxPlot) { %>
									<span class="button white small graph"><span class="hidden plot-type"><%= PlotType.BoxPlotMinMax%></span><%= PlotType.GetString(PlotType.BoxPlotMinMax)%></span>
                                    <span class="button white small graph"><span class="hidden plot-type"><%= PlotType.BoxPlot%></span><%= PlotType.GetString(PlotType.BoxPlot)%></span>
								<% } %>-->
								<span class="small"><%= R.Str(lid, "graphs.export", "Export this graph to:")%></span>
								<span class="button white small export">
                                    <% string exportDocXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "docx", additionalQuery); %>
							        <span class="hidden hidden-export-docx-url"><%= exportDocXUrl%></span>
									<%= HtmlHelper.Anchor("docx", exportDocXUrl, "class='export-docx-url' target='_blank'")%>
								</span>
								<span class="button white small export">
                                    <% string exportPptXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "pptx", additionalQuery); %>
							        <span class="hidden hidden-export-pptx-url"><%= exportPptXUrl%></span>
									<%= HtmlHelper.Anchor("pptx", exportPptXUrl, "class='export-pptx-url' target='_blank'")%>
								</span>
                                <% string exportXlsUrl = GetExportUrl(r.ReportPart.Id, r.Id, "xls", additionalQuery); %>
								<span class="button white small export">
							        <span class="hidden hidden-export-xls-url"><%= exportXlsUrl%></span>
									<%= HtmlHelper.Anchor("xls", exportXlsUrl, "class='export-xls-url' target='_blank'")%>
								</span>
								<span class="button white small export">
									<%= HtmlHelper.Anchor(R.Str(lid, "xls.verbose", "xls verbose"), exportXlsUrl + "&PLOT=" + PlotType.Verbose, "class='export-xls-verbose-url' target='_blank'")%>
								</span>
							</div>
						</div>
						<div class="report-part-bottom">&nbsp;</div>
					</div>
				<% } %>
			</div>
		<% } %>
    </div>

    <script type="text/javascript">
        $('.input-daterange').datepicker({
            format: "yyyy M",
            minViewMode: 1,
            language: "<%= GetLang(lid) %>",
            autoclose: true
        });
    </script>

</asp:Content>



