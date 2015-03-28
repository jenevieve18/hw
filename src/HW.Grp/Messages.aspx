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
				<asp:DropDownList ID=dropDownSendType runat=server>
				</asp:DropDownList>
				<%= R.Str(lid, "message.confirm", "message, confirm with password")%> 
                <asp:TextBox ID=textBoxPassword runat=server TextMode=Password /> <asp:Button ID=buttonSend runat=server Text="Send" />
                <br />
				<asp:Button ID=buttonRevert CssClass="btn" runat=server Text="Revert to default" />
			</div>
		</div>

		<div class="smallContent">
			<br />
			<table border="0" cellspacing="0" cellpadding="0">
				<tr>
                    <td><b><%= R.Str(lid, "registration.invitation.subject", "Registration invitation subject/message *")%></b> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: 
                        <asp:Label ID=labelInviteLastSent runat=server /></span>)</td>
					<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td bgcolor="#CCCCCC" rowspan="9"><img src="img/null.gif" width="1" height="1" /></td>
					<td rowspan="9">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td><b><%= R.Str(lid, "login.reminder.subject", "Login reminder subject/message")%></B> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: 
                        <asp:Label ID=labelLoginLastSent runat=server /></span>)</td>
				</tr>
				<tr>
					<td><asp:TextBox ID=textBoxInviteSubject runat=server Width=460 /></td>
					<td><asp:TextBox ID=textBoxLoginSubject runat=server Width=460 /></td>
				</tr>
				<tr>
					<td valign="top"><asp:TextBox ID=textBoxInviteTxt runat=server TextMode=MultiLine 
                            Rows=10 Width=460 /></td>
					<td>
						<asp:TextBox ID=textBoxLoginTxt runat=server TextMode=MultiLine Rows=6 
                            Width=460 /><br />
						<%= R.Str(lid, "send.individuals", "Send to individuals who have not logged in during the last")%> 
                        <asp:DropDownList ID=dropDownLoginDays runat=server>
						</asp:DropDownList><br />
						<%= R.Str(lid, "send.auto.perform", "Automatically perform check and send every")%> 
                        <asp:DropDownList ID=dropDownLoginWeekday runat=server>
						</asp:DropDownList><br /><br /><i><%= R.Str(lid, "send.auto.day", "The automatic send in every day mode will only execute once/week/user.")%></i>
					</td>
				</tr>
				<tr><td colspan=2>&nbsp;</td></tr>
				<tr>
					<td><b><%= R.Str(lid, "registration.reminder.subject", "Registration reminder subject/message **")%></b> (<span style="font-size:9px;"><%= R.Str(lid, "sent.last", "Last sent") %>: 
                        <asp:Label ID=labelInviteReminderLastSent runat=server /></span>)</td>
					<td>
                        <asp:Label Visible=False ID="labelExtendedSurvey" runat=server/>
                        <asp:Label Visible=False ID="labelExtendedSurveyFinished" runat=server/>
                    </td>
				</tr>
				<tr>
					<td><asp:TextBox ID=textBoxInviteReminderSubject runat=server Width=460 /></td>
					<td>
                        <asp:TextBox Visible=false ID=textBoxExtendedSurveySubject runat=server 
                            Width=460 />
                        <asp:TextBox Visible=false ID=textBoxExtendedSurveyFinishedSubject runat=server 
                            Width=460 />
                    </td>
				</tr>
				<tr>
					<td><asp:TextBox ID=textBoxInviteReminderTxt runat=server TextMode=MultiLine 
                            Rows=10 Width=460 /></td>
					<td>
                        <asp:TextBox Visible=false ID=textBoxExtendedSurveyTxt runat=server 
                            TextMode=MultiLine Rows=10 Width=460 />
                        <asp:TextBox Visible=false ID=textBoxExtendedSurveyFinishedTxt runat=server 
                            TextMode=MultiLine Rows=10 Width=460 />
                    </td>
				</tr>
				<tr><td colspan=2>&nbsp;</td></tr>
				<tr>
					<td><b><%= R.Str(lid, "message.activated.all", "Message to all activated users")%></b> (<span style="font-size:9px;"><asp:Label 
                            ID=labelAllMessageLastSent runat=server /></span>)</td>
				</tr>
				<tr>
					<td><asp:TextBox ID=textBoxAllMessageSubject runat=server Width=460 /></td>
				</tr>
				<tr>
					<td><asp:TextBox ID=textBoxAllMessageBody runat=server TextMode=MultiLine Rows=10 
                            Width=460 /></td>
				</tr>
				<tr><td colspan=2>&nbsp;</td></tr>
				<tr><td><%= R.Str(lid, "send.once", "* Sent only once to each individual (status is reset upon email address change)") %><br /><%= R.Str(lid, "send.only", "** Sent only to individuals who have received invitation but not activated their account")%></td></tr>
			</table>
		</div>
	</div>

</asp:Content>
