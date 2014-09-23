<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="MeasurementForm.aspx.cs" Inherits="HW.MobileApp.MeasurementForm" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
   
   var tb8 = 0;
   $(document).ready(function(){
   <% 
   for(var i = 0; i < componentcount;i++)
   {
      if(measureTextBox[i].ID == "5")
      {
   %>
          $('#<%=measureTextBox[i].ClientID %>').keyup(function(){

            var x = $('#<%=measureTextBox[i].ClientID %>').val();
            if(x != ""){
                
            
            <% 
            for(var y = 0; y < componentcount;y++)
            {
                if(measureTextBox[y].ID == "8")
                {%>
                    tb8 = Math.pow(parseFloat(x) / 100.0,2);
                    $('#<%=measureTextBox[y].ClientID %>').val(tb8);
                 <% 
                }
            }
            %>
            }
          });
   <%
      }
      if(measureTextBox[i].ID == "6")
      {
   %>
          $('#<%=measureTextBox[i].ClientID %>').keyup(function(){

            var x = $('#<%=measureTextBox[i].ClientID %>').val();
            if(x != ""){
                
            
            <% 
            for(var y = 0; y < componentcount;y++)
            {
                if(measureTextBox[y].ID == "7")
                {%>
                    $('#<%=measureTextBox[y].ClientID %>').val(x/tb8);
                 <% 
                }
            }
            %>
            }
          });
   <%
      }
   }
   %> 
   });

</script>

<div data-role="header" data-theme="b" data-position="fixed">
    
    <a <%="href='MeasurementsList.aspx?datetime="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'" %> data-icon="arrow-l" ><%= R.Str(lang,"button.back") %></a>
    <h1></h1>
    <a id="saveBtn" onServerClick="saveBtnClick" runat="server" data-icon="check"><%= R.Str(lang, "button.save")%></a>
</div>
 <div data-role="content" id="measurementform">
    

     <div class="header">
        <h3><asp:Label id="lblHeader" runat="server"></asp:Label></h3>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif"/>
        
    </div>

    
    
    <div class="ui-grid-b">
    <div class="ui-block-a">
    <asp:Label ID="lblTime" runat="server" Text="Time" AssociatedControlID="timeHour"></asp:Label>
    </div>
    <div class="ui-block-b">
    <div class="center">
    <fieldset data-role="controlgroup" data-type="horizontal">
        <asp:DropDownList ID="timeHour" runat="server" data-mini="true">
        </asp:DropDownList>
        <asp:DropDownList ID="timeMin" runat="server" data-mini="true">
        </asp:DropDownList>
    </fieldset>
    </div>
    </div>
    <div class="ui-block-c">
    </div>
    </div>
    

    <asp:PlaceHolder runat="server" ID="placeHolderList"></asp:PlaceHolder>
    
</div>

</asp:Content>
