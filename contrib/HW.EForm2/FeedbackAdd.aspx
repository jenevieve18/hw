<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="FeedbackAdd.aspx.cs" Inherits="HW.EForm2.FeedbackAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Feedback</td>
            <td>
                <asp:TextBox ID="textBoxFeedback" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="buttonSave" runat="server" Text="Save" OnClick="buttonSave_Click" /></td>
        </tr>
    </table>
</asp:Content>
