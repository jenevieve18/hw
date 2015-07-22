<%@ Page Language="C#" AutoEventWireup="true" Inherits="issue" Codebehind="issue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Issues</td></tr>
		</table>
		<table style="margin:20px;" width="900" border="0" cellspacing="0" cellpadding="3">
            <tr>
                <td><b>Title</b></td>
                <td><b>Description</b></td>
                <td><b>Date/time</b></td>
                <td><b>User</b></td>
                <td><b>Status</b></td>
                <td></td>
            </tr>
            <% int j = 0; %>
            <% foreach (var i in issues) { %>
            <% string c = j++ % 2 == 0 ? " style='background:#efefef'" : ""; %>
            <tr<%= c %>>
                <td valign="top"><%= i.Title %></td>
                <td valign="top"><%= i.Description %></td>
                <td width="80">
                    <% if (i.Date != null) { %>
                        <%= i.Date.Value.ToString("yyyy-MM-dd") %>
                    <% } %>
                </td>
                <td><%= i.User.Name %></td>
                <td width="80"><%= i.GetStatus() %></td>
                <td width="80">
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Edit", "issueedit.aspx?IssueID=" + i.Id) %>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Delete", "issuedelete.aspx?IssueID=" + i.Id, "onclick=\"return confirm('Are you sure you want to delete this issue?')\"")%>
                </td>
            </tr>
            <% } %>
		</table>
		<span style="margin:20px;">
            [<a href="issueadd.aspx">Add Issue</a>]
        </span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>