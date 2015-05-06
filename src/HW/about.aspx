<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.about" Codebehind="about.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Om v�ra tj�nster","H�lsonyheter, sj�lvhj�lp inom stress och h�lsa samt arbetsmilj�kartl�ggningar.")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("About our services","Health news, self-help in stress management and health promotion and work environment surveys.")); break;
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
                    case 1: HttpContext.Current.Response.Write("Om HealthWatch"); break;
                    case 2: HttpContext.Current.Response.Write("About HealthWatch"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div><p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("HealthWatch har som m�l �r att bidra med konkreta och praktiska verktyg f�r " +
			                    "individer, organisationer och f�retag f�r att bevara och �ka h�lsa och " +
			                    "livskvalitet samt motverka stressrelaterad oh�lsa. Tj�nsten best�r " +
			                    "huvudsakligen av tre delar: h�lsonyheter, sj�lvhj�lp och " +
			                    "arbetsmilj�kartl�ggningar. Sj�lvhj�lpsdelen �r en vidareutvecklad " +
			                    "version av ett vetenskapligt utv�rderat system med dokumenterat v�lg�rande " +
			                    "effekter p� psykologiska och biologiska variabler. Verktyget baserar sig p� dagens " +
			                    "samlade kunskap och p� flera �rs forskning. Tj�nsterna �r kostnadsfria f�r " +
			                    "privatpersoner och grupper om max 25 personer. Vid st�rre grupper " +
			                    "debiteras drifts- och administrationskostnader."); break;
                    case 2: HttpContext.Current.Response.Write("HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-releated problems."); break;
                }
             %></p>
             <br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Tj�nsterna i HealthWatch �r �mnade att ge f�ruts�ttningar f�r enskilda " +
			                    "individer att kunna arbeta med sin h�lsoutveckling �ver tid. Samtidigt vill " +
			                    "vi bidra till att underl�tta f�r organisationer och f�retag att genomf�ra sitt " +
			                    "systematiska arbetsmilj�arbete genom konkreta och enkla verktyg. P� ett " +
			                    "enkelt s�tt kan man f�lja variabler som h�lsa, stress, effektivitet och " +
			                    "arbetsgl�dje p� en eller flera grupper, avdelningar och sektioner �ver tid."); break;
                    case 2: HttpContext.Current.Response.Write(""); break;
                }
             %></p>
             <div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("HealthWatch"); break;
                    case 2: HttpContext.Current.Response.Write("HealthWatch"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("HealthWatch drivs av Interactive Health Group in Stockholm AB.<br/><br/>V�rt motto: F�r medm�nsklighet, omtanke och samh�llsnytta!"); break;
                    case 2: HttpContext.Current.Response.Write("HealthWatch is run by Interactive Health Group in Stockholm AB."); break;
                }
             %></p>
             <br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ansvarig utgivare och chefredakt�r f�r HealthWatch �r med dr <A class=\"lnk\" href=\"http://www.danhasson.se\">Dan Hasson</A>, e-post <A class=\"lnk\" HREF=\"javascript:dm('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')\"><script language=\"javascript\">de('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')</script></A>."); break;
                    case 2: HttpContext.Current.Response.Write(""); break;
                }
             %></p>
			<br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("L�s mer om <A class=\"lnk\" href=\"javascript:void(window.open('policy.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "','','width=600,height=600,scrollbars=yes'));\">integritetspolicy, anv�ndarvillkor, forskning och PUL</A>."); break;
                    case 2: HttpContext.Current.Response.Write(""); break;
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