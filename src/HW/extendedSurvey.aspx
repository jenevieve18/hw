<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.extendedSurvey" Codebehind="extendedSurvey.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Enk�t", "Enk�t")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Survey", "Survey")); break;
       }
           %>
   <script language="javascript">
        var pop = null;
        document.domain='healthwatch.se';
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
        <div class="statschosergroup">
          <h1 class="header" id=mainHeader runat=server></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
		<asp:Label ID="Survey" runat=server />
        </div> <!-- end .main -->
					<div class="sidebar">
						<h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Mer information"); break;
                    case 2: HttpContext.Current.Response.Write("More information"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("N�r du klickar p� l�nken till enk�ten h�r bredvid s� �ppnas den i ett nytt f�nster. V�nligen beh�ll detta f�nster �ppet i bakgrunden tills du f�rdigst�llt enk�ten."); break;
                    case 2: HttpContext.Current.Response.Write("After clicking the link to the survey it will open in a new window. Please keep this window open in the background until you've completed the survey."); break;
                    
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