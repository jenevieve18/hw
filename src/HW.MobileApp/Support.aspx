﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MobileApp.Master" AutoEventWireup="true" CodeBehind="Support.aspx.cs" Inherits="HW.MobileApp.Support" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div data-role="header" data-theme="b" data-position="fixed">
    <a href="More.aspx" data-icon="arrow-l">Back</a>
    <h1>Support</h1>
</div>
<div data-role="content" id="support">
    
         <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u"  data-inset="false" data-iconpos="right">
            <h1>How do I track my health?</h1>
            <div data-role="content">
            Track your health development over time by responding to the 11 questions in the Form. Once you save, you get immediate individual feedback and an action plan. When clicking the exercises in the action plan you will be directed to Exercises. You may also view your development over time by clicking ‘View’. You can choose to view one question at a time or several in the same graph.
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
            <h1>How do I change my Email Address?</h1>
            <div data-role="content">
                 You can change your email address by tapping the icon for ‘Settings’ which is placed on the top corner of ‘My Health’. Tap ‘Change Profile’ and fill in your new email address. Thereafter tap ‘Save’.</span>
            </div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1>What does the Calendar Do?</h1>
                    <div data-role="content">The diary may be used to write down your thoughts and feelings and can have a powerful effect for reducing anxiety and stress. There is both a typing mode and a read mode in the diary. You can also add other activities and measurements, such as physical activity, blood pressure and BMI for example. You may also see which exercises you have done and how you have responded to your forms.</div>
        </div>
        <div data-role="collapsible" data-content-theme="d" data-collapsed-icon="arrow-r" data-expanded-icon="arrow-u" data-inset="false" data-iconpos="right" >
                    <h1>More FAQs</h1>
                    <div data-role="content">More frequently asked questions will be displayed here shortly. Thank you for your patience.
                </div>
        </div>
    
</div>

</asp:Content>
