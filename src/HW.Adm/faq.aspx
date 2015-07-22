<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="HW.Adm.faq" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
    <form id="form1" runat="server">
		<%=Db.nav()%>
        <table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">FAQ</td></tr>
		</table>
        <table style="margin:20px;" border="0" cellspacing="0" cellpadding="3">
            <tr>
                <td width="300"><b>FAQ</b></td>
                <td><b>Lang</b></td>
                <td></td>
            </tr>
            <% int j = 0; %>
            <% foreach (var f in faqs) { %>
            <% string c = j++ % 2 == 0 ? " style='background:#efefef'" : ""; %>
            <tr<%= c %>>
                <td><%= f.Name %></td>
                <td>
                    <% foreach (var l in f.Languages) { %>
                        <img src='img/langID_<%= l.Language.Id %>.gif'>
                    <% } %>
                </td>
                <td>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Edit", "faqedit.aspx?FAQID=" + f.Id) %>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Delete", string.Format("faqdelete.aspx?FAQID={0}", f.Id), "onclick=\"return confirm('Are you sure you want to delete this frequently asked question?')\"")%>
                </td>
            </tr>
            <% } %>
        </table>
		<span style="margin:20px;">
            [<a href="faqadd.aspx">Add FAQ</a>]
        </span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>