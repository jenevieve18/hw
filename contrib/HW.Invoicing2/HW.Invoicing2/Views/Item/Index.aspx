<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HW.Invoicing2.Models.ItemModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Items</h3>
    <table class="table table-hover">
        <tr>
            <th>Item name</th>
        </tr>
        <% foreach (var i in Model.Items) { %>
        <tr>
            <td><%= i.Name %></td>
        </tr>
        <% } %>
    </table>

</asp:Content>
