<%@ Page language="c#" Codebehind="sent.aspx.cs" AutoEventWireup="false" Inherits="eForm.sent" %>
<html>
<head>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	-->
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=printSurveyName()%></title>
<script language="JavaScript" type="text/JavaScript">
window.history.forward(1);

function closeWindow(prmpt)
{
	if(prmpt == '' || confirm(prmpt))
	{
		window.open('','_parent','');
		window.close();
	}
}
</script>
<link href="submit2.css" rel="stylesheet" type="text/css">
</head>
<body>
<div id="container">
	<div align="right" id="fixedButtons" style="background:URL(submitImages/info_bgr2.gif) no-repeat;">
		<img src="submitImages/null.gif" width="1" height="50" border="0"><br/>
		<a href="JavaScript:void(closeWindow(''));" id="Close"><IMG SRC="submitImages/button_close0.gif" border="0"/></a>
	</div>
	<div id="header">
		<div id="left"><%=printLogo()%></div>
		<div id="right"><img src="submitImages/eform_logga.gif"></div>
		<div id="clear"></div>
	</div>
	<div id="eform_header"><p><%=printSurveyName()%></p></div>
	<div class="eform_area"><p>Information</p></div>
	<div class="eform_ques"><div style="padding:20 20 20 20">Informationen har skickats till din e-post.</div></div>
	<br/>&nbsp;
</div>
</body>
</html>