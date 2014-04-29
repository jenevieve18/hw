<%@ Page language="c#" Codebehind="optionSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.optionSetup" %>
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
	
    <form id="optionSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0">
			<tr><td align="right">Container&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList ID="OptionContainerID" Runat=server/>&nbsp;&nbsp;List components from container&nbsp;<asp:DropDownList ID="OptionComponentContainerID" AutoPostBack=True Runat=server/></td></tr>
			<tr><td align="right">Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=150/>&nbsp;&nbsp;Variable&nbsp;name&nbsp;<asp:TextBox ID="Variablename" Runat=server Width=100/>&nbsp;&nbsp;Inner width&nbsp;<asp:TextBox ID="InnerWidth" Runat=server Width=40/></td></tr>
			<tr><td align="right">Type&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList ID="OptionType" Runat=server Width=110>
			<asp:ListItem Value="1">Single choice</asp:ListItem>
			<asp:ListItem Value="2">Free text</asp:ListItem>
			<asp:ListItem Value="3">Multi choice</asp:ListItem>
			<asp:ListItem Value="4">Numeric</asp:ListItem>
			<asp:ListItem Value="9">VAS</asp:ListItem>
			</asp:DropDownList>&nbsp;&nbsp;Layout&nbsp;<asp:DropDownList ID="OptionPlacement" Runat=server Width=160>
			<asp:ListItem Value="1">Horizontal, labels top</asp:ListItem>
			<asp:ListItem Value="3">Horizontal, labels right</asp:ListItem>
			<asp:ListItem Value="5">Horizontal, no labels</asp:ListItem>
			<asp:ListItem Value="8">Vertical, labels right</asp:ListItem>
			</asp:DropDownList>&nbsp;&nbsp;Width&nbsp;<asp:TextBox ID="Width" Runat=server Width=40/>&nbsp;&nbsp;Height&nbsp;<asp:TextBox ID="Height" Runat=server Width=40/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<asp:PlaceHolder ID=OptionComponents Runat=server/>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" align="right"><button onclick="location.href='optionSetup.aspx';return false;">Add new</button><button onclick="location.href='options.aspx';return false;">Cancel</button><asp:Button ID=Save Runat=server Text="Save"/></td></tr>
		</table>
     </form>
	
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>
