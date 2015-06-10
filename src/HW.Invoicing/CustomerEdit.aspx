<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="HW.Invoicing.CustomerEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= DropDownListTimebookItems.ClientID %>').change(function () {
            var selected = $(this).find('option:selected');
            var selectedPrice = selected.data('price');
            $('#<%= textBoxTimebookPrice.ClientID %>').val(selectedPrice);
        });
        $('#<%= DropDownListTimebookItems.ClientID %>').change();
    });
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Customer name: <asp:Label ID="labelCustomer" runat="server" Text="Label"></asp:Label></h3>

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
                    <td colspan="4">
                        <a id="modal-625558" href="#customer-notes-form" role="button" class="btn btn-info" data-toggle="modal">Create Notes</a>
			            <div class="modal fade" id="customer-notes-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				            <div class="modal-dialog">
					            <div class="modal-content">
						            <div class="modal-header">
							            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							            <h4 class="modal-title" id="H3">
								            Add Notes
							            </h4>
						            </div>
						            <div class="modal-body">
                                        <div class="form-group">
	                                        <label for="<%= textBoxNotes.ClientID %>">Notes</label>
                                            <asp:TextBox ID="textBoxNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
						            </div>
						            <div class="modal-footer">
							            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        <asp:Button ID="ButtonSaveNotes" runat="server" Text="Save notes" CssClass="btn btn-primary" OnClick="buttonSaveNotes_Click" />
						            </div>
					            </div>
				            </div>
			            </div>
                    </td>
                </tr>
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
                        <%= HtmlHelper.Anchor("Delete", "customernotesdelete.aspx?Id=" + n.Id + "&CustomerId=" + id) %>
                    </td>
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
                    <td colspan="8">
                        <a id="modal-717670" href="#timebook-form" role="button" class="btn btn-info" data-toggle="modal">Add a timebook</a>
			            <div class="modal fade" id="timebook-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				            <div class="modal-dialog">
					            <div class="modal-content">
						            <div class="modal-header">
							            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							            <h4 class="modal-title" id="myModalLabel">
								            Add a timebook for this customer
							            </h4>
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
                                            <asp:DropDownList ID="DropDownListTimebookItems" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
	                                        <label for="<%= textBoxTime.ClientID %>">Time</label>
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
                                        <asp:Button ID="ButtonCustomerTimebook" runat="server" Text="Save timebook" OnClick="buttonSaveTimebook_Click" CssClass="btn btn-primary" />
						            </div>
					            </div>
				            </div>
			            </div>

                        <a id="modal-607147" href="#modal-container-607147" role="button" class="btn btn-default" data-toggle="modal">Create invoice</a>
			            <div class="modal fade" id="modal-container-607147" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				            <div class="modal-dialog">
					            <div class="modal-content">
						            <div class="modal-header">
							            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							            <h4 class="modal-title" id="H1">
								            Create invoice for the timebook below
							            </h4>
						            </div>
						            <div class="modal-body">
							            <table class="table table-hover">
                                            <tr>
                                                <th>Date</th>
                                                <th>Department</th>
                                                <th>Contact</th>
                                                <th width="10%">Time</th>
                                                <th width="10%">Price</th>
                                                <th>Amount</th>
                                                <th>Comments</th>
                                            </tr>
                                            <tr>
                                                <td>2015-06-14</td>
                                                <td>Andrea Bocelli</td>
                                                <td><input type="text" class="form-control input-sm" value="5" /></td>
                                                <td><input type="text" class="form-control input-sm" value="13" /></td>
                                                <td>123.00</td>
                                                <td><input type="text" class="form-control input-sm" value="I created a new survey aftaer talking to Ian. " /></td>
                                            </tr>
                                        </table>
						            </div>
						            <div class="modal-footer">
							             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                         <%= HtmlHelper.Anchor("Save changes", "invoices.aspx", "class='btn btn-primary'") %>
						            </div>
					            </div>
				            </div>
			            </div>
                    </td>
                </tr>
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
			<div class="alert alert-info">
                In ordinary usage, <strong>price</strong> is the quantity of payment or compensation given by one party to another in return for goods or services.
			</div>
            <table class="table table-hover">
                <tr>
                    <td colspan="3">
                        <a id="modal-692185" href="#customer-prices-form" role="button" class="btn btn-info" data-toggle="modal">Add Customer Price</a>
			            <div class="modal fade" id="customer-prices-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				            <div class="modal-dialog">
					            <div class="modal-content">
						            <div class="modal-header">
							            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							            <h4 class="modal-title" id="H2">
								            New Customer Price
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
                                        <asp:Button ID="ButtonSavePrice" runat="server" Text="Save price" OnClick="buttonSavePrice_Click" CssClass="btn btn-primary" />
						            </div>
					            </div>
				            </div>
			            </div>
                    </td>
                </tr>
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
            <div class="form-group">
	            <label for="<%= textBoxNumber.ClientID %>">Customer number</label>
                <asp:TextBox ID="textBoxNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <!--<div class="form-group">
	            <label for="<%= textBoxName.ClientID %>">Customer name</label>
                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxPhone.ClientID %>">Phone</label>
                <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxMobile.ClientID %>">Mobile</label>
                <asp:TextBox ID="textBoxMobile" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxEmail.ClientID %>">Email</label>
                <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>-->
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Billing Address</label>
                <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Invoice address</label>
                <asp:TextBox ID="textBox3" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Purchase order number</label>
                <asp:TextBox ID="textBox4" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Reference person</label>
                <asp:TextBox ID="textBox5" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Our reference person</label>
                <asp:TextBox ID="textBox6" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
		</div>
        <div class="tab-pane" id="contact-persons">
			<br />
			<div class="alert alert-info">
				<strong>Customer contact information</strong> are people serving as a go-between, messenger, connection, or source of special information. Business contacts.
			</div>
            <div class="form-group">
	            <label for="<%= textBoxEmail.ClientID %>">Primary Contact</label>
                <asp:TextBox ID="textBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxEmail.ClientID %>">Secondary Contact</label>
                <asp:TextBox ID="textBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <h4>Other Contacts</h4>
            <table class="table table-hover">
                <tr>
                    <td colspan="5">
                        <a id="modal-240447" href="#modal-container-240447" role="button" class="btn btn-info" data-toggle="modal">Add new contact person</a>
			            <div class="modal fade" id="modal-container-240447" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				            <div class="modal-dialog">
					            <div class="modal-content">
						            <div class="modal-header">
							            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							            <h4 class="modal-title" id="H4">
								            New customer contact
							            </h4>
						            </div>
						            <div class="modal-body">
                                        <div class="form-group">
	                                        <label for="<%= textBoxEmail.ClientID %>">Contact person</label>
                                            <asp:TextBox ID="textBoxContact" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
	                                        <label for="<%= textBoxPhone.ClientID %>">Phone</label>
                                            <asp:TextBox ID="textBoxContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
	                                        <label for="<%= textBoxMobile.ClientID %>">Mobile</label>
                                            <asp:TextBox ID="textBoxContactMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
	                                        <label for="<%= textBoxEmail.ClientID %>">Email</label>
                                            <asp:TextBox ID="textBoxContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
						            </div>
						            <div class="modal-footer">
							            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        <asp:Button ID="ButtonSaveCustomerContact" runat="server" Text="Save contact" CssClass="btn btn-primary" OnClick="buttonSaveContact_Click" />
						            </div>
					            </div>
				            </div>
			            </div>
                    </td>
                </tr>
                <tr>
                    <th>Contact Person</th>
                    <th>Phone</th>
                    <th>Mobile</th>
                    <th>Email</th>
                    <th></th>
                </tr>
                <% foreach (var c in contacts) { %>
                <tr>
                    <td><%= c.Contact %></td>
                    <td><%= c.Phone %></td>
                    <td><%= c.Mobile %></td>
                    <td><%= c.Email %></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "customercontactedit.aspx?Id=" + c.Id + "&CustomerId=" + id) %>
                        <%= HtmlHelper.Anchor("Deactivate", "customercontactdelete.aspx?Id=" + c.Id + "&CustomerId=" + id) %>
                    </td>
                </tr>
                <% } %>
            </table>
		</div>
	</div>
</div>

<br />
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
    <asp:Button ID="buttonDeactivate" OnClick="buttonDeactivate_Click" runat="server" Text="Deactivate" CssClass="btn btn-warning" OnClientClick="return confirm('Are you sure you want to de-active this customer?')" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>
<br />

<div class="tabbable" id="Div1">
    <ul class="nav nav-tabs">
    </ul>
	<div class="tab-content">
	</div>
</div>

</asp:Content>
