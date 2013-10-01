<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bq.aspx.cs" Inherits="HW.Adm.bq" %>
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
			<tr><td style="font-size:16px;" align="center">Background Questions</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
			<asp:Label ID=BQ runat=server />
		</table>
		<span style="margin:20px;">[<a href="bqSetup.aspx">Add</a>]</span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
