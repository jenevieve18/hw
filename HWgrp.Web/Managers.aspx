<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HWgrp.Web.Managers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="contentgroup grid_16">
	<div id="contextbar">
		<div class="actionPane2">
			<div class="" id=ActionNav runat=server>
				<a class="add-user" href="managerSetup.aspx">Add manager</a>
			</div>
		</div>
	</div>
	<div class="smallContent">
		<br />
		<table border="0" cellpadding="0" cellspacing="0">
			<asp:Label ID=labelManagers runat=server/>
		</table>
	</div>
</div>

</asp:Content>
