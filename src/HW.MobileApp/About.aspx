<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HW.MobileApp.About" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div data-role="header" data-theme="b" data-position="fixed">
        <a href="More.aspx"><%= R.Str(language,"home.more") %></a>
        <h1><%= R.Str(language, "about.title")%></h1>
    </div>

    <div data-role="content" id="about">
        <div class="more">
            <img  src="images/hw_logo@2x.png" />
            <div>
                <%= R.Str(language, "about.text")%>
            </div>
                
            <h5>
                &copy; HealthWatch <%= DateTime.Now.ToString("yyyy") %><br />
                Version <%= typeof(Default).Assembly.GetName().Version %><br />
                <%= R.Str(language, "about.copyright")%>
            </h5>
        </div>
                
    </div>
            
</asp:Content>
