<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="HW.ReportGenerator.Projects" %>
<%@ Import Namespace = "html=HW.Core.Helpers.HtmlHelper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Projects</h3>
      <table class="table table-striped table-hover">
        <tr>
          <th>Project Name</th>
        </tr>
          <% foreach (var p in projects) { %>
        <tr>
          <td><%= html.Anchor(p.Name, "projectshow.aspx") %></td>
        </tr>
          <% } %>
      </table>

</asp:Content>
