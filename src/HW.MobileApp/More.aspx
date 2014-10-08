<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="HW.MobileApp.More" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--<link rel="stylesheet" href="jquery.mobile-1.2.1.min.css" />-->
    <link rel="stylesheet" href="https://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/jquery.mobile-1.2.1.min.js"></script>
    
  
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page" id="main">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1><%= R.Str(language,"home.more") %></h1>
            </div>
            <div data-role="content" id="more">
                <ul data-role="listview">
	                <li data-icon="false"><a href="About.aspx"><img class="ui-li-thumb" src="images/about_hw.png" /><%= R.Str(language,"more.about") %></a></li>
	                <li data-icon="false"><a href="Support.aspx"><img class="ui-li-thumb" src="images/support.png" /><%= R.Str(language, "faq.title")%></a></li>
	                <li data-icon="false"><a href="ContactUs.aspx"><img class="ui-li-thumb" src="images/contact_us.png" /><%= R.Str(language, "contact.title")%></a></li>
	                <li data-icon="false"><a href="mailto:support@healthwatch.se?Subject=Hello"><img class="ui-li-thumb" src="images/tell_a_friend.png" /><%= R.Str(language, "more.tell")%></a></li>
	                <li data-icon="false"><a href="https://facebook.com/healthwatch.se"><img class="ui-li-thumb" src="images/fb_like.png" /><%= R.Str(language, "more.like")%></a></li>
                    <%if (displayReport)
                      { %>
                    <li data-icon="false"><a href="ReportIssue.aspx" rel="external"><img class="ui-li-thumb" src="/images/issue.png" /><%= R.Str(language, "dashboard.reportIssue")%></a></li>
                    <%} %>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health" rel="external"><%= R.Str(language, "home.myHealth")%></a></li>
                        <li><a href="News.aspx" data-icon="news" rel="external"><%= R.Str(language, "home.news")%></a></li>
                        <li><a href="More.aspx" data-icon="more" rel="external"><%= R.Str(language, "home.more")%></a></li>
                    </ul>
                </div>
            </div>
        </div>
        

    </form>
</body>
</html>