<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SuperStats.aspx.cs" Inherits="HW.Grp.SuperStats" %>
<%@ Import Namespace="HW.Grp" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $('.report-part .plot-types').change(function () {
            var plotType = $(this).val();

            var reportPart = $(this).closest('.report-part');
            var imageUrl = reportPart.find('.image-url').text();

            var img = reportPart.find('.image');
            img.attr('src', imageUrl + '&PLOT=' + plotType);
        });
        $('.all-plot-types').change(function () {
            var plotType = $(this).val();
            console.log(plotType);
            $.each($('.report-part'), function () {
                var p = $(this).find('.plot-types');
                console.log(plotType);
                p.val(plotType);
                p.change();
            });
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--<asp:Label ID=StatsImg runat=server />--%>

Change all graphs to:
<select class="all-plot-types">
    <% foreach (var p in plotTypes) { %>
        <% if (!p.SupportsMultipleSeries && !forSingleSeries) {} %>
        <% else { %><option value="<%= p.PlotType.Id %>"><%= p.ShortName %></option><% } %>
    <% } %>
</select>
Export all graphs to:
<input type="button" value="docx" />
<input type="button" value="pptx" />
<input type="button" value="xls" />
<input type="button" value="xlsverbose" />

<% int cx = 0; %>

<% foreach (var r in reportParts) { %>

<div <%= cx > 0 ? " style='page-break-before:always;'" : "" %>>&nbsp;<br>&nbsp;<br></div>
<table border="0" cellspacing="0" cellpadding="0">
	<tbody class="report-part">
		<tr class="noscreen">
			<td align="center" valign="middle" background="img/top_healthWatch.jpg" height="140" style="font-size:24px;"><%= r.Subject %></td>
		</tr>
		<tr class="noprint">
			<td style="font-size:18px;"><%= r.Subject %></td>
		</tr>
		<tr>
			<td><%= r.Header.Replace("\r", "").Replace("\n", "<br/>")%></td>
		</tr>
		<tr>
			<td>
                <% string url = GetReportImageUrl(r); %>
				<img class="image" src="<%= url %>" />
                <span class="hidden image-url"><%= url %></span>
			</td>
		</tr>
        <tr>
            <td>
                Change this graph to:
                <select class="plot-types">
                    <% foreach (var p in plotTypes) { %>
                        <option value="<%= p.PlotType.Id %>"><%= p.ShortName %></option>
                    <% } %>
                </select>
                Export this graph to:
                <input type="button" value="docx" />
                <input type="button" value="pptx" />
                <input type="button" value="xls" />
                <input type="button" value="xlsverbose" />
            </td>
        </tr>
	</tbody>
</table>

<% cx++; %>

<% } %>

</asp:Content>
