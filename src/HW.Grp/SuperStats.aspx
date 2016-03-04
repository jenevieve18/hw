﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SuperStats.aspx.cs" Inherits="HW.Grp.SuperStats" %>
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
        $('.report-part-subject').click(function () {
            $('.report-part-header').toggle("slow");
        });
        $('.report-part-subject .icon').mouseover(function () {
        	console.log('mouseover');
            $(this).removeClass('icon-active').addClass('icon-hover');
        });
        $('.report-part-subject .icon').mouseleave(function () {
        	console.log('mouseleave');
            $(this).removeClass('icon-hover').addClass('icon-active');
        });
    });
</script>

<style type="text/css">
    .report-part {
        border:1px solid #e7e7e7;
    }
    .report-part td {
    	padding:10px;
    }
    .report-part-subject {
    	background:#e7e7e7;
    	cursor:pointer;
    }
    .report-part-header {
        display:none;
    }
    .report-part-subject .icon {
	    width: 32px;
	    height: 16px;
	    background:url(img/myhealth_statistics_bar_detail_toggle.png);
	    display:inline-block;
	    cursor:pointer;
	}
	.report-part-subject .icon-active {
	}
	.report-part-subject .icon-hover {
	    background:url(img/myhealth_statistics_bar_detail_toggle.png);
	    background-position:0 -16px;
	}
</style>

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
<table border="0" cellspacing="0" cellpadding="0" class="report-part">
	<tbody>
		<tr class="noscreen">
			<td align="center" valign="middle" background="img/top_healthWatch.jpg" height="140" style="font-size:24px;"><%= r.Subject %></td>
		</tr>
		<tr class="noprint report-part-subject">
			<td style="font-size:18px;"><%= r.Subject %></td>
			<td style="text-align:right">
                <span class="icon icon-active"></span>
			</td>
		</tr>
		<tr class="report-part-header">
			<td colspan="2">
                <%= r.Header.Replace("\r", "").Replace("\n", "<br/>")%>
            </td>
		</tr>
		<tr>
			<td colspan="2">
                <% string url = GetReportImageUrl(r); %>
				<img class="image" src="<%= url %>" />
                <span class="hidden image-url"><%= url %></span>
			</td>
		</tr>
        <tr>
            <td colspan="2">
                Change this graph to:
                <select class="plot-types">
                    <% foreach (var p in plotTypes) { %>
                        <option value="<%= p.PlotType.Id %>"><%= p.ShortName %></option>
                    <% } %>
                </select>
                Export this graph to:
                <input type="button" value="docx" onclick="" />
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
