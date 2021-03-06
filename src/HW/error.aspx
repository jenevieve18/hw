<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.error" Codebehind="error.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Fel", "H�lsonyheter, sj�lvhj�lp inom stress och h�lsa samt arbetsmilj�kartl�ggningar")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Error", "Health news, self-help in stress management and health promotion, and work environment surveys")); break;
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
                    case 1: HttpContext.Current.Response.Write("Beklagar. Nu blev det n�got fel."); break;
                    case 2: HttpContext.Current.Response.Write("Sorry. Something went wrong."); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div><p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kontakta g�rna"); break;
                    case 2: HttpContext.Current.Response.Write("Feel free to contact"); break;
                }
             %> <A HREF="javascript:dm('8051 3149 5653 6790 79 4493 8031 7622 7044 105 5208 3260 3260 7622 2202 8031 4107 2742 3061 6790 4493 8031 2742 2253 6790 8031 2942 2742 2294 105 3061')"><script language="javascript">de('8051 3149 5653 6790 79 4493 8031 7622 7044 105 5208 3260 3260 7622 2202 8031 4107 2742 3061 6790 4493 8031 2742 2253 6790 8031 2942 2742 2294 105 3061')</script></A> <%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("om det �r n�got vi b�r r�tta till"); break;
                    case 2: HttpContext.Current.Response.Write("if there is something we should correct"); break;
                }
             %>.</p>
                        <div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Hj�lp oss bli b�ttre"); break;
                    case 2: HttpContext.Current.Response.Write("Help us improve"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Du �r varmt v�lkommen att skicka synpunkter och f�rb�ttringsf�rslag till oss."); break;
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