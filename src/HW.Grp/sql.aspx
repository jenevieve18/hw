<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="sql.aspx.cs" Inherits="HW.Grp.sql" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	<div class="contentgroup grid_16">
		<div id="contextbar">
			<div class="settingsPane">
                <h3>Enter SQL Command</h3>
                <span style="font-style:italic;color:#ff5b2b">
                    <asp:Label ID="labelMessage" runat="server" Text=""></asp:Label>
                </span>
                
                <p>PIN: <asp:TextBox ID="textBoxPIN" runat="server" TextMode="Password"></asp:TextBox></p>
                <p><asp:TextBox ID="textBoxSql" runat="server" TextMode="MultiLine" Height="160px" 
                    Width="440px"></asp:TextBox></p>
                <p><asp:Button ID="buttonExecute" runat="server" Text="Execute" 
                    onclick="buttonExecute_Click" /></p>
            </div>
            <asp:GridView ID="gridViewResult" runat="server">
            </asp:GridView>
        </div>
    </div>

</asp:Content>
