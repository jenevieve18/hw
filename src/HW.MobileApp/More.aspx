<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="HW.MobileApp.More" %>

<!DOCTYPE>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
  
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page" id="main">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>More</h1>
            </div>
            <div data-role="content" id="more">
                <ul data-role="listview">
	                <li><a href="#about"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/about_hw.png" />About HealthWatch</a></li>
	                <li><a href="Support.aspx"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/support.png" />Support</a></li>
	                <li><a href="ContactUs.aspx"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/contact_us.png" />Contact Us</a></li>
	                <li><a href="mailto:support@healthwatch.se?Subject=Hello"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/tell_a_friend.png" />Tell a Friend</a></li>
	                <li><a href="https://facebook.com/healthwatch.se"><img class="ui-li-thumb" src="http://clients.easyapp.se/healthwatch//images/fb_like.png" />Like us on Facebook</a></li>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="home">My Health</a></li>
                        <li><a href="News.aspx" data-icon="grid">News</a></li>
                        <li><a href="More.aspx" data-icon="info">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div data-role="page" id="about">
            <div data-role="header" data-theme="b" data-position="fixed">
            <a href="#main">More</a>
                <h1>More</h1>
            </div>

            <div data-role="content">
                <div class="more">
                    <img  src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
                <div>
                HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-related problems.
                </div>
                
                <h6>Health Watch © 2012 All Rights Reserved</h6>
                
                </div>
                
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="home">My Health</a></li>
                        <li><a href="News.aspx" data-icon="grid">News</a></li>
                        <li><a href="More.aspx" data-icon="info">More</a></li>
                    </ul>
                </div>
            </div>
        </div>

    </form>
</body>
</html>