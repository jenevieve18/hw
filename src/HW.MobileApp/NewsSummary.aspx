<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="NewsSummary.aspx.cs" Inherits="HW.MobileApp.NewsSummary" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div data-role="header" data-theme="b" data-position="fixed">
            <%if (Request.QueryString["ncid"] != null)
              { %>
                <a <%=back %> ><%= R.Str("button.back") %></a>
            <%}
              else
              { %>
                <a href="News.aspx" ><%= R.Str("button.back") %></a>
            <%} %>
                <h1><%= R.Str("news.summary") %></h1>
                
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
