"@fixture 4.0 Case Swedish";
"@page http://dev-grp.healthwatch.se";

"@test"["Usr514 completed"] = {
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
    '4.Click submit button "Logga in"': function() {
        act.click(":containsExcludeChildren(Logga in)");
    },
    '5.Click link "Statistik"': function() {
        act.click("[title='Visa resultat och jämför grupper']");
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
    '10.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "11.Wait 5000 milliseconds": function() {
        act.wait(5e3);
    },
    '12.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '13.Click option "En vecka"': function() {
        act.click(":containsExcludeChildren(En vecka)");
    },
    "14.Wait 1000 milliseconds": function() {
        act.wait(1e3);
    },
    '15.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    "16.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '17.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '18.Click option "En månad"': function() {
        act.click(":containsExcludeChildren(En månad)");
    },
    "19.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '20.Click submit button "Utför"': function() {
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

    "26.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '27.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
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
    '11.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
    '8.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '9.Click option "Användare på enhet..."': function() {
        act.click(":containsExcludeChildren(Användare på enhet och undergrupper)");
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
    '19.Click submit button "Utför"': function() {
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
    '24.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_GroupBy$/)");
    },
    '25.Click option "Två veckor, börja..."': function() {
        act.click(":containsExcludeChildren(Två veckor börja med jämn)");
    },
    '26.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
 
    
    //test here
    "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },

    "20.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(2);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '21.Click option "Linje (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje 196 SD)").eq(2);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '22.Click link "docx"': function() {
        var actionTarget = function() {
            return $(".export-docx-url").eq(1);
        };
        act.click(actionTarget);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "23.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(3);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '24.Click option "Linje (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje SD)").eq(3);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '25.Click link "pptx"': function() {
        var actionTarget = function() {
            return $(".export-pptx-url").eq(2);
        };
        act.click(actionTarget);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "26.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '27.Click option "Boxplot (Min/Max)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Boxplot MinMax)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '28.Click link "xls"': function() {
        act.click(".exportall-xls-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "29.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(3);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '30.Click option "Linje (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje 196 SD)").eq(3);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '31.Click link "xls"': function() {
        var actionTarget = function() {
            return $(".export-xls-url").eq(2);
        };
        act.click(actionTarget);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
        
    "11.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(11);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '12.Click option "Linje (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje SD)").eq(11);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '13.Click link "xls"': function() {
        var actionTarget = function() {
            return $(".export-xls-url").eq(10);
        };
        act.click(actionTarget);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "14.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '15.Click option "Linje (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje SD)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '16.Click link "xls"': function() {
        act.click(".exportall-xls-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '17.Click link "xls verbose"': function() {
        act.click(".exportall-xls-verbose-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '18.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Grouping$/)");
    },
    '19.Click option "Användare på enhet..."': function() {
        act.click(":containsExcludeChildren(Användare på enhet och undergrupper)");
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
    '25.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
       "16.Wait 3000 milliseconds": function() {
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
    '28.Click option "En vecka"': function() {
        act.click(":containsExcludeChildren(En vecka)");
    },
    '29.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
       "16.Wait 3000 milliseconds": function() {
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
    '33.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
       "16.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '34.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "35.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '36.Click option "Linje (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje SD)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '37.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "38.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '39.Click option "Linje (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje 196 SD)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '40.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    "41.Click select": function() {
        var actionTarget = function() {
            return $(".plot-types.small").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '42.Click option "Boxplot (Min/Max)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Boxplot MinMax)").eq(0);
        };
        act.click(actionTarget);
    },
        "12.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '43.Click link "docx"': function() {
        act.click(".exportall-docx-url");
    },
        "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
 
    '46.Click submit button "Utför"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Execute$/)");
    },
       "23.Wait 3000 milliseconds": function() {
        act.wait(3e3);
    },
    '47.Click link "docx"': function() {
        act.click(".exportall-docx-url");
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
    '49.Click option "Linje (± SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje SD)").eq(0);
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
    '52.Click option "Linje (± 1.96 SD)"': function() {
        var actionTarget = function() {
            return $("#admin").find(":containsExcludeChildren(Linje 196 SD)").eq(0);
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
    '57.Click link "Logga ut"': function() {
        act.click(":containsExcludeChildren(Logga ut)");
    },
};