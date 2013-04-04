<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HWgrp.Web.Mobile.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="page" id="page1">
    <div data-theme="a" data-role="header">
        <h3>
            Healthwatch
        </h3>
    </div>
    <div data-role="content">
        <div style="">
            <img src="http://dev-grp.healthwatch.se/img/hwlogo.png">
        </div>
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup">
                <label for="textinput1"></label>
                <input name="ANV" placeholder="Username" value="" type="text">
            </fieldset>
        </div>
        <div data-role="fieldcontain">
            <fieldset data-role="controlgroup"></label>
                <input name="LOS" placeholder="Password" value="" type="password">
            </fieldset>
        </div>
        <input type="submit" value="Login">
    </div>
</div>

</asp:Content>
