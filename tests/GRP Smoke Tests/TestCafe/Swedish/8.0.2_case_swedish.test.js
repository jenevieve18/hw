"@fixture 8.0.2 Case Swedish";
"@page http://dev-grp.healthwatch.se/";

"@test"["4th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

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
    '8.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '9.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '10.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '11.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '12.Click option "Registreringspåminn..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registreringspåminnelse)");
        };
        act.click(actionTarget);
    },
    '13.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '14.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["5th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail. Click link, register account, log out."] = {

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
    '8.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
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
    '15.Click submit button "Spara"': function() {
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
    '20.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '21.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '22.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '23.Click option "Registrering"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registrering)").eq(0);
        };
        act.click(actionTarget);
    },
    '24.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '25.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["6th run"] = {

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

    '9.Type in password input "LOS"': function() {
        act.type("#LOS", "s118");
    },
    '10.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '11.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(1);
        };
        act.click(actionTarget);
    },
    '12.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '13.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '14.Click option "Inloggningspåminnel..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Inloggningspåminnelse)");
        };
        act.click(actionTarget);
    },
    '15.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '16.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};