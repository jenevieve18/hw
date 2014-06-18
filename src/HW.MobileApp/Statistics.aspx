<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HW.MobileApp.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- 

*no view page

-->

<style>
    
   #progressbar {
    
    border-radius: 10px; /* (height of inner div) / 2 + padding */
    border:1px solid #D0D0D0;
    
    height:8px;
    }

    #progressbar > div {
    height: 8px;
    border-radius: 2px;
    }
    
    
    
     
    
    
    .green {background-image:url(http://clients.easyapp.se/healthwatch//images/green.png);background-position:center;}
    .pink  {background-image:url(http://clients.easyapp.se/healthwatch//images/pink.png);background-position:center;}
    .orange{background-image:url(http://clients.easyapp.se/healthwatch//images/orange.png);background-position:center;}
    
    .holder
    {
        position:absolute;       
        height: 8px; 
    }
    
    .upper
    {
        border-right:2px solid #ffffff;
        border-left:2px solid #ffffff;
        
        
     } 
    
    .lower
    {
        width:96%;
        height: 8px;  
        
    }
    
    .statlink
    {
        background-image: url('http://clients.easyapp.se/healthwatch//images/detail_desk.gif');
        background-repeat: no-repeat;
        background-position: 0 50%;
        text-decoration: none;
        margin: -8px -5px -8px 3px;
        padding: 8px 5px 8px 20px;
    }
    
    
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l">My Health</a>
    <h1>Statistics</h1>
    <a href="#" data-icon="check">View</a>
</div>
<div data-role="content">
    <ul data-role="listview">
        <li style="font-size:x-small;background-color:black;color:White;padding-top:1px;padding-bottom:1px;">
            <label runat="server">Result: <%=formInstance.dateTime %></label>
            <label style="text-align:right;position:absolute;right:2%;">Compare with: </label>
        </li>
        <li>

            <div class="ui-grid-c" style="width:140%;">  
                                <div class="ui-block-a" style="font-size:small;">
                                <img style="height:20px;"
                                src="http://clients.easyapp.se/healthwatch//images/green.png">&nbsp;Healthy level</div> 
                                
                                <div class="ui-block-b" style="font-size:small;">
                                <img style="height:20px;"  
                                src="http://clients.easyapp.se/healthwatch//images/orange.png"> &nbsp;Improvement needed</div>
                                
                                <div class="ui-block-c" style="font-size:small;">
                                <img style="height:20px;" 
                                src="http://clients.easyapp.se/healthwatch//images/pink.png">&nbsp;Unhealthy level</div>
            </div>
        </li>
        
        <%
            foreach (var f in fifeedback){
                if (f.feedbackTemplateID != 0){
        %> 
        <li data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-d" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" style="padding:0px 0px 0px 0px;">
                    
        <% 
            var img = "";
            var progress = "";
            if (f.rating.ToString() == "Warning")
            {
                img = "<img style='height:20px;' src='http://clients.easyapp.se/healthwatch//images/orange.png'>";
                progress = "class='orange' style='width:"+f.value+"%;'";
            }
            else if (f.rating.ToString() == "Healthy")
            {
                img = "<img style='height:20px;' src='http://clients.easyapp.se/healthwatch//images/green.png'>";
                progress = "class='green' style='width:" + f.value + "%;'";
            }
            else
            {
                img = "<img style='height:20px;' src='http://clients.easyapp.se/healthwatch//images/pink.png'>";
                progress = "class='pink' style='width:" + f.value + "%;'";
            }
                
            
        %>
                    <h1 style="margin:0px 0px 0px 0px;"><%=img %> <%= f.header%>
                        <div id="progressbar">
                            <div <%=progress %>></div>
                        </div>
                    </h1>
                    <%
                        var greenh = int.Parse(f.greenHigh+"") ;
                        var yellowh = int.Parse(f.yellowHigh + "");
                        var greenl = int.Parse(f.greenLow + "");
                        var yellowl = int.Parse(f.yellowLow + "");
                        
                        var greenStat = "style='margin-left:" + greenl + "%;width:"+((greenh-greenl)-5)+"%;'";
                        var orangeStat = "style='margin-left:" + yellowl + "%;width:" + ((yellowh-yellowl)-5) + "%;'";
                    
                    %>
                    <div data-role="content">
                        <div >
                        <div class="holder lower pink"></div>
                        <div class="holder upper orange" <%=orangeStat%>></div>
                        <div class="holder upper green" <%=greenStat %>></div>
                        </div>
                        <h3 style="color:#1987D1">Interpretation</h3>
                        <span style="font-size:small;"><%=f.analysis %></span>
                        <%
                            var rating = "";
                            if (f.rating.ToString() == "Warning")
                                rating = "Improvement Needed";
                            else rating = f.rating.ToString() + " Level";
                        %>
                        <h3 style="color:#1987D1"><%=rating%></h3>
                        <span style="font-size:small;"><%= f.feedback%></span>

                        <h3 style="color:#1987D1">Action Plan</h3>
                        <span style="font-size:small;"><%=replaceExerciseTags( f.actionPlan)%></span>
                        
                    </div>

                    
            </li>
           

        <%  }
            }%>

      
    </ul>
</div>

</asp:Content>
