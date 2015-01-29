<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="HW.Grp.Reminders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
        <div id="contextbar">
            <div class="settingsPane">
                <asp:PlaceHolder ID="Org" runat="server" />
                <asp:Button ID="buttonSave" CssClass="btn" runat="server" Text="Save" 
                    onclick="buttonSave_Click" />
            </div>
        </div>
    </div>

</asp:Content>
