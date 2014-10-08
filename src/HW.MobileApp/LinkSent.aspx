<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkSent.aspx.cs" Inherits="HW.MobileApp.LinkSent" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
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
    <div data-role="page" id="forgotpass">
            <div data-role="header" data-theme="b" data-position="fixed">
                <h1>&nbsp;</h1>
            </div>
            <div data-role="content">
                <div class="front_note center">
                    <img class="front_logo" src="images/hw_logo@2x.png" />
                    <div class="front_controls">
                        <div class="front_header">
                            <h4 ><%= R.Str("link.sent") %></h4>
                            <img class="front_header_img" src="images/divider.gif">
                        <p><%= R.Str("link.message") %></p>
                        </div>
                        <fieldset data-role="controlgroup">
                            <a href="Login.aspx" data-role="button" ><%= R.Str("button.backToLogin") %></a>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health" rel="external"><%= R.Str("home.myHealth") %></a></li>
                        <li><a href="News.aspx" data-icon="news" rel="external"><%= R.Str("home.news") %></a></li>
                        <li><a href="More.aspx" data-icon="more" rel="external"><%= R.Str("home.more") %></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
