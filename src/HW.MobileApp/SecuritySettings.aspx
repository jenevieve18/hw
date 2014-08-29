<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="SecuritySettings.aspx.cs" Inherits="HW.MobileApp.SecuritySettings" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx"><%= R.Str("button.back") %></a>
    <h1><%= R.Str("settings.security.title") %></h1>
    <a runat="server" onserverclick="saveBtnClick"><%= R.Str("button.save") %></a> 
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
