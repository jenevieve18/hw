<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="exercise.aspx.cs" Inherits="HW.exercise" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("Övningar", "Övningar")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("Exercises", "Exercises")); break;
       }
           %>
           <script type="text/javascript">
               $(document).ready(function () {
                   /** controls the dropdowns **/
                   $(".dropdown dt a").click(function () {
                       if ($(this).parent().parent().find("dd ul").hasClass("activated")) {
                           $(".dropdown dd ul").hide();
                           $(".activated").removeClass("activated")
                       } else {
                           $(".activated").removeClass("activated")
                           $(".dropdown dd ul").hide();
                           $(this).parent().parent().find("dd ul").toggle();
                           $(this).parent().parent().find("dd ul").addClass("activated")
                       }
                   });

                   $(".dropdown dd ul li a").click(function () {
                       var text = $(this).html();
                       $(this).parent().parent().parent().parent().find("dt a span").html(text);
                       $(".dropdown dd ul").hide();
                       //the id field of parent has the control hook, although I'd use a hrefs if you don't want to ajax
                       //alert($(this).parent().attr('id'))
                   });

                   $(document).bind('click', function (e) {
                       var $clicked = $(e.target);
                       if (!$clicked.parents().hasClass("dropdown")) {
                           $(".dropdown dd ul").hide();
                           $(".activated").removeClass("activated")
                       }
                   });

                   /** controls bar details **/
                   /*$(".bar .detailtoggle").click(function() {
                   $(this).parent().find(".detail").slideUp('fast', function() {
                   $(this).parent().removeClass("active");
                   })
		      
                   });
                   $(".bar:not(.active)").click(function() {
                   if(!$(this).hasClass("active")) {
                   $(this).find(".detail").slideDown('fast', function() {
                   $(this).parent().addClass("active");
                   })
                   }
                   });*/
                   $(".bar .overview").toggle(function () {
                       $(this).parent().find(".detail").slideDown('fast', function () {
                           $(this).parent().addClass("active");
                       })
                   }, function () {
                       $(this).parent().find(".detail").slideUp('fast', function () {
                           $(this).parent().removeClass("active");
                       })
                   })

               })
	</script>
  </head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides exercises<%=healthWatch.Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=Db.nav2()%>
        </div> <!-- end .headergroup -->
      <div class="contentgroup grid_16">
        <div class="statschosergroup">
          <h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Övningar"); break;
                    case 2: HttpContext.Current.Response.Write("Exercises"); break;
                }
             %></h1><a name=filter></a>
			    <div class="statschoser">
			      <div class="filter misc">
			        <div class="title"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Välj kategori"); break;
                    case 2: HttpContext.Current.Response.Write("Choose category"); break;
                }
             %></div>
			 <dl class="dropdown"><asp:PlaceHolder ID=CategoryID runat=server/></dl>
            </div>
            <div class="filter misc">
			        <div class="title"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Välj typ"); break;
                    case 2: HttpContext.Current.Response.Write("Choose type"); break;
                }
             %></div>
			        <dl class="dropdown"><asp:PlaceHolder ID=TypeID runat=server/></dl>
            </div>
			    </div>
			  </div>
              <div>
			    <div class="currentform">
			      <span class="lastform"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write(BX + " övningar - Sortering:"); break;
                    case 2: HttpContext.Current.Response.Write(BX + " exercises - Order:"); break;
                }
             %></span>
			      <div class="forms">
                    <asp:PlaceHolder ID=Sort runat=server />
			    </div>
			    </div>
			  </div>
				<div class="results">
				  <div class="largelegend">
				  	<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("<!--Övningsdelen är tänkt att kunna erbjuda ett brett urval av övningar som " +
			                    "syftar till att hantera skadlig stress samt främja hälsa och välbefinnande. " +
			                    "<br /><br />-->" +
			                    "För att uppnå resultat är det viktigt att inte bara titta igenom övningarna " +
			                    "utan också genomföra dem regelbundet. Om du helhjärtat utför övningarna " +
			                    "några gånger brukar det vara tillräckligt för att de ska integreras i " +
			                    "dig som ett automatiskt beteende. På så vis kan du påverka ditt beteende, " +
			                    "värderingar och annat som kan bidra till att öka din livskvalitet."); break;
                    case 2: HttpContext.Current.Response.Write("<!--This section offers a broad selection of exercises " +
                                "that aim to manage and prevent harmful stress, and promote health and well-being. " +
                                "<br /><br />-->" +
                                "To achieve results, it's important not only to browse the exercises but to execute them on a regular basis. " +
                                "If you dedicate time for this during the first couple of times, it's common that the changes of " +
                                "behaviour and thought becomes an integrated part of your daily life. Thus, after some time increases your " +
                                "quality of life without any further effort. "); break;
                }
             %>
				  </div>
				  
				  <div class="contentlist">

                  <asp:PlaceHolder ID="ExerciseList" runat="server" />

                  </div><!-- end .contentlist -->
                  <!--
					<div class="disclaimer">
					  <div class="paginationgroup">Sida 1 av 13
					      <a class="page">&lt;</a><a class="page active">1</a><a class="page">2</a><a class="page">3</a><a class="page">4</a><a class="page">5</a><a class="page">6</a><a class="page">7</a><a class="page">&gt;</a>
					  </div>
					</div>
                    -->
				</div><!-- end .results -->
				<div class="bottom"></div>
		</div><!-- end .contentgroup	-->
        
		<%=Db.bottom2()%>
        </div> <!-- end .container_12 -->
		</form>
  </body>
</html>
