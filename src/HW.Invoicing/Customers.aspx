<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="HW.Invoicing.Customers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Customers</h3>
<p><%= HtmlHelper.Anchor("Add a customer", "customeradd.aspx", "class='btn btn-info'") %></p>

<% if (!HasSubscribers) { %>
<div class="alert alert-info">
	<strong>Customers</strong> are people or organization that buys goods or services from a store or business.
</div>
<table class="table table-hover">
    <tr>
        <th>Name</th>
        <th>Contact Person</th>
        <th>Phone</th>
        <th>Email</th>
    </tr>
    <% foreach (var c in nonSubscribers) { %>
        <% if (c.Inactive) { %>
            <tr>
                <td><strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike></td>
                <td>
                    <% if (c.FirstPrimaryContact != null) { %>
                        <strike><%= c.FirstPrimaryContact.Contact %></strike>
                    <% } %>
                </td>
                <td><strike><%= c.Phone %></strike></td>
                <td><strike><%= c.Email %></strike></td>
            </tr>
        <% } else { %>
            <tr>
                <td><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></td>
                <td>
                    <% if (c.FirstPrimaryContact != null) { %>
                        <%= c.FirstPrimaryContact.Contact %>
                    <% } %>
                </td>
                <td><%= c.Phone %></td>
                <td><%= c.Email %></td>
            </tr>
        <% } %>
    <% } %>
</table>

<% } else { %>

<div class="tabbable" id="tabs-789614">
	<ul class="nav nav-tabs">
		<li class="active">
			<a href="#panel-842795" data-toggle="tab">Subscribed</a>
		</li>
		<li>
			<a href="#panel-697427" data-toggle="tab">Non-subscribers</a>
		</li>
	</ul>
	<div class="tab-content">
		<div class="tab-pane active" id="panel-842795">
			<p></p>
            <div class="alert alert-info">
	            <strong>Subscribed customers</strong> are customers that sign up for recurring, automatic billing scenarios (this means repeat purchases are inherit in their definition, or at least more likely).
            </div>
            <table class="table table-hover">
                <tr>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in subscribers) { %>
                    <% if (c.Inactive) { %>
                        <tr>
                            <td><strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike></td>
                            <td>
                                <% if (c.FirstPrimaryContact != null) { %>
                                    <strike><%= c.FirstPrimaryContact.Contact %></strike>
                                <% } %>
                            </td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></td>
                            <td>
                                <% if (c.FirstPrimaryContact != null) { %>
                                    <%= c.FirstPrimaryContact.Contact %>
                                <% } %>
                            </td>
                            <td><%= c.Phone %></td>
                            <td><%= c.Email %></td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="panel-697427">
			<p></p>
            <div class="alert alert-info">
	            <strong>Non-subscribers</strong> are single-purchase customers.
            </div>
            <table class="table table-hover">
                <tr>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in nonSubscribers) { %>
                    <% if (c.Inactive) { %>
                        <tr>
                            <td><strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike></td>
                            <td>
                                <% if (c.FirstPrimaryContact != null) { %>
                                    <strike><%= c.FirstPrimaryContact.Contact %></strike>
                                <% } %>
                            </td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></td>
                            <td>
                                <% if (c.FirstPrimaryContact != null) { %>
                                    <%= c.FirstPrimaryContact.Contact %>
                                <% } %>
                            </td>
                            <td><%= c.Phone %></td>
                            <td><%= c.Email %></td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
	</div>
</div>

<% } %>

</asp:Content>
