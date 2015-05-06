<%@ Page Language="C#" AutoEventWireup="true" Inherits="healthWatch.extendedSurveys" Codebehind="extendedSurveys.aspx.cs" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%
       switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
       {
           case 1: HttpContext.Current.Response.Write(healthWatch.Db.header2("Enkät", "Enkät")); break;
           case 2: HttpContext.Current.Response.Write(healthWatch.Db.header2("Survey", "Survey")); break;
       }
           %>
   <script language="javascript">
       var pop = null;
       document.domain = 'healthwatch.se';
       function updateChecks() 
       {
           var obj;
           if (obj = document.getElementById('SendPH')) { obj.style.display = (document.getElementById('Offer0').checked ? 'none' : '') };
       }
   </script>
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
          <h1 class="header"><asp:Label ID=PageHeader runat=server /></h1>
          <div class="content">
					<div class="main">
						<div class="top"></div>
		<table border="0">
		    <asp:Label ID="Survey" runat=server/>
		</table>
		<br />
		<asp:PlaceHolder ID="TreatmentOffer" Visible=false runat=server>
		    <asp:Label ID="TreatmentOfferText" runat=server/>
            <div id="SendPH" style="display:none;">
		    <table border="0">
		        <tr><td><asp:Label ID=NameTxt runat=server /></td><td><asp:TextBox Width=300 ID=Name runat=server /></td></tr>
		        <tr><td><asp:Label ID=EmailTxt runat=server /></td><td><asp:TextBox Width=300 ID=Email runat=server /></td></tr>
		        <tr><td><asp:Label ID=PhoneTxt runat=server /></td><td><asp:TextBox Width=300 ID=Phone runat=server /></td></tr>
		        <tr><td colspan="2"><asp:Label ID="IncludeTxt" runat=server /> <asp:CheckBox ID=Include Checked=true runat=server /></td></tr>
		    </table>
		    <asp:Button ID=Send runat=server />
            </div>
		</asp:PlaceHolder>
		<asp:Label ID=Sent Visible=false runat=server/><br /><br /><a href="submit.aspx" class="lnk"><asp:Label ID=Continue runat=server /></a>
        <div class="bottom"></div>
		</div> <!-- end .main -->
					<div class="sidebar">
						<h3><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Mer information"); break;
                    case 2: HttpContext.Current.Response.Write("More information"); break;
                }
             %></h3><br />
						<p><%
		    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: HttpContext.Current.Response.Write("Dessa enkäter är bara aktiva en kortare period, tex för att genomföra arbetsmiljöundersökning."); break;
                    case 2: HttpContext.Current.Response.Write("These surveys are only active during a shorter period of time, e.g. to investigate the work environment."); break;
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
     <script language="javascript">
         updateChecks();
    </script>
</html>