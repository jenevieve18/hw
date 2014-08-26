<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="HW.MobileApp.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Statistics.aspx" data-icon="arrow-l">Cancel</a>
    <h1>View...</h1>
    <a runat="server" onserverclick="doneBtn_Click" data-icon="check">Done</a>
</div>
<div data-role="content" id="view">
    <ul data-role="listview">
        <li class="minihead">
            Select results to view & compare
        </li>
        <li data-icon="false" class="nopadding"">
            <div>
                <div class="selection">
                    Timeframe
                </div>
                <asp:DropDownList ID="ddlTimeframe" runat="server" data-role="none">
                <asp:ListItem>Latest</asp:ListItem>
                <asp:ListItem>Past week</asp:ListItem>
                <asp:ListItem>Past month</asp:ListItem>
                <asp:ListItem>Past year</asp:ListItem>
                <asp:ListItem>Since first measure</asp:ListItem>
                </asp:DropDownList>
            </div>
        </li>
        <li data-icon="false" class="nopadding">
        <div>
            <div class="selection">
                Compare with
            </div>
            <asp:DropDownList ID="ddlCompare" runat="server" data-role="none">
                <asp:ListItem>None</asp:ListItem>
                <asp:ListItem>Database</asp:ListItem>
            </asp:DropDownList>
        </div>
        </li>
    </ul>

    
</div>

</asp:Content>
