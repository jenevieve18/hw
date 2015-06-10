<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerShow.aspx.cs" Inherits="HW.Invoicing.CustomerShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3><asp:Label ID="labelCustomer" runat="server" Text=""></asp:Label></h3>

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
            <p><a id="modal-625558" href="#customer-notes-form" role="button" class="btn btn-info" data-toggle="modal">Create notes</a></p>
			<div class="modal fade" id="customer-notes-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H3">Create notes</h4>
						</div>
						<div class="modal-body">
                            <div class="form-group">
	                            <label for="<%= textBoxNotes.ClientID %>">Notes</label>
                                <asp:TextBox ID="textBoxNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveNotes" runat="server" Text="Save notes" CssClass="btn btn-primary" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
				<strong>Customer notes</strong> are brief records of facts, topics, or thoughts, written down as an aid to memory.
			</div>
            <table class="table table-hover">
                <tr>
                    <th style="width:20%">Date</th>
                    <th style="width:10%">Creator</th>
                    <th>Notes</th>
                    <th></th>
                </tr>
                <% foreach (var n in notes) { %>
                <tr>
                    <td><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></td>
                    <td><%= n.CreatedBy.Name %></td>
                    <td><%= n.Notes %></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Deactivate", "") %>
                    </td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="timebook">
			<br />
            <p><a id="modal-717670" href="#timebook-form" role="button" class="btn btn-info" data-toggle="modal">Add a timebook</a></p>
			<div class="modal fade" id="timebook-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="myModalLabel">Add a timebook</h4>
						</div>
						<div class="modal-body">
                            <div class="form-group">
	                            <label for="<%= textBoxDate.ClientID %>">Date</label>
                                <asp:TextBox ID="textBoxDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= DropDownListContacts.ClientID %>">Contact Person</label>
                                <asp:DropDownList ID="DropDownListContacts" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= DropDownListTimebookItems.ClientID %>">Item</label>
                                <asp:DropDownList ID="DropDownListTimebookItems" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= DropDownListTimebookItems.ClientID %>">Unit</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTime.ClientID %>">Qty</label>
                                <asp:TextBox ID="textBoxTime" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookPrice.ClientID %>">Price</label>
                                <asp:TextBox ID="textBoxTimebookPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxConsultant.ClientID %>">Consultant</label>
                                <asp:TextBox ID="textBoxConsultant" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxComments.ClientID %>">Comments</label>
                                <asp:TextBox ID="textBoxComments" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonCustomerTimebook" runat="server" Text="Save timebook" CssClass="btn btn-primary" />
						</div>
					</div>
				</div>
			</div>
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
                    <th></th>
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
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Deactivate", "") %>
                    </td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="customer-prices">
			<br />
            <p><a id="modal-692185" href="#customer-prices-form" role="button" class="btn btn-info" data-toggle="modal">Add customer price</a></p>
			<div class="modal fade" id="customer-prices-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H2">
								Add customer price
							</h4>
						</div>
						<div class="modal-body">
                            <div class="form-group">
	                            <label for="<%= DropDownListItems.ClientID %>">Item</label>
                                <asp:DropDownList ID="DropDownListItems" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxPrice.ClientID %>">Price</label>
                                <asp:TextBox ID="textBoxPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSavePrice" runat="server" Text="Save price" CssClass="btn btn-primary" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
                In ordinary usage, <strong>price</strong> is the quantity of payment or compensation given by one party to another in return for goods or services.
			</div>
            <table class="table table-hover">
                <tr>
                    <th>Item</th>
                    <th>Price</th>
                    <th></th>
                </tr>
                <% foreach (var p in prices) { %>
                <tr>
                    <td><%= p.Item.Name %></td>
                    <td><%= p.Price.ToString("0.00") %></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Deactivate", "") %>
                        <%= HtmlHelper.Anchor("Move Up", "") %>
                        <%= HtmlHelper.Anchor("Move Down", "") %>
                    </td>
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
                    <td><strong>Customer Number</strong></td>
                    <td><asp:Label ID="labelCustomerNumber" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Postal Address</strong></td>
                    <td><asp:Label ID="labelInvoiceAddress" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Invoicing Address</strong></td>
                    <td><asp:Label ID="label1" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Reference / Purchase Order Number</strong></td>
                    <td><asp:Label ID="labelPurchaseOrderNumber" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Your Reference Person</strong></td>
                    <td><asp:Label ID="labelReferencePerson" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td><strong>Our Reference Person</strong></td>
                    <td><asp:Label ID="labelOurReferencePerson" runat="server" Text="Label"></asp:Label></td>
                </tr>
            </table>
		</div>
        <div class="tab-pane" id="contact-persons">
			<br />
            <p><a id="modal-240447" href="#modal-container-240447" role="button" class="btn btn-info" data-toggle="modal">Add new contact person</a></p>
			<div class="modal fade" id="modal-container-240447" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H4">New customer contact</h4>
						</div>
						<div class="modal-body">
                            <div class="form-group">
	                            <label for="">Contact person</label>
                                <asp:TextBox ID="textBoxContact" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="">Phone</label>
                                <asp:TextBox ID="textBoxContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="">Mobile</label>
                                <asp:TextBox ID="textBoxContactMobile" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="">Email</label>
                                <asp:TextBox ID="textBoxContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label class="radio-inline"><input type="radio" name="optradio">Primary</label>
                                <label class="radio-inline"><input type="radio" name="optradio">Secondary</label>
                                <label class="radio-inline"><input type="radio" name="optradio">Other</label>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveCustomerContact" runat="server" Text="Save contact" CssClass="btn btn-primary" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
				<strong>Customer contact information</strong> are people serving as a go-between, messenger, connection, or source of special information. Business contacts.
			</div>
            <table class="table table-hover">
                <tr>
                    <td></td>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Mobile</th>
                    <th>Email</th>
                </tr>
                <% foreach (var c in contacts) { %>
                <tr>
                    <td><span class="label label-success">PRIMARY</span></td>
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
