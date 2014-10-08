<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="HW.MobileApp.Form" %>
<%@ Import Namespace="HW.MobileApp" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--<link rel="stylesheet" href="jquery.mobile-1.2.1.min.css" />-->
    <link rel="stylesheet" href="https://code.jquery.com/mobile/1.2.1/jquery.mobile-1.2.1.min.css" />
    <link rel="stylesheet" href="/custom.css" />
    <link rel="Stylesheet" href="css/custom.css" />
	
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/jquery.mobile-1.2.1.min.js"></script>
    
  
</head>
<body>
    
    <form id="form1" runat="server">
    
        <div data-role="page" id="form">

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

            window.onload=function(){
                var x = <%= this.questNo %>;
                for(var i = 0;i<parseInt(x);i++)
                {
                    $("#myslider"+i).next().find('.ui-slider-handle').hide();
                    $("#myslider"+i+"button").closest('.ui-btn').hide();

                    $("#myslider"+i).change(function() {
                    var slider_value = $(this).slider().val();
                    if(slider_value > 0){
                        $(this).slider().next().find('.ui-slider-handle').show();
                        $("#"+$(this).attr("id")+"button").closest('.ui-btn').show();
                    }
                    });

                    $("#myslider"+i+"button").click(function(){
                        $(this).closest('.ui-btn').hide();
                        var slideid = $(this).attr("id").replace('button','');
                        $("#"+slideid).slider().val("0");
                        $("#"+slideid).slider().next().find('.ui-slider-handle').hide();
                    });  
                }
            };

           
            
            </script>   
            
            <div data-role="header" data-theme="b" data-position="fixed">
                <a href="Dashboard.aspx" data-icon="arrow-l"><%= R.Str(lang,"home.myHealth") %></a>
                <h1><%= R.Str(lang, "dashboard.form")%></h1>
                
                <a onClick="getSliderValue()"><%= R.Str(lang, "button.save")%></a>
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
                    var buttonIdName = "id='myslider" + i + "button'";
            %>      
                  <li>
                      
                      <label class="question"><%=questText %></label>
                      <div class="form_button">
                      <input type="button" <%=buttonIdName%> data-icon="delete" data-mini="true" data-iconpos="notext"/></div>
                      <div class="form_slider">
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
                      </div>               
                      
                  </li>

            <% } %>
                </ul>
           
            </div>
            </div>
            <div data-role="footer" dataid="footernav" data-position="fixed">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Dashboard.aspx" data-icon="health" rel="external"><%= R.Str(lang, "home.myHealth")%></a></li>
                        <li><a href="News.aspx" data-icon="news" rel="external"><%= R.Str(lang, "home.news")%></a></li>
                        <li><a href="More.aspx" data-icon="more" rel="external"><%= R.Str(lang, "home.more")%></a></li>
                    </ul>
                </div>
            </div>
            <a id="saveBtn" onServerClick="saveBtnClick" runat="server" class="hide"></a>
        </div>
        
        
    </form>
</body>
</html>
