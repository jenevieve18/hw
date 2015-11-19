<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebooks.aspx.cs" Inherits="HW.Invoicing.CustomerTimebooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {
            init();
        });
        function init() {
            $('.customer').change(onCustomerChange);
        }
        function onCustomerChange() {
            var customerId = $(this).val();
            var timebookId = $(this).data('timebookid');
            //alert(timebookId);
            $.ajax({
                type: "POST",
                url: "CustomerTimebooks.aspx/FindSubscribersByCompany",
                data: JSON.stringify({ companyId: <%= Session["CompanyId"] %>, customerId: customerId, timebookId: timebookId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#customers').empty();
                    var customers = '';
                    $.each(data.d, function (i, c) {
                        customers += "<tr>" +
                            "<td colspan='3'><strong>" + c.name + "</strong></td>" +
                            "</tr>";
                        $.each(c.timebooks, function (j, t) {
                            var options = "<select class='form-control customer' data-timebookid='" + t.id + "'>";
                            $.each(data.d, function(i, o) {
                                var selected = o.id == c.id ? "selected" : "";
                                options += "<option value='" + o.id + "' " + selected + ">" + o.name + "</option>";
                            });
                            options += "</select>";
                            customers += "<tr>" +
                                "<td style='width:32px'></td>" +
                                "<td>" + t.timebook + "</td>" +
                                "<td>" + options + "</td>" +
                                "</tr>";
                        });
                    });
                    $('#customers').html(customers);
                    init();
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Timebook Utilities</h3>
<div class="alert alert-info">
	<strong>Subscription</strong> is the action of making or agreeing to make an advance payment in order to receive or participate in something.
</div>

<table class="table table-hover">
    <tr>
        <th colspan="2">Customer</th>
        <th>Move to</th>
    </tr>
    <tbody id="customers">
    <% foreach (var c in customers) { %>
        <tr>
            <td colspan="3"><strong><%= c.Name %></strong></td>
        </tr>
        <% foreach (var t in c.Timebooks) { %>
        <tr>
            <td style="width:32px"></td>
            <td><%= t.ToString() %></td>
            <td>
                <select class="form-control customer" data-timebookid="<%= t.Id %>">
                    <% foreach (var m in customers) { %>
                        <% var selected = m.Id == c.Id ? "selected" : ""; %>
                        <option value="<%= m.Id %>" <%= selected %>><%= m.Name %></option>
                    <% } %>
                </select>
            </td>
        </tr>
        <% } %>
    <% } %>
    </tbody>
</table>


</asp:Content>
