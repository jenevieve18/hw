<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.myhealth" Codebehind="myhealth.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=healthWatch.Db.header2("Min hälsa", "Unikt verktyg för att mäta, följa och jämföra din hälsa och stressnivå över tiden")%>
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
                    case 1: HttpContext.Current.Response.Write("Min hälsa"); break;
                    case 2: HttpContext.Current.Response.Write("My health"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Via Min hälsa får du till gång till ett unikt verktyg för att mäta, följa och jämföra din hälsa och stressnivå över tiden. Du får fri tillgång till ett verktyg för stresshantering samt för att bevara och öka hälsa och välbefinnande. Verktyget baserar sig på dagens samlade kunskap och på flera års forskning. Delar av detta verktyg har utvärderats i en världsunik forskningsstudie. Där fann vi åtminstone kortsiktigt positiva effekter på exempelvis stresshanteringsförmåga, sömn, återhämtning, koncentrationsförmåga och allmänt välbefinnande. De positiva fynden återfanns också i relaterade hormoner som bland annat speglar återhämtningsförmåga och välbefinnande.<br/><br/>Tjänsterna är kostnadsfria för privatpersoner. Min hälsa är en del av HealthWatch som kräver att du har skapat ett konto. Om du redan har ett konto loggar du in genom att ange dina inloggningsuppgifter längst upp till höger."); break;
                    case 2: HttpContext.Current.Response.Write("With My health you get access to a unique tool to measure, follow and compare your health and stress level over time. You get free access to a toolbox for stress management and health promotion. The tool is based on todays collected knowledge from several years of research. Parts of this tool has been evaluated in a globally unique research study. In that study we found at least short term effects on stress management ability, sleep, recovery, ability to concentrate and general well-being. The positive findings were confirmed in related hormones that reflect recovery and well-being.<br/><br/>The service is free for private persons. My health is a part of HealthWatch that requires you to create an account. If you've already got an account you log on by entering your log in information in the upper right corner."); break;
                }
             %></p>
             <div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Registrera dig nu!"); break;
                    case 2: HttpContext.Current.Response.Write("Register now!"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Det tar inte mer än en minut och är helt gratis."); break;
                    case 2: HttpContext.Current.Response.Write("The process takes about one minute and is completely free of charge."); break;
                }
             %> <a href="/register.aspx"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Klicka här."); break;
                    case 2: HttpContext.Current.Response.Write("Click here."); break;
                }
             %></a></p>
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