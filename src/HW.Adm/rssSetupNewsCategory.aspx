<%@ Page Language="C#" AutoEventWireup="true" Inherits="rssSetupNewsCategory" Codebehind="rssSetupNewsCategory.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">News category setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
		    <tr><td>Category&nbsp;</td><td><asp:TextBox ID=NewsCategory runat=server Width="300" /></td></tr>
		    <tr><td>Category shortname&nbsp;</td><td><asp:TextBox ID=NewsCategoryShort runat=server Width="300" /></td></tr>
			<tr><td>Only direct from feed&nbsp;</td><td><asp:CheckBox ID=OnlyDirectFromFeed runat=server /></td></tr>
			<tr><td><asp:Button ID=Save runat=server Text=Save /></td></tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>