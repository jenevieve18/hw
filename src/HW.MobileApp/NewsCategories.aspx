<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsCategories.aspx.cs" Inherits="HW.MobileApp.NewsCategories" %>

<!DOCTYPE html>

<html>
<head>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    <style>
        .segmented-control { text-align:center; }
        .segmented-control .ui-controlgroup { margin:0.2em; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="News.aspx" data-role="button" data-icon="arrow-l">Back</a>
                <h1>&nbsp;</h1>
                
            </div>
            <div data-role="content">
                <ul data-role="listview">
                    <li>
                       
                        <div data-role="navbar">
                            <ul>
                            
                            <li><asp:LinkButton ID="btnInt" runat="server" OnClick="toEnglish">International</asp:LinkButton></li>
                            <li><asp:LinkButton ID="btnSwe" runat="server" OnClick="toSwedish">Swedish</asp:LinkButton></li>
                            
                            </ul>
                        </div>
                    <li class="minihead">
                    Select a category to view
                    </li>
                    <li><a href="News.aspx">
                    View all news
                    </a></li>
                    </li>


                    <% foreach (var c in categories) { %>
                        <li >
                        <%var hreflink = "href='NewsCategoriesList.aspx?ncid=" + c.newsCategoryID + "'"; %>
                        <a <%=hreflink %>>
                        <% var imgsrc = "src='" + c.newsCategoryImage + "'"; if (c.newsCategoryImage != null)
                           {%>
                        
                        <img class="newsimg" <%=imgsrc %>> <%} %>
                        <%= c.newsCategory %></a>
                        </li>
                    <% } %>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health">My Health</a></li>
                        <li><a href="News.aspx" data-icon="news">News</a></li>
                        <li><a href="More.aspx" data-icon="more">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
