<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="HW.MobileApp.View" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Statistics.aspx"><%= R.Str("button.cancel") %></a>
    <h1><%= R.Str("view.title") %></h1>
    <a runat="server" onserverclick="doneBtn_Click"><%= R.Str("button.done") %></a>
</div>
<div data-role="content" id="view">
    <ul data-role="listview">
        <li class="minihead">
            <%= R.Str("view.select")%>
        </li>
        <li data-icon="false" class="nopadding"">
            <div>
                <div class="selection">
                    <%= R.Str("view.timeframe")%>
                </div>
                <asp:DropDownList ID="ddlTimeframe" runat="server" data-role="none">
                </asp:DropDownList>
            </div>
        </li>
        <li data-icon="false" class="nopadding">
        <div>
            <div class="selection">
                <%= R.Str("view.compare")%>
            </div>
            <asp:DropDownList ID="ddlCompare" runat="server" data-role="none">
            </asp:DropDownList>
        </div>
        </li>
    </ul>

    
</div>

</asp:Content>
