<%@ Page Language="C#" Theme="" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Grp.Default" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>HealthWatch.se / Group admin</title>
	<meta http-equiv="Pragma" content="no-cache"/>
	<meta http-equiv="Expires" content="-1"/>
	<meta name="Robots" content="noarchive"/>
	<script type="text/javascript" language="JavaScript">window.history.forward(1);</script>
	<meta charset="utf-8"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>

	<link rel="stylesheet" href="css/bootstrap.css"/>
	<link rel="stylesheet" href="css/bootstrap-responsive.css"/>
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
        .i18n {
            background:url(https://healthwatch.se/includes/resources/rsaquo.gif) no-repeat 0 4px;
            padding-left: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form-signin">
    
		<img src="img/hwlogo.png" alt="HealthWatch group administrator"/>
        
        <h2>
            <%= R.Str(lid, "login.header", "HealthWatch.se<br>Group administration") %>
        </h2>
        <p style="font-size:14px"><%= HtmlHelper.Anchor(R.Str(lid, "i18n", "På svenska"), string.Format("default.aspx?lid={0}", lid == 1 ? 2 : 1), "class='i18n'")%></p>

		<% if (errorMessage != "") { %>
			<div class="alert alert-error">
				<%= errorMessage %>
			</div>
		<% } %>

		<%= FormHelper.Input("ANV", "", string.Format("class='input-block-level' placeholder='{0}'", R.Str(lid, "user.name", "Username"))) %>
		<%= FormHelper.Password("LOS", "", string.Format("class='input-block-level' placeholder='{0}'", R.Str(lid, "user.password", "Password")))%>
		
        <button class="btn btn-large btn-info" type="submit">
            <i class="icon-circle-arrow-right"></i><%= R.Str(lid, "login.signin", "Sign in") %>
        </button>
        <% if (adminNews.Count > 0) { %>
        	<hr />
            <div class="news">
                <h4><%= R.Str(lid, "news", "News") %></h4>
                <% var i = 0; %>
                <% foreach (var n in adminNews) { %>
                    <p>
                        <span class="date"><%= n.Date.Value.ToString("MMM d, yyyy").ToUpper() %></span>
                        <%= n.News %>
                    </p>
                    <% if (i < adminNews.Count - 1) { %>
                    	<hr />
                    <% } %>
                    <% i++; %>
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
