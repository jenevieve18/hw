<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.Grp.Settings" %>

<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
                <asp:PlaceHolder ID="Txt" runat="server"><%= R.Str(lid, "password.change", "Change password")%></asp:PlaceHolder>
                <asp:TextBox ID="Password" runat="server" TextMode="Password" />
                <asp:Button ID="Save" runat="server" CssClass="btn" Text="Save" />
                <asp:Label ID="Message" runat="server" />
            </div>
        </div>

        <div class="smallContent">
        </div>
    </div>

</asp:Content>
