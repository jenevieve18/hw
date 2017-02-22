"@fixture 8.0.4 Case Swedish";
"@page http://dev-grp.healthwatch.se/";

"@test"["6th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

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

"@test"["7th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

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
    '12.Click option "Alla aktiverade..."': function() {
        act.click(":containsExcludeChildren(Alla aktiverade användare)");
    },
    '13.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '14.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '15.Click option "Påminnelse:Test1Tes..."': function() {
        act.click(":containsExcludeChildren(PåminnelseTest1Test2)");
    },
    '16.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },    
    '17.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};