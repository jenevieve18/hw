<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="MeasurementsList.aspx.cs" Inherits="HW.MobileApp.MeasurementsList" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<div data-role="header" data-theme="b" data-position="fixed">
    <a <%="href='ActivityMeasurement.aspx?datetime="+datetime+"'" %> data-icon="arrow-l"><%= R.Str(lang, "button.back")%></a>
    <h1></h1>
</div>

<div data-role="content" >
    <ul data-role="listview">
        <%foreach (var c in category)
          { %>
            <li data-icon="false">
            <%var measlink = "href='MeasurementForm.aspx?mcid="+c.measureCategoryID+"&datetime="+Request.QueryString["datetime"]+"'"; %>
            <a <%=measlink %> rel="external">
                <span><%=c.measureCategory%></span>  
               </a> 
            </li>
        <%} %>

    </ul>
</div>

</asp:Content>
