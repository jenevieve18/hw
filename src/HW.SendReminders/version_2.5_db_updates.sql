ALTER TABLE [healthWatch].[dbo].[SystemSettingsLang] ADD ReminderPushTitle NVARCHAR(MAX);
ALTER TABLE [healthWatch].[dbo].[SystemSettingsLang] ADD ReminderPushBody NVARCHAR(MAX);

UPDATE [healthWatch].[dbo].[SystemSettingsLang] SET ReminderPushTitle = 'HealthWatch', ReminderPushBody = 'Inloggningspåminnelse' WHERE LID = 1;
UPDATE [healthWatch].[dbo].[SystemSettingsLang] SET ReminderPushTitle = 'HealthWatch', ReminderPushBody = 'Login reminder' WHERE LID = 2;
UPDATE [healthWatch].[dbo].[SystemSettingsLang] SET ReminderPushTitle = 'HealthWatch', ReminderPushBody = 'Login reminder' WHERE LID = 4;
