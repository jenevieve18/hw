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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:Panel ID="Panel1" runat="server">
        <h3>Edit customer agreement</h3>
        <div class="form-group">
	        <label for="<%= textBoxAgreementLecturer.ClientID %>">Lecturer</label>
            <asp:TextBox ID="textBoxAgreementLecturer" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementDate.ClientID %>">Date</label>
            <asp:TextBox ID="textBoxAgreementDate" runat="server" CssClass="date form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementRuntime.ClientID %>">Runtime</label>
            <asp:TextBox ID="textBoxAgreementRuntime" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementLectureTitle.ClientID %>">Lecture Title</label>
            <asp:TextBox ID="textBoxAgreementLectureTitle" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
	        <label for="<%= textBoxAgreementLocation.ClientID %>">Location</label>
            <asp:TextBox ID="textBoxAgreementLocation" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
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
        <%--<div class="form-group">
	        <label for="<%= textBoxAgreementBillingAddress.ClientID %>">Billing Address</label>
            <asp:TextBox ID="textBoxAgreementBillingAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Height="100"></asp:TextBox>
        </div>--%>
        <div class="form-group">
	        <label for="<%= textBoxAgreementOtherInformation.ClientID %>">Other Information</label>
            <asp:TextBox ID="textBoxAgreementOtherInformation" runat="server" CssClass="form-control" ViewStateMode="Inherit" TextMode="MultiLine" Height="210"></asp:TextBox>
        </div>

        <div>
            <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer agreement" 
                onclick="buttonSave_Click" />
                or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}", customerId)) %></i>
        </div>
    </asp:Panel>

</asp:Content>
