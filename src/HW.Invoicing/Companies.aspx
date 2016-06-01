<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="HW.Invoicing.Companies" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>My Companies</h3>
<p><%= HtmlHelper.Anchor("Add a company", "companyadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
    <strong>Company</strong> is a commercial business.
</div>
<table class="table table-hover">
    <tr>
        <th>Company name</th>
        <th>Actions</th>
    </tr>
    <% foreach (var c in companies) { %>
    <tr>
        <td>
            <!--<%= c.Name %>-->
            <%= HtmlHelper.Anchor(c.Name, "companyedit.aspx?Id=" + c.Id)%>
        </td>
        <td>
            <%--<%= HtmlHelper.Anchor("Edit", "companyedit.aspx?Id=" + c.Id) %>--%>
            <%= HtmlHelper.Anchor(" ", "companyedit.aspx?Id=" + c.Id, "title='Edit' class='glyphicon glyphicon-pencil'")%>
            <%--<% if (c.HasTerms) { %>
                <%= HtmlHelper.Anchor("Terms", "companyterms.aspx?Id=" + c.Id) %>
            <% } %>--%>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
