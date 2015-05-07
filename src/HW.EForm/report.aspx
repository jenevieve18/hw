<%@ Page language="c#" Codebehind="report.aspx.cs" AutoEventWireup="false" Inherits="eform.report" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>eForm</title>
	<meta http-equiv="Pragma" content="no-cache">
	<meta http-equiv="Expires" content="-1">
	<!--
	<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
	-->
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<link href="report.css" rel="stylesheet" type="text/css">
  </head>
  <body onload="">
	
    <form id="report" method="post" runat="server">
		<div align="center">
		<div id="eform_header"></div>
		<asp:Label ID=R Runat=server/>
		</div>
		<br><br>
     </form>
	
  </body>
</html>
