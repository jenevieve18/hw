<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsCategories.aspx.cs" Inherits="HW.MobileApp.NewsCategories" %>

<!DOCTYPE html>

<html>
<head>
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
                <a href="News.aspx" data-role="button" data-icon="back">Back</a>
                <h1>&nbsp;</h1>
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
            <ft:Footer ID="Footer1" runat="server" />
        </div>
    </form>
</body>
</html>
