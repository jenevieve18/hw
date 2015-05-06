<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.welcome" Codebehind="welcome.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=healthWatch.Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=healthWatch.Db.nav()%>
		    <h1>Välkommen till HealthWatch</h1>
		    <p>Här förklarar vi kort de olika delarna.</p>
		    <h2>Formulär</h2>
		    Här fyller du i självskattningsformulär.
		<%=healthWatch.Db.bottom()%>
		</form>
  </body>
</html>