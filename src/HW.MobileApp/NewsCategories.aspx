<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsCategories.aspx.cs" Inherits="HW.MobileApp.NewsCategories" %>

<!DOCTYPE html>

<html>
<head>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
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
                <a href="News.aspx" data-role="button" data-icon="back">Back</a>
                <h1>&nbsp;</h1>
                <div class="segmented-control ui-bar-d">
                    <div data-role="controlgroup" data-type="horizontal">
                        <a href="#" data-role="button">International</a>
                        <a href="#" data-role="button">Swedish</a>
                    </div>
                </div>
            </div>
            <div data-role="content">
                <ul data-role="listview">
	                <li><a href="#">Books</a></li>
	                <li><a href="#">Diabetes</a></li>
	                <li><a href="#">Environment</a></li>
	                <li><a href="#">Exercise</a></li>
	                <li><a href="#">Health</a></li>
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
