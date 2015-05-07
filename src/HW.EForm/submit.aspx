<%@ Page language="c#" Codebehind="submit.aspx.cs" AutoEventWireup="false" Inherits="eform.submit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	-->
	<meta http-equiv="Pragma" content="no-cache">
	<meta http-equiv="Cache-control" content="no-cache">
	<meta name="Robots" content="noindex,nofollow,noarchive">
	<% if(HttpContext.Current.Request.QueryString["Domain"] != null) { HttpContext.Current.Response.Write("<script language=\"javascript\" type=\"text/JavaScript\">document.domain='" + HttpContext.Current.Request.QueryString["Domain"] + "';</script>"); } %>
	<title><%=printSurveyName()%></title>
	<link href="submit2.css?V=<%=version()%>" rel="stylesheet" type="text/css" media="screen">
	<link href="submit2print.css?V=<%=version()%>" rel="stylesheet" type="text/css" media="print">
	<script type="text/javascript" src="submitJs.aspx?LID=<%=lang()%>&amp;V=<%=version()%>"></script>
</head>
<body onload="fixedButtonsInit();loadVAS();updateButtons(0,true);" onresize="loadVAS();" onscroll="fixedButtonsUpdate();">
<form id="form1" method="post" runat="server">
<div id="container">
	<div align="right" id="fixedButtons" style="background:URL(submitImages/info_bgr2.gif) no-repeat;">
		<img src="submitImages/null.gif" width="1" height="38" border="0"><br>
		<div id=status>&nbsp;</div>
		<img src="submitImages/null.gif" width="1" height="5" border="0"><br>
		<div id=progress><img id=progressBar width=132 height=9 src="submitImages/null.gif"></div>
		<img src="submitImages/null.gif" width="1" height="5" border="0"><br>
		<div id=minutes>&nbsp;</div>
		<img src="submitImages/null.gif" width="1" height="20" border="0"><br>
		<asp:PlaceHolder ID="FixedButtons" Runat=server />
	</div>
	<div id="header">
		<div id="left"><%=printLogo()%></div>
		<div id="right"><%=printRoundLogo()%></div>
		<div id="clear"></div>
	</div>
	<div id="eform_header"><p><%=printSurveyName()%></p></div>
	<asp:Label ID="SurveyIntro" Runat=server />
	<asp:PlaceHolder ID=Survey Runat=server />
	<asp:PlaceHolder ID="SurveyButtons" Runat=server />
	<br>&nbsp;
	<div id="processingWindow" style="position:absolute;background:URL(submitImages/processing_bg.gif) no-repeat;display:none;z-index:1000;width:389px;height:249px;text-align:center;font-family:Arial;font-weight:bold;font-size:18px;"><img src="img/null.gif" width="1" height="58"><br><img src="submitImages/Processing_ani.gif"><br><br><%=(lang() == 2 ? "Please wait while your answers are saved." : "Vänligen vänta. Dina svar sparas.") %></div>
</div>
</form>
</body>
</html>