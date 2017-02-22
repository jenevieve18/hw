"@fixture 7.0.2 Case Swedish";
"@page http://dev-grp.healthwatch.se/";

"@test"["4th run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {

    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '6.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Registreringspåminn..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registreringspåminnelse)");
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["5th run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail. Click link, register new account, log out."] = {
    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
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
    '9.Click submit button "Spara"': function() {
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
    '14.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '15.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '16.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '17.Click option "Registrering"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registrering)").eq(0);
        };
        act.click(actionTarget);
    },
    '18.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '19.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '20.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["6th run"] = {

    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "3.Press key TAB": function() {
        act.press("tab");
    },
    '4.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '5.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '6.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '8.Click option "Inloggningspåminnel..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Inloggningspåminnelse)");
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '11.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};