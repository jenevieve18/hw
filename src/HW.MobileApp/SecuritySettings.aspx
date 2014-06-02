<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="SecuritySettings.aspx.cs" Inherits="HW.MobileApp.SecuritySettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx" data-icon="arrow-l">Cancel</a>
    <h1>Security Settings</h1>
    <a href="#" data-icon="check">Save</a>
</div>
<div data-role="content">
    <label><input type="checkbox" name="checkbox-0" />Stay logged in</label>
    <label><input type="checkbox" name="checkbox-1" />Show Welcome Page</label>
</div>

</asp:Content>
