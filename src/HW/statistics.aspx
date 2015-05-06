<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="statistics.aspx.cs" Inherits="healthWatch.statistics" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Statistik", "Statistik")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Statistics", "Statistics")); break;
       }
           %>
           <script type="text/javascript">
               /*
               function selectAllIndexMulti()
               {
               var y, str = ":", i = 0;
               if(!document.getElementById('Submission'+str+i))
               {
               str = "_";
               }
               while(y = document.getElementById('IndexMulti'+str+(i++)))
               {
               y.checked = true;
               }
               }
               function selectFirstSubmission()
               {
               var y, str = ":", i = 0;
               if(!document.getElementById('Submission'+str+i))
               {
               str = "_";
               }
               while(y = document.getElementById('Submission'+str+i))
               {
               y.checked = (i++ == 0 ? true : false);
               }
               }
               function selectAllSubmission()
               {
               var y, str = ":", i = 0;
               if(!document.getElementById('Submission'+str+i))
               {
               str = "_";
               }
               while(y = document.getElementById('Submission'+str+(i++)))
               {
               y.checked = true;
               }
               }
               function getCompare()
               {
               for(var i=0; i < document.forms[0].Compare.length; i++)
               {
               if(document.forms[0].Compare[i].checked)
               {
               return document.forms[0].Compare[i].value;
               }
               }
               return 0;
               }
               function setCompare(x)
               {
               for(var i=0; i < document.forms[0].Compare.length; i++)
               {
               if(parseInt(document.forms[0].Compare[i].value) == x)
               {
               document.forms[0].Compare[i].checked = true;
               }
               }				
               }
               function toggleSettings(x)
               {
               var y = parseInt(document.forms[0].ShowSettings.value);
               if(x) { y = (y == 1 ? 0 : 1); document.forms[0].ShowSettings.value = y; }
               document.getElementById('Settings').style.display = (y == 1 ? '' : 'none');
               document.getElementById('SettingsLink').innerHTML = (y == 1 ? <%
               switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
               {
               case 1: HttpContext.Current.Response.Write("'Dölj avancerade inställningar &raquo;' : 'Visa avancerade inställningar &raquo;'"); break;
               case 2: HttpContext.Current.Response.Write("'Hide advanced settings &raquo;' : 'Show advanced settings &raquo;'"); break;
               }
               %>);
               }
               function toggleIndex()
               {
               var y = (getCompare() == 1);
               document.getElementById('IndexOne').style.display = (y ? 'none' : '');
               document.getElementById('IndexMulti').style.display = (y ? '' : 'none');
               document.getElementById('UpdateIndexesSeparator').style.display = (y ? '' : 'none');
               document.getElementById('UpdateIndexes').style.display = (y ? '' : 'none');
               }
               function shortcut(x)
               {
               switch(x)
               {
               case 1:
               {
               setCompare(1);
               selectAllIndexMulti();
               selectFirstSubmission();
               break;
               }
               case 2:
               {
               setCompare(1);
               selectAllIndexMulti();
               selectAllSubmission();
               break;
               }
               case 3:
               {
               setCompare(2);
               selectFirstSubmission();
               break;
               }
               case 4:
               {
               setCompare(2);
               selectAllSubmission();
               break;
               }
               }
               toggleIndex();
               document.forms[0].Refresh.value = 1;
               document.forms[0].submit();
               }
               function showExpl(x)
               {
               var currentMiddle = 0, currCenter = 0;
               if(document.documentElement && document.documentElement.clientHeight && document.documentElement.clientWidth)
               {
               currentMiddle = document.documentElement.scrollTop + document.documentElement.clientHeight/2;
               currentCenter = document.documentElement.scrollLeft + document.documentElement.clientWidth/2;
               }
               else
               {
               currentMiddle = document.body.scrollTop + document.body.clientHeight/2;
               currentCenter = document.body.scrollLeft + document.body.clientWidth/2;
               }
				
               pW = document.getElementById('expl'+x);
               pW.style.top = Math.round(currentMiddle - 200) + 'px';
               pW.style.left = Math.round(currentCenter - 200) + 'px';
               pW.style.display = '';
               }
               function hideExpl(x)
               {
               document.getElementById('expl'+x).style.display = 'none';
               }*/
               $(document).ready(function () {
                   /*
                   $(".when .custom").click(function () {
                   $(".currentform .period").html("<span class='period'>Från <select><option>2007</option></select><select><option>10</option></select><select><option>10</option></select> Till <select><option>2007</option></select><select><option>10</option></select><select><option>10</option></select></span>");
                   });
                   $(".when .else").click(function () {
                   $(".currentform .period").html($(this).find("a").html());
                   });*/

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
  <input type="hidden" id="ShowSettings" name="ShowSettings" value="0" runat=server />
  <input type="hidden" id="Refresh" name="Refresh" value="0" />
  <div class="container_16 myhealth statistics<%=healthWatch.Db.cobranded() %>">
      <div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
		</div> <!-- end .headergroup -->
      <div class="contentgroup grid_16">
        <div class="statschosergroup">
          <h1 class="header"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Statistik"); break;
				    case 2: HttpContext.Current.Response.Write("Statistics"); break;
				}
				%></h1>
                <!--
		<div class="boxTitle" style="width:707px;"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Inställningar"); break;
				    case 2: HttpContext.Current.Response.Write("Settings"); break;
				}
				%></div>
		<div class="box" style="width:707px;">
			<table border="0" cellpadding="0" cellspacing="0" class="txt">
				<tr>
					<td width="150"><a class="lnk" href="JavaScript:shortcut(1);"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Senaste mätning"); break;
				    case 2: HttpContext.Current.Response.Write("Last measure"); break;
				}
				%> &raquo;</a>&nbsp;&nbsp;</td>
					<td width="350"><a class="lnk" href="JavaScript:shortcut(3);"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Senaste mätning jämfört med andra"); break;
				    case 2: HttpContext.Current.Response.Write("Last measure compared to others"); break;
				}
				%> &raquo;</a>&nbsp;&nbsp;</td>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;<%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Formulär"); break;
				    case 2: HttpContext.Current.Response.Write("Forms"); break;
				}
				%>&nbsp;<asp:DropDownList ID=SurveyKey AutoPostBack CssClass="txt" runat=server /></td>
				</tr>
				<tr><td colspan="3"><img src="img/null.gif" width="1" height="3" /></td></tr>
				<tr>
					<td><a class="lnk" href="JavaScript:shortcut(2);"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Utveckling över tid"); break;
				    case 2: HttpContext.Current.Response.Write("Development over time"); break;
				}
				%> &raquo;</a>&nbsp;&nbsp;</td>
					<td><a class="lnk" href="JavaScript:shortcut(4);"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Utveckling över tid jämfört med andra"); break;
				    case 2: HttpContext.Current.Response.Write("Development over time compared to others"); break;
				}
				%> &raquo;</a>&nbsp;&nbsp;</td>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;<a class="lnk" id="SettingsLink" href="JavaScript:toggleSettings(true);"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Visa avancerade inställningar"); break;
				    case 2: HttpContext.Current.Response.Write("Show advanced settings"); break;
				}
				%> &raquo;</a></td>
				</tr>
			</table>
			<table id="Settings" border="0" cellpadding=0 cellspacing=0 class="txt">
			<tr><td colspan="3"><img src="img/null.gif" width="1" height="10" /></td></tr>
			<tr>
				<td valign="top">
					<%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Tidsperiod"); break;
				    case 2: HttpContext.Current.Response.Write("Time frame"); break;
				}
				%>&nbsp;<br />
					<table border="0" cellpadding="0" cellspacing="0">
						<tr>
							<td><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("From"); break;
				    case 2: HttpContext.Current.Response.Write("From"); break;
				}
				%>&nbsp;</td>
							<td><asp:DropDownList CssClass="txt" ID=YearLow runat=server /><asp:DropDownList CssClass="txt" ID=MonthLow runat=server /><asp:DropDownList CssClass="txt" ID=DayLow runat=server /></td>
						</tr>
						<tr>
							<td><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Tom"); break;
				    case 2: HttpContext.Current.Response.Write("To"); break;
				}
				%>&nbsp;</td>
							<td><asp:DropDownList CssClass="txt" ID=YearHigh runat=server /><asp:DropDownList CssClass="txt" ID=MonthHigh runat=server /><asp:DropDownList CssClass="txt" ID=DayHigh runat=server /></td>
						</tr>
					</table>
					<img src="img/null.gif" width="1" height="8" /><br />
					<%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Jämför"); break;
				    case 2: HttpContext.Current.Response.Write("Compare"); break;
				}
				%>&nbsp;<br />
					<asp:RadioButtonList CssClass="txt" CellPadding=0 CellSpacing=0 RepeatDirection=horizontal RepeatColumns=2 RepeatLayout=table ID=Compare runat=server>
						<asp:ListItem Value="1" Selected Text="olika index" />
						<asp:ListItem Value="2" Text="med andra i databasen" />
					</asp:RadioButtonList>
					<img src="img/null.gif" width="1" height="8" /><br />
					<asp:Button ID=Submit runat=server Text="Uppdatera" />
				</td>
				<td><img src="img/null.gif" width="30" height="1" /></td>
				<td valign="top">
					<%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Ifyllnadstillfällen under perioden"); break;
				    case 2: HttpContext.Current.Response.Write("Measures during the time frame"); break;
				}
				%><br />
					<div style="overflow:auto;width:445px;height:115px;border:solid 1px #D6D6D6;">
						<asp:CheckBoxList ID="Submission" CssClass="txt" CellPadding=0 CellSpacing=0 RepeatDirection=vertical RepeatColumns=4 RepeatLayout=table runat=server />
					</div>
				</td>
			</tr>
			</table>
		</div>
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td valign="top">
					<div class="boxTitle" style="width:520px;"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Diagram"); break;
                    case 2: HttpContext.Current.Response.Write("Diagram"); break;
				}
				%></div>
					<div class="box" style="width:520px;"><asp:Label ID="Stats" EnableViewState=false runat=server /></div>
				</td>
				<td><img src="img/null.gif" width="20" height="1" /></td>
				<td valign="top">
					<div style="width:144px;" class="boxTitle"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Index"); break;
                    case 2: HttpContext.Current.Response.Write("Index"); break;
				}
				%></div>
					<div style="width:144px;" class="box">
						<asp:RadioButtonList AutoPostBack=true CssClass="txt" CellPadding=0 CellSpacing=0 RepeatColumns=1 RepeatDirection=vertical RepeatLayout=table ID=IndexOne runat=server />
						<asp:CheckBoxList CssClass="txt" CellPadding=0 CellSpacing=0 RepeatColumns=1 RepeatDirection=vertical RepeatLayout=table ID=IndexMulti runat=server />
						<span id="UpdateIndexesSeparator" runat=server><br /><img src="img/null.gif" width="1" height="8" /></span><asp:Button ID=UpdateIndexes runat=server Text="<-- Uppdatera" />
					</div>
				</td>
			</tr>
		</table>-->
        <div class="statschoser">
			      <div class="filter what">
			        <div class="title"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Välj formulär"); break;
				    case 2: HttpContext.Current.Response.Write("Select form"); break;
				}
				%></div>
			        <dl class="dropdown">
                <dt><a href="#"><span><asp:PlaceHolder ID=selectedSurveyKey runat=server /></span></a></dt>
                <dd>
                  <ul>
                    <asp:PlaceHolder ID=surveyKeys runat=server />
                  </ul>
                </dd>
              </dl>
            </div><!-- end .filter.what -->
            <div class="filter when">
			        <div class="title"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Välj tidsperiod"); break;
				    case 2: HttpContext.Current.Response.Write("Select time frame"); break;
				}
				%></div>
			        <dl class="dropdown">
                  <dt><a href="#"><span><asp:PlaceHolder ID=selectedMeasurement runat=server /></span></a></dt>
                  <dd>
                    <ul>
                      <asp:PlaceHolder ID=availableMeasurements runat=server />
                    </ul>
                  </dd>
                </dl>
            </div><!-- end .filter.when -->
            <div class="filter who">
			        <div class="title"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Jämför med"); break;
				    case 2: HttpContext.Current.Response.Write("Compare with"); break;
				}
				%></div>
			        <dl class="dropdown">
                  <dt><a href="#"><span><asp:PlaceHolder ID=selectedCompare runat=server /></span></a></dt>
                  <dd>
                    <ul>
                      <asp:PlaceHolder ID=availableCompare runat=server />
                    </ul>
                  </dd>
                </dl>
            </div><!-- end .filter.who -->
			    </div>
			  </div>
			  <div>
			    <div class="currentform">
			      <span class="lastform"><%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
				    case 1: HttpContext.Current.Response.Write("Formulär"); break;
				    case 2: HttpContext.Current.Response.Write("Form"); break;
				}
				%>:</span> <asp:PlaceHolder ID=selectedSurvey2 runat=server /> 
			      <span>- <%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Tidsperiod"); break;
				    case 2: HttpContext.Current.Response.Write("Time frame"); break;
				}
				%>:</span> 
                <span class="period"><asp:PlaceHolder ID=selectedMeasurement2 runat=server /></span> 
                <span>- <%
				switch(Convert.ToInt32(HttpContext.Current.Session["LID"]))
				{
                    case 1: HttpContext.Current.Response.Write("Jämför med"); break;
				    case 2: HttpContext.Current.Response.Write("Compare with"); break;
				}
				%>:</span> <asp:PlaceHolder ID=selectedCompare2 runat=server />
			    </div><!-- end .currentform -->
			  </div>
	
				<div class="results">
                    <asp:PlaceHolder ID=vars runat=server />
                  <%=colors() %>
				  <asp:PlaceHolder ID=bars runat=server />
					<div class="disclaimer">
					  <asp:PlaceHolder ID=feedback runat=server />
					</div>
				</div><!-- end .results -->
				<div class="bottom"></div>
		</div><!-- end .contentgroup	-->
		<%=healthWatch.Db.bottom2()%>
		<!--<script language="javascript">toggleSettings(false);toggleIndex();</script>
		<asp:Label ID="ExplWins" runat=server />-->
		</div> <!-- end .container_12 -->
		</form>
  </body>
</html>