<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerNotesEdit.aspx.cs" Inherits="HW.Invoicing.CustomerNotesEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit customer note</h3>
<div class="form-group">
	<label for="<%= textBoxNotes.ClientID %>">Note</label>
    <asp:TextBox ID="textBoxNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
</div>
<asp:PlaceHolder ID="placeHolderReactivate" runat="server">
    <div class="form-group">
        <asp:CheckBox ID="checkBoxReactivate" runat="server" CssClass="form-control" Text="&nbsp;Re-activate this customer notes" />
    </div>
</asp:PlaceHolder>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer note" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}", customerId)) %></i>
</div>

</asp:Content>
