<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="grpUser.aspx.cs" Inherits="HW.Adm.grpUser" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<div style="padding:20px;">
		<a href="grpUserSetup.aspx?Rnd=<%=(new Random(unchecked((int)DateTime.Now.Ticks))).Next()%>">Add manager</a><br /><br />
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr><td><b>User</b>&nbsp;&nbsp;</td><td><b>Sponsor</b></td></tr>
		    <asp:Label ID=List runat=server />
		</table>
		</div>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
