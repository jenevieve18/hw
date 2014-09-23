<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="HW.MobileApp.Diary" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!--

*date greater than present can still be selected
*previous date notes and moods is not set automatically
*querystring is prioritized over textbox input

-->

<style type="text/css">
        .ui-controlgroup-controls{  width:101% !important; margin:-5px 0px; }
        .ui-select {    width:33.33% !important;   }
        .ui-radio {     width:25% !important;  }
        .ui-radio .ui-btn { height:100px !important; }
        .ui-radio .ui-btn-text { font-size:11px;}
        .ui-radio .ui-btn-inner{  height:100px !important; text-overflow: initial; padding:4px 0px 0px 0px;}
        textarea { height:130px !important; }
     
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div data-role="header" data-theme="b" data-position="fixed">

        <a href="Dashboard.aspx" data-icon="arrow-l"><%= R.Str(lang, "home.myHealth")%></a>
        <h1><%= R.Str(lang, "dashboard.diary")%></h1>
        <a href="Calendar.aspx?b=Diary" data-icon="bars" data-iconpos="notext" style="position:inherit;right:87px;top:8px;"></a>
        <a  runat="server" onserverclick="saveBtnClick" class="ui-btn-right"><%= R.Str(lang, "button.save")%></a>
    </div>


    <div data-role="content" >
    
        <ul data-role="listview">
            <li class="minihead"><%= R.Str("diary.date") %></li>
            <li style="padding:0px 0px 0px 0px;border-width:0px;">
                <fieldset data-role="controlgroup" data-type="horizontal" data-mini="true" >
                    <div style="display:none;"><asp:DropDownList runat="server"></asp:DropDownList></div>
                    <asp:DropDownList ID="dropDownListDateMonth" runat="server" 
                            onselectedindexchanged="dropDownListDate_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Jan" Value="01"></asp:ListItem>
                    <asp:ListItem Text="Feb" Value="02"></asp:ListItem>
                    <asp:ListItem Text="Mar" Value="03"></asp:ListItem>
                    <asp:ListItem Text="Apr" Value="04"></asp:ListItem>
                    <asp:ListItem Text="May" Value="05"></asp:ListItem>
                    <asp:ListItem Text="Jun" Value="06"></asp:ListItem>
                    <asp:ListItem Text="Jul" Value="07"></asp:ListItem>
                    <asp:ListItem Text="Aug" Value="08"></asp:ListItem>
                    <asp:ListItem Text="Sep" Value="09"></asp:ListItem>
                    <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                    <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                    <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:DropDownList ID="dropDownListDateDay" runat="server"   onselectedindexchanged="dropDownListDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="dropDownListDateYear" runat="server"  onselectedindexchanged="dropDownListDate_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <div style="display:none;"><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList></div>
                </fieldset>
    
            </li>
            <li class="minihead"><%= R.Str("diary.notes") %></li>
            <li style="padding:0px 0px 0px 0px;">
                <asp:TextBox id="textBoxNote" placeholder="Write here.." 
                        style="border-width:0px;margin:0px 0px 0px 0px;" TextMode="multiline" runat="server"></asp:TextBox>
            </li>
            <li class="minihead"><%= R.Str("diary.mood") %></li>
            <li style="padding:0px 0px 0px 0px;">
                <fieldset data-role="controlgroup" data-type="horizontal" data-mini="true">
                    <div style="display:none;"><asp:RadioButton text="x" runat="server"></asp:RadioButton></div>
                    <asp:RadioButton runat="server" id="rdbDontKnow" GroupName="rdbMoods" text="Don't Know<div><img class='image' src='http://clients.easyapp.se/healthwatch/images/dontKnow@2x.png'></div>" value="DontKnow" />
                    <asp:RadioButton runat="server" id="rdbUnhappy"  GroupName="rdbMoods" text="Unhappy<div><img class='image' src='http://clients.easyapp.se/healthwatch/images/unhappy@2x.png'></div>" value="Unhappy"  />
                    <asp:RadioButton runat="server" id="rdbNeutral"  GroupName="rdbMoods" text="Neutral<div><img class='image' src='http://clients.easyapp.se/healthwatch/images/neutral@2x.png'></div>" value="Neutral" />
                    <asp:RadioButton runat="server" id="rdbHappy"    GroupName="rdbMoods" text="Happy<div><img class='image' src='http://clients.easyapp.se/healthwatch/images/happy@2x.png'></div>" value="Happy" />
                    <div style="display:none;"><asp:RadioButton ID="RadioButton1" text="x" runat="server"></asp:RadioButton></div>
                </fieldset>

            </li>
            <li class="minihead"><%= R.Str(lang, "measurement.text")%></li>
        
            <li><asp:LinkButton runat="server" onclick="activitylink_Click"><%= R.Str(lang, "measurement.viewAdd")%></asp:LinkButton>
         
            </li>
        
        </ul>
    
    </div>

</asp:Content>
