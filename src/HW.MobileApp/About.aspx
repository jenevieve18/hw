<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HW.MobileApp.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


            <div data-role="header" data-theme="b" data-position="fixed">
            <a href="More.aspx">More</a>
                <h1>About</h1>
            </div>

            <div data-role="content" id="about">
                <div class="more">
                    <img  src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                <div>
                HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-related problems.
                </div>
                
                <h5>Health Watch © 2014<br />All Rights Reserved</h5>
                
                </div>
                
            </div>
            
</asp:Content>
