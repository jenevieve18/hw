<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyTermsThanks.aspx.cs" Inherits="HW.Invoicing.CompanyTermsThanks" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!DOCTYPE html>

<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>IHG Invoicing</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

	<!--link rel="stylesheet/less" href="less/bootstrap.less" type="text/css" /-->
	<!--link rel="stylesheet/less" href="less/responsive.less" type="text/css" /-->
	<!--script src="js/less-1.3.3.min.js"></script-->
	<!--append ‘#!watch’ to the browser URL, then refresh the page. -->
	
	<link href="css/bootstrap.min.css" rel="stylesheet">
	<link href="css/style.css" rel="stylesheet">

	<!--<link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>-->

	<style type="text/css">
	    body {
		    padding-top:30px;
	    }
	     h1, h2, h3, h4, h5, h6, .h1, .h2, .h3, .h4, .h5, .h6 {
	 	    font-family: 'Open Sans', sans-serif;
	     }
	    footer {
		    margin-top:40px;
		      background: #333;
		      color: #eee;
		      font-size: 11px;
		      padding:20px 0px;
	    }
	    footer ul 
	    {
	        padding-left: 20px;
	    }
	</style>

    <style type="text/css">
        table { border-collapse: collapse; empty-cells: show; }

        td { position: relative; }

        tr.strikeout td:before {
            content: " ";
            position: absolute;
            top: 50%;
            left: 0;
            border-bottom: 1px solid red;
            width: 100%;
        }

        tr.strikeout td:after {
            content: "\00B7";
            font-size: 1px;
        }

        /* Extra styling */
        td { width: 100px; }
        th { text-align: left; }
    </style>

    <!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
        <script src="js/html5shiv.js"></script>
    <![endif]-->

    <!-- Fav and touch icons -->
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="img/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="img/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="img/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="img/apple-touch-icon-57-precomposed.png">
    <!--<link rel="shortcut icon" href="img/favicon.png">-->
  
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/scripts.js"></script>
    <style type="text/css">
        .label-width 
        {
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
    
        <img src="uploads/<%= company.InvoiceLogo %>" />

        <h3>Thank you!</h3>
        <p>Your contract has been sent to your email! Click <%= HtmlHelper.Anchor("here", "http://wwww.healthwatch.se") %> to go to our official website.</p>

    </div>
    </form>
</body>
</html>
