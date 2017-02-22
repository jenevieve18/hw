"@fixture 8.0.7 Case Swedish (ADM)";
"@page http://test-adm.healthwatch.se/";
"@auth ian:Start123!!!";

"@test"["4.0 case 7 and 8"] = {
    '1.Click link "Sponsors"': function() {
        act.click(":containsExcludeChildren(Sponsors)");
    },
    '2.Click link "118"': function() {
        var actionTarget = function() {
            return $(":containsExcludeChildren(118)").eq(0);
        };
        act.click(actionTarget);
    },
    '3.Click input "Started142"': function() {
        act.click("#Started142");
    },
    "4.Press key END": function() {
        act.press("end");
    },
    "5.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '6.Type in input "Started142"': function() {
        act.type("#Started142", "2016-09-01 00:00");
    },
    '7.Click input "Closed142"': function() {
        act.click("#Closed142");
    },
    "8.Press key END": function() {
        act.press("end");
    },
    "9.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '10.Type in input "Closed142"': function() {
        act.type("#Closed142", "2016-09-01 00:00");
    },
    '11.Click submit button "Save"': function() {
        act.click("#Save");
    }
};