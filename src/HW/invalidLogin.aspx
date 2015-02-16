<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="invalidLogin.aspx.cs" Inherits="HW.invalidLogin" %>
<%@ Import Namespace="HW.Core.FromHW" %>
  <!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("Felaktig inloggning", "Hälsonyheter, självhjälp inom stress och hälsa samt arbetsmiljökartläggningar")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("Incorrect login", "Health news, self-help in stress management and health promotion, and work environment surveys")); break;
       }
           %>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides about<%=Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=Db.nav2()%>
			</div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Felaktig inloggningslänk"); break;
                    case 2: HttpContext.Current.Response.Write("Incorrect login link"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div><p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Länken du klickade på för att logga in är antingen felaktig eller redan förbrukad (engångslänk). Vänligen logga in manuellt med ditt användarnamn och lösenord här ovan."); break;
                    case 2: HttpContext.Current.Response.Write("The link that was used to log in is either broken or expired (one-time link that is already used). Please log in manually using your username and password above."); break;
                }
             %></p>
                        <div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Hjälp oss bli bättre"); break;
                    case 2: HttpContext.Current.Response.Write("Help us improve"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Du är varmt välkommen att skicka synpunkter och förbättringsförslag till oss."); break;
                    case 2: HttpContext.Current.Response.Write("You are most welcome to send your feedback and improvement suggestions to us."); break;
                }
             %></p>
                    <div class="bottom"></div>
					</div><!-- end .sidebar -->
					<div class="bottom"></div>
				</div>

		</div><!-- end .contentgroup	-->
		<%=Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>