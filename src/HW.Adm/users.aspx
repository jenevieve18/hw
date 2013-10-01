<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="HW.Adm.users" %>
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
			<tr><td style="font-size:16px;" align="center">User administration</td></tr>
		</table>
		<table style="margin:20px;" width="1200" border="0" cellspacing="5" cellpadding="0">
		    <tr><td colspan="8">Search by username or email <asp:TextBox ID="search" runat=server /><asp:Button ID=OK runat=server Text=OK /><asp:Button ID=FindDupes runat=server Text="Find dupes" /></td></tr>
		    <asp:Label ID="list" EnableViewState=false runat=server />
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
