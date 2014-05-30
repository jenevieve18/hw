<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.MobileApp.Settings" %>
<%@ Register Src="~/Footer.ascx" TagName="Footer" TagPrefix="ft" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
	                <li><a href="#">Change Profile</a></li>
	                <li><a href="#">Reminders</a></li>
	                <li><a href="#">Security Settings</a></li>
                </ul>
            </div>
            <ft:Footer runat="server" />
        </div>
    </form>
</body>
</html>
