<%@ Page language="c#" Codebehind="feedbackQuestion.aspx.cs" AutoEventWireup="false" Inherits="eform.feedbackQuestion" %>
<html>
<head>
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Expires" content="-1">
<title><%=printSurveyName()%></title>
<link href="submit2.css" rel="stylesheet" type="text/css" media="screen">
<link href="submit2print.css" rel="stylesheet" type="text/css" media="print">
</head>
<body>
<form id="Form1" method="post" runat="server">
<div id="container">
	<div id="header">
		<div id="left"><%=eform.submit.printLogo(projectID)%></div>
		<div id="right"><img src="submitImages/eform_logga.gif"></div>
		<div id="clear"></div>
	</div>
	<div id="eform_header"><p><%=printSurveyName()%></p><div id="langButton" align="right"><asp:Label ID=Lang Runat=server/></div></div>
	<asp:Label ID="FeedbackText" Runat=server/>
	<br/>&nbsp;
</div>
</form>
</body>
</html>