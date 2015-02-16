<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendarRead.aspx.cs" Inherits="HW.calendarRead" %>
<%@ Import Namespace="HW.Core.FromHW" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(Db.header2("Kalender", "Kalender")); break;
           case 2: HttpContext.Current.Response.Write(Db.header2("Calendar", "Calendar")); break;
       }
           %>
	<script type="text/javascript">
	    function zeroPad(i) {
	        return ((i < 10) ? "0" : "") + i;
	    } 

	    $(document).ready(function () {
        
            var fromdt = new Date(<%=fromDT.Year%>,<%=fromDT.Month-1%>,<%=fromDT.Day%>);
            var todt = new Date(<%=toDT.Year%>,<%=toDT.Month-1%>,<%=toDT.Day%>);

	        $(function () {
	            var dates = $("#frompicker, #topicker").datepicker({
	                changeMonth: true,
	                numberOfMonths: 1,
	                onSelect: function (selectedDate) {
	                    var option = this.id == "frompicker" ? "minDate" : "maxDate",
      					instance = $(this).data("datepicker"),
      					date = $.datepicker.parseDate(
      						instance.settings.dateFormat ||
      						$.datepicker._defaults.dateFormat,
      						selectedDate, instance.settings);
	                    dates.not(this).datepicker("option", option, date);
	                    /* update the date thingys to show the new date */
	                    var d = new Date($("#frompicker").val());
	                    if (d.getFullYear()) {
                            fromdt = d;
	                        $(".from.year span").html(d.getFullYear());
	                        $(".from.month span").html(d.getMonth() + 1);
	                        $(".from.day span").html(d.getDate());
	                    }
	                    d = new Date($("#topicker").val());
	                    if (d.getFullYear()) {
                            todt = d;
	                        $(".to.year span").html(d.getFullYear());
	                        $(".to.month span").html(d.getMonth() + 1);
	                        $(".to.day span").html(d.getDate());
	                    }
	                }
	            });
	        });

	        $("#updateDate").click(function () {
	            location.href = 'calendarRead.aspx?FD=' + fromdt.getFullYear() + '' + zeroPad(fromdt.getMonth() + 1) + '' + zeroPad(fromdt.getDate()) + '&TD=' + todt.getFullYear() + '' + zeroPad(todt.getMonth() + 1) + '' + zeroPad(todt.getDate());
	        })
	        //
	        $(".var.from").click(function () {
	            $("#frompicker").focus();
	        })
	        $(".var.to").click(function () {
	            $("#topicker").focus();
	        })

	        /** controls the redigera button (.edit) */
//	        function editPost(post) {
//	            $("#messagetext").val(post.find(".message").find("p").html());
//	            $("#moodchoser").find("." + (post.find(".message").find(".mood").attr("class").split("mood ")[1])).find("input").click()


//	        }
//	        $(".edit").click(function () {
//	            if ($("#messagetext").val()) {
//	                if (confirm("Du har ett osparat inlägg \n\nFörkasta inlägget och redigera detta istället?")) {
//	                    editPost($(this).parent().parent());
//	                }
//	            } else {
//	                editPost($(this).parent().parent());
//	            }
//	        });


	        /* Date operations */
//	        var now = new Date()
//	        var days = ["Söndag", "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", "Lördag"]
//	        var months = ["Januari", "Februari", "Mars", "April", "Maj", "Juni", "Juli", "Augusti", "September", "November", "December"]
//	        var relevantNow = [days[now.getDay()], now.getDate(), months[now.getMonth()], now.getFullYear()];


//	        $(".post.new .day_").html(relevantNow[0]);
//	        $(".post.new .date_").html(relevantNow[1]);
//	        $(".post.new .month_").html(relevantNow[2]);
//	        $(".post.new .year_").html(relevantNow[3]);


	        /*  */
//	        $(".activity .remove").click(function () {
//	            if (confirm("Är du säker på att du vill ta bort aktiviteten?\n\nDetta går inte att ångra")) {
//	                $(this).parent().remove();
//	            }
//	        });

	        /** More control */
	        /*  expandLines takes number lines that should be expanded as an argument, this should be computed
	        by you guys, I know you can, you rock! */
	        /*  You do that by if you decide to render a .moretoggle button/link, please add the number of lines you 
	        want to expand as the first thing in the class=, like class="2 moretoggle" */
	        $(".moretoggle").toggle(function () {
	            isExpanded = $(this).parent().parent().hasClass("expanded")
	            if (!isExpanded) {
	                expandLines = parseInt($(this).attr("class").split(" ")[0]);
	                currentMessageHeight = $(this).parent().parent().find(".message").css("height").split("px")[0]
	                newMessageHeight = ((parseInt(currentMessageHeight) + (expandLines * 26)) + "px");

	                currentActivitiesHeight = $(this).parent().parent().find(".activities").css("height").split("px")[0]
	                newActivitiesHeight = ((parseInt(currentActivitiesHeight) + (expandLines * 26)) + "px");

	                $(this).parent().parent().find(".message").animate({ height: newMessageHeight }, 200)
	                $(this).parent().parent().find(".activities").animate({ height: newActivitiesHeight }, 200)
	                $(this).parent().parent().addClass("expanded");
	                $(this).parent().parent().find(".moretoggle").html("<%=showLess() %>")
	                $(this).parent().parent().find(".moretoggle").css("background", "url('../includes/resources/greyarrow_reverse.png') no-repeat center right")
	            }

	        }, function () {
	            isExpanded = $(this).parent().parent().hasClass("expanded")
	            if (isExpanded) {
	                expandLines = parseInt($(this).attr("class").split(" ")[0]);
	                currentMessageHeight = $(this).parent().parent().find(".message").css("height").split("px")[0]
	                newMessageHeight = ((parseInt(currentMessageHeight) - (expandLines * 26)) + "px");

	                currentActivitiesHeight = $(this).parent().parent().find(".activities").css("height").split("px")[0]
	                newActivitiesHeight = ((parseInt(currentActivitiesHeight) - (expandLines * 26)) + "px");

	                $(this).parent().parent().find(".message").animate({ height: newMessageHeight }, 200)
	                $(this).parent().parent().find(".activities").animate({ height: newActivitiesHeight }, 200)
	                $(this).parent().parent().removeClass("expanded");
	                $(this).parent().parent().find(".moretoggle").html("<%=showMore() %>")
	                $(this).parent().parent().find(".moretoggle").css("background", "url('../includes/resources/greyarrow.png') no-repeat center right")
	            }
	        })

	    })
	</script>
	<style>
	  #ui-datepicker-div {
	    z-index: 99 !important;
	    top: 295px !important;
	  }
	</style>
  </head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
