
(function($) {
    $.extend({
        hw: {
            pageFrom: "",
            pageScripts: {
                "news": {
                    bound: 0,
                    init: function(){},
                    show: function(){}
                },
                "category": {
                    bound: 0,
                    init: function(){},
                    show: function(){}
                },
                "summary": {
                    bound: 0,
                    init: function(){},
                    show: function(){}
                },
                "more":{
                    bound: 0,
                    init: function(){},
                    show: function(){}
                },
                "exercises":{
                    bound: 0,
                    limitAuthorized: true,
                    init: function(){},
                    show: function(){}
                },
                "allexc":{
                    bound: 0,
                    limitAuthorized: true,
                    init: function(){},
                    show: function(){}
                },
                "instructions":{
                    bound: 0,
                    init: function(){},
                    show: function(){}
                },
                "statistics":{
                    bound: 0,
                    limitAuthorized: true,
                    init: function(){},
                    show: function(){}
                },
                "reminders":{
                    bound: 0,
                    init: function(){},
                    show: function(){}
                }
            },
            // stores cache objects
            cache: {},
            config: {
                // time to show splash
                splashTimeout: 1000,
                // show start page on start
                showStartPageOnStart: true,
                // current language
                lang: 2,
                // languages thatcan be used
                langs: {
                    1: "sv",
                    2: "en"
                },
                expirationMinutes: 20,
                // page is being viewed from device
                deviceMode: false,
                // baseURL
                baseURL: "http://clients.easyapp.se/healthwatch/",
                // piqliq related information
                piqliq: {
                    URL: "http://api.piqliq.com/applications/healthwatch/" +
                         "containers/",
                    userToken: "1896CCF9958D4BAC9D57547727AF9BB7",
                    sublevel: -1
                },

                webserviceURL: "http://clients.easyapp.se/healthwatch/" +
                               "webservice.wsgi",
                healthWatchURL: "https://ws.healthwatch.se/service.asmx",
                healthWatchNS: "https://ws.healthwatch.se/",

                // cookie options
                cookieOptions: {
                    path: '/'
                }
            },
            auth: {
                token: undefined,
                tokenExpires: undefined,
                isLoggedIn: function () {
                    var auth = $.hw.auth;
                    return ((new Date() < auth.tokenExpires ||
                            auth.tokenExpires === undefined) &&
                            auth.token !== undefined);
                },
                limitAuthorized: function () {
                    var authorized = $.hw.auth.isLoggedIn();
                    if (!authorized) {
                        var loc = $.hw.config.baseURL + '/account/login.html';
                        $.hw.auth.forget();
                        $.mobile.changePage(loc);
                    }
                    return authorized;
                },
                forceLogout: function () {
                    // cancel login
                    $.hw.auth.token = undefined;
                    $.hw.auth.forget();
                    $.hw.auth.limitAuthorized();
                },
                remember: function (options) {
                    options = $.extend({
                        token: $.hw.auth.token,
                        tokenExpires: moment().add('minutes', 20).toDate(),
                        lang: $.hw.config.lang
                    }, options);
                    $.cookie("authToken", options.token,
                             $.hw.config.cookieOptions);
                    $.cookie("authTokenExpire", options.tokenExpires,
                             $.hw.config.cookieOptions);
                    $.cookie("authLang", options.lang,
                             $.hw.config.cookieOptions);
                },
                forget: function () {
                    $.cookie("autoLogin", "");
                    $.cookie("password", "");
                    $.removeCookie('authToken',
                                   $.hw.config.cookieOptions);
                    $.removeCookie('authTokenExpire',
                                   $.hw.config.cookieOptions);
                    $.removeCookie('authLang',
                                   $.hw.config.cookieOptions);
                },
                userExtendPoll: (function () {
                    var timeout;
                    return {
                        start: function () {
                            $.hw.api.userExtendToken.sendRequest({});
                            timeout = setInterval(function () {
                                $.hw.api.userExtendToken.sendRequest({});
                            }, 600000);
                        },
                        stop: function () {
                            clearTimeout(timeout);
                        }
                    };
                })()
            },
            mobile: {
                // add/attach functions that are going to be used by the mobile
                // devices to this object

                // function to move to categories view
                moveToCategories: function(data){
                    $.mobile.changePage($.hw.config.baseURL +
                                            "/news/category.html");
                },
                // reminders
                reminders: {
                    save: function (){
                        $.hw.pageScripts.reminders.save();
                    }
                },
                // calendar
                calendar: {
                    getMeasure: function () {
                        var mom = moment();
                        var year = parseInt($('#addFormYear').val());
                        var month = parseInt($('#addFormMonth').val());
                        var day = parseInt($('#addFormDay').val());
                        var hour = parseInt($('#addFormHour').val());
                        var mins = parseInt($('#addFormMinute').val());
                        var secs = parseInt($('#addFormSecond').val());
                        var date = year + "-" +
                                   ((month < 10)? "0" : "") + month + "-" +
                                   ((day < 10)? "0" : "") + day + "T" +
                                   ((hour < 10)? "0" : "") + hour + ":" +
                                   ((mins < 10)? "0" : "") + mins + ":" +
                                   ((secs < 10)? "0" : "") + secs;
                        var mdata = [];
                        $('.calendarMeasure').each(function (i, v) {
                            var elem = $(this);
                            var id = elem.attr('id').replace("measure_", "");
                            var data = {
                                dateTime: date,
                                measureID: id,
                                measureComponentID_1: 0,
                                value_1: 0,
                                measureComponentID_2: 0,
                                value_2: 0
                            };
                            $.each(elem.find('input'), function (j, jv) {
                                var el = $(jv);
                                var index = (j + 1);
                                var compID = el.attr('id').split("C")[1];
                                data["measureComponentID_" + index] = compID;
                                data["value_" + index] = el.val();
                            });
                            mdata.push(data);
                        });
                        return mdata;
                    },
                    getMeasureData: function () {
                        var data = $.hw.mobile.calendar.getMeasure();
                        return JSON.stringify(data);
                    },
                    setMeasure: function () {
                        var data = $.hw.mobile.calendar.getMeasure();
                        var reqs = [];
                        for (var i = 0; i < data.length; i++) {
                            var req = $.hw.api.calendar.setMeasure(data[i]);
                            reqs.push(req);
                        }
                        $.when.apply(window, reqs).then(function () {
                            if ($.hw.config.device == "ios") {
                                window.location = "easyapp://success";
                            } else {
                                $.mobile.changePage('http://clients.easyapp.se/healthwatch//my_health/' +
                                    'calendar/activity_measurements.html');
                            }
                        }, function () {
                            if ($.hw.config.device == "ios") {
                                window.location = "easyapp://failure";
                            }
                        });
                    },
                    deleteMeasure: function (eventID) {
                        var msg = $.hw.i18n.getTranslation("calendar_activity_measurements");
                        msg = msg.alert_message
                        var makeSure = confirm(msg);
                        if (makeSure) {
                            var req = $.hw.api.calendar.deleteMeasure(eventID);
                            req.then(function (r) {
                                var elem = $('#measureEvent_' + eventID);
                                var listview = elem.closest('.listView');
                                elem.remove();
                                listview.listview().listview('refresh');
                            });
                        }
                    },
                    deleteForm: function (eventID, key) {
                        var msg = $.hw.i18n.getTranslation("calendar_activity_measurements");
                        msg = msg.alert_message
                        var makeSure = confirm(msg);
                        if (makeSure) {
                            var req = $.hw.api.calendar.deleteForm(eventID, key);
                            req.then(function (r) {
                                var elem = $('#measureEvent_' + eventID);
                                var listview = elem.closest('.listView');
                                elem.remove();
                                listview.listview().listview('refresh');
                            });
                        }
                    }
                },
                //news functions for mobile
                news:{
                    getSelectedNews: function(){
                        var data = {"data": $.hw.cache.news.selectedNews};
                        return JSON.stringify(data);
                    },
                    getSummaryTemplate: function(){
                        var data = {"data": $.hw.cache.news.summaryTemplate};
                        return JSON.stringify(data);
                    },
                    getSelectedCategory: function(){
                        var data = {
                            "data": {
                                "id": $.hw.cache.news.selectedCategory,
                                "category": $.hw.cache.news.selectedCategoryName,
                                "lang": $.hw.cache.news.selectedLanguage
                            }
                        };
                        return JSON.stringify(data);
                    }
                },
                exercise:{
                    getSelectedArea : function() {
                        var data = {
                            "data": $.hw.cache.exercises["selectedArea"],
                            "lang": $.hw.config.lang
                        };
                        return JSON.stringify(data);
                    },
                    getVariantID : function() {
                        var data = {
                            "data": $.hw.cache.exercises["selectedVariantID"]
                        };
                        return JSON.stringify(data);
                    },
                    checkLoadedExercise: function() {
                        if($.hw.cache.exercises === undefined){
                            $.hw.cache.exercises = {};
                        }
                        var data = $.hw.cache.exercises["allexercises"];
                        return data !== undefined;
                    },
                    loadAllExercises: function() {
                        var resp = $.hw.api.exerciseEnum.getAllExercises();
                        if($.hw.cache.exercises === undefined){
                            $.hw.cache.exercises = {};
                        }
                        resp.then(function(data){
                            data = data.Body.ExerciseEnumResponse;
                            data = data.ExerciseEnumResult.ExerciseInfo;
                            if(data.length === undefined){
                                var arr = [];
                                arr.push(data);
                                $.hw.cache.exercises["allexercises"] = arr;
                            } else {
                                $.hw.cache.exercises["allexercises"] = data;
                            }
                        });
                        return resp;
                    }

                },
                // register a new account
                account: {
                    getBirthday: function (page) {
                        var month = page.find('#month').val();
                        var day = page.find('#day').val();
                        var year = page.find('#year').val();

                        if (year == null || month == null || day == null) {
                            return null;
                        }

                        month = (month.length < 2)? "0" + month : month;
                        day = (day.length < 2)? "0" + day : day;

                        return year + "-" + month + "-" + day;
                    },
                    getGender: function (page) {
                        var m = page.find("#gender_M").prop("checked")? "M" : "0";
                        var f = page.find("#gender_F").prop("checked")? "F" : "0";

                        return (m == "M")? 1 : ((f == "F")? 2 : 0);
                    },
                    getStatus: function (page) {
                        var single = page.find("#status_S").prop("checked")? "S" : 0;
                        var married = page.find("#status_M").prop("checked")? "M" : 0;

                        if (single == "S") {
                            return 370;
                        } else if (married == "M") {
                            return 369;
                        } else {
                            return 0;
                        }
                    },
                    getOccupationType: function (page) {
                        var fullTime = page.find("#occupationType_F").prop("checked");
                        fullTime = fullTime? "F" : "0";
                        var partTime = page.find("#occupationType_P").prop("checked");
                        partTime = partTime? "P" : "0";

                        if (fullTime == "F") {
                            return 405;
                        } else if (partTime == "P") {
                            return 406;
                        } else {
                            return 0;
                        }
                    },
                    getManagerialPost: function (page) {
                        var yes = page.find("#mngr_Y").attr("checked") ? "Y" : "0";
                        var no = page.find("#mngr_N").attr("checked") ? "N" : "0";

                        if (yes == "Y") {
                            return 412;
                        } else if (no == "N") {
                            return 413;
                        } else {
                            return 0;
                        }
                    },
                    //Javascript used for android and iPhone
                    getArrayData: function () {
                        var arrayData = [
                            $('#lang').val(),
                            $('#username').val(),
                            $('#password').val(),
                            $('#email').val(),
                            $('#altEmail').val(),
                            $('#acceptPolicy').prop('checked') ? 'YES' : 'NO',
                            $.hw.mobile.account.getBirthday(),
                            $.hw.mobile.account.getGender(),
                            $.hw.mobile.account.getStatus(),
                            $('#occupation').val(),
                            $.hw.mobile.account.getOccupationType(),
                            $('#industry').val(),
                            $('#job').val(),
                            $.hw.mobile.account.getManagerialPost(),
                            $('#annualIncome').val(),
                            $('#education').val()
                        ];

                        return JSON.stringify(arrayData);
                    },
                    //Javascript used for android and iPhone
                    getProfileData: function (page) {
                        if (page === undefined) {
                            page = $.mobile.activePage;
                        }

                        var account = $.hw.mobile.account;
                        var acceptPolicy = $('#acceptPolicy').prop('checked');
                        return JSON.stringify({
                            userData: {
                                username: $('#username').val(),
                                password: $('#password').val(),
                                email: $('#email').val(),
                                alternateEmail: $('#altEmail').val(),
                                acceptPolicy: acceptPolicy,
                                languageID: $('#lang').val()
                            },
                            profileQuestions: [{
                                questionID: 4,
                                value: account.getBirthday(page),
                            }, {
                                questionID: 2,
                                value: account.getGender(page),
                            }, {
                                questionID: 7,
                                value: account.getStatus(page)
                            }, {
                                questionID: 9,
                                value: $('#occupation').val()
                            }, {
                                questionID: 16,
                                value: account.getOccupationType(page)
                            }, {
                                questionID: 10,
                                value: $('#studyArea').val(),
                            }, {
                                questionID: 5,
                                value: $('#industry').val(),
                            }, {
                                questionID: 6,
                                value: $('#job').val(),
                            }, {
                                questionID: 19,
                                value: account.getManagerialPost(page),
                            }, {
                                questionID: 8,
                                value: $('#annualIncome').val(),
                            }, {
                                questionID: 11,
                                value: $('#education').val()
                            }]
                        });
                    },
                    //Javascript used for android and iPhone
                    getAccountData: function (language,
                                              username,
                                              password,
                                              email,
                                              alternateEmail,
                                              birthYear,
                                              birthMonth,
                                              birthDay,
                                              gender,
                                              status,
                                              occupation,
                                              occupationType,
                                              industry,
                                              job,
                                              managerialPost,
                                              annualIncome,
                                              education) {
                        $('#lang').val(language);
                        $('#username').val(username);
                        $('#password').val(password);
                        $('#email').val(email);
                        $('#altEmail').val(alternateEmail);

                        // Parse date first and then assign it to
                        // their default elements

                        $('#year').val(birthYear);
                        $('#month').val(birthMonth);
                        $('#day').val(birthDay);

                        // Get gender
                        if (gender == 1) {
                            $('#gender_M').attr('checked', true);
                        } else if (gender == 2) {
                            $('#gender_F').attr('checked', true);
                        }

                        // Get status
                        if (status == 370) {
                            $('#status_S').attr('checked', true);
                        } else if (status == 369) {
                            $('#status_M').attr('checked', true);
                        }

                        $('#occupation').val(occupation);

                        // Get occupation type
                        if (occupationType == 405) {
                            $('#occupationType_F').attr('checked', true);
                        } else if (occupationType == 406) {
                            $('#occupationType_P').attr('checked', true);
                        }

                        $('#industry').val(industry);
                        $('#job').val(job);

                        // Get managerial post
                        if (managerialPost == 412) {
                            $('#mngr_Y').attr('checked', true);
                        } else if (managerialPost == 413) {
                            $('#mngr_N').attr('checked', true);
                        }

                        $('#annualIncome').val(annualIncome);
                        $('#education').val(education);
                    },
                    updateProfile: function (page) {
                        // update profile
                        var update = $.hw.api.user.setProfileQuestion;
                        var acc = $.hw.mobile.account;
                        return $.when(
                            // birthday
                            update(4, acc.getBirthday(page)),
                            // gender
                            update(2, acc.getGender(page)),
                            // marital status
                            update(7, acc.getStatus(page)),
                            // occupation
                            update(9, page.find('#occupation').val()),
                            // occupation type
                            update(16, acc.getOccupationType(page)),
                            // study area
                            update(10, page.find('#studyArea').val()),
                            // industry
                            update(5, page.find('#industry').val()),
                            // job
                            update(6, page.find('#job').val()),
                            // managerial
                            update(19, acc.getManagerialPost(page)),
                            // income
                            update(8, page.find('#annualIncome').val()),
                            // education
                            update(11, page.find('#education').val())
                        );
                    },
                    // load profile question choices
                    loadProfileQuestionChoices: function (page) {
                        $.hw.utils.fillDateDropdown(1900,
                          (new Date()).getFullYear(),'.year_dropdown');
                        $.hw.utils.fillDateDropdown(1, 12, '.month_dropdown');
                        $.hw.utils.fillDateDropdown(1, 31, '.day_dropdown');

                        var questions = $.hw.api.user.getProfileQuestions();

                        questions.then(function (res) {
                                var data = res.Body.ProfileQuestionsResponse;
                                data = data.ProfileQuestionsResult.Question;
                                // function to append options
                                var appendOptions = function (elem, val) {
                                    var ch = elem.children(':not(:disabled)');
                                    ch.remove();
                                    var answers = val.AnswerOptions.Answer;
                                    for (var j = 0; j < answers.length;
                                         j++) {
                                        elem.append($('<option />', {
                                            value: answers[j].AnswerID,
                                            text: answers[j].AnswerText
                                        }));
                                    }
                                };
                                // loop through items
                                for (var i = 0; i < data.length; i++) {
                                    var val = data[i];
                                    if (val.QuestionID == 5) {
                                        appendOptions(page.find('#industry'), val);
                                    } else if (val.QuestionID == 6) {
                                        appendOptions(page.find('#job'), val);
                                    } else if (val.QuestionID == 8) {
                                        appendOptions(page.find('#annualIncome'), val);
                                    } else if (val.QuestionID == 9) {
                                        appendOptions(page.find('#occupation'), val);
                                    } else if (val.QuestionID == 10) {
                                        appendOptions(page.find('#studyArea'), val);
                                    } else if (val.QuestionID == 11) {
                                        appendOptions(page.find('#education'), val);
                                    }
                                    // check if mandatory
                                    var elm = page.find('#profileQuestionID_' +
                                                val.QuestionID);
                                    var show = (val.Mandatory == "true");
                                    elm.find('.asterisk').toggle(show);
                                }
                                page.find("#occupation").trigger('change');
                        });
                        return questions;
                    },
                    // send request to get user account Info
                    load: function (page) {
                        if (page == undefined) {
                            page = $('[data-role="page"]');
                        }
                        // only continue if logged in
                        if (!$.hw.auth.isLoggedIn()) {
                            /*if (window.console) {
                                //console.log("Please login first.");
                            }*/
                            return;
                        }
                        var user = $.hw.api.user;
                        var getInfo = $.hw.api.user.getProfileQuestion;

                        var userGetInfo = user.getInfo();
                        var userGetBirthday = getInfo(4);
                        var userGender = getInfo(2);
                        var userGetMarital = getInfo(7);
                        var userGetOccupation = getInfo(9);
                        var userGetOccType = getInfo(16);
                        var userGetStudyArea = getInfo(10);
                        var userGetIndustry = getInfo(5);
                        var userGetJob = getInfo(6);
                        var userGetManPost = getInfo(19);
                        var userGetIncome = getInfo(8);
                        var userGetEducation = getInfo(11);

                        var promises = [userGetInfo, userGetBirthday,
                                        userGender, userGetMarital,
                                        userGetOccupation, userGetOccType,
                                        userGetStudyArea, userGetIndustry,
                                        userGetJob, userGetManPost,
                                        userGetIncome, userGetEducation];

                        // load user info
                        userGetInfo.then(function (res) {
                            var data = res.Body.UserGetInfoResponse;
                            data = data.UserGetInfoResult;
                            page.find('#lang').val(data.languageID);
                            page.find('#username').val(data.username);
                            page.find('#password').val('');
                            page.find('#email').val(data.email);
                            page.find('#altEmail').val(data.alternateEmail);
                            page.find('#acceptPolicy').prop('checked', true);
                        });
                        // load birthday
                        userGetBirthday.then(function (res) {
                            var date = res.Body.UserGetProfileQuestionResponse;
                            date = date.UserGetProfileQuestionResult;
                            date = date.split("-");
                            page.find('#year').val(date[0]);
                            page.find('#month').val(date[1]);
                            page.find('#day').val(date[2]);
                        });
                        // gender
                        userGender.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#gender_M").prop("checked", data == 1);
                            page.find("#gender_F").prop("checked", data == 2);
                        });
                        // marital status
                        userGetMarital.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#status_S").prop("checked", data == 370);
                            page.find("#status_M").prop("checked", data == 369);
                        });
                        // occupation
                        userGetOccupation.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#occupation").val(data).trigger('change');
                        });
                        // occupation type
                        userGetOccType.then(function (res) {
                            var val = res.Body.UserGetProfileQuestionResponse;
                            val = val.UserGetProfileQuestionResult;
                            page.find("#occupationType_F").prop("checked", val == 405);
                            page.find("#occupationType_P").prop("checked", val == 406);
                        });
                        // studyArea
                        userGetStudyArea.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#studyArea").val(data);
                        });
                        // industry
                        userGetIndustry.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#industry").val(data);
                        });
                        // job
                        userGetJob.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#job").val(data);
                        });
                        // managerial post
                        userGetManPost.then(function (res) {
                            var val = res.Body.UserGetProfileQuestionResponse;
                            val = val.UserGetProfileQuestionResult;
                            page.find("#mngr_Y").prop("checked", val == 412);
                            page.find("#mngr_N").prop("checked", val == 413);
                        });
                        // income
                        userGetIncome.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#annualIncome").val(data);
                        });
                        // education
                        userGetEducation.then(function (res) {
                            var data = res.Body.UserGetProfileQuestionResponse;
                            data = data.UserGetProfileQuestionResult;
                            page.find("#education").val(data);
                        });
                        return promises;
                    },
                    // send request to update an account
                    update: function (page) {
                        if (page == undefined) {
                            page = $('[data-role="page"]');
                        }
                        // only continue if logged in
                        if (!$.hw.auth.isLoggedIn()) {
                            /*if (window.console) {
                                //console.log("Please login first.");
                            }*/
                            return;
                        }
                        var user = $.hw.api.user;
                        // update language
                        var oldLang = $.hw.config.lang;
                        var lang = page.find('#lang').val();
                        var resp_error = $('#instructions .text.error')
                        user.updateLanguage(lang).then(function () {
                            $.hw.config.lang = lang;
                            $.hw.auth.remember();
                        });
                        // save user info
                        var resp = user.updateInfo(
                            page.find('#username').val(),
                            page.find('#email').val(),
                            page.find('#altEmail').val()
                        );
                        resp.then(function(data){
                            var data_status = $.isEmptyObject(data)
                            if(!data_status){
                                var passwd = page.find('#password').val();
                                if (passwd.length > 0) {
                                    resp = user.updatePassword(passwd);
                                    resp.then(function(data){
                                        data_status = $.isEmptyObject(data)
                                        if(!data_status){
                                            resp_error.show();
                                        }
                                        updateProfile()
                                    })
                                } else {
                                    updateProfile()
                                }
                            } else {
                                resp_error.show();
                            }
                        })
                        function updateProfile(){
                            var req = $.hw.mobile.account.updateProfile(page);
                            req.then(function (resp) {
                                // switch to welcome page
                                var data_status = $.isEmptyObject(resp[0])
                                if(!data_status){
                                    if ($.hw.config.device == "ios") {
                                        window.location = "easyapp://success:"+
                                                          lang;
                                    } else {
                                        //reloading to my health to clear cached
                                        //translations.
                                        var base_url = $.hw.config.baseURL
                                        if (oldLang != lang) {
                                            location.href = base_url +
                                                    '/my_health/index.html';
                                        } else {
                                            $.mobile.changePage(base_url +
                                                    '/my_health/index.html');
                                        }
                                        
                                    }

                                } else {
                                    resp_error.show()
                                }
                            });
                        }
                        
                    },
                    // send request to create an account
                    create: function (page) {
                        var data = {
                            username: page.find('#username').val(),
                            password: page.find('#password').val(),
                            email: page.find('#email').val(),
                            alternateEmail: page.find('#altEmail').val(),
                            acceptPolicy: page.find('#acceptPolicy').prop('checked'),
                            languageID: page.find('#lang').val()
                        };
                        // create user
                        var create = $.hw.api.user.create(data);
                        create.then(function (resp) {
                            var body = resp.Body;
                            if (body.Fault) {
                                /*if (window.console) {
                                    //console.log(body.Fault.Reason.Text.text);
                                }*/
                                $('#instructions .text.error').show();
                                return;
                            }
                            var auth = body.UserCreateResponse;
                            auth = auth.UserCreateResult;
                            if (auth.token !== undefined) {
                                /*if (window.console) {
                                    //console.log("User created! Saving profiles.");
                                }*/
                                var req = $.hw.mobile.account.updateProfile(page);
                                req.then(function () {
                                    // switch to welcome page
                                    if ($.hw.config.device == "ios") {
                                        window.location = "easyapp://success";
                                    } else {
                                        $.mobile.changePage('./welcome.html');
                                    }
                                });
                            } else {
                                $('#instructions .text.error').show();
                                /*if (window.console) {
                                    //console.log("Error creating user");
                                }*/
                            }
                        });
                    }
                }
            },
            api: {
                init: function(){
                    var reminderData = $.hw.api.reminder.data;
                    var defArray = ['dummy1', 'dummy2', 'dummy3', 'dummy4'];
                    $.hw.api.reminder.defData = $.extend({}, reminderData);
                    return defArray;
                },
                // api helper functions
                ajaxCall: function(data, scall, ecall) {
                    var blankFunc = function () {};
                    scall = scall !== undefined ? scall : blankFunc;
                    ecall = ecall !== undefined ? ecall : blankFunc;
                    return $.ajax({
                        type: "POST",
                        url: $.hw.config. webserviceURL,
                        dataType: "xml",
                        data: data,
                        success: scall,
                        error: ecall
                    });
                },
                createSOAPRequest: function(root, data) {
                    var rqStr = "<" + root + " xmlns=\"" +
                                $.hw.config.healthWatchNS + "\">";
                    rqStr += objToXMLStr(data);
                    rqStr += "</" + root + ">";
                    return rqStr;

                    function objToXMLStr(data) {
                        return $.map(data, function (v, k) {
                            var str = '<' + k + '>';
                            if ($.isArray(v)) {
                                v.forEach(function (val, i) {
                                    str += parseValue(val);  
                                });
                            } else {
                                str += parseValue(v);
                            }
                            str += '</' + k + '>';
                            return str;
                        }).join("");
                    }

                    function parseValue (value) {
                        if ($.isPlainObject(value)) {
                            return objToXMLStr(value);
                        } else {
                            return value;
                        }
                    }
                },

                sendRequest: function(obj, data, scall, ecall) {
                    var dfd = jQuery.Deferred();
                    var name = obj.name;
                    var exceptionList = ["UserSetMeasureParameterized"]
                    var reqString = $.hw.api.createSOAPRequest(name, data);
                    // send request
                    var ajax = $.hw.api.ajaxCall({
                        url: $.hw.config.healthWatchURL,
                        data: reqString,
                        scall: scall,
                        ecall: ecall
                    });
                    ajax.then(function(response){
                        var xml = $.xml2json(response);
                        var result = [];
                        //Set xml.Body to soap:Body for IE.
                        if (xml.Body === undefined &&
                                xml['soap:Body'] !== undefined) {
                            xml.Body = xml['soap:Body'];
                        }

                        try {
                            result = xml.Body[name + "Response"];
                            result = result[name + "Result"];
                        } catch (err) {
                            result = xml.Body;
                        }
                        dfd.resolve(xml, result);
                        // update token expiration and logout
                        if (data.expirationMinutes && result !== undefined) {
                            var exp = data.expirationMinutes;
                            exp = (exp > 20)? 20 : exp;
                            var expiration = moment();
                            expiration = expiration.add('minutes', exp).toDate();
                            $.cookie("authTokenExpire", expiration,
                                     $.hw.config.cookieOptions);
                        } else if (result === undefined) {
                            if(exceptionList.indexOf(name) == -1){
                                $.hw.auth.forceLogout();
                            }
                        }
                    });
                    return dfd.promise();
                },
                // api classes
                userExtendToken: {
                    name: "UserExtendToken",
                    data: {
                        expirationMinutes: 20
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, {
                            token: $.hw.auth.token
                        });
                        token: (function () { return $.hw.auth.token; })
                        data = $.extend(data, $.hw.api.userExtendToken.data);
                        return $.hw.api.sendRequest($.hw.api.userExtendToken,
                                                    data, scall, ecall);
                    }
                },
                wordsOfWisdom: {
                    name: "TodaysWordsOfWisdom",
                    data: {
                        languageID: 2
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, $.hw.api.wordsOfWisdom.data);
                        return $.hw.api.sendRequest($.hw.api.wordsOfWisdom,
                                                    data, scall, ecall);
                    }
                },
                newsCategories: {
                    name: "NewsCategories",
                    data: {
                        lastXdays: 0,
                        languageID: 2,
                        includeEnglishNews: true
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, $.hw.api.newsCategories.data);
                        return $.hw.api.sendRequest($.hw.api.newsCategories,
                                                    data, scall, ecall);
                    }
                },
                newsEnum: {
                    name: "NewsEnum",
                    data : {
                        lastXdays: 0,
                        startOffset: 0,
                        topX: 50,
                        languageID: 2,
                        includeEnglishNews: true,
                        newsCategoryID: 0
                    },
                    getSwedishNews: function(data){
                        $.hw.api.newsEnum.data.languageID = 1;
                        $.hw.api.newsEnum.data.includeEnglishNews = false;
                        data = $.extend(data, $.hw.api.newsEnum.data);
                        return $.hw.api.newsEnum.sendRequest(data);
                    },
                    getInternationalNews: function(data){
                        $.hw.api.newsEnum.data.languageID = 2;
                        $.hw.api.newsEnum.data.includeEnglishNews = true;
                        data = $.extend(data, $.hw.api.newsEnum.data);
                        return $.hw.api.newsEnum.sendRequest(data);
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, $.hw.api.newsEnum.data);
                        return $.hw.api.sendRequest($.hw.api.newsEnum, data,
                                                    scall, ecall);
                    },
                    renderList: function (objToAppend, data,
                                          template, filterID) {
                        filterID = filterID !== undefined ? filterID : -1;
                        var format = "MMM DD, YYYY";
                        var fm = $.hw.utils.formatDate;
                        for (var i=0; i < data.length; i++) {
                            var v = data[i];
                            var tempData = {
                                headline: v.headline,
                                category: v.newsCategory,
                                date: fm(v.DT, format),
                                id: i
                            };
                            if($.hw.config.deviceMode){
                                tempData["device"] = true;
                            }
                            if(filterID != -1) {
                                if(v.newsCategoryID == filterID) {
                                    var l = Mustache.render(template, tempData);
                                    objToAppend.append(l);
                                }
                            } else {
                                var l = Mustache.render(template, tempData);
                                objToAppend.append(l);
                            }
                        }
                    }
                },
                exerciseAreaEnum: {
                    name: "ExerciseAreaEnum",
                    data: {
                        type: 0
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, {
                            token: $.hw.auth.token,
                            languageID: $.hw.config.lang
                        }, $.hw.api.exerciseAreaEnum.data);
                        return $.hw.api.sendRequest($.hw.api.exerciseAreaEnum,
                                                    data, scall, ecall);
                    }
                },
                exerciseEnum: {
                    name: "ExerciseEnum",
                    data: {
                        exerciseAreaID: 0,
                        type: 0,
                        expirationMinutes: 20
                    },
                    getAllExercises: function(token){
                        var data = {
                            exerciseAreaID: 0,
                            type: 0,
                            expirationMinutes: 20
                        };
                        return $.hw.api.exerciseEnum.sendRequest(data, 
                                                            $.hw.auth.token);
                    },
                    sendRequest: function(data, token, scall, ecall) {
                        data = $.extend(data, {
                            token: $.hw.auth.token,
                            languageID: $.hw.config.lang
                        }, $.hw.api.exerciseEnum.data);
                        return $.hw.api.sendRequest($.hw.api.exerciseEnum,
                                                    data, scall, ecall);
                    },
                    loadExcList: function (data, objToAppend, template){
                        objToAppend.empty();
                        for(var i=0; i < data.length; i++ ){
                            v = data[i];
                            var variantTemp = v.exerciseVariant.ExerciseVariant;
                            var variantID = variantTemp.exerciseVariantLangID;

                            var params = {
                                name: v.exercise,
                                area: v.exerciseArea,
                                time: v.exerciseTime,
                                id: variantID
                            };
                            if(v.exerciseImage){
                                params = $.extend(params,
                                        {"thumbnail": v.exerciseImage} );
                            }
                            if($.hw.config.deviceMode){
                                params["device"] = true;
                            }
                            var l = Mustache.render(template, params);
                            objToAppend.append(l);
                            }
                    },
                    sortShowExercises: function(data, property, order){
                        function sortBy(prop){
                            return function(a, b){
                                if(b[prop] == a[prop]) {
                                    return 0;
                                } else {
                                    if(order == 'asc') {
                                        num = a[prop] > b[prop] ? 1 : -1;
                                    } else {
                                        num = a[prop] < b[prop] ? 1 : -1;
                                    }
                                    return num;
                                }
                            };
                        }
                        return data.sort(sortBy(property));
                    },
                    emptyInstructions: function (){
                        var x = $("body").find("[data-pageKey='instructions']");
                        var insContent = x.find("#content");
                        insContent.html("");
                    }
                },
                exerciseExec: {
                    name: "ExerciseExec",
                    data: {
                        exerciseVariantLangID: 0,
                        expirationMinutes: 20
                    },
                    sendRequest: function(data, scall, ecall) {
                        data = $.extend(data, {
                            token: $.hw.auth.token
                        }, $.hw.api.exerciseExec.data);
                        return $.hw.api.sendRequest($.hw.api.exerciseExec,
                                                    data, scall, ecall);
                    },
                    createRenderedTemplate: function (data, template) {
                        var parsedContent = unescape(data.exerciseContent);
                        parsedContent = parsedContent.replace(/IMG SRC="/gi, 
                                        'IMG SRC="https://healthwatch.se');
                        var toBeRendered = Mustache.render(template, {
                            header: data.exerciseHeader,
                            area: data.exerciseArea,
                            time: data.exerciseTime,
                            content: parsedContent
                        });
                        return toBeRendered;
                    }
                },
                reminder: {
                        data: {
                            type: 0,
                            autoLoginLink: 0,
                            sendAtHour: 6,
                            regularity: 0,
                            regularityDailyMonday: false,
                            regularityDailyTuesday: false,
                            regularityDailyWednesday: false,
                            regularityDailyThursday: false,
                            regularityDailyFriday: false,
                            regularityDailySaturday: false,
                            regularityDailySunday: false,
                            regularityWeeklyDay: 0,
                            regularityWeeklyEvery: 0,
                            regularityMonthlyWeekNr: 0,
                            regularityMonthlyDay: 0,
                            regularityMonthlyEvery: 0,
                            inactivityCount: 0,
                            inactivityPeriod: 0,
                            expirationMinutes: 20
                        },
                        defData: {},
                        setAsNever: function(){
                            var token = $.hw.auth.token;
                            var data = $.hw.api.reminder.data;
                            data = $.hw.api.reminder.defData;
                            return $.hw.api.reminder.sendRequest(data, token, {
                                name: "UserSetReminderParameterized"
                            });
                        },
                        setInactivity: function(params){
                            var token = $.hw.auth.token;
                            var data = $.hw.api.reminder.data;
                            var defData = $.hw.api.reminder.defData;
                            data = $.extend(defData, params, {type: 2});
                            return $.hw.api.reminder.sendRequest(data, token, {
                                name: "UserSetReminderParameterized"
                            });
                        },
                        setRegularly: function(params){
                            var token = $.hw.auth.token;
                            var data = $.hw.api.reminder.data;
                            var defData = $.hw.api.reminder.defData;
                            data = $.extend(defData, params, {type: 1});
                            return $.hw.api.reminder.sendRequest(data, token, {
                                name: "UserSetReminderParameterized"
                            });
                        },
                        getReminder: function(){
                            var token = $.hw.auth.token;
                            var exp = $.hw.config.expirationMinutes;
                            var data = $.extend({}, {
                                token: token,
                                expirationMinutes: exp
                            });
                            return $.hw.api.reminder.sendRequest(data, token, {
                                name: "UserGetReminder"
                            });
                        },
                        sendRequest: function(data, token, name) {
                            data = $.extend(data, {token: token});
                            return $.hw.api.sendRequest(name, data);
                        }
                },
                user: {
                    expirationMinutes: 20,
                    login: function (user, pass, exp) {
                        var userObj = $.hw.api.user;
                        var data = $.extend({
                            expirationMinutes: userObj.expirationMinutes
                        }, {
                            username: user,
                            password: pass,
                            expirationMinutes: exp
                        });
                        var request = $.hw.api.sendRequest({
                            name: "UserLogin"
                        }, data);
                        request.then(function (resp) {
                            var resp_status = $.isEmptyObject(resp)
                            if(!resp_status){
                                $.cookie("username", data.username);
                                $.cookie("password", data.password);
                                var auth = resp.Body.UserLoginResponse;
                                auth = auth.UserLoginResult;
                                var exp_date = new Date(auth.tokenExpires)
                                if (auth.token !== undefined) {
                                    //Set Expiration from client time
                                    auth.tokenExpires = moment().add(
                                        'minutes',
                                        userObj.expirationMinutes
                                    );
                                    $.extend($.hw.auth, {
                                        token: auth.token,
                                        tokenExpires: exp_date
                                    });
                                    $.hw.config.lang = auth.languageID;
                                    // save login cookies
                                    $.hw.auth.remember({
                                        authToken: auth.token,
                                        authTokenExpire: auth.tokenExpires
                                    });
                                } else {
                                    $.hw.auth.token = undefined;
                                    $.hw.auth.tokenExpires = new Date();
                                }
                            } else {
                                $.hw.auth.token = undefined;
                                $.hw.auth.tokenExpires = new Date();
                            }
                        });
                        return request;
                    },
                    logout: function (redirect) {
                        var myHealth = $.hw.config.baseURL +
                                       "/account/login.html";
                        var request = $.hw.api.sendRequest({
                            name: "UserLogout"
                        }, {
                            token: $.hw.auth.token
                        });
                        $.hw.auth.userExtendPoll.stop();
                        request.then(function () {
                            $.hw.auth.token = undefined;
                            $.hw.auth.tokenExpires = new Date();
                            if (redirect) {
                                $('body').hide();
                                location.href = myHealth;
                            }
                            $.hw.auth.forget();
                        });
                        return request;
                    },
                    resetPassword: function (email) {
                        return $.hw.api.sendRequest({
                            name: "UserResetPassword"
                        }, {
                            email: email,
                            languageID: $.hw.config.lang
                        });
                    },
                    getInfo: function (token, expiration) {
                        token = token || $.hw.auth.token;
                        var mins = $.hw.api.user.expirationMinutes;
                        expiration = expiration || mins;
                        return $.hw.api.sendRequest({
                                name: "UserGetInfo"
                            }, {
                                token: token,
                                expirationMinutes: expiration
                        });
                    },
                    updateInfo: function (username, email, alternateEmail,
                                          token, expiration) {
                        token = token || $.hw.auth.token;
                        var mins = $.hw.api.user.expirationMinutes;
                        expiration = expiration || mins;
                        return $.hw.api.sendRequest({
                                name: "UserUpdateInfo"
                            }, {
                                username: username,
                                email: email,
                                alternateEmail: alternateEmail,
                                token: token,
                                expirationMinutes: expiration
                        });
                    },
                    updateLanguage: function (languageID, token, expiration) {
                        token = token || $.hw.auth.token;
                        var mins = $.hw.api.user.expirationMinutes;
                        expiration = expiration || mins;
                        return $.hw.api.sendRequest({
                                name: "UserUpdateLanguage"
                            }, {
                                languageID: languageID,
                                token: token,
                                expirationMinutes: expiration
                        });
                    },
                    updatePassword: function (password, token, expiration) {
                        token = token || $.hw.auth.token;
                        var mins = $.hw.api.user.expirationMinutes;
                        expiration = expiration || mins;
                        return $.hw.api.sendRequest({
                                name: "UserUpdatePassword"
                            }, {
                                password: password,
                                token: token,
                                expirationMinutes: expiration
                        });
                    },
                    create: function (data) {
                        data = $.extend({
                            username: "",
                            password: "",
                            email: "",
                            alternateEmail: "",
                            acceptPolicy: "false",
                            languageID: $.hw.config.lang,
                            sponsorID: 0,
                            departmentID: 0,
                            expirationMinutes: 20
                        }, data);
                        var request = $.hw.api.sendRequest({
                            name: "UserCreate"
                        }, data);
                        request.then(function (resp) {
                            var body = resp.Body;
                            if (body.Fault) {
                                /*if (window.console) {
                                    //console.log(body.Fault.Reason.Text.text);
                                }*/
                                return;
                            }
                            var auth = body.UserCreateResponse;
                            auth = auth.UserCreateResult;
                            if (auth.token !== undefined) {
                                var expire = moment();
                                expire = expire.add('minutes', 20).toDate();
                                $.extend($.hw.auth, {
                                    token: auth.token,
                                    tokenExpires: expire
                                });
                                $.hw.config.lang = auth.languageID;
                                // save login cookies
                                $.hw.auth.remember({
                                    authToken: auth.token,
                                    authTokenExpire: auth.tokenExpires
                                });
                            } else {
                                $.hw.auth.token = undefined;
                                $.hw.auth.tokenExpires = new Date();
                            }
                        });
                        return request;
                    },
                    // function to get all profile questions
                    getProfileQuestions: function () {
                        return $.hw.api.sendRequest({
                                name: "ProfileQuestions"
                            }, {
                                languageID: $.hw.config.lang,
                                sponsorID: 0
                            }
                        );
                    },
                    // function to update profile
                    setProfileQuestion: function (questionID, answer, token,
                                                  expiration) {
                        token = token || $.hw.auth.token;
                        expiration = expiration || 60;
                        return $.hw.api.sendRequest({
                                name: "UserSetProfileQuestion"
                            }, {
                                questionID: questionID,
                                answer: answer,
                                token: token,
                                expirationMinutes: expiration
                            }
                        );
                    },
                    // function to load profile
                    getProfileQuestion: function (questionID, token, exp) {
                        token = token || $.hw.auth.token;
                        exp = exp || 60;
                        return $.hw.api.sendRequest({
                                name: "UserGetProfileQuestion"
                            }, {
                                questionID: questionID,
                                token: token,
                                expirationMinutes: exp
                            }
                        );
                    }
                },
                calendar: {
                    calendarEvents: function (from, to, token, exp) {
                        token = token || $.hw.auth.token;
                        exp = exp || 20;
                        return $.hw.api.sendRequest({
                                name: "CalendarEnum"
                            }, {
                                fromDT: from,
                                toDT: to,
                                token: token,
                                languageID: $.hw.config.lang,
                                expirationMinutes: exp
                            }
                        );
                    },
                    calendarUpdate: function (mood, note, date, token, exp) {
                        token = token || $.hw.auth.token;
                        note = $.hw.utils.htmlEncode(note)
                        exp = exp || 20;
                        return $.hw.api.sendRequest({
                                name: "CalendarUpdate"
                            }, {
                                mood: mood,
                                note: note,
                                date: date,
                                token: token,
                                expirationMinutes: exp
                            }
                        );
                    },
                    measureCategoryEnum: function (typeID, token, exp) {
                        typeID = typeID || 0;
                        token = token || $.hw.auth.token;
                        exp = exp || 60;
                        return $.hw.api.sendRequest({
                                name: "MeasureCategoryEnum"
                            }, {
                                measureTypeID: typeID,
                                token: token,
                                languageID: $.hw.config.lang,
                                expirationMinutes: exp
                            }
                        );
                    },
                    measureEnum: function (categoryID, token, exp) {
                        token = token || $.hw.auth.token;
                        exp = exp || 60;
                        return $.hw.api.sendRequest({
                                name: "MeasureEnum"
                            }, {
                                measureCategoryID: categoryID,
                                token: token,
                                languageID: $.hw.config.lang,
                                expirationMinutes: exp
                            }
                        );
                    },
                    setMeasure: function (data, token, expiration) {
                        token = token || $.hw.auth.token;
                        expiration = expiration || 60;
                        var request = $.hw.api.sendRequest({
                            name: "UserSetMeasureParameterized"
                        }, $.extend({
                                token: token,
                                expirationMinutes: expiration
                            }, data)
                        );
                        return request;
                    },
                    deleteMeasure: function (eventID, token, expiration) {
                        token = token || $.hw.auth.token;
                        expiration = expiration || 60;
                        var request = $.hw.api.sendRequest({
                            name: "UserDeleteMeasure"
                        }, {
                            eventID: eventID,
                            token: token,
                            expirationMinutes: expiration
                        });
                        return request;
                    },
                    deleteForm: function (eventID, key, token, expiration) {
                        token = token || $.hw.auth.token;
                        expiration = expiration || 60;
                        var request = $.hw.api.sendRequest({
                            name: "UserDeleteFormInstance"
                        }, {
                            eventID: eventID,
                            formInstanceKey: key,
                            token: token,
                            expirationMinutes: expiration
                        });
                        return request;
                    }
                },
                form: {
                    formKey: function (data) {
                        var name = {name: "FormEnum"};
                        data = $.extend(data, {
                                    token: $.hw.auth.token,
                                    expirationMinutes: 20
                                });
                        return $.hw.api.sendRequest(name, data);
                    },
                    formQuestion: function (data) {
                        var name = {name: "FormQuestionEnum"};
                        data = $.extend(data, {
                                    token: $.hw.auth.token,
                                    expirationMinutes: 20,
                                    languageID: $.hw.config.lang
                                });
                        return $.hw.api.sendRequest(name, data);
                    },
                    saveAnswer: function (data) {
                        var name = {name: "UserSetFormInstance"};
                        data = $.extend(data, {
                                    token: $.hw.auth.token,
                                    expirationMinutes: 20
                                });
                        return $.hw.api.sendRequest(name, data);
                    }
                },
                statistics: {
                    getForm: function (data) {
                        var name = {name: "UserGetFormInstanceFeedback"};
                        data = $.extend({
                            token: $.hw.auth.token,
                            languageID: $.hw.config.lang,
                            expirationMinutes: 20
                        }, data);
                        return $.hw.api.sendRequest(name, data);
                    },
                    feedbackTemplateEnum: function (data) {
                        var name = {name: "FormFeedbackTemplateEnum"};
                        data = $.extend(data, {
                                    token: $.hw.auth.token,
                                    languageID: $.hw.config.lang,
                                    expirationMinutes: 20
                                });
                        return $.hw.api.sendRequest(name, data);
                    },
                    graphData: function (data){
                        var name = {name: "UserGetFormFeedback"};
                        data = $.extend(data, { 
                                    token: $.hw.auth.token,
                                    languageID: $.hw.config.lang,
                                    expirationMinutes: 20
                                });
                        return $.hw.api.sendRequest(name, data);
                    }
                }
            },
            init: function () {
                // handle showStartPageOnStart
                var show = parseInt($.cookie("noStartPage"));
                if (!isNaN(show)) {
                    $.hw.config.showStartPageOnStart = false;
                }

                if ($.hw.currentTab === undefined) {
                    $.hw.currentTab = '.my_health';
                }

                // preload some images
                var imgs = ['/images/list_icon.png',
                            '/images/btn_icon_settings@2x.png'];
                $.each(imgs, function preloadImage(k, url) {
                    var img = new Image();
                    img.src = $.hw.config.baseURL + url;
                });

                // check login cookies
                var token = $.cookie("authToken");
                var lang = $.cookie("authLang");
                var expire = new Date($.cookie("authTokenExpire"));
                if (token !== undefined && token !== null &&
                    expire > new Date()) {
                    $.hw.auth.token = token;
                    $.hw.auth.tokenExpires = expire;
                    $.hw.config.lang = lang;
                }
                // handle language
                var params = $.hw.utils.getQueryParams();
                if (params.lang !== undefined) {
                    $.hw.config.lang = parseInt(params.lang);
                }
                // check if device mode
                $.hw.config.device = params.device;
                $.hw.config.deviceMode = (params.deviceMode !== undefined ||
                                          $.hw.config.device !== undefined);
                if ($.hw.config.device) {
                    $('body').addClass('device_' + $.hw.config.device);
                }
                $("body").on("click", ".tabbar_link", function(){
                    var target = $(this).parent();
                    var path = $(target).attr("class");
                    if(!$.hw.auth.isLoggedIn() && path == "my_health"){
                        $.mobile.changePage($.hw.config.baseURL +
                                                "/account/login.html");
                    } else {
                        $.mobile.changePage($.hw.config.baseURL + "/" + path);
                    }
                }).on("click", "#my_health_btn", function () {
                    $.mobile.changePage($.hw.config.baseURL +
                                        "/my_health/index.html");
                });

                // fix for ios position: fixed + virtual keyboard bug
                $('body').on('focus', 'input', function(){
                    $('.ui-header, .ui-footer').css({position:'absolute'});
                });
                $('body').on('blur', 'input', function(){
                    $('.ui-header, .ui-footer').css({position:'fixed'});
                });

                // get token if set in the params
                $.hw.auth.token = params.token || $.hw.auth.token;
                //$.mobile.loading("show");
                $.hw.utils.loader.show();
                var trans = $.hw.i18n.getAllTranslations();
                var defArray = $.hw.api.init();
                $.when.apply(this, [defArray, trans]).then(function(){
                    // TODO;
                    $.hw.utils.loader.hide();
                    $('body').show();
                });
                return trans;
            },
            i18n: {
                data: {},
                getAllTranslations: function (){
                    var url = $.hw.config.piqliq.URL;
                    var piqliq = $.hw.config.piqliq;
                    var dfd = jQuery.Deferred();
                    $.support.cors = true;
                    $.ajax({
                            url: url,
                            headers: { 
                                Accept : "application/json"
                            },
                            data: {
                                user_token: piqliq.userToken,
                                sublevel: piqliq.sublevel,
                                media_type: "json"
                            },
                            dataType: "json"
                        }).then(function (resp) {
                            var pData = resp;
                            pData = $.hw.i18n.parseTranslation(pData);
                            $.hw.i18n.data = $.extend($.hw.i18n.data, pData);
                            dfd.resolve();
                        }).fail(function (resp, a, b) {
                            dfd.reject;
                        });
                    return dfd;
                },
                parseTranslation: function(data){
                    function parseContainer(key, dataObject) {
                        $.hw.i18n.data[key] = dataObject.settings;
                        $.each(dataObject.containers, parseContainer);
                    }
                    $.each(data, parseContainer);
                    $.each($.hw.i18n.data, function(i, v){
                        var newData = {};
                        $.each($.hw.i18n.data[i], function (j, val){
                            $.each(val.values, function(k, value){
                                var lk = $.hw.utils.getLangValue(k);
                                if(newData[lk] === undefined){
                                    newData[lk] = {};
                                }
                                var obj = {};
                                obj[j] = value;
                                $.extend(newData[lk], obj);
                            });
                        });
                        $.hw.i18n.data[i] = newData;
                    });
                },
                getSettings: function (data){
                    data = $.extend({}, data.settings);
                    return data;
                },
                getTranslation: function (pageKey) {
                    var data = $.hw.i18n.data[pageKey][$.hw.config.lang];
                    return data;
                }
            },
            setCurrentTab: function () {
                selected = $($.hw.currentTab);
                $('.tabbar .selected').removeClass('selected');
                selected.addClass('selected');
            },
            utils: {
                css: {
                    load: function (url) {
                        $('<link/>').attr({
                            'class': 'custom_css',
                            rel: 'stylesheet',
                            type: 'text/css',
                            href: url
                        }).appendTo('head');
                    },
                    clearCustom: function () {
                        $('link.custom_css').remove();
                    }
                },
                loader: {
                    show: function (){
                        var d = $("<div>", {
                            "class": "hw-loader"
                        });
                        d.css({
                            'background-color': "#FFFFFF",
                            'width': "100%",
                            'height': "1000",
                            'position': 'absolute',
                            'z-index': '1001'
                        });
                        $("body").prepend(d);
                    },
                    hide: function (){
                        $("body .hw-loader").remove();
                    }
                },
                toURL: function (path) {
                    return $.hw.config.baseURL + path;
                },
                formatDate:function(strDate, format){
                    var temp = moment(strDate);
                    return temp.format(format);
                },
                loadFunction: function(func){
                    $.mobile.loading("show");
                    func.then(function(){
                        $.mobile.loading("hide");
                    });
                },
                beforeChangePage: function (func) {
                    $(document).one("pagebeforeshow", "div[data-role='page']",
                                    func);
                },
                getQueryParams: function (url) {
                    url = url || document.location.search;
                    if (url.indexOf('#') != -1) {
                        url = url.substring(url.indexOf('#') + 1);
                    }
                    url = url.substring(url.indexOf('?'));
                    var func = function (qs) {
                        qs = qs.split("+").join(" ");

                        var params = {};
                        var tokens;
                        var re = /[?&]?([^=]+)=([^&]*)/g;

                        while (tokens = re.exec(qs)) {
                            var key = decodeURIComponent(tokens[1]);
                            params[key] = decodeURIComponent(tokens[2]);
                        }
                        return params;
                    };
                    return func(url);
                },
                fillDateDropdown: function (start, end, dropdown) {
                    dropdown = $(dropdown);
                    dropdown.children(':not(:disabled)').remove();
                    var val = '01';
                    for (var i = start; i <= end; i++) {
                        if (i < 10) {
                            val = '0' + i;
                        } else {
                            val = i;
                        }
                        var option = $('<option />', {value: val, text: val});
                        dropdown.append(option);
                    }
                },
                stringToBoolean: function(string){
                    switch(string.toLowerCase()){
                        case "true": case "yes": case "1": return true;
                        case "false": case "no": case "0": 
                        case null: return false;
                        default: return undefined;
                    }
                },
                isBool: function(string){
                    switch(string.toLowerCase()){
                        case "true": case "false": return true;
                        default: return false;
                    }
                },
                parseValues: function(data){
                    for (var key in data){
                        var v = data[key].toString();
                        if(data.hasOwnProperty(key)){
                            if($.isNumeric(v)){
                                data[key] = parseInt(v);
                                continue;
                            }
                            if($.hw.utils.isBool(v)){
                                data[key] = $.hw.utils.stringToBoolean(v);
                                continue;
                            }
                        }
                    }
                    return data;
                },
                selectTabbar: function(name){
                    $(".tabbar > li").removeClass("selected");
                    $(".tabbar ." + name).addClass("selected");
                },
                setDataInObjectArray: function (arr, name, value, nameToSet, data) {
                    for (i = 0; i < arr.length; i++) {
                        if (arr[i][name] == value) {
                            arr[i][nameToSet] = data;
                        }
                    }
                },
                inObjectArray: function (arr, name, value) {
                    for (i = 0; i < arr.length; i++) {
                        if (arr[i][name] == value) {
                            return true;
                        }
                    }
                },
                getLangValue: function(str){
                    var idLang = 0;
                    var langVals = $.hw.config.langs;
                    $.each(langVals, function (i, v){
                        if(v === str){
                            idLang = i;
                            return;
                        }
                    });
                    return idLang;
                },
                htmlEncode: function(value){
                    return $('<div/>').text(value).html();
                },
                htmlDecode: function(value){
                    return $('<div/>').html(value).text();
                }
            }
        }
    });
})(jQuery);

