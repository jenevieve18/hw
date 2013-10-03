<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="HW.Adm.stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Statistics</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
			<tr><td colspan="3"><table border="0" cellpadding="0" cellspacing="0"><asp:Label ID=Demographics runat=server /></table></td></tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
			            <asp:Label ID=Stats runat=server />
			        </table>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0">
			            <asp:Label ID=StatsRight runat=server />
			        </table>
                </td>
            </tr>
		</table>
</asp:Content>