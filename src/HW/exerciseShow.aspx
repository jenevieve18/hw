<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exerciseShow.aspx.cs" Inherits="HW.exerciseShow" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (LID)
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Övningar", "Övningar", replacementHead)); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Exercises", "Exercises", replacementHead)); break;
       }
%>
        <script type="text/javascript">            $("body").addClass("popup"); $(document).ready(function () { $("body").addClass("popup"); });</script>
        <script src="AC_ActiveX.js" type="text/javascript"></script>
    </head>
<!--[if lt IE 7 ]> <body class="ie6" class="popup"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7" class="popup"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8" class="popup"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9" class="popup"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body class="popup"> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">

<div class="popupie">
	<div class="header">
		<h1>HealthWatch.se<%=headerText%></h1> <a href="#" id="printBtn" onclick="window.print();return false;" class="print"><%
		    switch (LID)
                {
                    case 1: HttpContext.Current.Response.Write("Skriv ut"); break;
                    case 2: HttpContext.Current.Response.Write("Print"); break;
                }
             %></a>
	</div>
	<div class="content">
    <img src="img/hwlogosmall.gif" /><%=logos%>
    <br /><br />
		<asp:PlaceHolder id="exercise" runat="server"/>
    </div>
<div class="footer">&copy; <%=DateTime.Now.Year%> www.healthwatch.se</div>
</div>
</form>
</body>
</html>

