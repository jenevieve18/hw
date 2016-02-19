<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SuperStats.aspx.cs" Inherits="HW.Grp.SuperStats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--<asp:Label ID=StatsImg runat=server />--%>

<% int cx = 0; %>

<% foreach (var p in reportParts) { %>

<div<%= cx > 0 ? " style='page-break-before:always;'" : "" %>>&nbsp;<br>&nbsp;<br></div>
<table border="0" cellspacing="0" cellpadding="0">
	<tbody>
		<tr class="noscreen">
			<td align="center" valign="middle" background="img/top_healthWatch.jpg" height="140" style="font-size:24px;"><%= p.Subject %></td>
		</tr>
		<tr class="noprint">
			<td style="font-size:18px;"><%= p.Subject %></td>
		</tr>
		<tr>
			<td><%= p.Header.Replace("\r", "").Replace("\n", "<br/>")%></td>
		</tr>
		<tr>
			<td>
				<img src="<%= X(p) %>">
			</td>
		</tr>
	</tbody>
</table>

<% cx++; %>

<% } %>

</asp:Content>
