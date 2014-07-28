<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="welcome.aspx.cs" Inherits="HW.welcome" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		    <h1>Välkommen till HealthWatch</h1>
		    <p>Här förklarar vi kort de olika delarna.</p>
		    <h2>Formulär</h2>
		    Här fyller du i självskattningsformulär.
		<%=Db.bottom()%>
		</form>
  </body>
</html>
