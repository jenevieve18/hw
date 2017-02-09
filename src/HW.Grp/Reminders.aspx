<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="HW.Grp.Reminders" %>

<%@ Import Namespace="HW.Grp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
                <asp:PlaceHolder ID="Org" runat="server" />

                <p>
                    <%= R.Str(lid, "reminder.star", "* = setting inherited from parent unit") %>
                </p>
                <asp:Button ID="buttonSave" CssClass="btn" runat="server" Text="Save"
                    OnClick="buttonSave_Click" />
            </div>
        </div>
    </div>

</asp:Content>
