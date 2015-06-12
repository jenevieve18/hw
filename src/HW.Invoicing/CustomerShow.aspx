<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerShow.aspx.cs" Inherits="HW.Invoicing.CustomerShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= dropDownListTimebookItems.ClientID %>').change(function () {
                var selected = $(this).find('option:selected');
                var selectedPrice = selected.data('price');
                var selectedUnit = selected.data('unit');
                $('#<%= textBoxTimebookPrice.ClientID %>').val(selectedPrice);
                $('#<%= labelTimebookUnit.ClientID %>').text(selectedUnit);
            });
            $('#<%= dropDownListTimebookItems.ClientID %>').change();

            /*$('#<%= textBoxCustomerNumber.ClientID %>').hide();
            $('#<%= labelCustomerNumber.ClientID %>').click(function () {
                $(this).hide();
                $('#<%= textBoxCustomerNumber.ClientID %>').show();
                $('#<%= textBoxCustomerNumber.ClientID %>').focus();
            });
            $('#<%= textBoxCustomerNumber.ClientID %>').focusout(function () {
                $(this).hide();
                $('#<%= labelCustomerNumber.ClientID %>').show();
            });*/

            turnEditable('#<%= labelCustomerNumber.ClientID %>', '#<%= textBoxCustomerNumber.ClientID %>');

            $('#<%= textBoxTimebookDate.ClientID %>').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
        });

        function turnEditable(labelId, textBoxId) {
            var label = $(labelId);
            var textBox = $(textBoxId);
            textBox.hide();

            label.click(function () {
                $(this).hide();
                textBox.show();
                textBox.focus();
            });
            textBox.focusout(function () {
                $(this).hide();
                label.show();
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3><asp:Label ID="labelCustomer" runat="server" Text=""></asp:Label></h3>

<div class="tabbable" id="tabs-179602">
	<ul class="nav nav-tabs">
	    <li <%= selectedTab == "notes" ? "class='active'" : "" %>><a href="#notes" data-toggle="tab">Note</a></li>
	    <li <%= selectedTab == "timebook" ? "class='active'" : "" %>><a href="#timebook" data-toggle="tab">Timebook</a></li>
	    <li <%= selectedTab == "customer-prices" ? "class='active'" : "" %>><a href="#customer-prices" data-toggle="tab">Customer Prices</a></li>
		<li <%= selectedTab == "customer-info" ? "class='active'" : "" %>><a href="#customer-info" data-toggle="tab">Customer Info</a></li>
		<li <%= selectedTab == "contact-persons" ? "class='active'" : "" %>><a href="#contact-persons" data-toggle="tab">Contact Persons</a></li>
	</ul>
	<div class="tab-content">
        <div class="tab-pane <%= selectedTab == "notes" ? "active" : "" %>                                                                                                                                                                                                                                                                                       " id="notes">
			<br />
            <p><a id="modal-625558" href="#customer-notes-form" role="button" class="btn btn-info" data-toggle="modal">Create note</a></p>
			<div class="modal fade" id="customer-notes-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H3">Create note</h4>
						</div>
						<div class="modal-body">
                            <div class="form-group">
	                            <label for="<%= textBoxNotes.ClientID %>">Note</label>
                                <asp:TextBox ID="textBoxNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveNotes" runat="server" Text="Save note" CssClass="btn btn-primary" OnClick="buttonSaveNotes_Click" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
				<strong>Customer note</strong> is a brief records of facts, topics, or thoughts, written down as an aid to memory.
			</div>
            <table class="table table-hover">
                <tr>
                    <th style="width:20%">Date</th>
                    <th style="width:10%">Creator</th>
                    <th>Note</th>
                    <th></th>
                </tr>
                <% foreach (var n in notes) { %>
                    <% if (n.Inactive) { %>
                        <tr>
                            <td><strike><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></strike></td>
                            <td><strike><%= n.CreatedBy.Name %></strike></td>
                            <td><strike><%= n.Notes %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", string.Format("customernotesedit.aspx?Id={0}&CustomerId={1}", n.Id, id)) %>
                                <%= HtmlHelper.Anchor("Delete", string.Format("customernotesdelete.aspx?Id={0}&CustomerId={1}", n.Id, id), "onclick=\"return confirm('Are you sure you want to delete this customer note?')\"") %>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></td>
                            <td><%= n.CreatedBy.Name %></td>
                            <td><%= n.Notes %></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", string.Format("customernotesedit.aspx?Id={0}&CustomerId={1}", n.Id, id)) %>
                                <%= HtmlHelper.Anchor("Deactivate", string.Format("customernotesdeactivate.aspx?Id={0}&CustomerId={1}", n.Id, id))%>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane <%= selectedTab == "timebook" ? "active" : "" %>" id="timebook">
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
	                            <label for="<%= textBoxTimebookDate.ClientID %>">Date</label>
                                <asp:TextBox ID="textBoxTimebookDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookDepartment.ClientID %>">Department</label>
                                <asp:TextBox ID="textBoxTimebookDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= dropDownListTimebookContacts.ClientID %>">Contact Person</label>
                                <asp:DropDownList ID="dropDownListTimebookContacts" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= dropDownListTimebookItems.ClientID %>">Item</label>
                                <asp:DropDownList ID="dropDownListTimebookItems" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= labelTimebookUnit.ClientID %>">Unit</label>
                                <asp:Label ID="labelTimebookUnit" runat="server" Text="" CssClass="form-control"></asp:Label>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookQty.ClientID %>">Qty</label>
                                <asp:TextBox ID="textBoxTimebookQty" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookPrice.ClientID %>">Price</label>
                                <asp:TextBox ID="textBoxTimebookPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookConsultant.ClientID %>">Consultant</label>
                                <asp:TextBox ID="textBoxTimebookConsultant" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookComments.ClientID %>">Comments</label>
                                <asp:TextBox ID="textBoxTimebookComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveTimebook" runat="server" Text="Save timebook" CssClass="btn btn-primary" OnClick="buttonSaveTimebook_Click" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
				<strong>Time book</strong> is a sheet for recording the time of arrival and departure of workers and for recording the amount of time spent on each job.
			</div>
            <table class="table table-hover small">
                <tr>
                    <th></th>
                    <th>Date</th>
                    <th>Department</th>
                    <th>Contact</th>
                    <th>Item</th>
                    <th>Unit</th>
                    <th>Qty</th>
                    <th>Price</th>
                    <th>Amount</th>
                    <th>Consultant</th>
                    <th>Status</th>
                    <th>Comments</th>
                    <th></th>
                </tr>
                <% foreach (var t in timebooks) { %>
                <tr>
                    <td><input type="checkbox"" /></td>
                    <td><%= t.Date.Value.ToString("yyyy-MM-dd") %></td>
                    <td><%= t.Department %></td>
                    <td><%= t.Contact.Contact %></td>
                    <td><%= t.Item.Name %></td>
                    <td><%= t.Item.Unit.Name %></td>
                    <td><%= t.Quantity.ToString() %></td>
                    <td><%= t.Price.ToString("# ##0.00") %></td>
                    <td><%= t.Amount.ToString("# ##0.00") %></td>
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
		<div class="tab-pane <%= selectedTab == "customer-prices" ? "active" : "" %>" id="customer-prices">
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
	                            <label for="<%= dropDownListItems.ClientID %>">Item</label>
                                <asp:DropDownList ID="dropDownListItems" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxItemPrice.ClientID %>">Price</label>
                                <asp:TextBox ID="textBoxItemPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveItem" runat="server" Text="Save price" CssClass="btn btn-primary" OnClick="buttonSaveItem_Click" />
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
                    <% if (p.Inactive) { %>
                        <tr>
                            <td><strike><%= p.Item.Name %></strike></td>
                            <td><strike><%= p.Price.ToString("0.00") %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", string.Format("customerpriceedit.aspx?Id={0}&CustomerId={1}", p.Id, id)) %>
                                <%= HtmlHelper.Anchor("Delete", string.Format("customerpricedelete.aspx?Id={0}&CUstomerId={1}", p.Id, id), "onclick=\"return confirm('Are you sure you want to delete this customer price?')\"")%>
                                <%= HtmlHelper.Anchor("Move Up", "") %>
                                <%= HtmlHelper.Anchor("Move Down", "") %>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= p.Item.Name %></td>
                            <td><%= p.Price.ToString("0.00") %></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", string.Format("customerpriceedit.aspx?Id={0}&CustomerId={1}", p.Id, id)) %>
                                <%= HtmlHelper.Anchor("Deactivate", string.Format("customerpricedeactivate.aspx?Id={0}&CustomerId={1}", p.Id, id)) %>
                                <%= HtmlHelper.Anchor("Move Up", "") %>
                                <%= HtmlHelper.Anchor("Move Down", "") %>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane <%= selectedTab == "customer-info" ? "active" : "" %>" id="customer-info">
            <br />
			<div class="alert alert-info">
				<strong>Customer information</strong> is lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
			</div>
            <table class="table">
                <tr>
                    <td style="width: 30%;"><strong>Customer Number</strong></td>
                    <td>
                        <asp:Label ID="labelCustomerNumber" runat="server" Text="" CssClass="form-control"></asp:Label>
                        <asp:TextBox ID="textBoxCustomerNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Postal Address</strong></td>
                    <td>
                        <asp:Label ID="labelInvoiceAddress" runat="server" Text="Label"></asp:Label>
                    </td>
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
                    <td>
                        <asp:Label ID="labelOurReferencePerson" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <asp:Button ID="buttonDeactivate" runat="server" Text="Deactivate this customer" CssClass="btn btn-warning" OnClientClick="return confirm('Are you sure you want to de-activate this customer?')" />
            </div>
		</div>
        <div class="tab-pane <%= selectedTab == "contact-persons" ? "active" : "" %>" id="contact-persons">
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
                                <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
                                </asp:RadioButtonList>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveContact" runat="server" Text="Save contact" CssClass="btn btn-primary" OnClick="buttonSaveContact_Click" />
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
                    <th></th>
                </tr>
                <% foreach (var c in contacts) { %>
                    <% if (c.Inactive) { %>
                        <tr>
                            <td><%= c.GetContactType() %></td>
                            <td><strike><%= c.Contact %></strike></td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Mobile %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", "") %>
                                <%= HtmlHelper.Anchor("Delete", string.Format("customercontactdelete.aspx?Id={0}&CustomerId={1}", c.Id, id)) %>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= c.GetContactType() %></td>
                            <td><%= c.Contact %></td>
                            <td><%= c.Phone %></td>
                            <td><%= c.Mobile %></td>
                            <td><%= c.Email %></td>
                            <td>
                                <%= HtmlHelper.Anchor("Edit", "") %>
                                <%= HtmlHelper.Anchor("Deactivate", string.Format("customercontactdeactivate.aspx?Id={0}&CustomerId={1}", c.Id, id)) %>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
	</div>
</div>

</asp:Content>
