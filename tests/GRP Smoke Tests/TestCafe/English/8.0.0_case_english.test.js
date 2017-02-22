"@fixture 8.0.0 Case English";
"@page http://dev-grp.healthwatch.se/";

"@test"["01st run, Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail. Login in to GMAIL with hwgrptst2@gmail.com and Apels!nju1ce, check mails. 1 new mail. Login in to GMAIL with hwgrptst@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
        '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '3.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "4.Press key TAB": function() {
        act.press("tab");
    },
    '5.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '6.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '7.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '8.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '9.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)");
    },
    "10.Press key END": function() {
        act.press("end");
    },
    "11.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '12.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "Invitation 2015-01-29 15:02 suj");
    },
    "13.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '14.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "bject");
    },
    '15.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "16.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '17.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", "Invitation 2015-01-29 15:02 message!!\r\rtest with Jay");
    },
    '18.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '19.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '20.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '21.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)");
    },
    '22.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)");
    },
    "23.Press key END": function() {
        act.press("end");
    },
    "24.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '25.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)", "Reminder 2015-01-29 15:06 subject");
    },
    '26.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "27.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '28.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", "Reminder 2015-01-29 15:06 message!!\r\rtest with Jay");
    },
    '29.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)");
    },
    "30.Press key END": function() {
        act.press("end");
    },
    "31.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '32.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "Message to All 2015-01-29 subject");
    },
    "33.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "34.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "35.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "36.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "37.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "38.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "39.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '40.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "15:16 subject");
    },
    '41.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "42.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '43.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", "All 2015-01-29 15:16 message!!\r\rtest with jay");
    },
    '44.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LoginSubject$/)");
    },
    "45.Press key END": function() {
        act.press("end");
    },
    "46.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '47.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LoginSubject$/)", "Login reminder 2015-01-29 15:09 subject");
    },
    '48.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LoginTxt$/)");
    },
    "49.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '50.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LoginTxt$/)", "Login 2015-01-29 15:09 messae");
    },
    "51.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '52.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LoginTxt$/)", "ge!\r\rtest with Jay");
    },
    '53.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)");
    },
    "54.Press key END": function() {
        act.press("end");
    },
    "55.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '56.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)", "Reminder 2015-01-29 15:09 subject");
    },
    '57.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "58.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '59.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "Reminder extended sure");
    },
    "60.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '61.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "bey");
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
    '65.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "vey 2015-01-29 15:09 message!\r\rtest with Jay");
    },
    '66.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '67.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '68.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '69.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '70.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '71.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    }
};


"@test"["02nd run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '13.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '15.Click option "Login reminder"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Login reminder)");
        };
        act.click(actionTarget);
    },
    '16.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '17.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '18.Click option "All activated users"': function() {
        act.click(":containsExcludeChildren(All activated users)");
    },
    '19.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '20.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '21.Click option "Reminder:Test1Test2"': function() {
        act.click(":containsExcludeChildren(ReminderTest1Test2)");
    },
    '22.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '23.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

/*"@test"["04th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
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
    '13.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '14.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["05th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    "10.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "11.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '12.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "13.Press key END": function() {
        act.press("end");
    },
    '14.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '15.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "16.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '17.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "18.Press key END": function() {
        act.press("end");
    },
    "19.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '20.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '21.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '22.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '23.Click option "Registration"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registration)").eq(0);
        };
        act.click(actionTarget);
    },
    '24.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '25.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["06th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "2");
    },
    "8.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '9.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '10.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '11.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '12.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '13.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '14.Click option "Login reminder"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Login reminder)");
        };
        act.click(actionTarget);
    },
    '15.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '16.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["07th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '11.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '12.Click option "All activated users"': function() {
        act.click(":containsExcludeChildren(All activated users)");
    },
    '13.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '15.Click option "Reminder:Test1Test2"': function() {
        act.click(":containsExcludeChildren(ReminderTest1Test2)");
    },
    '16.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },    
    '17.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["09th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '11.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedSubject$/)");
    },
    "12.Press key END": function() {
        act.press("end");
    },
    "13.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '14.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedSubject$/)", "Thank you Test1Test2 2015-01-30 15:32 subject");
    },
    '17.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedTxt$/)");
    },
    "18.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '19.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedTxt$/)", 'Thank you Test1Test2 2015-01-30 15:32 message""\r\rtest with Jay');
    },
    '20.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '21.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    '22.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    '23.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '24.Click option "Thank you:..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Thank you Test1Test2)");
        };
        act.click(actionTarget);
    },
    '25.Click submit button "Send"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '26.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};

"@test"["11th run"] = {
    '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    '2.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "3.Press key END": function() {
        act.press("end");
    },
    "4.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '5.Type in input "ANV"': function() {
        act.type("#ANV", "s118");
    },
    "6.Press key TAB": function() {
        act.press("tab");
    },
    '7.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '8.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Messages"': function() {
        act.click("[title='administer messages, invitations and reminders']");
    },
    "11.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '12.Click link "Organization"': function() {
        act.click("[title='administer units and users']");
    },
    "13.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "14.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '15.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "16.Press key END": function() {
        act.press("end");
    },
    '17.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '18.Click label "The previously..."': function() {
        act.click(":containsExcludeChildren(The previously registered email address has never been correct and the created account should be detached from organization)");
    },
    '19.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "20.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '21.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "22.Press key END": function() {
        act.press("end");
    },
    "23.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '24.Click label "Update the user..."': function() {
        act.click(":containsExcludeChildren(Update the user profile as if these settings were set from start)");
    },
    '25.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "26.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(5) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "27.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(5) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '28.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "29.Press key END": function() {
        act.press("end");
    },
    '30.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '31.Click label "The previously..."': function() {
        act.click(":containsExcludeChildren(The previously registered email address has never been correct and the created account should be detached from organization)");
    },
    '32.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "33.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(5) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '34.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "35.Press key END": function() {
        act.press("end");
    },
    "36.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '37.Click label "Update the user..."': function() {
        act.click(":containsExcludeChildren(Update the user profile as if these settings were set from start)");
    },
    '38.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "39.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "40.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '41.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "42.Press key END": function() {
        act.press("end");
    },
    '43.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "1");
    },
    '44.Click label "The previously..."': function() {
        act.click(":containsExcludeChildren(The previously registered email address has never been correct and the created account should be detached from organization)");
    },
    '45.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "46.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '47.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "48.Press key END": function() {
        act.press("end");
    },
    "49.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '50.Click label "Update the user..."': function() {
        act.click(":containsExcludeChildren(Update the user profile as if these settings were set from start)");
    },
    '51.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '52.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    }
};*/