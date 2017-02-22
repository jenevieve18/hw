"@fixture 8.0.3 Case English (ADM)";
"@page http://test-adm.healthwatch.se/";
"@auth ian:Start123!!!";

"@test"["2.0 case  7 and 8 "] = {
    '1.Click link "Users"': function() {
        act.click(":containsExcludeChildren(Users)");
    },
    '2.Type in input "search"': function() {
        act.type("#search", "hwgrptst1");
    },
    '3.Click submit button "OK"': function() {
        act.click("#OK");
    },
    '4.Click link "fake 2"': function() {
        act.click(":containsExcludeChildren(fake 2)");
    }
};