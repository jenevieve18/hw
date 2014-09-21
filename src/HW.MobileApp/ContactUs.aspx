<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="HW.MobileApp.ContactUs" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div data-role="header" data-theme="b" data-position="fixed">
        <a href="More.aspx" ><%= R.Str("home.more") %></a>
        <h1><%= R.Str("contact.title") %></h1>
    
    </div>
    <div data-role="content" id="contact">
        <h3>Interactive Health Group<br /> in Stockholm AB<br />
            Box 4047<br />
            10261 Stockholm <br />
            Sweden</h3>
        <p>
            <strong><%= R.Str("contact.publisher") %></strong><br />
            <a href="mailto:dan.hasson@healthwatch.se">dan.hasson@healthwatch.se</a>
        </p>
        <p>
            <strong><%= R.Str("support.title") %></strong><br />
            <a href="mailto:support@healthwatch.se">support@healthwatch.se</a>
        </p>
        <p>
            <strong>Public relations</strong><br />
            <a href="mailto:support@healthwatch.se">support@healthwatch.se</a>
        </p>
        <p>
            <!--<a href="tel:" data-role="button" data-inline="true" data-icon="">Contact Us</a> use to call-->
            <div class="ui-grid-a">
                <div class="ui-block-a">
                    <fieldset data-role="controlgroup">
                        <a href="#" data-role="button" data-inline="true" data-icon=""><%= R.Str("button.contactUs") %></a>
                    </fieldset>
                </div>
                <div class="ui-block-b">
                    <fieldset data-role="controlgroup">
                        <a href="#" data-role="button" data-inline="true" data-icon=""><%= R.Str("button.emailUs") %></a>
                    </fieldset>
                </div>
            </div>
        </p>
    </div>

</asp:Content>
