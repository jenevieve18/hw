"@fixture 4.0 Case English";
"@page http://dev-grp.healthwatch.se/";

"@test"["Usr514 completed"] = {
           '1.01 Click link "In English"': function() {
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
    
    
    '1.Type in input "ANV"': function() {
        act.type("#ANV", "Usr514");
    },
    "2.Press key TAB": function() {
        act.press("tab");
    },
    '3.Type in password input "LOS"': function() {
        act.type("#LOS", "Pas514");
    },
    '4.Click submit button "Sign in"': function() {
        act.click(":containsExcludeChildren(Sign in)");
    },
    '5.1 Click link "Statistics"': function() {
        act.click("[title='view results and compare groups']");
    },
    '6.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_FromYear$/)");
    },
    '7.Click option "2012"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_FromYear$/)").find(":containsExcludeChildren(2012)");
        };
        act.click(actionTarget);
    },
    '8.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)");
    },
    '9.Click option "2013"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)").find(":containsExcludeChildren(2013)");
        };
        act.click(actionTarget);
    },
    '10.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "11.Wait 5000 milliseconds": function() {
        act.wait(5e3);
    },
    '12.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '13.Click option "One week"': function() {
        act.click(":containsExcludeChildren(One week)");
    },
    "14.Wait 1000 milliseconds": function() {
        act.wait(1e3);
    },
    '15.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "16.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '17.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '18.Click option "One month"': function() {
        act.click(":containsExcludeChildren(One month)");
    },
    "19.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '20.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "21.Wait 5000 milliseconds": function() {
        act.wait(5e3);
    },
    '22.Click link "xls"': function() {
        act.click(".exportall-xls-url");
    },
    "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },


        '7.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_FromYear$/)");
    },
        '8.Click option "2012"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_FromYear$/)").find(":containsExcludeChildren(2012)");
        };
        act.click(actionTarget);
    },
    '9.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)");
    },
    '10.Click option "2012"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)").find(":containsExcludeChildren(2012)");
        };
        act.click(actionTarget);
    },
    '11.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    '12.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '13.Click option "Users on..."': function() {
        act.click(":containsExcludeChildren(Users on unitsubunits)");
    },
    '14.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID925$/)");
    },
    '15.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID1256$/)");
    },
    '16.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID928$/)");
    },
    '17.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID1257$/)");
    },
    '18.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID2646$/)");
    },
    '19.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    '20.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '21.Click option "< none >"': function() {
        act.click(":containsExcludeChildren(none)");
    },
    '22.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)");
    },
    '23.Click option "2013"': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ToYear$/)").find(":containsExcludeChildren(2013)");
        };
        act.click(actionTarget);
    },
          '10.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '11.Click option "Two weeks, start..."': function() {
        act.click(":containsExcludeChildren(Two weeks start with even)");
    },
    '24.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
 
    
    //test here
    "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "13.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(5);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '14.Click option "Line (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line SD)").eq(5);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "15.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(5);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '16.Click option "Line (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line 196 SD)").eq(5);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "17.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '18.Click option "Line (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line SD)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "19.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '20.Click option "Line (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line 196 SD)").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "21.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '22.Click option "Boxplot (Min/Max)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Boxplot MinMax)").eq(0);
        };
        act.click(actionTarget);
    },
    "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
       "14.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(2);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '15.Click option "Line (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line 196 SD)").eq(2);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '16.Click link "docx"': function() {
        var actionTarget = function() {
            return $(".export-docx-url").eq(1);
        };
        act.click(actionTarget);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },



    '18.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '19.Click option "Users on..."': function() {
        act.click(":containsExcludeChildren(Users on unitsubunits)");
    },
//    '20.Click check box "on"': function() {
//        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID925$/)");
//    },
//    '21.Click check box "on"': function() {
//        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID1256$/)");
//    },
//    '22.Click check box "on"': function() {
//        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID928$/)");
//    },
//    '23.Click check box "on"': function() {
//        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID1257$/)");
//    },
//    '24.Click check box "on"': function() {
//        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DID2646$/)");
//    },
    '25.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '26.Click link "xls verbose"': function() {
        act.click(".exportall-xls-verbose-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '27.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '28.Click option "One week"': function() {
        act.click(":containsExcludeChildren(One week)");
    },
    '29.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '30.Click link "xls verbose"': function() {
        act.click(".exportall-xls-verbose-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '31.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '32.Click option "< none >"': function() {
        act.click(":containsExcludeChildren(none)");
    },
    '33.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '34.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "48.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '49.Click option "Line (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line SD)").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '50.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "51.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '52.Click option "Line (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Line 196 SD)").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '53.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "54.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '55.Click option "Boxplot (Min/Max)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Boxplot MinMax)").eq(0);
        };
        act.click(actionTarget);
    },
      "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '56.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '57.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    },    
 
};