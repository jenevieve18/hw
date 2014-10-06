<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ReportIssue.aspx.cs" Inherits="HW.MobileApp.ReportIssue" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="More.aspx" data-icon="arrow-l"><%= R.Str(language,"home.more") %></a>
                <h1><%= R.Str(language,"dashboard.reportIssue") %></h1>
                
                <a onserverclick="saveBtn_Click" runat="server"><%= R.Str(language, "button.send")%></a>
</div>

<div data-role="content" id="reportissue">
    <div class="header">
        <h3><asp:Label ID="errormsg" runat="server"></asp:Label></h3>
    </div>
    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblTitle" runat="server" AssociatedControlID="textBoxTitle"><%= R.Str(language, "issue.title")%><span class='req'> *</span></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxTitle" runat="server"></asp:TextBox>
    </div>
    </div>

    <div class="ui-grid-a">
    <div class="ui-block-a">
        <asp:Label ID="lblDescription" runat="server" AssociatedControlID="textBoxDescription"><%= R.Str(language,"issue.description") %><span class='req'> *</span></asp:Label>
    </div>
    <div class="ui-block-b">
        <asp:TextBox data-mini="true" ID="textBoxDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
    </div>
    </div>

</div>

</asp:Content>
