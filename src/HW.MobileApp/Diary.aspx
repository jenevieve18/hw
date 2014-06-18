<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="HW.MobileApp.Diary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!--

*date greater than present can still be selected
*previous date notes and moods is not set automatically
*no measurement and activities page

-->
  <style type="text/css">
    .minihead
    {
        font-size:x-small;
        background-color:#494A48;
        color:White;
        height:10px;
        
    }
    
    
    
    .mood1 {    background-image:url('http://clients.easyapp.se/healthwatch/images/dontKnow@2x.png');   }
    .mood2 {    background-image:url('http://clients.easyapp.se/healthwatch/images/unhappy@2x.png');    }
    .mood3 {    background-image:url('http://clients.easyapp.se/healthwatch/images/neutral@2x.png');    }
    .mood4 {    background-image:url('http://clients.easyapp.se/healthwatch/images/happy@2x.png');      }
    .image{ 
        background-repeat: no-repeat;
        background-position:center center;
        background-size:30px;
        } 

     #mood{border-width:0px;margin:0px 0px 0px 0px;width:100%;}

    table{
        border-collapse:collapse;
        
        height:90px;
        width:100%;
        margin:0px 0px 0px 0px;}
    td{
        text-align:center;
        vertical-align:bottom;
        margin-top:60%;
        border-right-style: solid;
        border-left-style: solid;
        border-width:1px;
        font-size:small;
        width:25%;
        }

        td.select { 
            background-color:#A3A2A6;

        }
</style>
<script type="text/javascript">
    function getMoodValue() {
        var selected = "";

        if (document.getElementById('dontknow').checked)
            selected = "DontKnow";
        else if (document.getElementById('unhappy').checked)
            selected = "Unhappy";
        else if (document.getElementById('neutral').checked)
            selected = "Neutral";
        else if (document.getElementById('happy').checked)
            selected = "Happy";
        else
            selected = "NotSet";

        var hd = document.getElementById('<%=moody.ClientID %>');
        hd.value = selected;
        hd = document.getElementById('<%=dateset.ClientID %>');
        hd.value = document.getElementById('date').value;

        document.getElementById('<%=saveBtn.ClientID %>').click();

    }

    function default_mood() {
        document.getElementById('m4').className = 'mood4 image';
        document.getElementById('m2').className = 'mood2 image';
        document.getElementById('m3').className = 'mood3 image';
        document.getElementById('m1').className = 'mood1 image';
     }

    function mood1_onclick() {
        document.getElementById('dontknow').checked = 'true';
        default_mood();
        document.getElementById('m1').className = 'mood1 image select';
    }

    function mood2_onclick() {
        document.getElementById('unhappy').checked = 'true';
        default_mood();
        document.getElementById('m2').className = 'mood2 image select';
    }

    function mood3_onclick() {
        document.getElementById('neutral').checked = 'true';
        default_mood();
        document.getElementById('m3').className = 'mood3 image select';
    }

    function mood4_onclick() {
        document.getElementById('unhappy').checked = 'true';
        default_mood();
        document.getElementById('m4').className = 'mood4 image select';
    }

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Dashboard.aspx" data-icon="arrow-l">My Health</a>
    <h1>Diary</h1>
    <a href="Calendar.aspx" data-icon="grid" data-iconpos="notext" style="position:inherit;right:87px;top:8px;">Delete</a>
    <a onClick="getMoodValue()" data-icon="check" class="ui-btn-right">Save</a>
</div>

<%
    var set = DateTime.Now;
    if (Request.QueryString["date"] != null) {
        set = setdate;
       }
    var date = "value='"+set.ToString("yyyy-MM-dd")+"'";
                
%>

<div data-role="content" >
    
    <ul data-role="listview">
    <a id="saveBtn" onServerClick="saveBtnClick" runat="server" style="display:none;"></a>
    <asp:HiddenField ID="moody" runat="server" />
    <asp:HiddenField ID="dateset" runat="server" />

        <li class="minihead">Date</li>
         <li style="padding:0px 0px 0px 0px;border-width:0px;">
          
            <input type="date" name="date" id="date" <%=date %> style="margin:0px 0px 0px 0px;"/>

         </li>
         <li class="minihead">Notes</li>
         <li style="padding:0px 0px 0px 0px;"><asp:TextBox id="diarynote" placeholder="Write here.." 
                    style="border-width:0px;margin:0px 0px 0px 0px;" TextMode="multiline" runat="server"></asp:TextBox></li>
         <li class="minihead">Mood</li>
         <li style="padding:0px 0px 0px 0px;">
            
                <table >
                    <tbody>
                        <tr>
                            <td class="mood1 image" id= "m1" 
                            onclick="mood1_onclick()" >
                            Don't Know
                            <input type="radio" id="dontknow" name="mood" value="0" style="display:none;"/></td>

                            <td class="mood2 image" id= "m2" 
                            onclick="mood2_onclick()" >
                            Unhappy
                            <input type="radio" id="unhappy" name="mood" value="1" style="display:none;"/></td>

                            <td class="mood3 image" id= "m3" 
                            onclick="mood3_onclick()" >
                            Neutral
                            <input type="radio" id="neutral" name="mood" value="2" style="display:none;"/></td>

                            <td class="mood4 image" id= "m4"
                            onclick="mood4_onclick()" >
                            Happy
                            <input type="radio" id="happy" name="mood" value="3" style="display:none;"/></td>
                            
                        </tr>
                    </tbody>
                </table>
            
         </li>
         <li class="minihead">Activities & Measurements</li>
         <li><a href="dashboard.aspx">View Activities & Measurements</a>
         
         </li>
        
    </ul>
    
</div>

</asp:Content>
