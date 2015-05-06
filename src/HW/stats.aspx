<%@ Page language="c#" Inherits="healthWatch.stats" Codebehind="stats.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
   <%=healthWatch.Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=healthWatch.Db.nav()%>
		<h1 id="mainHeader">Exempelåterkoppling</h1>
		<asp:Label ID="Stats" runat=server />
		<ul>
		<li><a href="JavaScript:void(window.open('http://dev.eform.se/feedback.aspx?K=3C08C0F78F5F7D6A&A=OIAJSD123','','width=970,height=570,scrollbars=1,location=0,menubar=0,status=0,titlebar=0,toolbar=0,resizable=1'));">WebbQPS, gruppnivå</a></li>
		<li><a href="JavaScript:void(window.open('http://dev.eform.se/report.aspx?Anonymous=1&PRUID=97&RK=62097240a7f5455fb3e8a354069ff9e8','','width=970,height=570,scrollbars=1,location=0,menubar=0,status=0,titlebar=0,toolbar=0,resizable=1'));">HealthWatch, Hälsa- & stressformuläret, gruppnivå över tid</a></li>
		<li><a href="JavaScript:void(window.open('http://pqlife.ceos.nu/admin/teamstats/pq10graphNames.asp','','width=970,height=570,scrollbars=1,location=0,menubar=0,status=0,titlebar=0,toolbar=0,resizable=1'));">HealthWatch, Hälsa- & stressformuläret, gruppjämförelse över tid</a></li>
		<li><a href="JavaScript:void(window.open('http://pqlife.ceos.nu/admin/teamstats/pq10energy.asp','','width=970,height=570,scrollbars=1,location=0,menubar=0,status=0,titlebar=0,toolbar=0,resizable=1'));">HealthWatch, Hälsa- & stressformuläret, gruppjämförelse kvartalsvis</a></li>
		</ul>
		<h1 id="mainHeader">Draft slides</h1>
		<ul>
		<li><a href="HW.ppt">PowerPoint</a></li>
		<li><a href="HW.pdf">PDF</a></li>
		</ul>

		<%=healthWatch.Db.bottom()%>
		</form>
  </body>
</html>
