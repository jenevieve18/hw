<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectEdit.aspx.cs" Inherits="HW.EForm2.ProjectEdit" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Internal</td>
            <td>
                <asp:TextBox ID="textBoxInternal" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Logo</td>
            <td></td>
        </tr>
        <tr>
            <td>Survey(s)</td>
            <td>
                <ul>
                <% foreach (var s in project.Surveys) { %>
                    <li><%= s.Survey.ToString() %></li>
                <% } %>
                </ul>
            </td>
        </tr>
        <tr>
            <td>Add Survey</td>
            <td>
                <asp:DropDownList ID="dropDownListSurvey" runat="server"></asp:DropDownList></td>
        </tr>
    </table>

    <h4>Project Rounds</h4>
    <table>
        <% foreach (var pr in project.Rounds) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(pr.Internal, "projectroundedit.aspx?ProjectRoundID=" + pr.ProjectRoundID) %></td>
            <td><%= pr.Survey.ToString() %></td>
            <td>
                <ul>
                    <ul>
                    <% foreach (var pru in pr.Units) { %>
                        <li><%= pru.Unit %></li>
                    <% } %>
                    </ul>
                </ul>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
