<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="MeasurementForm.aspx.cs" Inherits="HW.MobileApp.MeasurementForm" %>
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
    
    <a <%="href='MeasurementsList.aspx?datetime="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'" %> data-icon="arrow-l" >Back</a>
    <h1></h1>
    <a id="saveBtn" onServerClick="saveBtnClick" runat="server" data-icon="check">Save</a>
</div>
 <div data-role="content">
    <div class="header">
        <h3 class="padd exheader" ><%=measure[0].measureCategory %></h3>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>

    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
    <legend><asp:Label ID="Label11" runat="server" AssociatedControlID="timeHour">Time</asp:Label></legend>
        <asp:DropDownList ID="timeHour" runat="server" >
        </asp:DropDownList>
        <asp:DropDownList ID="timeMin" runat="server">
        </asp:DropDownList>
    </fieldset>
    </div>
    

    <asp:PlaceHolder runat="server" ID="placeHolderList"></asp:PlaceHolder>
    
</div>
    <style type="text/css">
        input { max-width:500px !important; min-width:200px !important}
        .ui-controlgroup-controls{  max-width:500px !important;  }
        .ui-select {    width:50% !important;   }
        .header { text-align:center; }
        .header h4 { margin-bottom:0 }
        .header img { width:235px }
    </style>
</asp:Content>
