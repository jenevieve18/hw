<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HW.Invoicing2.Models.UnitModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Units</h3>
    <table>
        <tr>
            <th>Unit name</th>
            <th></th>
        </tr>
        <% foreach (var u in Model.Units) { %>
        <tr>
            <td><%= u.Name %></td>
            <td></td>
        </tr>
        <% } %>
    </table>

</asp:Content>
