<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="HW.MobileApp.News" %>

<!DOCTYPE html>
<html class="ui-mobile">
<head>
    <base href="http://clients.easyapp.se/healthwatch//news/">
    <title>Health Watch</title>
    <meta http-equiv="content-type" content="text/html;charset=UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link href="http://clients.easyapp.se/healthwatch//css/custom_UI.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/reset.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/style.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/main.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/dashboard.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/start.css" rel="stylesheet" type="text/css">
    <link href="http://clients.easyapp.se/healthwatch//css/form.css" rel="stylesheet" type="text/css">
    <script src="http://clients.easyapp.se/healthwatch//js/jquery-1.8.3.min.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/mobileinit.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/main.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.mobile-1.2.0.min.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.xml2json.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.cookie.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/mustache.min.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/moment.min.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/moment_sv.js"></script>
    <link href="http://clients.easyapp.se/healthwatch//js/mobiscroll/css/mobiscroll.datetime-2.4.min.css" rel="stylesheet" type="text/css">
    <script src="http://clients.easyapp.se/healthwatch//js/mobiscroll/mobiscroll.datetime-2.4.min.js"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.flot.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.flot.time.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.flot.dashes.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jquery.flot.symbol.js" type="text/javascript"></script>
    <script src="http://clients.easyapp.se/healthwatch//js/jQuery.XDomainRequest.js" type="text/javascript"></script>
</head>
<body class="ui-mobile-viewport ui-overlay-c landscape" style="display: block;">
    <div id="wrapper" data-role="page" data-pagekey="news" data-url="wrapper" tabindex="0" class="ui-page ui-body-c ui-page-active" style="padding-top: 0px; padding-bottom: 0px; min-height: 303px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner">
            <h1 class="ui-title" role="heading" aria-level="1">All News</h1>
            <a href="#" id="remindersSaveButton" class="navbarBtn nav_button cat_button ui-btn-left ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-a" data-inline="true" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="a"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Categories</span></span></a>
        </div>
        <div id="content" data-role="content" class="page_news ui-content" role="main">
            <ul data-role="listview" class="news-listview ui-listview">
            </ul>
            <input type="hidden" class="tabbar_name" value="news">
        </div>
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo">
            <ul class="tabbar">
                <li class="my_health">
                    <a href="#" class="tabbar_link ui-link">
                        <div class="tabbar_text">My Health</div>
                    </a>
                    <div class="tabbar_mask"></div>
                </li>
                <li class="news selected">
                    <a href="#" class="tabbar_link ui-link">
                        <div class="tabbar_text">News</div>
                    </a>
                    <div class="tabbar_mask"></div>
                </li>
                <li class="more">
                    <a href="#" class="tabbar_link ui-link">
                        <div class="tabbar_text">More</div>
                    </a>
                    <div class="tabbar_mask"></div>
                </li>
            </ul>
        </div>
    </div>
    <div class="ui-loader ui-corner-all ui-body-a ui-loader-default">
        <span class="ui-icon ui-icon-loading"></span>
        <h1>loading</h1>
    </div>
</body>
</html>
