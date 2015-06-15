<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HW.Invoicing2.Models.CustomerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Customers</h2>

    <table>
        <tr>
            <th>Customer name</th>
            <th></th>
            <th></th>
        </tr>
        <% foreach (var c in Model.Customers) { %>
            <tr>
                <td><%= c.Name %></td>
                <td></td>
                <td></td>
            </tr>
        <% } %>
    </table>

</asp:Content>
