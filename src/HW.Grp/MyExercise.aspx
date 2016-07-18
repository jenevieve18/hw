<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="MyExercise.aspx.cs" Inherits="HW.Grp.MyExercise" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <% if (exercises.Count > 0) { %>
    <div class="contentgroup grid_16 exercises">
		<div class="results">
			<div class="contentlist">
            <% foreach (var l in exercises) { %>
                <div class="item">
                    <div class="overview"></div>
                    <div class="detail">
                        <% var evl = l.ExerciseVariantLanguage; %>
                        <% var v = l.ExerciseVariantLanguage.Variant; %>
                        <% var e = v.Exercise; %>
                        <% var el = e.Languages[0]; %>
                        <div class="image"><img src="<%= e.Image %>" width="121" height="100"></div>
                        <div class="time"><%= el.Time%><span class="time-end"></span></div>
                        <div class="descriptions">
                            <%= l.Date.Value.ToString("yyyy-MM-dd") %>
                            <%= e.AreaCategoryName%>
                        </div>
                        <h2><%= e.Languages[0].ExerciseName %></h2>
                        <p><%= e.Languages[0].Teaser %></p>
                        <div>
                            <% string t = string.Format(
                                    "JavaScript:void(window.open('MyExerciseShow.aspx?SID={1}&AUID={2}&ExerciseVariantLangID={3}&SponsorAdminExerciseID={6}','EVLID{3}','scrollbars=yes,resizable=yes,width={4},height={5}'));",
                                    ConfigurationManager.AppSettings["healthWatchURL"],
                                    sponsorID,
                                    sponsorAdminID,
                                    evl.Id,
                                    evl.ExerciseWindowX,
                                    evl.ExerciseWindowY,
                                    l.Id
                                );
                            %>
                            <%--<%= HtmlHelper.Anchor(v.Type.Languages[0].ToString(), t, "class='sidearrow'") %>--%>
                            <a class='sidearrow' href="<%= t %>"><%= v.Type.Languages[0].ToString() %></a>
                            <%= HtmlHelper.Anchor("PDF", string.Format("exerciseexport.aspx?SponsorAdminExerciseID={0}", l.Id), "class='sidearrow' target='__blank'") %>
                        </div>
                        <div class="bottom">&nbsp;</div>
                    </div>
                </div>
            <% } %>
            </div>
        </div>
    </div>
    <% } %>

</asp:Content>
