<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="HW.about" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("Om våra tjänster","Hälsonyheter, självhjälp inom stress och hälsa samt arbetsmiljökartläggningar.")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("About our services","Health news, self-help in stress management and health promotion and work environment surveys.")); break;
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
		<%=Db.nav2()%>
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
                    case 1: HttpContext.Current.Response.Write("HealthWatch har som mål är att bidra med konkreta och praktiska verktyg för " +
			                    "individer, organisationer och företag för att bevara och öka hälsa och " +
			                    "livskvalitet samt motverka stressrelaterad ohälsa. Tjänsten består " +
			                    "huvudsakligen av tre delar: hälsonyheter, självhjälp och " +
			                    "arbetsmiljökartläggningar. Självhjälpsdelen är en vidareutvecklad " +
			                    "version av ett vetenskapligt utvärderat system med dokumenterat välgörande " +
			                    "effekter på psykologiska och biologiska variabler. Verktyget baserar sig på dagens " +
			                    "samlade kunskap och på flera års forskning. Tjänsterna är kostnadsfria för " +
			                    "privatpersoner och grupper om max 25 personer. Vid större grupper " +
			                    "debiteras drifts- och administrationskostnader."); break;
                    case 2: HttpContext.Current.Response.Write("HealthWatch provides tools for individuals and organisations to preserve and increase health and quality of life, as well as reduce stress-releated problems."); break;
                }
             %></p>
             <br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Tjänsterna i HealthWatch är ämnade att ge förutsättningar för enskilda " +
			                    "individer att kunna arbeta med sin hälsoutveckling över tid. Samtidigt vill " +
			                    "vi bidra till att underlätta för organisationer och företag att genomföra sitt " +
			                    "systematiska arbetsmiljöarbete genom konkreta och enkla verktyg. På ett " +
			                    "enkelt sätt kan man följa variabler som hälsa, stress, effektivitet och " +
			                    "arbetsglädje på en eller flera grupper, avdelningar och sektioner över tid."); break;
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
                    case 1: HttpContext.Current.Response.Write("HealthWatch drivs av Interactive Health Group in Stockholm AB.<br/><br/>Vårt motto: För medmänsklighet, omtanke och samhällsnytta!"); break;
                    case 2: HttpContext.Current.Response.Write("HealthWatch is run by Interactive Health Group in Stockholm AB."); break;
                }
             %></p>
             <br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ansvarig utgivare och chefredaktör för HealthWatch är med dr <A class=\"lnk\" href=\"http://www.danhasson.se\">Dan Hasson</A>, e-post <A class=\"lnk\" HREF=\"javascript:dm('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')\"><script language=\"javascript\">de('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')</script></A>."); break;
                    case 2: HttpContext.Current.Response.Write(""); break;
                }
             %></p>
			<br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Läs mer om <A class=\"lnk\" href=\"javascript:void(window.open('policy.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "','','width=600,height=600,scrollbars=yes'));\">integritetspolicy, användarvillkor, forskning och PUL</A>."); break;
                    case 2: HttpContext.Current.Response.Write(""); break;
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
