<%@ Page language="c#" Inherits="healthWatch.password" Codebehind="password.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Nytt lösenord", "Fyll i nytt lösenord här")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("New password", "Fill in a new password here")); break;
       }
           %>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides about<%=healthWatch.Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
			</div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Nytt lösenord"); break;
                    case 2: HttpContext.Current.Response.Write("New password"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Fyll i nytt lösenord här"); break;
                    case 2: HttpContext.Current.Response.Write("Fill in a new password here"); break;
                }
             %></p><input type="password" name="ForgotPassword" style="width:120px;"><input type="submit" value="OK">
		<br>
		<asp:Label ID=error Runat=server/>
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
		<%=healthWatch.Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>