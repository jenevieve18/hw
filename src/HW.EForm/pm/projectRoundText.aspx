<%@ Page ValidateRequest="false" language="c#" Codebehind="projectRoundText.aspx.cs" AutoEventWireup="false" Inherits="eform.projectRoundText" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Text elements</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel=stylesheet type=text/css href="survey.css">
  </head>
  <body>
	
    <form id="projectRoundText" method="post" runat="server">
		<input type="hidden" ID="LastLangID" Name="LastLangID" runat=server/>
		<table border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td><asp:RadioButtonList ID=LangID AutoPostBack=True RepeatDirection=Horizontal RepeatLayout=Table Runat=server/></td>
				<td><asp:Button ID="Save" Text="Save" Runat=server/></td><td><asp:Button ID="Close" Text=Close Runat=server/></td>
				<td>&nbsp;&nbsp;&nbsp;&lt;LINK&gt; specifies clickable link location</td>
			</tr>
		</table>
		<table border="0" cellspacing="0" cellpadding="0">
			<tr><td colspan="2"><B>Survey name &amp; introduction</B></td></tr>
			<tr><td colspan="2"><asp:TextBox ID="SurveyName" Runat=server Width=700 /></td></tr>
			<tr><td colspan="2"><asp:TextBox ID=SurveyIntro Runat=server Rows=10 Width=700 TextMode=MultiLine /></td></tr>
			<tr><td colspan="2"><B>Invitation letter subject &amp; body</B></td></tr>
			<tr><td><asp:TextBox ID="InvitationSubject" Runat=server Width=350 /></td><td><asp:TextBox ID="ExtraInvitationSubject" Runat=server Width=350 /></td></tr>
			<tr><td><asp:TextBox ID="InvitationBody" Runat=server Rows=10 Width=350 TextMode=MultiLine /></td><td><asp:TextBox ID="ExtraInvitationBody" Runat=server Rows=10 Width=350 TextMode=MultiLine /></td></tr>
			<tr><td colspan="2"><B>Reminder letter subject &amp; body</B></td></tr>
			<tr><td><asp:TextBox ID="ReminderSubject" Runat=server Width=350 /></td><td><asp:TextBox ID="ExtraReminderSubject" Runat=server Width=350 /></td></tr>
			<tr><td><asp:TextBox ID="ReminderBody" Runat=server Rows=10 Width=350 TextMode=MultiLine /></td><td><asp:TextBox ID="ExtraReminderBody" Runat=server Rows=10 Width=350 TextMode=MultiLine /></td></tr>
			<tr><td colspan="2"><B>Thank you text</B></td></tr>
			<tr><td colspan="2"><asp:TextBox ID="ThankyouText" Runat=server Rows=10 Width=700 TextMode=MultiLine /></td></tr>
			<tr><td colspan="2"><B>Unit affiliation question</B></td></tr>
			<tr><td colspan="2"><asp:TextBox ID="UnitText" Runat=server Width=700 /></td></tr>
     </form>
  </body>
</html>
