<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerShow.aspx.cs" Inherits="HW.Invoicing.CustomerShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Customer name: <asp:Label ID="labelCustomer" runat="server" Text="Label"></asp:Label></h3>
<p><%= HtmlHelper.Anchor("Edit this customer", "customeredit.aspx?Id=" + id, "class='btn btn-info'") %></p>

<div class="tabbable" id="tabs-179602">
	<ul class="nav nav-tabs">
	    <li class="active"><a href="#notes" data-toggle="tab">Notes</a></li>
	    <li><a href="#timebook" data-toggle="tab">Timebook</a></li>
	    <li><a href="#customer-prices" data-toggle="tab">Customer Prices</a></li>
		<li><a href="#customer-info" data-toggle="tab">Customer Info</a></li>
		<li><a href="#contact-persons" data-toggle="tab">Contact Persons</a></li>
	</ul>
	<div class="tab-content">
        <div class="tab-pane active" id="notes">
			<br />
			<div class="alert alert-info">
				<strong>Customer notes</strong> are brief records of facts, topics, or thoughts, written down as an aid to memory.
			</div>
            <table class="table table-hover">
                <tr>
                    <th style="width:20%">Date</th>
                    <th style="width:10%">Creator</th>
                    <th>Notes</th>
                </tr>
                <% foreach (var n in notes) { %>
                <tr>
                    <td><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></td>
                    <td><%= n.CreatedBy.Name %></td>
                    <td><%= n.Notes %></td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="timebook">
			<br />
			<div class="alert alert-info">
				<strong>Time book</strong> is a sheet for recording the time of arrival and departure of workers and for recording the amount of time spent on each job.
			</div>
            <table class="table table-hover">
                <tr>
                    <th></th>
                    <th>Date</th>
                    <th>Department</th>
                    <th>Contact</th>
                    <th>Time</th>
                    <th>Price</th>
                    <th>Amount</th>
                    <th>Consultant</th>
                    <th>Status</th>
                    <th>Comments</th>
                </tr>
                <% foreach (var t in timebooks) { %>
                <tr>
                    <td></td>
                    <td><%= t.Date.ToString("yyyy-MM-dd") %></td>
                    <td><%= t.Department %></td>
                    <td><%= t.Contact.Contact %></td>
                    <td><%= t.Quantity.ToString() %></td>
                    <td><%= t.Price.ToString("# ##0.00") %></td>
                    <td><%= t.Price.ToString("# ##0.00") %></td>
                    <td><%= t.Consultant %></td>
                    <td><span class="label label-success">INVOICED</span></td>
                    <td><%= t.Comments %></td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="customer-prices">
			<br />
			<div class="alert alert-info">
                In ordinary usage, <strong>price</strong> is the quantity of payment or compensation given by one party to another in return for goods or services.
			</div>
            <table class="table table-hover">
                <tr>
                    <th>Item</th>
                    <th>Price</th>
                </tr>
                <% foreach (var p in prices) { %>
                <tr>
                    <td><%= p.Item.Name %></td>
                    <td><%= p.Price.ToString("0.00") %></td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="customer-info">
            <br />
			<div class="alert alert-info">
				<strong>Customer information</strong> is lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
			</div>
            <table class="table">
                <tr>
                    <td><strong>Customer name</strong></td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Address</strong></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Customer number</strong></td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Phone</strong></td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Mobile</strong></td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Email</strong></td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>
		</div>
        <div class="tab-pane" id="contact-persons">
			<br />
			<div class="alert alert-info">
				<strong>Customer contact information</strong> are people serving as a go-between, messenger, connection, or source of special information. Business contacts.
			</div>
            <table class="table">
                <tr>
                    <td><strong>Primary Contact</strong></td>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Secondary Contact</strong></td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>
            <h4>Other Contacts</h4>
            <table class="table table-hover">
                <tr>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Mobile</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in contacts) { %>
                <tr>
                    <td><%= c.Contact %></td>
                    <td><%= c.Phone %></td>
                    <td><%= c.Mobile %></td>
                    <td><%= c.Email %></td>
                </tr>
                <% } %>
            </table>
		</div>
	</div>
</div>

</asp:Content>
