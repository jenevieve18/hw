<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HWgrp.Web.Default" %>
<%@ Import Namespace="HWgrp.Web" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>HealthWatch.se / Group admin</title>
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="-1">
    <meta name="Robots" content="noarchive">
    <meta charset="utf-8">
    <script language="JavaScript">window.history.forward(1);</script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0;">
    <!--<link type="text/css" rel="stylesheet" href="includes/css/960.css">
    <link type="text/css" rel="stylesheet" href="includes/css/site.css">
    <link type="text/css" rel="stylesheet" href="includes/css/admin.css">
    <link type="text/css" href="includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css" rel="Stylesheet">
    <script type="text/javascript" src="includes/ui/js/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="includes/ui/js/jquery-ui-1.8.11.custom.min.js"></script>
    <script type="text/javascript">        $(document).ready(function () { var descriptionS = $("#submenu .description").html(); $("#submenu a").mouseover(function () { $("#submenu .description").html($(this).html()); $("#submenu .active").css('background-position', 'center -80px'); }); $("#submenu a").mouseout(function () { $("#submenu .description").html(descriptionS); $("#submenu .active").css('background-position', 'center -120px'); }); });</script>-->
    <!--<link type="text/css" rel="stylesheet" href="css/bootstrap.css">-->
    <link rel="stylesheet" href="http://twitter.github.com/bootstrap/assets/css/bootstrap.css">
    <style>
        body {
            padding-top: 40px;
            padding-bottom: 40px;
            background-color: #f5f5f5;
        }
        .form-signin {
            max-width: 300px;
            padding: 19px 29px 29px;
            margin: 0 auto 20px;
            background-color: #fff;
            border: 1px solid #e5e5e5;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            -webkit-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            -moz-box-shadow: 0 1px 2px rgba(0,0,0,.05);
            box-shadow: 0 1px 2px rgba(0,0,0,.05);
        }
        .form-signin input[type="text"], .form-signin input[type="password"] {
            font-size: 16px;
            height: auto;
            margin-bottom: 15px;
            padding: 7px 9px;
        }
        .form-signin h2  {
            padding-top:0;
            padding-bottom:10px;
            font:20px Arial;
        }
    </style>
</head>
<body>
    <form id="Form1" method="post" runat="server" class="form-signin">
        <img src="img/hwlogo.png" alt="HealthWatch group administrator">
        <h2><%= R.Str("login.header", "HealthWatch.se<br>Group administration") %></h2>
		<%= HtmlHelper.Input("ANV", "", "input-block-level", R.Str("user.name", "Username")) %>
		<%= HtmlHelper.Password("LOS", "", "input-block-level", R.Str("user.password", "Password")) %>
        <button class="btn btn-large btn-info" type="submit"><i class="icon-circle-arrow-right"></i>Sign in</button>
    </form>
	<% if (errorMessage != "") { %>
		<div class="alert alert-error">
			<%= errorMessage %>
		</div>
	<% } %>

    <script src="js/bootstrap.js"></script>
</body>
</html>
