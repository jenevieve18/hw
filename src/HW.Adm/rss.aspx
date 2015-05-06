<%@ Page Language="C#" AutoEventWireup="true" Inherits="rss" Codebehind="rss.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">News setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="5" cellpadding="0">
		    <tr>
		        <td>RSS Feeds [<a href="rssSetupChannel.aspx">Add</a>]</td>
		        <td>RSS Sources [<a href="rssSetupSource.aspx">Add</a>]</td>
		        <td>News categories [<a href="rssSetupNewsCategory.aspx">Add</a>]</td>
		    </tr>
			<tr>
			    <td valign="top"><asp:Label ID=Channel runat=server /></td>
			    <td valign="top"><asp:Label ID=Source runat=server /></td>
			    <td valign="top"><asp:Label ID="NewsCategory" runat=server /></td>
			</tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>