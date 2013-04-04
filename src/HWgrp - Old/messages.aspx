<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeFile="messages.aspx.cs" Inherits="messages" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db2.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
		<div class="container_16" id="admin">
		<%=Db2.nav()%>
            <div class="contentgroup grid_16">
                <div id="contextbar">
                    <div class="settingsPane">
                        <asp:Button ID=Save CssClass="btn" runat=server Text="Save" />
                        
                        Send
                        <asp:DropDownList ID=SendType runat=server>
							<asp:ListItem Value=0 Text="< select send type >" />
							<asp:ListItem Value=1 Text="Registration" />
							<asp:ListItem Value=2 Text="Registration reminder" />
							<asp:ListItem Value=3 Text="Login reminder" />
                            <asp:ListItem Value=9 Text="All activated users" />
						</asp:DropDownList>
						message, confirm with password <asp:TextBox ID=Password runat=server TextMode=Password /> <asp:Button ID=Send runat=server Text="Send" />
                    </div>
                </div>

                <div class="smallContent">
		<table border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td><b>Registration invitation subject/message *</b> (<span style="font-size:9px;">Last sent: <asp:Label ID=InviteLastSent runat=server /></span>)</td>
			<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td bgcolor="#CCCCCC" rowspan="9"><img src="img/null.gif" width="1" height="1" /></td>
			<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td><b>Login reminder subject/message</B> (<span style="font-size:9px;">Last sent: <asp:Label ID=LoginLastSent runat=server /></span>)</td>
		</tr>
		<tr>
			<td><asp:TextBox ID=InviteSubject runat=server Width=460 /></td>
			<td><asp:TextBox ID=LoginSubject runat=server Width=460 /></td>
		</tr>
		<tr>
			<td valign="top"><asp:TextBox ID=InviteTxt runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
			<td>
			    <asp:TextBox ID=LoginTxt runat=server TextMode=MultiLine Rows=6 Width=460 /><br />
			    Send to individuals who have not logged in during the last <asp:DropDownList ID=LoginDays runat=server>
			        <asp:ListItem Value=1 Text="every day" />
				    <asp:ListItem Value=7 Text="week" />
				    <asp:ListItem Value=14 Text="2 weeks" />
				    <asp:ListItem Value=30 Text="month" />
				    <asp:ListItem Value=90 Text="3 months" />
				    <asp:ListItem Value=180 Text="6 months" />
			    </asp:DropDownList><br />
			    Automatically perform check and send every <asp:DropDownList ID=LoginWeekday runat=server>
				    <asp:ListItem Value=NULL Text="< disabled >" />
				    <asp:ListItem Value=0 Text="< every day >" />
				    <asp:ListItem Value=1 Text="Monday" />
				    <asp:ListItem Value=2 Text="Tuesday" />
				    <asp:ListItem Value=3 Text="Wednesday" />
				    <asp:ListItem Value=4 Text="Thursday" />
				    <asp:ListItem Value=5 Text="Friday" />
			    </asp:DropDownList><br /><br /><i>The automatic send in every day mode will only execute once/week/user.</i>
			</td>
		</tr>
		<tr><td colspan=2>&nbsp;</td></tr>
		<tr>
			<td><b>Registration reminder subject/message **</b> (<span style="font-size:9px;">Last sent: <asp:Label ID=InviteReminderLastSent runat=server /></span>)</td>
			<td><asp:Label Visible=false ID="ExtendedSurvey" runat=server/><asp:Label Visible=false ID="ExtendedSurveyFinished" runat=server/></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=InviteReminderSubject runat=server Width=460 /></td>
			<td><asp:TextBox Visible=false ID=ExtendedSurveySubject runat=server Width=460 /><asp:TextBox Visible=false ID=ExtendedSurveyFinishedSubject runat=server Width=460 /></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=InviteReminderTxt runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
			<td><asp:TextBox Visible=false ID=ExtendedSurveyTxt runat=server TextMode=MultiLine Rows=10 Width=460 /><asp:TextBox Visible=false ID=ExtendedSurveyFinishedTxt runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
		</tr>
        <tr><td colspan=2>&nbsp;</td></tr>
		<tr>
			<td><b>Message to all activated users</b> (<span style="font-size:9px;"><asp:Label ID=AllMessageLastSent runat=server /></span>)</td>
		</tr>
		<tr>
			<td><asp:TextBox ID=AllMessageSubject runat=server Width=460 /></td>
		</tr>
		<tr>
			<td><asp:TextBox ID=AllMessageBody runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
		</tr>
		<tr><td colspan=2>&nbsp;</td></tr>
		<tr><td>* Sent only once to each individual (status is reset upon email address change)<br />** Sent only to individuals who have received invitation but not activated their account</td></tr>
		</table>
                </div>
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>
