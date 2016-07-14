<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Org.aspx.cs" Inherits="HW.Grp3.Org" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Organization - Group Administration | HealthWatch</title>

    <style>
        .search-box
        {
            background: #e3f4fb;
            padding: 20px 0;
            border-top: 1px solid #b0e1f3;
            border-bottom: 1px solid #b0e1f3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="search-box">
        <div class="row">
            <div class="col-md-3">Search user by email</div>
            <div class="col-md-3 form-group">
                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Search" />
            </div>
        </div>
    </div>

    <nav>
        <ul>
            <li><a href="">Add unit</a></li>
            <li><a href="">Import units</a></li>
            <li><a href="">Add user</a></li>
            <li><a href="">Import users</a></li>
        </ul>
    </nav>

</asp:Content>
