<%@ Page language="c#" Codebehind="idxSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.idxSetup" %>
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
	
    <form id="idxSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0"">
			<tr><td colspan="4"><button onclick="location.href='idxSetup.aspx';">Add new index</button><button onclick="location.href='idx.aspx';">Return to list of indexes</button></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><u>Index information</u></td></tr>
			<tr><td colspan="4" align="right"><asp:Button ID="Save" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=203/>&nbsp;Target value&nbsp;<asp:TextBox ID="TargetVal" Runat=server Width=30/></td></tr>
			<tr><td valign="top">Description&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox TextMode=MultiLine ID="Description" Runat=server Width=400/></td></tr>
			<tr>
				<td>Color range&nbsp;</td>
				<td>
					<table border="0" cellspacing="0" cellpadding="0" width="300" style="background:URL(../img/colorRange.gif) repeat-y;">
						<tr>
							<td><asp:TextBox ID="RedLow" ReadOnly Font-Size=8px Runat=server Width=20>0</asp:TextBox></td>
							<td><img src="../img/null.gif" width="30" height="1"/></td>
							<td><asp:TextBox ID="YellowLow" Font-Size=8px Runat=server Width=20/></td>
							<td><img src="../img/null.gif" width="40" height="1"/></td>
							<td><asp:TextBox ID="GreenLow" Font-Size=8px Runat=server Width=20/></td>
							<td><img src="../img/null.gif" width="40" height="1"/></td>
							<td><asp:TextBox ID="GreenHigh" Font-Size=8px Runat=server Width=20/></td>
							<td><img src="../img/null.gif" width="40" height="1"/></td>
							<td><asp:TextBox ID="YellowHigh" Font-Size=8px Runat=server Width=20/></td>
							<td><img src="../img/null.gif" width="30" height="1"/></td>
							<td><asp:TextBox ID="RedHigh" ReadOnly Font-Size=8px Runat=server Width=20>100</asp:TextBox></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr><td>Require all&nbsp;</td><td colspan="3">parts to be completed&nbsp;<asp:CheckBox ID="AllPartsRequired" Runat=server/>&nbsp;&nbsp;Required answer count&nbsp;<asp:TextBox ID="RequiredAnswerCount" Runat=server Width=55/></td></tr>
			<asp:PlaceHolder ID=Text Runat=server/>
			<asp:PlaceHolder ID=Part Runat=server/>
			<asp:PlaceHolder ID=NewPart Visible=false Runat=server>
				<tr><td colspan="4">&nbsp;</td></tr>
				<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
				<tr><td colspan="4">&nbsp;</td></tr>
				<tr><td>Add part&nbsp;</td><td colspan="3"><asp:DropDownList ID="QuestionID" AutoPostBack=True Runat=server><asp:ListItem Value="0">&lt; none &gt;</asp:ListItem></asp:DropDownList><asp:DropDownList ID="OptionID" Runat=server/></td></tr>
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
