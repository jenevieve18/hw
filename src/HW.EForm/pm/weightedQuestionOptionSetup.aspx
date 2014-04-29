<%@ Page language="c#" validaterequest=false Codebehind="weightedQuestionOptionSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.weightedQuestionOptionSetup" %>
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
	
    <form id="weightedQuestionOptionSetup" method="post" runat="server">
		<table border="0" cellspacing="0" cellpadding="0"">
			<tr><td colspan="4"><button onclick="location.href='weightedQuestionOptionSetup.aspx';">Add new weighted question</button><button onclick="location.href='weightedQuestionOption.aspx';">Return to list of weighted questions</button></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4" bgcolor="#CCCCCC"><img src="../img/null.gif" width="1" height="1"></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr><td colspan="4"><u>Weighted question information</u></td></tr>
			<tr><td colspan="4" align="right"><asp:Button ID="Save" Runat=server/></td></tr>
			<tr><td colspan="4">&nbsp;</td></tr>
			<tr>
				<td>Internal&nbsp;</td>
				<td colspan="3">
					<table border="0" cellspacing=0 cellpadding=0>
						<tr>
							<td>&nbsp;<asp:TextBox ID=Internal Runat=server Width=90/>&nbsp;Target&nbsp;<asp:TextBox ID="TargetVal" Runat=server Width=30/>&nbsp;&nbsp;Zones&nbsp;</td>
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
					</table>
				</td>
			</tr>
			<tr><td>Question&nbsp;</td><td colspan="3"><asp:DropDownList ID="QuestionID" Width="250" AutoPostBack=True Runat=server/><asp:DropDownList ID="OptionID" Width="250" Runat=server/></td></tr>
			<asp:PlaceHolder ID=Text Runat=server/>
		</table>
     </form>
	
</td>
		<td width="20" bgcolor="#FFFFFF"><img src="../img/null.gif" width="15" height="1"></td>
		<td width="*" bgcolor="#FFFFFF"><img src="../img/null.gif" width="5" height="1"></td>
	</tr>
</table>
</head>
</html>
