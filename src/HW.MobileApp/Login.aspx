<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.MobileApp.Login" %>

<!DOCTYPE html>
<html class="ui-mobile">
<head>
    <base href="http://clients.easyapp.se/healthwatch//account/login.html">
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
    <div id="wrapper" data-role="page" data-pagekey="login" data-url="wrapper" tabindex="0" class="ui-page ui-body-c ui-page-active" style="padding-top: 0px; padding-bottom: 0px; min-height: 429px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner">
            <h1 class="ui-title" role="heading" aria-level="1"></h1>
        </div>
        <div id="content" data-role="content" class="page_login ui-content" role="main">
            <div class="front_note center" id="loginContent" style="">
                <img class="front_logo" src="http://clients.easyapp.se/healthwatch//images/hw_logo@2x.png">
                <div class="front_controls">
                    <div class="front_header">
                        <h1 class="text">Login to a better life.</h1>
                        <h1 class="text error">Sorry, incorrect login details.</h1>
                        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch//images/divider.gif">
                    </div>
                    <input id="loginUsername" type="text" placeholder="Username or Email" class="ui-input-text ui-body-c ui-corner-all ui-shadow-inset">
                    <label id="loginUsernameLabel">Username or Email</label>
                    <input id="loginPassword" type="password" placeholder="Password" class="ui-input-text ui-body-c ui-corner-all ui-shadow-inset">
                    <label id="loginPasswordLabel">Password</label>
                    <div id="loginLoader"></div>
                    <div id="loginButton" class="button button35">
                        <div class="buttonContent no_icon_btn">Log In</div>
                    </div>
                    <div class="forgotButton button button35">
                        <a href="./forgot_password.html" class="ui-link">
                            <div class="buttonContent no_icon_btn">Forgot Password</div>
                        </a>
                    </div>
                </div>
            </div>
            <input type="hidden" class="tabbar_name" value="my_health">
        </div>
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo">
            <ul class="tabbar">
                <li class="my_health selected">
                    <a href="#" class="tabbar_link ui-link">
                        <div class="tabbar_text">My Health</div>
                    </a>
                    <div class="tabbar_mask"></div>
                </li>
                <li class="news">
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
        <span class="ui-icon ui-icon-loading"></span><h1>loading</h1>
    </div>
</body>
</html>
