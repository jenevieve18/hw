"@fixture 9.0 Case English";
"@page http://dev-grp.healthwatch.se";

"@test"["New Features Reminders and SUsers"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "jens");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "jens");
    },
    "5.Click <i>": function() {
        act.click(".icon-circle-arrow-right");
    },
    '6.Click link "Sponsor83"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Sponsor83)").eq(0);
        };
        act.click(actionTarget);
    },
    '7.Click link "Managers"': function() {
        act.click("[title='administer unit managers']");
    },
    '8.Click link "Add manager"': function() {
        act.click(":containsExcludeChildren(Add manager)");
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
    '18.Click check box "Super user (can..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SuperUser$/)");
    },
    '19.Click check box "Organization..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_0$/)");
    },
    '20.Click check box "Messages..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_2$/)");
    },
    '21.Click check box "Reminders..."': function() {
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
    '25.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '26.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
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
    '30.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '31.Click link "Reminders"': function() {
        act.click("[title='reminders settings']");
    },
    '32.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)");
    },
    '33.Click option "2 weeks"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)").find(":containsExcludeChildren(2 weeks)");
        };
        act.click(actionTarget);
    },
    '34.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)");
    },
    '35.Click option "Monday"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)").find(":containsExcludeChildren(Monday)");
        };
        act.click(actionTarget);
    },
    '36.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)");
    },
    '37.Click option "month"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)").find(":containsExcludeChildren(month)").eq(0);
        };
        act.click(actionTarget);
    },
    '38.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)");
    },
    '39.Click option "Tuesday"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)").find(":containsExcludeChildren(Tuesday)");
        };
        act.click(actionTarget);
    },

    '44.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '45.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '46.Click link "Reminders"': function() {
        act.click("[title='reminders settings']");
    },
    
    
   '10.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)");
    },
    '11.Click option "< same as parent >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID925$/)").find(":containsExcludeChildren(same as parent)");
        };
        act.click(actionTarget);
    },
    '12.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)");
    },
    '13.Click option "< same as parent >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID925$/)").find(":containsExcludeChildren(same as parent)");
        };
        act.click(actionTarget);
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)");
    },
    '15.Click option "< same as parent >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LDID1256$/)").find(":containsExcludeChildren(same as parent)");
        };
        act.click(actionTarget);
    },
    '16.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)");
    },
    '17.Click option "< same as parent >"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LWID1256$/)").find(":containsExcludeChildren(same as parent)");
        };
        act.click(actionTarget);
    },

    '22.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '23.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '24.Click link "Reminders"': function() {
        act.click("[title='reminders settings']");
    },
    
    '47.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
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
    '51.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '52.Click link "Sponsor83"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Sponsor83)").eq(0);
        };
        act.click(actionTarget);
    },
    '53.Click link "Managers"': function() {
        act.click("[title='administer unit managers']");
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
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
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
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '14.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '15.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '16.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '17.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
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
    '24.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '25.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '30.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '31.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '32.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '33.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
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
    '42.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '43.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '72.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '73.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '74.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '75.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
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
    '82.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '83.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '112.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '113.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '114.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '115.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};



"@test"["Revert Message final"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "Usr514");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "Invitation 2015-02-02 subject");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)", "Invitation Reminder 2015-02-02");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "message to all 2015-02-02");
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
    '37.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '38.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '39.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    "40.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '41.Click submit button "Revert to default"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonRevert$/)");
    },
    '42.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '43.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '44.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    "45.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '46.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["test exercises English"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
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
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "Exercises"': function() {
        act.click("[title='manager exercises']");
    },
    '10.Click span "Show all"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Show all)").eq(0);
        };
        act.click(actionTarget);
    },
    '11.Click link "Work joy and..."': function() {
        var actionTarget = function() {
            return $("#EAID7").find(":containsExcludeChildren(Work joy and efficiency)");
        };
        act.click(actionTarget);
    },
    '12.Click span "Show all"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(Show all)").eq(1);
        };
        act.click(actionTarget);
    },
    '13.Click link "Celebrate success"': function() {
        var actionTarget = function() {
            return $("#ECID2").find(":containsExcludeChildren(Celebrate success)");
        };
        act.click(actionTarget);
    },
    '14.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '15.Click link "Exercises"': function() {
        act.click("[title='manager exercises']");
    },
    '16.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};