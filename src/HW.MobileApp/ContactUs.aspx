<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="HW.MobileApp.ContactUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx" data-icon="arrow-l">Cancel</a>
    <h1>Change Profile</h1>
    <a href="#" data-icon="check">Save</a>
</div>
<div data-role="content">
    <h3>Interactive Health Group in Stockholm AB<br />
        Box 4047<br />
        10261 Stockholm <br />
        Sweden</h3>
    <p>
        <strong>Publisher</strong><br />
        <a href="mailto:dan.hasson@healthwatch.se">dan.hasson@healthwatch.se</a>
    </p>
    <p>
        <strong>Support</strong><br />
        <a href="mailto:support@healthwatch.se">support@healthwatch.se</a>
    </p>
    <p>
        <strong>Public relations</strong><br />
        <a href="mailto:support@healthwatch.se">support@healthwatch.se</a>
    </p>
    <p>
        <a href="#" data-role="button" data-inline="true" data-icon="">Contact Us</a>
        <a href="#" data-role="button" data-inline="true" data-icon="">Email Us</a>
    </p>
</div>

</asp:Content>
