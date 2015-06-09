<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HW.Invoicing.Default" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="Interactive Health Group">

    <!-- Le styles -->
    <!--<link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap.css" rel="stylesheet">-->
    <!--<link href="http://getbootstrap.com/2.3.2/assets/css/bootstrap-responsive.css" rel="stylesheet">-->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
        .form-signin .form-signin-heading,
        .form-signin .checkbox {
            margin-bottom: 10px;
        }
        .form-signin input[type="text"],
        .form-signin input[type="password"] {
            font-size: 16px;
            height: auto;
            margin-bottom: 15px;
            padding: 7px 9px;
        }

    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
        <script src="../assets/js/html5shiv.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="../assets/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="../assets/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="../assets/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="../assets/ico/apple-touch-icon-57-precomposed.png">
    <link rel="shortcut icon" href="../assets/ico/favicon.png">
  </head>
<body>
    <div class="container">
        <form id="form1" runat="server" class="form-signin">
            <h2 class="form-signin-heading">Please sign in</h2>
            <% if (errorMessage != null && errorMessage != "") { %>
                <div class="alert alert-danger">
	                <%= errorMessage %>
                </div>
            <% } %>
            <asp:TextBox CssClass="input-block-level" placeholder="User name" ID="textBoxName" runat="server"></asp:TextBox>
            <asp:TextBox CssClass="input-block-level" ID="textBoxPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
            <label class="checkbox">
                <asp:CheckBox ID="CheckBoxRememberMe" runat="server" /> Remember me
            </label>
            <asp:Button ID="buttonLogin" CssClass="btn btn-large btn-primary" runat="server" 
                Text="Sign in" onclick="buttonLogin_Click" />
        </form>
    </div>
</body>
</html>
