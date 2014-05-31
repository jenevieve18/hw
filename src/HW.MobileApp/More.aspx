﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="More.aspx.cs" Inherits="HW.MobileApp.More" %>

<!DOCTYPE>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>More</h1>
            </div>
            <div data-role="content">
                <ul data-role="listview">
	                <li><a href="#"><img class="ul-li-icon" src="http://clients.easyapp.se/healthwatch//images/about_hw.png" />About HealthWatch</a></li>
	                <li><a href="#"><img class="ul-li-icon" src="http://clients.easyapp.se/healthwatch//images/support.png" />Support</a></li>
	                <li><a href="#"><img class="ul-li-icon" src="http://clients.easyapp.se/healthwatch//images/contact_us.png" />Contact Us</a></li>
	                <li><a href="#"><img class="ul-li-icon" src="http://clients.easyapp.se/healthwatch//images/tell_a_friend.png" />Tell a Friend</a></li>
	                <li><a href="#"><img class="ul-li-icon" src="http://clients.easyapp.se/healthwatch//images/fb_like.png" />Like us on Facebook</a></li>
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
    </form>
</body>
</html>