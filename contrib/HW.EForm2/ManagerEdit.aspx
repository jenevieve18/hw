<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ManagerEdit.aspx.cs" Inherits="HW.EForm2.ManagerEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Manager information</h3>
    <table>
        <tr>
            <td>Name</td>
            <td><asp:TextBox ID="textBoxName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email</td>
            <td><asp:TextBox ID="textBoxEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Password</td>
            <td><asp:TextBox ID="textBoxPassword" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Phone</td>
            <td><asp:TextBox ID="textBoxPhone" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr>
            <td>Project Round</td>
            <td><asp:DropDownList ID="dropDownListProjectRounds" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropDownListProjectRounds_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Units</td>
            <td><asp:CheckBoxList ID="checkBoxListUnits" runat="server"></asp:CheckBoxList></td>
        </tr>
    </table>
</asp:Content>
