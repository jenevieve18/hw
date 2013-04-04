<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Exercise.aspx.cs" Inherits="HWgrp.Web.Exercise" %>
<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
	<div class="contentgroup grid_16 exercises">
	<div class="statschosergroup">
		<h2 class="header"><%= LanguageFactory.GetGroupExercise(LID) %></h2>
		<a name=filter></a>
		<div class="statschoser">
			<div class="filter misc">
				<div class="title btn-group">
					<button class="btn btn-info">
						<%= LanguageFactory.GetChooseArea(LID) %>
					</button>
					<button class="btn btn-info dropdown-toggle" data-toggle="dropdown">
						<span class="caret"></span>
					</button>
					<%--<dl class="dropdown"><asp:PlaceHolder ID=AreaID runat=server/></dl>--%>
					<%= BootstrapHelper.DropDownList<ExerciseAreaLanguage>(areas, "dropdown-menu") %>
				</div>
			</div>
			<div class="filter misc">
				<div class="title btn-group">
					<button class="btn btn-info">
						<%= LanguageFactory.GetChooseCategory(LID) %>
					</button>
					<button class="btn btn-info dropdown-toggle" data-toggle="dropdown">
						<span class="caret"></span>
					</button>
					<%--<dl class="dropdown"><asp:PlaceHolder ID=CategoryID runat=server/></dl>--%>
					<%--<asp:BulletedList ID="BulletedList1" runat="server" CssClass="dropdown-menu">
					</asp:BulletedList>--%>
					<%= BootstrapHelper.DropDownList<ExerciseCategoryLanguage>(categories, "dropdown-menu")%>
				</div>
			</div>
		</div>
	</div>
	<div>
		<div class="currentform">
			<br />
			<span class="lastform">
				<%= LanguageFactory.GetSortingOrder(LID, BX) %>
			</span>
			<div class="forms btn-group">
				<%-- <asp:PlaceHolder ID=Sort runat=server /> --%>
				<a class="active btn" href="javascript:;"><span>Slumpmässigt</span></a>
				<a class="btn" href="exercise.aspx?SORT=1#filter"><span>Popularitet</span></a>
				<a class="btn" href="exercise.aspx?SORT=2#filter"><span>Bokstavsordning</span></a>
			</div>
		</div>
	</div>
	<div class="results">
		<div class="largelegend">
			<%= LanguageFactory.GetLegend(LID) %>
		</div>

		<div class="contentlist">
			<br />
			<%--<asp:PlaceHolder ID="ExerciseList" runat="server" />--%>
			<% foreach (var l in exercises) { %>
				<div class="item well">
					<div class="overview"></div>
					<div class="detail">
						<div class="image"><%= HtmlHelper.Image(l.Image, 121, 100) %></div>
						<div class="time">
							<%= l.CurrentLanguage.Time %>
							<span class="time-end"></span>
						</div>
						<div class="descriptions"><%= l.ToString() %></div>
						<h2><%= l.CurrentLanguage.ExerciseName %></h2>
						<p><%= l.CurrentLanguage.Teaser %></p>
						<div><a class="sidearrow" href="JavaScript:void(window.open('http://localhost:1878/exerciseShow.aspx?SID=1&AUID=0&ExerciseVariantLangID=1','EVLID1','scrollbars=yes,resizable=yes,width=650,height=580'));">Type 1 (Sub Type 1)</a></div>
						<div class="bottom">&nbsp;</div>
					</div>
				</div>
			<% } %>
		</div><!-- end .contentlist -->
		<!--<div class="disclaimer">
		<div class="paginationgroup">Sida 1 av 13
		<a class="page">&lt;</a><a class="page active">1</a><a class="page">2</a><a class="page">3</a><a class="page">4</a><a class="page">5</a><a class="page">6</a><a class="page">7</a><a class="page">&gt;</a>
		</div>
		</div>-->
	</div><!-- end .results -->
	<div class="bottom"></div>
</div>

</asp:Content>