// configure jquery
$(document).ready(function () {
    // initialize library
    var initDef = $.hw.init();
    $.when(initDef).then(function(){
        $(document).on("pagebeforeshow", "div[data-role='page']",
                   function (event, prevPage) {
            var $this = $(this);
            var viewKey = $this.attr('data-pageKey');
            if(prevPage){
                var prevPage = $(prevPage.prevPage);
                var prevPageKey = $(prevPage).attr("data-pageKey");
                $.hw.pageFrom = prevPageKey;
            }
            // remove class
            var body = $('body');
            // hide header and footer if device mode
            if ($.hw.config.deviceMode) {
                $("div[data-role='header'], div[data-role='footer']").remove();
                $('body').addClass('deviceMode');
                $('body').find('.ui-content').css('padding', '0px');
            }
            // call before show function for page if present
            var pageScript = $.hw.pageScripts[viewKey];
            if (pageScript) {
                if (pageScript.limitAuthorized) {
                    if (!$.hw.auth.isLoggedIn()) {
                        $.hw.auth.limitAuthorized();
                        return false;
                    }
                }
                if ((pageScript.bound == 0 ||
                    pageScript.bound === undefined) &&
                    pageScript.init) {
                    pageScript.init($this, event);
                } else if (pageScript.show) {
                    if(viewKey == "home"){
                        $.mobile.changePage(prevPage.data().url);
                        event.preventDefault();
                        return false;
                    }
                    pageScript.show($this, event);
                }
            }
            var tabbar = $("[data-pagekey='" + viewKey + "'] .tabbar_name").val();
            $.hw.utils.selectTabbar(tabbar);
            // handle translations
            var defaultPageKey = "home";
            var langValues = {};
            try {
                var data = $.hw.i18n.data;
                var lang = $.hw.config.lang;
                langValues = $.extend({}, data[defaultPageKey][lang],
                                      data[viewKey][lang]);
                langValues = (langValues === undefined)? {} : langValues;
            } catch (err) {
                // translation not found
                /*if (window.console) {
                    //console.log("Translation data for " + viewKey +
                    //            " not found");
                }*/
            }
            // localize html title
            var title = $('head > title');
            title.text(Mustache.render(title.text(), langValues));
            // localize body
            $this.html(Mustache.render($this.html(), langValues));
            $(window).trigger('resize');
            body.trigger('afterTranslation');

        });
        // handle orientation changes
        $(window).bind('orientationchange',function (event) {
            var body = $('body');
            var portrait = "portrait";
            var landscape = "landscape";
            if (event.orientation == landscape) {
                body.removeClass(portrait).addClass(landscape);
            } else {
                body.removeClass(landscape).addClass(portrait);
            }
        }).trigger('orientationchange');

        //Trigger pagebeforeshow after initialize
        try {
            $.mobile.activePage.trigger('pagebeforeshow');
        } catch (err) {
            /*if (window.console) {
                //console.log(err.type);
            }*/
        }
    });
});

