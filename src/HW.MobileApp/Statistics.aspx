<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HW.MobileApp.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- 

*no view page

-->


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l">My Health</a>
    <h1>Statistics</h1>
    <a href="#" data-icon="check">View</a>
</div>
<div data-role="content">
    <ul data-role="listview">
        <li class="minihead">
            <label runat="server">Result: <%=formInstance.dateTime %></label>
            <label style="text-align:right;position:absolute;right:2%;">Compare with: </label>
        </li>
        <li>

            <div class="ui-grid-c" style="width:140%;">  
                                <div class="ui-block-a statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="http://clients.easyapp.se/healthwatch//images/green.png"></div><div >Healthy level</div> </div>
                                
                                <div class="ui-block-b statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="http://clients.easyapp.se/healthwatch//images/orange.png"></div> <div>Improvement needed</div></div>
                                
                                <div class="ui-block-c statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="http://clients.easyapp.se/healthwatch//images/pink.png"></div><div>Unhealthy level</div></div>
            </div>
        </li>
        
        <%
            foreach (var f in fifeedback){
                if (f.feedbackTemplateID != 0 && f.value != "0"){
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
                        
                        var greenStat = "style='left:" + (greenl -1) + "%;width:"+((greenh-greenl)-5)+"%;'";
                        var orangeStat = "style='left:" + (yellowl -1)+ "%;width:" + ((yellowh-yellowl)-5) + "%;'";
                    
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
