<%@ Page Language="C#" AutoEventWireup="true" Inherits="exerciseCategorySetup" Codebehind="exerciseCategorySetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercise category setup</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <asp:PlaceHolder ID="ExerciseLang" runat=server />
		</table>
        <asp:Button runat="server" Text="Cancel" id="buttonCancel"
     OnClientClick="window.location='exercise.aspx'; return false;">   </asp:Button>
        <!--<button onclick="location.href='exercise.aspx';">Cancel</button>-->
        <asp:Button ID=Save runat=server Text="Save" />
		<%=Db.bottom()%>
		</form>
  </body>
</html>