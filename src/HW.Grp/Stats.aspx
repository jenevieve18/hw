﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $('.report-part .action .graph').click(function () {
            var partContent = $(this).closest('.report-part-content');

            var img = partContent.find('img');
            var imageUrl = partContent.find('.hidden-image-url').text();
            img.attr('src', imageUrl + '&PLOT=' + $(this).text());

            var exportDocXUrl = partContent.find('.hidden-export-docx-url').text();
            partContent.find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + $(this).text());

            var exportXlsXUrl = partContent.find('.hidden-export-xls-url').text();
            partContent.find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + $(this).text());

            var exportPptXUrl = partContent.find('.hidden-export-pptx-url').text();
            partContent.find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + $(this).text());
        });
        $('.report-parts > .action .graph').click(function () {
            text = $(this).text();

            var exportAllDocXUrl = $('.hidden-exportall-docx-url').text();
            $('.exportall-docx-url').attr('href', exportAllDocXUrl + '&PLOT=' + text);

            var exportAllXlsUrl = $('.hidden-exportall-xls-url').text();
            $('.exportall-xls-url').attr('href', exportAllXlsUrl + '&PLOT=' + text);

            $.each($('.report-part-content'), function () {
                img = $(this).find('img');
                imageUrl = $(this).find('.hidden-image-url').text();
                img.attr('src', imageUrl + '&PLOT=' + text);

                var exportDocXUrl = $(this).find('.hidden-export-docx-url').text();
                $(this).find('.export-docx-url').attr('href', exportDocXUrl + '&PLOT=' + text);

                var exportXlsXUrl = $(this).find('.hidden-export-xls-url').text();
                $(this).find('.export-xls-url').attr('href', exportXlsXUrl + '&PLOT=' + text);

                var exportPptXUrl = $(this).find('.hidden-export-pptx-url').text();
                $(this).find('.export-pptx-url').attr('href', exportPptXUrl + '&PLOT=' + text);
            });
        });
        /*$('.action .button').mouseover(function () {
        $(this).parent().next().text($(this).text());
        });
        $('.action .button').mouseout(function () {
        $(this).parent().next().text('');
        });*/
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
					<span class="button white small graph">Line</span>
					<span class="button white small graph">Line (mean ± SD)</span>
					<span class="button white small graph">Line (mean ± 1.96 SD)</span>
                    <% if (supportsBoxPlot) { %>
						<span class="button white small graph">Boxplot</span>
					<% } %>
					<span class="small">Export all graphs to:</span>
					<span class="button white small export">
                        <% string exportAllDocXUrl = GetExportAllUrl("docx", additionalQuery); %>
                        <span class="hidden hidden-exportall-docx-url"><%= exportAllDocXUrl%></span>
						<%= HtmlHelper.Anchor("docx", exportAllDocXUrl, new Dictionary<string, string>() { { "class", "exportall-docx-url" } }, "_blank")%>
                    </span>
                    <% string exportAllXlsUrl = GetExportAllUrl("xls", additionalQuery); %>
					<span class="button white small export">
                        <span class="hidden hidden-exportall-xls-url"><%= exportAllXlsUrl%></span>
						<%= HtmlHelper.Anchor("xls", exportAllXlsUrl, new Dictionary<string, string>() { { "class", "exportall-xls-url" } }, "_blank")%>
                    </span>
					<span class="button white small export">
						<%= HtmlHelper.Anchor("xls verbose", exportAllXlsUrl + "&Plot=Verbose", new Dictionary<string, string>() { { "class", "exportall-xls-verbose-url" } }, "_blank")%>
                    </span>
				</div>
				<div class="small action-desc"></div>
	        	<% foreach (var r in reportParts) { %>
	       			<div>&nbsp;<br />&nbsp;<br /></div>
					<div class="report-part">
						<div class="report-part-subject">
							<h3><%= r.Subject %></h3>
						</div>
						<!--<div class="report-part-header"><%= r.Header %></div>-->
						<div class="report-part-content">
                            <% string imageUrl = GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery); %>
							<span class="hidden hidden-image-url"><%= imageUrl %></span>
							<%= HtmlHelper.Image(imageUrl) %>
							<div class="action">
								<span class="small">Change graph to:</span>
								<span class="button white small graph">Line</span>
								<span class="button white small graph">Line (mean ± SD)</span>
								<span class="button white small graph">Line (mean ± 1.96 SD)</span>
                                <% if (supportsBoxPlot) { %>
									<span class="button white small graph">Boxplot</span>
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
									<%= HtmlHelper.Anchor("xls verbose", exportXlsUrl + "&Plot=Verbose", new Dictionary<string, string>() { { "class", "export-xls-verbose-url" } }, "_blank")%>
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
