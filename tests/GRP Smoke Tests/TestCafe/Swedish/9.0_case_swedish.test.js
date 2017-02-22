"@fixture 9.0 Case Swedish";
"@page http://dev-grp.healthwatch.se";

"@test"["New Features Reminders and SUsers"] = {

    '1.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "2.Press key END": function() {
        act.press("end");
    },
    "3.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '4.Type in input "ANV"': function() {
        act.type("#ANV", "jens");
    },
    "5.Press key TAB": function() {
        act.press("tab");
    },
    '6.Type in password input "LOS"': function() {
        act.type("#LOS", "jens");
    },
    '7.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '8.Click link "Sponsor83"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Sponsor83)").eq(0);
        };
        act.click(actionTarget);
    },
        '155.Click link "Chefer"': function() {
        act.click("[title='Administrera enheternas chefer']");
    },
    '100.Click link "Lägg till chef"': function() {
        act.click(":containsExcludeChildren(Lägg till chef)");
    },
    '101.Click check box "928"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(4) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '102.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ReadOnly$/)");
    },

    '9.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Name$/)", "test1");
    },
    "10.Press key TAB": function() {
        act.press("tab");
    },
    '11.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LastName$/)", "test2");
    },
    "12.Press key TAB": function() {
        act.press("tab");
    },
    '13.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Usr$/)", "test3");
    },
    "14.Press key TAB": function() {
        act.press("tab");
    },
    '15.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Pas$/)", "test4");
    },
    "16.Press key TAB": function() {
        act.press("tab");
    },
    '17.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "test5");
    },
    '12.Click label "Superanvändare..."': function() {
        act.click(":containsExcludeChildren(Superanvändare kan administrera sitt eget chefskonto inlkusive alla enheter)");
    },
    '19.Click check box "Organisation..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_0$/)");
    },
    '20.Click check box "Meddelanden..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_2$/)");
    },
    '21.Click check box "Påminnelser..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_5$/)");
    },
    '22.Click check box "925"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(2) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '23.Click check box "1256"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(3) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '24.Click check box "928"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(4) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '25.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '26.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
    '27.Type in input "ANV"': function() {
        act.type("#ANV", "test3");
    },
    "28.Press key TAB": function() {
        act.press("tab");
    },
    '29.Type in password input "LOS"': function() {
        act.type("#LOS", "test4");
    },
    '30.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '14.Click link "Påminnelser"': function() {
        act.click("[title='Påminnelseinställningar']");
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)");
    },
    '15.Click option "2 veckor"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)").find(":containsExcludeChildren(2 veckor)");
        };
        act.click(actionTarget);
    },
    '34.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)");
    },
    '26.Click option "Måndag"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)").find(":containsExcludeChildren(Måndag)");
        };
        act.click(actionTarget);
    },
    '36.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)");
    },
    '22.Click option "månad"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)").find(":containsExcludeChildren(månad)").eq(0);
        };
        act.click(actionTarget);
    },
    '38.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)");
    },
    '28.Click option "Tisdag"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)").find(":containsExcludeChildren(Tisdag)");
        };
        act.click(actionTarget);
    },
   
    '44.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '67.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '46.Click link "Påminnelser"': function() {
        act.click("[title='Påminnelseinställningar']");
    },
    
   '10.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)");
    },
    '11.Click option "< samma som förälder >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)").find(":containsExcludeChildren(samma som förälder)");
        };
        act.click(actionTarget);
    },
    '12.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)");
    },
    '13.Click option "< samma som förälder >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)").find(":containsExcludeChildren(samma som förälder)");
        };
        act.click(actionTarget);
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)");
    },
    '15.Click option "< samma som förälder >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)").find(":containsExcludeChildren(samma som förälder)");
        };
        act.click(actionTarget);
    },
    '16.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)");
    },
    '17.Click option "< samma som förälder >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)").find(":containsExcludeChildren(samma som förälder)");
        };
        act.click(actionTarget);
    },

    '22.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '23.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '24.Click link "Påminnelser"': function() {
        act.click("[title='Påminnelseinställningar']");
    },
    
    '47.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
    '48.Type in input "ANV"': function() {
        act.type("#ANV", "jens");
    },
    "49.Press key TAB": function() {
        act.press("tab");
    },
    '50.Type in password input "LOS"': function() {
        act.type("#LOS", "jens");
    },
    '51.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '52.Click link "Sponsor83"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Sponsor83)").eq(0);
        };
        act.click(actionTarget);
    },
    '53.Click link "Chefer"': function() {
        act.click("[title='Administrera enheternas chefer']");
    },
    "54.Click image": function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(0) > tbody:nth(0) > tr:nth(10) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        handleConfirm(true);
        act.click(actionTarget);
    }
};

