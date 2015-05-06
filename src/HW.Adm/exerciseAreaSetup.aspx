<%@ Page Language="C#" AutoEventWireup="true" Inherits="exerciseAreaSetup" Codebehind="exerciseAreaSetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercise area setup</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr><td>Image (optional)&nbsp;</td><td><input type="file" runat=server id=ExerciseImg /></td><td><asp:Label ID=ExerciseImgUploaded runat=server /></td></tr>
            <asp:PlaceHolder ID="ExerciseLang" runat=server />
		</table>
        <button onclick="location.href='exercise.aspx';">Cancel</button><asp:Button ID=Save runat=server Text="Save" />
		<%=Db.bottom()%>
		</form>
  </body>
</html>