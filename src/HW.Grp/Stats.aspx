<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link rel="stylesheet" href="css/smoothness/jquery-ui-1.9.2.custom.min.css">
<style type="text/css">
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
	.report-part { border:1px solid #e7e7e7; }
	.report-part-subject { background:#efefef; padding:5px 0 5px 20px; }
	.report-part-content { padding:20px; }
	.ui-dialog {
		font-family:Arial;
		font-size:11pt;
	}
	.chart-description1 {
    border:1px solid #efefef;
    padding:5px;
    background-color:#fcfcfc;
    margin-bottom:5px;
    font-size:9pt;
}
</style>

<script type="text/javascript" src="js/jquery-ui-1.9.2.custom.min.js"></script>
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
            return { title: 'Verbose', description: 'Verbose' };
        } else if (plotType == 6) {
            return { title: 'Box Plot', description: 'median value chart, similar to the min/max BloxPlot but removes outlying extreme values' };
        }
    }
    function f(obj, plotType, all) {
        obj.closest('.report-part').find('.selected-plot-type').text(plotType);
        plot = getPlot(plotType);
        if (all) {
            $('.chart-description1').text("{0} - {1}".format(plot.title, plot.description));
        } else {
            obj.closest('.report-part').find('.chart-description1').text("{0} - {1}".format(plot.title, plot.description));
        }
    }
    $(document).ready(function () {
        f($('.report-part'), 1, true);
        //$(".chart-description").dialog({ autoOpen: false });
        $(".report-part .report-part-content img").click(function () {
            //d = $('.chart-description');
            //if (d.dialog('isOpen') == true) {
            //d.dialog('close');
            //}
            //plotType = $(this).closest('.report-part').find('.selected-plot-type').text();
            //plot = getPlot(plotType);
            //$('.ui-dialog-title').text(plot.title);
            //$('.chart-description p').text(plot.description);
            //d.dialog('open');
            $(this).closest('.report-part').find('.chart-description1').slideToggle();
        });

        $('.report-part .action .graph').click(function () {
            var partContent = $(this).closest('.report-part-content');
            var plotType = $(this).find('.plot-type').text();
            //$(this).closest('.report-part').find('.selected-plot-type').text(plotType);
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
        });
        $('.report-parts > .action .graph').click(function () {
            var plotType = $(this).find('.plot-type').text();

            var exportAllDocXUrl = $('.hidden-exportall-docx-url').text();
            $('.exportall-docx-url').attr('href', exportAllDocXUrl + '&PLOT=' + plotType);

            var exportAllXlsUrl = $('.hidden-exportall-xls-url').text();
            $('.exportall-xls-url').attr('href', exportAllXlsUrl + '&PLOT=' + plotType);

            var exportAllPptxUrl = $('.hidden-exportall-pptx-url').text();
            $('.exportall-pptx-url').attr('href', exportAllPptxUrl + '&PLOT=' + plotType);

            $.each($('.report-part-content'), function () {
                //$(this).closest('.report-part').find('.selected-plot-type').text(plotType);
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
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
	            <span class="desc">Timeframe</span>
                <asp:DropDownList ID="FromYear" runat="server" />--<asp:DropDownList ID="ToYear" runat="server" />
                Survey
                <asp:DropDownList AutoPostBack="true" ID="ProjectRoundUnitID" runat="server" />
			    Aggregation
				<asp:DropDownList AutoPostBack="true" ID="GroupBy" runat="server">
					<asp:ListItem Value="1" Text="One week" />
					<asp:ListItem Value="7" Text="Two weeks, start with even" />
					<asp:ListItem Value="2" Text="Two weeks, start with odd" />
					<asp:ListItem Value="3" Text="One month" />
					<asp:ListItem Value="4" Text="Three months" />
					<asp:ListItem Value="5" Text="Six months" />
					<asp:ListItem Value="6" Text="One year" />
				</asp:DropDownList><br />
			    <span class="desc">Grouping</span>
				<asp:DropDownList AutoPostBack="true" ID="Grouping" runat="server">
					<asp:ListItem Value="0" Text="< none >" />
					<asp:ListItem Value="1" Text="Users on unit" />
					<asp:ListItem Value="2" Text="Users on unit+subunits" />
					<asp:ListItem Value="3" Text="Background variable" />
				</asp:DropDownList>
                Language
                <asp:DropDownList ID="LangID" runat="server" AutoPostBack="true" />
                <br />
                <asp:PlaceHolder ID="Org" runat="server" Visible="false" />
    	        <asp:CheckBoxList RepeatDirection="Vertical" RepeatLayout="table" CellPadding="0" CellSpacing="0" ID="BQ" runat="server" Visible="false" />
                <asp:Button ID="Execute" CssClass="btn" runat="server" Text="Execute" />
			</div>
        </div>
		<% if (reportParts != null) { %>
			<% Q additionalQuery = GetGID(urlModels); %>
            <% bool supportsBoxPlot = SelectedDepartments.Count == 1 || Grouping.SelectedValue == "0"; %>
			<div class="report-parts">
				<div class="action">
					<span class="small">Change all graphs to:</span>
					<span class="button white small graph"><span class="hidden plot-type"><%= Plot.Line %></span><%= Plot.GetString(Plot.Line) %></span>
					<span class="button white small graph"><span class="hidden plot-type"><%= Plot.LineSD %></span><%= Plot.GetString(Plot.LineSD) %></span>
					<span class="button white small graph"><span class="hidden plot-type"><%= Plot.LineSDWithCI %></span><%= Plot.GetString(Plot.LineSDWithCI) %></span>
                    <% if (supportsBoxPlot) { %>
						<span class="button white small graph"><span class="hidden plot-type"><%= Plot.BoxPlotMinMax %></span><%= Plot.GetString(Plot.BoxPlotMinMax) %></span>
						<span class="button white small graph"><span class="hidden plot-type"><%= Plot.BoxPlot %></span><%= Plot.GetString(Plot.BoxPlot) %></span>
					<% } %>
					<span class="small">Export all graphs to:</span>
					<span class="button white small export">
                        <% string exportAllDocXUrl = GetExportAllUrl("docx", additionalQuery); %>
                        <span class="hidden hidden-exportall-docx-url"><%= exportAllDocXUrl%></span>
						<%= HtmlHelper.Anchor("docx", exportAllDocXUrl, new Dictionary<string, string>() { { "class", "exportall-docx-url" } }, "_blank")%>
                    </span>
					<span class="button white small export">
                        <% string exportAllPptxUrl = GetExportAllUrl("pptx", additionalQuery); %>
                        <span class="hidden hidden-exportall-pptx-url"><%= exportAllPptxUrl%></span>
						<%= HtmlHelper.Anchor("pptx", exportAllPptxUrl, new Dictionary<string, string>() { { "class", "exportall-pptx-url" } }, "_blank")%>
                    </span>
                    <% string exportAllXlsUrl = GetExportAllUrl("xls", additionalQuery); %>
					<span class="button white small export">
                        <span class="hidden hidden-exportall-xls-url"><%= exportAllXlsUrl%></span>
						<%= HtmlHelper.Anchor("xls", exportAllXlsUrl, new Dictionary<string, string>() { { "class", "exportall-xls-url" } }, "_blank")%>
                    </span>
					<span class="button white small export">
						<%= HtmlHelper.Anchor("xls verbose", exportAllXlsUrl + "&PLOT=" + Plot.Verbose, new Dictionary<string, string>() { { "class", "exportall-xls-verbose-url" } }, "_blank")%>
                    </span>
				</div>
				<div class="small action-desc"></div>
				<div class="chart-description" title="">
					<p></p>
				</div>
	        	<% foreach (var r in reportParts) { %>
	       			<div>&nbsp;<br />&nbsp;<br /></div>
					<div class="report-part">
                        <div class="hidden selected-plot-type"><%= Plot.Line %></div>
						<div class="report-part-subject">
							<h3><%= r.Subject %></h3>
						</div>
						<%--<div class="report-part-header"><%= r.Header %></div>--%>
						<div class="report-part-content">
                            <% string imageUrl = GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery); %>
							<span class="hidden hidden-image-url"><%= imageUrl %></span>
                            <img class="report-part-graph" src="<%= imageUrl %>" />
							<!--<%= HtmlHelper.Image(imageUrl) %>-->
                            <div class="chart-description1" style="display:none"></div>
							<div class="action">
								<img class="info" src="img/information.png"/>
								<span class="small">Change graph to:</span>
								<span class="button white small graph"><span class="hidden plot-type"><%= Plot.Line %></span><%= Plot.GetString(Plot.Line) %></span>
								<span class="button white small graph"><span class="hidden plot-type"><%= Plot.LineSD %></span><%= Plot.GetString(Plot.LineSD) %></span>
								<span class="button white small graph"><span class="hidden plot-type"><%= Plot.LineSDWithCI %></span><%= Plot.GetString(Plot.LineSDWithCI) %></span>
                                <% if (supportsBoxPlot) { %>
									<span class="button white small graph"><span class="hidden plot-type"><%= Plot.BoxPlotMinMax %></span><%= Plot.GetString(Plot.BoxPlotMinMax) %></span>
                                    <span class="button white small graph"><span class="hidden plot-type"><%= Plot.BoxPlot %></span><%= Plot.GetString(Plot.BoxPlot) %></span>
								<% } %>
								<span class="small">Export graph to:</span>
								<span class="button white small export">
                                    <% string exportDocXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "docx", additionalQuery); %>
							        <span class="hidden hidden-export-docx-url"><%= exportDocXUrl%></span>
									<%= HtmlHelper.Anchor("docx", exportDocXUrl, new Dictionary<string, string>() { { "class", "export-docx-url" } }, "_blank")%>
								</span>
								<span class="button white small export">
                                    <% string exportPptXUrl = GetExportUrl(r.ReportPart.Id, r.Id, "pptx", additionalQuery); %>
							        <span class="hidden hidden-export-pptx-url"><%= exportPptXUrl%></span>
									<%= HtmlHelper.Anchor("pptx", exportPptXUrl, new Dictionary<string, string>() { { "class", "export-pptx-url" } }, "_blank")%>
								</span>
                                <% string exportXlsUrl = GetExportUrl(r.ReportPart.Id, r.Id, "xls", additionalQuery); %>
								<span class="button white small export">
							        <span class="hidden hidden-export-xls-url"><%= exportXlsUrl%></span>
									<%= HtmlHelper.Anchor("xls", exportXlsUrl, new Dictionary<string, string>() { { "class", "export-xls-url" } }, "_blank")%>
								</span>
								<span class="button white small export">
									<%= HtmlHelper.Anchor("xls verbose", exportXlsUrl + "&PLOT=" + Plot.Verbose, new Dictionary<string, string>() { { "class", "export-xls-verbose-url" } }, "_blank")%>
								</span>
							</div>
							<div class="small action-desc"></div>
						</div>
						<div class="report-part-bottom">&nbsp;</div>
					</div>
				<% } %>
			</div>
		<% } %>
    </div>

</asp:Content>