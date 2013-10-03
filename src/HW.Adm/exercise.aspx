<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="exercise.aspx.cs" Inherits="HW.Adm.exercise" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercises</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
		    <tr>
                <td><i>Exercise</i>&nbsp;&nbsp;</td>
                <td><i>Use count</i>&nbsp;&nbsp;</td>
                <td><i>Users count</i>&nbsp;&nbsp;</td>
                <td><i>Has content</i>&nbsp;&nbsp;</td>
                <td><i>ID</i>&nbsp;&nbsp;</td>
                <td><i>Minutes</i>&nbsp;&nbsp;</td>
                <td><i>Type</i>&nbsp;&nbsp;</td>
                <td><i>Lang</i>&nbsp;&nbsp;</td>
            </tr>
			<asp:Label ID=Exercise runat=server />
		</table>
		<span style="margin:20px;">[<a href="exerciseSetup.aspx">Add exercise</a>] [<a href="exerciseAreaSetup.aspx">Add exercise area</a>] [<a href="exerciseCategorySetup.aspx">Add exercise category</a>]</span>
</asp:Content>
