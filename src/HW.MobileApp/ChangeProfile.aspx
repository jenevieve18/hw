<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs" Inherits="HW.MobileApp.ChangeProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="Settings.aspx" data-icon="arrow-l">Cancel</a>
    <h1>Change Profile</h1>
    <a href="#" data-icon="check">Save</a>
</div>
<div data-role="content">
    <div class="header">
        <h4>Account Details</h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label2" runat="server" Text="Language" AssociatedControlID="dropDownListLanguage"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListLanguage"
            runat="server">
            <asp:ListItem >English</asp:ListItem>
            <asp:ListItem >Swedish</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label1" runat="server" Text="Username" AssociatedControlID="textBoxUsername"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxUsername" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label3" runat="server" Text="Password" AssociatedControlID="textBoxPassword"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxPassword" TextMode="Password" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label4" runat="server" Text="Email" AssociatedControlID="textBoxEmail"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxEmail" runat="server"></asp:TextBox>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label5" runat="server" Text="Alternate Email" AssociatedControlID="textBoxAlternateEmail"></asp:Label>
        <asp:TextBox data-mini="true" ID="textBoxAlternateEmail" runat="server"></asp:TextBox>
    </div>
    
    <div class="header">
        <h4>Personal Information</h4>
        <img class="front_header_img" src="http://clients.easyapp.se/healthwatch/images/divider.gif">
    </div>
    <div data-role="fieldcontain">
    
    <fieldset data-role="controlgroup" data-mini="true" data-type="horizontal" >
    <legend><asp:Label ID="Label11" runat="server" Text="Birthdate" AssociatedControlID="birthYear"></asp:Label></legend>
        <asp:DropDownList ID="birthYear" runat="server" >
        </asp:DropDownList>
        <asp:DropDownList ID="birthMonth" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="birthDay" runat="server"  >
        </asp:DropDownList>
    </fieldset>
    </div>   

    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label12" runat="server" Text="Gender" AssociatedControlID="rdbGender"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbGender" runat="server" >
            <asp:ListItem Text="Male" Value="1" ></asp:ListItem>
            <asp:ListItem Text="Female" Value="2" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>
    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label13" runat="server" Text="Status" AssociatedControlID="rdbStatus"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbStatus" runat="server" >
            <asp:ListItem Text="Married" Value="369" ></asp:ListItem>
            <asp:ListItem Text="Single" Value="370" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>

    <div data-role="fieldcontain">
        <asp:Label ID="Label6" runat="server" Text="Occupation" AssociatedControlID="dropDownListOccupation"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListOccupation" runat="server" 
            onselectedindexchanged="dropDownListOccupation_SelectedIndexChanged"
            AutoPostBack="true" ViewStateMode="Enabled" EnableViewState="true" >
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="Label14" runat="server" Text="Occupation Type" AssociatedControlID="rdbOccupationType"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbOccupationType" runat="server" >
            <asp:ListItem Text="Full time" Value="405"  ></asp:ListItem>
            <asp:ListItem Text="Part time" Value="406" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>

    
       
    <div data-role="fieldcontain" id="divIndustry" style="display:none;" runat="server">
        <asp:Label ID="LblIndustry" runat="server" Text="Industry" AssociatedControlID="dropDownListIndustry"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListIndustry" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain" id="divJob" style="display:none;" runat="server">
        <asp:Label ID="LblJob" runat="server" Text="Job"  AssociatedControlID="dropDownListJob"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListJob" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain" id="divManagerial" style="display:none;" runat="server">
    <fieldset data-role="controlgroup" data-mini="true" >
    <legend><asp:Label ID="LblManage" runat="server" Text="Managerial post with subordinate staff?" AssociatedControlID="rdbManagerial"></asp:Label></legend>
        <asp:RadioButtonList ID="rdbManagerial" runat="server"  
            ViewStateMode="Enabled" >
            <asp:ListItem Text="Yes" Value="413" ></asp:ListItem>
            <asp:ListItem Text="No" Value="412" ></asp:ListItem>
        </asp:RadioButtonList>
    </fieldset>
    </div>
    <div data-role="fieldcontain" id="divStudyArea" style="display:none;" runat="server">
        <asp:Label ID="LblStudyArea" runat="server" Text="Study Area"  AssociatedControlID="dropDownListStudyArea"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListStudyArea"  runat="server">
        </asp:DropDownList>
    </div>
    
    

    <div data-role="fieldcontain">
        <asp:Label ID="Label9" runat="server" Text="Annual Income" AssociatedControlID="dropDownListAnnualIncome"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListAnnualIncome" runat="server">
        </asp:DropDownList>
    </div>
    <div data-role="fieldcontain">
        <asp:Label ID="Label10" runat="server" Text="Education" AssociatedControlID="dropDownListEducation"></asp:Label>
        <asp:DropDownList data-mini="true" ID="dropDownListEducation" runat="server">
        </asp:DropDownList>
        
       
    </div>
  
</div>

<style>
    .header { text-align:center; }
    .header h4 { margin-bottom:0 }
    .header img { width:235px }
</style>
</asp:Content>
