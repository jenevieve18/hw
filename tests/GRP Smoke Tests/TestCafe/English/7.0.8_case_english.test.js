"@fixture 7.0.8 Case English";
"@page http://dev-grp.healthwatch.se/";

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
};