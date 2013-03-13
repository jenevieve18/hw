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
      padding: 1em 0 15em 0;
}
</style>

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
			<cc1:DepartmentHtmlTable runat="server" ID="tableDepartment"></cc1:DepartmentHtmlTable>
			<asp:PlaceHolder ID="Org" runat="server" Visible="false" />
			<asp:CheckBoxList ID="checkBoxDepartments" runat="server">
			</asp:CheckBoxList>
			<asp:CheckBoxList RepeatDirection="Vertical" RepeatLayout="table" CellPadding="0"
				CellSpacing="0" ID="checkBoxQuestions" runat="server" Visible="false" />
			<br />
			<asp:Button ID="Execute" CssClass="btn" runat="server" Text="Execute" />
		</div>
	</div>
	<!--<asp:Label ID="StatsImg" runat="server" />-->
	<% if (reportParts != null) { %>
		<div id="accordion">
		<% foreach (var r in reportParts) { %>
			<h1><%= r.Header %></h1>
			<div>
				<%= HtmlHelper.Image(GetReportImageUrl(r.ReportPart.Id, r.Id, "reportImage", GetURL(urlModels))) %>
			</div>
		<% } %>
		</div>
		<script>
			$("#accordion").accordion({
				heightStyle: "content"
			});
		</script>
	<% } %>
</div>

</asp:Content>
