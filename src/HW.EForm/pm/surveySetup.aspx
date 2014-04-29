<%@ Page validaterequest=false language="c#" Codebehind="surveySetup.aspx.cs" AutoEventWireup="false" Inherits="eform.surveySetup" %>
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
		
		    <form id="surveySetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0">
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=150/>&nbsp;Bottom&nbsp;text&nbsp;<asp:TextBox ID="Copyright" Runat=server Width=150/>&nbsp;List questions from <asp:DropDownList ID=QuestionContainerID AutoPostBack=True Runat=server/></td></tr>
			<tr><td>Languages&nbsp;</td><td colspan="3">&nbsp;<asp:CheckBoxList CellPadding=0 CellSpacing=0 Runat=server RepeatDirection=Horizontal ID=LangID RepeatLayout=Flow />&nbsp;&nbsp;&nbsp;Clear questions <asp:CheckBox ID=ClearQuestions Runat=server/>&nbsp;&nbsp;&nbsp;Flip flop background <asp:CheckBox ID="FlipFlopBg" Runat=server/>&nbsp;&nbsp;&nbsp;No time <asp:CheckBox ID="NoTime" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><button onclick="location.href='surveySetup.aspx';">Add new</button><button onclick="location.href='surveys.aspx';">Cancel</button><asp:Button ID=Move Runat=server Text="Move up"/><asp:TextBox ID=MoveStep Runat=server Width=50>1</asp:TextBox><asp:Button ID="Export" Runat=server Text="Export"/><asp:Button ID=Save Runat=server Text="Save"/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<asp:PlaceHolder ID="Questions" Runat=server/>
		</table>
     </form>
	
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>
