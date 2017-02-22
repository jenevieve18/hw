"@fixture 8.0.8 Case English";
"@page http://dev-grp.healthwatch.se/";

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
};