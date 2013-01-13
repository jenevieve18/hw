<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="settings.aspx.cs" Inherits="HWgrp.settings" %>
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
                    <div class="settingsPane">
                        <asp:PlaceHolder ID=Txt runat=server>Change password</asp:PlaceHolder> <asp:TextBox ID=Password runat=server TextMode=Password /> <asp:Button ID=Save runat=server Text="Save" /> <asp:Label ID=Message runat=server />
                    </div>
                </div>

                <div class="smallContent">

                </div>
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>