//News Init and Show
$.hw.pageScripts['news']["init"] = function(page){
    $.hw.pageScripts['news']['bound'] = 1;
    $.hw.cache["news"] = {};

    var template;
    var data = [];
    var main_url = $.hw.config.baseURL;
    var temp = $.ajax({url: (main_url) + "/news/news-list-template.html"});
    var newsResult = $.hw.api.newsEnum.getInternationalNews();
    var newsResultSE = $.hw.api.newsEnum.getSwedishNews();
    var summaryTemplate = $.ajax({url: ($.hw.config.baseURL) +
                        "/news/summary-template.html"});
    $.hw.utils.loadFunction(newsResult);
    temp.then(function(data){
        $.hw.cache["news"]["newsListTemp"] = data;
    });
    newsResult.then(function(resp){
        var temp = resp.Body.NewsEnumResponse.NewsEnumResult.News;
        if(temp.length > 0){
            data = temp;
        } else{
            data.push(temp);
        }
        $.hw.cache["news"]["newsData"] = data;
    });

    newsResultSE.then(function(resp){
        var temp = resp.Body.NewsEnumResponse.NewsEnumResult.News;
        if(temp.length > 0){
            data = temp;
        } else{
            data.push(temp);
        }
        $.hw.cache["news"]["newsDataSe"] = data;
    });

    summaryTemplate.then(function(data){ 
        $.hw.cache.news["summaryTemplate"] = data;
    });
    $.when(temp, newsResult, newsResultSE, summaryTemplate).then(function() {
        var sumObj = {};
        var limit = 50;
        var newsPage = $("body").find("[data-pageKey='news']");
        var sumPage = $("body").find("[data-pageKey='summary']");
        var template = $.hw.cache["news"]["newsListTemp"];
        var data;
        newsPage.find(".news-listview").empty();
        var renderFunc = $.hw.api.newsEnum.renderList;
        if($.hw.config.deviceMode) {
            var params = $.hw.utils.getQueryParams();
            $.hw.cache.news.selectedCategory = params.id;
            $.hw.cache.news.selectedLanguage = params.lang;
            if(params.lang == 1){
                data = $.hw.cache["news"]["newsDataSe"];
            } else {
                data = $.hw.cache["news"]["newsData"];
            }
            renderFunc(newsPage.find(".news-listview"), data,
                                     template,  params.id);
        } else {
            $.hw.cache.news.selectedLanguage = $.hw.config.lang;
            if ($.hw.config.lang == 1) {
                data = $.hw.cache["news"]["newsDataSe"];
            } else {
                data = $.hw.cache["news"]["newsData"];
            }
            renderFunc(newsPage.find(".news-listview"), data, template);
        }
        newsPage.find(".news-listview").listview();

    });
    $("body").on("click", ".news-list", function(){
        var data;
        if($.hw.cache["news"]["selectedLanguage"] == 1){
            data = $.hw.cache["news"]["newsDataSe"];
        }
        else{
            data = $.hw.cache["news"]["newsData"];
        }
        var index = $(this).attr("data-list-id");
        $.hw.cache["news"]['selectedNews'] = data[index];
        if(!$.hw.config.deviceMode){
            $.mobile.changePage($.hw.config.baseURL + "/news/summary.html");
        }
        
    });
    $("body").on("click", ".cat_button", function(){
        $.hw.mobile.moveToCategories();
    });

};
$.hw.pageScripts['news']["show"] = function(page){
    var newsPage = $("body").find("[data-pageKey='news']");
    var sumPage = $("body").find("[data-pageKey='summary']");
    var template = $.hw.cache["news"]["newsListTemp"];
    var format = "MMM DD, YYYY";
    var fm = $.hw.utils.formatDate;
    if($.hw.cache["news"]["selectedCategory"]){
        var data;
        var category = $.hw.cache["news"]["selectedCategoryName"];

        if (category !== undefined || category !== '') {
            page.find('.ui-title').text(category);
        }

        if($.hw.cache["news"]["selectedLanguage"] == 1){
            data = $.hw.cache["news"]["newsDataSe"];
        }
        else{
            data = $.hw.cache["news"]["newsData"];
        }

        newsPage.find(".news-listview").empty();
        for (var i=0; i < data.length; i++){
            var v = data[i];
            var filterID = $.hw.cache["news"]["selectedCategory"];
            if(filterID != -1){
                if(v.newsCategoryID == filterID){
                    var l = Mustache.render(template, {
                                    headline: v.headline,
                                    category: v.newsCategory,
                                    date: fm(v.DT, format),
                                    id: i
                                });
                        newsPage.find(".news-listview").append(l);
                    }
            } else{
                var l = Mustache.render(template, {
                                    headline: v.headline,
                                    category: v.newsCategory,
                                    date: fm(v.DT, format),
                                    id: i
                                });
                        newsPage.find(".news-listview").append(l);
            }
        }
        newsPage.find(".news-listview").listview();
        newsPage.find(".news-listview").listview("refresh");
    }
};

