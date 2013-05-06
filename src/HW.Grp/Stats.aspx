<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp.Stats" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style>
	.button {
		display: inline-block;
		outline: none;
		cursor: pointer;
		text-align: center;
		text-decoration: none;
		/*font: 14px/100% Arial, Helvetica, sans-serif;*/
		padding: .5em 2em .55em;
		text-shadow: 0 1px 1px rgba(0, 0, 0, .3);
		-webkit-border-radius: .4em;
		-moz-border-radius: .4em;
		border-radius: .4em;
		-webkit-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
		-moz-box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
		box-shadow: 0 1px 2px rgba(0, 0, 0, .2);
	}
	.button:hover {
		text-decoration: none;
	}
	.button:active {
		position: relative;
		top: 1px;
	}
	.white {
		color: #606060;
		border: solid 1px #b7b7b7;
	}
	.white:hover {
		background: -webkit-gradient(linear, left top, left bottom, from(#fff), to(#dcdcdc));
	}
	.small {
		font-size: 11px;
		padding: .2em 1em .275em;
	}
	.report-part { border:1px solid #e7e7e7; }
	.report-part-subject { background:#efefef; padding:5px 0 5px 20px; }
	.report-part-content { padding:20px; }
</style>

<script type="text/javascript">
	$(document).ready(function () {
		$('.report-part .action .button').click(function () {
			var partImg = $(this).closest('.report-part-content');
			var img = partImg.find('img');
			var newSrc = partImg.find('.hidden').text();
			img.attr('src', newSrc + '&PLOT=' + $(this).text());
		});
		$('.report-parts > .action .button').click(function () {
			text = $(this).text();
			$.each($('.report-part-content'), function () {
				img = $(this).find('img');
				url = $(this).find('.hidden').text();
				img.attr('src', url + '&PLOT=' + text);
			});
		});
		$('.action .button').mouseover(function () {
			$(this).parent().next().text($(this).text());
		});
		$('.action .button').mouseout(function () {
			$(this).parent().next().text('');
		});
	});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                <br />
                <asp:PlaceHolder ID="Org" runat=server Visible=false />
    	        <asp:CheckBoxList RepeatDirection=Vertical RepeatLayout=table CellPadding=0 CellSpacing=0 ID="BQ" runat=server Visible=false />
                <asp:Button ID="Execute" CssClass="btn" runat=server Text="Execute" />
			</div>
        </div>
		<% if (reportParts != null) { %>
			<% Q additionalQuery = GetGID(urlModels); %>
			<% int departments = SelectedDepartments.Count; %>
			<div class="report-parts">
				<div class="action">
					<span class="small">Change all graphs to:</span>
					<span class="button white small">Line</span>
					<span class="button white small">Line (mean ± SD)</span>
					<span class="button white small">Line (mean ± 1.96 SD)</span>
					<% if (departments == 1) { %>
						<span class="button white small">Boxplot</span>
					<% } %>
				</div>
				<div class="small action-desc"></div>
	        	<% foreach (var r in reportParts) { %>
	       			<div>&nbsp;<br />&nbsp;<br /></div>
					<div class="report-part">
						<div class="report-part-subject">
							<h3><%= r.Subject %></h3>
						</div>
						<!--<div class="report-part-header"><%= r.Header %></div>-->
						<div class="report-part-content">
							<span class="hidden"><%= GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery) %></span>
							<%= HtmlHelper.Image(GetReportImageUrl(r.ReportPart.Id, r.Id, additionalQuery)) %>
							<div class="action">
								<span class="small">Change graph to:</span>
								<span class="button white small">Line</span>
								<span class="button white small">Line (mean ± SD)</span>
								<span class="button white small">Line (mean ± 1.96 SD)</span>
								<% if (departments == 1) { %>
									<span class="button white small">Boxplot</span>
								<% } %>
							</div>
							<div class="small action-desc"></div>
						</div>
						<div class="report-part-bottom">&nbsp;</div>
					</div>
				<% } %>
			</div>
		<% } %>
    </div>

</asp:Content>
