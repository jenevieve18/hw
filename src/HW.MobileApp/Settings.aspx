<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.MobileApp.Settings" %>

<!DOCTYPE html>
<html class="ui-mobile">
<head>
    <base href="http://clients.easyapp.se/healthwatch//settings/index.html">
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
<body class="ui-mobile-viewport landscape ui-overlay-c" style="display: block;">
    <div id="wrapper" data-role="page" data-pagekey="login" data-url="wrapper" tabindex="0" class="ui-page ui-body-c" style="padding-top: 0px; padding-bottom: 0px; min-height: 616px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner" style="position: fixed;">
            <h1 class="ui-title" role="heading" aria-level="1"></h1>
        </div>
        <div id="content" data-role="content" class="page_login ui-content" role="main">
            <div class="front_note center" id="loginContent" style="">
                <img class="front_logo" src="http://clients.easyapp.se/healthwatch//images/hw_logo@2x.png">
                <div class="front_controls">
                    <div class="front_header">
                        <h1 class="text">Login to a better life.</h1>
                        <h1 class="text error" style="display: none;">Sorry, incorrect login details.</h1>
                        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch//images/divider.gif">
                    </div>
                    <input id="loginUsername" type="text" placeholder="Username or Email" class="ui-input-text ui-body-c ui-corner-all ui-shadow-inset">
                    <label id="loginUsernameLabel">Username or Email</label>
                    <input id="loginPassword" type="password" placeholder="Password" class="ui-input-text ui-body-c ui-corner-all ui-shadow-inset">
                    <label id="loginPasswordLabel">Password</label>
                    <div id="loginLoader" style="display: none;"></div>
                    <div id="loginButton" class="button button35" style="display: block;">
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
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo" style="position: fixed;">
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
    <div id="wrapper" data-role="page" data-pagekey="news" data-url="/healthwatch//news" data-external-page="true" tabindex="0" class="ui-page ui-body-c" style="padding-top: 0px; padding-bottom: 0px; min-height: 616px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner" style="position: fixed;">
            <h1 class="ui-title" role="heading" aria-level="1">All News</h1>
            <a href="#" id="remindersSaveButton" class="navbarBtn nav_button cat_button ui-btn-left ui-btn ui-btn-up-a ui-shadow ui-btn-corner-all ui-btn-inline" data-inline="true" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="a"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Categories</span></span></a>
        </div>
        <div id="content" data-role="content" class="page_news ui-content" role="main">
            <ul data-role="listview" class="news-listview ui-listview"></ul>
            <input type="hidden" class="tabbar_name" value="news">
        </div>
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo" style="position: fixed;">
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
    <div id="wrapper" data-role="page" data-pagekey="more" data-url="/healthwatch//more" data-external-page="true" tabindex="0" class="ui-page ui-body-c" style="padding-top: 0px; padding-bottom: 0px; min-height: 616px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner" style="position: fixed;">
            <h1 class="ui-title" role="heading" aria-level="1">More</h1>
        </div>
        <div id="content" data-role="content" class="page_more ui-content" role="main">
            <ul class="listview moreListView ui-listview" data-role="listview">
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <img class="left_image" src="http://clients.easyapp.se/healthwatch//images/about_hw.png">
                    <a class="listview_item ui-link-inherit" href="http://clients.easyapp.se/healthwatch//more/about_us.html">
                        About HealthWatch</a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <img class="left_image" src="http://clients.easyapp.se/healthwatch//images/support.png">
                    <a class="listview_item ui-link-inherit" href="http://clients.easyapp.se/healthwatch//more/support.html">
                        Support</a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <img class="left_image" src="http://clients.easyapp.se/healthwatch//images/contact_us.png">
                    <a class="listview_item ui-link-inherit" href="http://clients.easyapp.se/healthwatch//more/contact_us.html">
                        Contact Us</a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <img class="left_image" src="http://clients.easyapp.se/healthwatch//images/tell_a_friend.png">
                    <a class="listview_item ui-link-inherit" href="mailto:support@healthwatch.se?Subject=Hello">
                        Tell a Friend</a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" id="fb_like" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-li-last ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <img class="left_image" src="http://clients.easyapp.se/healthwatch//images/fb_like.png">
                    <a class="listview_item ui-link-inherit" target="_blank" href="https://facebook.com/healthwatch.se">
                        Like us on Facebook</a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
            </ul>
            <input type="hidden" class="tabbar_name" value="more">
        </div>
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo" style="position: fixed;">
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
    <div id="wrapper" data-role="page" data-pagekey="my_health" data-url="/healthwatch//my_health/index.html" data-external-page="true" tabindex="0" class="ui-page ui-body-c" style="padding-top: 0px; padding-bottom: 0px; min-height: 616px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner" style="position: fixed;">
            <h1 class="ui-title" role="heading" aria-level="1"></h1>
            <a id="mh_settings" href="#" class="navbarBtn ui-btn-left ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-a" data-inline="true" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="a"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">
                <img class="gearButton" src="http://clients.easyapp.se/healthwatch//images/btn_icon_settings@2x.png">
            </span></span></a>
        </div>
        <div id="content" data-role="content" class="page_my_health ui-content" role="main">
            <div id="dashboardContent">
                <div id="logoP">
                    <div id="hwLogo">
                    </div>
                </div>
                <div class="iconsContainer" style="width: 540px;">
                    <a id="mh_form" href="#" class="ui-link">
                        <div class="icon form">
                            Form
                        </div>
                    </a>
                    <a id="mh_statistics" href="#" class="ui-link">
                        <div class="icon stats">
                            Statistics
                        </div>
                    </a>
                    <a id="mh_calendar" href="#" class="ui-link">
                        <div class="icon cal">
                            Diary
                        </div>
                    </a>
                    <a id="mh_exercises" href="#" class="ui-link">
                        <div class="icon exer">
                            Exercises
                        </div>
                    </a>
                    <div style="clear:both;"></div>
                </div>
            </div>
            <input type="hidden" class="tabbar_name" value="my_health">
        </div>
        <div id="footer" data-role="footer" data-position="fixed" class="ui-footer ui-bar-a ui-footer-fixed slideup" role="contentinfo" style="position: fixed;">
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
    <div class="ui-loader ui-corner-all ui-body-a ui-loader-default"><span class="ui-icon ui-icon-loading"></span><h1>loading</h1></div><div id="wrapper" data-role="page" data-pagekey="settings" data-url="/healthwatch//settings/index.html" data-external-page="true" tabindex="0" class="ui-page ui-body-c ui-page-active" style="padding-top: 0px; padding-bottom: 0px; min-height: 429px;">
        <div id="header" data-role="header" class="header ui-header ui-bar-a ui-header-fixed slidedown" data-position="fixed" role="banner">
            <a id="my_health_btn" class="backBtn ui-btn-left ui-btn ui-btn-up-a ui-shadow ui-btn-corner-all ui-btn-inline" data-inline="true" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="a"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">
                My Health</span></span></a>
            <h1 class="ui-title" role="heading" aria-level="1">Settings</h1>
            <a href="#" class="navbarBtn ui-btn-right ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-a" data-inline="true" onclick="$.hw.api.user.logout(true)" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" data-theme="a"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Logout</span></span></a>
        </div>
        <div id="content" data-role="content" class="page_settings ui-content" role="main">
            <ul class="listview settingsListView ui-listview" data-role="listview">
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <a id="change_profile" href="#" class="ui-link-inherit">
                        Change Profile
                        <div class="subtitle">Language, email address and personal information</div>
                    </a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <a id="reminders" href="#" class="ui-link-inherit">
                        Reminders
                        <div class="subtitle">Frequency</div>
                    </a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
                <li data-icon="false" data-corners="false" data-shadow="false" data-iconshadow="true" data-wrapperels="div" data-iconpos="right" data-theme="c" class="ui-btn ui-btn-icon-right ui-li ui-li-last ui-btn-up-c"><div class="ui-btn-inner ui-li"><div class="ui-btn-text">
                    <a id="security_settings" href="#" class="ui-link-inherit">
                        Security Settings
                        <div class="subtitle">Options for automatic login</div>
                    </a>
                    <img class="right_image" src="http://clients.easyapp.se/healthwatch//images/lv_right_img.png">
                </div></div></li>
            </ul>
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
</body>
</html>
