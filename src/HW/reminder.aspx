<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.reminder" Codebehind="reminder.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("P�minnelser", "P�minnelser")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Reminders", "Reminders")); break;
       }
           %>
           <script type="text/javascript">
		var sponsored = <%= (sponsorLoginDays > 0).ToString().ToLower() %>;
		function updateReminder()
		{
			var t = document.forms[0].reminderType;
			var ts = 0;
			for(var i=0;i<t.length;i++)
			{
				if(t[i].checked)
				{
					ts = parseInt(t[i].value);
				}
			}
			switch(ts)
			{
				case 0:	
					document.getElementById('rReminderRepeat').style.display='none'; 
					document.getElementById('rReminder').style.display='none'; 
					document.getElementById('rReminderLink').style.display=(sponsored ? '' : 'none'); 
					break;
				case 2:
					document.getElementById('rReminderRepeat').style.display='none'; 
					document.getElementById('rReminder').style.display=''; 
					document.getElementById('rReminderLink').style.display='';
					document.getElementById('dReminderInactivity').style.display='';
					document.getElementById('dReminderRepeatWeek').style.display='none';
					document.getElementById('dReminderRepeatMonth').style.display='none';
					document.getElementById('dReminderRepeatDay').style.display='none';
					break;
				case 1:
					document.getElementById('rReminderRepeat').style.display=''; 
					document.getElementById('rReminder').style.display=''; 
					document.getElementById('rReminderLink').style.display='';
					document.getElementById('dReminderInactivity').style.display='none';
					var r = document.forms[0].reminderRepeat;
					var rs = 0;
					for(var i=0;i<r.length;i++)
					{
						if(r[i].checked)
						{
							rs = parseInt(r[i].value);
						}
					}
					switch(rs)
					{
						case 1:
							document.getElementById('dReminderRepeatDay').style.display='';
							document.getElementById('dReminderRepeatWeek').style.display='none';
							document.getElementById('dReminderRepeatMonth').style.display='none';
							break;
						case 2:
							document.getElementById('dReminderRepeatWeek').style.display='';
							document.getElementById('dReminderRepeatMonth').style.display='none';
							document.getElementById('dReminderRepeatDay').style.display='none';
							break;
						case 3:
							document.getElementById('dReminderRepeatMonth').style.display='';
							document.getElementById('dReminderRepeatWeek').style.display='none';
							document.getElementById('dReminderRepeatDay').style.display='none';
							break;
					}
					break;
			}
		}
   </script>
  </head>
