<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Exercises.aspx.cs" Inherits="HW.MobileApp.Exercises" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l"><%= R.Str("home.myHealth") %></a>
    <h1><%= R.Str("dashboard.exercises") %></h1>
</div>

<div data-role="content">


<ul data-role="listview" id="areaselected" >

    <li data-icon="false">
    <a href="ExercisesList.aspx?exaid=0&sort=0"><%= R.Str("exercise.viewAll") %></a> 
    </li>

    <% foreach (var e in exerciseArea){
       var value = "href='ExercisesList.aspx?exaid="+e.exerciseAreaID+"&sort=0'";%>
    <li>
        <a class="areas" <%=value %>><%=e.exerciseArea %></a>
    </li>
    <%} %>
</ul>
</div>

</asp:Content>
