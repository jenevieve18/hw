<%@ Page language="c#" validaterequest=false Codebehind="customFeedback.aspx.cs" AutoEventWireup="false" Inherits="eform.customFeedback" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Feedback</title>
    <script type="text/javascript" src="swfobject.js"></script>
    <style type="text/css">
		body, div, td {font-family: Arial, Helvetica, sans-serif; font-size: 12px;}
		body {margin: 0;padding: 0;text-align: center;}
	</style>
  </head>
  <body>
    <form id="Form1" method="post" runat="server">
    <div style="text-align:left;margin: 0 auto;width:620px;">
		<br/><br/>
		<asp:Button ID=Save Visible=false Runat=server Text="SAVE"/><br/><br/>
		<asp:PlaceHolder ID=feedback Runat=server/>
		<br/><br/>
	</div>
     </form>
	
  </body>
</html>