<form id="Form1" method="post" runat="server">
        <input type="hidden" name="MCID" id="MCID" value="0" />
	    <input type="hidden" name="DeleteUMID" id="DeleteUMID" value="0" />
	    <input type="hidden" name="DeleteUPRUA" id="DeleteUPRUA" value="0" />
		<div class="container_16 myhealth diary read<%=Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=Db.nav2()%>
        </div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			  <div id="newentrygroup">
			    <h1 class="header"><%=pageHeader() %></h1>
			    <div id="nav">
			      <%=menu() %>
			    </div>
			    <div class="addgroup">
			      <div class="post new">
				      <div class="title"><%=timePeriod() %></div>
				      <div class="timespace">
				        <%=timeSpace() %>
				      </div> <!-- end .timespace -->
				      <div class="timeinput">
				        <input id="frompicker" type="text" /><input id="topicker" type="text" />
			        </div>
				    </div><!-- end .post.new -->
			    </div>
			 
			  </div>
			  <div>
			    <div class="currentform">
			      <!--<span class="string">
			        <span class="lastform">H&auml;ndelser:</span> 130 
			        <span>- Tidsperiod:</span> 2007-02-03 - 2011-02-03
			        <span>- Sortering:</span> 
			      </span>
			      <span id="sorter"><a class="active" href="#"><span>Senaste</span></a><a href="#"><span>Fr&aring;n b&ouml;rjan</span></a></span>--><a href="#" onclick="window.print();return false;" class="print"><%=print() %></a>
			    </div><!-- end .currentform -->
			  </div>
	
				<div class="content">
				    <asp:PlaceHolder ID=posts runat=server />
				    
					    <!--
                        <div class="disclaimer">
					      <div class="paginationgroup">Sida 1 av 13
					          <a class="page">&lt;</a><a class="page active">1</a><a class="page">2</a><a class="page">3</a><a class="page">4</a><a class="page">5</a><a class="page">6</a><a class="page">7</a><a class="page">&gt;</a>
					      </div>
					    </div>-->
				    </div><!-- end .results -->
				<div class="bottom"></div>
		</div><!-- end .contentgroup	-->
		<%=Db.bottom2()%>
        </div> <!-- end .container_12 -->
		</form>
  </body>
</html>
