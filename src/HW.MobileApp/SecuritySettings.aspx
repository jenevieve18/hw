<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="SecuritySettings.aspx.cs" Inherits="HW.MobileApp.SecuritySettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx" data-icon="arrow-l">Back</a>
    <h1>Security Settings</h1>
    <a runat="server" onserverclick="saveBtnClick" data-icon="check">Save</a> 
</div>
<div data-role="content">
    </div>
    <div data-role="fieldcontain">
    
        <asp:CheckBox data-mini="true" ID="cbLogin" Text="Stay logged in" 
            runat="server" 
             EnableViewState="True" ></asp:CheckBox >

        <asp:CheckBox data-mini="true" ID="cbSplash" Text="Show welcome page" 
            runat="server" 
             EnableViewState="True" ></asp:CheckBox >
        <asp:Label ID="test" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>
