<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ReportIssue.aspx.cs" Inherits="HW.MobileApp.ReportIssue" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l"><%= R.Str("home.myHealth") %></a>
                <h1><%= R.Str("dashboard.reportIssue") %></h1>
                
                <a onClick="" runat="server"><%= R.Str("button.save") %></a>
</div>

<div data-role="content" id="reportissue">
    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblTitle" runat="server" AssociatedControlID="textBoxTitle"><%= R.Str("issue.title") %></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxTitle" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblDescription" runat="server" AssociatedControlID="textBoxDescription"><%= R.Str("issue.description") %></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxDescription" placeholder="Write here.."  TextMode="MultiLine" runat="server"></asp:TextBox>
    </div>
    </div>

</div>

</asp:Content>
