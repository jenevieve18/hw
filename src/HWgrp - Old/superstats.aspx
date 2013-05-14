<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="superstats.aspx.cs" Inherits="HWgrp___Old.superstats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!doctype html> 
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