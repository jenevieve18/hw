<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="superadmin.aspx.cs" Inherits="HW.Adm.superadmin" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Super managers</td></tr>
		</table>
        <table border="0" cellspacing="0" cellpadding="5" id="SponsorAdminChange" runat=server visible=false>
        <tr><td>Username</td><td><asp:TextBox ID=Username runat=server /></td></tr>
        <tr><td>Password</td><td><asp:TextBox ID=Password runat=server /></td></tr>
        <tr><td valign="top">Sponsor</td><td><asp:CheckBoxList RepeatColumns=3 ID=SponsorID runat=server /></td></tr>
        <tr><td><asp:Button ID=Submit Text="Save" runat=server /></td></tr>
        </table>
		<asp:PlaceHolder id="SuperAdminList" runat=server visible=false>
        <table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
            <tr><td><b>Username</b></td><td><b>Sponsors</b></td></tr>
			<asp:Label ID=Superadmins runat=server />
            <tr><td colspan="2"><a href="superadmin.aspx?SuperAdminID=0">Add new &gt;&gt;</a></td></tr>
		</table>
        </asp:PlaceHolder>
</asp:Content>
