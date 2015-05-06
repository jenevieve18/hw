<%@ Page language="c#" Inherits="healthWatch.home" Codebehind="home.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
  <%=healthWatch.Db.header2(getTitle(), getDesc())%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="form1" method="post" runat="server">

<div class="container_16 index<%=healthWatch.Db.cobranded() %>">
		<div class="headergroup grid_16">
	   <%=healthWatch.Db.nav2()%>
		</div> <!-- end .headergroup -->
        <% if (!categorySupplied)
           { %>
		<div class="datagroup grid_16">
           <%=rightNow()%>
		</div> <!-- end .datagroup -->
        <% } %>
		<div class="contentgroup grid_16">
        <% if (categorySupplied)
           { %>
		<div><br /></div>
        <% } %>
			<div class="articles grid_12 alpha">
				<div class="grid_6 alpha column one">
			        <asp:Label ID="left" EnableViewState=false runat="server" />
				</div><!-- .column-->
				<div class="grid_6 alpha omega column two">
			        <asp:Label ID="right" EnableViewState=false runat="server" />
				</div>
			</div><!-- end column -->
			
			<div class="sidebar grid_4 omega">
				<div class="box" id="wisdom">
					<%=healthWatch.Db.wiseOfToday() %>
				</div>
				<%=splash() %>
				<div class="box">
					<%=healthWatch.Db.mostRead()%>
				</div>
			</div>
		</div>
		
		<%=healthWatch.Db.bottom2() %>
	</div> <!-- end .container_12 -->
	<!--<img style="position: absolute; top: 0; left: 46px; z-index: -1" id="underlay" src="/01.png" />-->
	<!--<button id="hide" style="position: absolute; top: 0; left: 0; z-index: 99">Hide image</button><button id="hide_ol" style="position: absolute; top: 0; left: 100px; z-index: 99">Hide html</button>-->
		</form>
</body>
</html>
