<%@ Page language="c#" ValidateRequest="false" Codebehind="surveyQuestionSetup.aspx.cs" AutoEventWireup="false" Inherits="eform.surveyQuestionSetup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Language elements</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel=stylesheet type=text/css href="survey.css">
  </head>
  <body>
	
    <form id="surveyQuestionSetup" method="post" runat="server">
		<asp:Button ID="Save" Text="Save" Runat=server/><asp:Button ID="Close" Text=Close Runat=server/>
		<br/><br/>
		<table border="0" cellspacing="0" cellpadding="0"><asp:PlaceHolder ID="Lang" Runat=server/></table>
     </form>
  </body>
</html>
