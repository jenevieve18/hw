<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Stats.aspx.cs" Inherits="HW.Grp3.Stats" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Timeframe"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList> --
    <asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
    <asp:Label ID="Label2" runat="server" Text="Survey"></asp:Label>
    <asp:DropDownList ID="DropDownList3" runat="server">
    </asp:DropDownList>
    <asp:Label ID="Label3" runat="server" Text="Aggregation"></asp:Label>
    <asp:DropDownList ID="DropDownList4" runat="server">
    </asp:DropDownList>
    <asp:Label ID="Label4" runat="server" Text="Grouping"></asp:Label>
    <asp:DropDownList ID="DropDownList5" runat="server">
    </asp:DropDownList>
    <asp:Label ID="Label5" runat="server" Text="Language"></asp:Label>
    <asp:DropDownList ID="DropDownList6" runat="server">
    </asp:DropDownList>

</asp:Content>
