<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs" Inherits="HW.MobileApp.ChangeProfile" %>
<%@ Import Namespace="HW.MobileApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        
       
    #profileheader .header { text-align:left;margin-top:40px;}
    #profileheader .header h3 { margin-bottom:0; margin-left:20px; }
    #profileheader .header img { width:235px; }
    
    #changeprofile .front_header {
        text-align: center;
        margin: 15px 0px 25px 0px;
        font-size: 16px;
    }
    #changeprofile .front_note {
        max-width: 560px;
        margin-left: auto;
        margin-right: auto;
        min-width: 268px;
        padding: 15px 15px 70px 15px;
    }
    #changeprofile .front_logo {
        width: 180px;
        height: 126px;
        margin-bottom: 30px;
        display: inline-block;
        vertical-align: bottom;
        margin-right: 20px;
    }
    #changeprofile .front_controls {
        max-width: 350px;
        margin: 0 auto;
        min-height: 250px;
        display: inline-block;
        min-width: 320px;
    }
    #changeprofile .center {
        text-align:center;
    }
    #changeprofile .front_header_img {
        width: 235px;
        margin-left:-15px;
    }
    
    
    .ui-grid-a label, .ui-grid-a a  {margin:13px 10px 0px 0px;font-size:12px;}  
    .ui-grid-a .ui-block-a{ width:24%; text-align:right; margin-right:1%;}
    .ui-grid-a .ui-block-b{ width:75%; }
    .ui-select .ui-btn-text { padding:0px !important;font-size:12px;font-weight:normal;}
            
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="header" id="profileheader" data-theme="b" data-position="fixed">
        <a href="Settings.aspx"><%= R.Str(language,"button.cancel") %></a>
        <h1><%= R.Str(language, "profile.title")%></h1>
        <a id="createBtn" onserverclick="saveChangesBtn_Click" runat="server"><%= R.Str(language, "button.save")%></a>

    </div>
    <div data-role="content" id="changeprofile" style="padding:5px;">
        <div style="left:20px;top:42px;position:absolute;font-size:11px;font-style:italic;color:#909090  ;">
            <p><%= R.Str(language, "label.required")%></p>
        </div>

        <div class="header">
            <h3><asp:Label id="lblHeader" runat="server" text="Account Details"></asp:Label></h3>
            <img class="front_header_img" src="images/divider.gif">
            <h3><asp:Label ID="labelMessage" runat="server"></asp:Label></h3>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblLanguage" runat="server" Text="Language" AssociatedControlID="dropDownListLanguage"></asp:Label>
            </div>
            <div class="ui-block-b">
                <fieldset data-role="controlgroup">
                <asp:DropDownList data-mini="true" ID="dropDownListLanguage"
                    runat="server" AutoPostBack="true" ViewStateMode="Enabled" 
                        EnableViewState="true" 
                        onselectedindexchanged="dropDownListLanguage_SelectedIndexChanged">
                    <asp:ListItem value="2">English</asp:ListItem>
                    <asp:ListItem value="1">Swedish</asp:ListItem>
                </asp:DropDownList></fieldset>
            </div>
        </div>


        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblUsername" runat="server" AssociatedControlID="textBoxUsername">Username<span class="req">*</span>
                        </asp:Label>
            </div>
            <div class="ui-block-b">
                <asp:TextBox data-mini="true" ID="textBoxUsername" runat="server"></asp:TextBox>
            </div>
        </div>

   
        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblPassword" runat="server" Text="New Password<span class='req'>*</span>" AssociatedControlID="textBoxPassword"></asp:Label>
            </div>
            <div class="ui-block-b">
                <asp:TextBox data-mini="true" ID="textBoxPassword" TextMode="Password" EnableViewState="true" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password<span class='req'>*</span>" AssociatedControlID="textBoxConfirmPassword"></asp:Label>
            </div>
            <div class="ui-block-b">
                <asp:TextBox data-mini="true" ID="textBoxConfirmPassword" TextMode="Password" EnableViewState="true" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="textBoxEmail">Email<span class="req">*</span></asp:Label>
            </div>
            <div class="ui-block-b">
                <asp:TextBox data-mini="true" ID="textBoxEmail" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblAltEmail" runat="server" Text="Alternate Email" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
            </div>
            <div class="ui-block-b">
                <asp:TextBox data-mini="true" ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
            </div>
        </div>


        <div class="header">
            <h3><asp:Label id="Label1" runat="server" text="Personal Information"></asp:Label></h3>
            <img class="front_header_img" src="images/divider.gif">
            <h3><asp:Label ID="label2" runat="server"></asp:Label></h3>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblBirth" runat="server" AssociatedControlID="birthYear"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
                <asp:DropDownList ID="birthYear" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="birthMonth" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="birthDay" runat="server" >
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>   

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblGender" runat="server" AssociatedControlID="rdbGenderMale"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:RadioButton ID="rdbGenderMale"  GroupName="rdbGender" Value="1" runat="server" />
                <asp:RadioButton ID="rdbGenderFemale" GroupName="rdbGender" Value="2" runat="server" />
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
            <asp:Label ID="lblStatus" runat="server" AssociatedControlID="rdbStatusMarried" ></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:RadioButton ID="rdbStatusMarried"  GroupName="rdbStatus" Text="Married" Value="369" runat="server" />
                <asp:RadioButton ID="rdbStatusSingle" GroupName="rdbStatus" Text="Single" Value="370" runat="server" />
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
            <asp:Label ID="lblOccupation" runat="server" AssociatedControlID="dropDownListOccupation"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup">
                <asp:DropDownList data-mini="true" ID="dropDownListOccupation" runat="server" 
                    AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" 
                    onselectedindexchanged="dropDownListOccupation_SelectedIndexChanged">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
            <asp:Label ID="lblOccupationType" runat="server" AssociatedControlID="rdbOccupationTypeFull"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:RadioButton ID="rdbOccupationTypeFull" GroupName="rdbOccupationType" Value="405" runat="server" />
                <asp:RadioButton ID="rdbOccupationTypePart" GroupName="rdbOccupationType" Value="406" runat="server" />
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a" id="divIndustry" style="display:none;" runat="server">
            <div class="ui-block-a">
                <asp:Label ID="LblIndustry" runat="server" AssociatedControlID="dropDownListIndustry"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup">
                <asp:DropDownList data-mini="true" ID="dropDownListIndustry" runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

    
        <div class="ui-grid-a" id="divJob" style="display:none;" runat="server">
            <div class="ui-block-a">
                <asp:Label ID="LblJob" runat="server" Text="Job"  AssociatedControlID="dropDownListJob"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup">
                <asp:DropDownList data-mini="true" ID="dropDownListJob" runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a" id="divManagerial" style="display:none;" runat="server">
            <div class="ui-block-a">
            <asp:Label ID="LblManage" runat="server" AssociatedControlID="rdbMangerialYes"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:RadioButton ID="rdbMangerialYes"  GroupName="rdbMangerial" Text="Yes" 
                    Value="412" runat="server" oncheckedchanged="rdbMangerialYes_CheckedChanged" AutoPostBack="true"/>
                <asp:RadioButton ID="rdbMangerialNo" GroupName="rdbMangerial" Text="No" 
                    Value="413" runat="server"/>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a" id="divSubordinates" style="display:none;" runat="server">
            <div class="ui-block-a">
                <asp:Label ID="lblSubordinates" runat="server" AssociatedControlID="dropDownListSubordinates"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:DropDownList data-mini="true" ID="dropDownListSubordinates"  runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a" id="divStudyArea" style="display:none;" runat="server">
            <div class="ui-block-a">
                <asp:Label ID="LblStudyArea" runat="server" Text="Study Area"  AssociatedControlID="dropDownListStudyArea"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:DropDownList data-mini="true" ID="dropDownListStudyArea"  runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

    

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblIncome" runat="server" AssociatedControlID="dropDownListAnnualIncome"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:DropDownList data-mini="true" ID="dropDownListAnnualIncome" runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblEducation" runat="server" AssociatedControlID="dropDownListEducation"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:DropDownList data-mini="true" ID="dropDownListEducation" runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <div class="ui-grid-a">
            <div class="ui-block-a">
                <asp:Label ID="lblCoffee" runat="server" AssociatedControlID="dropDownListCoffee"></asp:Label>
            </div>
            <div class="ui-block-b">
            <fieldset data-role="controlgroup" data-mini="true" >
                <asp:DropDownList data-mini="true" ID="dropDownListCoffee" runat="server">
                </asp:DropDownList>
            </fieldset>
            </div>
        </div>

        <hr style="width:80%;"/>

        <div class="ui-grid-a" style="text-align:center;">
            <asp:Label ID="lblTerms" runat="server"><a <%=policylink %>><%= R.Str(language, "user.terms") %></a></asp:Label>
            <asp:CheckBox ID="cbTerms" Text=" I accept" runat="server" data-mini="true"></asp:CheckBox >
        </div>

  
    
    </div>
</asp:Content>
