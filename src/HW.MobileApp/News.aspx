<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="HW.MobileApp.News" %>

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
                <h1>All News</h1>
                <a href="#" data-role="button" class="ui-btn-right">Categories</a>
            </div>
            <div data-role="content">
                <ul data-role="listview">
	                <li><a href="#">Home walking program may help clogged leg arteries</a></li>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="#" data-icon="home" class="ui-btn-active">My Health</a></li>
                        <li><a href="News.aspx" data-icon="calendar">News</a></li>
                        <li><a href="More.aspx" data-icon="bullets">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
