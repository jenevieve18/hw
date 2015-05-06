<%@ Page Language="C#" AutoEventWireup="true" Inherits="issue" Codebehind="issue.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Issues</td></tr>
		</table>
		<table style="margin:20px;" width="900" border="0" cellspacing="5" cellpadding="0">
            <tr><td><b>Title</b></td><td><b>Description</b></td><td><b>Date/time</b></td><td><b>User</b></td></tr>
		    <asp:Label ID="list" EnableViewState=false runat=server />
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>