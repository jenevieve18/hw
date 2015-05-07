<%@ Page ValidateRequest="false" language="c#" Codebehind="controlPanel.aspx.cs" AutoEventWireup="false" Inherits="eform.controlPanel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>eForm Control Panel</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script language=javascript>
		function saveUser()
		{
			if(document.forms[0].ProjectRoundUserID.value!=0 && document.forms[0].UserOrgProjectRoundUnitID.value!=document.forms[0].UserProjectRoundUnitID[document.forms[0].UserProjectRoundUnitID.selectedIndex].value)
			{
				if(confirm('Vill du flytta även formulär kopplade till denna användare?'))
				{
					document.forms[0].MoveData.value=1;
				}
			}
			document.forms[0].SaveAction.value='1';
			document.forms[0].submit();
		}
		function setSrc(v)
		{
			event.dataTransfer.setData("Text", v.toString()); 
			event.dataTransfer.effectAllowed = "move";
		}
		function cncSrc() 
		{
			event.returnValue = false;                  
			event.dataTransfer.dropEffect = "move";  
		}
		function getSrc()
		{
			event.returnValue = false;                           
			event.dataTransfer.dropEffect = "move";
			var act = event.dataTransfer.getData("Text");
			if(act.substr(0,1) == 'X')
			{
				if(confirm('Vill du flytta även alla formulär kopplade till denna användare?'))
				{
					document.forms[0].MoveData.value = 1;
				}
			}
			document.forms[0].MoveObject.value = act;
			document.forms[0].MoveTarget.value = event.srcElement.id.toString().substr(5);
			document.forms[0].submit();
		}
    </script>
	<style type="text/css">
	body {
		margin: 0;
		padding: 0;
		text-align: center;
		background-color: #ffffff;
		background-image: url(submitImages/bgr.jpg);
		background-repeat: repeat-x;
		background-position: 0 0;
	}
	#container {
		text-align: left;
		width: 762px;
		margin: 0 auto;
	}
	#header {
		height: 100px;
		background-color: #ffffff;
		background-image: url(submitImages/header_bgr.gif);
		background-repeat: no-repeat;
		background-position: 0 0;
	}
	#left {
		float: left;
	}
	#right {
		float: right;
	}
	#clear {
		clear: both;
	}
	#units {
		background-color: #e0e8ec;
		height: 20px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: middle;
		padding-left: 4px;
		padding-top: 1px;
		background-image: url(img/units_bgr.gif);
	}
	#content1 {
		background-color: #e6e6e6;
		height: 27px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
		background-image: url(img/content1_bgr.gif);
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: top;
		padding-left: 9px;
		padding-top: 8px;
		padding-bottom: 6px;
		padding-right: 6px;
	}
	#content2 {
		background-color: #e6e6e6;
		height: 27px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
		background-image: url(img/content2_bgr.gif);
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: top;
		padding-left: 8px;
		padding-top: 8px;
		padding-bottom: 6px;
		padding-right: 6px;
	}
	.content3 {
		background-color: #f2f2f2;
		height: 27px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		border-left-width: 1px;
		border-left-style: solid;
		border-left-color: #a1a1a1;
		vertical-align: top;
		padding-left: 8px;
		padding-top: 8px;
		padding-bottom: 6px;
		padding-right: 6px;
	}
	.content4 {
		background-color: #f2f2f2;
		height: 27px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: top;
		padding-left: 8px;
		padding-top: 8px;
		padding-bottom: 6px;
		padding-right: 6px;
	}
		#units1 {
		background-color: #e0e8ec;
		height: 20px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: middle;
		padding-left: 5px;
		padding-top: 1px;
		background-image: url(img/units1_bgr.gif);
	}
	#units2 {
		background-color: #e0e8ec;
		height: 20px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: middle;
		padding-left: 4px;
		padding-top: 1px;
		background-image: url(img/units2_bgr.gif);
	}
	#units3 {
		background-color: #e0e8ec;
		height: 20px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		vertical-align: middle;
		padding-left: 4px;
		padding-top: 1px;
	}
	#units4 {
		background-color: #e0e8ec;
		height: 20px;
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
		border-bottom-width: 1px;
		border-bottom-style: solid;
		border-bottom-color: #a1a1a1;
		border-right-width: 1px;
		border-right-style: solid;
		border-right-color: #a1a1a1;
		border-left-width: 1px;
		border-left-style: solid;
		border-left-color: #a1a1a1;
		vertical-align: middle;
		padding-left: 4px;
		padding-top: 1px;
	}
	.cp_foretag {
		font-family: Arial, Helvetica, sans-serif;
		font-size: 14px;
	}
	A {
		color: #000000;
		text-decoration: underline;
	}
	.cp_12px {
		font-family: Arial, Helvetica, sans-serif;
		font-size: 12px;
	}
	.cp_11px, .dd {
		font-family: Arial, Helvetica, sans-serif;
		font-size: 11px;
	}
	.dd
	{
		background-color: #f2f2f2;
	}
	.topheader {
		padding-top: 30px;
		padding-bottom: 20px;
		font-size: 22px;
		font-family: Helvetica, Arial, sans-serif;
		margin: 0;
	}
	</style>
