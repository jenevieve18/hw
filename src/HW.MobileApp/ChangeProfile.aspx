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
        <label for="dropDownListLanguage">Language:</label>
        <asp:DropDownList ID="dropDownListLanguage"
            runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <label for="textBoxUsername">Username:</label>
        <asp:TextBox ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <label for="textBoxPassword">Password:</label>
        <asp:TextBox ID="textBoxPassword" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <label for="textBoxEmail">Email:</label>
        <asp:TextBox ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <label for="textBoxAlternateEmail">Alternate Email:</label>
        <asp:TextBox ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>
</div>

</asp:Content>
