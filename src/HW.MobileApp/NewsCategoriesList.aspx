<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="NewsCategoriesList.aspx.cs" Inherits="HW.MobileApp.NewsCategoriesList" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="header" data-theme="b" data-position="fixed">
                <h1><%=head %></h1>
                <a href="NewsCategories.aspx" rel="external" data-role="button" class="ui-btn-right"><%= R.Str(lang, "news.categories")%></a>
            </div>
            <div data-role="content">
                <ul data-role="listview">
                       
                    <% if (news.Count() > 0)
                       {


                           foreach (var n in news)
                           { %>
                        <li>
                            
                            <%var newslink = "href='NewsSummary.aspx?nid=" + n.newsID + "&ncid=" + categ + "'"; %>
                            <a <%=newslink %>>
                                
                                <!--<h1><%= n.teaser %></h1>-->
                                <h1><%= n.headline%></h1>
                                <p><%= n.newsCategory%></p>
                                <p><%= n.DT.ToString("m")%></p>
                            </a>
                        </li>
                    <% }
                       }
                       else { 
                       %>
                       <li>
                            <h1>Category is empty.</h1>
                       </li>
                       <%
                       } %>
                </ul>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health"><%= R.Str(lang,"home.myHealth") %></a></li>
                        <li><a href="News.aspx" data-icon="news"><%= R.Str(lang, "home.news")%></a></li>
                        <li><a href="More.aspx" data-icon="more"><%= R.Str(lang, "home.more")%></a></li>
                    </ul>
                </div>
            </div>

</asp:Content>
