<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CompanyEdit.aspx.cs" Inherits="HW.Invoicing.CompanyEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
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

<h3>Edit a company</h3>

<div class="tabbable" id="tabs-813930">
	<ul class="nav nav-tabs">
		<li class="active">
			<a href="#panel-20704" data-toggle="tab">Company Info</a>
		</li>
		<li>
			<a href="#panel-281177" data-toggle="tab">Terms and Advice</a>
		</li>
        <li>
			<a href="#panel-931818" data-toggle="tab">Agreement Email Text</a>
		</li>
	</ul>
	<div class="tab-content">
		<div class="tab-pane active" id="panel-20704">
			<br />
            <div class="form-group">
	            <label for="<%= textBoxName.ClientID %>">Company Name</label>
                <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAddress.ClientID %>">Address</label>
                <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxPhone.ClientID %>">Phone</label>
                <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxEmail.ClientID %>">Email</label>
                <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxWebsite.ClientID %>">Website</label>
                <asp:TextBox ID="textBoxWebsite" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxBankAccountNumber.ClientID %>">Bankgiro</label>
                <asp:TextBox ID="textBoxBankAccountNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxTIN.ClientID %>">TIN/Momsregistreringsnummer</label>
                <asp:TextBox ID="textBoxTIN" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxOrganizationNumber.ClientID %>">Oganization Number</label>
                <asp:TextBox ID="textBoxOrganizationNumber" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="<%= fileUploadSignature.ClientID %>">Signature</label>
                <% if (company.HasSignature) { %>
                    <br /><img src="uploads/<%= company.Signature %>" />
                <% } %>
                <asp:FileUpload ID="fileUploadSignature" runat="server" />
            </div>
            <div class="form-group">
	            <label for="<%= textBoxFinancialMonthStart.ClientID %>">Start Financial Year</label>
                <asp:TextBox ID="textBoxFinancialMonthStart" runat="server" CssClass="date form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxFinancialMonthEnd.ClientID %>">End Financial Year</label>
                <asp:TextBox ID="textBoxFinancialMonthEnd" runat="server" CssClass="date form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxInvoicePrefix.ClientID %>">Invoice Prefix</label>
                <asp:TextBox ID="textBoxInvoicePrefix" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
	            <label for="<%= textBoxAgreementPrefix.ClientID %>">Agreement Prefix</label>
                <asp:TextBox ID="textBoxAgreementPrefix" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="<%= fileUploadInvoiceLogo.ClientID %>">Invoice Logo</label>
                <% if (company.HasInvoiceLogo) { %>
                    <br /><img src="uploads/<%= company.InvoiceLogo %>" />
                <% } %>
                <asp:FileUpload ID="fileUploadInvoiceLogo" runat="server" />
            </div>
            <div class="form-group">
                <label for="<%= fileUploadInvoiceTemplate.ClientID %>">Invoice Template</label>
                <% if (company.HasInvoiceTemplate) { %>
                    <br /><%= HtmlHelper.Anchor(company.InvoiceTemplate, "uploads/" + company.InvoiceTemplate, "target='_blank'") %>
                <% } %>
                <asp:FileUpload ID="fileUploadInvoiceTemplate" runat="server" />
            </div>
            <div class="form-group">
                <label for="<%= fileUploadAgreementTemplate.ClientID %>">Agreement Template</label>
                <% if (company.HasAgreementTemplate) { %>
                    <br /><%= HtmlHelper.Anchor(company.AgreementTemplate, "uploads/" + company.AgreementTemplate, "target='_blank'")%>
                <% } %>
                <asp:FileUpload ID="fileUploadAgreementTemplate" runat="server" />
            </div>
            <div class="form-group">
                <asp:CheckBox ID="checkBoxHasSubscriber" runat="server" CssClass="form-control" Text="&nbsp;This company has subscribers." />
            </div>
            <div>
                <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save company info" 
                    onclick="buttonSave_Click" />
                    or <i><%= HtmlHelper.Anchor("cancel", "companies.aspx") %></i>
            </div>
		</div>
		<div class="tab-pane" id="panel-281177">
			<br />
            <div class="form-group">
                <label for="<%= textBoxTerms.ClientID %>">Terms</label>
                <asp:TextBox ID="textBoxTerms" runat="server" CssClass="form-control" TextMode="MultiLine" Height="450"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="buttonSaveTerms" CssClass="btn btn-success" runat="server" Text="Save company terms and advice" 
                    onclick="buttonSaveTerms_Click" />
                    or <i><%= HtmlHelper.Anchor("cancel", "companies.aspx") %></i>
            </div>
		</div>
        <div class="tab-pane" id="panel-931818">
            <br />
            <asp:Panel ID="Panel1" runat="server" DefaultButton="buttonSaveAgreementEmailText">
                <div class="tabbable" id="tabs-64570">
				    <ul class="nav nav-tabs">
					    <li class="active">
						    <a href="#panel-67773" data-toggle="tab">Email Texts</a>
					    </li>
					    <li>
						    <a href="#panel-807776" data-toggle="tab">Signed Email Texts</a>
					    </li>
				    </ul>
				    <div class="tab-content">
					    <div class="tab-pane active" id="panel-67773">
                            <br />
                            <div class="form-group">
	                            <label for="<%= textBoxAgreementEmailSubject.ClientID %>">Agreement Email Subject</label>
                                <asp:TextBox ID="textBoxAgreementEmailSubject" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= textBoxAgreementEmailText.ClientID %>">Agreement Email Text</label>
                                <asp:TextBox ID="textBoxAgreementEmailText" runat="server" CssClass="form-control" TextMode="MultiLine" Height="450"></asp:TextBox>
                            </div>
					    </div>
					    <div class="tab-pane" id="panel-807776">
                            <br />
                            <div class="form-group">
	                            <label for="<%= textBoxAgreementSignedEmailSubject.ClientID %>">Agreement Email Subject</label>
                                <asp:TextBox ID="textBoxAgreementSignedEmailSubject" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="<%= textBoxAgreementSignedEmailText.ClientID %>">Agreement Email Text</label>
                                <asp:TextBox ID="textBoxAgreementSignedEmailText" runat="server" CssClass="form-control" TextMode="MultiLine" Height="450"></asp:TextBox>
                            </div>
					    </div>
				    </div>
			    </div>
                <div>
                    <asp:Button ID="buttonSaveAgreementEmailText" CssClass="btn btn-success" runat="server" Text="Save agreement email text" 
                        onclick="buttonSaveAgreementEmailText_Click" />
                        or <i><%= HtmlHelper.Anchor("cancel", "companies.aspx") %></i>
                </div>
            </asp:Panel>
		</div>
	</div>
</div>


</asp:Content>