//Category Init and show
$.hw.pageScripts['category']['init'] = function(page) {
    $.hw.pageScripts['category']['bound'] = 1;
    var catInt = $.hw.api.newsCategories.sendRequest();
    $.hw.api.newsCategories.data.languageID = 1;

    var catSwe = $.hw.api.newsCategories.sendRequest();
    var main_url = $.hw.config.baseURL;
    var catPage = $("body").find("[data-pageKey='category']");
    var catListView = catPage.find(".category-listview");
    if(!$.hw.config.deviceMode){
        $.hw.cache.news.selectedLanguage = $.hw.config.lang;
        if ($.hw.config.lang == 1) {
            swapCategory($(".category-filter-control li:last-child a"));
        } else {
            swapCategory($(".category-filter-control li:first-child a"));
        }
    } else{
        $.hw.cache.news = {};
    }
    $.hw.utils.loadFunction(catInt);
    var temp = $.ajax({url: (main_url) + "/news/category-listitem.html"});
    temp.then(function(data){
        $.hw.cache.news["categoryListTemp"] = data;
    });
    catInt.then(function(resp){
        var temp = resp.Body.NewsCategoriesResponse.NewsCategoriesResult.NewsCategory;
        if(temp.length > 0){
            $.hw.cache.news["categoryInter"] = temp;
        } else{
            var dummy = [];
            dummy.push(temp);
            $.hw.cache.news["categoryInter"] = dummy;
        }
        $(this).data("bound", true);
    });
    catSwe.then(function(resp){
        var temp = resp.Body.NewsCategoriesResponse.NewsCategoriesResult.NewsCategory;
        if(temp.length > 0){
            $.hw.cache.news["categorySwed"] = temp;
        } else{
            var dummy = [];
            dummy.push(temp);
            $.hw.cache.news["categorySwed"] = dummy;
        }
        $(this).data("bound", true);
    });

    $.when(catInt, catSwe, temp).then(function(){
        //catListView.empty();
        var intFilter = $(".category-filter-control li:first-child a");
        var sweFilter = $(".category-filter-control li:last-child a");
        var template = $.hw.cache.news["categoryListTemp"];
        var data = [];
        if($.hw.config.deviceMode){
            var params = $.hw.utils.getQueryParams();
            $.hw.cache["news"]["selectedLanguage"] = params.lang;
            var urlLang = $.hw.cache["news"]["selectedLanguage"];
            if(urlLang == 1){
                swapCategory(sweFilter);
                data = $.hw.cache.news["categorySwed"];
            } else {
                swapCategory(intFilter);
                data = $.hw.cache.news["categoryInter"];
            }
        } else {
            var lang = $.hw.cache["news"]["selectedLanguage"];
            if (lang == 1) {
                data = $.hw.cache.news["categorySwed"];
            } else {
                data = $.hw.cache.news["categoryInter"];
            }
        }

        $(".category-listview").empty();
        var li = $("<li>", {"data-icon": "false",
                            "data-list-id": -1,
                            "class": "category-list view-all-category"});
        var href = !($.hw.config.deviceMode)? '' : 'easyapp://category';

        var lang = $.hw.cache["news"]["selectedLanguage"];
        var translate = $.hw.i18n.data.news[lang];
        li.append($("<a />", {html: translate.view_all_news, href: href}));

        $(".category-listview").append(li);
        for (var i=0; i < data.length; i++){
            var v = data[i];
            var tempData = {
                name: v.newsCategory,
                thumbnail: v.newsCategoryImage,
                id: v.newsCategoryID
            };
            if($.hw.config.deviceMode){
                tempData["device"] = true;
            }
            var l = Mustache.render(template, tempData);
            $(".category-listview").append(l);
        }
        $(".category-listview").listview();
        $(".category-listview").listview("refresh");
        $("body").on('click', ".category-list", function(){
            var selectedId = $(this).attr("data-list-id");
            var category = $(this).find('.news-category').text();
            if (selectedId == -1) {
                var lang = $.hw.cache["news"]["selectedLanguage"];
                var translate = $.hw.i18n.data.news[lang];
                category = translate.news_title;
            }
            $.hw.cache["news"]["selectedCategory"] = selectedId;
            $.hw.cache["news"]["selectedCategoryName"] = category;
            if(!$.hw.config.deviceMode){
                $.mobile.changePage($("body").find("[data-pageKey='news']"));
            }
        });
        var cfunc = function (data){
            var template = $.hw.cache.news["categoryListTemp"];
            var catPage = $("body").find("[data-pageKey='category']");
            $(".category-listview").empty();
            var li = $("<li>", {"data-icon": "false",
                                "data-list-id": -1,
                                "class": "category-list view-all-category"});

            var href = !($.hw.config.deviceMode)? '' : 'easyapp://category';
            var lang = $.hw.cache["news"]["selectedLanguage"];
            var translate = $.hw.i18n.data.news[lang];
            li.append($("<a />", {html: translate.view_all_news, href: href}));

            $(".category-listview").append(li);
            for (var i=0; i < data.length; i++){
                var v = data[i];
                var l = Mustache.render(template, {
                    name: v.newsCategory,
                    thumbnail: v.newsCategoryImage,
                    id: v.newsCategoryID
                });
                catPage.find(".category-listview").append(l);
            }
            $(".category-listview").listview();
            $(".category-listview").listview("refresh");
        };
        $.hw.pageScripts.news.category = {};
        $.hw.pageScripts.news.category["refreshList"] = cfunc;
    });
    function swapCategory(that){
        var categoryLi = $(".category-filter-control li");
        var activeButton = categoryLi.find(".ui-btn-active");
        activeButton.removeClass("ui-btn-active");
        that.addClass("ui-btn-active");
    }
    $("body").on("click touchend", ".category-filter-control li", function(){
        var child = $(this).find("a");
        var activeButton = $(".category-filter-control li").find(".ui-btn-active");
        var catSwed = $.hw.cache.news.categorySwed;
        var catInter = $.hw.cache.news.categoryInter;
        if (child != activeButton){
            swapCategory(child);
            if($(this).hasClass("international")){
                $.hw.cache["news"]["selectedLanguage"] = 2;
                $.hw.pageScripts.news.category["refreshList"](catInter);
            } else{
                $.hw.cache["news"]["selectedLanguage"] = 1;
                $.hw.pageScripts.news.category["refreshList"](catSwed);
            }
        }
    });
};
$.hw.pageScripts['category']['show'] =  function(){
    var template = $.hw.cache.news["categoryListTemp"];
    var catPage = $("body").find("[data-pageKey='category']");
    checkSelectedCatLang($.hw.cache.news.selectedLanguage);
    $(".category-listview").listview();
    $(".category-listview").listview("refresh");
    $("body").on('click', ".category-list", function(){
        var id = $(this).attr("data-list-id");
        $.hw.cache["news"]["selectedCategory"] = id;
        if(!$.hw.config.deviceMode){
            $.mobile.changePage($("body").find("[data-pageKey='news']"));
        }
        
    });
    function checkSelectedCatLang(lang){
        var catSwed = $.hw.cache.news.categorySwed;
        var catInter = $.hw.cache.news.categoryInter;
        var activeButton = $(".category-filter-control li").find(".ui-btn-active");
        activeButton.removeClass("ui-btn-active");
        $.each($(".category-filter-control li a"), function(i ,v){
            v = $(v);
            if(v.hasClass("swedish") && lang == 1){
                v.addClass("ui-btn-active");
                $.hw.pageScripts.news.category["refreshList"](catSwed);
            } else if(v.hasClass("international") && lang == 2) {
                v.addClass("ui-btn-active");
                $.hw.pageScripts.news.category["refreshList"](catInter);
            }
        });
    }
};

