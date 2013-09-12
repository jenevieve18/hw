<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HW.Grp.Managers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
		<div id="contextbar">
			<div class="actionPane2">
				<div class="bottom" id=ActionNav runat=server>
					<a class="add-user" href="managerSetup.aspx">Add manager</a>
				</div>
			</div>
		</div>
		<div class="smallContent">
			<br />
			<table border="0" cellpadding="0" cellspacing="0">
				<asp:Label ID=labelManagers runat=server/>
				<!--<tr>
					<th>Name</th>
					<th>Roles</th>
				</tr>
				<% foreach (var s in sponsorAdmins) { %>
				<tr>
					<td>
						<% if (s.ReadOnly) { %>
							<img src="img/locked.gif" />
						<% } %>
						<%= HtmlHelper.Anchor(s.ToString(), "managerSetup.aspx?SAID=" + s.Id.ToString()) %>
					</td>
					<td>
						<% int cx = 0; %>
						<% foreach (var f in managerRepository.FindBySponsorAdmin(s.Id)) { %>
							<% if (cx++ > 0) { %>, <% } %>
							<%= f.Function %>
						<% } %>
					</td>
					<td>
						<%
							string url = string.Format(
								@"javascript:if(confirm(""Are you sure you want to delete this manager?"")) {{
									location.href=""managers.aspx?Delete={0}"";
								}}",
								s.Id
							);
						%>
						<%= HtmlHelper.AnchorImage(url, "img/deltoolsmall.gif")%>
					</td>
				</tr>
				<% } %>-->
			</table>
		</div>
	</div>

</asp:Content>
