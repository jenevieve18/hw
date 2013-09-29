<%@ Page Title="" Language="C#" MasterPageFile="~/GrpMobile.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Grp.Mobile.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="page" id="page1">
    <div data-theme="a" data-role="header">
        <h3>
            HealthWatch - Group administration
        </h3>
    </div>
    <div data-role="content">
        <div style="">
            <img style="width: px; height: px" src="https://healthwatch.se/images/hwlogo.png">
        </div>
        <form action="">
            <div data-role="fieldcontain">
                <input name="" id="ANV" placeholder="Username" value="" type="text">
            </div>
            <div data-role="fieldcontain">
                <input name="" id="LOS" placeholder="Password" value="" type="password">
            </div>
            <input type="submit" value="Log In">
        </form>
    </div>
</div>

</asp:Content>
