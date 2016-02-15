<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plotType.aspx.cs" Inherits="HW.Adm.plotType" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
<head>
    <%=Db.header()%>
</head>
<body>
    <form id="form1" runat="server">
        <%=Db.nav()%>
        <table width="500" border="0" cellspacing="0" cellpadding="0">
            <tr><td style="font-size:16px;" align="center">Plot Types</td></tr>
        </table>
        <table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td><i>Name</i>&nbsp;&nbsp;</td>
                <td><i>Description</i>&nbsp;&nbsp;</td>
                <td></td>
            </tr>
            <% int i = 0; %>
            <% foreach (var t in types) { %>
            <tr <%= i++ % 2 == 0 ? "bgcolor='#F2F2F2'" : "" %>>
                <td><%= HW.Core.Helpers.HtmlHelper.Anchor(t.Name, "plotTypeSetup.aspx?ID=" + t.Id) %></td>
                <td><%= t.Description %></td>
            </tr>
            <% } %>
        </table>
        <span style="margin:20px;">[<a href="plotTypeSetup.aspx">Add plot type</a>]</span>
        <%=Db.bottom()%>
    </form>
</body>
</html>
