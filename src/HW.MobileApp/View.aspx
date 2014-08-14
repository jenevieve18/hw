﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="HW.MobileApp.View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        select 
        {
            background-color:transparent;
            border:none;
            width:100%;
            height:30px;
            -webkit-appearance: none;
            -moz-appearance: none;
            text-overflow: '';
            direction:rtl;
            padding:8px 0px 0px 55%;
            
            
            
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Statistics.aspx" data-icon="arrow-l">Cancel</a>
    <h1>View...</h1>
    <a runat="server" onserverclick="doneBtn_Click" data-icon="check">Done</a>
</div>
<div data-role="content">
    <ul data-role="listview">
        <li class="minihead">
            Select results to view & compare
        </li>
        <li data-icon="false" style="padding:0px 0px 0px 0px;">
            <div>
                <div style="position:absolute; margin:8px 0px 0px 18px;">
                    Timeframe
                </div>
                <asp:DropDownList ID="ddlTimeframe" runat="server" data-role="none">
                <asp:ListItem>Latest</asp:ListItem>
                <asp:ListItem>Past week</asp:ListItem>
                <asp:ListItem>Past month</asp:ListItem>
                <asp:ListItem>Past Year</asp:ListItem>
                <asp:ListItem>Since first measure</asp:ListItem>
                </asp:DropDownList>
            </div>
        </li>
        <li data-icon="false" style="padding:0px 0px 0px 0px;">
        <div>
            <div style="position:absolute; margin:8px 0px 0px 18px;">
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