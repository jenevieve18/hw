<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="HW.Invoicing.CustomerEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit a customer</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Customer name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxAddress.ClientID %>">Address</label>
    <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Email</label>
    <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPhone.ClientID %>">Phone</label>
    <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
</div>

<div class="tabbable" id="Div1">
    <ul class="nav nav-tabs">
	    <li class="active"><a href="#notes" data-toggle="tab">Notes</a></li>
		<li><a href="#panel-368875" data-toggle="tab">Contacts</a></li>
	    <li><a href="#customer-prices" data-toggle="tab">Customer Prices</a></li>
	    <li><a href="#timebook" data-toggle="tab">Timebook</a></li>
    </ul>
	<div class="tab-content">
        <div class="tab-pane active" id="notes">
			<br />
			<div class="alert alert-info">
				<strong>Customer notes</strong> are lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
			</div>
            <table class="table table-hover">
                <tr>
                    <td colspan="4">
                        <a id="modal-625558" href="#customer-notes-form" role="button" class="btn btn-primary" data-toggle="modal">Create Notes</a>
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
                    <th>Date</th>
                    <th>Creator</th>
                    <th>Notes</th>
                    <th></th>
                </tr>
                <% foreach (var n in notes) { %>
                <tr>
                    <td><%= n.CreatedAt.Value.ToString("yyyy-MM-dd") %></td>
                    <td><%= n.CreatedBy.Name %></td>
                    <td><%= n.Notes %></td>
                    <td>
                    </td>
                </tr>
                <% } %>
            </table>
		</div>
        <div class="tab-pane" id="panel-368875">
			<br />
			<div class="alert alert-info">
				<strong>Customer contact information</strong> are lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
			</div>
            <table class="table table-hover">
                <tr>
                    <td colspan="5">
                        <a id="modal-240447" href="#modal-container-240447" role="button" class="btn btn-primary" data-toggle="modal">Add new contact person</a>
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
                                            <asp:TextBox ID="textBoxMobile" runat="server" CssClass="form-control"></asp:TextBox>
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
                    <td></td>
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
                        <a id="modal-692185" href="#customer-prices-form" role="button" class="btn btn-primary" data-toggle="modal">Add Customer Price</a>
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
                    <td>SEK<%= p.Price.ToString("0.00") %></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Delete", "") %>
                        <%= HtmlHelper.Anchor("Move Up", "") %>
                        <%= HtmlHelper.Anchor("Move Down", "") %>
                    </td>
                </tr>
                <% } %>
            </table>
		</div>
		<div class="tab-pane" id="timebook">
			<br />
			<div class="alert alert-info">
				<strong>Time book</strong> is lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
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
	                                        <label for="<%= textBoxTimebookContact.ClientID %>">Contact Person</label>
                                            <asp:TextBox ID="textBoxTimebookContact" runat="server" CssClass="form-control"></asp:TextBox>
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
							            <table>
                                            <tr>
                                                <th>Date</th>
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
                    <th>Contact</th>
                    <th>Time</th>
                    <th>Price</th>
                    <th>Amount</th>
                    <th>Status</th>
                    <th></th>
                </tr>
                <tr>
                    <td></td>
                    <td>2015-06-14</td>
                    <td>Andrea Bocelli</td>
                    <td>5hours</td>
                    <td>SEK13.00</td>
                    <td>SEK123.00</td>
                    <td><span class="label label-success">INVOICED</span></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Delete", "") %>
                    </td>
                </tr>
                <tr>
                    <td><input type="checkbox" /></td>
                    <td>2015-06-14</td>
                    <td>Andrea Bocelli</td>
                    <td>5hours</td>
                    <td>SEK13.00</td>
                    <td>SEK123.00</td>
                    <td></td>
                    <td>
                        <%= HtmlHelper.Anchor("Edit", "") %>
                        <%= HtmlHelper.Anchor("Delete", "") %>
                    </td>
                </tr>
            </table>
		</div>
	</div>
</div>

<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
