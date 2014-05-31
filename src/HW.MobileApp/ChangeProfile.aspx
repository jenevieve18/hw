<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs" Inherits="HW.MobileApp.ChangeProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx" data-icon="arrow-l">Cancel</a>
    <h1>Change Profile</h1>
    <a href="#" data-icon="check">Save</a>
</div>
<div data-role="content">
    <div data-role="fieldcontain">
        <label for="textBoxLanguage">Language</label>
        <asp:TextBox ID="textBoxLanguage" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <label for="textBoxUsername">Username</label>
        <asp:TextBox ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
</div>

</asp:Content>
