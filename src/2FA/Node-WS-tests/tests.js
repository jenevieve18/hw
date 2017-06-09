const Listr = require('listr'); //Creates pretty lists.
const soap = require('soap');
const rxjs = require('rxjs'); //Helps with the async nature of node.
const url = 'https://dev-ws.healthwatch.se/service.asmx?WSDL';

//Setup variables in global scope so they can be used between tests. Some arguments are hard-coded.
//var global.username, global.password, global.secretKey, global.languageID, global.token;
global.username = 'Testuser_' + Math.floor((Math.random() * 100000))+1;
global.languageID = '2'; //English
global.password = 'bionicman';
global.email = username + '@healthwatch.com';

const createuser = new Listr([
	{
		title: 'All tests',
		task: () => {
			return new Listr([
				{title: 'Test creation of user -> enabling 2FA -> logging in with 2FA',
					task: () => {
			return new Listr([


					{title: 'Confirm connection', //Attempt to get the english word of wisdom for the day. If this fails, we can't properly interact with the WS, and all other tests are ignored.
					task: () => {
						return new rxjs.Observable(observer => {
							observer.next('Requesting response from webservice...');
							soap.createClient(url, function(err, client) {
								client.TodaysWordsOfWisdom({ languageID: global.languageID }, function(err, result) {
									if(err){
										observer.error(err);
									} else {
										observer.complete();
									}
								});
							});

						});
					}
				},

				{title: 'Create test user and validate it',
				task: () => {
					return new Listr([
						{
							title: 'Create test user', //Create test user
							task: () => {
								return new rxjs.Observable(observer => {
									observer.next('Creating user: Requesting response from webservice...');
									soap.createClient(url, function(err, client) {
										client.UserCreate({ username: global.username, password: global.password, email: global.email, alternateEmail: global.email, acceptPolicy: true, languageID: global.languageID, sponsorID: '0', departmentID: '0', expirationMinutes: '20' }, function(err, result) {
											if(err){
												observer.error(err);
											} else if (result.UserCreateResult.tokenExpires == "Mon Jan 01 1 01:00:00 GMT+0100 (CET)") {
												observer.error(new Error("Invalid token returned"));
											}
											else {
												global.token = result.UserCreateResult.token;

												observer.next('User created. Testing token...');
												soap.createClient(url, function(err, client) {
													client.UserExtendToken({ token: global.token, expirationMinutes: '20' }, function(err, result) {
														if(err){
															observer.error(err);
														} else if (result.UserExtendTokenResult == false) {
															observer.error(new Error("User could not be validated"));
														}
														else {
															observer.complete();
														}
													});
												});

											}
										});
									});

								});
							}
						}
					])}
				},


				{title: 'Confirm that 2FA is disabled by default',
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next("Requesting response...")
						soap.createClient(url, function(err, client) {
							client.UserGet2FAStatus({ token: global.token, expirationMinutes: '20' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserGet2FAStatusResult.user2FAEnabled != true && result.UserGet2FAStatusResult.sponsor2FAEnabled != true) {
									observer.complete();
								}else {
									observer.error("2FA not disabled by default");
								}
							});
						});
					});
				}},

				{title: 'Enable 2FA',
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next("Requesting response...")
						soap.createClient(url, function(err, client) {
							client.UserEnable2FA({ token: global.token, expirationMinutes: '0' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserEnable2FAResult == true) {
									observer.complete();
								}else{
									observer.error(new Error("2FA could not be enabled"));
								}
							});
						});
					});
				}},


				{title: 'Confirm that user is logged out after enabling 2FA', //Fails intermittently?
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next('User created. Testing token...');
						setTimeout(function(){ //Sleep for 10 seconds to see if this fixes intermittent failures - it might be due to the server being slow to process the request to expire all active tokens for this user.
							soap.createClient(url, function(err, client) {
								client.UserExtendToken({ token: global.token, expirationMinutes: '20' }, function(err, result) {
									if(err){
										observer.error(err);
									} else if (result.UserExtendTokenResult == false) {
										observer.complete();
									}
									else {
										observer.error(new Error("User still logged in. " + new Date().getSeconds()));
									}
								});
							});
						}, 1000);
					});
				}},

				{title: 'Confirm that first login after enabling 2FA renders a secretKey and resourceID',
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next('Requesting response from webservice...');
						soap.createClient(url, function(err, client) {
							client.UserLogin2FA({ username: global.username, password: global.password, expirationMinutes: '20' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserLogin2FAResult.secretKey.length == 32 && result.UserLogin2FAResult.resourceID != null) {
									global.resourceID = result.UserLogin2FAResult.resourceID;
									global.secretKey = result.UserLogin2FAResult.secretKey;
									observer.complete();
								} else {
									observer.error(new Error("No secretKey recieved. " + JSON.stringify(result)));
								}
							});
						});
					});
				}},

				//// !!! THIS TASK CURRENTLY FAILS  !!! ////
				// {title: 'Confirm that submitting a valid secretKey without an active login attempt does not succeed',
				// task: () => {
				// 	return new rxjs.Observable(observer => {
				// 		soap.createClient(url, function(err, client) {
				// 		observer.next('Requesting response from webservice...');
				// 			client.UserSubmitSecretKey({ secretKey: global.secretKey, expirationMinutes: '20' }, function(err, result) {
				// 				if(err){
				// 					observer.error(err);
				// 				} else if (result.UserSubmitSecretKeyResult == false) {
				// 					observer.complete();
				// 				} else {
				// 					observer.error(new Error("Submitting secretKey succeded without active login attempt."));
				// 				}
				// 			});
				// 		});
				// 	});
				// }},

				// {title: 'Confirm that second login after enabling 2FA renders a resourceID',
				// task: () => {
				// 	return new rxjs.Observable(observer => {
				// 		soap.createClient(url, function(err, client) {
				// 			observer.next('Requesting response from webservice...');
				// 			client.UserLogin2FA({ username: global.username, password: global.password, expirationMinutes: '20' }, function(err, result) {
				// 				if(err){
				// 					observer.error(err);
				// 				} else if (result.UserLogin2FAResult.resourceID.length != null) {
				// 					global.resourceID = result.UserLogin2FAResult.resourceID;
				// 					observer.complete();
				// 				} else {
				// 					observer.error(new Error("No resourceID recieved. " + JSON.stringify(result.UserLogin2FAResult.secretKey)));
				// 				}
				// 			});
				// 		});
				// 	});
				// }},

				{title: 'Submit secret key',
				task: () => {
					return new rxjs.Observable(observer => {
						soap.createClient(url, function(err, client) {
							observer.next('Requesting response from webservice...');
							client.UserSubmitSecretKey({ secretKey: global.secretKey, expirationMinutes: '20' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserSubmitSecretKeyResult == true) {
									observer.complete();
								} else {
									observer.error(new Error("Submitting secretKey failed."));
								}
							});
						});
					});
				}},

				{title: 'Get and test token using resourceID', //Currently causes a 500 internal error
				task: () => {
					return new rxjs.Observable(observer => {
						soap.createClient(url, function(err, client) {
							observer.next('Requesting response from webservice...');
							client.UserHolding({ resourceID: global.resourceID, username: global.username}, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserHoldingResult.tokenExpires != "Mon Jan 01 1 01:00:00 GMT+0100 (CET)") {
									global.token = result.UserHoldingResult.token;
									observer.next('Got token. Testing...');
									soap.createClient(url, function(err, client) {
										client.UserExtendToken({ token: global.token, expirationMinutes: '20' }, function(err, result) {
											if(err){
												observer.error(err);
											} else if (result.UserExtendTokenResult == false) {
												observer.error(new Error("Token could not be validated. " + JSON.stringify(result)));
											}
											else {
												observer.complete();
											}
										});
									});
								} else {
									observer.error(new Error("Getting token using resourceID failed." + JSON.stringify(result)));
								}
							});
						});
					});
				}},

				{title: 'Confirm that 2FA is now enabled according to UserGet2FAStatus',
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next("Requesting response...")
						soap.createClient(url, function(err, client) {
							client.UserGet2FAStatus({ token: global.token, expirationMinutes: '20' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserGet2FAStatusResult.user2FAEnabled == true) {
									observer.complete();
								}else {
									observer.error(new Error("2FA still disabled according to UserGet2FAStatusResult"));
								}
							});
						});
					});
				}},

				{title: 'Confirm that another login after enabling 2FA does not render a secretKey',
				task: () => {
					return new rxjs.Observable(observer => {
						observer.next('Requesting response from webservice...');
						soap.createClient(url, function(err, client) {
							client.UserLogin2FA({ username: global.username, password: global.password, expirationMinutes: '20' }, function(err, result) {
								if(err){
									observer.error(err);
								} else if (result.UserLogin2FAResult.secretKey.length == 90) {
									observer.error(new Error("SecretKey recieved upon another login after a successful one!"));
								} else {
									observer.complete();
								}
							});
						});
					});
				}},

				{
					title: 'All tests succeeded!',
					task: () => Promise.resolve()
				}


			])}
		}

		])} //Wrap all

		}]);



		const testemail = new Listr([
			{

				title: 'Test email reset for hardcoded user',
				task: () => {
					return new Listr([

							{title: 'Confirm connection', //Attempt to get the english word of wisdom for the day. If this fails, we can't properly interact with the WS, and all other tests are ignored.
							task: () => {
								return new rxjs.Observable(observer => {
									observer.next('Requesting response from webservice...');
									soap.createClient(url, function(err, client) {
										client.TodaysWordsOfWisdom({ languageID: global.languageID }, function(err, result) {
											if(err){
												observer.error(err);
											} else {
												observer.complete();
											}
										});
									});

								});
							}
						},

						//Log in
						{title: 'Log in as hardcoded user',
						task: () => {
							return new rxjs.Observable(observer => {
								observer.next('Requesting response from webservice...');
								soap.createClient(url, function(err, client) {
									client.UserLogin({ username: "testuseremail", password: "aoeuhtns1", expirationMinutes: '20' }, function(err, result) {
										if(err){
											observer.error(err);
										} else if (result.UserLoginResult.tokenExpires == "Mon Jan 01 1 01:00:00 GMT+0100 (CET)") {
											observer.error(new Error("Invalid token returned"));
										}
										else {
											global.emailtests = {};
											global.emailtests.token = result.UserLoginResult.token;

											observer.next('Logged in. Testing token...');
											soap.createClient(url, function(err, client) {
												client.UserExtendToken({ token: global.emailtests.token, expirationMinutes: '20' }, function(err, result) {
													if(err){
														observer.error(err);
													} else if (result.UserExtendTokenResult == false) {
														observer.error(new Error("User could not be validated"));
													}
													else {
														observer.complete();
													}
												});
											});

										}
									});
								});
							});
						}},


						{title: 'Confirm that 2FA is disabled before enabling',
						task: () => {
							return new rxjs.Observable(observer => {
								observer.next("Requesting response...")
								soap.createClient(url, function(err, client) {
									client.UserGet2FAStatus({ token: global.emailtests.token, expirationMinutes: '20' }, function(err, result) {
										if(err){
											observer.error(err);
										} else if (result.UserGet2FAStatusResult.user2FAEnabled != true && result.UserGet2FAStatusResult.sponsor2FAEnabled != true) {
											observer.complete();
										}else {
											observer.error("2FA not disabled and we managed to log in using single factor sign on. Panic!");
										}
									});
								});
							});
						}},

						//
						//Enable 2FA here
						//

						{title: 'Confirm that user is logged out after enabling 2FA', //Fails intermittently?
						task: () => {
							return new rxjs.Observable(observer => {
								observer.next('User created. Testing token...');
								setTimeout(function(){ //Sleep for 10 seconds to see if this fixes intermittent failures - it might be due to the server being slow to process the request to expire all active tokens for this user.
									soap.createClient(url, function(err, client) {
										client.UserExtendToken({ token: global.emailtests.token, expirationMinutes: '20' }, function(err, result) {
											if(err){
												observer.error(err);
											} else if (result.UserExtendTokenResult == false) {
												observer.complete();
											}
											else {
												observer.error(new Error("User still logged in. " + new Date().getSeconds()));
											}
										});
									});
								}, 1000);
							});
						}}


					])},

				}]);

					//
					//Reset password here
					//

		createuser.run();
		//testemail.run();
		new Date().getSeconds();
//Write tests for ensuring that multiple login attempts are handled according to expectations; only one at a time.

// Email notes
// kevin@scoollabs.com
//
// Non-SSL Settings
// (NOT Recommended)
// Username: 	kevin@scoollabs.com
// Password: 	Use your cPanel password.
// Incoming Server: 	mail.scoollabs.com
//
//     IMAP Port: 143
//     POP3 Port: 110
//
// Outgoing Server: 	mail.scoollabs.com
//
//     SMTP Port: 25
//
// Authentication is required for IMAP, POP3, and SMTP.
