<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="exerciseCategorySetup.aspx.cs" Inherits="HW.Adm.exerciseCategorySetup" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercise category setup</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <asp:PlaceHolder ID="ExerciseLang" runat=server />
		</table>
        <button onclick="location.href='exercise.aspx';">Cancel</button><asp:Button ID=Save runat=server Text="Save" />
</asp:Content>