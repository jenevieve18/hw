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
        <asp:Label ID="Label2" runat="server" Text="Language:" AssociatedControlID="dropDownListLanguage"></asp:Label>
        <asp:DropDownList ID="dropDownListLanguage"
            runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label1" runat="server" Text="Username:" AssociatedControlID="textBoxUsername"></asp:Label>
        <asp:TextBox ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label3" runat="server" Text="Password:" AssociatedControlID="textBoxPassword"></asp:Label>
        <asp:TextBox ID="textBoxPassword" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label4" runat="server" Text="Email:" AssociatedControlID="textBoxEmail"></asp:Label>
        <asp:TextBox ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label5" runat="server" Text="Alternate Email:" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
        <asp:TextBox ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>
</div>

</asp:Content>
