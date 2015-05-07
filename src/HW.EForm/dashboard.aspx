<%@ Page language="c#" ValidateRequest="false" Codebehind="dashboard.aspx.cs" AutoEventWireup="false" Inherits="eform.dashboard" %>
<html>
<head>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	-->
<title>eForm Dashboard</title>
<link href="submit2.css" rel="stylesheet" type="text/css" media="screen">
<link href="submit2print.css" rel="stylesheet" type="text/css" media="print">
</head>
<body>
<form name="usermgr" method="post" runat="server" ID="Form1">
<div id="container">
	<div id="header">
		<div id="left"><asp:Label ID=LeftLogo Runat=server/></div>
		<div id="right"><img src="submitImages/eform_logga.gif"></div>
		<div id="clear"></div>
	</div>
	<div id="eform_header"><p><asp:Label ID=DashboardHeader Runat=server/></p></div>
	<asp:PlaceHolder ID=LoginBox Runat=server>
		<div class="eform_area"><p>Log in console</p></div>
		<div class="eform_ques" style="padding:20px;">
			Username&nbsp;<asp:TextBox ID="Username" runat=server/>&nbsp;Password&nbsp;<asp:TextBox ID=Password TextMode=Password Runat=server/> <asp:Button ID="Login" Text="OK" Runat=server/>
		</div>
	</asp:PlaceHolder>
	<asp:PlaceHolder ID=LoggedIn Runat=server Visible=False>
		<asp:PlaceHolder ID=EditUser Runat=server visible=false>
			<div class="eform_area"><p><asp:Label ID=Edit Runat=server/></p></div>
			<div class="eform_ques" style="padding:20px;">
				<asp:LinkButton ID=DeleteUser Runat=server Visible=False><img align="right" src="img/delToolSmall.gif" border="0"/></asp:LinkButton>
				<table border="0" cellspacing="0" cellpadding="0">
				<asp:PlaceHolder ID="UserNrPH" Visible=false Runat=server><tr><td><asp:Label ID="UserNrText" runat=server/>&nbsp;</td><td><asp:Label ID="UserNr" runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent1PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent1Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent1" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent2PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent2Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent2" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent3PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent3Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent3" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent4PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent4Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent4" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent5PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent5Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent5" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent6PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent6Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent6" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent7PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent7Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent7" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent8PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent8Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent8" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent9PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent9Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent9" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserIdent10PH" Visible=false Runat=server><tr><td><asp:Label ID="UserIdent10Text" runat=server/>&nbsp;</td><td><asp:TextBox ID="UserIdent10" width=300 runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserCheck1PH" Visible=false Runat=server><tr><td><asp:Label ID="UserCheck1Text" runat=server/>&nbsp;</td><td><asp:RadioButtonList ID="UserCheck1" RepeatDirection=horizontal RepeatLayout=table runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserCheck2PH" Visible=false Runat=server><tr><td><asp:Label ID="UserCheck2Text" runat=server/>&nbsp;</td><td><asp:RadioButtonList ID="UserCheck2" RepeatDirection=horizontal RepeatLayout=table runat=server/></td></tr></asp:PlaceHolder>
				<asp:PlaceHolder ID="UserCheck3PH" Visible=false Runat=server><tr><td><asp:Label ID="UserCheck3Text" runat=server/>&nbsp;</td><td><asp:RadioButtonList ID="UserCheck3" RepeatDirection=horizontal RepeatLayout=table runat=server/></td></tr></asp:PlaceHolder>
				</table>
				<br/>
				<asp:Button ID=Cancel Text="Avbryt" Runat=server/>&nbsp;<asp:Button ID="Save" Text="Spara" Runat=server/>&nbsp;&nbsp;<asp:Button ID="AddNote" Visible=False Text="Lägg till notering" Runat=server/>&nbsp;<asp:Button ID="AddSurvey" Visible=False Text="Lägg till enkät" Runat=server/>&nbsp;<asp:Button ID="AddVisit" Visible=False Text="Lägg till besök" Runat=server/>
				<asp:PlaceHolder ID=NotePH Visible=false Runat=server><br/><br/><asp:Label ID="NoteDetails" runat=server/><asp:TextBox ID="Note" Width=500 Rows=10 TextMode=MultiLine Runat=server/></asp:PlaceHolder>
				<asp:PlaceHolder ID="VisitPH" Visible=false Runat=server>
					<br/><br/>
					<table border="0" cellspacing="0" cellpadding="0">
						<tr><td>Datum/tid&nbsp;</td><td><asp:TextBox width=150 ID="VisitDT" runat=server/> (yyyy-MM-dd HH:mm)</td></tr>
						<tr><td>Kommentar&nbsp;</td><td><asp:TextBox width=300 ID="VisitNote" runat=server/></td></tr>
						<tr><td>Påminnelse&nbsp;</td><td><asp:TextBox width=50 ID="VisitReminder" runat=server/> dagar innan med e-post</td></tr>
						<tr><td>Påminnelse text&nbsp;</td><td><asp:DropDownList width=300 ID="VisitSponsorReminderID" runat=server/></td></tr>
						<tr><td>Påminnelse enkät&nbsp;</td><td><asp:DropDownList width=300 ID="VisitUPRUID" runat=server/></td></tr>
						<tr><td>Påminnelse e-post&nbsp;</td><td><asp:TextBox Width=300 ID="VisitEmail" Runat=server/></td></tr>
					</table>
				</asp:PlaceHolder>
				<asp:PlaceHolder ID=SurveyPH Visible=False Runat=server>
					<br/><br/>
					<table border="0" cellspacing="0" cellpadding="0">
						<tr><td>Enkät&nbsp;</td><td><asp:DropDownList width=300 ID="ProjectRoundUnitID" runat=server/></td></tr>
						<tr><td>E-post&nbsp;</td><td><asp:TextBox Width=300 ID=Email Runat=server/></td></tr>
						<tr><td>Kommentar&nbsp;</td><td><asp:TextBox width=300 ID=SurveyNote runat=server/></td></tr>
					</table>
				</asp:PlaceHolder>
				<asp:Label ID=Notes Runat=server/>
				<asp:Label ID=Surveys Runat=server/>
				<asp:Label ID="Visits" Runat=server/>
				<div id="sendlink" style="display:none;">
				<hr/><input type="hidden" id="sendlinkUPRUID" runat=server NAME="sendlinkUPRUID"/>
				<table border="0" cellpadding="0" cellspacing="0">
					<tr><td>Enkät</td><td>Skickas till</td><td>Skickas från</td></tr>
					<tr><td><asp:Label ID=sendlinksurvey Runat=server/></td><td><asp:Label ID=sendlinkto Runat=server/></td><td><asp:Label ID="sendlinkfrom" Runat=server/></td><td align="right"><asp:Button ID=sendlinkbutton Text=Skicka Runat=server/></td></tr>
					<tr><td>Ämnesrad</td></tr>
					<tr><td colspan="4"><asp:TextBox ID="sendlinksubject" Runat=server Width=500/></td></tr>
					<tr><td>Meddelande</td></tr>
					<tr><td colspan="4"><asp:TextBox ID="sendlinkbody" Runat=server Width=500 Rows=10 TextMode=MultiLine/></td></tr>
				</table>
				</div>
				<asp:PlaceHolder ID=SendVisitReminder Runat=server Visible=False>
				<hr/>
				<asp:Label ID=FirstLastName runat=server visible=false/>
				<table border="0" cellpadding="0" cellspacing="0">
					<tr><td>Besöksdatum</td><td>Skickas till</td><td>Skickas från</td></tr>
					<tr><td><asp:Label ID="SendVisitReminderDT" Runat=server/></td><td><asp:Label ID="SendVisitReminderTo" Runat=server/></td><td><asp:Label ID="SendVisitReminderFrom" Runat=server/></td><td align="right"><asp:Button ID="SendVisitReminderButton" Text=Skicka Runat=server/></td></tr>
					<tr><td>Ämnesrad</td></tr>
					<tr><td colspan="4"><asp:TextBox ID="SendVisitReminderSubject" Runat=server Width=500/></td></tr>
					<tr><td>Meddelande</td></tr>
					<tr><td colspan="4"><asp:TextBox ID="SendVisitReminderBody" Runat=server Width=500 Rows=10 TextMode=MultiLine/></td></tr>
				</table>
				</asp:PlaceHolder>
			</div>
			<br/><br/>
		</asp:PlaceHolder>
		<div class="eform_area"><p>Personer</p></div>
		<div class="eform_ques" style="padding:20px;">
			<div style="float:left;"><asp:TextBox ID=SearchText Width=200 runat=server/>&nbsp;<asp:Button ID=Search Text="Sök" Runat=server/>&nbsp;&nbsp;&nbsp;<asp:Button ID="AddUser" Text="Lägg till" Runat=server/></div>
			<div style="float:right;"><asp:Button ID="SPSS" Visible=false Text="Ta ut data" Runat=server/><asp:Button ID="ExportButton" Visible=false Text="Ta ut lista" Runat=server/>&nbsp;&nbsp;<asp:Button ID="Logout" Text="Logga ut" Runat=server/></div>
			<div style="clear:all;"></div>
			<asp:Label ID=SearchResults Runat=server/>
			<asp:PlaceHolder ID=ExportSurvey Visible=false Runat=server>
				<table border="0"><tr><td>Välj formulär<br/><asp:RadioButtonList RepeatColumns=1 RepeatDirection=Vertical RepeatLayout=Table ID=ProjectRoundID Runat=server/></td><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td valign="top">Övriga inställningar<br/><asp:CheckBox ID=IDS Runat=server/> Inkludera identifikationsvariabler<br/><asp:CheckBox ID="Unfinished" Runat=server/> Inkludera även icke finaliserad data<br/><br/><asp:Button ID="ExportSPSS" Text="Exportera till SPSS" Runat=server/><br/><br/><i>Öppna filen i SPSS, kör &quot;RUN ALL&quot;.</i></td></tr></table>
			</asp:PlaceHolder>
		</div>
		<br/><br/>
	</asp:PlaceHolder>
</div>
</form>
</body>
</html>

