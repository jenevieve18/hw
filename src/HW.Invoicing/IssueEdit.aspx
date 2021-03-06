﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="IssueEdit.aspx.cs" Inherits="HW.Invoicing.IssueEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Edit an issue</h3>
    <div class="form-group">
	    <label for="<%= textBoxTitle.ClientID %>">Title</label>
        <asp:TextBox ID="textBoxTitle" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
	    <label for="<%= textBoxDescription.ClientID %>">Description</label>
        <asp:TextBox ID="textBoxDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div class="form-group">
	    <label for="<%= dropDownListMilestone.ClientID %>">Milestone</label>
        <asp:DropDownList ID="dropDownListMilestone" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <div class="form-group">
	    <label for="<%= dropDownListPriority.ClientID %>">Priority</label>
        <asp:DropDownList ID="dropDownListPriority" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <div class="form-group">
	    <label for="<%= dropDownListStatus.ClientID %>">Status</label>
        <asp:DropDownList ID="dropDownListStatus" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <%-- 
    <asp:PlaceHolder ID="placeHolderReactivate" runat="server">
        <div class="form-group">
            <asp:CheckBox ID="checkBoxReactivate" runat="server" CssClass="form-control" Text="&nbsp;Reactivate this item" />
        </div>
    </asp:PlaceHolder>
        --%>
    <div>
        <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save issue" 
            onclick="buttonSave_Click" />
            or <i><%= HtmlHelper.Anchor("cancel", "issues.aspx") %></i>
    </div>

    <%--<h3>Comments</h3>

    <% if (issue != null) { %>
        <% foreach (var c in issue.Comments) { %>
            <div class="well">
                <%= c.Comments %>
            </div>
        <% } %>
    <% } %>--%>

</asp:Content>
