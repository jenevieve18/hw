<%@ Page language="c#" Codebehind="reportSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.pm.reportSetup" %>
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
	
    <form id="reportSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0"">
			<tr><td colspan="4"><button onclick="location.href='reportSetup.aspx';">Add new report</button><button onclick="location.href='reports.aspx';">Return to list of reports</button></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><u>Report information</u></td></tr>
			<tr><td colspan="4" align="right"><asp:Button ID="Save" Text="Save" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=300/></td></tr>
			<asp:Label ID=Part Runat=server/>
			<asp:PlaceHolder ID="EditQuestionPart" Visible=false Runat=server>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="QuestionInternal" Runat=server Width=300/></td></tr>
			<tr><td>Question&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList AutoPostBack=true Width=300 Runat=server ID=QuestionIDOptionID/></td></tr>
			<tr><td>Required answer&nbsp;</td><td colspan="3">count&nbsp;<asp:TextBox Width=30 Runat=server ID="QuestionRequiredAnswerCount">10</asp:TextBox></td></tr>
			<asp:PlaceHolder Runat=server ID=QuestionText/>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><button onclick="location.href='reportSetup.aspx?ReportID=<%=reportID%>';">Cancel</button><asp:Button ID="SaveQuestionPart" Text="Save page" Runat=server/></td></tr>
			</asp:PlaceHolder>
			
			<asp:PlaceHolder ID="EditIdxPart" Visible=false Runat=server>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="IdxInternal" Runat=server Width=300/></td></tr>
			<asp:Label ID="Idxes" Runat=server/>
			<tr><td valign="top">Index(es)&nbsp;</td><td colspan="3"><asp:CheckBoxList RepeatColumns=2 Runat=server ID="IdxID"/></td></tr>
			<asp:PlaceHolder Runat=server ID="IdxText"/>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><button onclick="location.href='reportSetup.aspx?ReportID=<%=reportID%>';">Cancel</button><asp:Button ID="SaveIdxPart" Text="Save page" Runat=server/></td></tr>
			</asp:PlaceHolder>
			
			<asp:PlaceHolder ID="EditWqoPart" Visible=false Runat=server>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="WqoInternal" Runat=server Width=300/></td></tr>
			<asp:Label ID="Wqos" Runat=server/>
			<tr><td>Weighted Q(s)&nbsp;</td><td colspan="3">&nbsp;<asp:CheckBoxList Runat=server ID="WeightedQuestionOptionID"/></td></tr>
			<tr><td>Required answer&nbsp;</td><td colspan="3">count&nbsp;<asp:TextBox Width=30 Runat=server ID="WqoRequiredAnswerCount">10</asp:TextBox>&nbsp;&nbsp;Over time <asp:CheckBox ID=WqoOvertime Runat=server/> (max 2 vars)</td></tr>
			<asp:PlaceHolder Runat=server ID="WqoText"/>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><button onclick="location.href='reportSetup.aspx?ReportID=<%=reportID%>';">Cancel</button><asp:Button ID="SaveWqoPart" Text="Save page" Runat=server/></td></tr>
			</asp:PlaceHolder>
			
			<asp:PlaceHolder ID=EditPart Visible=false Runat=server>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><asp:Button ID="AddQuestionPart" Text="Add question page" Runat=server/><asp:Button ID="AddIdxPart" Text="Add index page" Runat=server/><asp:Button ID="AddWqoPart" Text="Add weight page" Runat=server/></td></tr>
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
