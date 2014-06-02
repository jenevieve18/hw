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
    <div class="header">
        <h4>Account Details</h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label2" runat="server" Text="Language:" AssociatedControlID="dropDownListLanguage"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListLanguage"
            runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label1" runat="server" Text="Username:" AssociatedControlID="textBoxUsername"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label3" runat="server" Text="Password:" AssociatedControlID="textBoxPassword"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxPassword" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label4" runat="server" Text="Email:" AssociatedControlID="textBoxEmail"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label5" runat="server" Text="Alternate Email:" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>
    
    <div class="header">
        <h4>Personal Information</h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label6" runat="server" Text="Occupation" AssociatedControlID="dropDownListOccupation"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListOccupation" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label7" runat="server" Text="Industry:" AssociatedControlID="dropDownListIndustry"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListIndustry" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label8" runat="server" Text="Job:" AssociatedControlID="dropDownListJob"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListJob" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label9" runat="server" Text="Annual Income:" AssociatedControlID="dropDownListAnnualIncome"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListAnnualIncome" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label10" runat="server" Text="Education:" AssociatedControlID="dropDownListEducation"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListEducation" runat="server">
        </asp:DropDownList>
    </div>
</div>

<style>
    .header { text-align:center; }
    .header h4 { margin-bottom:0 }
    .header img { width:235px }
</style>
</asp:Content>
