<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="HWgrp___Old.stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<!doctype html>
<html lang="en" class="no-js">
<head>
   <%=Db.header()%>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
		<div class="container_16" id="admin">
		<%=Db2.nav()%>
            <div class="contentgroup grid_16">
                <div id="contextbar">
                    <div class="settingsPane">
	                    <span class="desc">Timeframe</span>
                        <asp:DropDownList ID=FromYear runat=server />--<asp:DropDownList ID=ToYear runat=server />
                        Survey 
                        <asp:DropDownList AutoPostBack=true ID=ProjectRoundUnitID runat=server />
			            Aggregation
				            <asp:DropDownList AutoPostBack=true ID=GroupBy runat=server>
					            <asp:ListItem Value=1 Text="One week" />
					            <asp:ListItem Value=7 Text="Two weeks, start with even" />
					            <asp:ListItem Value=2 Text="Two weeks, start with odd" />
					            <asp:ListItem Value=3 Text="One month" />
					            <asp:ListItem Value=4 Text="Three months" />
					            <asp:ListItem Value=5 Text="Six months" />
					            <asp:ListItem Value=6 Text="One year" />
				            </asp:DropDownList><br />
			            <span class="desc">Grouping</span>
				            <asp:DropDownList AutoPostBack=true ID=Grouping runat=server>
					            <asp:ListItem Value=0 Text="< none >" />
					            <asp:ListItem Value=1 Text="Users on unit" />
					            <asp:ListItem Value=2 Text="Users on unit+subunits" />
					            <asp:ListItem Value=3 Text="Background variable" />
				            </asp:DropDownList>
                        Language <asp:DropDownList ID=LangID runat=server />
				        <asp:CheckBox ID="STDEV" runat=server Text="Show standard deviation" />
                        <br />
                        <asp:PlaceHolder ID="Org" runat=server Visible=false />
    	                <asp:CheckBoxList RepeatDirection=Vertical RepeatLayout=table CellPadding=0 CellSpacing=0 ID="BQ" runat=server Visible=false />
                        <asp:Button ID="Execute" CssClass="btn" runat=server Text="Execute" />
			        </div>
                </div>
        		<asp:Label ID=StatsImg runat=server />
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>
