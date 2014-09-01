<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="HW.MobileApp.Support" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="More.aspx" ><%= R.Str("button.back") %></a>
    <h1><%= R.Str("support.title") %></h1>
</div>
<div data-role="content" id="support">
    
         <div class="div1" data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u"  data-inset="false" data-iconpos="right" >
            <h1><%= R.Str("support.healthTrack")%></h1>
            <div data-role="content">
            <%= R.Str("support.healthTrack.description")%>
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
            <h1><%= R.Str("support.changeEmail")%></h1>
            <div data-role="content">
                 <%= R.Str("support.changeEmail.description")%>
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1><%= R.Str("support.calendar")%></h1>
                    <div data-role="content"><%= R.Str("support.calendar.description")%></div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1><%= R.Str("support.faq")%></h1>
                    <div data-role="content"><%= R.Str("support.faq.descriptioin")%>
                </div>
        </div>
    
</div>

</asp:Content>
