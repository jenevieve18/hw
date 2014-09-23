<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="HW.MobileApp.Support" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="More.aspx" ><%= R.Str(language,"button.back") %></a>
    <h1><%= R.Str(language, "support.title")%></h1>
</div>
<div data-role="content" id="support">
    
         <div class="div1" data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u"  data-inset="false" data-iconpos="right" >
            <h1><%= R.Str(language, "support.healthTrack")%></h1>
            <div data-role="content">
            <%= R.Str(language, "support.healthTrack.description")%>
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
            <h1><%= R.Str(language, "support.changeEmail")%></h1>
            <div data-role="content">
                 <%= R.Str(language, "support.changeEmail.description")%>
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1><%= R.Str(language, "support.calendar")%></h1>
                    <div data-role="content"><%= R.Str(language, "support.calendar.description")%></div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1><%= R.Str(language, "support.faq")%></h1>
                    <div data-role="content"><%= R.Str(language, "support.faq.descriptioin")%>
                </div>
        </div>
    
</div>

</asp:Content>
