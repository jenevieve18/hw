<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundShow.aspx.cs" Inherits="HW.EForm.Report.ProjectRoundShow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= projectRound.Internal %></h3>
    <table class="table table-hover table-striped">
        <tr>
            <th>Unit</th>
            <th>ID</th>
            <th>Survey</th>
        </tr>
        <% foreach (var u in projectRound.Units) { %>
        <tr>
            <td><%= u.Unit %></td>
            <td><%= u.ID %></td>
            <td><%= u.Survey != null ? u.Survey.Internal : "" %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
