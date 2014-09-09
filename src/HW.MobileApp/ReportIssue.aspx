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

<div data-role="content" >
</div>

</asp:Content>
