<%@ Page ValidateRequest=false language="c#" Codebehind="optionComponentSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.optionComponentSetup" %>
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
	
    <form id="optionComponentSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0">
			<tr><td align="right">Container&nbsp;</td><td>&nbsp;<asp:DropDownList ID="OptionComponentContainerID" Runat=server/></td></tr>
			<tr><td>Internal&nbsp;</td><td>&nbsp;<asp:TextBox ID=Internal Runat=server Width=200/>&nbsp;&nbsp;Export&nbsp;value&nbsp;<asp:TextBox ID="ExportValue" Runat=server Width=50/></td></tr>
			<asp:PlaceHolder ID=Lang Runat=server/>
			<tr><td colspan="2" align="right"><button onclick="location.href='optionComponentSetup.aspx';return false;">Add new</button><button onclick="location.href='optionComponents.aspx';return false;">Cancel</button><asp:Button ID=Save Runat=server Text="Save"/></td></tr>
		</table>
     </form>
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>