<!--[if lt IE 7 ]> <body class="ie6" onload="self.updateReminder();"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7" onload="self.updateReminder();"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8" onload="self.updateReminder();"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9" onload="self.updateReminder();"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body onload="self.updateReminder();"> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides remind<%=healthWatch.Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
		</div> <!-- end .headergroup -->
			<div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("P�minnelser"); break;
                    case 2: HttpContext.Current.Response.Write("Reminders"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
					<% if (sponsorLoginDays > 0) { 
            %><p><%
		        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: HttpContext.Current.Response.Write("<strong>Observera!</strong> Ut�ver den inst�llning du sj�lv g�r p� denna sida har ditt f�retag/organisation st�llt in att en p�minnelse skall skickas till alla som varit inaktiva mer �n " + sponsorLoginDays + " dagar. Inst�llningen l�ngst ner p� denna sida g�ller �ven denna p�minnelse."); break;
                        case 2: HttpContext.Current.Response.Write("<strong>Please note!</strong> In addition to this setting, your company/organisation have a global reminder to everyone that has been inactive for more than " + sponsorLoginDays + " days. The setting on the bottom of this page is valid for that reminder as well."); break;
                    }%></p><br /><%
                 }
                 %>
                 <p><strong><%
					switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Hur ofta vill du f� en p�minnelse med e-post om att logga in p� HealthWatch?"); break;
                    case 2: HttpContext.Current.Response.Write("How often do you want a reminder to log on to HealthWatch?"); break;
                }%></strong></p>
					<asp:RadioButtonList ID=reminderType CellPadding=0 CellSpacing=0 RepeatDirection=vertical cssclass="bg" RepeatLayout=table runat=server>
						<asp:ListItem Text="Vid inaktivitet" Value="2" />
						<asp:ListItem Text="Regelbundet" Value="1" selected />
						<asp:ListItem Text="Aldrig" Value="0" />
					</asp:RadioButtonList>
				<div id="rReminderRepeat" style="display:none;">
					<br />
                    <p><strong>
					<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Schema"); break;
                    case 2: HttpContext.Current.Response.Write("Schedule"); break;
                }
             %></strong></p><asp:RadioButtonList ID=reminderRepeat CellPadding=0 CellSpacing=0 RepeatDirection=vertical cssclass="bg" RepeatLayout=table runat=server>
						<asp:ListItem Text="Veckodagsbasis" Value="1" selected />
						<asp:ListItem Text="Veckobasis" Value="2" />
						<asp:ListItem Text="M�nadsbasis" Value="3" />
					</asp:RadioButtonList><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 2: HttpContext.Current.Response.Write("&nbsp;basis"); break;
                }
             %>
			</div>
			<div id="rReminder" style="display:none;">
					<br />
					<div style="float:left;">
							<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Kl "); break;
                    case 2: HttpContext.Current.Response.Write("At "); break;
                }
             %><asp:DropDownList ID=reminderHour runat=server>
									<asp:ListItem Text="06:00" Value="6" />
									<asp:ListItem Text="07:00" Value="7" selected />
									<asp:ListItem Text="08:00" Value="8" />
									<asp:ListItem Text="09:00" Value="9" />
									<asp:ListItem Text="10:00" Value="10" />
									<asp:ListItem Text="11:00" Value="11" />
									<asp:ListItem Text="12:00" Value="12" />
									<asp:ListItem Text="13:00" Value="13" />
									<asp:ListItem Text="14:00" Value="14" />
									<asp:ListItem Text="15:00" Value="15" />
									<asp:ListItem Text="16:00" Value="16" />
									<asp:ListItem Text="17:00" Value="17" />
									<asp:ListItem Text="18:00" Value="18" />
									<asp:ListItem Text="19:00" Value="19" />
									<asp:ListItem Text="20:00" Value="20" />
									<asp:ListItem Text="21:00" Value="21" />
									<asp:ListItem Text="22:00" Value="22" />
								</asp:DropDownList>
								</div><div id="dReminderRepeatDay" style="float:left;display:none;">
								<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write(" varje "); break;
                    case 2: HttpContext.Current.Response.Write(" every "); break;
                }
             %><asp:CheckBoxList ID=reminderRepeatDay CellPadding=0 CellSpacing=0 RepeatDirection=horizontal RepeatLayout=Flow runat=server>
									<asp:ListItem Text="m�ndag" Value="1" selected />
									<asp:ListItem Text="tisdag" Value="2" />
									<asp:ListItem Text="onsdag" Value="3" selected />
									<asp:ListItem Text="torsdag" Value="4" />
									<asp:ListItem Text="fredag" Value="5" selected />
									<asp:ListItem Text="l�rdag" Value="6" />
									<asp:ListItem Text="s�ndag" Value="7" />
								</asp:CheckBoxList>
							</div>
							<div id="dReminderRepeatWeek" style="float:left;display:none;">
								<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write(" p� "); break;
                    case 2: HttpContext.Current.Response.Write(" on "); break;
                }
             %><asp:DropDownList ID=reminderRepeatWeekDay runat=server>
									<asp:ListItem Text="m�ndagar" Value="1" selected />
									<asp:ListItem Text="tisdagar" Value="2" />
									<asp:ListItem Text="onsdagar" Value="3" />
									<asp:ListItem Text="torsdagar" Value="4" />
									<asp:ListItem Text="fredagar" Value="5" />
									<asp:ListItem Text="l�rdagar" Value="6" />
									<asp:ListItem Text="s�ndagar" Value="7" />
								</asp:DropDownList><asp:DropDownList ID=reminderRepeatWeek CellPadding=0 CellSpacing=0 RepeatDirection=horizontal RepeatLayout=Flow runat=server>
									<asp:ListItem Text="varje vecka" Value="1" selected />
									<asp:ListItem Text="varannan vecka" Value="2" />
									<asp:ListItem Text="var tredje vecka" Value="3" />
								</asp:DropDownList>
							</div>
							<div id="dReminderRepeatMonth" style="float:left;display:none;">
								<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write(" den "); break;
                    case 2: HttpContext.Current.Response.Write(" the "); break;
                }
             %><asp:DropDownList ID=reminderRepeatMonthWeek runat=server>
									<asp:ListItem Text="f�rsta" Value="1" selected/>
									<asp:ListItem Text="andra" Value="2" />
									<asp:ListItem Text="tredje" Value="3" />
									<asp:ListItem Text="fj�rde" Value="4" />
								</asp:DropDownList><asp:DropDownList ID=reminderRepeatMonthDay runat=server>
									<asp:ListItem Text="m�ndagen" Value="1" selected />
									<asp:ListItem Text="tisdagen" Value="2" />
									<asp:ListItem Text="onsdagen" Value="3" />
									<asp:ListItem Text="torsdagen" Value="4" />
									<asp:ListItem Text="fredagen" Value="5" />
									<asp:ListItem Text="l�rdagen" Value="6" />
									<asp:ListItem Text="s�ndagen" Value="7" />
								</asp:DropDownList><asp:DropDownList ID=reminderRepeatMonth runat=server>
									<asp:ListItem Text="i varje" Value="1" selected />
									<asp:ListItem Text="varannan" Value="2" />
									<asp:ListItem Text="var tredje" Value="3" />
									<asp:ListItem Text="var sj�tte" Value="6" />
								</asp:DropDownList> <%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("m�nad."); break;
                    case 2: HttpContext.Current.Response.Write("month."); break;
                }
             %>
							</div>
							<div id="dReminderInactivity" style="float:left;display:none;">
								<asp:DropDownList ID=reminderInactivityCount runat=server>
									<asp:ListItem Text="1" Value="1" selected/>
									<asp:ListItem Text="2" Value="2" />
									<asp:ListItem Text="3" Value="3" />
									<asp:ListItem Text="4" Value="4" />
									<asp:ListItem Text="5" Value="5" />
									<asp:ListItem Text="6" Value="6" />
								</asp:DropDownList><asp:DropDownList ID=reminderInactivityPeriod runat=server>
									<asp:ListItem Text="dag/dagar" Value="1" selected />
									<asp:ListItem Text="vecka/veckor" Value="7" />
									<asp:ListItem Text="m�nad/m�nader" Value="30" />
								</asp:DropDownList><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write(" efter senaste inloggning, d�refter en g�ng i veckan."); break;
                    case 2: HttpContext.Current.Response.Write(" after last login, and after that once a week."); break;
                }
             %>
							</div>
						</div>
					<div id="rReminderLink" style="clear:both;display:none;">
					<br/>
                    <p><strong>
					<%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("I e-postp�minnelsen finns en l�nk till HealthWatch. Vill du att denna prepareras s� att du loggas in utan att ange anv�ndardnamn och l�senord n�r du klickar p� den?"); break;
                    case 2: HttpContext.Current.Response.Write("In the reminder there is a link to HealthWatch. Do you want it to be prepared so that you are logged in without prompt for username and password when you click on it?"); break;
                }
             %></strong></p>
					<asp:RadioButtonList ID=reminderLink CellPadding=0 CellSpacing=0 RepeatDirection=vertical cssclass="bg" RepeatLayout=table runat=server>
						<asp:ListItem Text="Ja, med en l�nk som �ndras varje g�ng och som endast kan anv�ndas en g�ng (rekommenderas)" Value="2" Selected />
						<asp:ListItem Text="Ja, med en l�nk som �r likadan varje g�ng och som kan sparas som bokm�rke" Value="1" />
						<asp:ListItem Text="Nej" Value="0" />
					</asp:RadioButtonList>
				</div>

                <div class="form_footer">
								<asp:Button ID=submit runat=server text="Spara" />
							</div>

					</div> <!-- end .main -->
					<div class="sidebar">
						<h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Om p�minnelser"); break;
                    case 2: HttpContext.Current.Response.Write("About reminders"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("H�r v�ljer du om du vill att en p�minnelse skall skickas med e-post till <a href=\"profile.aspx\">" + email + "</a>. �ndringar av denna e-postadress g�r du i din profil. Du kan n�r som helst g� tillbaka hit och �ndra ditt val."); break;
                    case 2: HttpContext.Current.Response.Write("Please select whether or not you want a reminder to be sent by email to <a href=\"profile.aspx\">" + email + "</a>. This email address can be changed in your profile. You can visit this page at any time to change your selection."); break;
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