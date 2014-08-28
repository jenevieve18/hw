﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="HW.MobileApp.News" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1><%= R.Str("news.text") %></h1>
                <a href="NewsCategories.aspx" rel="external" data-role="button" class="ui-btn-right"><%= R.Str("news.categories") %></a>
            </div>
            <div data-role="content">
                <ul data-role="listview">
                    <% foreach (var n in news) { %>
                        <li>
                            <%var newslink = "href='NewsSummary.aspx?nid=" + n.newsID + "'"; %>
                            <a <%=newslink %>>
                                
                                <!--<h1><%= n.teaser %></h1>-->
                                <h1><%= n.headline %></h1>
                                <p><%= n.newsCategory %></p>
                                <p><%= n.DT.ToString("m") %></p>
                            </a>
                        </li>
                    <% } %>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health"><%= R.Str("home.myHealth") %></a></li>
                        <li><a href="News.aspx" data-icon="news"><%= R.Str("home.news") %></a></li>
                        <li><a href="More.aspx" data-icon="more"><%= R.Str("home.more") %></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
