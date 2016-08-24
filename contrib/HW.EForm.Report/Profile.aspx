<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HW.EForm.Report.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Profile</h3>
    <div class="form-group">
        <label>Username</label>
        <asp:TextBox ID="textBoxUsername" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Password</label>
        <asp:TextBox ID="textBoxPassword" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Confirm Password</label>
        <asp:TextBox ID="textBoxConfirmPassword" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Email Address</label>
        <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Phone</label>
        <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Button ID="buttonSave" runat="server" Text="Save profile" CssClass="btn btn-success" OnClick="buttonSave_Click" />
    </div>
</asp:Content>
