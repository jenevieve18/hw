<%@ Page Language="C#" AutoEventWireup="true" Inherits="grpUser" Codebehind="grpUser.aspx.cs" %>
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
		    <tr><td><b>User</b>&nbsp;&nbsp;</td><td colspan="2"><b>Sponsor</b>&nbsp;&nbsp;</td><td><b>Logins</b>&nbsp;&nbsp;</td><td><b>Total time</b>&nbsp;&nbsp;</td></tr>
		    <asp:Label ID=List runat=server />
		</table>
		</div>
		<%=Db.bottom()%>
		</form>
  </body>
</html>