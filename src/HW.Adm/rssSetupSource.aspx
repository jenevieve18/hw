<%@ Page Language="C#" AutoEventWireup="true" Inherits="rssSetupSource" Codebehind="rssSetupSource.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">RSS Source setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
		    <tr><td>Source&nbsp;</td><td><asp:TextBox ID=source runat=server Width="300" /></td></tr>
			<tr><td>Source shortname&nbsp;</td><td><asp:TextBox ID=sourceShort runat=server Width="300" /></td></tr>
			<tr><td>Favourite&nbsp;</td><td><asp:CheckBox ID=Favourite runat=server /></td></tr>
			<tr><td><asp:Button ID=Save runat=server Text=Save /></td></tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>