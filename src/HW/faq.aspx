<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.faq" Codebehind="faq.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("FAQ", "Vanliga fr�gor och hj�lp med tj�nsten")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("FAQ", "Frequently asked questions")); break;
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
                    case 1: HttpContext.Current.Response.Write("Hj�lp med tj�nsten"); break;
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
                    case 1: HttpContext.Current.Response.Write("Gl�mt anv�ndarnamn och/eller l�senord"); break;
                    case 2: HttpContext.Current.Response.Write("Forgotten username and/or password"); break;
                }
             %></p><br />
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("G� till sidan <a href=\"forgot.aspx\" class=\"lnk\">Gl�mt ditt l�senord?</a>. Fyll i den e-postadress du anv�nde n�r du skapade kontot s� skickar vi en l�nk till dig som du kan anv�nda f�r att skapa ett nytt l�senord."); break;
                    case 2: HttpContext.Current.Response.Write("Please visit the page <a href=\"forgot.aspx\" class=\"lnk\">Forgot your password?</a>. Input the email address used to create the account and we will send you a link to reset your password."); break;
                }
             %></p><br />
			<% } %>

            <%
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: %>
			<p style="font-weight:bold;">St�nga av p�minnelser eller �ndra p�minnelseintervall</p><br />
			<p><% if (HttpContext.Current.Session["UserID"] != null)
                     { %>G� till <a href="reminder.aspx" class="lnk">P�minnelser</a> som finns under menyalternativet <A HREF="#" class="lnk">Min h�lsa</A>.<% }
                     else
                     { %>Logga in h�r upptill. I menyn under rubriken <A HREF="#" class="lnk">Min h�lsa</A>, klicka p� <A HREF="#" class="lnk">P�minnelser</A>.<% } %> V�l d�r kan du �ndra intervall eller st�nga av p�minnelsefunktionen. Kom ih�g att spara efter valt alternativ.</p><br />

			<p style="font-weight:bold;">Radera konto</p><br />
			<p><% if (HttpContext.Current.Session["UserID"] != null)
                     { %>G� till <a href="profile.aspx" class="lnk">�ndra profil</a> h�r ovan.<% }
                     else
                     { %>F�r att radera ditt konto beh�ver du logga in och sedan klicka p� <a href="#" class="lnk">�ndra profil</a> (till v�nster om logga ut l�nken i menyn upptill).<% } %> L�ngst ned p� sidan finns d� alternativet <A href="#" class="lnk">Ta bort mitt konto</A>. N�r du tar bort ditt konto st�ngs kontot av men uppgifterna finns kvar i 30 dagar utifall du skulle �ngra dig. Under denna tid kan du �teraktivera ditt konto genom att skicka ett e-postmeddelande till <A class="lnk" HREF="javascript:dm('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')"><script language="javascript">de('865 459 124 98 255 272 436 66 487 664 86 634 98 272 272 49 364 86 98 272 436 364 139 98 436 634 364 456 205 86')</script></A>. Om du inte har kvar ditt referensnummer som visas direkt efter att kontot togs bort, ange det anv�ndarnamn och den e-postadressen som g�llde f�r kontot.</p>
            <% break;
                } %>
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