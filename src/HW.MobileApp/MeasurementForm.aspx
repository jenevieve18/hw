<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="MeasurementForm.aspx.cs" Inherits="HW.MobileApp.MeasurementForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function getFormValues() { 
        var ids = "";
        var idvals = "";
        var x = <%=measureNo %>;
          
         for(var i = 0;i<parseInt(x);i++)
        {
            ids+= $("#mcid"+i).val()+"x";
            idvals+= $("#mcidvalue"+i).val()+"x";
        }
        
        $('#<%=mcids.ClientID%>').val(ids);
        $('#<%=mcidvals.ClientID%>').val(idvals);
        $('#<%=inhours.ClientID%>').val( $("#timeHr").val());
        $('#<%=inmins.ClientID%>').val( $("#timeMin").val());

        document.getElementById('<%=saveBtn.ClientID%>').click();
    }
</script>

<div data-role="header" data-theme="b" data-position="fixed">
    
    <a <%="href='MeasurementsList.aspx?datetime="+date.ToString("yyyy-MM-ddTHH:mm:ss")+"'" %> data-icon="arrow-l" >Back</a>
    <h1></h1>
    <a onClick="getFormValues()" data-icon="check">Save</a>
</div>
<asp:HiddenField ID="mcids" runat="server" />
<asp:HiddenField ID="mcidvals" runat="server" />
<asp:HiddenField ID="inhours" runat="server" />
<asp:HiddenField ID="inmins" runat="server" />

 <div class="noleftpadding" data-role="content">
        <h3 class="padd exheader" ><%=measure[0].measureCategory %></h3>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">

        <div data-role="fieldcontain"  class="contentsmaller">

            <div style="margin:5px;text-align:center;">
            Time
           <input type="number" min="1" max="12" data-mini="true" id="timeHr" <%="value='"+date.ToString("HH")+"'" %> style="width:50px;" >
          :<input type="number" min="0" max="60" data-mini="true" id="timeMin" <%="value='"+date.ToString("mm")+"'" %> style="width:50px;" >
            </div>

        <%for (var m = 0; m < measureNo;m++ )
          { %>

        <%foreach (var mc in measure[m].measureComponents)
          {%>
            <div style="margin:5px;text-align:center;">
            <%=mc.measureComponent%>
            <input type="hidden" <%="id='mcid"+m+"' value='"+mc.measureComponentID+"'" %> />
            <input data-mini="true" type="number" <%="id='mcidvalue"+m+"'" %> style="width:80px;">
            <%=mc.unit%>
            </div>
        <%} %>
        <%} %>   
            
        </div> 
        <a id="saveBtn" onServerClick="saveBtnClick" runat="server" style="display:none;"></a>
    </div>
</asp:Content>
