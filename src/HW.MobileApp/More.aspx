<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="HW.MobileApp.More" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE>

<html>
<head runat="server">
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
        <div data-role="page" id="main">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1><%= R.Str("home.more") %></h1>
            </div>
            <div data-role="content" id="more">
                <ul data-role="listview">
	                <li><a href="About.aspx"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/about_hw.png" /><%= R.Str(language,"more.about") %></a></li>
	                <li><a href="Support.aspx"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/support.png" /><%= R.Str(language, "support.title")%></a></li>
	                <li><a href="ContactUs.aspx"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/contact_us.png" /><%= R.Str(language, "contact.title")%></a></li>
	                <li><a href="mailto:support@healthwatch.se?Subject=Hello"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/tell_a_friend.png" /><%= R.Str(language, "more.tell")%></a></li>
	                <li><a href="https://facebook.com/healthwatch.se"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/fb_like.png" /><%= R.Str(language, "more.like")%></a></li>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health"><%= R.Str(language, "home.myHealth")%></a></li>
                        <li><a href="News.aspx" data-icon="news"><%= R.Str(language, "home.news")%></a></li>
                        <li><a href="More.aspx" data-icon="more"><%= R.Str(language, "home.more")%></a></li>
                    </ul>
                </div>
            </div>
        </div>
        

    </form>
</body>
</html>