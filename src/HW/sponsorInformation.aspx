<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sponsorInformation.aspx.cs" Inherits="HW.sponsorInformation" %>
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
			 
				    <h1 class="header"></h1>
                    <div class="content">
					<div class="main">
						<div class="top"></div>
		            <asp:Label ID=contents Runat=server/>
                    <div class="bottom"></div>
					</div> <!-- end .main -->
					
					<div class="bottom"></div>
				</div>
		    </div><!-- end .contentgroup	-->
		<%=healthWatch.Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>