$.hw.pageScripts['summary']['init'] = function(page){
    var params = $.hw.utils.getQueryParams();
    var fm = $.hw.utils.formatDate;
    var format = "MMM DD, YYYY";
    var template = $.hw.cache.news["summaryTemplate"];
    var sumPage = $("body").find("[data-pageKey='summary']");
    var sumObj = $.hw.cache["news"]["selectedNews"];
    var content = sumPage.find("#content");
    var langvals = $.hw.i18n.data["summary"][$.hw.config.lang];
    var toBeRendered = Mustache.render(template, $.extend(langvals, {
        newsTitle: sumObj.headline,
        categoryIcon: sumObj.newsCategoryImage,
        newsDate: fm(sumObj.DT, format),
        newsContent: sumObj.body,
        newsArticleURL: sumObj.link
    }));
    content.html(toBeRendered);
};
$.hw.pageScripts['summary']['show'] = function(page){
    var fm = $.hw.utils.formatDate;
    var format = "MMM DD, YYYY";
    var sumPage = $("body").find("[data-pageKey='summary']");
    var template = $.hw.cache.news["summaryTemplate"];
    var sumObj = $.hw.cache["news"]["selectedNews"];
    var content = sumPage.find("#content");
    var langvals = $.hw.i18n.data["summary"][$.hw.config.lang];
    var toBeRendered = Mustache.render(template, $.extend(langvals, {
        newsTitle: sumObj.headline,
        categoryIcon: sumObj.newsCategoryImage,
        newsDate: fm(sumObj.DT, format),
        newsContent: sumObj.body,
        newsArticleURL: sumObj.link
    }));
    content.html(toBeRendered);
};

//Exercise Init and Show
$.hw.pageScripts['exercises']['init'] = function(page){
    if ($.hw.cache['exercises'] === undefined) {
        $.hw.cache['exercises'] = {};
    }
    $.hw.pageScripts['exercises']['bound'] = 1;
    var template;
    if($.hw.config.deviceMode){
        var params = $.hw.utils.getQueryParams();
        $.hw.auth.token = params.token;
        $.hw.config.lang = Number(params.lang);
    }
    var exerciseArea = $.hw.api.exerciseAreaEnum.sendRequest();
    var excTemplate = $.ajax({url: ($.hw.config.baseURL) +
                        "/my_health/exercises/exc-template.html"});
    var excCatArray = [];
    exerciseArea.then(function(data){
        data = data.Body.ExerciseAreaEnumResponse.ExerciseAreaEnumResult.ExerciseArea;
        excCatArray = data;
    });
    excTemplate.then(function(resp){
        template = resp;
    });
    $.when(exerciseArea, excTemplate).then(function(data){
        var v = {};
        var li = $("<li />", {"data-icon": "false",
                            "data-earea-id": -1,
                            "class": "exc-list view-all-exc"});
        var href = !($.hw.config.deviceMode)? '' : 'easyapp://exercisearea';
        var translate = $.hw.i18n.data.exercises[$.hw.config.lang];
        li.append($("<a />", {html: translate.view_all_exercises, href: href}));

        $(".exc-listview").append(li);
        for(var i = 0; i < excCatArray.length; i++){
            v = excCatArray[i];
            var tempData = {
                area: v.exerciseArea,
                id: v.exerciseAreaID
            };
            if($.hw.config.deviceMode){
                tempData["device"] = true;
            }
            var l = Mustache.render(template, tempData);
            $(".exc-listview").append(l);
        }
        $(".exc-listview").listview();
        $(".exc-listview").listview("refresh");
    });
    $("body").on("click", ".exc-list", function(data){
        var val = $(this).attr("data-earea-id");
        var selectedHeader = $(this).find('.exc-category').text();

        if (selectedHeader === '' || selectedHeader === undefined) {
            var translate = $.hw.i18n.getTranslation('allexc');
            selectedHeader = translate.allexc_header;
        }

        $.hw.cache["exercises"]["selectedArea"] = val;
        $.hw.cache["exercises"]["selectedHeader"] = selectedHeader;
        if(!($.hw.config.deviceMode)){
            $.mobile.changePage($.hw.config.baseURL +
                                    "/my_health/exercises/all-exc.html");
        }
    });
};

// My Health Form init and show
$.hw.pageScripts['form']  = {
    key: "",
    limitAuthorized: true,
    show: function (page) {
        var requestKey = $.hw.api.form.formKey();

        page.find(".listview_form").children().remove();
        requestKey.then(function (resp) {
            var form = resp.Body.FormEnumResponse.FormEnumResult.Form;

            if ($.isArray(form)) {
                var items = {}
                $.each(form, function (k, v) {
                    items[v.form] = v.formKey
                });
                var aKey = "Hälsa & Stress",
                    bKey = "Health & Stress"
                if (items[aKey] !== undefined) {
                    key = items[aKey];
                } else {
                    key = items[bKey];
                }
            } else {
                key = form.formKey;
            }

            $.hw.pageScripts['form'].key = key;
            var data = {formKey: key};
            var requestQuestion = $.hw.api.form.formQuestion(data);

            requestQuestion.then(function (resp){
                var question = resp.Body.FormQuestionEnumResponse.
                               FormQuestionEnumResult.Question;
                populateQuestion(question);
            });
        });

        function populateQuestion (question) {
            var request = $.ajax({url: ($.hw.config.baseURL) +
                            "/my_health/form-list-template.html"});
            var template = '';
            request.then(function (resp) {
                template = resp;
            });
            page.find(".listview_form").children().remove();
            $.when(request).then(function () {
                question.forEach(function (v, i) {
                    var answer = v.AnswerOptions.Answer;
                    var toReplace = {};
                    toReplace.question = v.QuestionText;
                    toReplace.questionID = v.QuestionID;
                    toReplace.optionID = v.OptionID;

                    switch (answer.length) {
                        case 2:
                            toReplace.first = answer[0].AnswerText;
                            toReplace.fifth = answer[1].AnswerText;
                            break;
                        case 3:
                            toReplace.first = answer[0].AnswerText;
                            toReplace.third = answer[1].AnswerText;
                            toReplace.fifth = answer[2].AnswerText;
                            break;
                        case 5:
                            toReplace.first = answer[0].AnswerText;
                            toReplace.second = answer[1].AnswerText;
                            toReplace.third = answer[2].AnswerText;
                            toReplace.fourth = answer[3].AnswerText;
                            toReplace.fifth = answer[4].AnswerText;
                            break;
                        default:
                            break;
                    }

                    var item = Mustache.render(template, toReplace);
                    $(".listview_form").append(item);
                });
                $('.listview_form').listview();
                $('.listview_form').listview('refresh');
                $('.form_slider').slider();
                $('.form_slider2').slider();
            });
        }
    },
    init: function (page) {
        $.hw.pageScripts['form'].bound = 1;
        $('body').on('click touchend', '#hiddenRange div.ui-slider',
                        function (e) {
            var parent = $(this).parent();
            var value = $(this).children('.ui-slider-handle');
                value = value.attr('aria-valuenow');
            var slide_icon = parent.siblings('.ui-slider');
            var close = parent.siblings('.form_close');
            var less = (value >= 100)? 1: 0;

            $(this).css('z-index', -100);

            slide_icon = slide_icon.children('.ui-slider-handle');
            slide_icon.attr('style', 'left: ' + (value - less) +
                                     '%; display: inline;');
            slide_icon.attr('aria-valuetext', value);
            slide_icon.attr('aria-valuenow', value);
            slide_icon.attr('title', value);
            slide_icon.show();
            close.show();
        }).on('click touchend', '.form_close', function (e) {
            var parent = $(this).parent();
            var slide_icon = parent.find('.ui-slider-handle');
            var close = parent.find('.form_close');
            var slider = parent.find('#hiddenRange div.ui-slider');

            slider.css('z-index', 300);
            slide_icon.hide();
            close.hide();
        }).on('click', '#saveForm', function () {
            var question = $('.form_slider');
            var formInstanceKey = '';
            var data = {};

            data = {
                formKey: $.hw.pageScripts['form'].key,
                fqa: []
            };

            question.each(function (k, v) {
                var que = $(v);
                var ans = que.siblings().find('.ui-slider-handle');
                if (ans.is(':visible')) {
                    var questionID = parseInt(que.attr('question-id'));
                    var optionID = parseInt(que.attr('option-id'));
                    var answer = ans.attr('aria-valuetext');
                    var ansObj = {
                            questionID: questionID,
                            optionID: optionID,
                            answer: answer
                        };
                    data.fqa.push({FormQuestionAnswer: ansObj});
                }
            });

            var requestSave = $.hw.api.form.saveAnswer(data);

            requestSave.then(function (resp) {
                formInstanceKey = resp.Body.
                                    UserSetFormInstanceResponse.
                                    UserSetFormInstanceResult;

                var url = $.hw.config.baseURL +
                          '/my_health/statistics/index.html';
                // Fix for statistics not showing after add form
                url += '?formInstanceKey=' + formInstanceKey
                $.mobile.changePage(url);
            });
        });
        $.hw.pageScripts['form'].show(page);
    }
};

// My Health statistics init and show
$.hw.pageScripts['statistics']['init'] = function (page, event) {
    $.hw.pageScripts['statistics']['bound'] = 1;
    $.hw.pageScripts["statistics"]["refreshStatistics"](page, event);
    //Attach Click Event Handler for anchors inside actionPlan
    $("body").on("click", ".stat-exc-link", function(event){
        var areaID = parseInt($(this).attr("data-area-id"));
        if(!$.hw.cache.exercises){
            $.hw.cache.exercises = {};
        }
        $.hw.cache.exercises.selectedArea = areaID;
        if(!$.hw.config.deviceMode){
            $.mobile.changePage("http://clients.easyapp.se/healthwatch//my_health/exercises/all-exc.html");
        }
    });
    $('body').on('click','.ui-btn-text', function (e) {
        if (!$(this).attr('toggled') || $(this).attr('toggled') == 'off'){
            $(this).attr('toggled','on');
            toggle(this, 'on');
        }
        else if ($(this).attr('toggled') == 'on'){
            if (e.target.className !== 'stat-exc-link'){
                $(this).attr('toggled','off');
                toggle(this, 'off');
            }
        }
    });

    function toggle (list, tag) {
        image = $(list).parent().find('.stat_collapse');
        content = $(list).parent().find('.stat_content');
        content.slideToggle();
        image.attr('src', $.hw.config.baseURL + '/images/collapse_' +
                         tag + '.png');
    }
};
$.hw.pageScripts['statistics']['show'] = function (page, event){
    if($.hw.pageFrom != "allexc"){
        $.hw.pageScripts["statistics"]["refreshStatistics"](page, event);
    }
};

