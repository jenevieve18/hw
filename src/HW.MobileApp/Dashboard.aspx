<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HW.MobileApp.Dashboard" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>
    
    <style>
        .front_logo 
        {
            
width: 180px;
height: 126px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1></h1>
                <a href="Settings.aspx" data-role="button" data-icon="gear" class="ui-btn-right" data-iconpos="notext"></a>
            </div>
            <div data-role="content">
                <img class="front_logo" src="http://clients.easyapp.se/healthwatch/images/hw_logo@2x.png" />
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