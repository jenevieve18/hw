<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Units.aspx.cs" Inherits="HW.Invoicing.Units" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Units</h3>
<p><%= HtmlHelper.Anchor("Add a unit", "unitadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Unit</strong> is a quantity chosen as a standard in terms of which other quantities may be expressed.
</div>
<table class="table table-hover">
    <tr>
        <th>Unit name</th>
        <th></th>
    </tr>
    <% foreach (var u in units) { %>
        <% if (u.Inactive) { %>
            <tr>
                <td><strike><%= u.Name %></strike></td>
                <td>
                    <%= HtmlHelper.Anchor("Edit", "unitedit.aspx?Id=" + u.Id) %>
                    <%= HtmlHelper.Anchor("Delete", "unitdelete.aspx?Id=" + u.Id, "onclick=\"return confirm('Are you sure you want to delete this unit?')\"")%>
                </td>
            </tr>
        <% } else { %>
            <tr>
                <td><%= u.Name %></td>
                <td>
                    <%= HtmlHelper.Anchor("Edit", "unitedit.aspx?Id=" + u.Id) %>
                    <%= HtmlHelper.Anchor("Deactivate", "unitdeactivate.aspx?Id=" + u.Id) %>
                </td>
            </tr>
        <% } %>
    <% } %>
</table>

</asp:Content>