$.hw.pageScripts["statistics"]["refreshStatistics"] = function (page, event) {
    page.find('.stat_list').remove();
    var key='';
    if($.hw.config.deviceMode){
        var mobileParam = $.hw.utils.getQueryParams();
        $.hw.auth.token = mobileParam.token;
    }
    var requestKey = $.hw.api.form.formKey();

    requestKey.then(function (resp) {
        var form = resp.Body.FormEnumResponse.FormEnumResult.Form;

        if ($.isArray(form)) {
            var items = {}
            $.each(form, function (k, v) {items[v.form] = v.formKey});
            var aKey = "Hälsa & Stress",
                bKey = "Health & Stress"
            if (items[aKey] != undefined) {
                key = items[aKey]
            } else {
                key = items[bKey]
            }
        } else {
            key = form.formKey;
        }

        var data = {};
        data = {formKey: key, formInstanceKey: ''};

        // try to get params
        var url = event.delegateTarget.URL;
        var params = $.hw.utils.getQueryParams(url);

        var requestFeedback;
        if (params.formInstanceKey) {
            requestFeedback = $.hw.api.statistics.getForm($.extend(data, {
                formInstanceKey: params.formInstanceKey
            }));
        } else {
            requestFeedback = $.hw.api.statistics.getForm(data);
        }

        var handler = function (resp) {
            var feedback = resp.Body.UserGetFormInstanceFeedbackResponse.
                           UserGetFormInstanceFeedbackResult;
            if (feedback.fiv == undefined && feedback.formInstanceKey) {
                data.formInstanceKey = feedback.formInstanceKey;
                $.hw.api.statistics.getForm(data).then(handler);
            } else {
                $('.stat_header_result_text').text(" " +
                    moment(feedback.dateTime).format('YYYY-MM-DD hh:mm'));

                if (feedback.fiv !== undefined) {
                    populateFeedback(feedback.fiv.FormInstanceFeedback);
                }
            }
        }

        requestFeedback.then(handler);
    });
    function populateFeedback(feedback){
        var excLoad;
        var template = '';
        var request = $.ajax({url: ($.hw.config.baseURL) +
                        "/my_health/statistics/statistics-list-template.html"});

        request.then(function (resp) {
            template = resp;
        });

        if($.hw.cache.exercises){
            if($.hw.cache.exercises.allexercises === undefined){
                excLoad = $.hw.mobile.exercise.loadAllExercises();
            } else {
                excLoad = $.hw.mobile.exercise.allexercises;
            }
        } else {
            excLoad = $.hw.mobile.exercise.loadAllExercises();
        }

        function parseActionPlanString (str) {
            var parr = [];
            var arr = [];
            arr = str.match(/<.*?>/g);
            if(arr){
                str = str.replace(/<.*?>/g, "$_placeholder_$");
                for(var i=0; i < arr.length; i++) {
                    var val = arr[i];
                    var id = parseAction(val);
                    var obj = searchForAreaID(id);
                    parr.push(obj);
                }
                for(var j=0; j < parr.length; j++){
                    var rep = replaceForAnchor(parr[j]);
                    str = str.replace(/\$_placeholder_\$/, rep);
                }
            }

            function parseAction(str) {
                return Number(str.substr(-4, 3));
            }
            function searchForAreaID(id) {
                var tar;
                var allexec = $.hw.cache.exercises.allexercises;

                var data = allexec.slice(0);

                for(var i=0;i<data.length;i++){
                    if(data[i].exerciseID == id){
                        tar = data[i];
                        break;
                    }
                }
                return tar;
            }
            function replaceForAnchor(obj){
                if(obj === undefined){
                    obj = {
                        exercise: "",
                        exerciseAreaID: 0
                    };
                }
                var title = obj.exercise;
                var areaID = obj.exerciseAreaID;
                var toAppend;
                if($.hw.config.deviceMode){
                    toAppend =  "href=easyapp://statistics";
                }
                var astr = [
                    "<a class='stat-exc-link'",
                    "data-area-id=", areaID ," ",
                    toAppend, " >", title, "</a>"
                ].join("");
                return astr;
            }
            return str;
        }

        $.when(request, excLoad).then(function () {
            feedback.forEach(function (v, i) {
                if (!isNaN(parseInt(v.value))) {
                    var toReplace = $.hw.i18n.data.statistics[$.hw.config.lang];
                    var value = parseInt(v.value);
                    var yellowLeft = parseInt(v.yellowLow);
                    var yellowWidth = parseInt(v.yellowHigh) - yellowLeft;
                    var greenLeft = parseInt(v.greenLow);
                    var greenWidth = parseInt(v.greenHigh) - greenLeft;
                    if (parseInt(v.greenHigh) >= 101) {
                        greenWidth -= 1;
                    }
                    if (parseInt(v.yellowHigh) >= 101) {
                        yellowWidth -= 1;
                    }

                    var color = '';

                    if(value >= greenLeft && value <= parseInt(v.greenHigh)) {
                        color = 'green';
                        toReplace.health_level_label = toReplace.healthy_level_label;
                    } else if (value >= yellowLeft &&
                               value <= parseInt(v.yellowHigh)) {
                        color = 'orange';
                        toReplace.health_level_label = toReplace.improvement_needed_label;
                    } else {
                        color = 'pink';
                        toReplace.health_level_label = toReplace.unhealthy_level_label;
                    }
                    if(v.actionPlan === undefined){
                        v.actionPlan = "";
                    }
                    toReplace.header = v.header;
                    toReplace.interpretation = v.analysis;
                    toReplace.healthyLevel = v.feedback;
                    toReplace.actionPlan = parseActionPlanString(v.actionPlan);
                    toReplace.stat_list_img = $.hw.config.baseURL + '/images/' +
                                              color + '.png';
                    toReplace.stat_range = 'background: url(' + 
                                            $.hw.config.baseURL + '/images/' +
                                            color + '_range.png) no-repeat;' +
                                      'background-size: ' + v.value + '%;';
                    toReplace.orange_holder = 'margin-left: ' + yellowLeft +
                                        '%;' + 'width: ' + yellowWidth + '%;';
                    toReplace.green_holder = 'margin-left: ' + greenLeft +
                                        '%;' + 'width: ' + greenWidth + '%;';

                    if (parseInt(v.greenHigh) >= 100) {
                        toReplace.green_holder += 'border-right: none;';
                    }
                    if (parseInt(v.yellowHigh) >= 100) {
                        toReplace.orange_holder += 'border-right: none;';
                    }

                    var item = Mustache.render(template, toReplace);
                    $(".listview_stat").append(item);
                }
            });
            $('.listview_stat').listview();
            $('.listview_stat').listview('refresh');
        });
    }
};

// My Health statistics view init and show
$.hw.pageScripts['statistics_view'] = {
    init: function (page) {
        $.hw.cache.statistics_view = {};
        var translate = $.hw.i18n.getTranslation('statistics_view');

        var cache = $.hw.cache.statistics_view;
        var option1 = $('<option />', {value: '0', text: translate.latest});
        var option2 = $('<option />', {value: '1',
                                            text: translate.passed_week});
        var option3 = $('<option />', {value: '2', text:
                                            translate.passed_month});
        var option4 = $('<option />', {value: '3', 
                                            text: translate.passed_year});
        var option11 = $('<option />', {value: '1', text: translate.none});
        var option12 = $('<option />', {value: '2', text: translate.database});

        $('#timeframe_option').empty().append(option1, option2,
                                              option3, option4);
        $('#compare_option').empty().append(option11, option12);

        cache['timeframe'] = option1.text();
        cache['timeframeVal'] = option1.val();
        cache['compare'] = option11.text();
        cache['compareVal'] = option11.val();

        $('.timeframe_option_label').text(option1.text());
        $('.compare_option_label').text(option11.text())

        $('body').on('change', '#timeframe_option', function () {
            var value =  $('#timeframe_option').val();
            var label = $('.timeframe_option_label');
            var text = $('#timeframe_option option:selected').text();

            cache['timeframeVal'] = value;
            cache['timeframe'] = text;
            label.text(text);
        }).on('change', '#compare_option', function () {
            var value =  $('#compare_option').val();
            var label = $('.compare_option_label');
            var text = $('#compare_option option:selected').text();

            cache['compareVal'] = value;
            cache['compare'] = text;
            label.text(text);
        }).on('click', '#statGraphBtn', function (){
            if (cache['timeframeVal'] == 0) {
                $.mobile.changePage('index.html');
            } else {
                $.mobile.changePage('graph.html');
            }
        });
    }
};
// My Health statistics graph init and show
$.hw.pageScripts['statistics_graph'] = {
    formKey: '',
    fromDateTime: '',
    toDateTime: '',
    graphColors: {},
    feedbackEnum: [],
    compareWith: false,
    limitAuthorized: true,
    plotData: [
                {label: 'blue', data: []},
                {label: 'orange', data: []},
                {label: 'green', data: []},
                {label: 'pink', data: []},
                {label: 'right', data: [], yaxis: 2}
            ],
    plotCompare: [
                {label: 'blue', data: []},
                {label: 'orange', data: []},
                {label: 'green', data: []},
                {label: 'pink', data: []}
            ],
    show: function(page){
        var formKey='';
        var requestKey;
        var firstValue = 0;
        var timeframe = '';
        var compare = '';
        var timeframeVal = 0;
        var compareVal = 0;
        var fromDateTime;
        var toDateTime;
        var statistics_graph = $.hw.pageScripts['statistics_graph'];
        var toDate = moment().add('days', 1);
        toDateTime = moment(toDate).format('YYYY-MM-DDThh:mm:ss.ssss');
        statistics_graph.toDateTime = toDateTime;

        var translate = $.hw.i18n.getTranslation('statistics_view');

        // try to get params
        if($.hw.config.deviceMode){
            var params = $.hw.utils.getQueryParams();
            $.hw.auth.token = params.token;
            $.hw.config.lang = Number(params.lang);
            timeframeVal = params.timeframeVal;
            compareVal = params.compareVal;
            formKey = params.formKey;
        }
        else {
            timeframeVal = $.hw.cache.statistics_view['timeframeVal'];
            compareVal = $.hw.cache.statistics_view['compareVal'];
        }
        //set Value of startDate
        switch (parseInt(timeframeVal)){
            case 1: //Passed week
                fromDateTime = moment().subtract('days', 7).calendar();
                timeframe = translate.passed_week;
                break;
            case 2: //Passed month
                fromDateTime = moment().subtract('months', 1).calendar();
                timeframe = translate.passed_month;
                break;
            case 3: //Passed year
                fromDateTime = moment().subtract('years', 1).calendar();
                timeframe = translate.passed_year;
                break;
            default:
                break;
        }

        switch (parseInt(compareVal)) {
            case 1:
                compare = translate.none;
                statistics_graph.compareWith = false;
                break;
            case 2:
                compare = translate.database;
                statistics_graph.compareWith = true;
                break;
            default:
                break;
        }

        fromDateTime = moment(fromDateTime).format('YYYY-MM-DDThh:mm:ss.ssss');
        statistics_graph.fromDateTime = fromDateTime;

        page.find('.stat_header_result_text').text(' ' + translate.result + ' ' + timeframe);
        page.find('.stat_header_compare').text(translate.compare_with + ' ' + compare);

        if (statistics_graph.feedbackEnum.length <= 0) {
            if ($.hw.deviceMode) {
                getFeedback(formKey);
            }
            else {
                requestKey =  $.hw.api.form.formKey();

                requestKey.then(function (resp) {
                    var form = resp.Body.FormEnumResponse.FormEnumResult.Form;

                    if ($.isArray(form)) {
                        var items = {}
                        $.each(form, function (k, v) {
                            items[v.form] = v.formKey
                        });
                        var aKey = "Hälsa & Stress",
                            bKey = "Health & Stress"
                        if (items[aKey] !== undefined) {
                            key = items[aKey];
                        } else {
                            key = items[bKey];
                        }
                    } else {
                        key = form.formKey;
                    }

                    $.hw.pageScripts['statistics_graph'].formKey = key;
                    getFeedback(key);
                });
            }
        } else {
            statistics_graph.refreshOptions();
            $(window).trigger('resize');
        }

        function getFeedback (formKey) {
            var data = {formKey: formKey};

            var requestFeedbackEnum = $.hw.api.statistics.
                                      feedbackTemplateEnum(data);

            requestFeedbackEnum.then(function (resp){
                var feedEnum = resp.Body.FormFeedbackTemplateEnumResponse.
                                   FormFeedbackTemplateEnumResult.
                                   FormFeedbackTemplate;
                firstValue = feedEnum[0].feedbackTemplateID;
                statistics_graph.feedbackEnum = feedEnum;
                statistics_graph.graphColors['blue'] = firstValue;
                statistics_graph.populateOption();
                statistics_graph.setPlotData($('#graph_1'));
            });
        }

        page.on('change', '#graph_1, #graph_2, #graph_3, #graph_4',
                function() {
            $.hw.pageScripts['statistics_graph'].setPlotData($(this));
            $(window).trigger('resize');
        });
        var doit;
        $(window).on('resize', function () {
            clearTimeout(doit);
            doit = setTimeout(function() {
                $.hw.pageScripts['statistics_graph'].showGraph();
                setOptionsWidth();
            }, 300);
        });
        function setOptionsWidth () {
            var windowWidth = $(window).width();
            var legendWidth;
            if (windowWidth < 520) {
                legendWidth = (windowWidth / 2) - 1;
            } else {
                legendWidth = (windowWidth / 4) - 1;
            }
            page.find('.graph_legend').css('width', legendWidth + 'px');
        }
        setOptionsWidth();
    },
    init: function (page) {
        $.hw.pageScripts['statistics_graph']['bound'] = 1;
        $('body').on('click', '#viewBtn', function (){
            $.mobile.changePage('view.html');
        });
        $.hw.pageScripts['statistics_graph'].show(page);
    },
    refreshOptions: function (page) {
        var statistics_graph = $.hw.pageScripts['statistics_graph'];
        var noCompare = [];
        noCompare = [
                        {label: 'blue', data: []},
                        {label: 'orange', data: []},
                        {label: 'green', data: []},
                        {label: 'pink', data: []}
                    ];
        statistics_graph.plotCompare = noCompare;
        statistics_graph.setPlotData($('#graph_1'));
        statistics_graph.setPlotData($('#graph_2'));
        statistics_graph.setPlotData($('#graph_3'));
        statistics_graph.setPlotData($('#graph_4'));
        $.hw.pageScripts['statistics_graph'].populateOption();
    },
    populateOption: function () {
        var colors = $.hw.pageScripts['statistics_graph'].graphColors;
        var blue = colors['blue'] || 0;
        var orange = colors['orange'] || 0;
        var green = colors['green'] || 0;
        var pink = colors['pink'] || 0;

        populate($('#graph_1'), [orange, green, pink], false, true);
        populate($('#graph_2'), [blue, green, pink], true, true);
        populate($('#graph_3'), [blue, orange, pink], true,
                    (orange > 0 || green > 0));
        populate($('#graph_4'), [blue, green, orange], true,
                    (green > 0 || pink > 0));

        function populate(obj, exclude, withNone, withFeed) {
            obj.empty();
            if (withNone) {
                var translate = $.hw.i18n.getTranslation('statistics_view');
                var option = $('<option />', {value: 0, text: translate.none});
                obj.append(option);
            }

            if (withFeed) {
                var feedbackEnum = $.hw.pageScripts['statistics_graph'].
                                        feedbackEnum;
                feedbackEnum.forEach(function (v, k) {
                    if ($.inArray(v.feedbackTemplateID, exclude) < 0) {
                        var option = $('<option />', {
                                              value: v.feedbackTemplateID,
                                              text: v.header
                                     });
                        obj.append(option);
                    }
                });
            }
        }

        if (blue > 0) {
            $('#graph_1').val(blue).attr('selected', true).
                siblings('option').removeAttr('selected');
        }
        if (orange > 0) {
            $('#graph_2').val(orange).attr('selected', true).
                siblings('option').removeAttr('selected');
        }
        if (green > 0) {
            $('#graph_3').val(green).attr('selected', true).
                siblings('option').removeAttr('selected');
        }
        if (pink > 0) {
            $('#graph_4').val(pink).attr('selected', true).
                siblings('option').removeAttr('selected');
        }
        $('.graph_legend.blue div').text($('#graph_1 option:selected').text());
        $('.graph_legend.orange div').text($('#graph_2 option:selected').text());
        $('.graph_legend.green div').text($('#graph_3 option:selected').text());
        $('.graph_legend.pink div').text($('#graph_4 option:selected').text());
    },
    setPlotData: function (obj) {
        var statistics_graph = $.hw.pageScripts['statistics_graph'];
        var val = obj.find(":selected").val();
        var text = obj.find(":selected").text();
        var color = obj.attr('gcolor');
        var data = {};

        data = {
                   formKey: statistics_graph.formKey,
                   fromDateTime: statistics_graph.fromDateTime,
                   toDateTime: statistics_graph.toDateTime,
                   formFeedbackTemplateID: parseInt(val)
               };

        var plotData = $.hw.pageScripts['statistics_graph'].plotData;
        var plotCompare = $.hw.pageScripts['statistics_graph'].plotCompare;
        $.hw.utils.setDataInObjectArray(plotData, 'label', color, 'data', []);
        $.hw.utils.setDataInObjectArray(plotCompare, 'label', color, 'data', []);

        if (text.toLowerCase() != 'none') {
            var requestPlot = $.hw.api.statistics.graphData(data);

            requestPlot.then(function (resp) {
                var plotDataEnum = resp.Body.UserGetFormFeedbackResponse.
                               UserGetFormFeedbackResult.
                               FormFeedback;
                var plotValue = [];
                var databaseValue = '';

                if (plotDataEnum.length > 0) {
                    plotDataEnum.forEach(function (v, k) {
                        fillPlotArray(v.dateTime, v.value);
                        databaseValue = v.databaseValue;
                    });
                } else {
                    fillPlotArray(plotDataEnum.dateTime,
                                  plotDataEnum.databaseValue);
                    databaseValue = plotDataEnum.databaseValue;
                }

                function fillPlotArray(date, value) {
                    var dt = moment(date).format('MM/DD/YYYY');
                    var plot = [new Date(dt), value];
                    plotValue.push(plot);
                }

                $.hw.utils.setDataInObjectArray(plotData, 'label', color,
                                                'data', plotValue);

                if (statistics_graph.compareWith == true) {
                    var plotDatabase = [];
                    var fromDate = '';
                    var toDate = '';

                    fromDate =  moment(statistics_graph.fromDateTime).
                                format('MM/DD/YYYY');
                    toDate =  moment(statistics_graph.toDateTime).
                                format('MM/DD/YYYY');

                    plotDatabase.push([new Date(fromDate), databaseValue]);
                    plotDatabase.push([new Date(toDate), databaseValue]);

                    $.hw.utils.setDataInObjectArray(plotCompare, 'label', color,
                                                'data', plotDatabase);
                }

                statistics_graph.graphColors[color] = val;
                statistics_graph.populateOption();
                statistics_graph.showGraph();
            });
        }
        statistics_graph.graphColors[color] = val;
        statistics_graph.populateOption();
        statistics_graph.showGraph();
    },
    showGraph: function () {
        var statistics_graph = $.hw.pageScripts['statistics_graph'];
        var fromDate = statistics_graph.fromDateTime;
        var toDate = statistics_graph.toDateTime;
        var min = moment(fromDate).add('days',-1).format('MM/DD/YYYY');
        var max = moment(toDate).add('days', 1).format('MM/DD/YYYY');
        var options = {};

        options = {};
        options = {
            legend: {show: false},
            xaxis:  {
                        mode: 'time',
                        timeformat: '%Y-%m-%d',
                        min: new Date(min),
                        max: new Date(max),
                        tickLength: 0,
                        ticks: 0
                    },
            yaxes:  [{position: 'left', min: 0,
                        max: 100, inteval: 10, tickSize: 10},
                     {position: 'right', min: 0, font: {size: 11},
                        max: 100, inteval: 10, tickSize: 10}],
            colors: ['#1288BA', '#F18807', '#BDD142', '#DC0066'],
            grid: {
                      borderWidth: 1,
                      color: "rgb(145, 145, 145)"
                  }
        };

        $("#chart").empty();
        var placeholder = $("#chart");
        var data = [];

        // Plot graph value
        var plotData = statistics_graph.plotData;
        normalOptions = {
            lines: {show: true},
            points: {show: true, symbol: 'circle'}
        };
        plotData = $.map(plotData, function (v, k) {
            return $.extend(v, normalOptions);
        });

        plotGraph(plotData);
        //Plot graph compare with
        if (statistics_graph.compareWith == true) {
            compareOptions = {
                lines: {show: false},
                points: {show: true, symbol: 'square'},
                dashes: {
                        show: true,
                        lineWidth: 2,
                        dashLength: 3
                }
            };
            var plotCompare = statistics_graph.plotCompare;
            plotCompare = $.map(plotCompare, function (v, k) {
                return $.extend(v, compareOptions);
            });

            plotGraph(plotCompare);
        }

        function plotGraph(plotValue) {
            plotValue.forEach(function (series) {
                data.push(series);
                // and plot all we got
            });
            $.plot(placeholder, data, options);
        }

        toDate = moment(toDate).subtract('days', 1);
        $('#chart_label label.left').text(moment(fromDate).format('MM/DD/YYYY'));
        $('#chart_label label.right').text(moment(toDate).format('MM/DD/YYYY'));
    }
};

