<%@ Page Language="C#" AutoEventWireup="true" Inherits="sponsorStats" Codebehind="sponsorStats.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Sponsor statistics</td></tr>
		</table>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
			<asp:CheckBoxList ID=Sponsor RepeatColumns=3 runat=server />
		</table>
        <asp:Button text="OK" id=OK runat=server/>
        <asp:Label ID=res runat=server />
		<%=Db.bottom()%>
		</form>
  </body>
</html>