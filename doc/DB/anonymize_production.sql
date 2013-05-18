USE eform
GO
TRUNCATE TABLE MailQueue
GO
TRUNCATE TABLE Debug
GO
DELETE FROM AnswerValue WHERE ValueText IS NOT NULL
GO
DELETE FROM AnswerValue WHERE ValueTextJapaneseUnicode IS NOT NULL
GO
UPDATE Manager SET Email = 'info' + CAST(ManagerID AS VARCHAR(4)) + '@eform.se',Password = 'password' + CAST(ManagerID AS VARCHAR(4)),Name = 'name' + CAST(ManagerID AS VARCHAR(4)), Phone = 'phone' + CAST(ManagerID AS VARCHAR(4))
GO
UPDATE ManagerProjectRound SET EmailSubject = 'EmailSubject'+RTRIM(RIGHT(CAST(EmailSubject AS VARCHAR(1024))+' ',1)), EmailBody = 'EmailBody'+RTRIM(RIGHT(CAST(EmailBody AS VARCHAR(1024))+' ',1)), MPRK = NEWID()
GO
UPDATE Project SET Internal = 'Internal' + CAST(ProjectID AS VARCHAR(4)), Name = 'Name' + CAST(ProjectID AS VARCHAR(4))
GO
UPDATE ProjectRound SET Internal = 'Internal' + CAST(ProjectRoundID AS VARCHAR(4)), EmailFromAddress = 'info@eform.se', SFTPhost = null, SFTPpass = null, SFTPpath = null, SFTPuser = null, SendSurveyAsPdfTo = null, RoundKey = NEWID()
GO
UPDATE ProjectRoundLang SET InvitationSubject = 'InvitationSubject',InvitationBody = 'InvitationBody',ReminderSubject = 'ReminderSubject',ReminderBody = 'ReminderBody',SurveyName = 'SurveyName',SurveyIntro = 'SurveyIntro',UnitText = 'UnitText',ThankyouText = 'ThankyouText',ExtraInvitationSubject = 'ExtraInvitationSubject',ExtraInvitationBody = 'ExtraInvitationBody',ExtraReminderSubject = 'ExtraReminderSubject',ExtraReminderBody = 'ExtraReminderBody',InvitationSubjectJapaneseUnicode = 'InvitationSubjectJapaneseUnicode',InvitationBodyJapaneseUnicode = 'InvitationBodyJapaneseUnicode',ReminderSubjectJapaneseUnicode = 'ReminderSubjectJapaneseUnicode',ReminderBodyJapaneseUnicode = 'ReminderBodyJapaneseUnicode',SurveyNameJapaneseUnicode = 'SurveyNameJapaneseUnicode',SurveyIntroJapaneseUnicode = 'SurveyIntroJapaneseUnicode',UnitTextJapaneseUnicode = 'UnitTextJapaneseUnicode',ThankyouTextJapaneseUnicode = 'ThankyouTextJapaneseUnicode',ExtraInvitationSubjectJapaneseUnicode = 'ExtraInvitationSubjectJapaneseUnicode',ExtraInvitationBodyJapaneseUnicode = 'ExtraInvitationBodyJapaneseUnicode',ExtraReminderSubjectJapaneseUnicode = 'ExtraReminderSubjectJapaneseUnicode',ExtraReminderBodyJapaneseUnicode = 'ExtraReminderBodyJapaneseUnicode'
GO
UPDATE ProjectRoundUnit SET Unit = 'Unit' + CAST(ProjectRoundUnitID AS VARCHAR(5)),ID = ProjectRoundUnitID,UnitKey = NEWID(),SurveyIntro='SurveyIntro'+RTRIM(RIGHT(CAST(SurveyIntro AS VARCHAR(1024))+' ',1))
GO
UPDATE ProjectRoundUser SET UserKey = NEWID(), Email = 'info@eform.se', Name = 'Name'+RTRIM(RIGHT(CAST(Name AS VARCHAR(64))+' ',1)), Extra = 'Extra'+RTRIM(RIGHT(CAST(Extra AS VARCHAR(64))+' ',1)), ExternalID = 0*ExternalID
GO
UPDATE Sponsor SET Sponsor = 'Sponsor' + CAST(SponsorID AS VARCHAR(4)),UserIdent1 = 'UserIdent1'+RTRIM(RIGHT(CAST(UserIdent1 AS VARCHAR(64))+' ',1)),UserIdent2 = 'UserIdent2'+RTRIM(RIGHT(CAST(UserIdent2 AS VARCHAR(64))+' ',1)),UserIdent3 = 'UserIdent3'+RTRIM(RIGHT(CAST(UserIdent3 AS VARCHAR(64))+' ',1)),UserCheck1 = 'UserCheck1'+RTRIM(RIGHT(CAST(UserCheck1 AS VARCHAR(64))+' ',1)),UserCheck2 = 'UserCheck2'+RTRIM(RIGHT(CAST(UserCheck2 AS VARCHAR(64))+' ',1)),UserCheck3 = 'UserCheck3'+RTRIM(RIGHT(CAST(UserCheck3 AS VARCHAR(64))+' ',1)),UserIdent4 = 'UserIdent4'+RTRIM(RIGHT(CAST(UserIdent4 AS VARCHAR(64))+' ',1)),UserIdent5 = 'UserIdent5'+RTRIM(RIGHT(CAST(UserIdent5 AS VARCHAR(64))+' ',1)),UserIdent6 = 'UserIdent6'+RTRIM(RIGHT(CAST(UserIdent6 AS VARCHAR(64))+' ',1)),UserIdent7 = 'UserIdent7'+RTRIM(RIGHT(CAST(UserIdent7 AS VARCHAR(64))+' ',1)),UserIdent8 = 'UserIdent8'+RTRIM(RIGHT(CAST(UserIdent8 AS VARCHAR(64))+' ',1)),UserIdent9 = 'UserIdent9'+RTRIM(RIGHT(CAST(UserIdent9 AS VARCHAR(64))+' ',1)),UserIdent10 = 'UserIdent10'+RTRIM(RIGHT(CAST(UserIdent10 AS VARCHAR(64))+' ',1)),FeedbackEmailFrom = 'info@eform.se'+RTRIM(RIGHT(CAST(FeedbackEmailFrom AS VARCHAR(64))+' ',1)),FeedbackEmailSubject = 'FeedbackEmailSubject'+RTRIM(RIGHT(CAST(FeedbackEmailSubject AS VARCHAR(64))+' ',1)),FeedbackEmailBody = 'FeedbackEmailBody'+RTRIM(RIGHT(CAST(FeedbackEmailBody AS VARCHAR(64))+' ',1))
GO
UPDATE SponsorAdmin SET Username = 'Username' + CAST(SponsorAdminID AS VARCHAR(4)),Password='6abf93bd52365e24893aae8795ff7dbb',Name = 'Name',Email='info@eform.se'
GO
UPDATE [SponsorReminder] SET [Reminder] = 'Reminder',[FromEmail] = 'info@eform.se',[Subject]='Subject',[Body]='Body'
GO
UPDATE SponsorSuperAdmin SET Username = 'Username'+CAST(SponsorSuperAdminID AS VARCHAR(4)), Password='6abf93bd52365e24893aae8795ff7dbb'
GO
UPDATE [User] SET UserIdent1 = 'UserIdent1'+RTRIM(RIGHT(CAST(UserIdent1 AS VARCHAR(64))+' ',1)),UserIdent2 = 'UserIdent2'+RTRIM(RIGHT(CAST(UserIdent2 AS VARCHAR(64))+' ',1)),UserIdent3 = 'UserIdent3'+RTRIM(RIGHT(CAST(UserIdent3 AS VARCHAR(64))+' ',1)),UserIdent4 = 'UserIdent4'+RTRIM(RIGHT(CAST(UserIdent4 AS VARCHAR(64))+' ',1)),UserIdent5 = 'UserIdent5'+RTRIM(RIGHT(CAST(UserIdent5 AS VARCHAR(64))+' ',1)),UserIdent6 = 'UserIdent6'+RTRIM(RIGHT(CAST(UserIdent6 AS VARCHAR(64))+' ',1)),UserIdent7 = 'UserIdent7'+RTRIM(RIGHT(CAST(UserIdent7 AS VARCHAR(64))+' ',1)),UserIdent8 = 'UserIdent8'+RTRIM(RIGHT(CAST(UserIdent8 AS VARCHAR(64))+' ',1)),UserIdent9 = 'UserIdent9'+RTRIM(RIGHT(CAST(UserIdent9 AS VARCHAR(64))+' ',1)),UserIdent10 = 'UserIdent10'+RTRIM(RIGHT(CAST(UserIdent10 AS VARCHAR(64))+' ',1))
GO
UPDATE UserNote SET Note = 'Note', NoteJapaneseUnicode = 'Note'
GO
UPDATE UserSchedule SET Note = 'Note', Email = 'info@eform.se', NoteJapaneseUnicode = 'Note'
GO
UPDATE UserProjectRoundUser SET Note = 'Note'
GO
BACKUP LOG [eForm] TO DISK='NUL:'
DBCC SHRINKFILE(eForm_Log,100)
BACKUP DATABASE [eForm] TO DISK = N'c:\temp\eForm.dat' WITH NOFORMAT, NOINIT,  NAME = N'eForm', SKIP, NOREWIND, NOUNLOAD, STATS = 10
GO
USE healthWatch
GO
UPDATE Affiliate SET Affiliate = 'Affiliate'+CAST(AffiliateID AS VARCHAR(4))
GO
UPDATE Department SET Department = 'Department'+CAST(DepartmentID AS VARCHAR(5)), DepartmentShort = 'D'+CAST(DepartmentID AS VARCHAR(5))
GO
UPDATE Diary SET DiaryNote = 'DiaryNote'
GO
TRUNCATE TABLE ExerciseMiracle
GO
UPDATE Reminder SET Subject = 'Subject', Body = 'Body'
GO
TRUNCATE TABLE Session
GO
UPDATE Sponsor SET Sponsor='Sponsor'+CAST(SponsorID AS VARCHAR(4)),Application = 'Application'+CAST(SponsorID AS VARCHAR(4)),SponsorKey = NEWID(),InviteTxt = 'InviteTxt'+CAST(SponsorID AS VARCHAR(4)),InviteReminderTxt = 'InviteReminderTxt'+CAST(SponsorID AS VARCHAR(4)),LoginTxt = 'LoginTxt'+CAST(SponsorID AS VARCHAR(4)),InviteSubject = 'InviteSubject'+CAST(SponsorID AS VARCHAR(4)),InviteReminderSubject = 'InviteReminderSubject'+CAST(SponsorID AS VARCHAR(4)),LoginSubject = 'LoginSubject'+CAST(SponsorID AS VARCHAR(4)),TreatmentOfferText = 'TreatmentOfferText'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(TreatmentOfferText AS VARCHAR(64))+' ',1)),TreatmentOfferEmail = 'info@eform.se'+RTRIM(RIGHT(CAST(TreatmentOfferEmail AS VARCHAR(64))+' ',1)),TreatmentOfferIfNeededText = 'TreatmentOfferIfNeededText'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(TreatmentOfferIfNeededText AS VARCHAR(64))+' ',1)),InfoText = 'InfoText'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(InfoText AS VARCHAR(64))+' ',1)),ConsentText = 'ConsentText'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(ConsentText AS VARCHAR(64))+' ',1)),AlternativeTreatmentOfferText = 'AlternativeTreatmentOfferText'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(AlternativeTreatmentOfferText AS VARCHAR(64))+' ',1)),AlternativeTreatmentOfferEmail = 'info@eform.se'+RTRIM(RIGHT(CAST(AlternativeTreatmentOfferEmail AS VARCHAR(64))+' ',1)),SponsorApiKey = NEWID(),AllMessageSubject = 'AllMessageSubject'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(AllMessageSubject AS VARCHAR(64))+' ',1)),AllMessageBody = 'AllMessageBody'+CAST(SponsorID AS VARCHAR(4))+RTRIM(RIGHT(CAST(AllMessageBody AS VARCHAR(64))+' ',1))
GO
UPDATE SponsorAdmin SET Usr = 'Usr'+CAST(SponsorAdminID AS VARCHAR(4)),Pas = 'Pas'+CAST(SponsorAdminID AS VARCHAR(4)),Name = 'Name',Email='info@eform.se',[SponsorAdminKey] = NEWID()
GO
UPDATE SponsorExtendedSurvey SET Internal = 'Internal'+CAST(SponsorExtendedSurveyID AS VARCHAR(4)),RoundText = CAST(SponsorExtendedSurveyID AS VARCHAR(4)),EmailSubject = 'EmailSubject',EmailBody = 'EmailBody',IndividualFeedbackEmailSubject = 'IndividualFeedbackEmailSubject',IndividualFeedbackEmailBody = 'IndividualFeedbackEmailBody',ExtraEmailSubject = 'ExtraEmailSubject',ExtraEmailBody = 'ExtraEmailBody',FinishedEmailSubject = 'FinishedEmailSubject',FinishedEmailBody = 'FinishedEmailBody'
GO
UPDATE SponsorInvite SET Email = 'info@eform.se', InvitationKey = NEWID()
GO
UPDATE SponsorLang SET TreatmentOfferText = 'TreatmentOfferText'+CAST(SponsorLangID AS VARCHAR(4))+RTRIM(RIGHT(CAST(TreatmentOfferText AS VARCHAR(64))+' ',1)),TreatmentOfferIfNeededText = 'TreatmentOfferIfNeededText'+CAST(SponsorLangID AS VARCHAR(4))+RTRIM(RIGHT(CAST(TreatmentOfferIfNeededText AS VARCHAR(64))+' ',1)),AlternativeTreatmentOfferText = 'AlternativeTreatmentOfferText'+CAST(SponsorLangID AS VARCHAR(4))+RTRIM(RIGHT(CAST(AlternativeTreatmentOfferText AS VARCHAR(64))+' ',1))
GO
UPDATE SuperAdmin SET Password = 'Pass'+CAST(SuperAdminID AS VARCHAR(4)),Username = 'Username'+CAST(SuperAdminID AS VARCHAR(4))
GO
UPDATE SuperSponsor SET SuperSponsor = 'SuperSponsor'+CAST(SuperSponsorID AS VARCHAR(4))
GO
UPDATE SuperSponsorLang SET Slogan = 'Slogan'+CAST(SuperSponsorLangID AS VARCHAR(4)),Header = 'Header'+CAST(SuperSponsorLangID AS VARCHAR(4))
GO
UPDATE [User] SET Email = 'info@eform.se',Password = '170126491552140941271912365287179213233129',UserKey = NEWID(),Username='Username'+CAST(UserID AS VARCHAR(5)),AltEmail ='info@eform.se'+RTRIM(RIGHT(CAST(AltEmail AS VARCHAR(64))+' ',1))
GO
DELETE FROM UserMeasureComponent WHERE ValTxt IS NOT NULL
GO
BACKUP LOG [healthWatch] TO DISK='NUL:'
DBCC SHRINKFILE(healthWatch_Log,100)
BACKUP DATABASE [healthWatch] TO DISK = N'c:\temp\healthWatch.dat' WITH NOFORMAT, NOINIT,  NAME = N'healthWatch', SKIP, NOREWIND, NOUNLOAD, STATS = 10
GO
BACKUP LOG [healthWatchNews] TO DISK='NUL:'
DBCC SHRINKFILE(healthWatchNews_Log,100)
BACKUP DATABASE [healthWatchNews] TO DISK = N'c:\temp\healthWatchNews.dat' WITH NOFORMAT, NOINIT,  NAME = N'healthWatchNews', SKIP, NOREWIND, NOUNLOAD, STATS = 10