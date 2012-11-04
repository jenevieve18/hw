<%@ Page Language="C#" AutoEventWireup="true" CodeFile="superstats.aspx.cs" Inherits="superstats" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
	    <%=Db.nav()%>
		<asp:Label ID=StatsImg runat=server />
		<%=Db.bottom()%>
		</form>
  </body>
</html>