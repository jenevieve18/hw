<%@ Page language="c#" Inherits="healthWatch.news" Codebehind="news.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=healthWatch.Db.header2(getTitle(),getTeaser())%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
	<div class="container_16 index detail<%=healthWatch.Db.cobranded() %>">
		<div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
        </div> <!-- end .headergroup -->

        <div class="contentgroup grid_16">
            <div class="articles grid_12 alpha">
                <div class="article first">
                    <asp:Label EnableViewState=false ID="article" runat="server" />
                </div><!-- end .article.first -->
            </div><!-- end column -->
			
            <div class="sidebar grid_4 omega">
                <div class="box">
                <%=healthWatch.Db.mostRead()%>
                </div>
            </div>
        </div>
	    <%=healthWatch.Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>