<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="HW.Invoicing.Invoices" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Invoices</h3>
<!--<p><%= HtmlHelper.Anchor("Create an invoice", "invoiceadd.aspx", "class='btn btn-info'") %></p>-->
<div class="alert alert-info">
	<strong>Invoices</strong> are lists of goods sent or services provided, with a statement of the sum due for these; a bill.
</div>
<table class="table table-hover">
    <tr>
        <th>Date</th>
        <th>Number</th>
        <th>Customer</th>
        <th>Amount</th>
        <th>Status</th>
        <th></th>
    </tr>
    <tr>
        <td>2015-06-03</td>
        <td>IHG-001</td>
        <td>Customer 1</td>
        <td>SEK123.00</td>
        <td><span class="label label-warning">NOT PAID</span></td>
        <td>
            <div class="btn-group">
				 <button class="btn btn-default">Action</button> <button data-toggle="dropdown" class="btn btn-default dropdown-toggle"><span class="caret"></span></button>
				<ul class="dropdown-menu">
					<li>
						<a href="#">Edit</a>
					</li>
					<li class="divider">
					</li>
					<li>
						<a href="#">Receive Payment</a>
					</li>
				</ul>
			</div>
        </td>
    </tr>
    <tr>
        <td>2015-06-03</td>
        <td>IHG-002</td>
        <td>Customer 1</td>
        <td>SEK123.00</td>
        <td><span class="label label-warning">NOT PAID</span></td>
        <td>
           <div class="btn-group">
				 <button class="btn btn-default">Action</button> <button data-toggle="dropdown" class="btn btn-default dropdown-toggle"><span class="caret"></span></button>
				<ul class="dropdown-menu">
					<li>
						<a href="#">Edit</a>
					</li>
					<li class="divider">
					</li>
					<li>
						<a href="#">Receive Payment</a>
					</li>
				</ul>
			</div>
        </td>
    </tr>
    <tr>
        <td>2015-06-03</td>
        <td>IHG-003</td>
        <td>Customer 1</td>
        <td>SEK1234.00</td>
        <td><span class="label label-success">PAID</span></td>
        <td>
            <div class="btn-group">
				 <button class="btn btn-default">Action</button> <button data-toggle="dropdown" class="btn btn-default dropdown-toggle"><span class="caret"></span></button>
				<ul class="dropdown-menu">
					<li>
						<a href="#">Edit</a>
					</li>
					<li class="divider">
					</li>
					<li>
						<a href="#">Receive Payment</a>
					</li>
				</ul>
			</div>
        </td>
    </tr>
    <!--<% foreach (var i in invoices) { %>
    <tr>
        <td><%= i.Date.Value.ToString("yyyy-MM-dd") %></td>
        <td><%= i.Customer.Name %></td>
        <td><%= i.AmountDue.ToString("0.00") %></td>
        <td><%= i.TotalAmount.ToString("0.00") %></td>
        <td><span class="label label-danger">NOT PAID</span></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "invoiceedit.aspx?ItemID=" + i.Id)%>
        </td>
    </tr>
    <% } %>-->
</table>

</asp:Content>
