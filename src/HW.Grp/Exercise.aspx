﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Exercise.aspx.cs" Inherits="HW.Grp.Exercise" %>

<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/js/exercise.js"></script>
    <link rel="stylesheet" href="assets/theme1/css/exercise.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16 exercises">
        <div class="exercise-options">
            <a class="hw-button hw-button-group" href="exercise.aspx"><strong><%= R.Str(lid, "exercises.group1", "Group Exercises") %></strong></a>
            <a class="hw-button hw-button-save" href="savedexercise.aspx"><%= R.Str(lid, "exercises.saved1", "Saved Exercises") %></a>
        </div>
        <div class="statschosergroup">
            <h1 class="header"><%= R.Str(lid, "exercises.group", "Group-<br>exercises")%></h1>
            <a name="filter"></a>
            <div class="statschoser">
                <div class="filter misc">
                    <div class="title">
                        <%= R.Str(lid, "exercises.area", "Choose area")%>
                    </div>
                    <dl class="dropdown">
                        <%-- <asp:PlaceHolder ID="AreaID" runat="server" />--%>
                        <dt>
                            <% if (HasSelectedArea) { %>
                                <a href="javascript:;"><span><%= SelectedArea.AreaName %></span></a>
                            <% } else { %>
                                <a href="javascript:;"><span><%= R.Str(lid, "show.all", "Show all") %></span></a>
                            <% } %>
                        </dt>
                        <dd>
                            <ul>
                                <% if (HasSelectedArea) { %>
                                    <li><%= HtmlHelper.Anchor(R.Str(lid, "show.all", "Show all"), string.Format("exercise.aspx?EAID=0{0}#filter", sortQueryString))%></li>
                                <% } %>
                                <% int i = 0; %>
                                <% foreach (var a in areaData) { %>
                                    <%--<% if (!HasSelectedArea || (HasSelectedArea && a.Area.Id != SelectedArea.Area.Id)) { %>
                                        <li<%= (i++ < areas.Count - 1) ? "" : " class='last'" %> id="EAID=<%= a.Area.Id %>">
                                            <%= HtmlHelper.Anchor(a.AreaName, string.Format("exercise.aspx?EAID={0}{1}#filter", a.Area.Id, sortQueryString)) %>
                                        </li>
                                    <% } %>--%>
                                <% if (!HasSelectedArea || (HasSelectedArea && a.Id != SelectedArea.Id)) { %>
                                        <li<%= (i++ < areaData.Length - 1) ? "" : " class='last'" %> id="EAID=<%= a.Id %>">
                                            <%= HtmlHelper.Anchor(a.AreaName, string.Format("exercise.aspx?EAID={0}{1}#filter", a.Id, sortQueryString)) %>
                                        </li>
                                    <% } %>
                                <% } %>
                            </ul>
                        </dd>
                    </dl>
                </div>
                <div class="filter misc">
                    <div class="title">
                        <%= R.Str(lid, "exercises.category", "Choose a Category")%>
                    </div>
                    <dl class="dropdown">
                        <%-- <asp:PlaceHolder ID="CategoryID" runat="server" />--%>
                        <dt>
                            <% if (HasSelectedCategory) { %>
                                <a href="javascript:;"><span><%= SelectedCategory.CategoryName %></span></a>
                            <% } else { %>
                                <a href="javascript:;"><span><%= R.Str(lid, "show.all", "Show all") %></span></a>
                            <% } %>
                        </dt>
                        <dd>
                            <ul>
                                <% if (HasSelectedCategory) { %>
                                    <li><%= HtmlHelper.Anchor(R.Str(lid, "show.all", "Show all"), string.Format("exercise.aspx?ECID=0{0}{1}#filter", sortQueryString, (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "")))%></li>
                                <% } %>
                                <% i = 0; %>
                                <% foreach (var c in categoryData) { %>
                                    <% if (!HasSelectedCategory || (HasSelectedCategory && c.Id != SelectedCategory.Id)) { %>
                                        <li<%= (i++ < categoryData.Length - 1) ? "" : " class='last'" %> id="ECID<%= c.Id %>">
                                            <%= HtmlHelper.Anchor(c.CategoryName, string.Format("exercise.aspx?ECID={0}{1}{2}#filter", c.Id, sortQueryString, (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "")))%>
                                        </li>
                                    <% } %>
                                <% } %>
                            </ul>
                        </dd>
                    </dl>
                </div>
            </div>
        </div>
        <div>
            <div class="currentform">
                <span class="lastform">
                    <%= R.Str(lid, "exercises.sorting", "Exercises - Sorting:")%>
                </span>
                <div class="forms">
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "random", "Random"), sort == 0 ? "javascript:;" : "exercise.aspx?SORT=0" + AdditionalSortQuery + "#filter", sort == 0 ? "class='active'" : "")%>
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "popularity", "Popularity"), sort == 1 ? "javascript:;" : "exercise.aspx?SORT=1" + AdditionalSortQuery + "#filter", sort == 1 ? "class='active'" : "")%>
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "alphabetical", "Alphabethical"), sort == 2 ? "javascript:;" : "exercise.aspx?SORT=2" + AdditionalSortQuery + "#filter", sort == 2 ? "class='active'" : "")%>
                </div>
            </div>
        </div>
        <% if (exerciseData.Length > 0) { %>
            <div class="results">
                <div class="largelegend">
                    <%-- LanguageFactory.GetLegend(LID) --%>
                </div>

                <div class="contentlist">
                    <%--<asp:PlaceHolder ID="ExerciseList" runat="server" />--%>
                    <%--<% foreach (var l in exercises) { %>
                        <div class="item">
                            <div class="overview"></div>
                            <div class="detail">
                                <div class="image">
                                    <img src="<%= l.Image %>" width="121" height="100">
                                </div>
                                <div class="time"><%= l.CurrentLanguage.Time %><span class="time-end"></span></div>
                                <div class="descriptions"><%= l.AreaCategoryName %></div>
                                <h2><%= l.CurrentLanguage.ExerciseName %></h2>
                                <p><%= l.CurrentLanguage.Teaser %></p>
                                <div>
                                    <% string t = string.Format(
                                                "JavaScript:void(window.open('ExerciseShow.aspx?SID={1}&AUID={2}&ExerciseVariantLangID={3}','EVLID{3}','scrollbars=yes,resizable=yes,width={4},height={5}'));",
                                                ConfigurationManager.AppSettings["healthWatchURL"],
                                                sponsorID,
                                                sponsorAdminID,
                                                l.CurrentVariant.Id,
                                                l.CurrentVariant.ExerciseWindowX,
                                                l.CurrentVariant.ExerciseWindowY
                                            );
                                    %>
                                    <a class="sidearrow" href="<%= t %>"><%= l.CurrentType.ToString() %></a>
                                </div>
                                <div class="bottom">&nbsp;</div>
                            </div>
                        </div>
                    <% } %>--%>
                    <% foreach (var l in exerciseData) { %>
                        <div class="item">
                            <div class="overview"></div>
                            <div class="detail">
                                <div class="image">
                                    <img src="<%= l.Image %>" width="121" height="100">
                                </div>
                                <div class="time"><%= l.Time %><span class="time-end"></span></div>
                                <div class="descriptions"><%= l.AreaCategoryName %></div>
                                <h2><%= l.Name %></h2>
                                <p><%= l.Teaser %></p>
                                <div>
                                    <% string t = string.Format(
                                                "JavaScript:void(window.open('ExerciseShow.aspx?SID={1}&AUID={2}&ExerciseVariantLangID={3}','EVLID{3}','scrollbars=yes,resizable=yes,width=960,height=760'));",
                                                ConfigurationManager.AppSettings["healthWatchURL"],
                                                sponsorID,
                                                sponsorAdminID,
                                                l.ExerciseVariantId
                                            );
                                    %>
                                    <a class="sidearrow" href="<%= t %>">Text</a>
                                </div>
                                <div class="bottom">&nbsp;</div>
                            </div>
                        </div>
                    <% } %>
                </div>
                <!-- end .contentlist -->

                <!--<div class="disclaimer">
				    <div class="paginationgroup">Sida 1 av 13
					    <a class="page">&lt;</a><a class="page active">1</a><a class="page">2</a><a class="page">3</a><a class="page">4</a><a class="page">5</a><a class="page">6</a><a class="page">7</a><a class="page">&gt;</a>
				    </div>
			    </div>-->

            </div>
            <!-- end .results -->
        <% } %>
        <div class="bottom"></div>
    </div>

</asp:Content>
