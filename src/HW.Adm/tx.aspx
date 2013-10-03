<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="tx.aspx.cs" Inherits="HW.Adm.tx" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Uploaded files</td></tr>
		</table>
		<table style="margin:20px;" width="900" border="0" cellspacing="5" cellpadding="0">
		    <asp:Label ID="list" EnableViewState=false runat=server />
		</table>
</asp:Content>
