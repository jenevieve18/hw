<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="HW.EForm.Report.Projects" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Projects</h3>
    <table class="table table-hover table-striped">
        <tr>
            <th>Project Name</th>
        </tr>
        <% foreach (var p in projects) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(p.Name, "projectshow.aspx?ProjectID=" + p.ProjectID) %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
