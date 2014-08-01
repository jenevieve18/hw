<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Exercise.aspx.cs" Inherits="HW.Grp3.Exercise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<% foreach (var e in exercises) { %>
    <div>
        <%= e.CurrentLanguage.ExerciseName %>
    </div>
<% } %>

</asp:Content>
