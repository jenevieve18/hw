﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HW.MobileApp.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- 

*no view page

-->
<style type="text/css">
    .ui-controlgroup-controls{ width:100% !important; margin: -4px 0px -4px 1px;}
    .ui-select {   max-width: 50% !important;    min-width: 50% !important;  }
    
    
    .chartimg{
        position:absolute;
        width:18px;
        margin: 8px 0px 0px 3px;
    }
    
  /* .ui-btn-text{
        font-size: 10px; 
    }*/
    .indexb{ z-index:7 !important; }
    .indexo{ z-index:7 !important;}
    .indexg{ z-index:7 !important;}
    .indexp{ z-index:7 !important;}
    
    .ui-select .ui-btn-text {
        font-size:10px;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l" rel="external">My Health</a>
    <h1>Statistics</h1>
    <%=viewlink %>
</div>
<div data-role="content" id="stat">
    <ul data-role="listview">

    <%if (Request.QueryString["tf"] != null && Request.QueryString["comp"] != null)
      { %>
        <li class="minihead">
            <label style="margin-left:-12px;">Select measurement(s) to view</label>
        </li>
        <li style = "margin:0px;padding:0px;">
        <fieldset data-role="controlgroup" data-type="horizontal" data-mini="true" >
        <div style="display:none;">
            <asp:DropDownList runat="server"  />
            
        </div>    
        <div>
            <div style="position:absolute; margin: 0px 0px 0px 0px;">
                <img class="chartimg indexb upper" src="images/blue_graph.png" />
            </div>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="rp0" 
                onselectedindexchanged="rp_SelectedIndexChanged" data-icon="false"></asp:DropDownList>
        </div>  
        <div>
            <div style="position:absolute; margin: 0px 0px 0px 50%;">
                <img class="chartimg indexo upper" src="images/orange_graph.png" />
            </div>
            
            <asp:DropDownList runat="server" AutoPostBack="true" ID="rp1" 
                onselectedindexchanged="rp_SelectedIndexChanged" data-icon="false"></asp:DropDownList>
        </div>              
        <div>
            <div style="position:absolute; margin: 32px 0px 0px 3px;">
                <img class="chartimg indexg" src="images/green_graph.png"  />
            </div>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="rp2"
                onselectedindexchanged="rp_SelectedIndexChanged" data-icon="false"></asp:DropDownList>
        </div>
        <div>
            <div style="position:absolute; margin: 32px 0px 0px 50%;">
                <img class="chartimg indexp" src="images/pink_graph.png"  />
            </div>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="rp3"
                onselectedindexchanged="rp_SelectedIndexChanged" data-icon="false"></asp:DropDownList>
        </div>
        <div style="display:none;">                
            
            <asp:DropDownList ID="DropDownList3" runat="server" style="display:none;" />
        </div>
        </fieldset>
        </li>
        <li class="minihead">
            <label style="margin-left:-12px;">Result:<%=timeframe %></label>
            <label style="text-align:right;position:absolute;right:2%;">Compare with: <%=compare %></label>
        </li>
        <li>
            <div>
                <img src="<%=chartlink %>" style="width:100%;height:200px;"/>
            </div>

        </li>
    <%}
      else
      { %>
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
        foreach (var f in fifeedback)
        {
            if (f.feedbackTemplateID != 0 && f.value != "0")
            {
        %> 
        <li data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-d" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" style="padding:0px 0px 0px 0px;">
                    
        <% 
        var img = "";
        var progress = "";
        if (f.rating.ToString() == "Warning")
        {
            img = "<img style='height:20px;' src='http://clients.easyapp.se/healthwatch//images/orange.png'>";
            progress = "class='orange' style='width:" + f.value + "%;'";
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
                    <h1 style="margin:0px 0px 0px 0px;"><%=img%> <%= f.header%>
                        <div id="progressbar">
                            <div <%=progress %>></div>
                        </div>
                    </h1>
                    <%
        var greenh = int.Parse(f.greenHigh + "");
        var yellowh = int.Parse(f.yellowHigh + "");
        var greenl = int.Parse(f.greenLow + "");
        var yellowl = int.Parse(f.yellowLow + "");

        var greenStat = "style='left:" + (greenl) + "%;width:" + ((greenh-1)-greenl )+ "%;'";
        var orangeStat = "style='left:" + (yellowl) + "%;width:" +( (yellowh-1)-yellowl )+ "%;'";
                    
                    %>
                    <div data-role="content">
                        <div style = "position:relative;margin-right:24px;">
                        <div class="holder lower pink"></div>
                        <div class="holder upper orange" <%=orangeStat%>></div>
                        <div class="holder upper green" <%=greenStat %>></div>
                        </div>
                        <h3 style="color:#1987D1">Interpretation</h3>

                        <span style="font-size:small;"><%=f.analysis%></span>
                        <%
        var rating = "";
        if (f.rating.ToString() == "Warning")
            rating = "Improvement Needed";
        else rating = f.rating.ToString() + " Level";
                        %>
                        <h3 style="color:#1987D1"><%=rating%></h3>
                        <span style="font-size:small;"><%= f.feedback%></span>

                        <h3 style="color:#1987D1">Action Plan</h3>
                        <span style="font-size:small;"><%=replaceExerciseTags(f.actionPlan)%></span>
                        
                    </div>

                    
            </li>
           

        <%  }
        }
      }%>

      
    </ul>
</div>

</asp:Content>
