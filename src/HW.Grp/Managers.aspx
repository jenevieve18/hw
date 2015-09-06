<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HW.Grp.Managers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
    .sort {
        background-repeat:no-repeat;
        padding-left:16px;
    }
    .sort-asc {
        background-image:url(images/bullet_arrow_down.png);
    }
    .sort-desc {
        background-image:url(images/bullet_arrow_up.png);
    }
</style>

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
			<table border="0" cellpadding="3" cellspacing="0">
				<%--<asp:Label ID=labelManagers runat=server/>--%>
				<tr>
                    <th>
                        <a class="sort <%= sortFirstName == 0 ? "sort-asc" : "sort-desc" %>" href="managers.aspx?Sort=FirstName&SortFirstName=<%= sortFirstName == 0 ? 1 : 0 %>&SortLastName=<%= sortLastName %>"><%= R.Str(lid, "manager.name", "Name")%></a>
                    </th>
                    <th>
                        <a class="sort <%= sortLastName == 0 ? "sort-asc" : "sort-desc" %>" href="managers.aspx?Sort=LastName&SortFirstName=<%= sortFirstName %>&SortLastName=<%= sortLastName == 0 ? 1 : 0 %>"><%= R.Str(lid, "manager.name.last", "Last Name")%></a>
                    </th>
                    <th><%= R.Str(lid, "manager.access", "Roles")%></th>
                    <th><%= R.Str(lid, "manager.login.last", "Last Login")%></th>
                    <th></th>
				</tr>
				<% foreach (var s in sponsorAdmins) { %>
				<tr>
					<td>
						<% if (s.ReadOnly) { %>
							<img src="img/locked.gif" />
						<% } %>
						<%= HtmlHelper.Anchor(s.ToString(), "managerSetup.aspx?SAID=" + s.Id.ToString()) %>
					</td>
                    <td><%= s.LastName %></td>
					<td>
						<% int cx = 0; %>
						<% foreach (var f in s.Functions) { %>
							<% if (cx++ > 0) { %>, <% } %>
							<%= f.Function.SelectedLanguage.Function %>
						<% } %>
					</td>
                    <td style="text-align:center"><%= s.GetLoginDays(R.Str(lid, "manager.login.notactivated", "Not activated"), R.Str(lid, "manager.login.notonrecord", "Not on record"), R.Str(lid, "manager.login.days", "days(s)")) %></td>
					<td>
						<%
							string url = string.Format(
								@"javascript:if(confirm(""{1}"")) {{
									location.href=""managers.aspx?Delete={0}"";
								}}",
								s.Id,
								R.Str(lid, "manager.delete", "Are you sure you want to delete this manager?")
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
