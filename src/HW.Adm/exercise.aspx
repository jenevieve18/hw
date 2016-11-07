<%@ Page Language="C#" AutoEventWireup="true" Inherits="exercise" Codebehind="exercise.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercises</td></tr>
		</table>
        <table style="margin:20px;" width="1000" border="0" cellspacing="0" cellpadding="0">
		    <tr>
                <td><i>Exercise</i>&nbsp;&nbsp;</td>
                <td><i>Use count</i>&nbsp;&nbsp;</td>
                <td><i>Users count</i>&nbsp;&nbsp;</td>
                <td><i>Has content</i>&nbsp;&nbsp;</td>
                <td><i>ID</i>&nbsp;&nbsp;</td>
                <td><i>Minutes</i>&nbsp;&nbsp;</td>
                <td><i>Type</i>&nbsp;&nbsp;</td>
                <td><i>Lang</i>&nbsp;&nbsp;</td>
                <td><asp:Button ID="buttonSaveExerciseStatus" runat="server" Text="Save Exercise Status" OnClick="buttonSaveExerciseStatusClick" />
		</td>
            </tr>
			<asp:Label ID=Exercise runat=server />
		</table>
		<span style="margin:20px;">[<a href="exerciseSetup.aspx">Add exercise</a>] [<a href="exerciseAreaSetup.aspx">Add exercise area</a>] [<a href="exerciseCategorySetup.aspx">Add exercise category</a>]</span>
		<%=Db.bottom()%>
		</form>
  </body>
</html>