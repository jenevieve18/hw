<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="HW.MobileApp.Welcome" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    
    
</style>
<div data-role="header" data-theme="b" data-position="fixed">
    <h1><%= R.Str(language, "welcome.title")%></h1>
    
</div>
<div data-role="content" id="welcomepage">
    <div class="welcome">
        <%= R.Str(language,"welcome.message")%>
    </div>
    <div class="list">
        <img src="img/dash_form.png" />
        <h5><%= R.Str(language, "welcome.form")%></h5>
        <span><%= R.Str(language, "welcome.form.description")%></span>
        
    </div>
    <div class="list">
        <img src="img/dash_stats.png" />
        <h5><%= R.Str(language, "statistics.title")%></h5>
        <span><%= R.Str(language, "welcome.statistics.description")%></span>
    </div>
    <div class="list">
        <img src="img/dash_cal.png" />
        <h5><%= R.Str(language, "welcome.diary")%></h5>
        <span><%= R.Str(language, "welcome.diary.description")%></span>
    </div>
    <div class="list">
        <img src="img/dash_exer.png" />
        <h5><%= R.Str(language, "welcome.exercise")%></h5>
        <span><%= R.Str(language, "welcome.exercise.description")%></span>
    </div>
    <br />
    <fieldset data-role="controlgroup">
    <a href="Dashboard.aspx" rel="external" data-role="button"><%= R.Str(language, "button.getStarted")%></a>
    </fieldset>
</div>
</asp:Content>
