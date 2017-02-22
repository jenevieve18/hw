"@fixture 7.0.0 Case Swedish";
"@page http://dev-grp.healthwatch.se";







"@test"["1st run. Login in to GMAIL with hwgrptst1@gmail.com and Apels!nju1ce, check mails. 1 new mail."] = {

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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteSubject$/)", "Invitation 2015-02-07 15:53 subject");
    },
    '11.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)");
    },
    "12.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '13.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteTxt$/)", "Invitation 2015-02-07 15:53 message!!\r\rtest with Jay");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderSubject$/)", "Reminder 2015-02-07 15:53 subject");
    },

    '28.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)");
    },
    "29.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '30.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_InviteReminderTxt$/)", "Reminder 2015-02-07 15:53 message!!\r\rtest with Jay");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageSubject$/)", "Message to All 2015-02-07 15:53 subject");
    },
    '35.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)");
    },
    "36.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '37.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_AllMessageBody$/)", "All 2015-02-07 15:53 message!!\r\rtest with Jay");
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveySubject$/)", "Reminder 2015-02-07 15:53 subject");
    },
    '42.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)");
    },
    "43.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '48.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyTxt$/)", "Reminder extended survey 2015-02-07 15:53 message!!\r\rtest with Jay");
    },
    '49.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '50.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '51.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '52.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '53.Click option "Registrering"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registrering)").eq(0);
        };
        act.click(actionTarget);
    },
    '54.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '55.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "117");
    },
    '56.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '57.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '58.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '59.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '60.Click option "Registrering"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registrering)").eq(0);
        };
        act.click(actionTarget);
    },
    '61.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '62.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["2nd run"] = {

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
    '8.Click option "Registrering"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Registrering)").eq(0);
        };
        act.click(actionTarget);
    },
    '9.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '10.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
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
    '13.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '14.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '15.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '16.Click option "Inloggningspåminnel..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)").find(":containsExcludeChildren(Inloggningspåminnelse)");
        };
        act.click(actionTarget);
    },
    '17.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '18.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '19.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '20.Click option "Alla aktiverade..."': function() {
        act.click(":containsExcludeChildren(Alla aktiverade användare)");
    },
    '21.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '22.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '23.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '24.Click option "Påminnelse:Test1Tes..."': function() {
        act.click(":containsExcludeChildren(PåminnelseTest1Test2)");
    },
    '25.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '26.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '27.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

/*"@test"["4th run"] = {

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

"@test"["5th run"] = {
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

"@test"["8th run"] = {

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
    '11.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["9th run"] = {

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



"@test"["run 12th"] = {

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
    '12.Click submit button "Spara"': function() {
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
    '17.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '18.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};

"@test"["7.1th run"] = {

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

"@test"["run 11th"] = {
    '1.Click input "ANV"': function() {
        act.click("#ANV");
    },
    "2.Press key END": function() {
        act.press("end");
    },
    "3.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '4.Type in input "ANV"': function() {
        act.type("#ANV", "118");
    },
    "5.Press key TAB": function() {
        act.press("tab");
    },
    '6.Type in password input "LOS"': function() {
        act.type("#LOS", "118");
    },
    '7.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '8.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '9.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedSubject$/)");
    },
    "10.Press key END": function() {
        act.press("end");
    },
    "11.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '12.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedSubject$/)", "Thank you Test1Test2 2015-02-09 9:24 subject");
    },
    '13.Click text area "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedTxt$/)");
    },
    "14.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '15.Type in text area "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ExtendedSurveyFinishedTxt$/)", "Thank you Test1Test2 2015-02-09 9:24 message!!\r\rtest with Jay");
    },
    '16.Click submit button "Spara"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSave$/)");
    },
    '17.Click link "Organisation"': function() {
        act.click("[title='Administrera enheter och användare']");
    },
    '18.Click link "Meddelanden"': function() {
        act.click("[title='Administrera meddelanden, inbjudningar och påminnelser']");
    },
    '19.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SendType$/)");
    },
    '20.Click option "Tack: Test1Test2"': function() {
        act.click(":containsExcludeChildren(Tack Test1Test2)");
    },
    '21.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Password$/)", "118");
    },
    '22.Click submit button "Skicka"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_buttonSend$/)");
    },
    '23.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    }
};*/