<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Stats.aspx.cs" Inherits="HWgrp.Web.Stats" %>

<%@ Register Assembly="HW.Core" Namespace="HW.Core.Helpers" TagPrefix="cc1" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
	.ui-accordion-header {
		font-size:10pt;
		font-family: Arial;
	}
	.ui-accordion-content {
		font-size:10pt;
		font-family: Arial;
	}
	.ui-accordion .ui-accordion-content {
			padding: 1em;
	}
</style>
<script type="text/javascript">
	$(document).ready(function () {
		$('.plot .btn').click(function () {
			var partImg = $(this).closest('.report-part-content');
			var img = partImg.find('img');
			var newSrc = partImg.find('.hidden').text();
			img.attr('src', newSrc + '&PLOT=' + $(this).text());
		});
		$('.report-parts .btn').click(function() {
			var partImgs = $('.report-part-content');
			for (i = 0; i < partImgs.length; i++) {
				var partImg = partImgs[i];
				console.log(partImg);
				var img = partImg.find('img');
				var newSrc = partImg.find('.hidden').text();
				img.attr('src', newSrc + '&PLOT=' + $(this).text());
			}
		});
	});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
	<div id="contextbar">
		<div class="settingsPane">
			<span class="desc">Timeframe</span>
			<asp:DropDownList ID="dropDownYearFrom" runat="server" CssClass="input-small" />
			--
			<asp:DropDownList ID="dropDownYearTo" runat="server" CssClass="input-small" />
			Survey
			<asp:DropDownList AutoPostBack=true ID=dropDownListProjectRoundUnits 
				runat=server CssClass="input-medium" />
			Aggregation
			<asp:DropDownList AutoPostBack="true" ID="dropDownGroupBy" runat="server">
				<asp:ListItem Value="1" Text="One week" />
				<asp:ListItem Value="7" Text="Two weeks, start with even" />
				<asp:ListItem Value="2" Text="Two weeks, start with odd" />
				<asp:ListItem Value="3" Text="One month" />
				<asp:ListItem Value="4" Text="Three months" />
				<asp:ListItem Value="5" Text="Six months" />
				<asp:ListItem Value="6" Text="One year" />
			</asp:DropDownList>
			<br />
			<span class="desc">Grouping</span>
			<asp:DropDownList AutoPostBack="true" ID="dropDownGrouping" runat="server" 
				CssClass="input-medium">
				<asp:ListItem Value="0" Text="< none >" />
				<asp:ListItem Value="1" Text="Users on unit" />
				<asp:ListItem Value="2" Text="Users on unit+subunits" />
				<asp:ListItem Value="3" Text="Background variable" />
			</asp:DropDownList>
			Language
			<asp:DropDownList ID="dropDownLanguages" runat="server" AutoPostBack="true" 
				CssClass="input-small" />
			<!--<input id="STDEV" type="checkbox" name="STDEV" /><label for="STDEV">Show standard deviation</label>-->
			Distribution
			<asp:DropDownList AutoPostBack="true" ID="dropDownDistribution" runat="server" 
				CssClass="input-medium">
				<asp:ListItem Value="0" Text="< none >" />
				<asp:ListItem Value="1" Text="Standard Deviation" />
				<asp:ListItem Value="2" Text="Confidence Interval" />
			</asp:DropDownList>
			<br />
			<cc1:DepartmentListHtmlTable runat="server" ID="tableDepartments"></cc1:DepartmentListHtmlTable>
			<asp:PlaceHolder ID="Org" runat="server" Visible="false" />
			<asp:CheckBoxList ID="checkBoxDepartments" runat="server">
			</asp:CheckBoxList>
			<asp:CheckBoxList RepeatDirection="Vertical" RepeatLayout="table" CellPadding="0"
				CellSpacing="0" ID="checkBoxQuestions" runat="server" Visible="false" />
			<br />
			<asp:Button ID="Execute" CssClass="btn" runat="server" Text="Execute" />
		</div>
	</div>
	<% if (reportParts != null) { %>
		<% Q additionalQuery = GetGID(urlModels); %>
		<br>
		<div class="btn-toolbar">
			<div class="btn-group">
				<%= HtmlHelper.Anchor("PDF", GetExportAllUrl("pdf", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } }, "_blank")%>
				<%= HtmlHelper.Anchor("CSV", GetExportAllUrl("csv", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
				<%= HtmlHelper.Anchor("DOC", GetExportAllUrl("docx", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
				<%= HtmlHelper.Anchor("PPT", GetExportAllUrl("pptx", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
			</div>
			<% if (SelectedDepartments.Count == 1) { %>
			<div class="btn-group report-parts">
				<span class="btn btn-mini">LINE</span>
				<span class="btn btn-mini">Boxplot</span>
			</div>
			<% } %>
		</div>
		<br>
		<div id="notaccordion">
		<% foreach (var r in reportParts) { %>
			<h3><%= r.Subject %></h3>
			<div class="report-part-content">
				<span class="hidden"><%= GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery) %></span>
				<div>
					<%= HtmlHelper.Image(GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery)) %>
				</div>
				<div class="btn-toolbar">
					<div class="btn-group">
						<%= HtmlHelper.Anchor("PDF", GetExportUrl(r.ReportPart.Id, r.Id, "pdf", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } }, "_blank")%>
						<%= HtmlHelper.Anchor("CSV", GetExportUrl(r.ReportPart.Id, r.Id, "csv", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
						<%= HtmlHelper.Anchor("DOC", GetExportUrl(r.ReportPart.Id, r.Id, "docx", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
						<%= HtmlHelper.Anchor("PPT", GetExportUrl(r.ReportPart.Id, r.Id, "pptx", additionalQuery), new Dictionary<string, string> { { "class", "btn btn-mini" } })%>
					</div>
					<% if (SelectedDepartments.Count == 1) { %>
					<div class="btn-group plot">
						<span class="btn btn-mini">Line (mean ± SD)</span>
						<span class="btn btn-mini">Line (mean ± 2 SD)</span>
						<span class="btn btn-mini">Boxplot</span>
					</div>
					<% } %>
				</div>
			</div>
		<% } %>
		</div>
		<script>
			$("#accordion").accordion({
				heightStyle:"content"
			});
			$("#notaccordion").addClass("ui-accordion ui-widget ui-helper-reset")
				.find("h3")
				.addClass("ui-accordion-header ui-helper-reset ui-state-default ui-corner-all ui-accordion-icons ui-state-hover")
				.hover(function() { 
					$(this).toggleClass("ui-state-hover"); 
				})
				.prepend('<span class="ui-accordion-header-icon ui-icon ui-icon-triangle-1-e"></span>')
				.click(function() {
					$(this)
						.toggleClass("ui-accordion-header-active ui-state-active ui-state-default ui-corner-bottom")
						.find("> .ui-icon").toggleClass("ui-icon-triangle-1-e ui-icon-triangle-1-s").end()
						.next().toggleClass("ui-accordion-content-active").slideToggle();
					return false;
				})
				.next()
				.addClass("ui-accordion-content  ui-helper-reset ui-widget-content ui-corner-bottom")
				.hide();
		</script>
	<% } %>
</div>

</asp:Content>
