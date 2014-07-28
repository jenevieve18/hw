<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="HW.calendar" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Kalender", "Kalender")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Calendar", "Calendar")); break;
       }
           %>
	<script type="text/javascript">
        function zeroPad(i) {
            return ((i < 10) ? "0" : "") + i;
        } 

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

	        /** controls the redigera button (.edit) */
//	        function editPost(post) {
//	            /* This function takes the html representation of a previous post */
//	            /* like this:
//	            <div class="message">
//	            <div class="mood happy"></div> <-- mood
//	            <p>Kära dagboken... idag var igen en jättebra dag. Barnen var snälla och solen sken... hoppas det blir mera sådana dagar i framtiden!</p> <-- post body
//	            </div>
//	            */

//	            if ($("#messagetext").val()) {
//	                if (confirm("Du har ett osparat inlägg \n\nFörkasta inlägget och redigera detta istället?")) {
//	                    $("#messagetext").val(post.find(".message").find("p").html());
//	                    $("#moodchoser").find("." + (post.find(".message").find(".mood").attr("class").split("mood ")[1])).find("input").click();
//	                }
//	            } else {
//	                $("#messagetext").val(post.find(".message").find("p").html());
//	                $("#moodchoser").find("." + (post.find(".message").find(".mood").attr("class").split("mood ")[1])).find("input").click();
//	            }
//	        }

	        $(".post .edit").click(function () {
	            isNew = $(this).parent().parent().hasClass("new");
	            if (!isNew) {
	                editPost($(this).parent().parent());
	            } else {
	                // it is the "new" one
	                $(this).parent().find("#dateStr").datepicker({ firstDay: 1,
	                    dayNamesMin: <%=shortDays() %>
	                });
	                $(this).parent().find("#dateStr").focus();
	            }
	        });


	        /* Date operations */
	        var now = new Date(<%=dt.Year%>,<%=dt.Month-1%>,<%=dt.Day%>);
	        var days = <%=days() %>;
	        var months = <%=months() %>;
	        var relevantNow = [days[now.getDay()], now.getDate(), months[now.getMonth()], now.getFullYear()];


	        $(".post.new .day_").html(relevantNow[0]);
	        $(".post.new .date_").html(relevantNow[1]);
	        $(".post.new .month_").html(relevantNow[2]);
	        $(".post.new .year_").html(relevantNow[3]);

	        function updateNewDate(d) {
	            $(".post.new .date .day_").html(d[0]);
	            $(".post.new .date .date_").html(d[1]);
	            $(".post.new .date .month_").html(d[2]);
	            $(".post.new .date .year_").html(d[4]);
	        }

	        /* Datepicker returned data */
	        $("#dateStr").change(function () {
	            now = new Date($(this).val());
                location.href='calendar.aspx?D='+now.getFullYear()+''+zeroPad(now.getMonth()+1)+''+zeroPad(now.getDate());
//	            var relevantDate = [days[now.getDay()], now.getDate(), months[now.getMonth()], now.getFullYear()];
//	            updateNewDate(relevantDate)
	            //fetch that dates post
	            /*$.get("blah.asp", { date }, function(data) {
	            editPost(data); <-- note that data needs to be formatted quite perculiarly, 
	            check that function for more info
	            })*/
	        })

	        /* Controls the next and prev buttons */
	        $(".tab.next").click(function () {
                now.setDate(now.getDate()+1);
	            location.href='calendar.aspx?D='+now.getFullYear()+''+zeroPad(now.getMonth()+1)+''+zeroPad(now.getDate());
	        })
	        $(".tab.prev").click(function () {
	            now.setDate(now.getDate()-1);
	            location.href='calendar.aspx?D='+now.getFullYear()+''+zeroPad(now.getMonth()+1)+''+zeroPad(now.getDate());
	        })

	        /*
	        $(".activity .remove").click(function () {
	        if (confirm("Är du säker på att du vill ta bort aktiviteten?\n\nDetta går inte att ångra")) {
	        $(this).parent().remove();
	        }
	        });  */

	        /** More control */
	        /*  expandLines takes number lines that should be expanded as an argument, this should be computed
	        by you guys, I know you can, you rock! */
	        /*  You do that by if you decide to render a .moretoggle button/link, please add the number of lines you 
	        want to expand as the first thing in the class=, like class="2 moretoggle" */
//	        $(".moretoggle").toggle(function () {
//	            isExpanded = $(this).parent().parent().hasClass("expanded")
//	            if (!isExpanded) {
//	                expandLines = parseInt($(this).attr("class").split(" ")[0]);
//	                currentMessageHeight = $(this).parent().parent().find(".message").css("height").split("px")[0]
//	                newMessageHeight = ((parseInt(currentMessageHeight) + (expandLines * 26)) + "px");

