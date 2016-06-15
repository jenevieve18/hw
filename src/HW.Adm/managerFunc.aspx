<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managerFunc.aspx.cs" Inherits="HW.Adm.managerFunc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <%=Db.header()%>
</head>
<body>
    <form id="form1" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Manager Functions</td></tr>
		</table>
        <table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
		    <tr>
                <td><i>URL</i>&nbsp;&nbsp;</td>
            </tr>
            <% foreach (var f in repo.FindAll()) { %>
            <tr>
                <td><%= f.URL %></td>
                <td>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Edit", "managerFuncEdit.aspx?ManagerFunctionID=" + f.Id) %>
                    <%= HW.Core.Helpers.HtmlHelper.Anchor("Delete", "managerFuncDelete.aspx?ManagerFunctionID=" + f.Id) %>
                </td>
            </tr>
            <% } %>
		</table>
		<span style="margin:20px;">[<a href="managerFuncAdd.aspx">Add manager function</a>]</span>
		<%=Db.bottom()%>
    </form>
</body>
</html>
