<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="HW.Adm.messages" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</asp:Content>
