<%@ Page Language="C#" AutoEventWireup="true" Inherits="superadmin" Codebehind="superadmin.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Super managers</td></tr>
		</table>
        <table border="0" cellspacing="0" cellpadding="5" id="SponsorAdminChange" runat=server visible=false>
        <tr><td>Username</td><td><asp:TextBox ID=Username runat=server /></td></tr>
        <tr><td>Password</td><td><asp:TextBox ID=Password runat=server /></td></tr>
        <tr><td valign="top">Sponsor</td><td><asp:PlaceHolder ID=SponsorID runat=server /></td></tr>
        <tr><td><asp:Button ID=Submit Text="Save" runat=server /></td></tr>
        </table>
		<asp:PlaceHolder id="SuperAdminList" runat=server visible=false>
        <table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
            <tr><td><b>Username</b></td><td><b>Sponsors</b></td></tr>
			<asp:Label ID=Superadmins runat=server />
            <tr><td colspan="2"><a href="superadmin.aspx?SuperAdminID=0">Add new &gt;&gt;</a></td></tr>
		</table>
        </asp:PlaceHolder>
		<%=Db.bottom()%>
		</form>
  </body>
</html>