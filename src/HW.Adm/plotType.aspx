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
        </table>
        <span style="margin:20px;">[<a href="plotTypeSetup.aspx">Add exercise</a>]</span>
        <%=Db.bottom()%>
    </form>
</body>
</html>
