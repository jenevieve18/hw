<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exercise.aspx.cs" Inherits="HW.Grp.Exercise" %>
<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Core.Helpers" %>
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
            <a name="filter"></a>
			<div class="statschoser">
			    <div class="filter misc">
			        <div class="title">
                        <%= LanguageFactory.GetChooseArea(LID) %>
                    </div>
			        <dl class="dropdown">
						<asp:PlaceHolder ID="AreaID" runat="server" />
                        <!--<dt>
                            <% if (HasSelectedArea) { %>
                                <a href="javascript:;"><span><%= SelectedArea.AreaName %></span></a>
                            <% } else { %>
                                <a href="javascript:;"><span><%= LID == 0 ? "Visa alla" : "Show all" %></span></a>
                            <% } %>
                        </dt>
                        <dd>
                            <ul>
                                <% if (HasSelectedArea) { %>
                                    <li><%= HtmlHelper.Anchor(LID == 0 ? "Visa alla" : "Show all", string.Format("exercise.aspx?EAID=0{0}#filter", sortQS))%></li>
                                <% } %>
                                <% int i = 0; %>
                                <% foreach (var a in areas) { %>
                                    <% if (!HasSelectedArea || (HasSelectedArea && a.Area.Id != SelectedArea.Area.Id)) { %>
                                	    <li<%= (i++ < areas.Count - 1) ? "" : " class='last'" %> id="EAID=<%= a.Area.Id %>">
                                            <%= HtmlHelper.Anchor(a.AreaName, string.Format("exercise.aspx?EAID={0}{1}#filter", a.Area.Id, sortQS)) %>
                                        </li>
                                    <% } %>
                                <% } %>
                            </ul>
                        </dd>-->
					</dl>
                </div>
                <div class="filter misc">
			        <div class="title">
                        <%= LanguageFactory.GetChooseCategory(LID) %>
                    </div>
			        <dl class="dropdown">
						<asp:PlaceHolder ID="CategoryID" runat="server" />
                        <!--<dt>
                            <% if (HasSelectedCategory) { %>
                                <a href="javascript:;"><span><%= SelectedCategory.CategoryName %></span></a>
                            <% } else { %>
                                <a href="javascript:;"><span><%= LID == 0 ? "Visa alla" : "Show all" %></span></a>
                            <% } %>
                        </dt>
                        <dd>
                            <ul>
                                <% if (HasSelectedCategory) { %>
                                    <li><%= HtmlHelper.Anchor(LID == 0 ? "Visa alla" : "Show all", string.Format("exercise.aspx?ECID=0{0}{1}#filter", sortQS, (EAID != 0 ? "&EAID=" + EAID : "")))%></li>
                                <% } %>
                                <% i = 0; %>
                                <% foreach (var c in categories) { %>
                                    <% if (!HasSelectedCategory || (HasSelectedCategory && c.Category.Id != SelectedCategory.Category.Id)) { %>
                                        <li<%= (i++ < categories.Count - 1) ? "" : " class='last'" %> id="ECID<%= c.Category.Id %>">
                                            <%= HtmlHelper.Anchor(c.CategoryName, string.Format("exercise.aspx?ECID={0}{1}{2}#filter", c.Category.Id, sortQS, (EAID != 0 ? "&EAID=" + EAID : "")))%>
                                        </li>
                                    <% } %>
                                <% } %>
                            </ul>
                        </dd>-->
					</dl>
                </div>
			</div>
		</div>
        <div>
			<div class="currentform">
			    <span class="lastform">
					<%= LanguageFactory.GetSortingOrder(LID, BX) %>
                </span>
			    <div class="forms">
                    <asp:PlaceHolder ID="Sort" runat="server" />
			    </div>
			</div>
		</div>
		<div class="results">
			<div class="largelegend">
				<%= LanguageFactory.GetLegend(LID) %>
			</div>
				  
			<div class="contentlist">
				<asp:PlaceHolder ID="ExerciseList" runat="server" />
                <!--<% foreach (var l in exercises) { %>
                <div class="item">
                    <div class="overview"></div>
                    <div class="detail">
                        <div class="image"><img src="<%= l.Image %>" width="121" height="100"></div>
                        <div class="time"><%= l.CurrentLanguage.Time %><span class="time-end"></span></div>
                        <div class="descriptions"><%= l.ToString() %></div>
                        <h2><%= l.CurrentLanguage.ExerciseName %></h2>
                        <p><%= l.CurrentLanguage.Teaser %></p>
                        <div><%= HtmlHelper.Anchor("Text()", string.Format("javascript:void(window.open(''))"), new Dictionary<string, string>() { })%>
                            <a class="sidearrow" href="JavaScript:void(window.open('http://localhost/hw/exerciseShow.aspx?SID=83&AUID=514&ExerciseVariantLangID=71','EVLID71','scrollbars=yes,resizable=yes,width=0,height=0'));">Text ()</a>
                        </div>
                        <div class="bottom">&nbsp;</div>
                    </div>
                </div>
                <% } %>-->
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
