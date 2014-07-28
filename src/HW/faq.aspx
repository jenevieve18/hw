<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="faq.aspx.cs" Inherits="HW.faq" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("FAQ", "Vanliga frågor och hjälp med tjänsten")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("FAQ", "Frequently asked questions")); break;
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
                    case 1: HttpContext.Current.Response.Write("Hjälp med tjänsten"); break;
                    case 2: HttpContext.Current.Response.Write("Support"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<% if(HttpContext.Current.Session["UserID"] == null) { %>
			<p style="font-weight:bold;"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Glömt användarnamn och/eller lösenord"); break;
                    case 2: HttpContext.Current.Response.Write("Forgotten username and/or password"); break;
                }
             %></p><br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Gå till sidan <a href=\"forgot.aspx\" class=\"lnk\">Glömt ditt lösenord?</a>. Fyll i den e-postadress du använde när du skapade kontot så skickar vi en länk till dig som du kan använda för att skapa ett nytt lösenord."); break;
                    case 2: HttpContext.Current.Response.Write("Please visit the page <a href=\"forgot.aspx\" class=\"lnk\">Forgot your password?</a>. Input the email address used to create the account and we will send you a link to reset your password."); break;
                }
             %></p><br />
			<% } %>

            <%
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: %>
			<p style="font-weight:bold;">Stänga av påminnelser eller Ändra påminnelseintervall</p><br />
			<p><% if (HttpContext.Current.Session["UserID"] != null)
                     { %>Gå till <a href="reminder.aspx" class="lnk">Påminnelser</a> som finns under menyalternativet <A HREF="#" class="lnk">Min hälsa</A>.<% }
                     else
                     { %>Logga in här upptill. I menyn under rubriken <A HREF="#" class="lnk">Min hälsa</A>, klicka på <A HREF="#" class="lnk">Påminnelser</A>.<% } %> Väl där kan du ändra intervall eller stänga av påminnelsefunktionen. Kom ihåg att spara efter valt alternativ.</p><br />

			<p style="font-weight:bold;">Radera konto</p><br />
			<p><% if (HttpContext.Current.Session["UserID"] != null)
                     { %>Gå till <a href="profile.aspx" class="lnk">Ändra profil</a> här ovan.<% }
                     else
                     { %>För att radera ditt konto behöver du logga in och sedan klicka på <a href="#" class="lnk">Ändra profil</a> (till vänster om logga ut länken i menyn upptill).<% } %> Längst ned på sidan finns då alternativet <A href="#" class="lnk">Ta bort mitt konto</A>. När du tar bort ditt konto stängs kontot av men uppgifterna finns kvar i 30 dagar utifall du skulle ångra dig. Under denna tid kan du återaktivera ditt konto genom att skicka ett e-postmeddelande till <A class="lnk" HREF="javascript:dm('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')"><script language="javascript">                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           de('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')</script></A>. Om du inte har kvar ditt referensnummer som visas direkt efter att kontot togs bort, ange det användarnamn och den e-postadressen som gällde för kontot.</p>
            <% break;
                } %>
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
