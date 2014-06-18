<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="HW.MobileApp.Calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
      .minihead
    {
        font-size:x-small;
        background-color:#494A48;
        color:White;
        height:10px;
        
    }
    .moodimg{width:30px;margin:14px 0px 0px 27px;padding: 0px 0px 0px 0px;}
    .notes{height:60px;}
    .events{ padding: 0px 0px 0px 0px; margin:0px 0px 0px 31px;display:inline;}
    .time{ display:inline;margin-left:15px; font-size:small; }
    .fade{ color:#7A7A7A;}
   
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Diary.aspx" data-icon="arrow-l">My Health</a>
    <h1>Calendar</h1>


</div>
<div data-role="content" >
    <ul data-role="listview">
    <%
    foreach (var c in calendar) { 
    %>
        <li class="minihead">
            <%=c.date.ToString("ddd, yyyy MMM dd")%>
        </li>
        <li data-icon="false" class="notes">
            <%
                var moodsrc = "";
                if (c.mood == HW.MobileApp.HWService.Mood.DontKnow)
                    moodsrc = "http://clients.easyapp.se/healthwatch/images/dontKnow@2x.png";
                else if (c.mood == HW.MobileApp.HWService.Mood.Unhappy)
                    moodsrc = "http://clients.easyapp.se/healthwatch/images/unhappy@2x.png";
                else if (c.mood == HW.MobileApp.HWService.Mood.Neutral)
                    moodsrc = "http://clients.easyapp.se/healthwatch/images/neutral@2x.png";
                else if (c.mood == HW.MobileApp.HWService.Mood.Happy)
                    moodsrc = "http://clients.easyapp.se/healthwatch/images/happy@2x.png";

                moodsrc = "src='" + moodsrc + "'";
                var linksrc = "href='Diary.aspx?date=" + c.date.ToString("yyyy-MM-ddThh:mm:ss") + "'";
            %>

            <a <%=linksrc %>>
            <%if (c.mood != HW.MobileApp.HWService.Mood.NotSet) { %>
            
                <img class="moodimg" <%=moodsrc %> />
            
            <%} %>
                <span>Notes</span>
                <p><%=c.note%></p>
                
            </a>
        </li>
        <%
            if (c.events != null)
            {
                foreach (var cevent in c.events)
                {
        %>
                <li >
                
                    <div class="time fade"><%=cevent.time.ToString("hh:mm")%></div>
                    <div class="events"><%=cevent.description%></div>
                
                </li>

        <% } } %>
        
        
    
    
    <% } %>

    </ul>
</div>
</asp:Content>
