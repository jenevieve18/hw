<%@ Page Language="C#" AutoEventWireup="true" Inherits="stats" Codebehind="stats.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Statistics</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td colspan="3"><table border="0" cellpadding="0" cellspacing="0"><asp:Label ID=Demographics runat=server /></table></td></tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
			            <asp:Label ID=Stats runat=server />
			        </table>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
			            <asp:Label ID=StatsRight runat=server />
			        </table>
                </td>
            </tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>