//Exercise Area init and show
$.hw.pageScripts['allexc']['init'] = function (page){
    $.hw.pageScripts['allexc']['bound'] = 1;
    var selectedArea;
    var params = $.hw.utils.getQueryParams();
    if(!$.hw.config.deviceMode) {
        if(params.eid){
            $.hw.cache["exercises"]["selectedArea"] = params.eid;
        }
        selectedArea = Number($.hw.cache["exercises"]["selectedArea"]);
        //Set Exercises Header
        var header = $.hw.cache.exercises.selectedHeader;
        if (header !== undefined || header !== '') {
            page.find('.ui-title').text(header);
        }
    } else {
        selectedArea = Number(params.id);
        $.hw.auth.token = params.token;
        $.hw.cache.exercises = {};
        $.hw.config.lang = Number(params.lang);
    }
    if(selectedArea == -1){
        selectedArea = 0;
    }
    $.hw.api.exerciseEnum.data.exerciseAreaID = selectedArea;
    var allExercises = $.hw.api.exerciseEnum.sendRequest();
    var excTemplate = $.ajax({url: ($.hw.config.baseURL) +
                        "/my_health/exercises/exc-listtemplate.html"});
    allExercises.then(function(data){
        data = data.Body.ExerciseEnumResponse.ExerciseEnumResult.ExerciseInfo;
        data = data.sort(function() { return 0.5 - Math.random() });
        if (data.length) {
            $.hw.cache.exercises.selectedHeader = data[0].exerciseArea;
            page.find('.ui-title').text(data[0].exerciseArea);
        }
        $.hw.cache.exercises["showExercises"] = data;
    });
    excTemplate.then(function(resp){
        $.hw.cache["exercises"]["exclistTemplate"] = resp;
    });
    
    $.when(allExercises, excTemplate).then(function(){
        var excArray = $.hw.cache.exercises["showExercises"];
        var template = $.hw.cache["exercises"]["exclistTemplate"];
        var loadExcList = $.hw.api.exerciseEnum.loadExcList;
        loadExcList(excArray, $(".allexc-listview"), template);
        $(".allexc-listview").listview();
        $(".allexc-listview").listview("refresh");

    });
    $("body").on("click", ".allexc-list", function(event){
        var that = $(this);
        var variantID = that.attr("data-variant-id");
        $.hw.cache["exercises"]["selectedVariantID"] =  variantID;
        if(!($.hw.config.deviceMode)){
            $.mobile.changePage("./instructions.html");
        }
    });
    $("body").on("click", ".exercise-filter", function(event){
        var that = $(this);
        if(!that.hasClass("ui-btn-active")){
            var $this = $(this);
            var parent = that.closest(".exercise-filter-control");
            var title = that.text().replace(/^\s*|\s*$/g, '');
            var data = $.hw.cache.exercises["showExercises"];
            data = data.slice(0);
            var sortExer = $.hw.api.exerciseEnum.sortShowExercises;
            var refreshList = $.hw.api.exerciseEnum.loadExcList;
            var listview = $(".allexc-listview");
            var template = $.hw.cache.exercises.exclistTemplate;
            parent.find(".ui-btn-active").removeClass("ui-btn-active");
            that.addClass("ui-btn-active");
            if($this.hasClass('filter_random')){
                data = data.sort(function() { return 0.5 - Math.random() });
                refreshList(data, listview, template);
            } else if($this.hasClass('filter_popular')){
                data = sortExer(data, "popularity", "desc");
                refreshList(data, listview, template);
            } else{
                data = sortExer(data, "exercise", "asc");
                refreshList(data, listview, template);
            }
        listview.listview();
        listview.listview("refresh");
        }
    });
};
$.hw.pageScripts['allexc']['show'] = function(page){
    var active = "ui-btn-active";
    $(".exercise-filter-control").find("." + active).removeClass(active);
    $(".exercise-filter").first().addClass(active);
    if($.hw.pageScripts['instructions']['bound']){
        $.hw.api.exerciseEnum.emptyInstructions();
    }
    $(".allexc-listview").empty();
    var params = $.hw.utils.getQueryParams();
    if(params.eid){
        $.hw.cache["exercises"]["selectedArea"] = params.eid;
    }
    var selectedArea = Number($.hw.cache["exercises"]["selectedArea"]);
    if(selectedArea == -1){
        selectedArea = 0;
    }
    $.hw.api.exerciseEnum.data.exerciseAreaID = selectedArea;
    var allExercises = $.hw.api.exerciseEnum.sendRequest();
    //Set Exercises Header
    var header = $.hw.cache.exercises.selectedHeader;
    if (header !== undefined || header !== '') {
        page.find('.ui-title').text(header);
    }

    allExercises.then(function(data){
        data = data.Body.ExerciseEnumResponse.ExerciseEnumResult.ExerciseInfo;
        if (data.length) {
            $.hw.cache.exercises.selectedHeader = data[0].exerciseArea;
            page.find('.ui-title').text(data[0].exerciseArea);
        }
        $.hw.cache.exercises["showExercises"] = data;
    });
    $.when(allExercises).then(function(){
        var excArray = $.hw.cache.exercises["showExercises"];
        var template = $.hw.cache["exercises"]["exclistTemplate"];
        
        var loadExcList = $.hw.api.exerciseEnum.loadExcList;
        loadExcList(excArray, $(".allexc-listview"), template);
        $(".allexc-listview").listview();
        $(".allexc-listview").listview("refresh");
        page.find('.ui-content').css('min-height', $(window).height());
    });
};

