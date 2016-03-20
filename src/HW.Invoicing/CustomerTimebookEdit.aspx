<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebookEdit.aspx.cs" Inherits="HW.Invoicing.CustomerTimebookEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= dropDownListTimebookItems.ClientID %>').change(function () {
                console.log("changing item...");
                var selected = $(this).find('option:selected');
                var id = selected.data('id');
                <% if (timebook != null && timebook.Item != null) { %>
                    if (id != <%= timebook.Item.Id %>) {
                        console.log("triggering not same item...");
                        var selectedPrice = selected.data('price');
                        $('#<%= textBoxTimebookPrice.ClientID %>').val(selectedPrice);
                    }
                <% } %>
                var selectedUnit = selected.data('unit');
                $('#<%= labelTimebookUnit.ClientID %>').text(selectedUnit);
            });
            $('#<%= dropDownListSubscriptionTimebookItems.ClientID %>').change(function () {
                console.log("changing item...");
                var selected = $(this).find('option:selected');
                var id = selected.data('id');
                <% if (timebook != null && timebook.Item != null) { %>
                if (id != <%= timebook.Item.Id %>) {
                    console.log("triggering not same item...");
                    var selectedPrice = selected.data('price');
                    $('#<%= textBoxSubscriptionTimebookPrice.ClientID %>').val(selectedPrice);
                }
                <% } %>
                var selectedUnit = selected.data('unit');
                $('#<%= labelSubscriptionTimebookUnit.ClientID %>').text(selectedUnit);
            });
            $('#<%= dropDownListTimebookItems.ClientID %>').change();

            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            var textGeneratedComments = $('#<%= textBoxSubscriptionTimebookComments.ClientID %>');
            $('.date').change(function () {
                var startDate = $('#<%= textBoxSubscriptionTimebookStartDate.ClientID %>').datepicker('getDate');
                var endDate = $('#<%= textBoxSubscriptionTimebookEndDate.ClientID %>').datepicker('getDate');
                var months = monthDiff(startDate, endDate);
                $('#<%= textBoxSubscriptionTimebookQty.ClientID %>').val(months);
                var text = 'Subscription fee for HealthWatch.se';
                var generatedText = text + ' ' + $('#<%= textBoxSubscriptionTimebookStartDate.ClientID %>').val().replace(/-/g, ".") + ' - ' + $('#<%= textBoxSubscriptionTimebookEndDate.ClientID %>').val().replace(/-/g, ".");
                textGeneratedComments.val(generatedText);
            });
            $('#<%= textBoxSubscriptionTimebookQty.ClientID %>').change(function () {
                $('#<%= textBoxSubscriptionTimebookQty.ClientID %>').val($(this).val());
                var startDate = $('#<%= textBoxSubscriptionTimebookStartDate.ClientID %>').datepicker('getDate');
                //var months = parseInt($(this).val());
                /*var months = parseFloat($(this).val());
                var d = new Date(startDate);
                var currentMonth = d.getMonth();
                var newMonth = currentMonth + months;
                d = new Date(d.setMonth(newMonth));*/
                var d = addMonth(startDate, $(this).val());
                $('#<%= textBoxSubscriptionTimebookEndDate.ClientID %>').datepicker('update', d);
            });

            $('#<%= checkBoxTimebookIsHeader.ClientID %>').change(function() {
                if ($(this).is(':checked')) {
                    $('#<%= Panel2.ClientID %>').hide();
                    $('#<%= Panel1.ClientID %>').hide();
                } else {
                    $('#<%= Panel2.ClientID %>').show();
                    $('#<%= Panel1.ClientID %>').show();
                }
            });
            $('#<%= checkBoxTimebookIsHeader.ClientID %>').change();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Edit customer timebook</h3>
    <% if (message != null && message != "") { %>
    <div class="alert alert-warning">
        <%= message %>
    </div>
    <% } %>

    <asp:Panel ID="panelTimebook" runat="server">
        <div class="form-group">
	        <label for="<%= textBoxTimebookDate.ClientID %>">Date</label>
            <asp:TextBox ID="textBoxTimebookDate" runat="server" CssClass="date form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:CheckBox ID="checkBoxTimebookIsHeader" runat="server" CssClass="form-control" Text="&nbsp;This timebook is a header type" />
        </div>
        <asp:Panel ID="Panel2" runat="server">
            <div class="form-group">
                <asp:CheckBox ID="checkBoxTimebookDateHidden" runat="server" CssClass="form-control" Text="&nbsp;Timebook date is hidden" />
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
	            <label for="<%= textBoxTimebookVAT.ClientID %>">VAT</label>
                <asp:TextBox ID="textBoxTimebookVAT" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxTimebookConsultant.ClientID %>">Consultant</label>
                <asp:TextBox ID="textBoxTimebookConsultant" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </asp:Panel>
        <div class="form-group">
	        <label for="<%= textBoxTimebookComments.ClientID %>">Comments</label>
            <asp:TextBox ID="textBoxTimebookComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <div class="form-group">
	            <label for="<%= textBoxTimebookInternalComments.ClientID %>">Internal Comments</label>
                <asp:TextBox ID="textBoxTimebookInternalComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
        </asp:Panel>
        <asp:PlaceHolder ID="placeHolderReactivate" runat="server">
            <div class="form-group">
                <asp:CheckBox ID="checkBoxReactivate" runat="server" CssClass="form-control" Text="&nbsp;Re-activate this customer timebook" />
            </div>
        </asp:PlaceHolder>
    </asp:Panel>

    <asp:Panel ID="panelSubscriptionTimebook" runat="server">
        <table>
            <tr>
                <td>
                    <div class="form-group">
	                    <label for="<%= textBoxSubscriptionTimebookStartDate.ClientID %>">Start Date</label>
                        <asp:TextBox ID="textBoxSubscriptionTimebookStartDate" runat="server" CssClass="date form-control"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div class="form-group">
	                    <label for="<%= textBoxSubscriptionTimebookEndDate.ClientID %>">End Date</label>
                        <asp:TextBox ID="textBoxSubscriptionTimebookEndDate" runat="server" CssClass="date form-control"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div class="form-group">
	                    <label for="<%= textBoxSubscriptionTimebookQty.ClientID %>">Quantity</label>
                        <asp:TextBox ID="textBoxSubscriptionTimebookQty" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="form-group">
	                    <label for="<%= dropDownListSubscriptionTimebookItems.ClientID %>">Item</label>
                        <asp:DropDownList ID="dropDownListSubscriptionTimebookItems" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </td>
                <td>
                    <div class="form-group">
	                    <label for="<%= labelSubscriptionTimebookUnit.ClientID %>">Unit</label>
                        <asp:Label ID="labelSubscriptionTimebookUnit" runat="server" Text="" CssClass="form-control"></asp:Label>
                    </div>
                </td>
                <td>
                    <div class="form-group">
	                    <label for="<%= textBoxSubscriptionTimebookPrice.ClientID %>">Price</label>
                        <asp:TextBox ID="textBoxSubscriptionTimebookPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>

        <div class="form-group">
	        <label for="<%= textBoxSubscriptionTimebookComments.ClientID %>">Comments</label>
            <asp:TextBox ID="textBoxSubscriptionTimebookComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
    </asp:Panel>

    <div>
        <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer timebook" 
            onclick="buttonSave_Click" />
            or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", customerId)) %></i>
    </div>

</asp:Content>
