﻿<%@ Page Language="C#" AutoEventWireup="true" Inherits="wise" Codebehind="wise.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Words of wisdom</td></tr>
		</table>
		<!--<table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
			<asp:Label ID=Wisdom runat=server />
		</table>-->
        <table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
            <% foreach (var w in words) { %>
            <tr>
                <td width="600"><%= w.WiseName %></td>
                <td valign="top"><%= w.WiseBy %></td>
                <td valign="top">
                    <% if (w.Wise.LastShown != null) { %>
                        <%= w.Wise.LastShown.Value.ToString("yyyy-MM-dd") %>
                    <% } %>
                </td>
            </tr>
            <% } %>
        </table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>