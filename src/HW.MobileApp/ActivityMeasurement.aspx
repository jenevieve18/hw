<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ActivityMeasurement.aspx.cs" Inherits="HW.MobileApp.ActivityMeasurement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <%var diarylink = "href='Diary.aspx?date="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'"; %>
    <a <%=diarylink %> data-icon="arrow-l" rel="external">Back</a>
    <h1>Activities & Measurements</h1>
    <%var measlink = "href='MeasurementsList.aspx?datetime="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'"; %>
    <a <%=measlink %> data-icon="plus" data-iconpos="notext"></a>

</div>
<div data-role="content" >
    <ul data-role="listview">
        <%  if (activities != null)
            {
                foreach (var ev in activities)
                { %>
            <li>
                <span><%=ev.description%>  <%=ev.result != null? " - "+ev.result.Replace(',','.'):"" %></span>        
                <span style="font-size:small;"><br /><%=ev.time.ToString("hh:mm")%></span>
            </li>
        <%}
            }
            else
            {%>
            <li>No activities and measurements</li>
            <%} %>

    </ul>
</div>



</asp:Content>
