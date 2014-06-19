<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="NewsSummary.aspx.cs" Inherits="HW.MobileApp.NewsSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="News.aspx" data-icon="arrow-l">Back</a>
                <h1>Summary</h1>
                
            </div>
        
            <div class="noleftpadding" data-role="content">
        
                <h3 class="padd exheader" ><%=news.headline %></h3>
                <%var imgsrc = "src='"+news.newsCategoryImage+"'"; %>
                <p class="padd">
                <img <%=imgsrc %> style="width:15px;">
                <%=news.DT.ToString("MMM d, yyyy") %></p>
                <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
                <div class="padd contentsmaller">
                    <%=news.body %>
                     <hr />
                     <% var full = "href='" + news.link + "'"; %>
                     <a <%=full %> target="_blank" style="text-decoration:none;">Read Full Story</a>
                </div>
               
                
            </div>
            
</asp:Content>
