<%@ Page language="c#" Codebehind="managerSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.managerSetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>eForm Administration</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel=stylesheet type=text/css href="survey.css">
  </head>
	<body bgcolor="#F3F3F3" background="../img/bg/1.jpg" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" style="background-repeat: no-repeat">
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="text">
	<tr>
		<td><img src="../img/null.gif" width="1" height="78"></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td><div align="right"><img src="../img/logo.gif" width="125" height="51"><img src="../img/null.gif" width="30" height="1"></div></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr> 
		<td><img src="../img/null.gif" width="1" height="16"></td>
		<td>&nbsp;</td>
		<td background="../img/topNav_bgrL.gif">&nbsp;</td>
		<td colspan="3" background="../img/topNav_bgr.gif" class="graySmall"><img src="../img/arrow_topNavN.gif" width="5" height="7">&nbsp;eForm Administration</span></td>
	</tr>
	<tr> 
		<td width="185" height="100%" valign="top">
			<%=eform.Db.printNav()%>
		</td>
		<td width="10"><img src="../img/null.gif" width="6" height="1"></td>
		<td width="25" bgcolor="#FFFFFF" class="mainLeft"><img src="../img/null.gif" width="20" height="1"></td>
		<td width="780" valign="top" bgcolor="#FFFFFF">
		<br/>
		
    <form id="managerSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0"">
			<tr><td colspan="4"><button onclick="location.href='managerSetup.aspx';">Add new manager</button><button onclick="location.href='managers.aspx';">Return to list of managers</button></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><u>Manager information</u></td></tr>
			<tr><td colspan="4" align="right"><asp:Button ID="Save" Text="Save" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Name&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Name Runat=server Width=300/></td></tr>
			<tr><td>Email&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="Email" Runat=server Width=300/></td></tr>
			<tr><td>Password&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="Password" Runat=server Width=300/></td></tr>
			<tr><td>Phone&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="Phone" Runat=server Width=300/></td></tr>
			<asp:PlaceHolder ID=ProjectRound Runat=server Visible=False>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>ProjectRound&nbsp;</td><td colspan="3"><asp:DropDownList AutoPostBack=true ID="ProjectRoundID" Runat=server/>&nbsp;&nbsp;ShowCompleteOrg&nbsp;<asp:CheckBox ID=ShowCompleteOrg Runat=server/></td></tr>
			<tr><td valign="top">Units&nbsp;</td><td colspan="3"><asp:CheckBoxList RepeatLayout=Table RepeatDirection=Vertical RepeatColumns=1 ID="ProjectRoundUnitID" Runat=server/></td></tr>
			</asp:PlaceHolder>
		</table>
     </form>
	
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>