<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="PasswordActivation.aspx.cs" Inherits="HW.Grp.PasswordActivation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    #admin #contextbar .actionPane 
    {
        border-top: 1px solid #b0e1f3;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="contentgroup grid_16">
    <div id="contextbar">
        <div class="actionPane">
            <div class="actionBlock">
                <span style="width:150px" class="desc">New Password</span><asp:TextBox 
                    ID="textBoxPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                <span style="width:150px" class="desc">Confirm Password</span><asp:TextBox 
                    ID="textBoxConfirmPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                <span style="color: #CC0000;">
                    <asp:Label ID="labelErrorMessage" runat="server" /><br />
                </span>
                <asp:Button ID="buttonActivate" runat="server" Text="Activate Password" 
                    onclick="buttonActivate_Click" />
            </div>
        </div>
    </div>
</div>

</asp:Content>
