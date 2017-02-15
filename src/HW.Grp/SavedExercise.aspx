<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="SavedExercise.aspx.cs" Inherits="HW.Grp.SavedExercise" %>

<%@ Import Namespace="HW.Core.Models" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="assets/js/exercise.js"></script>
    <link rel="stylesheet" href="assets/css/exercise.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16 exercises">
        <div class="exercise-options">
            <a class="hw-button hw-button-group" href="exercise.aspx">Group Exercises</a>
            <a class="hw-button hw-button-save" href="savedexercise.aspx"><strong>Saved Exercises</strong></a>
        </div>
        <div class="statschosergroup">
            <h1 class="header"><%= R.Str(lid, "exercises.group.save", "Saved-<br>exercises")%></h1>
            <a name="filter"></a>
            <div class="statschoser">
                <div class="filter misc">
                    <div class="title">
                        <%= R.Str(lid, "exercises.area", "Choose area")%>
                    </div>
                    <dl class="dropdown">
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
                                <% foreach (var a in areas) { %>
                                    <% if (!HasSelectedArea || (HasSelectedArea && a.Area.Id != SelectedArea.Area.Id)) { %>
                                        <li<%= (i++ < areas.Count - 1) ? "" : " class='last'" %> id="EAID=<%= a.Area.Id %>">
                                            <%= HtmlHelper.Anchor(a.AreaName, string.Format("exercise.aspx?EAID={0}{1}#filter", a.Area.Id, sortQueryString)) %>
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
                                <% foreach (var c in categories) { %>
                                    <% if (!HasSelectedCategory || (HasSelectedCategory && c.Category.Id != SelectedCategory.Category.Id)) { %>
                                        <li<%= (i++ < categories.Count - 1) ? "" : " class='last'" %> id="ECID<%= c.Category.Id %>">
                                            <%= HtmlHelper.Anchor(c.CategoryName, string.Format("exercise.aspx?ECID={0}{1}{2}#filter", c.Category.Id, sortQueryString, (exerciseAreaID != 0 ? "&EAID=" + exerciseAreaID : "")))%>
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
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "random", "Random"), sort == 0 ? "javascript:;" : "savedexercise.aspx?SORT=0" + AdditionalSortQuery + "#filter", sort == 0 ? "class='active'" : "")%>
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "popularity", "Popularity"), sort == 1 ? "javascript:;" : "savedexercise.aspx?SORT=1" + AdditionalSortQuery + "#filter", sort == 1 ? "class='active'" : "")%>
                    <%= HtmlHelper.AnchorSpan(R.Str(lid, "alphabetical", "Alphabethical"), sort == 2 ? "javascript:;" : "savedexercise.aspx?SORT=2" + AdditionalSortQuery + "#filter", sort == 2 ? "class='active'" : "")%>
                </div>
            </div>
        </div>
        <div style="clear:both"></div>
        <% if (exercises.Count > 0) { %>
        <div>
            <div class="largelegend">
            </div>

            <div class="contentlist">
                <table class="hw-table small">
                    <tr>
                        <th>Edit/Delete</th>
                        <th>Date</th>
                        <th>Exercise</th>
                        <th>Comment</th>
                        <th>Area</th>
                        <th>Category</th>
                    </tr>
                    <% foreach (var l in exercises) { %>
                    <% var evl = l.ExerciseVariantLanguage; %>
                    <% var v = l.ExerciseVariantLanguage.Variant; %>
                    <% var e = v.Exercise; %>
                    <% var el = e.Languages[0]; %>
                    <tr>
                        <td>
                            <%
                                string editUrl = string.Format(
                                    "javascript:void(window.open('SavedExerciseShow.aspx?SID={1}&AUID={2}&ExerciseVariantLangID={4}&SponsorAdminExerciseID={7}','EVLID{4}','scrollbars=yes,resizable=yes,width={5},height={6}'));",
                                    ConfigurationManager.AppSettings["healthWatchURL"],
                                    sponsorID,
                                    sponsorAdminID,
                                    v.Id,
                                    evl.Id,
                                    evl.ExerciseWindowX,
                                    evl.ExerciseWindowY,
                                    l.Id
                                );
                            %>
                            <a href="<%= editUrl %>"><img src="assets/img/application_edit.png" /></a>
                            <%
                                string deleteUrl = string.Format(
                                    @"javascript:if(confirm(""{1}"")) {{
									    location.href=""savedexercise.aspx?Delete={0}"";
								    }}",
                                    l.Id,
                                    R.Str(lid, "exercise.delete", "Are you sure you want to delete this exercise?")
                                );
                            %>
                            <%= HtmlHelper.AnchorImage(deleteUrl, "assets/img/cross.png")%>
                        </td>
                        <td><%= l.Date.Value.ToString("yyyy-MM-dd HH:mm") %></td>
                        <td><%= el.ExerciseName %></td>
                        <td class="exercise-comments">
                            <span class="hw-icon hw-icon-exercise"></span>
                            <span class="exercise-comment-label"><%= l.Comments %></span>
                            <textarea data-id="<%= l.Id %>" class="exercise-comment-text"><%= l.Comments %></textarea>
                            <img alt="" class="spinner" src="assets/img/spiffygif_30x30.gif" />
                        </td>
                        <td><%= e.Area.AreaName %></td>
                        <td><%= e.Category.CategoryName %></td>
                    </tr>
                    <% } %>
                </table>
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
        <%--<div class="bottom"></div>--%>
    </div>

</asp:Content>
