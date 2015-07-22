<%@ Page Language="C#" AutoEventWireup="true" Inherits="wise" Codebehind="wise.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Words of wisdom</td></tr>
		</table>
		<!--<table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
			<asp:Label ID=Wisdom runat=server />
		</table>-->
        <table style="margin:20px;" border="0" cellspacing="0" cellpadding="3">
            <tr>
                <td width="600"><b>Words of Wisdom</b></td>
                <td><b>Last Shown</b></td>
                <td><b>Lang</b></td>
            </tr>
            <% int j = 0; %>
            <% foreach (var w in words) { %>
            <% string c = j++ % 2 == 0 ? " style='background:#efefef'" : ""; %>
            <tr<%= c %>>
                <td width="600" valign="top"><%= w.WiseName %></td>
                <td>
                    <% if (w.LastShown != null) { %>
                        <%= w.LastShown.Value.ToString("yyyy-MM-dd") %>
                    <% } %>
                </td>
                <td>
                    <% foreach (var l in w.Languages) { %>
                        <img align='right' src='img/langID_<%= l.Language.Id - 1 %>.gif'>
                    <% } %>
                </td>
                <td>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Edit", "wiseedit.aspx?WiseID=" + w.Id) %>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Delete", "wisedelete.aspx?WiseID=" + w.Id, "onclick=\"return confirm('Are you sure you want to delete this words of wisdom?')\"")%>
                </td>
            </tr>
            <% } %>
        </table>
		<span style="margin:20px;">
            [<a href="wiseadd.aspx">Add Words of Wisdom</a>]
        </span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>