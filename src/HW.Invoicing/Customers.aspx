<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="HW.Invoicing.Customers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    .badge-success {
        background-color: #47a447;
        border-color: #398439;
    }
    .badge-sm {
        font-size: 5px;
        border-radius: 5px;
        padding: 3px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Customers</h3>
<p><%= HtmlHelper.Anchor("Add a customer", "customeradd.aspx", "class='btn btn-info'") %></p>

<div class="alert alert-info">
	<strong>Customers</strong> are people or organization that buys goods or services from a store or business.
</div>

<% if (!company.HasSubscriber) { %>

<div class="tabbable" id="tabs-327908">
	<ul class="nav nav-tabs">
		<%--<li class="active"><a href="#nonsubscribers" data-toggle="tab">Customers</a></li>
		<li><a href="#excustomers" data-toggle="tab">Ex-Customers</a></li>--%>
	    <li <%= selectedTab == "nonsubscribers" ? "class='active'" : "" %>><a href="#nonsubscribers" data-toggle="tab">Customers</a></li>
	    <li <%= selectedTab == "excustomers" ? "class='active'" : "" %>><a href="#excustomers" data-toggle="tab">Ex-Customers</a></li>
	</ul>
	<div class="tab-content">
		<%--<div class="tab-pane active" id="nonsubscribers">--%>
		<div class="tab-pane <%= selectedTab == "nonsubscribers" ? "active" : "" %>" id="nonsubscribers">
            <p></p>
            <div class="alert alert-info">
	            <strong>Customer</strong> is a person or organization that buys goods or services from a store or business.
            </div>
			<table class="table table-hover">
                <tr>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in nonSubscribers) { %>
                    <% if (c.IsInactive) { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike>                                
                            </td>
                            <td>
                                <% if (c.PrimaryContact != null) { %>
                                    <strike><%= c.PrimaryContact.Name%></strike>
                                <% } %>
                            </td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %>
                            </td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.ToString() : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Phone : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Email : "" %></td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<%--<div class="tab-pane" id="excustomers">--%>
		<div class="tab-pane <%= selectedTab == "excustomers" ? "active" : "" %>" id="excustomers">
			<p></p>
            <div class="alert alert-info">
	            <strong>Ex-customers</strong> are customers that have been deleted.
            </div>
            <table class="table table-hover">
                <tr>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in deletedCustomers) { %>
                    <tr>
                        <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                            <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %>
                        </td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.ToString() : "" %></td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.Phone : "" %></td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.Email : "" %></td>
                    </tr>
                <% } %>
            </table>
		</div>
	</div>
</div>

<% } else { %>

<div class="tabbable" id="tabs-789614">
	<ul class="nav nav-tabs">
		<%--<li class="active"><a href="#subscribed" data-toggle="tab">Subscribed</a></li>
		<li><a href="#nonsubscribers" data-toggle="tab">Non-subscribers</a></li>
        <li><a href="#excustomers" data-toggle="tab">Ex-Customers</a></li>--%>
	    <li <%= selectedTab == "subscribed" ? "class='active'" : "" %>><a href="#subscribed" data-toggle="tab">Subscribed</a></li>
        <li <%= selectedTab == "nonsubscribers" ? "class='active'" : "" %>><a href="#nonsubscribers" data-toggle="tab">Non-subscribers</a></li>
        <li <%= selectedTab == "excustomers" ? "class='active'" : "" %>><a href="#excustomers" data-toggle="tab">Ex-Customers</a></li>
	</ul>
	<div class="tab-content">
		<%--<div class="tab-pane active" id="subscribed">--%>
		<div class="tab-pane <%= selectedTab == "subscribed" ? "active" : "" %>" id="subscribed">
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
                    <% if (c.IsInactive) { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike>
                            </td>
                            <td>
                                <% if (c.PrimaryContact != null) { %>
                                    <strike><%= c.PrimaryContact.Name%></strike>
                                <% } %>
                            </td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %>
                            </td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.ToString() : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Phone : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Email : "" %></td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<%--<div class="tab-pane" id="nonsubscribers">--%>
		<div class="tab-pane <%= selectedTab == "nonsubscribers" ? "active" : "" %>" id="nonsubscribers">
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
                    <% if (c.IsInactive) { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike>
                            </td>
                            <td>
                                <% if (c.PrimaryContact != null) { %>
                                    <strike><%= c.PrimaryContact.Name%></strike>
                                <% } %>
                            </td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                                <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %>
                            </td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.ToString() : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Phone : "" %></td>
                            <td><%= c.PrimaryContact != null ? c.PrimaryContact.Email : "" %></td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
        <%--<div class="tab-pane" id="excustomers">--%>
		<div class="tab-pane <%= selectedTab == "excustomers" ? "active" : "" %>" id="excustomers">
			<p></p>
            <div class="alert alert-info">
	            <strong>Ex-customers</strong> are customers that have been deleted.
            </div>
            <table class="table table-hover">
                <tr>
                    <th>Name</th>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in deletedCustomers) { %>
                    <tr>
                        <td>
                                <% var openTimebooks = s.CustomerFindOpenTimebooks(c.Id); %>
                                <% if (openTimebooks.Count > 0) { %>
                                    <span class="badge badge-sm">&nbsp;</span>
                                <% } else { %>
                                    <span class="badge badge-sm badge-success">&nbsp;</span>
                                <% } %>
                            <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %>
                        </td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.ToString() : "" %></td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.Phone : "" %></td>
                        <td><%= c.PrimaryContact != null ? c.PrimaryContact.Email : "" %></td>
                    </tr>
                <% } %>
            </table>
		</div>
	</div>
</div>

<% } %>

</asp:Content>
