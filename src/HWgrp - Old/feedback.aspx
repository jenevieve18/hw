﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="feedback" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db2.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
        <div class="container_16" id="admin">
            <div style="margin:20px;">
		        <asp:Label ID=List runat=server/><br /><br />
            </div>
        </div> <!-- end .container_12 -->
        
	</form>
  </body>
</html>
