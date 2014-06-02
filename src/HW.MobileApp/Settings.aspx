<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.MobileApp.Settings" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="Dashboard.aspx" data-role="button" data-icon="arrow-l">My Health</a>
                <h1>Settings</h1>
                <a href="Logout.aspx" data-role="button" class="ui-btn-right">Logout</a>
            </div>
            <div data-role="content">
                <ul data-role="listview">
	                <li>
                        <a href="ChangeProfile.aspx">
                            <h1>Change Profile</h1>
                            <p>Language, email address and personal information</p>
                        </a>
                    </li>
	                <li>
                        <a href="#">
                            <h1>Reminders</h1>
                            <p>Frequency</p>
                        </a>
                    </li>
	                <li>
                        <a href="SecuritySettings.aspx">
                            <h1>Security Settings</h1>
                            <p>Options for automatic login</p>
                        </a>
                    </li>
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
