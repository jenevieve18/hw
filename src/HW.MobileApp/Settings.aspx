﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.MobileApp.Settings" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="Stylesheet" href="css/custom.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="Dashboard.aspx" data-role="button" data-icon="arrow-l"><%= R.Str("home.myHealth") %></a>
                <h1><%= R.Str("settings.title") %></h1>
                <a href="Logout.aspx" data-role="button" class="ui-btn-right" rel="external"><%= R.Str("home.logout") %></a>
            </div>
            <div data-role="content">
                <ul data-role="listview">
	                <li>
                        <a href="ChangeProfile.aspx" rel="external">
                            <h1><%= R.Str("settings.changeProfile")%></h1>
                            <p><%= R.Str("settings.changeProfile.description")%></p>
                        </a>
                    </li>
	                <li>
                        <a href="Reminders.aspx" rel="external">
                            <h1><%= R.Str("settings.reminders")%></h1>
                            <p><%= R.Str("settings.reminders.description")%></p>
                        </a>
                    </li>
	                <li>
                        <a href="SecuritySettings.aspx"  rel="external">
                            <h1>Security Settings</h1>
                            <p>Options for automatic login</p>
                        </a>
                    </li>
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
