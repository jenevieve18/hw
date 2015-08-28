<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerAgreementEdit.aspx.cs" Inherits="HW.Invoicing.CustomerAgreementEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
        });
    </script>
    
    <style type="text/css">
        .label-width
        {
            width:200px;
        }
        .label2-width
        {
            width:100px;
        }
        .date-width 
        {
            width:120px;
        }
        .time-width 
        {
            width:75px;
        }
        .icon-width 
        {
            width:16px;
        }
        .compensation 
        {
            text-align:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Panel ID="Panel1" runat="server">
        <h3>Edit customer agreement</h3>
        <div class="form-group">
	        <label for="<%= textBoxAgreementLecturer.ClientID %>">Lecturer</label>
            <asp:TextBox ID="textBoxAgreementLecturer" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementDate.ClientID %>">Date Created</label>
            <asp:TextBox ID="textBoxAgreementDate" runat="server" CssClass="date form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementLectureTitle.ClientID %>">Lecture Title</label>
            <asp:TextBox ID="textBoxAgreementLectureTitle" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <table width="100%" cellpadding="2">
            <tr>
                <th>Date of Lecture</th>
                <th>Time From</th>
                <th>Time To</th>
                <th>Address</th>
            </tr>
            <% foreach (var d in agreement.DateTimeAndPlaces) { %>
                <tr>
                    <td class="date-width"><%= FormHelper.Input("agreement-date", d.Date.Value.ToString("yyyy-MM-dd"), "class='date form-control'") %></td>
                    <td class="time-width"><%= FormHelper.Input("agreement-timefrom", d.TimeFrom, "class='form-control'") %></td>
                    <td class="time-width"><%= FormHelper.Input("agreement-timeto", d.TimeTo, "class='form-control'") %></td>
                    <td><%= FormHelper.Input("agreement-address", d.Address, "class='form-control'") %></td>
                </tr>
            <% } %>
        </table>

        <br />
        <div class="form-group">
	        <label for="<%= textBoxAgreementContact.ClientID %>">Contact</label>
            <asp:TextBox ID="textBoxAgreementContact" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementMobile.ClientID %>">Mobile</label>
            <asp:TextBox ID="textBoxAgreementMobile" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementEmail.ClientID %>">Email</label>
            <asp:TextBox ID="textBoxAgreementEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementCompensation.ClientID %>">Compensation</label>
            <asp:TextBox ID="textBoxAgreementCompensation" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementPaymentTerms.ClientID %>">Payment Terms</label>
            <asp:TextBox ID="textBoxAgreementPaymentTerms" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementOtherInformation.ClientID %>">Other Information</label>
            <asp:TextBox ID="textBoxAgreementOtherInformation" runat="server" CssClass="form-control" ViewStateMode="Inherit" TextMode="MultiLine" Height="210"></asp:TextBox>
        </div>
        
        <div class="form-group">
	        <label for="<%= textBoxAgreementContactPlaceSigned.ClientID %>">Contact Place Signed</label>
            <asp:TextBox ID="textBoxAgreementContactPlaceSigned" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementContactDateSigned.ClientID %>">Contact Date Signed</label>
            <asp:TextBox ID="textBoxAgreementContactDateSigned" runat="server" CssClass="date form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementContactName.ClientID %>">Contact Name</label>
            <asp:TextBox ID="textBoxAgreementContactName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementContactTitle.ClientID %>">Contact Title</label>
            <asp:TextBox ID="textBoxAgreementContactTitle" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementContactCompany.ClientID %>">Company</label>
            <asp:TextBox ID="textBoxAgreementContactCompany" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="form-group">
	        <label for="<%= textBoxAgreementDateSigned.ClientID %>">Date Signed</label>
            <asp:TextBox ID="textBoxAgreementDateSigned" runat="server" CssClass="date form-control"></asp:TextBox>
        </div>

        <asp:PlaceHolder ID="placeHolderClosed" runat="server">
            <div class="form-group">
                <asp:CheckBox ID="checkBoxClosed" runat="server" CssClass="form-control" Text="&nbsp;This customer agreement is closed" />
            </div>
        </asp:PlaceHolder>

        <div>
            <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer agreement" 
                onclick="buttonSave_Click" />
                or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}&SelectedTab=agreements", customerId)) %></i>
        </div>
    </asp:Panel>

</asp:Content>
