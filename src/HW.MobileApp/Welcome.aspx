<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="HW.MobileApp.Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<style type="text/css">
    .ui-title { margin: 0.6em 10% 0.8em !important; }
    
</style>
<div data-role="header" data-theme="b" data-position="fixed">
    <h1>Welcome to Health Watch!</h1>
    
</div>
<div data-role="content">
    <div class="welcome">
        Thank you for creating an account with us! Here are a few things that you need to know in order to get started on a healthier and happier life:
    </div>
    <div class="list">
        <img src="img/dash_form.png" />
        <h5>Form</h5><span>Start tracking your health by filling out the form on a regular basis.</span>
        
    </div>
    <div class="list">
        <img src="img/dash_stats.png" />
        <h5>Statistics</h5><span>Check your overall progress and recommended activities.</span>
    </div>
    <div class="list">
        <img src="img/dash_cal.png" />
        <h5>Diary</h5><span>View daily activity and log your thoughts and feelings.</span>
    </div>
    <div class="list">
        <img src="img/dash_exer.png" />
        <h5>Exercises</h5><span>Learn exercises that help improve your overall health.</span>
    </div>
    <br />
    <fieldset data-role="controlgroup">
    <a href="Dashboard.aspx" rel="external" data-role="button">Get started!</a>
    </fieldset>
</div>
</asp:Content>
