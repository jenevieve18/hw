<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Exercise.aspx.cs" Inherits="HWgrp.Web.Exercise" %>
<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
	<div class="contentgroup grid_16 exercises">
	<div class="statschosergroup">
		<h1 class="header"><%= LanguageFactory.GetGroupExercise(LID) %></h1>
		<a name=filter></a>
		<div class="statschoser">
			<div class="filter misc">
				<div class="title">
					<%= LanguageFactory.GetChooseArea(LID) %>
				</div>
				<dl class="dropdown"><asp:PlaceHolder ID=AreaID runat=server/></dl>
			</div>
			<div class="filter misc">
				<div class="title btn-group">
					<a href="" class="btn dropdown-toggle" data-toggle="dropdown">
						<%= LanguageFactory.GetChooseCategory(LID) %>
						<span class="caret"></span>
					</a>
					<!--<dl class="dropdown"><asp:PlaceHolder ID=CategoryID runat=server/></dl>-->
					<asp:BulletedList ID="BulletedList1" runat="server" CssClass="dropdown-menu">
					</asp:BulletedList>
				</div>
			</div>
		</div>
	</div>
	<div>
		<div class="currentform">
			<span class="lastform">
				<%= LanguageFactory.GetSortingOrder(LID, BX) %>
			</span>
			<div class="forms">
				<asp:PlaceHolder ID=Sort runat=server />
			</div>
		</div>
	</div>
	<div class="results">
		<div class="largelegend">
			<%= LanguageFactory.GetLegend(LID) %>
		</div>

		<div class="contentlist">
			<asp:PlaceHolder ID="ExerciseList" runat="server" />
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