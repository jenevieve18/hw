﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exercise.aspx.cs" Inherits="HW.Grp.Exercise" %>
<%@ Import Namespace="HW.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
    	/** controls the dropdowns **/
    	$(".dropdown dt a").click(function () {
    		if ($(this).parent().parent().find("dd ul").hasClass("activated")) {
    			$(".dropdown dd ul").hide();
    			$(".activated").removeClass("activated")
    		} else {
    			$(".activated").removeClass("activated")
    			$(".dropdown dd ul").hide();
    			$(this).parent().parent().find("dd ul").toggle();
    			$(this).parent().parent().find("dd ul").addClass("activated")
    		}
    	});

    	$(".dropdown dd ul li a").click(function () {
    		var text = $(this).html();
    		$(this).parent().parent().parent().parent().find("dt a span").html(text);
    		$(".dropdown dd ul").hide();
    		//the id field of parent has the control hook, although I'd use a hrefs if you don't want to ajax
    		//alert($(this).parent().attr('id'))
    	});

    	$(document).bind('click', function (e) {
    		var $clicked = $(e.target);
    		if (!$clicked.parents().hasClass("dropdown")) {
    			$(".dropdown dd ul").hide();
    			$(".activated").removeClass("activated")
    		}
    	});

    	/** controls bar details **/
    	/*$(".bar .detailtoggle").click(function() {
    	$(this).parent().find(".detail").slideUp('fast', function() {
    	$(this).parent().removeClass("active");
    	})
		      
    	});
    	$(".bar:not(.active)").click(function() {
    	if(!$(this).hasClass("active")) {
    	$(this).find(".detail").slideDown('fast', function() {
    	$(this).parent().addClass("active");
    	})
    	}
    	});*/
    	$(".bar .overview").toggle(function () {
    		$(this).parent().find(".detail").slideDown('fast', function () {
    			$(this).parent().addClass("active");
    		})
    	}, function () {
    		$(this).parent().find(".detail").slideUp('fast', function () {
    			$(this).parent().removeClass("active");
    		})
    	})

    })
</script>

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
			        <div class="title">
                        <%= LanguageFactory.GetChooseCategory(LID) %>
                    </div>
			        <dl class="dropdown"><asp:PlaceHolder ID=CategoryID runat=server/></dl>
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
