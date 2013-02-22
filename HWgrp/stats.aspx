<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stats.aspx.cs" Inherits="HWgrp.stats" %>

<!doctype html>
<html lang="en" class="no-js">
<head>
    <%= Db.header() %>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).bind('click', function (e) {
                var $clicked = $(e.target);
                if (!$clicked.parents().hasClass("dropdown")) {
                    $(".dropdown dd ul").hide();
                    $(".activated").removeClass("activated")
                }
            });
			$('#selectAll').click(function () {
				$(this).parents('#StatsImg').find(':checkbox').attr('checked', this.checked);
			});
			$('#graphTypes').click(function() {
				var graphType = $('#graphTypes input:checked').val();
				$('#StatsImg table :checkbox').each(function () {
				    if (this.checked) {
				        var reportUrl = $(this).parent().find('#reportUrl:hidden').val();
			            reportUrl += "&Plot=" + graphType;
			            var img = $(this).parents('#StatsImg table').find('.img');
			            img.attr('src', reportUrl);
			            
			            var exportPdfUrl = $(this).parent().find('#exportPdfUrl:hidden').val();
			            exportPdfUrl += "&Plot=" + graphType;
			            var exportPdfAnchor = $(this).parents('#StatsImg table').find('.exportPdfAnchor');
			            exportPdfAnchor.attr('href', exportPdfUrl);
			            
			            var exportCsvUrl = $(this).parent().find('#exportCsvUrl:hidden').val();
			            exportCsvUrl += "&Plot=" + graphType;
			            var exportCsvAnchor = $(this).parents('#StatsImg table').find('.exportCsvAnchor');
			            exportCsvAnchor.attr('href', exportCsvUrl);
				    }
				});
			});
        });
    </script>
</head>
<!--[if lt IE 7 ]> <body class="ie6"> <![endif]-->
<!--[if IE 7 ]>    <body class="ie7"> <![endif]-->
<!--[if IE 8 ]>    <body class="ie8"> <![endif]-->
<!--[if IE 9 ]>    <body class="ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!--> <body> <!--<![endif]-->
    <form id="Form1" method="post" runat="server">
        <div class="container_16" id="admin">
		<%= Db2.nav() %>
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
                        <asp:DropDownList ID="LangID" runat="server" AutoPostBack=true />
				        <!--<asp:CheckBox ID="STDEV" runat=server Text="Show standard deviation" />-->
			            Distribution
                        <asp:DropDownList AutoPostBack=true ID=ExtraPoint runat=server>
					        <asp:ListItem Value=0 Text="< none >" />
					        <asp:ListItem Value=1 Text="Standard Deviation" />
					        <asp:ListItem Value=2 Text="Confidence Interval" />
				        </asp:DropDownList>
                        <br />
                        <asp:PlaceHolder ID="Org" runat=server Visible=false />
    	                <asp:CheckBoxList RepeatDirection=Vertical RepeatLayout=table CellPadding=0 CellSpacing=0 ID="BQ" runat=server Visible=false />
                        <asp:Button ID="Execute" CssClass="btn" runat=server Text="Execute" />
			        </div>
                </div>
        		<asp:Label ID=StatsImg runat=server />
				<!-- <% if (reportParts != null) { %>
					<% int i = 0; %>
        			<% foreach (var r in reportParts) { %>
        				<% if (i == 0) { %>
        					<div>&nbsp;<br>&nbsp;<br></div>
        				<% } else { %>
        					<div style='page-break-before:always;'>&nbsp;<br>&nbsp;<br></div>
        				<% } %>
						<table>
							<tr><td><%= r.Subject %></td></tr>
							<tr>
								<td style="font-size:18px;">
									<input id="chk<%= i %>" type="checkbox"><label for="chk<%= i %>"><%= r.Subject %></label>
									<input name="reportUrl" type="hidden" id="reportUrl" value="reportImage.aspx?LangID=1&amp;FY=2012&amp;TY=2013&amp;SAID=0&amp;SID=1&amp;Anonymized=1&amp;STDEV=0&amp;GB=7&amp;RPID=1&amp;PRUID=1&amp;GID=0,9&amp;GRPNG=2">
									<input name="exportPdfUrl" type="hidden" id="exportPdfUrl" value="Export.aspx?LangID=1&amp;FY=2012&amp;TY=2013&amp;SAID=0&amp;SID=1&amp;Anonymized=1&amp;STDEV=0&amp;GB=7&amp;RPID=1&amp;PRUID=1&amp;GID=0,9&amp;type=pdf&amp;GRPNG=2">
									<input name="exportCsvUrl" type="hidden" id="exportCsvUrl" value="Export.aspx?LangID=1&amp;FY=2012&amp;TY=2013&amp;SAID=0&amp;SID=1&amp;Anonymized=1&amp;STDEV=0&amp;GB=7&amp;RPID=1&amp;PRUID=1&amp;GID=0,9&amp;type=csv&amp;GRPNG=2">
								</td>
							</tr>
							<tr><td><%= r.Header %></td></tr>
							<tr>
								<td>
									<img src='<%= GetReportImageUrl(r.ReportPart.Id, r.Id, "reportImage", GetURL(urlModels)) %>'>
								</td>
							</tr>
							<tr>
								<td>
									<a href='<%= GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", GetURL(urlModels) + "&type=pdf") %>' target="_blank" class="exportPdfAnchor"><img src="images/page_white_acrobat.png"></a>
									<a href='<%= GetReportImageUrl(r.ReportPart.Id, r.Id, "Export", GetURL(urlModels) + "&type=csv") %>' target="_blank" class="exportCsvAnchor"><img src="images/page_white_excel.png"></a>
								</td>
							</tr>
							<tr>
								<td><%= r.Footer %></td>
							</tr>
						</table>
					<% } %>
				<% } %> -->
            </div><!-- end .contentgroup	-->
        </div> <!-- end .container_12 -->
	</form>
  </body>
</html>

