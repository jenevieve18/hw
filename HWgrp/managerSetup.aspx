<%@ Page Language="C#" AutoEventWireup="true" CodeFile="managerSetup.aspx.cs" Inherits="managerSetup" %>
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
		<%=Db2.nav()%>
            <div class="contentgroup grid_16">
                <div id="contextbar">
                    <div class="settingsPane">
			        <asp:Button CssClass="btn" ID="Cancel" runat=server Text="Cancel" />
			        <asp:Button CssClass="btn" ID="Save" runat=server Text="Save" />
			        <asp:Label ID=ErrorMsg runat=server />
                    </div>
                </div>

        <div class="smallContent">
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr>
		        <td valign="top">
		            <table border="0" cellpadding="0" cellspacing="0">
		                <tr><td colspan="2"><b>Credentials</b></td></tr>
		                <tr><td>Name&nbsp;</td><td><asp:TextBox ID="Name" Width=200 runat=server /></td></tr>
		                <tr><td>Username&nbsp;</td><td><asp:TextBox ID="Usr" Width=200 runat=server /></td></tr>
		                <tr><td>Password&nbsp;</td><td><asp:TextBox ID="Pas" TextMode=Password Width=200 runat=server /></td></tr>
		                <tr><td>Email&nbsp;</td><td><asp:TextBox ID="Email" Width=200 runat=server /></td></tr>
                        <tr><td>Organization read only&nbsp;</td><td><asp:CheckBox ID=ReadOnly runat=server /></td></tr>
        		    </table>
        		</td>
        		<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
        		<td valign="top">
		            <table border="0" cellpadding="0" cellspacing="0">
		                <tr><td><b>Roles</b></td></tr>
		                <tr><td><asp:CheckBox ID="SuperUser" Text="Super user (can administer its own manager account, including all units)" runat=server /></td></tr>
		                <tr><td><asp:CheckBoxList CellPadding=0 CellSpacing=0 ID="ManagerFunctionID" runat=server /></td></tr>
        		    </table>
        		</td>
            </tr>
		</table>
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr><td><b>Organisation access</b></td></tr>
		    <asp:Label ID=OrgTree runat=server />
		</table>
        </div>

            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>
