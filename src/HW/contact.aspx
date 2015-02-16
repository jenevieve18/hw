<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="HW.contact" %>
<%@ Import Namespace="HW.Core.FromHW" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("Kontakta oss", "HealthWatch c/o Interactive Health Group in Stockholm AB, Box 4047, 10261 Stockholm, Sweden")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("Contact us", "HealthWatch c/o Interactive Health Group in Stockholm AB, Box 4047, 10261 Stockholm, Sweden")); break;
       }
           %>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides about<%=Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=Db.nav2()%>
			</div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kontaktinformation"); break;
                    case 2: HttpContext.Current.Response.Write("Contact information"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div><p>Interactive Health Group in Stockholm AB<br />Box 4047<br />10261 Stockholm</br>Sweden</p>
                        <br />
		<p><b><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ansvarig utgivare"); break;
                    case 2: HttpContext.Current.Response.Write("Publisher"); break;
                }
             %></b><br /><A class="lnk" HREF="javascript:dm('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')"><script language="javascript">                                                                                                                                                                                                                                de('6179 4781 4334 859 4619 1866 757 3663 4851 5264 859 6104 4548 3876 859 25 25 3663 6104 4636 3876 4599 859 1866 757 3876 3797 859 757 5506 3876 4548 25 4599')</script></A></p>
		<br /><p><b><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Hjälp med tjänsten"); break;
                    case 2: HttpContext.Current.Response.Write("Support"); break;
                }
             %></b><br /><A class="lnk" HREF="javascript:dm('8051 3149 5653 6790 79 4493 8031 7622 7044 105 5208 3260 3260 7622 2202 8031 4107 2742 3061 6790 4493 8031 2742 2253 6790 8031 2942 2742 2294 105 3061')"><script language="javascript">                                                                                                                                                                                                                           de('8051 3149 5653 6790 79 4493 8031 7622 7044 105 5208 3260 3260 7622 2202 8031 4107 2742 3061 6790 4493 8031 2742 2253 6790 8031 2942 2742 2294 105 3061')</script></A></p>
		<br /><p><b><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Media, sponsring, annonser & företagstjänster"); break;
                    case 2: HttpContext.Current.Response.Write("Public relations"); break;
                }
             %></b><br /><A class="lnk" HREF="javascript:dm('2323 1467 1118 2057 771 646 2163 1707 2303 771 2244 1920 1707 1968 532 1212 2057 646 2163 532 984 2057 2163 1608 532 2093 1633 1212')"><script language="javascript">                                                                                                                                                                                                        de('2323 1467 1118 2057 771 646 2163 1707 2303 771 2244 1920 1707 1968 532 1212 2057 646 2163 532 984 2057 2163 1608 532 2093 1633 1212')</script></A>
             </p>
             <div class="bottom"></div>
					</div> <!-- end .main -->
					<div class="sidebar">
                    <h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kontakta oss"); break;
                    case 2: HttpContext.Current.Response.Write("Contact us"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Välkommen att kontakta oss när du vill. Alla frågor är bra frågor."); break;
                    case 2: HttpContext.Current.Response.Write("Please feel free to contact us at any time."); break;
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