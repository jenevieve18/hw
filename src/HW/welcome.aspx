<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.welcome" Codebehind="welcome.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=healthWatch.Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=healthWatch.Db.nav()%>
		    <h1>V�lkommen till HealthWatch</h1>
		    <p>H�r f�rklarar vi kort de olika delarna.</p>
		    <h2>Formul�r</h2>
		    H�r fyller du i sj�lvskattningsformul�r.
		<%=healthWatch.Db.bottom()%>
		</form>
  </body>
</html>