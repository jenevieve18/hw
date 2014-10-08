<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="HW.MobileApp.Calendar" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div data-role="header" data-theme="b" data-position="fixed">
        <a href="Diary.aspx" data-icon="arrow-l" rel="external"><%= R.Str(lang, "button.back")%></a>
        <h1><%= R.Str(lang, "calendar.title")%></h1>

    </div>
    <div data-role="content" >
        <ul data-role="listview">
        <% foreach (var c in calendar) { %>
            <li class="minihead">
                <%=c.date.ToString("ddd, yyyy MMM dd", R.GetCultureInfo(lang))%>
            </li>

            <li data-icon="false">
                <%
                    var moodsrc = "";
                    if (c.mood == HW.MobileApp.HWService.Mood.DontKnow)
                        moodsrc = "images/dontKnow@2x.png";
                    else if (c.mood == HW.MobileApp.HWService.Mood.Unhappy)
                        moodsrc = "images/unhappy@2x.png";
                    else if (c.mood == HW.MobileApp.HWService.Mood.Neutral)
                        moodsrc = "images/neutral@2x.png";
                    else if (c.mood == HW.MobileApp.HWService.Mood.Happy)
                        moodsrc = "images/happy@2x.png";

                    moodsrc = "src='" + moodsrc + "'";
                    var linksrc = "href='Diary.aspx?date=" + c.date.ToString("yyyy-MM-ddThh:mm:ss", R.GetCultureInfo(lang)) + "'";
                %>

                <a <%=linksrc %> rel="external">
                    <% if (c.mood != HW.MobileApp.HWService.Mood.NotSet) { %>
            
                        <img class="moodimg" <%=moodsrc %> />
            
                    <%} %>
                    <span><%= R.Str(lang,"diary.notes") %></span>
                    <p><br /><%=c.note%></p>
                    
                
                </a>
            </li>
            <% if (c.events != null) { %>
                <% foreach (var cevent in c.events) { %>  
                    <li >
                    
                        <% if (cevent.formInstanceKey != null){ %>
                            <% var filink = "href='Statistics.aspx?fik=" + cevent.formInstanceKey + "'"; %>
                            <a <%=filink %> rel="external" >
                        <% } %>       

                        <div class="time fade"><%=cevent.time.ToString("hh:mm")%></div>
                        <div class="events"><%=cevent.description%> <span> <%=cevent.result != null? " "+cevent.result.Replace(',','.'):"" %></span></div>
                    
                        <% if (cevent.formInstanceKey != null) { %>
                            </a>
                        <% } %>       
                    </li>

                <% } %>
            <% } %>

        <% } %>

        </ul>
    </div>
</asp:Content>
