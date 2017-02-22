"@fixture 7.0.4 Case English";
"@page http://dev-grp.healthwatch.se/";






"@test"["07th run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {
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


"@test"["08th run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {
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

"@test"["09th run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {
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