$.hw.pageScripts['instructions']['init'] = function(page) {
    $.hw.pageScripts['instructions']['bound'] = 1;
    var fm = $.hw.utils.formatDate;
    var format = "MMM DD, YYYY";
    if($.hw.config.deviceMode){
        $.hw.cache.exercises = {};
        var params = $.hw.utils.getQueryParams();
        $.hw.api.exerciseExec.data.exerciseVariantLangID = params.id;
        $.hw.auth.token = params.token;
    } else {
        var variantID = Number($.hw.cache.exercises["selectedVariantID"]);
        $.hw.api.exerciseExec.data.exerciseVariantLangID = variantID;
        var header = $.hw.cache.exercises.selectedHeader;
        if (header !== undefined || header !== '') {
            page.find('.ui-title').text(header);
        }
    }
    var exerciseAjax = $.hw.api.exerciseExec.sendRequest();
    var exerciseObj;
    exerciseAjax.then(function(data){
        data = data.Body.ExerciseExecResponse.ExerciseExecResult;
        $.hw.cache.exercises["selectedExercise"] = data;
    });
    var instructionTemp = $.ajax({url: ($.hw.config.baseURL) +
                        "/my_health/exercises/instruction-template.html"});
    instructionTemp.then(function(data){
        $.hw.cache.exercises["insTemplate"] = data;
    });
    $.when(exerciseAjax, instructionTemp).then(function() {
        var insPage = $("body").find("[data-pageKey='instructions']");
        var insContent = insPage.find("#content");
        var template = $.hw.cache.exercises["insTemplate"];
        var exerciseObj = $.hw.cache.exercises["selectedExercise"];
        var render = $.hw.api.exerciseExec.createRenderedTemplate;
        var toBeRendered = render(exerciseObj, template);
        insContent.html($.parseHTML(toBeRendered));
    });
    //var id = params["id"];
    //$.hw.api.newsEnum.newsCategoryID = id;
    //var sumObj = data.Body.NewsEnumResponse.NewsEnumResult.News[id];
};
$.hw.pageScripts['instructions']['show'] = function(page){
    var variantID = Number($.hw.cache.exercises["selectedVariantID"]);
    $.hw.api.exerciseExec.data.exerciseVariantLangID = variantID;
    var exerciseAjax = $.hw.api.exerciseExec.sendRequest();
    var header = $.hw.cache.exercises.selectedHeader;
    if (header !== undefined || header !== '') {
        page.find('.ui-title').text(header);
    }
    exerciseAjax.then(function(data){
        data = data.Body.ExerciseExecResponse.ExerciseExecResult;
        $.hw.cache.exercises["selectedExercise"] = data;
    });
    $.when(exerciseAjax).then(function() {
        var insPage = $("body").find("[data-pageKey='instructions']");
        var insContent = insPage.find("#content");
        var template = $.hw.cache.exercises["insTemplate"];
        var exerciseObj = $.hw.cache.exercises["selectedExercise"];
        var render = $.hw.api.exerciseExec.createRenderedTemplate;
        var toBeRendered = render(exerciseObj, template);
        insContent.html($.parseHTML(toBeRendered));
    });
};
$.hw.pageScripts["reminders"]["init"] = function() {
    $.hw.pageScripts['reminders']['bound'] = 1;
    /* HIDES REMINDER & SCHEDULE SETTINGS */
    $('.scheduleSettings, .remSettings').hide();
    var reminder = $.hw.api.reminder.getReminder();
    reminder.then(function(data) {
        data = data.Body.UserGetReminderResponse;
        data = data.UserGetReminderResult;
        $.hw.api.reminder.data = data;
        $.hw.pageScripts['reminders']['populate']();
    });
    /* CHANGES SETTINGS ACCORDING TO SELECTED OPTION */
    $("body").on("change", '#reminderOptions', function() {
        $('.remSettings').hide();
        $('#' + $(this).val()).show();
    });

    $("body").on('change', '#scheduleOptions', function() {
        $('.scheduleSettings').hide();
        $('#' + $(this).val()).show();
    });

    /* STYLES THE DROPDOWN PLACEHOLDERS */
    $("body").on("change", 'select', function () {
        if ($(this).val() == "empty") {
            $(this).addClass("none");
        } else {
            $(this).removeClass("none");
        }
    });
    $("body select").trigger("change");

    /* DEFAULT SELECTION FOR THE YEAR DROPDOWN IN THE BIRTHDAY FIELD */
    $('body').on ("click", "#year", function () {
        if ($(this).val() == "empty") {
            $(this).val(1970);
        }
    });

    /* TOGGLES BUTTON STATES ON THE CONTACT US PAGE */
    $('.button30, .button44').on('mousedown', function () {
        $(this).addClass("selected");
    });

    $('.button30, .button44').on('mouseup', function () {
        $(this).removeClass("selected");
    });

    $('.button30, .button44').on('touchstart' , function () {
        $(this).addClass("selected");
    });

    $('.button30, .button44').on('touchend', function () {
        $(this).removeClass("selected");
    });

    $("body").on("click", "#remindersSaveButton", $.hw.mobile.reminders.save);
};
$.hw.pageScripts['reminders']['show'] = function(page) {
    $('.scheduleSettings, .remSettings').hide();
    $("body #scheduleOptions").trigger("change");
    var reminder = $.hw.api.reminder.getReminder();
    reminder.then(function(data) {
        data = data.Body.UserGetReminderResponse;
        data = data.UserGetReminderResult;
        $.hw.api.reminder.data = data;
        $.hw.pageScripts['reminders']['populate']();
    });
};
$.hw.pageScripts['reminders']['populate'] = function(page) {
    var data = $.hw.api.reminder.data;
    $.hw.api.reminder.data = $.hw.utils.parseValues(data);
    var parsedData = $.hw.api.reminder.data;
    var remName = "";
    var remOpt = $("#reminderOptions");
    var sndHr = parsedData.sendAtHour;
    
    if(parsedData.type == 0) {
        remName = "never";
        $('.scheduleSettings, .remSettings').hide();
        remOpt.find("option[value='"+ remName +"']").attr("selected", true);
        $("body select").trigger("change");
    } else if(parsedData.type == 1) {
        remName = "regularly";
        remOpt.find("option[value='"+ remName +"']").attr("selected", true);

        var nme;
        var params = [];
        var reg = parsedData.regularity;
        if(reg == 1){
            nme = 'daily';
            $("#dailyTime option[value='"+ sndHr +"']");
            $("#scheduleOptions option[value='"+ nme +"']");
            $("#mon").attr("checked", parsedData.regularityDailyMonday);
            $("#tue").attr("checked", parsedData.regularityDailyTuesday);
            $("#wed").attr("checked", parsedData.regularityDailyWednesday);
            $("#thu").attr("checked", parsedData.regularityDailyThursday);
            $("#fri").attr("checked", parsedData.regularityDailyFriday);
            $("#sat").attr("checked", parsedData.regularityDailySaturday);
            $("#sun").attr("checked", parsedData.regularityDailySunday);
            params = [
                $("#dailyTime option[value='"+ sndHr +"']")
            ];
        } else if(reg == 2){
            nme = 'weekly';
            var wdy = parsedData.regularityWeeklyDay;
            var wev = parsedData.regularityWeeklyEvery;
            params = [
                $("#scheduleOptions option[value='"+ nme +"']"),
                $("#weeklyTime option[value='"+ sndHr +"']"),
                $("#weeklyDay option[value='"+ wdy +"']"),
                $("#weeklyEvery option[value='"+ wev +"']")
            ];
        } else if(reg == 3){
            nme = 'monthly';
            var mnr = parsedData.regularityMonthlyWeekNr;
            var mdy = parsedData.regularityMonthlyDay;
            var mev = parsedData.regularityMonthlyEvery;
            params = [
                $("#scheduleOptions option[value='"+ nme +"']"),
                $("#monthlyTime option[value='"+ sndHr +"']"),
                $("#monthPart option[value='"+ mnr +"']"),
                $("#monthlyDay option[value='"+ mdy +"']"),
                $("#monthlyInterval option[value='"+ mev +"']")
            ];
        }
        if(params.length){
            for(var i=0; i<params.length; i++){
                params[i].attr('selected', true);
            }
        }

        $("body select").trigger("change");
    } else if(parsedData.type == 2) {
        remName = "atInactivity";
        var cnt = parsedData.inactivityCount;
        var prd = parsedData.inactivityPeriod;
        remOpt.find("option[value='"+ remName +"']").attr("selected", true);

        $("body select").trigger("change");

        $("#inactiveTime option[value='"+ sndHr +"']").attr('selected', true);
        $("#number option[value='"+ cnt +"']").attr('selected', true);
        $("#timeFrame option[value='"+ prd +"']").attr('selected', true);
    }
};
$.hw.pageScripts['reminders']['save'] = function(){
    var data = {};
    var pint = parseInt;
    var def = {};
    var reminderVal = $("#reminderOptions").val();
    if(reminderVal == "never"){
        def = $.hw.api.reminder.setAsNever();
    } else if (reminderVal == "atInactivity"){
        data = $.extend(data, {
            sendAtHour: pint($("#inactiveTime").val()),
            inactivityCount: pint($("#number").val()),
            inactivityPeriod: pint($("#timeFrame").val())
        });
        def = $.hw.api.reminder.setInactivity(data);
    } else if (reminderVal == "regularly"){
        var val = $("#scheduleOptions").val();
        if(val == "monthly"){
            data = {
                regularity: 3,
                sendAtHour: pint($("#monthlyTime").val()),
                regularityMonthlyWeekNr: pint($("#monthPart").val()),
                regularityMonthlyDay: pint($("#monthlyDay").val()),
                regularityMonthlyEvery: pint($("#monthlyInterval").val())

            };
        } else if(val == "weekly"){
            data = {
                regularity: 2,
                sendAtHour: pint($("#weeklyTime").val()),
                regularityWeeklyDay: pint($("#weeklyDay").val()),
                regularityWeeklyEvery: pint($("#weeklyEvery").val())
            };
        } else if(val == "daily") {
            var temp = {};
            var checkedData = {};
            var cItems = $(".dayCheckBox");
            cItems = cItems.find("input[type='checkbox']:checked");

            $.each(cItems, function(i, v){
                var tempName = "regularityDaily" + $(v).siblings().text();
                checkedData[tempName] = true;
            });

            data = {
                regularity: 1,
                sendAtHour: pint($("#dailyTime").val())
            };
            data = $.extend(data, checkedData);
        }
        def = $.hw.api.reminder.setRegularly(data);
    }
    $.when(def).then(function(data){
        var data = data.Body.UserSetReminderParameterizedResponse;
        var toBool = $.hw.utils.stringToBoolean;
        data = toBool(data.UserSetReminderParameterizedResult);
        if(data){
            if($.hw.config.deviceMode){
                window.location = "easyapp://success";
            } else {
                var reminder = $.hw.api.reminder.getReminder();
                reminder.then(function(data){
                    data = data.Body.UserGetReminderResponse;
                    data = data.UserGetReminderResult;
                    $.hw.api.reminder.data = $.extend({}, data);
                    $.mobile.changePage("http://clients.easyapp.se/healthwatch//settings/index.html");
                });
            }
        } else {
            if($.hw.config.deviceMode){
                window.location = "easyapp://failure";
            }
        }
    });
};

$.hw.pageScripts['home'] = {
    bound: 0,
    init: function (page, event) {
        $.hw.pageScripts['home'].bound = 1;
        // check if we show start page
        if ($.hw.config.showStartPageOnStart == false) {
            $('#splash_image_link').attr('href', "http://clients.easyapp.se/healthwatch//my_health/");
        }
        setTimeout(function () {
            $('#splash_image_link').trigger('click');
        }, $.hw.config.splashTimeout);
    },
    show: function (page, event){
        event.preventDefault();
        return false;
    }
};

$.hw.pageScripts['settings'] = {
    bound: 0,
    init: function (page) {
        $.hw.pageScripts['settings'].bound = 1;
        $('html').on('click', 'a', function (e) {
            switch (e.currentTarget.id) {
                case 'change_profile':
                    $.mobile.changePage("http://clients.easyapp.se/healthwatch//account/change_profile.html");
                    break;
                case 'reminders':
                    $.mobile.changePage("http://clients.easyapp.se/healthwatch//settings/reminders.html");
                    break;
                case 'security_settings':
                    $.mobile.changePage("http://clients.easyapp.se/healthwatch//settings/security.html");
                    break;
                default:
                    break;
            }
        });
    },
    show: function (page, event){
        event.preventDefault();
        return false;
    }
};

$.hw.pageScripts['security'] = {
    bound: 0,
    init: function (page) {
        $.hw.pageScripts['security'].bound = 1;

        $('body').on('change', '#show_start_page_toggle', function () {
            if ($(this).is(":checked")) {
                $.cookie("noStartPage", "");
            } else {
                $.cookie("noStartPage", 1);
            }
        });

        $('body').on('change', '#auto_login_toggle', function () {
            if ($(this).is(":checked")) {
                $.cookie("autoLogin", 1);
                $.hw.auth.userExtendPoll.start();
            } else {
                $.cookie("autoLogin", "");
                $.hw.auth.userExtendPoll.stop();
            }
        });

        $.hw.pageScripts['security'].show(page);
    },
    show: function (page, event) {

        var control = $('#show_start_page_toggle');
        var cook = $.cookie("noStartPage");
        var checked = isNaN(cook) || !Boolean(parseInt(cook));
        control.prop('checked', checked);
        if (checked) {
            control.attr('checked', 'checked');
        } else {
            control.removeAttr('checked', 'checked');
        }

        control = $('#auto_login_toggle');
        var cook = $.cookie("autoLogin");
        checked = cook && cook.toString().length > 0;
        control.prop('checked', checked);
        if (checked) {
            control.attr('checked', 'checked');
        } else {
            control.removeAttr('checked', 'checked');
        }
    }
};

$.hw.pageScripts['my_health'] = {
    bound: 0,
    limitAuthorized: true,
    init: function (page) {
        $.hw.pageScripts['my_health'].bound = 1;
        $('body').on('click', 'a', function (e) {
            var mhPath = 'http://clients.easyapp.se/healthwatch//my_health';
            switch (e.currentTarget.id) {
                case 'mh_settings':
                    $.mobile.changePage("http://clients.easyapp.se/healthwatch//settings/index.html");
                    break;
                case 'mh_form':
                    $.mobile.changePage(mhPath + "/form.html");
                    break;
                case 'mh_statistics':
                    $.mobile.changePage(mhPath + "/statistics/index.html");
                    break;
                case 'mh_calendar':
                    // get date
                    var to = moment().format("YYYY-MM-DDTHH:mm:ss");
                    $.mobile.changePage(mhPath +
                                        "/calendar/edit_day.html?datetime=" +
                                        to);
                    break;
                case 'mh_exercises':
                    $.mobile.changePage(mhPath + "/exercises/index.html");
                    break;
                default:
                    break;
            }
        });
        var doit;
        $(window).on('resize', function () {
            doit = setTimeout(function() {
                var width = $(window).width();
                var iconsContainer = $('body').find('.iconsContainer');
                if (width <= 540 || $('body.portrait').length !== 0) {
                    iconsContainer.css('width', '270px');
                } else {
                    iconsContainer.css('width', '540px');
                }
                clearTimeout(doit);
            }, 300);
        });
    }
};
$.hw.pageScripts['change_profile'] = {
    limitAuthorized: true,
    init: function (page) {
        $.hw.pageScripts['change_profile'].bound = 1;
        $.hw.pageScripts['change_profile'].show(page);
        $('body').on('click', '#saveProfileBtn', function () {
            $.hw.mobile.account.update(page);
        });
    },
    show: function (page){
        // set password as optional
        page.find("#content").hide();
        page.find('.accountQuestion_password .asterisk').hide();

        questions = $.hw.mobile.account.loadProfileQuestionChoices(page);
        questions.then(function () {
            var loadValues = $.hw.mobile.account.load(page);

            $.when.apply($, loadValues).then(function (){
                page.find("#content").show();
            });
        });
    }
};

$.hw.pageScripts['create_account'] = {
    init: function (page){
        $.hw.pageScripts['create_account'].bound = 1;
        // load profile question choices
        $.hw.pageScripts['create_account'].show(page);
        $('body').on('click', '#createAccountBtn', function () {
            $.hw.mobile.account.create(page);
        });
    },
    show: function (page) {
        // load profile question choices
        $.hw.mobile.account.loadProfileQuestionChoices(page);
    }
};

$.hw.pageScripts['startpage'] = {
    bound: 0,
    init: function (page, event) {
        $.hw.pageScripts['startpage'].bound = 1;
        $('body').on('afterTranslation', function () {
            var check = $.hw.config.showStartPageOnStart;
            var control = page.find('#show_startpage');
            control.prop('checked', check);
            control.on('click', function () {
                if ($(this).is(":checked")) {
                    $.cookie("noStartPage");
                } else {
                    $.cookie("noStartPage", 1);
                }
            });
        });
        $("body").on("click", "#startPageLink", function(){
            if($.hw.auth.isLoggedIn()){
                $.mobile.changePage("http://clients.easyapp.se/healthwatch//my_health");
            } else {
                $.mobile.changePage("http://clients.easyapp.se/healthwatch//account/login.html");
            }
        });
        $.hw.api.wordsOfWisdom.sendRequest({
            languageID: $.hw.config.lang
        }).then(function (resp) {
            var wisdom = resp.Body.TodaysWordsOfWisdomResponse;
            wisdom = wisdom.TodaysWordsOfWisdomResult;
            $('#start_words_of_wisdom').text(wisdom.words);
            $('#start_author').text(wisdom.author);
        });
    }
};
$.hw.pageScripts['calendar'] = {
    bound: 0,
    limitAuthorized: true,
    init: function (page) {
        var tpl = '<li class="stat_header calendarDayEventHead">' +
                '<p>{{date}}</p>' +
            '</li>' +
            '{{#dayEvents}}' +
            '<li class="stat_list calendarDayEvent" ' +
                'data-icon="false">' +
                '<a href="{{link}}">' +
                    '<div class="list_item time_col">{{{time}}}</div>' +
                    '<div class="list_item">' +
                        '<div class="title">{{title}}</div>' +
                        '<div class="subtitle">{{subtitle}}</div>' +
                    '</div>' +
                    '<div class="list_item value_col">{{{value}}}</div>' +
                '</a>' +
            '</li>' +
            '{{/dayEvents}}' +
        '</ul>' +
        '</div>';
        // send request
        var to = moment().format("YYYY-MM-DDTHH:mm:ss");
        var from = moment().subtract("months", 1);
        from = from.format("YYYY-MM-DDTHH:mm:ss");
        var request = $.hw.api.calendar.calendarEvents(from, to);
        request.then(function (res,  data) {
            // get element
            var listBox = page.find('#calendarEventsList > ul');
            listBox.empty();
            // retrieve data
            data = data.Calendar;
            if (data === undefined) {
                // cancel login
                return;
            }
            var today = moment().format("dddd, YYYY MMM DD");
            var array = [];

            if (!$.isArray(data)) {
                array.push(data);
            } else {
                array = data;
            }

            // loop through days
            for (var i = 0; i < array.length; i++) {
                var day = array[i];
                var cmoment = moment(day.date);
                var day_note = $.hw.utils.htmlDecode(day.note);
                var cdate = cmoment.format("dddd, YYYY MMM DD");
                cdate = (cdate == today)? "TODAY" : cdate;
                var events = [];
                // show mood if present
                if (day.mood !== "NotSet" || day_note) {
                    // add mood
                    var img = "";
                    if (day.mood !== "NotSet") {
                        var img = day.mood.toLowerCase();
                        img = (img == "dontknow")? "dontKnow" : img;
                        var imgPath = "http://clients.easyapp.se/healthwatch//images/" + img +
                                      "@2x.png";
                        img = '<img src="' + imgPath + '" ></img>';
                    }
                    events.push({
                        time: img,
                        title: "Notes",
                        subtitle: day_note,
                        link: './edit_day.html?datetime=' +
                              cmoment.format("YYYY-MM-DDTHH:mm:ss")
                    });
                }
                if (day.events !== undefined) {
                    var eventArray = [];

                    if (!$.isArray(day.events.Event)) {
                        eventArray.push(day.events.Event);
                    } else {
                        eventArray = day.events.Event;
                    }

                    for (var j = 0; j < eventArray.length; j++) {
                        var eve = eventArray[j];
                        // add event
                        var result = eve.result;
                        var link = "#";
                        if (eve.type == "Form") {
                            result = '<img class="right_image" ' +
                                     'src="http://clients.easyapp.se/healthwatch//images/' +
                                     'lv_right_img.png" ></img>';
                            link = "../statistics/index.html?" +
                                   "formInstanceKey=" + eve.formInstanceKey;
                        }
                        events.push({
                            time: moment(eve.time).format("hh:mm"),
                            title: eve.description,
                            value: result,
                            link: link
                        });
                    }
                }
                if (events.length > 0) {
                    var html = Mustache.render(tpl, {
                        date: cdate,
                        dayEvents: events
                    });
                    listBox.append(html);
                    listBox.listview().listview("refresh");
                }
            }
        });
    }
};
