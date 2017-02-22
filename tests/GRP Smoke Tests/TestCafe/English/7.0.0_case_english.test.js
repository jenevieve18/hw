"@fixture 7.0.0 Case English";
"@page http://dev-grp.healthwatch.se/";







"@test"["01st run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. Click link in email to verify that email address is correctly populated in HW, then cancel registration. Login in to GMAIL with hwgrptst2@gmail.com and Apels!nju1ce, check mails"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
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
    "9.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '10.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "Invitation 2015-02-07 15:37 subject");
    },
    '11.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "12.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '13.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", "Invitation 2015-02-07 15:37 message!!\r\rtest with Jay");
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
    '17.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)");
    },
    "18.Press key END": function() {
        act.press("end");
    },
    "19.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '20.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)", "Reminder 2015-02-07 15:37 subject");
    },

    '28.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "29.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '30.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", "Reminder 2015-02-07 15:37 message!!\r\rtest with Jay");
    },
    '31.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)");
    },
    "32.Press key END": function() {
        act.press("end");
    },
    "33.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '34.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "Message to All 2015-02-07 15:37 subject");
    },
    '35.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "36.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '37.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", "All 2015-02-07 15:37 message!!\r\rtest with Jay");
    },
    '38.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)");
    },
    "39.Press key END": function() {
        act.press("end");
    },
    "40.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '41.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)", "Reminder 2015-02-07 15:37 subject");
    },
    '42.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "43.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },

    '48.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "Reminder extended survey 2015-02-07 15:37 message!!\r\rtest with Jay");
    },
    '49.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '50.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '51.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '52.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '53.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '54.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '55.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "117");
    },
    '56.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '57.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '58.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '59.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '60.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '61.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '62.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["02nd run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '12.Click option "Registration..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration reminder)");
        };
        act.click(actionTarget);
    },
    '13.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '14.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '15.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '16.Click option "Login reminder"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Login reminder)");
        };
        act.click(actionTarget);
    },
    '17.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '18.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '19.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '20.Click option "All activated users"': function() {
        act.click(":containsExcludeChildren(All activated users)");
    },
    '21.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '22.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '23.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '24.Click option "Reminder:Test1Test2"': function() {
        act.click(":containsExcludeChildren(ReminderTest1Test2)");
    },
    '25.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '26.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '27.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

/*"@test"["04th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Registration..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration reminder)");
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["05th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    "6.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "7.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '8.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '9.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "10.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '11.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "12.Press key END": function() {
        act.press("end");
    },
    "13.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '14.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '15.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '16.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '17.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '18.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '19.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '20.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["07th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Login reminder"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Login reminder)");
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["08th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "All activated users"': function() {
        act.click(":containsExcludeChildren(All activated users)");
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["09th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    '3.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '4.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '5.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '6.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '7.Click option "Reminder:Test1Test2"': function() {
        act.click(":containsExcludeChildren(ReminderTest1Test2)");
    },
    '8.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '9.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '10.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["11th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '6.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Thank you:..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Thank you Test1Test2)");
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["12th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    "6.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "7.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '8.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "9.Press key END": function() {
        act.press("end");
    },
    '10.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '11.Click radio button "The previously..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UserUpdateFrom_2$/)");
    },
    '12.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "13.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '14.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "15.Press key END": function() {
        act.press("end");
    },
    "16.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '17.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '18.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};*/