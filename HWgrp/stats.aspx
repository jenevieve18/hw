<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="HWgrp.stats" %>

<!doctype html>
<html lang="en" class="no-js">
<head>
    <%=Db.header()%>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).bind('click', function (e) {
                var $clicked = $(e.target);
                if (!$clicked.parents().hasClass("dropdown")) {
                    $(".dropdown dd ul").hide();
                    $(".activated").removeClass("activated")
                }
            });
        });
        function xxx(plot, img, imgUrl) {
            var r = document.getElementsByName(plot);
            for (var i = 0; i < r.length; i++) {
                if (r[i].checked) {
                    //alert("Selected item value " + r[i].value);
                    document.getElementById(img).src = imgUrl + "&Plot=" + r[i].value;
                }
            }
        }
    </script>
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
                        Language
                        <asp:DropDownList ID="LangID" runat="server" />
				        <asp:CheckBox ID="STDEV" runat=server Text="Show standard deviation" />
                        <br />
                        <asp:PlaceHolder ID="Org" runat=server Visible=false />
    	                <asp:CheckBoxList RepeatDirection=Vertical RepeatLayout=table CellPadding=0 CellSpacing=0 ID="BQ" runat=server Visible=false />
                        <asp:Button ID="Execute" CssClass="btn" runat=server Text="Execute" />
			        </div>
                </div>
        		<asp:Label ID=StatsImg runat=server />
                <!--<% if (reportParts != null) { %>
                    <% int cx = 0; %>
                    <% string URL = GetURL(Convert.ToInt32(Grouping.SelectedValue)); %>
                    <% foreach (var r in reportParts) { %>
                        <% if (cx == 0) { %>
                            <div>&nbsp;<br>&nbsp;<br></div>
                        <% } else { %>
                            <div style="page-break-before:always;">&nbsp;<br>&nbsp;<br></div>
                        <% } %>
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr class="nocreen">
                                <td align="center" valign="middle" background="img/top_healthWath.jpg" height="140" style="font-size:24px;">
                                    <%= r.Subject %>
                                </td>
                            </tr>
                            <tr class="noprint"><td style="font-size:18px;"><%= r.Subject %></td></tr>
                            <tr><td><%= r.Header.Replace("\r", "").Replace("\n", "<br>") %></td></tr>
                            <tr>
                                <td>
                                    <img src="<%= GetReportImageUrl(r.Id, URL) %>"/>
                                </td>
                            </tr>
                            <tr><td><%= r.Footer.Replace("\r", "").Replace("\n", "<br>") %></td></tr>
                        </table>
                        <% cx++; %>
                    <% } %>
                <% } %>-->
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>

