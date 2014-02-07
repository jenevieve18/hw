<%@ Page Language="C#" Theme="" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Grp.Default" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>HealthWatch.se / Group admin</title>
	<meta http-equiv="Pragma" content="no-cache">
	<meta http-equiv="Expires" content="-1">
	<meta name="Robots" content="noarchive">
	<script language="JavaScript">window.history.forward(1);</script>
	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<!--
	<link type="text/css" rel="stylesheet" href="includes/css/960.css">
	<link type="text/css" rel="stylesheet" href="includes/css/site.css">
	<link type="text/css" rel="stylesheet" href="includes/css/admin.css">
	<link type="text/css" href="includes/ui/css/ui-lightness/jquery-ui-1.8.11.custom.css" rel="Stylesheet">
	<script type="text/javascript" src="includes/ui/js/jquery-1.5.1.min.js"></script>
	<script type="text/javascript" src="includes/ui/js/jquery-ui-1.8.11.custom.min.js"></script>
	<script type="text/javascript">		$(document).ready(function () { var descriptionS = $("#submenu .description").html(); $("#submenu a").mouseover(function () { $("#submenu .description").html($(this).html()); $("#submenu .active").css('background-position', 'center -80px'); }); $("#submenu a").mouseout(function () { $("#submenu .description").html(descriptionS); $("#submenu .active").css('background-position', 'center -120px'); }); });</script>
	-->

	<link rel="stylesheet" href="~/css/bootstrap.css">
	<link rel="stylesheet" href="~/css/bootstrap-responsive.css">
    <style type="text/css">
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
        .footer {
            max-width:300px;
            margin:auto;
        }
        .news {
            max-width:300px;
            margin:auto;
        }
        .news .date {
            color:#999999;
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
    <form id="form1" runat="server" class="form-signin">
    
		<!--<div class="container_16" id="admin">
			<div class="headergroup grid_16">
				<div class="grid_3 alpha">
					<img src="img/hwlogo.png" width="186" height="126" alt="HealthWatch group administrator">
				</div>
				<div class="grid_8 omega p2">HealthWatch.se<br>Group administration<br></div><br>
				<table border="0" cellspacing="0" cellpadding="0">
					<tbody>
						<tr><td>Username&nbsp;</td><td><input type="text" name="ANV">&nbsp;</td></tr>
						<tr><td>Password&nbsp;</td><td><input type="password" name="LOS">&nbsp;</td><td><input type="submit" value="OK"></td></tr>
					</tbody>
				</table>
			</div>
		</div>-->

		<img src="img/hwlogo.png" alt="HealthWatch group administrator">
        
        <h2><%= R.Str("login.header", "HealthWatch.se<br>Group administration") %></h2>

		<% if (errorMessage != "") { %>
			<div class="alert alert-error">
				<%= errorMessage %>
			</div>
		<% } %>

		<%= FormHelper.Input("ANV", "", R.Str("user.name", "Username"), "input-block-level") %>
		<%= FormHelper.Password("LOS", "", R.Str("user.password", "Password"), "input-block-level")%>
		
        <button class="btn btn-large btn-info" type="submit">
            <i class="icon-circle-arrow-right"></i><%= R.Str("login.signin", "Sign in") %>
        </button>
        <% if (adminNews.Count > 0) { %>
        <div class="news">
            <h4>News</h4>
            <% foreach (var n in adminNews) { %>
                <p>
                    <span class="date"><%= n.Date.Value.ToString("MMM d, yyyy")%></span>
                    <%= n.News %>
                </p>
                <hr />
            <% } %>
        </div>
        <% } %>
    </form>
    <div class="footer">
        &copy; Interactive Health Group <%= DateTime.Now.ToString("yyyy") %><br />
        Version <%= typeof(Default).Assembly.GetName().Version%>
    </div>

</body>
</html>
