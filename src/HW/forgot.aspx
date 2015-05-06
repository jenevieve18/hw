<%@ Page language="c#" Inherits="healthWatch.forgot" Codebehind="forgot.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Glömt lösenord?", "Fyll i din e-postadress här för att återställa ditt lösenord")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Forgot your password?", "Fill in your email here to reset your password")); break;
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
                    case 1: HttpContext.Current.Response.Write("Glömt ditt användarnamn eller lösenord?"); break;
                    case 2: HttpContext.Current.Response.Write("Did you forgot your username or password?"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Fyll i din e-postadress nedan så skickar vi en länk till dig som du kan använda för att skapa ett nytt lösenord."); break;
                    case 2: HttpContext.Current.Response.Write("Fill in your email address below and we will send you a link that can be used to create a new password."); break;
                }
             %></p>
			<input type="text" name="ForgotEmail" size=50<%= (!Page.IsPostBack && HttpContext.Current.Request.QueryString["Email"] != null ? " value=\"" + HttpContext.Current.Request.QueryString["Email"] + "\"" : "") %>> <input type="submit" value="<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Skicka"); break;
                    case 2: HttpContext.Current.Response.Write("Submit"); break;
                }
             %>">
			<br>
			<asp:Label ID=sent Runat=server/>
<div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Fick du inget meddelande?"); break;
                    case 2: HttpContext.Current.Response.Write("No email showed up?"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Det troliga är att du skrev fel eller att ditt konto skapades med en annan e-postadress. Vänligen prova igen."); break;
                    case 2: HttpContext.Current.Response.Write("The most likely reason is that you misspelled the address or that your account was created with a different email address. Please try again."); break;
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