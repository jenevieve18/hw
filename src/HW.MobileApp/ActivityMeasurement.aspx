<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ActivityMeasurement.aspx.cs" Inherits="HW.MobileApp.ActivityMeasurement" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function deleteEvent(e) {
            e = e || window.event;
            var target = e.target || e.srcElement;
            $('#<%=hdEventId.ClientID %>').val(target.id);
            $('#popuplink').click();

        }

    </script>

    <div data-role="popup" id="mypopup" data-overlay-theme="" data-position-to="window" data-theme="c" style="max-width:400px;" class="ui-corner-all">
		<div data-role="header" data-theme="a" class="ui-corner-top">
			<h1>Delete Event?</h1>
		</div>
		<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content">
			<h3>Are you sure you want to delete this activity/measurement?</h3>
			<p>This action cannot be undone.</p>
			<a data-role="button" data-inline="true" data-rel="back" data-theme="c"><%= R.Str(lang, "button.cancel") %></a>    
			<a runat="server" onserverclick="deleteActivity" data-role="button" data-inline="true" action="callserverdelete()" data-transition="flow" data-theme="b">Delete</a>  
		</div>
	</div>

    <div data-role="header" data-theme="b" data-position="fixed">
        <%var diarylink = "href='Diary.aspx?date="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'"; %>
        <a <%=diarylink %> data-icon="arrow-l" rel="external"><%= R.Str(lang, "button.back")%></a>
        <h1><%= R.Str(lang, "measurement.viewAdd")%></h1>
        <%var measlink = "href='MeasurementsList.aspx?datetime="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'"; %>
        <a <%=measlink %> data-icon="plus" data-iconpos="notext"></a>

    </div>

    <div data-role="content" >
        <asp:HiddenField ID="hdEventId" runat="server" />

        <ul data-role="listview">
            <% if (activities != null) { %>
                <% foreach (var ev in activities) { %>
                    <li>
                        <span><%= ev.description %>  <%= ev.result != null? " - "+ev.result.Replace(',','.'):"" %></span>        
                        <span style="font-size:small;"><br /><%=ev.time.ToString("hh:mm")%></span>
                        <div style="position:absolute;right:10px;top:10px;margin:0px;padding:0px;">
                        <% var buttonid = "id='" + ev.eventID + "$*#"+ev.formInstanceKey+"'"; %>
                        <% if (ev.eventID != 0) {%>
                            <input <%=buttonid %> type="button" onclick="deleteEvent(event)" data-icon="delete" data-iconpos="notext" />
                        <%} %>
                        </div>
                    </li>
                <%} %>
            <% } else {%>
                <li><%= R.Str(lang, "measurement.none")%></li>
            <%} %>

        </ul>
    </div>
    <asp:LinkButton runat="server" ID="deleteBtn" OnClick="deleteActivity" style="display:hidden;"/>
    <a id="popuplink" href="#mypopup" data-rel="popup"></a>

</asp:Content>
