<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="HW.upload" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=healthWatch.Db.header2("Filuppladdning", "Ladda upp filer till HealthWatch teamet")%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
	    <form id="Form1" method="post" runat="server">
        <div class="container_16 myhealth two-sides about<%=healthWatch.Db.cobranded() %>">
			<div class="headergroup grid_16">
		<%=healthWatch.Db.nav2()%>
        </div> <!-- end .headergroup -->
        <div class="contentgroup grid_16">
			 
				<h1 class="header"><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Ladda upp filer till HealthWatch teamet"); break;
                    case 2: HttpContext.Current.Response.Write("Upload files to the HealthWatch team"); break;
                }
             %></h1>
				<div class="content">
					<div class="main">
						<div class="top"></div>
			<table><tr><td><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Organisation"); break;
                    case 2: HttpContext.Current.Response.Write("Organisation"); break;
                }
             %></td><td><asp:TextBox ID=Organisation runat=server /></td></tr>
             <tr><td><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Beskrivning"); break;
                    case 2: HttpContext.Current.Response.Write("Description"); break;
                }
             %></td><td><asp:TextBox ID=Description runat=server /></td></tr>
             <tr><td><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Fil"); break;
                    case 2: HttpContext.Current.Response.Write("File"); break;
                }
             %></td><td><input type="file" id=File runat=server /></td></tr></table>
             <asp:Button ID=submit runat=server />
             <h2><asp:Label ID=thanks runat=server /></h2>
             <div class="bottom"></div>
					</div> <!-- end .main -->
					
					<div class="bottom"></div>
				</div>

		</div><!-- end .contentgroup	-->
		<%=healthWatch.Db.bottom2()%>
	</div> <!-- end .container_12 -->
    </form>
</body>
</html>
