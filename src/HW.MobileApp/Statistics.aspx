<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="HW.MobileApp.Statistics" %>
<%@ Import Namespace="HW.MobileApp" %>
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
    <% if (Request.QueryString["fromCal"]!=null&&Request.QueryString["fromCal"].Equals("true")) { %>
        <a href="Calendar.aspx" data-icon="arrow-l" rel="external"><%= R.Str(language, "button.back")%></a>
    <%}else { %>
        <a href="Dashboard.aspx" data-icon="arrow-l" rel="external"><%= R.Str(language, "home.myHealth")%></a>
    <%} %>
    <h1><%= R.Str(language, "statistics.title")%></h1>
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
            <label style="margin-left:-12px;"><%= R.Str(language, "statistics.result")%>: <%=timeframe %></label>
            <label style="text-align:right;position:absolute;right:2%;"><%= R.Str(language, "statistics.compare")%>: <%=compare %></label>
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
            <label runat="server"><%= R.Str(language, "statistics.result")%>: <%=formInstance.dateTime %></label>
            <label style="text-align:right;position:absolute;right:2%;"><%= R.Str(language, "statistics.compare")%>: </label>
        </li>
        <li>

            <div class="ui-grid-c" style="width:140%;">  
                                <div class="ui-block-a statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="images/green.png"></div><div ><%= R.Str(language, "statistics.healthy") %></div> </div>
                                
                                <div class="ui-block-b statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="images/orange.png"></div> <div><%= R.Str(language, "statistics.improvement") %></div></div>
                                
                                <div class="ui-block-c statlegend" >
                                <div style="float:left;margin-right:5px;">
                                <img class="statimg"
                                src="images/pink.png"></div><div><%= R.Str(language, "statistics.unhealthy") %></div></div>
            </div>
        </li>
        
        <%
        foreach (var f in fifeedback)
        {
            if (f.feedbackTemplateID != 0 && f.value != "0" && f.value != null)
            {
        %> 
        <li data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-d" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" style="padding:0px 0px 0px 0px;">
                    
        <% 
        var img = "";
        var progress = "";
        if (f.rating.ToString() == "Warning")
        {
            img = "<img style='height:20px;' src='images/orange.png'>";
            progress = "class='orange' style='width:" + f.value + "%;'";
        }
        else if (f.rating.ToString() == "Healthy")
        {
            img = "<img style='height:20px;' src='images/green.png'>";
            progress = "class='green' style='width:" + f.value + "%;'";
        }
        else
        {
            img = "<img style='height:20px;' src='images/pink.png'>";
            progress = "class='pink' style='width:" + f.value + "%;'";
        }
                
            
        %>
                    <h1 style="margin:0px 0px 0px 0px;"><%=img%> <%= f.header%>
                        <div id="progressbar">
                            <div <%=progress %>></div>
                        </div>
                    </h1>
                    <%
        var greenh = 0;
        int.TryParse(f.greenHigh + "",out greenh);
        var yellowh = 0;
        int.TryParse(f.yellowHigh + "", out yellowh);
        var greenl = 0;
        int.TryParse(f.greenLow + "", out greenl);
        var yellowl = 0;
        int.TryParse(f.yellowLow + "", out yellowl);

        var greenStat = "style='left:" + (greenl) + "%;width:" + ((greenh-1)-greenl )+ "%;'";
        var orangeStat = "style='left:" + (yellowl) + "%;width:" +( (yellowh-1)-yellowl )+ "%;'";
                    
                    %>
                    <div data-role="content">
                        <div style = "position:relative;margin-right:24px;">
                        <div class="holder lower pink"></div>
                        <div class="holder upper orange" <%=orangeStat%>></div>
                        <div class="holder upper green" <%=greenStat %>></div>
                        </div>
                        <h3 style="color:#1987D1"><%= R.Str(language, "statistics.interpretation")%></h3>

                        <span style="font-size:small;"><%=f.analysis%></span>
                        <%
        var rating = "";
        if (f.rating.ToString() == "Warning")
            rating = R.Str(language, "statistics.improvement");
        else if (f.rating.ToString() == "Healthy")
            rating = R.Str(language, "statistics.healthy");
        else if (f.rating.ToString() == "Unhealthy")
            rating = R.Str(language, "statistics.unhealthy");                
        
                        %>
                        <h3 style="color:#1987D1"><%=rating%></h3>
                        <span style="font-size:small;"><%= f.feedback%></span>

                        <h3 style="color:#1987D1"><%= R.Str(language, "statistics.action")%></h3>
                        <span style="font-size:small;"><%=replaceExerciseTags(f.actionPlan)%></span>
                        
                    </div>

                    
            </li>
           

        <%  }
        }
      }%>

      
    </ul>
</div>

</asp:Content>
