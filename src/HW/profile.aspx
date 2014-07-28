<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="HW.profile" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Konto och jämförelseprofil", "Konto och jämförelseprofil")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Account and comparison profile", "Account and comparison profile")); break;
       }
           %>
           <script type="text/javascript" src="profile.js"></script>
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
         case 1: HttpContext.Current.Response.Write((guest ? "Gör detta till ditt konto" : "Konto och jämförelseprofil"));
    break;
         case 2: HttpContext.Current.Response.Write((guest ? "Turn this into your account" : "Account and comparison profile"));
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
                            <asp:Button ID=submit Runat=server/>
                        </div>
					</div> <!-- end .main -->
					<div class="sidebar">
                        <h3><%
		                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1: HttpContext.Current.Response.Write("Vill du avsluta ditt konto?"); break;
                                case 2: HttpContext.Current.Response.Write("Do you want to remove your account?"); break;
                            }
                         %></h3><br />
                        <asp:PlaceHolder ID=DeleteAccount runat=server />
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
