"@fixture 1.0 Case English";
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
    "5.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(2) > td:nth(1) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '6.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Department$/)");
    },
    "7.Press key END": function() {
        act.press("end");
    },
    "8.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '9.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Department$/)", "Very very long department name says Yoda");
    },
    "14.Press key TAB": function() {
        act.press("tab");
    },
    "15.Press key END": function() {
        act.press("end");
    },
    "16.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '17.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentShort$/)", "L");
    },
    '18.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)");
    },
    '19.Click option "Medium Length..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        };
        act.click(actionTarget);
    },
    '20.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUnit$/)");
    },
    "21.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(1) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '22.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Department$/)", {
            caretPos: 38
        });
    },
    "23.Press key END": function() {
        act.press("end");
    },
    "24.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '25.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Department$/)", "Short department");
    },
    "26.Press key TAB": function() {
        act.press("tab");
    },
    "27.Press key END": function() {
        act.press("end");
    },
    "28.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '29.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentShort$/)", "S");
    },
    '30.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)");
    },
    '31.Click option "< top level >"': function() {
        act.click(":containsExcludeChildren(top level)");
    },
    '32.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUnit$/)");
    },
    '33.Click link "Add user"': function() {
        act.click(":containsExcludeChildren(Add user)");
    },
    '34.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "hwgrptst@gmail.com");
    },
    '37.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '38.Click link "Add user"': function() {
        act.click(":containsExcludeChildren(Add user)");
    },
    '39.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "hwgrptst@gmail.com");
    },
    '42.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '43.Click submit button "Cancel"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_CancelUser$/)");
    },
    "44.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(2) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '45.Click link "Send"': function() {
        act.click(":containsExcludeChildren(Send)");
    },
    "46.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '47.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Hidden23$/)");
    },
    '48.Click option "Nej"': function() {
        act.click(":containsExcludeChildren(Nej)");
    },
    '49.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "50.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '51.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "52.Press key END": function() {
        act.press("end");
    },
    '53.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "x");
    },
    '54.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "55.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '56.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentID$/)");
    },
    '57.Click option "Medium Length..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        };
        act.click(actionTarget);
    },
    '58.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_StoppedReason$/)");
    },
    '59.Click option "Stopped, work..."': function() {
        act.click(":containsExcludeChildren(Stopped work related)");
    },
    '60.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Stopped$/)", {
            caretPos: 6
        });
    },
    "61.Press key END": function() {
        act.press("end");
    },
    "62.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    "63.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '64.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Stopped$/)", "2015-02-02");
    },
    '65.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "66.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "67.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '68.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "69.Press key END": function() {
        act.press("end");
    },
    "70.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '71.Click label "Update the user..."': function() {
        act.click(":containsExcludeChildren(Update the user profile as if these settings were set from start)");
    },
    '72.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    '73.Click link "Send"': function() {
        act.click(":containsExcludeChildren(Send)");
    },
    "74.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '75.Click radio button "The previously..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UserUpdateFrom_2$/)");
    },
    '76.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
    "77.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(2) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '78.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteUser$/)");
    },
        '79.Click link "Import users"': function() {
        act.click(":containsExcludeChildren(Import users)");
    },
    '80.Click file button "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UsersFilename$/)");
    },
    '81.Upload "testImport.txt" file': function() {
        act.upload(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UsersFilename$/)", "./uploads/testImport.txt");
    },
    '82.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUsersParentDepartmentID$/)");
    },
    '82.Click option "Medium Length..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUsersParentDepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        };
        act.click(actionTarget);
    },
    '84.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveImportUser$/)");
    },
        "85.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(2) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "86.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '87.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteUser$/)");
    },
    "88.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "89.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(2) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '90.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteUser$/)");
    },
    
    '91.Click link "Add unit"': function() {
        act.click(":containsExcludeChildren(Add unit)");
    },
    '92.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Department$/)", "Delete");
    },
    "93.Press key TAB": function() {
        act.press("tab");
    },
    '94.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentShort$/)", "D");
    },
    '95.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)");
    },
    '96.Click option "Short department »..."': function() {
        act.click(":containsExcludeChildren(Short department Empty subsubsub Subsubshort Bottom)");
    },
    '97.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUnit$/)");
    },
    "98.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '99.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteDepartment$/)");
    },
    
        '225.Click link "Import units"': function() {
        act.click(":containsExcludeChildren(Import units)");
    },
    '226.Click file button "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UnitsFilename$/)");
    },
    '227.Upload "testUnitImport.txt" file': function() {
        act.upload(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UnitsFilename$/)", "./uploads/testUnitImport.txt");
    },
    '228.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUnitsParentDepartmentID$/)");
    },
    '229.Click option "Medium Length..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUnitsParentDepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        };
        act.click(actionTarget);
    },
    '210.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveImportUnit$/)");
    },
    "211.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '212.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteDepartment$/)");
    },
    "213.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(7) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '214.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteDepartment$/)");
    },
    
    '155.Click link "Managers"': function() {
        act.click("[title='administer unit managers']");
    },
    '100.Click link "Add manager"': function() {
        act.click(":containsExcludeChildren(Add manager)");
    },
    '101.Click check box "928"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(4) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '102.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ReadOnly$/)");
    },
    '103.Click check box "Organization..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_0$/)");
    },
    '104.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Name$/)", "test1");
    },
      '104.5.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_LastName$/)", "test2");
    },
    "105.Press key TAB": function() {
        act.press("tab");
    },
  
    '106.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Usr$/)", "Usr514");
    },
    "107.Press key TAB": function() {
        act.press("tab");
    },
    '108.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Pas$/)", "test4");
    },
    "109.Press key TAB": function() {
        act.press("tab");
    },
    '110.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "test5");
    },
    '111.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '112.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Usr$/)");
    },
    "113.Press key END": function() {
        act.press("end");
    },
    "114.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '115.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Usr$/)", "test3");
    },
    "116.Press key TAB": function() {
        act.press("tab");
    },
    '117.Type in password input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Pas$/)", "test4");
    },
    '118.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '119.Click link "test1"': function() {
        act.click(":containsExcludeChildren(test1)");
    },
    '120.Click check box "1256"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(3) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '121.Click check box "on"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ReadOnly$/)");
    },
    '122.Click check box "Organization..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_0$/)");
    },
    '123.Click check box "Messages..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ManagerFunctionID_2$/)");
    },
    '124.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '125.Click link "test1"': function() {
        act.click(":containsExcludeChildren(test1)");
    },
    '126.Click check box "1256"': function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(1) > tbody:nth(0) > tr:nth(3) > td:nth(0) > table:nth(0) > tbody:nth(0) > tr:nth(0) > td:nth(1) > input:nth(0)");
        };
        act.click(actionTarget);
    },
    '127.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Save$/)");
    },
    '128.Click link "test1"': function() {
        act.click(":containsExcludeChildren(test1)");
    },
    '129.Click submit button "Cancel"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Cancel$/)");
    },
    "130.Click image": function() {
        var actionTarget = function() {
            return $(".smallContent").find(" > table:nth(0) > tbody:nth(0) > tr:nth(4) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        handleConfirm(true);
        act.click(actionTarget);
    },
        '131.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    },
};