<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="HW.MobileApp.Welcome" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    
    
</style>
<div data-role="header" data-theme="b" data-position="fixed">
    <h1><%= R.Str("welcome.title") %></h1>
    
</div>
<div data-role="content" id="welcomepage">
    <div class="welcome">
        <%= R.Str("welcome.message")%>
    </div>
    <div class="list">
        <img src="img/dash_form.png" />
        <h5><%= R.Str("welcome.form")%></h5>
        <span><%= R.Str("welcome.form.description")%></span>
        
    </div>
    <div class="list">
        <img src="img/dash_stats.png" />
        <h5><%= R.Str("welcome.statistics")%></h5>
        <span><%= R.Str("welcome.statistics.description")%></span>
    </div>
    <div class="list">
        <img src="img/dash_cal.png" />
        <h5><%= R.Str("welcome.diary")%></h5>
        <span><%= R.Str("welcome.diary.description")%></span>
    </div>
    <div class="list">
        <img src="img/dash_exer.png" />
        <h5><%= R.Str("welcome.exercise")%></h5>
        <span><%= R.Str("welcome.exercise.description")%></span>
    </div>
    <br />
    <fieldset data-role="controlgroup">
    <a href="Dashboard.aspx" rel="external" data-role="button"><%= R.Str("button.getStarted") %></a>
    </fieldset>
</div>
</asp:Content>