//	                currentActivitiesHeight = $(this).parent().parent().find(".activities").css("height").split("px")[0]
//	                newActivitiesHeight = ((parseInt(currentActivitiesHeight) + (expandLines * 25)) + "px");

//	                $(this).parent().parent().find(".message").animate({ height: newMessageHeight }, 200)
//	                $(this).parent().parent().find(".activities").animate({ height: newActivitiesHeight }, 200)
//	                $(this).parent().parent().addClass("expanded");
//	                $(this).parent().parent().find(".moretoggle").html("Visa färre")
//	                $(this).parent().parent().find(".moretoggle").css("background", "url('../includes/resources/greyarrow_reverse.png') no-repeat center right")
//	            }

//	        }, function () {
//	            isExpanded = $(this).parent().parent().hasClass("expanded")
//	            if (isExpanded) {
//	                expandLines = parseInt($(this).attr("class").split(" ")[0]);
//	                currentMessageHeight = $(this).parent().parent().find(".message").css("height").split("px")[0]
//	                newMessageHeight = ((parseInt(currentMessageHeight) - (expandLines * 26)) + "px");

//	                currentActivitiesHeight = $(this).parent().parent().find(".activities").css("height").split("px")[0]
//	                newActivitiesHeight = ((parseInt(currentActivitiesHeight) - (expandLines * 25)) + "px");

