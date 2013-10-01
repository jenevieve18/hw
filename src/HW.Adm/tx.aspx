<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tx.aspx.cs" Inherits="HW.Adm.tx" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Uploaded files</td></tr>
		</table>
		<table style="margin:20px;" width="900" border="0" cellspacing="5" cellpadding="0">
		    <asp:Label ID="list" EnableViewState=false runat=server />
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
