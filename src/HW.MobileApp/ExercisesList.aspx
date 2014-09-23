<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ExercisesList.aspx.cs" Inherits="HW.MobileApp.ExercisesList" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div data-role="header" data-theme="b" data-position="fixed">
        <a href="Exercises.aspx" data-icon="arrow-l"><%= R.Str(lang,"button.back") %></a>
        <%var headname = areaname; %>
        <h1><%=headname %></h1>
    </div>
    <div data-role="content">
        <ul data-role="listview">
            <li>
            <%
        
               var random = "href='ExercisesList.aspx?exaid=" + areaid + "&sort=0'";
               var popular = "href='ExercisesList.aspx?exaid=" + areaid + "&sort=1'";
               var alpha = "href='ExercisesList.aspx?exaid=" + areaid + "&sort=2'";

               if (sort == 0)
               {
                   random = "class='ui-btn-active'";
               }
               else if (sort == 1) 
               { 
                   popular = "class='ui-btn-active'";
               }
               else if (sort == 2)
               {
                   alpha = "class='ui-btn-active'";
               }
             
            %>
                <div data-role="navbar">
                <ul>
                    <li><a <%= random %>><%= R.Str(lang, "exercise.random")%></a></li>
                    <li><a <%= popular %>><%= R.Str(lang, "exercise.popular")%></a></li>
                    <li><a <%=alpha %>><%= R.Str(lang, "exercise.alphabetical")%></a></li>
            
                </ul>
                </div>
            </li>
            <%  
        
                foreach(var ex in exercises){ 
                    var varid = "href='ExercisesItem.aspx?varid="+ex.exerciseVariant[0].exerciseVariantLangID+"'";%>

    
            <li>
                <a <%=varid %>>
                <% var src = "src='" + ex.exerciseImage + "'"; if(ex.exerciseImage!=null){ %>

                <img class="exerciseimg" <%=src %>><%} %>

                <h2><%=ex.exercise %></h2>
                <p><%=ex.exerciseTeaser %></p>
                </a>
            </li>
            <%} %>

        </ul>
    </div>

</asp:Content>
