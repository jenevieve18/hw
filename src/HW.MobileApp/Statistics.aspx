<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HW.MobileApp.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l">My Health</a>
    <h1>Statistics</h1>
    <a href="#" data-icon="check">View</a>
</div>
<div data-role="content">
    <ul data-role="listview">
        <% foreach (var q in questions) { %>
            <li>
                <%= q.QuestionText %>
            </li>
        <% } %>
    </ul>
</div>

</asp:Content>
