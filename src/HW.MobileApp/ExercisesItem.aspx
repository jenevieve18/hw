﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ExercisesItem.aspx.cs" Inherits="HW.MobileApp.ExercisesItem" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="header" data-theme="b" data-position="fixed">
        <%var temp = "href='ExerciseList.aspx?exaid="+(ex.exerciseAreaID)+"&sort=0'"; %>
        <a href="#" onclick="goBack()" data-icon="arrow-l"><%= R.Str(lang, "button.back")%></a>
        <script type="text/javascript">
            function goBack() {
                window.history.back();
            }
        </script>
        <h1><%=ex.exerciseArea %></h1>
    

    </div>

    <div class="noleftpadding" data-role="content">
        
        <h3 class="padd exheader" ><%=ex.exerciseHeader %></h3>
        
        <p class="padd areas" ><%=ex.exerciseArea %></p>
        <p class="padd areas" >
        <img src="images/time@2x.png" style="width:15px;">
        <%=ex.exerciseTime %></p>
        <img class="front_header_img" src="images/divider.gif">
        <div class="padd contentsmaller">
            <%=ex.exerciseContent %>
        </div>
        
    
    </div>

</asp:Content>
