<%@ Page language="c#" Inherits="healthWatch._default" Codebehind="default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=healthWatch.Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=healthWatch.Db.nav()%>
		<%=healthWatch.Db.bottom()%>
		</form>
  </body>
</html>