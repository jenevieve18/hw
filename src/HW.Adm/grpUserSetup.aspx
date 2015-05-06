<%@ Page Language="C#" AutoEventWireup="true" Inherits="grpUserSetup" Codebehind="grpUserSetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<div style="padding:20px;">
		<a href="grpUser.aspx?Rnd=<%=(new Random(unchecked((int)DateTime.Now.Ticks))).Next()%>">Back</a><br /><br />
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr><td>Username&nbsp;</td><td><asp:TextBox ID="Usr" runat=server />&nbsp;&nbsp;<asp:Button ID=Save runat=server Text="Save" /></td></tr>
		    <tr><td>Password&nbsp;</td><td><asp:TextBox TextMode=Password ID="Pas" runat=server /></td></tr>
		    <tr><td>Sponsor&nbsp;</td><td><asp:DropDownList AutoPostBack=true ID="SponsorID" runat=server /></td></tr>
		    <tr><td valign="top">Departments&nbsp;</td><td><asp:CheckBoxList ID="DepartmentID" runat=server /></td></tr>
		    <tr><td valign="top">Access&nbsp;</td><td><asp:CheckBoxList ID="AccessID" runat=server /></td></tr>
            <tr><td valign="top" colspan="2"><asp:Label ID=errTxt runat=server /></td></tr>
		</table>
		</div>
		<%=Db.bottom()%>
		</form>
  </body>
</html>