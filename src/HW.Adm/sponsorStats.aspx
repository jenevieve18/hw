<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="sponsorStats.aspx.cs" Inherits="HW.Adm.sponsorStats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Sponsor statistics</td></tr>
		</table>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
			<asp:CheckBoxList ID=Sponsor RepeatColumns=3 runat=server />
		</table>
        <asp:Button text="OK" id=OK runat=server/>
        <asp:Label ID=res runat=server />
</asp:Content>