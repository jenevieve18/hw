<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundEdit.aspx.cs" Inherits="HW.EForm2.ProjectRoundEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Project Round Information</h3>
    <table>
        <tr>
            <td>Internal</td>
            <td>
                <asp:TextBox ID="textBoxInternal" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Survey</td>
            <td>
                <asp:DropDownList ID="dropDownListSurveys" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="buttonUpdate" runat="server" Text="Update" OnClick="buttonUpdate_Click" /></td>
        </tr>
        <tr>
            <td>Feedback</td>
            <td>
                <asp:DropDownList ID="dropDownListFeedback" runat="server"></asp:DropDownList></td>
        </tr>
    </table>
    <h4>Project Round Units</h4>
    <table>
        <% foreach (var pru in projectRound.Units) { %>
        <tr>
            <td><%= pru.Unit %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