</head>

<body>
<form id="controlPanel" method="post" runat="server">
	<div id="container">
	<div id="header">
		<div id="left"><img src="submitImages/logga.gif" width="160" height="100"></div>
		<div id="right"><asp:Label id=logo Runat=server/></div>
		<div id="clear"></div>
	</div>
		<input runat=server id=ProjectRoundUnitID type=hidden NAME="ProjectRoundUnitID"/>
		<input runat=server id="MoveData" type=hidden NAME="MoveData"/>
		<input runat=server id="MoveObject" type=hidden NAME="MoveObject"/>
		<input runat=server id="MoveTarget" type=hidden NAME="MoveTarget"/>
		<input runat=server id="ChangeMode" type=hidden NAME="ChangeMode"/>
		<input runat=server id="UserOrgProjectRoundUnitID" type=hidden NAME="UserOrgProjectRoundUnitID"/>
		<input runat=server id="SaveSettings" type=hidden NAME="SaveSettings"/>
		<input runat=server id="Action" type=hidden NAME="Action"/>
		<input runat=server id="SaveAction" type=hidden NAME="SaveAction"/>
		<input runat=server id="ProjectRoundUserID" type=hidden NAME="ProjectRoundUserID"/>
		<input runat=server id="UnitProjectRoundUnitID" type=hidden NAME="UnitProjectRoundUnitID"/>
		<p class="topheader"></p>
		<table width="762" border="0" cellspacing="0" cellpadding="0">
		<tr>
			<td id="units1">F&ouml;retag/organisation och formulär</td>
			<td colspan="2" id="units2">Tidsperiod</td>
			<td colspan="2" id="units2">Svarsfrekvens</td>
			<td colspan="2" id="units2">Inställningar</td>
		</tr>
		<tr>
			<td id="content1"><span class="cp_foretag"><b><asp:Label ID="Org" Runat=server/></b></span><br/><img src="img/null.gif" width="1" height="8"><br/><asp:Label ID=Intro Runat=server/></td>
			<td colspan="2" id="content2"><table border="0" cellpadding=0 cellspacing=0><tr><td><asp:DropDownList Enabled=false CssClass="dd" ID=YearLow Runat=server/></td><td><asp:DropDownList Enabled=false CssClass="dd" ID=MonthLow Runat=server/></td><td><asp:DropDownList Enabled=false CssClass="dd" ID=DayLow Runat=server/></td></tr><tr><td><asp:DropDownList CssClass="dd" ID="YearHigh" Runat=server/></td><td><asp:DropDownList CssClass="dd" ID="MonthHigh" Runat=server/></td><td><asp:DropDownList CssClass="dd" ID="DayHigh" Runat=server/></td></tr></table></td>
			<td colspan="2" id="content2"><asp:Label ID="OrgStatus" Runat=server/><br/><img src="img/null.gif" width="1" height="5"><br/><asp:Label ID="OrgAnswer" Runat=server/></td>
			<td colspan="2" id="content2"><table border="0" cellpadding=0 cellspacing=0><asp:Label ID=Change Runat=server/></table></td>
		</tr>
		<asp:PlaceHolder ID=Settings Runat=server Visible=false>
		<tr>
			<td id="units4" colspan="3">Introduktionstext i valt formulär</td>
			<td colspan="2" id="units3">Organisation</td>
			<td colspan="2" id="units3">Brytvärden för status, %</td>
		</tr>
		<tr>
			<td colspan="3" id="content1" rowspan=3><asp:TextBox id=SurveyIntro runat=server/></td>
			<td colspan="2" id="content2" rowspan=3><table border="0" cellspacing=0 cellpadding=0><tr><td><img src=img/unit_on.gif></td><td>&nbsp;<a href="JavaScript:document.forms[0].Action.value=1;document.forms[0].submit();" class="cp_11px" style="color:#777777;">Lägg till enhet</a></td></tr><tr><td><img src=img/user_on.gif></td><td>&nbsp;<a href="JavaScript:document.forms[0].Action.value=2;document.forms[0].UserOrgProjectRoundUnitID.value=0;document.forms[0].MoveData.value=0;document.forms[0].ProjectRoundUserID.value=0;document.forms[0].submit();" class="cp_11px" style="color:#777777;">Lägg till användare</a></td></tr></table></td>
			<td colspan="2" id="content2"><table border="0" cellspacing=0 cellpadding=0><tr><td><img src="img/red.gif"></td><td><asp:TextBox id="Yellow" runat=server/></td><td><img src="img/yellow.gif"></td><td><asp:TextBox id="Green" runat=server/></td><td><img src="img/green.gif"></td></tr></table></td>
		</tr>
		<tr>
			<td colspan="3" id="units3">Tidsintervall för valt formulär</td>
		</tr>
		<tr>
			<td colspan="3" id="content2"><asp:TextBox id="Timeframe" runat=server/> dagar</td>
		</tr>
		<asp:PlaceHolder ID=ChangeUser runat=server visible=false>
		<tr>
			<td height="20" colspan="7"></td>
		</tr>
		<tr>
			<td id="units1">Namn</td>
			<td colspan="6" id="units2">Avdelning/Enhet</td>
		</tr>
		<tr>
			<td id="content1"><asp:TextBox id="UserName" runat=server/></td>
			<td colspan="6" id="content2"><asp:DropDownList id="UserProjectRoundUnitID" runat=server/></td>
		</tr>
		<tr>
			<td id="units4">E-post</td>
			<td colspan="5" id="units3">Roll</td>
			<td id="units3">Spara</td>
		</tr>
		<tr>
			<td id="content1"><asp:TextBox id="UserEmail" runat=server/><asp:Label ID="ErrorEmail" Runat=server/></td>
			<td colspan="5" id="content2"><asp:DropDownList id="UserCategoryID" runat=server/></td>
			<td id="content2"><A HREF="JavaScript:saveUser();"><img border="0" src="img/saveTool.gif"></A></td>
		</tr>
		</asp:PlaceHolder>
		<asp:PlaceHolder ID="ChangeUnit" runat=server visible=false>
		<tr>
			<td height="20" colspan="7"></td>
		</tr>
		<tr>
			<td id="units1">Avdelning/enhet</td>
			<td colspan="5" id="units3">Ovanstående avdelning/enhet</td>
			<td id="units2">Spara</td>
		</tr>
		<tr>
			<td id="content1"><asp:TextBox id="UnitName" runat=server/></td>
			<td colspan="5" id="content2"><asp:DropDownList id="UnitParentProjectRoundUnitID" runat=server/></td>
			<td id="content2"><A HREF="JavaScript:document.forms[0].SaveAction.value='1';document.forms[0].submit();"><img border="0" src="img/saveTool.gif"></A></td>
		</tr>
		</asp:PlaceHolder>
		</asp:PlaceHolder>
		<asp:PlaceHolder ID="Send" runat=server visible=false>
		<tr>
			<td height="20" colspan="7"></td>
		</tr>
		<tr>
			<td id="units1" colspan="2">Meddelande</td>
			<td id="units2" colspan="5">Skicka till</td>
		</tr>
		<tr>
			<td id="content1" colspan="2" rowspan="5"><asp:TextBox id="Message" runat=server/></td>
			<td id="content2" colspan="5"><asp:Label CssClass="cp_11px" id="SendTo" runat=server/></td>
		</tr>
		<tr>
			<td id="units3" colspan="5">Ämnesrad</td>
		</tr>
		<tr>
			<td colspan="5" id="content2"><asp:TextBox id="Subject" runat=server/></td>
		</tr>
		<tr>
			<td id="units3" colspan="4">Avsändare</td>
			<td id="units3">Skicka</td>
		</tr>
		<tr>
			<td colspan="4" id="content2"><asp:TextBox id="FromEmail" runat=server/></td>
			<td id="content2"><A HREF="JavaScript:document.forms[0].SaveAction.value='1';document.forms[0].submit();"><img border="0" src="img/saveTool.gif"></A></td>
		</tr>
		</asp:PlaceHolder>
		<tr>
			<td height="20" colspan="7"></td>
		</tr>
		<tr>
			<td id="units1">Avdelning/enhet</td>
			<td id="units2">Inlagda</td>
			<td id="units2">Mott. e-post</td>
			<td id="units2">Status</td>
			<td id="units2">Påbörjade</td>
			<td id="units2">Klara</td>
			<td id="units2">Rapport</td>
		</tr>
		<asp:Label ID=List Runat=server/>
		<asp:Label ID=Powered Runat=server/>
		</table>
</div>
</form>
</body>
</html>