//	                $(this).parent().parent().find(".message").animate({ height: newMessageHeight }, 200)
//	                $(this).parent().parent().find(".activities").animate({ height: newActivitiesHeight }, 200)
//	                $(this).parent().parent().removeClass("expanded");
//	                $(this).parent().parent().find(".moretoggle").html("Visa mera")
//	                $(this).parent().parent().find(".moretoggle").css("background", "url('../includes/resources/greyarrow.png') no-repeat center right")
//	            }
//	        })

	        /** Add dialog related */

	        /* displays add dialog*/
	        $(".activities .add").click(function () {
	            $("#add").show();
	        })
	        /* aborts add */
	        $("#add .cancel").click(function () {
	            $("#add").hide();
	            $("#add #sub").hide();
	            $("input[name='MCID']").val("0");
	        });

	        /* shows a form based on selection of value */
	        /* TODO make forms */
	        $("#selector dd ul li").click(function () {
	            //alert("should show form for:" + $(this).attr('id')); //I dunno how, probably preload all of them and 
	            // class the form and do as next..
	            var active = $(this).attr('id');
	            $("input[name='MCID']").val(active.substr(1));
	            $("li[id^='MC']").hide();
	            $("li[id^='M" + active + "_']").show();
	            $("#add #sub").show();
	        });
	    })
	</script>
	<style>
	  .ui-datepicker {
	    margin-top: -23px;
	    margin-left: -2px;
	  }
	</style>
    <script language=javascript>
        /*
        function c(id)
        {
        if(id < document.forms[0].a1.value)
        {
        switch(id)
        {
        case 0 : actS(0,0,0); break;
        case 1 : actS(1,document.forms[0].a2.value,0); break;
        case 2 : actS(2,document.forms[0].a2.value,document.forms[0].a3.value); break;
        case 3 : actS(3,document.forms[0].a2.value,document.forms[0].a3.value); break;
        }
        }
        }
        function setDDL(id,i)
        {
        eval('document.forms[0].'+id+'.selectedIndex = '+i);
        }
        function d(id)
        {
        return document.getElementById(id);
        }
        function actA(i1)
        {
        for(i = 0; i <= 3; i++)
        {
        d('act'+i).className = 'actHead' + (i == 0 ? 'First' : '') + (i == i1 ? 'A' : '');
        }
        }
        function actH(i1,i2,i3)
        {
        obj = null;
        if(obj = d('act'+i1+'_'+i2+'_'+i3))
        {
        obj.style.display = 'none';
        }
        return (obj != null);
        }
        function actG(m,i2,i3,t)
        {
        d('act3_0_0').innerHTML = '';
        if(m.indexOf(':') == -1)
        {
        d('act3_0_0').innerHTML += '<A HREF="JavaScript:void(window.open(\'calendarGraphOverTime.aspx?MUID=' + m + '&R=' + Math.random() + '&T=' + t + '&W=960&H=480\',\'\',\'width=1000,height=520\'));"><img border="0" alt="<%
        switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
        {
        case 1: HttpContext.Current.Response.Write("Klicka för större bild..."); break;
        case 2: HttpContext.Current.Response.Write("Click for larger image..."); break;
        }
        %>" id="GraphImg" src="calendarGraphOverTime.aspx?MUID=' + m + '&R=' + Math.random() + '&T=' + t + '" /></A>';
        }
        else
        {
        x = m.split(':');
        for(i=0; i < x.length; i++)
        {
        if(i != 0)
        {
        d('act3_0_0').innerHTML += '<br/>';
        }
        d('act3_0_0').innerHTML += '<A HREF="JavaScript:void(window.open(\'calendarGraphOverTime.aspx?MUID=' + x[i] + '&R=' + Math.random() + '&T=' + t + '&W=960&H=480\',\'\',\'width=1000,height=520\'));"><img border="0" alt="<%
        switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
        {
        case 1: HttpContext.Current.Response.Write("Klicka för större bild..."); break;
        case 2: HttpContext.Current.Response.Write("Click for larger image..."); break;
        }
        %>" id="GraphImg' + i + '" src="calendarGraphOverTime.aspx?MUID=' + x[i] + '&R=' + Math.random() + '&T=' + t + '" /></A>';
        }
        }
        actS(3,i2,i3);
        }
        function actS(i1,i2,i3)
        {
        actA(i1);

        if(document.forms[0].a1.value < 3)
        {
        actH(document.forms[0].a1.value,document.forms[0].a2.value,document.forms[0].a3.value);
        }
        else
        {
        actH(3,0,0);			
        }
        document.forms[0].a1.value = i1; 
        document.forms[0].a2.value = i2; 
        document.forms[0].a3.value = i3;
        if(i1 == 3)
        {
        i2 = 0; i3 = 0;
        }
        d('act'+i1+'_'+i2+'_'+i3).style.display = '';
        d('actInnerBoxTopContainer').style.display = (i1 == 2 ? '' : 'none');
        }
        */
   </script>
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
		<div class="container_16 myhealth diary<%=healthWatch.Db.cobranded() %>">
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
				      <div class="dategroup">
				        <div class="title"><%=selectDay() %></div>
				        <div class="date">
				          <div class="small"><span class="day_"></span> </div>
				          <div class="large date_"></div>
  				        <div class="small monthyear"><span class="month_"></span> <span class="year_"></span></div>
  				        <div class="tab prev"></div>
  				        <div class="tab next"></div>
				        </div>
				        <a href="#!" class="edit"><%=showCalendar() %></a>
				        <input type="text" style="visibility: hidden; position: absolute; z-index: 3;" id="dateStr" /><!--  cheating to the max-->
				      </div>
				      <div class="messagegroup">
				        <div class="title"><%=notes() %></div>
                        <asp:TextBox TextMode="MultiLine" CssClass="message" ID="messagetext" Runat="server"/>
				        <!--<a class="regular">Vill du ha tips med skrivandet?</a>-->
				      </div>
				      <div id="moodchoser" class="moods">
				        <div class="title"><%=todaysMood() %></div>
				          <ul>
				            <asp:PlaceHolder ID=mood runat=server />
				          </ul>
				      </div>
				      <div class="activities">
				        <div class="title"><%=todaysActivities() %></div>
				        <asp:PlaceHolder ID=TodaysActivities runat=server />
				        <a class="add" href="#!"><%=add() %></a>
                <div id="add">
                  <div class="addtop"></div>
                  <div class="addcontent">
                    <div id="selector" class="var">
        				      <dl class="dropdown">
                        <dt><a class="null"><span><%=selectActivity() %></span></a></dt>
                        <dd>
                          <ul>
                            <asp:PlaceHolder ID=formlinks runat=server />
                          </ul>
                        </dd>
                      </dl>
                    </div>
                    <div id="sub">
                      <ul class="subform">
                        <asp:PlaceHolder ID=subform runat=server />
                      </ul>
                        <div class="buttoncontroller">
                          <asp:LinkButton cssclass="button" id=submit1 runat=server><span>Spara</span></asp:LinkButton><a class="cancel" href="#cancel"><%=orCancel() %></a>
                        </div>
                    </div>
                  </div>
                  <div class="addbottom"></div>
                </div>
				      </div>
				    </div><!-- end .post.new -->
				    <div class="savegroup">
				      <asp:Button id=submit2 class="save" Text="Spara" runat=server />
				      <!--<a href="#" class="save"><span>Spara</span></a>
				      <a href="#" class="cancel">Eller klicka h&auml;r om du vill radera dagen.</a>-->
				    </div>
			    </div>
			 
			  </div>
              
			  <div>
			    <div class="currentform">
			      <!--<span class="string">
			        <span class="lastform">H&auml;ndelser:</span> 130 
			        <span>- Tidsperiod:</span> 2007-02-03 - 2011-02-03
			        <span>- Sortering:</span> 
			      </span>
			      <span id="sorter"><a class="active" href="#"><span>Senaste</span></a><a href="#"><span>Fr&aring;n b&ouml;rjan</span></a></span>-->
                  <a href="#" onclick="window.print();return false;" class="print"><%=print() %></a>
			    </div>
			  </div>
              
	<!--
				<div class="content">
				    <div class="post">
				      <div class="top"></div>
				      <div class="dategroup">
				        <div class="date">
				          <div class="small">Onsdag den</div>
				          <div class="large">9</div>
  				        <div class="small monthyear">Mars 2011</div>
				        </div>
				        <a href="#" class="edit">Redigera</a>
				      </div>
				      <div class="messagegroup">
				        <div class="message">
				          <div class="mood happy"></div>
 <p>abcdefghijklmnopqrstuvwxyza bcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz</p>
				        </div>
				        <a href="#!" class="2 moretoggle">Visa mera</a>
				      </div>
				      <div class="activities">
				        <div class="title">Dagens aktiviteter/m&auml;tningar</div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				      </div>
				      <span>
				        <a href="#!" class="2 moretoggle">Visa mera</a>
				      <span>
				      <div class="bottomcontainer"></div>
  			      <div class="bottom"></div>
				    </div>
				    
				    <div class="post">
				      <div class="top"></div>
				      <div class="dategroup">
				        <div class="date">
				          <div class="small">Onsdag den</div>
				          <div class="large">9</div>
  				        <div class="small monthyear">Mars 2011</div>
				        </div>
				        <a href="#" class="edit">Redigera</a>
				      </div>
				      <div class="messagegroup">
				        <div class="message">
				          <div class="mood happy"></div>
				          <p>Kära dagboken... idag var igen en jättebra dag. Barnen var snälla och solen sken... hoppas det blir mera sådana dagar i framtiden!</p>
				        </div>
				      </div>
				      <div class="activities">
				        <div class="title">Dagens aktiviteter/m&auml;tningar</div>
				        <div class="activity">
				          <span><span>9:19 Hälsa & Stress</span></span> <a class="remove"></a><a class="statstoggle"></a>
				        </div>
				      </div>
				      <span><span>
				      <div class="bottomcontainer"></div>
  			      <div class="bottom"></div>
				    </div>
					<div class="disclaimer">
					  <div class="paginationgroup">Sida 1 av 13
					      <a class="page">&lt;</a><a class="page active">1</a><a class="page">2</a><a class="page">3</a><a class="page">4</a><a class="page">5</a><a class="page">6</a><a class="page">7</a><a class="page">&gt;</a>
					  </div>
					</div>
				</div>
                -->
				<div class="bottom"></div>
		</div><!-- end .contentgroup	-->
                <!--
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td valign="top">
					
				</td>
				<td><img src="img/null.gif" width="20" height="1" /></td>
				<td valign="top">
					<div class="boxTitle" width="450px;"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Anteckningar"); break;
                    case 2: HttpContext.Current.Response.Write("Notes"); break;
				}
				%></div>
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td valign="top">
								
							</td>
							<td valign="top">
								<div class="rightInnerBox" style="height:150px;width:80px;">
									
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<br />
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td valign="top">
					<div id="actTitle"><asp:Label ID="actHeader" Runat="server"/></div>
					<div id="actBox" runat="server">
						<input type="hidden" name="a1" id="a1" runat="server" />
						<input type="hidden" name="a2" id="a2" runat="server" />
						<input type="hidden" name="a3" id="a3" runat="server" />
						<div id="act0_0_0" class="actInnerBox" runat="server" style="display:none;"></div>
						<div id="act3_0_0" class="actInnerBox" runat="server" style="display:none;"></div>
						<div id="actInnerBoxTopContainer" style="display:none;">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<div id="actInnerBoxTop" runat="server"></div>
								</td>
								<td><img src="img/null.gif" width="74" height="1" /></td>
								<td>
									
								</td>
							</tr>
						</table>
						</div>
					</div>
				</td>
				<td><img src="img/null.gif" width="20" height="1" /></td>
				<td valign="top">
					<div class="boxTitle" style="width:270px;"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Dagens aktiviteter/mätningar"); break;
                    case 2: HttpContext.Current.Response.Write("Todays notes/measures"); break;
				}
				%></div>
					<div class="box" style="width:270px;"><div id="actsBox" runat="server"></div></div>
				</td>
			</tr>
		</table>
        -->
		
		<%=healthWatch.Db.bottom2()%>
        </div> <!-- end .container_12 -->
		</form>
  </body>
</html>

