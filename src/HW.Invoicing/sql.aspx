<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="HW.Invoicing.sql" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
<h3>Enter SQL Command</h3>
                <span style="font-style:italic;color:#ff5b2b"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></span>
                <p><asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="160px" 
                    Width="440px"></asp:TextBox></p>
                <p><asp:Button ID="Button1" runat="server" Text="Execute" 
                    onclick="Button1_Click" /></p>
                    </div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>

</asp:Content>
