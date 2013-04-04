<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="Settings.aspx.cs" Inherits="HWgrp.Web.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="contentgroup grid_16">
	<div id="contextbar">
		<div class="settingsPane">
			<asp:PlaceHolder ID="Txt" runat="server">Change password</asp:PlaceHolder>
			<asp:TextBox ID="textBoxPassword" runat="server" TextMode="Password" />
			<asp:Button ID="buttonSave" runat="server" Text="Save" />
			<asp:Label ID="labelMessage" runat="server" />
		</div>
	</div>
	<div class="smallContent">
	</div>
</div>

</asp:Content>
