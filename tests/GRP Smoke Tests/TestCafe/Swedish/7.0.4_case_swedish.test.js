"@fixture 7.0.4 Case Swedish";
"@page http://dev-grp.healthwatch.se/";

"@test"["7.1th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

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

"@test"["8th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

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
    '8.Click option "Alla aktiverade..."': function() {
        act.click(":containsExcludeChildren(Alla aktiverade användare)");
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

"@test"["9th run. Login in to GMAIL with \"hwgrptst1@gmail.com\" and \"Apels!nju1ce\", check mails. 1 new mail."] = {

    '2.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    '3.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '4.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '5.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '6.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '7.Click option "Påminnelse:Test1Tes..."': function() {
        act.click(":containsExcludeChildren(PåminnelseTest1Test2)");
    },
    '8.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '9.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '10.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};