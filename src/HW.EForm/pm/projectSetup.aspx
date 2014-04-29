<%@ Page ValidateRequest="false" language="c#" Codebehind="projectSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.projectSetup" %>
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
					<div style="position:absolute;left:700;top:185;"><asp:Label ID=LogoImg Runat=server/></div>
					<br/>
					<form id="projectSetup" method="post" runat="server">
						<table border="0" cellspacing="0" cellpadding="0">
							<tr><td colspan="4"><button onclick="location.href='projectSetup.aspx';">Add new project</button><button onclick="location.href='projects.aspx';return false;">Return to list of projects</button></td></tr>
							<tr><td colspan="4">&nbsp;</td></tr>
							<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
							<tr><td colspan="4">&nbsp;</td></tr>
							<tr><td colspan="4"><u>Project specific</u></td></tr>
							<tr><td colspan="4">&nbsp;</td></tr>
							<tr><td>Internal&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID=Internal Runat=server Width=300/></td></tr>
							<tr><td>Name&nbsp;</td><td colspan="3">&nbsp;<asp:TextBox ID="Name" Runat=server Width=300/></td></tr>
							<tr><td>Logo&nbsp;</td><td colspan="3">&nbsp;<input type="file" ID="Logo" Runat=server style="Width:300"/></td></tr>
							<tr><td valign="top"><asp:Label ID="ProjectSurveyText" Runat=server/></td><td colspan="3"><asp:Label ID="ProjectSurvey" Runat=server/></td></tr>
							<tr><td>Add survey&nbsp;</td><td colspan="3">&nbsp;<asp:DropDownList ID="SurveyID" Runat=server Width=300><asp:ListItem Value="0">&lt; select &gt;</asp:ListItem></asp:DropDownList></td></tr>
							<tr><td colspan="4">&nbsp;</td></tr>
							<tr><td colspan="4"><asp:Button ID=Save Runat=server Text="Save"/></td></tr>
							<asp:PlaceHolder ID="Rounds" Runat=server/>
							<tr><td colspan="4"><table border="0" cellspacing="0" cellpadding="0"><tr><td><asp:Button ID=AddRound Runat=server Text="Add survey round"/><asp:Button ID="SaveRound" Runat=server Text="Save"/><asp:PlaceHolder ID="RoundTextElements" Runat=server/><asp:Button ID="CancelRound" Runat=server Text="Cancel"/><asp:Button ID=AddPRQO Runat=server Visible=false/></td><td align="right" width="100%"><asp:PlaceHolder ID="SendAll" Runat=server/></td></tr></table></td></tr>
							<asp:PlaceHolder ID="Units" Runat=server/>
						</table>
					</form>
					
				</td>
				<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
				<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
			</tr>
		</table>
	</body>
</html>
