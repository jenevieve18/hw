<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HW.Grp.Managers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
		<div id="contextbar">
			<div class="actionPane2">
				<div class="bottom" id=ActionNav runat=server>
					<a class="add-user" href="managerSetup.aspx"><%= R.Str(lid, "manager.add", "Add manager")%></a>
				</div>
			</div>
		</div>
		<div class="smallContent">
			<br />
			<table border="0" cellpadding="0" cellspacing="0">
				<%--<asp:Label ID=labelManagers runat=server/>--%>
				<tr>
                <style>
                    .sort 
                    {
                        background-repeat:no-repeat;
                        padding-left:16px;
                    }
                    .sort-asc 
                    {
                        background-image:url(images/bullet_arrow_down.png);
                    }
                    .sort-desc 
                    {
                        background-image:url(images/bullet_arrow_up.png);
                    }
                </style>
					<td><b><a class="sort <%= sort == 0 ? "sort-asc" : "sort-desc" %>" href="managers.aspx?sort=<%= sort == 0 ? 1 : 0 %>"><%= R.Str(lid, "manager.name", "Name")%></a></b></th>
					<td><b><%= R.Str(lid, "manager.access", "Roles")%></b></th>
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
						<% foreach (var f in managerRepository.FindBySponsorAdmin(s.Id, lid)) { %>
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
				<% } %>
			</table>
		</div>
	</div>

</asp:Content>
