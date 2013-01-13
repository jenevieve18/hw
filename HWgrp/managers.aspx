<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managers.aspx.cs" Inherits="HWgrp.managers" %>

<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
		<div class="container_16" id="admin">
		<%=Db2.nav()%>
            <div class="contentgroup grid_16">
                <div id="contextbar">
                    <div class="actionPane2">
                    <div class="bottom" id=ActionNav runat=server>
                        <a class="add-user" href="managerSetup.aspx">Add manager</a>
                    </div>
                    </div>
                </div>
                <div class="smallContent">
				<br />
                <table border="0" cellpadding="0" cellspacing="0">
		            <asp:Label ID=Managers runat=server/>
		        </table>
                </div>
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>

