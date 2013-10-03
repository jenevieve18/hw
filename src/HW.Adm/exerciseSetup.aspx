<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="exerciseSetup.aspx.cs" Inherits="HW.Adm.exerciseSetup" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercise setup</td></tr>
		</table>
		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr><td>Type&nbsp;</td><td><asp:DropDownList ID="RequiredUserLevel" runat=server><asp:ListItem Value="0">End user</asp:ListItem><asp:ListItem Value="10">Manager</asp:ListItem></asp:DropDownList></td><td rowspan="4"><asp:Label ID=ExerciseImgUploaded runat=server /></td></tr>
		    <tr><td>Area&nbsp;</td><td><asp:DropDownList ID=ExerciseAreaID runat=server /></td></tr>
            <tr><td>Category&nbsp;</td><td><asp:DropDownList ID=ExerciseCategoryID runat=server /></td></tr>
            <tr><td>Image (optional)&nbsp;</td><td><input type="file" runat=server id=ExerciseImg /></td></tr>
            <tr><td>Time (statistics)&nbsp;</td><td><asp:TextBox ID=Minutes runat=server Width=35 />minutes</td></tr>
            <asp:PlaceHolder ID="ExerciseLang" runat=server />
            <asp:PlaceHolder ID="ExerciseVariant" runat=server />
		</table>
        <button onclick="location.href='exercise.aspx';">Cancel</button><asp:Button ID=Save runat=server Text="Save" /> Add <asp:DropDownList ID=ExerciseTypeID runat=server />
</asp:Content>