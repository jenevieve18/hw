<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rssSetupChannel.aspx.cs" Inherits="HW.Adm.rssSetupChannel" %>
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
			<tr><td style="font-size:16px;" align="center">RSS Feed setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
		    <tr><td>Source&nbsp;</td><td><asp:DropDownList ID=sourceID runat=server Width="400" /></td></tr>
		    <tr><td>Short name&nbsp;</td><td><asp:TextBox ID=Internal runat=server Width="400" /></td></tr>
			<tr><td>URL&nbsp;</td><td><asp:TextBox ID=feed runat=server Width="400" /></td></tr>
			<tr><td>Language&nbsp;</td><td><asp:DropDownList ID=langID runat=server Width="400"><asp:ListItem Value=0 Text=Swedish /><asp:ListItem Value=1 Text=English /></asp:DropDownList></td></tr>
			<tr><td>Pause until&nbsp;</td><td><asp:TextBox ID=Pause runat=server Width="400" /></td></tr>
			<tr><td>News category&nbsp;</td><td><asp:DropDownList ID=NewsCategoryID runat=server Width="400" /></td></tr>
			<tr><td><asp:Button ID=Save runat=server Text=Save /></td></tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>
