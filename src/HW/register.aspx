<%@ Page language="c#" Inherits="healthWatch.register" Codebehind="register.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Skapa konto", "Skapa konto")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Create account", "Create account")); break;
       }
           %>
           <script type="text/javascript" src="profile.js"></script>
           <script type="text/javascript">
               function selectedDupeAction() {
                   var obj = document.forms[0].DupeAction;
                   for (i = 0; i < obj.length; i++) {
                       if (obj[i].checked) {
                           return obj[i].value;
                       }
                   }
               }
            </script>
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
			 
				<h1 class="header">
		<% switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
     {
         case 1:
%>
Skapa ditt konto
<%
    break;
         case 2:
 %>
 Create your account
 <%
             break;
     }
		%>
		</h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
		                <asp:PlaceHolder ID=contents Runat=server/>
		                <asp:PlaceHolder ID="error" Runat=server/>
                        <div class="form_footer">
                            <asp:Button ID=submit Runat=server/><asp:Button Visible=false ID=next runat=server />
                        </div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kom ig�ng p� en minut!"); break;
                    case 2: HttpContext.Current.Response.Write("Get going in one minute!"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Genom att skapa ett konto f�r du fri tillg�ng till verktyg f�r stresshantering och h�lsopromotion. B�rja med att fylla i uppgifterna h�r till v�nster f�r att skapa dig en j�mf�relseprofil."); break;
                    case 2: HttpContext.Current.Response.Write("By creating an account you will have free access to tools for stress management and health promotion. Start by filling in the information below to create your comparison profile."); break;
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