<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grpManual.aspx.cs" Inherits="HW.Adm.grpManual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <%=Db.header()%>
</head>
<body>
    <form id="form1" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">GRP Manual</td></tr>
		</table>
		<span style="margin:20px;">[<a href="grpManualSetup.aspx">Add GRP Manual</a>]</span>
		<%=Db.bottom()%>
    </form>
</body>
</html>