"@test"["Messages - different User Messages"] = {

    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "Usr514");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '8.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '9.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '10.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", " Usr514");
    },
    '11.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", " Usr514");
    },
    '12.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", " Usr514");
    },
    '13.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", " Usr514");
    },
    '14.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '15.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '16.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '17.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
    '18.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "19.Press key END": function() {
        act.press("end");
    },
    "20.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '21.Type in input "ANV"': function() {
        act.type("#ANV", "Usr515");
    },
    "22.Press key TAB": function() {
        act.press("tab");
    },
    '23.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas515");
    },
    '24.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '25.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '26.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", " Usr515");
    },
    '27.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", " Usr515");
    },
    '28.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", " Usr515");
    },
    '29.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", " Usr515");
    },
    '30.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '31.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '32.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '33.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
    '34.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "35.Press key END": function() {
        act.press("end");
    },
    "36.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '37.Type in input "ANV"': function() {
        act.type("#ANV", "Usr515");
    },
    "38.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '39.Type in input "ANV"': function() {
        act.type("#ANV", "4");
    },
    "40.Press key TAB": function() {
        act.press("tab");
    },
    '41.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '42.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '43.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '44.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "45.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "46.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "47.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "48.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "49.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "50.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '51.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "52.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "53.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "54.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "55.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "56.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "57.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '58.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "59.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "60.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "61.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "62.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "63.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "64.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '65.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "66.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "67.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "68.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "69.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "70.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "71.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '72.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '73.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '74.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '75.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
    '76.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "77.Press key END": function() {
        act.press("end");
    },
    "78.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '79.Type in input "ANV"': function() {
        act.type("#ANV", "Usr515");
    },
    "80.Press key TAB": function() {
        act.press("tab");
    },
    '81.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas515");
    },
    '82.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '83.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '84.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "85.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "86.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "87.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "88.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "89.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "90.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '91.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "92.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "93.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "94.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "95.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "96.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "97.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '98.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "99.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "100.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "101.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "102.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "103.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "104.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '105.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "106.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "107.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "108.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "109.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "110.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "111.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '112.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '113.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '114.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '115.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};



"@test"["Revert Message final"] = {

    '2.Type in input "ANV"': function() {
        act.type("#ANV", "Usr514");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '5.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '6.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '7.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)");
    },
    "8.Press key END": function() {
        act.press("end");
    },
    "9.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '10.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "Invitation 2015-02-10 subject");
    },
    '11.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "12.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    "13.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "14.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '15.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", "test with Jay");
    },
    '16.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)");
    },
    "17.Press key END": function() {
        act.press("end");
    },
    "18.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '19.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)", "Invitation Reminder 2015-02-10");
    },
    '20.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "21.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '22.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", "test with Jay");
    },
    '23.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)");
    },
    "24.Press key END": function() {
        act.press("end");
    },
    "25.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '26.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "message to all 2015-02-10");
    },
    '27.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "28.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '29.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", "test with Jay");
    },
    '30.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)");
    },
    "31.Press key END": function() {
        act.press("end");
    },
    "32.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '33.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)", "test with Jay");
    },
    '34.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "35.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '36.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "test with jay");
    },
    '37.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '38.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '39.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    "40.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '41.Click submit button "Återgå till standard"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonRevert$/)");
    },
    '42.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '43.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '44.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    "45.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '46.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};



"@test"["test exercises Swedish"] = {
    '1.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "2.Press key END": function() {
        act.press("end");
    },
    "3.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "Usr514");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '8.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '9.Click link "Övningar"': function() {
        act.click("[title='Chefsövningar']");
    },
    '10.Click span "Visa alla"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Visa alla)").eq(0);
        };
        act.click(actionTarget);
    },
    '11.Click link "Arbetsglädje och..."': function() {
        var actionTarget = function() {
            return $("#EAID7").find(":containsExcludeChildren(Arbetsglädje och effektivitet)");
        };
        act.click(actionTarget);
    },
    '12.Click span "Visa alla"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Visa alla)").eq(1);
        };
        act.click(actionTarget);
    },
    '13.Click link "Feedback"': function() {
        var actionTarget = function() {
            return $("#ECID1").find(":containsExcludeChildren(Feedback)");
        };
        act.click(actionTarget);
    },
    '14.Click span "Feedback"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Feedback)").eq(0);
        };
        act.click(actionTarget);
    },
    '15.Click link "Fira framgång"': function() {
        act.click(":containsExcludeChildren(Fira framgång)");
    },
    '16.Click span "Fira framgång"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Fira framgång)").eq(0);
        };
        act.click(actionTarget);
    },
    '17.Click link "Mål & visioner"': function() {
        act.click(":containsExcludeChildren(Mål visioner)");
    },
    '18.Click span "Mål & visioner"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Mål visioner)").eq(0);
        };
        act.click(actionTarget);
    },
    '19.Click link "Utveckla..."': function() {
        act.click(":containsExcludeChildren(Utveckla medarbetarna)");
    },
    '20.Click span "Arbetsglädje och..."': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Arbetsglädje och effektivitet)").eq(0);
        };
        act.click(actionTarget);
    },
    '21.Click link "Visa alla"': function() {
        var actionTarget = function() {
            return $("#EAID0").find(":containsExcludeChildren(Visa alla)");
        };
        act.click(actionTarget);
    },
    '22.Click span "Visa alla"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Visa alla)").eq(1);
        };
        act.click(actionTarget);
    },
    '23.Click span "Visa alla"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Visa alla)").eq(1);
        };
        act.click(actionTarget);
    },
    '24.Click span "Popularitet"': function() {
        act.click(":containsExcludeChildren(Popularitet)");
    },
    '25.Click span "Alfabetisk"': function() {
        act.click(":containsExcludeChildren(Alfabetisk)");
    },
    '26.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};