<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" Inherits="messages" Codebehind="messages.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table border="0" cellspacing="0" cellpadding="0" style="margin:20px;">
		<tr>
			<td><b>Reminder email</b></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=ReminderEmail runat=server Width=460 /></td>
		</tr>
		<tr>
			<td><b>Reminder subject</b></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=ReminderSubject runat=server Width=460 /></td>
		</tr>
		<tr>
			<td><b>Reminder subject</b></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=ReminderMessage runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
		</tr>
		<tr><td>&nbsp;</td></tr>
		<tr><td align=center><asp:Button ID=Save runat=server Text="Save" /><asp:Button ID=RecalculateReminders runat=server Text="Recalculate reminders" /></td></tr>
		</table>
		<%=Db.bottom()%>
		</form>
  </body>
</html>