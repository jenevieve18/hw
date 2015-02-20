<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="HW.Grp.Messages" ValidateRequest="false" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
		<div id="contextbar">
			<div class="settingsPane">
				<asp:Button ID=buttonSave CssClass="btn" runat=server Text="Save" />
                        
				<%= R.Str(lid, "send", "Send")%>
				<asp:DropDownList ID=SendType runat=server>
				<%--<asp:ListItem Value=0 Text="< select send type >" />
				<asp:ListItem Value=1 Text="Registration" />
				<asp:ListItem Value=2 Text="Registration reminder" />
				<asp:ListItem Value=3 Text="Login reminder" />
				<asp:ListItem Value=9 Text="All activated users" />--%>
				</asp:DropDownList>
				<%= R.Str(lid, "message.confirm", "message, confirm with password")%> <asp:TextBox ID=Password runat=server TextMode=Password /> <asp:Button ID=buttonSend runat=server Text="Send" />
                <br />
				<asp:Button ID=buttonRevert CssClass="btn" runat=server Text="Revert to default" />
			</div>
		</div>

		<div class="smallContent">
			<br />
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td><b><%= R.Str(lid, "registration.invitation.subject", "Registration invitation subject/message *")%></b> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: <asp:Label ID=InviteLastSent runat=server /></span>)</td>
					<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td bgcolor="#CCCCCC" rowspan="9"><img src="img/null.gif" width="1" height="1" /></td>
					<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td><b><%= R.Str(lid, "login.reminder.subject", "Login reminder subject/message")%></B> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: <asp:Label ID=LoginLastSent runat=server /></span>)</td>
				</tr>
				<tr>
					<td><asp:TextBox ID=InviteSubject runat=server Width=460 /></td>
					<td><asp:TextBox ID=LoginSubject runat=server Width=460 /></td>
				</tr>
				<tr>
					<td valign="top"><asp:TextBox ID=InviteTxt runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
					<td>
						<asp:TextBox ID=LoginTxt runat=server TextMode=MultiLine Rows=6 Width=460 /><br />
						<%= R.Str(lid, "send.individuals", "Send to individuals who have not logged in during the last")%> <asp:DropDownList ID=LoginDays runat=server>
						<%--<asp:ListItem Value=1 Text="every day" />
						<asp:ListItem Value=7 Text="week" />
						<asp:ListItem Value=14 Text="2 weeks" />
						<asp:ListItem Value=30 Text="month" />
						<asp:ListItem Value=90 Text="3 months" />
						<asp:ListItem Value=180 Text="6 months" />--%>
						</asp:DropDownList><br />
						<%= R.Str(lid, "send.auto.perform", "Automatically perform check and send every")%> <asp:DropDownList ID=LoginWeekday runat=server>
						<%--<asp:ListItem Value=NULL Text="< disabled >" />
						<asp:ListItem Value=0 Text="< every day >" />
						<asp:ListItem Value=1 Text="Monday" />
						<asp:ListItem Value=2 Text="Tuesday" />
						<asp:ListItem Value=3 Text="Wednesday" />
						<asp:ListItem Value=4 Text="Thursday" />
						<asp:ListItem Value=5 Text="Friday" />--%>
						</asp:DropDownList><br /><br /><i><%= R.Str(lid, "send.auto.day", "The automatic send in every day mode will only execute once/week/user.")%></i>
					</td>
				</tr>
				<tr><td colspan=2>&nbsp;</td></tr>
				<tr>
					<td><b><%= R.Str(lid, "registration.reminder.subject", "Registration reminder subject/message **")%></b> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: <asp:Label ID=InviteReminderLastSent runat=server /></span>)</td>
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
					<td><b><%= R.Str(lid, "message.activated.all", "Message to all activated users")%></b> (<span style="font-size:9px;"><asp:Label ID=AllMessageLastSent runat=server /></span>)</td>
				</tr>
				<tr>
					<td><asp:TextBox ID=AllMessageSubject runat=server Width=460 /></td>
				</tr>
				<tr>
					<td><asp:TextBox ID=AllMessageBody runat=server TextMode=MultiLine Rows=10 Width=460 /></td>
				</tr>
				<tr><td colspan=2>&nbsp;</td></tr>
				<tr><td><%= R.Str(lid, "send.once", "* Sent only once to each individual (status is reset upon email address change)") %><br /><%= R.Str(lid, "send.only", "** Sent only to individuals who have received invitation but not activated their account")%></td></tr>
			</table>
		</div>
	</div>

</asp:Content>
