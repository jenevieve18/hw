"@fixture 3.0 Case English";
"@page http://dev-grp.healthwatch.se/default.aspx?SKEY=FB8CC11083";

"@test"["Administrator completed"] = {
        '1.Click link "In English"': function() {
        act.click(":containsExcludeChildren(In English)");
    },
    "6.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(2) > td:nth(1) > a:nth(0) > img:nth(0)");
        act.click(image);
    },
    '7.Click input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Department$/)");
        act.click(input);
    },
    "8.Press key END": function() {
        act.press("end");
    },
    "9.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '10.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Department$/)");
        act.type(input, "Very very long department name this is,says Yoda");
    },
    "11.Press key TAB": function() {
        act.press("tab");
    },
    "12.Press key END": function() {
        act.press("end");
    },
    "13.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '14.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:DepartmentShort$/)");
        act.type(input, "L");
    },
    '15.Click select "_ctl0:ContentPlaceH..."': function() {
        var select = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)");
        act.click(select);
    },
    '16.Click option "Medium Length..."': function() {
        var option = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        act.click(option);
    },
    '17.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUnit$/)");
        act.click(submitButton);
    },
    "18.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(33) > td:nth(1) > a:nth(0) > img:nth(0)");
        act.click(image);
    },
    '19.Click input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Department$/)");
        act.click(input);
    },
    "20.Press key END": function() {
        act.press("end");
    },
    "21.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '22.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Department$/)");
        act.type(input, "Short departmet");
    },
    "23.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '24.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Department$/)");
        act.type(input, "nt");
    },
    "25.Press key TAB": function() {
        act.press("tab");
    },
    "26.Press key END": function() {
        act.press("end");
    },
    "27.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '28.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:DepartmentShort$/)");
        act.type(input, "S");
    },
    '29.Click select "_ctl0:ContentPlaceH..."': function() {
        var select = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ParentDepartmentID$/)");
        act.click(select);
    },
    '30.Click option "< top level >"': function() {
        var option = $(":containsExcludeChildren(top level)");
        act.click(option);
    },
    '31.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUnit$/)");
        act.click(submitButton);
    },
    '32.Click link "Add user"': function() {
        var link = $(":containsExcludeChildren(Add user)");
        act.click(link);
    },
    '33.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Email$/)");
        act.type(input, "hwgrptst@gmail.com");
    },
    '34.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUser$/)");
        act.click(submitButton);
    },
    '35.Click link "Add user"': function() {
        var link = $(":containsExcludeChildren(Add user)");
        act.click(link);
    },
    '36.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Email$/)");
        act.type(input, "hwgrptst@gmail.com");
    },
    '37.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUser$/)");
        act.click(submitButton);
    },
    "38.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(2) > td:nth(1) > a:nth(1) > img:nth(0)");
        act.click(image);
    },
    '39.Click link "Send"': function() {
        var link = $(":containsExcludeChildren(Send)");
        act.click(link);
    },
    "40.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        act.click(image);
    },
    '41.Click select "_ctl0:ContentPlaceH..."': function() {
        var select = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Hidden23$/)");
        act.click(select);
    },
    '42.Click option "Nej"': function() {
        var option = $(":containsExcludeChildren(Nej)");
        act.click(option);
    },
    '43.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUser$/)");
        act.click(submitButton);
    },
    "44.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        act.click(image);
    },
    '45.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Email$/)");
        act.type(input, "x");
    },
    '46.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUser$/)");
        act.click(submitButton);
    },
    "47.Click image": function() {
        var image = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(3) > td:nth(2) > a:nth(0) > img:nth(0)");
        act.click(image);
    },
    '48.Click select "_ctl0:ContentPlaceH..."': function() {
        var select = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentID$/)");
        act.click(select);
    },
    '49.Click option "Medium Length..."': function() {
        var option = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_DepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        act.click(option);
    },
    '50.Click select "_ctl0:ContentPlaceH..."': function() {
        var select = $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_StoppedReason$/)");
        act.click(select);
    },
    '51.Click option "Stopped, work..."': function() {
        var option = $(":containsExcludeChildren(Stopped work related)");
        act.click(option);
    },
    '52.Click input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Stopped$/)");
        act.click(input, {
            caretPos: 9
        });
    },
    "53.Press key END": function() {
        act.press("end");
    },
    "54.Press key BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE BACKSPACE": function() {
        act.press("backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace backspace");
    },
    '55.Type in input "_ctl0:ContentPlaceH..."': function() {
        var input = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:Stopped$/)");
        act.type(input, "2015-02-02");
    },
    '56.Click submit button "Save"': function() {
        var submitButton = $("[name='aspnetForm']").find(":attrRegExp(name:/^_ctl\\d+:ContentPlaceHolder1:SaveUser$/)");
        act.click(submitButton);
    },
    "57.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "58.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '59.Click input "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)");
    },
    "60.Press key BACKSPACE": function() {
        act.press("backspace");
    },
    '61.Click label "Update the user..."': function() {
        act.click(":containsExcludeChildren(Update the user profile as if these settings were set from start)");
    },
    '62.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveUser$/)");
    },
       "63.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(37) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "64.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(2) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '65.Click submit button "Execute"': function() {
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
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    "89.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(2) > a:nth(1) > img:nth(0)");
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
    
    '226.Click link "Import units"': function() {
        act.click(":containsExcludeChildren(Import units)");
    },
    '227.Click file button "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UnitsFilename$/)");
    },
    '228.Upload "testUnitImport.txt" file': function() {
        act.upload(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_UnitsFilename$/)", "./uploads/testUnitImport.txt");
    },
    '229.Click select "_ctl0:ContentPlaceH..."': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUnitsParentDepartmentID$/)");
    },
    '210.Click option "Medium Length..."': function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_ImportUnitsParentDepartmentID$/)").find(":containsExcludeChildren(Medium Length Department)");
        };
        act.click(actionTarget);
    },
    '211.Click submit button "Save"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveImportUnit$/)");
    },
    "212.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(6) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '213.Click submit button "Execute"': function() {
        act.click(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_SaveDeleteDepartment$/)");
    },
    "214.Click image": function() {
        var actionTarget = function() {
            return $(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_OrgTree$/)").find(" > table:nth(0) > tbody:nth(0) > tr:nth(38) > td:nth(1) > a:nth(1) > img:nth(0)");
        };
        act.click(actionTarget);
    },
    '215.Click submit button "Execute"': function() {
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
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Pas$/)", "test3");
    },
    "109.Press key TAB": function() {
        act.press("tab");
    },
    '110.Type in input "_ctl0:ContentPlaceH..."': function() {
        act.type(":attrRegExp(id:/^_ctl\\d+_ContentPlaceHolder1_Email$/)", "test4");
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
            return $(".smallContent").find(" > table:nth(0) > tbody:nth(0) > tr:nth(10) > td:nth(2) > a:nth(0) > img:nth(0)");
        };
        handleConfirm(true);
        act.click(actionTarget);
    },
        '131.Click link "Log out"': function() {
        act.click(":containsExcludeChildren(Log out)");
    },
};