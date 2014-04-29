<%@ Page language="c#" Codebehind="questionSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.questionSetup" validateRequest="false" %>
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
	
    <form id="questionSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0">
			<tr><td align="right">Container&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList ID=QuestionContainerID Runat=server/>&nbsp;&nbsp;List options from container&nbsp;<asp:DropDownList AutoPostBack=True ID="OptionContainerID" Runat=server/></td></tr>
			<tr><td align="right">Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=273/>&nbsp;&nbsp;Variable&nbsp;name&nbsp;<asp:TextBox ID="Variablename" Runat=server Width=120/></td></tr>
			<tr><td align="right">Font&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList ID="FontFamily" Runat=server Width=120>
			<asp:ListItem Value="0">&lt; default &gt;</asp:ListItem>
			<asp:ListItem Value="1">Tahoma</asp:ListItem>
			<asp:ListItem Value="2">Verdana</asp:ListItem>
			<asp:ListItem Value="3">Arial</asp:ListItem>
			<asp:ListItem Value="4">Courier New</asp:ListItem>
			<asp:ListItem Value="5">Times New Roman</asp:ListItem>
			</asp:DropDownList>&nbsp;&nbsp;Font&nbsp;size&nbsp;<asp:DropDownList ID="FontSize" Runat=server Width=92>
			<asp:ListItem Value="0">&lt; default &gt;</asp:ListItem>
			<asp:ListItem Value="8">8px</asp:ListItem>
			<asp:ListItem Value="9">9px</asp:ListItem>
			<asp:ListItem Value="10">10px</asp:ListItem>
			<asp:ListItem Value="11">11px</asp:ListItem>
			<asp:ListItem Value="12">12px</asp:ListItem>
			<asp:ListItem Value="13">13px</asp:ListItem>
			<asp:ListItem Value="14">14px</asp:ListItem>
			<asp:ListItem Value="16">16px</asp:ListItem>
			<asp:ListItem Value="18">18px</asp:ListItem>
			<asp:ListItem Value="20">20px</asp:ListItem>
			<asp:ListItem Value="24">24px</asp:ListItem>
			</asp:DropDownList>&nbsp;&nbsp;Text&nbsp;decoration&nbsp;<asp:DropDownList ID="FontDecoration" Runat=server Width=121>
			<asp:ListItem Value="0">&lt; default &gt;</asp:ListItem>
			<asp:ListItem Value="1">Normal</asp:ListItem>
			<asp:ListItem Value="2">Bold</asp:ListItem>
			<asp:ListItem Value="3">Italic</asp:ListItem>
			<asp:ListItem Value="4">Underlined</asp:ListItem>
			<asp:ListItem Value="5">Line-through</asp:ListItem>
			</asp:DropDownList></td></tr>
			<tr><td align="right">Separator&nbsp;</td><td colspan="3"><asp:CheckBox ID=Underlined Runat=server/>&nbsp;&nbsp;Option(s)&nbsp;placement&nbsp;<asp:DropDownList ID="OptionsPlacement" Runat=server Width=141>
			<asp:ListItem Value="1">Right-hand-side</asp:ListItem>
			<asp:ListItem Value="2">Below</asp:ListItem>
			</asp:DropDownList>&nbsp;&nbsp;Font&nbsp;color&nbsp;<asp:DropDownList ID="FontColor" Runat=server Width=149>
			<asp:ListItem Value="">&lt; default &gt;</asp:ListItem>
			<asp:ListItem Value="#000000">Black</asp:ListItem>
			<asp:ListItem Value="#006699">Blue</asp:ListItem>
			</asp:DropDownList></td></tr>
			<tr><td align="right">Box&nbsp;</td><td colspan="3"><asp:CheckBox ID="Box" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<asp:PlaceHolder ID=QuestionLang Runat=server/>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" align="right"><button onclick="location.href='questionSetup.aspx';return false;">Add new</button><button onclick="location.href='questions.aspx';return false;">Cancel</button><asp:Button ID=Save Runat=server Text="Save"/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<asp:PlaceHolder ID="Options" Runat=server/>
		</table>
     </form>
	
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>
