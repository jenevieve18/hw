<%@ Page language="c#" Inherits="healthWatch.closed" Codebehind="closed.aspx.cs" %>
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
                    case 1: HttpContext.Current.Response.Write("Kontot avstängt"); break;
                    case 2: HttpContext.Current.Response.Write("Account closed"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ditt konto är nu avstängt och kommer att tas bort om 30 dagar. Om du vill ångra denna åtgärd innan dess, skicka ett e-postmeddelande till <A ID=RecallLink HREF=\"mailto:recall@healthwatch.se?Subject=Avbryt borttagning och aktivering av konto. Referens X" + HttpContext.Current.Request.QueryString["Xref"] + "\"><script language=\"javascript\">de('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')</script></A>. Ange <B>referens X" + HttpContext.Current.Request.QueryString["Xref"] + "</B> i ämnesraden."); break;
                    case 2: HttpContext.Current.Response.Write("Your account is now closed and will be deleted within 30 days. If you wish to reverse this action before then, please send an email to <A ID=RecallLink HREF=\"mailto:recall@healthwatch.se?Subject=Cancel account deletion. Reference X" + HttpContext.Current.Request.QueryString["Xref"] + "\"><script language=\"javascript\">de('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')</script></A>. State <B>reference X" + HttpContext.Current.Request.QueryString["Xref"] + "</B> in the subject."); break;
                }
             %></p><br>
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