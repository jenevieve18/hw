<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="HW.MobileApp.Form" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <script src="http://code.jquery.com/jquery-1.8.3.min.js"></script>
    <script src="http://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.js"></script>

   
    

    <script type="text/javascript">
        
        function getSliderValue() 
        {
            var sliderVal = "";
            var x = <%= this.questNo %>;
            
            for(var i = 0;i<parseInt(x);i++)
            {
                sliderVal+= $("#myslider"+i).val()+"x";
            }
            
            $('#answers').val(sliderVal);
            document.getElementById('saveBtn').click();
        }
    </script>

    
  
</head>
<body>

<style type=text/css>
        
    input.ui-slider-input {
        display : none ;
    }
    
    div.ui-slider{width:95%;}
    
    .ui-slider-track {
    height: 10px;
    }
    
    .ui-slider-handle {
        background: rgb(0,140,220);
    }

    div.ui-grid-d{
        text-align:center;
        font-size:x-small;
        width:97%;
    }
    
    div.ui-block-a
    {   text-align:left;  }
    div.ui-block-e
    {   text-align:right;  }
    
    
    </style>

    <form id="form1" runat="server">
    
        <div data-role="page">
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="Dashboard.aspx" data-icon="arrow-l">My Health</a>
                <h1>Form</h1>
                
                <a onClick="getSliderValue()" data-icon="check">Save</a>
            </div>
            <div data-role="content">
            <div class="fieldcontain">
                <ul data-role="listview">

                <asp:HiddenField ID="answers" runat="server" />
                
            <%
                
                for (var i = 0; i < questNo ; i++)
                { 
                    var questText = question[i].QuestionText;
                    var option = question[i].AnswerOptions;
                    var optionNo = question[i].AnswerOptions.Count();
                    var idName = "id='myslider"+ i + "'";
            %>      
                  <li>
                      <label style="font-size:small;font-weight:700;"><%=questText %></label>
                      <input type="range" name="myslider" <%=idName%> value="0" min="1" max="100" data-mini="true" />  

                      <%
                            
                         if (optionNo == 2){ 
                      %>
                            <div class="ui-grid-d">  
                                <div class="ui-block-a"><%=option[0].AnswerText %></div>
                                <div class="ui-block-b"></div>
                                <div class="ui-block-c"></div>
                                <div class="ui-block-d"></div>  
                                <div class="ui-block-e"><%=option[1].AnswerText %></div>
                            </div>
                      <% }
                         else if (optionNo == 5){
                      %>
                            <div class="ui-grid-d">  
                                 <div class="ui-block-a"><%=option[0].AnswerText %></div>
                                 <div class="ui-block-b"><%=option[1].AnswerText %></div>
                                 <div class="ui-block-c"><%=option[2].AnswerText %></div>
                                 <div class="ui-block-d"><%=option[3].AnswerText %></div>  
                                 <div class="ui-block-e"><%=option[4].AnswerText %></div>
                            </div>                        
                      <% } %>
                  </li>

            <% } %>
                </ul>
           
            </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Login.aspx" data-icon="home">My Health</a></li>
                        <li><a href="News.aspx" data-icon="grid">News</a></li>
                        <li><a href="More.aspx" data-icon="info">More</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <a id="saveBtn" onServerClick="saveBtnClick" runat="server" style="display:none;"></a>
    </form>
</body>
</html>
