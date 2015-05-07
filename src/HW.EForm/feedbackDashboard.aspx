<%@ Page language="c#" Codebehind="feedbackDashboard.aspx.cs" AutoEventWireup="false" Inherits="eform.feedbackDashboard" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Feedback</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
  </head>
  <body MS_POSITIONING="GridLayout">
	
    <form id="Form1" method="post" runat="server">
		<table border="1" cellspacing="0" cellpadding="5">
		<tr><td><b>Department/unit</b></td><td><b>Answer count</b></td><td><b>This only</b></td><td><b>Vs all</b></td></tr>
		<asp:PlaceHolder ID=Org Runat=server/>
		</table>
     </form>
	
  </body>
</html>
