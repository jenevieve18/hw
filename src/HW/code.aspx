<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.code" Codebehind="code.aspx.cs" %>
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
	    <form id="Form1" method="post" runat="server" autocomplete=off>
        <div class="container_16 myhealth two-sides about<%=healthWatch.Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
        </div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Logga in med kod"); break;
                    case 2: HttpContext.Current.Response.Write("Log in using code"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ange den kod du fått nedan. Om du också anger din e-postadress kommer du kunna få återkoppling skickad till dig efter att du fyllt i enkäten."); break;
                    case 2: HttpContext.Current.Response.Write("Please enter the code you have received below. If you also enter your email address you will be able get feedback sent to you after filling out the survey."); break;
                }
             %></p><br /><p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kod"); break;
                    case 2: HttpContext.Current.Response.Write("Code"); break;
                }
             %></p>
             <asp:TextBox ID=Code Width=200 runat=server autocomplete=off /><br /><p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("E-post (frivilligt)"); break;
                    case 2: HttpContext.Current.Response.Write("Email (optional)"); break;
                }
             %></p>
             <asp:TextBox ID=Email Width=200 runat=server autocomplete=off />

                <div class="form_footer">
                    <asp:Button ID=submit Runat=server/>
                </div>
             <div class="bottom"></div>
					</div> <!-- end .main -->
					<!--
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
					</div>-->
                    <!-- end .sidebar -->
					<div class="bottom"></div>
				</div>

		</div><!-- end .contentgroup	-->
		<%=healthWatch.Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>
