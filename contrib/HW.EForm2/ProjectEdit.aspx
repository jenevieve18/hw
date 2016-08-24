<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="HW.EForm2.ProjectEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Internal</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Logo</td>
            <td></td>
        </tr>
        <tr>
            <td>Survey(s)</td>
            <td></td>
        </tr>
        <tr>
            <td>Add Survey</td>
            <td>
                <asp:DropDownList ID="dropDownListSurvey" runat="server"></asp:DropDownList></td>
        </tr>
    </table>
</asp:Content>
