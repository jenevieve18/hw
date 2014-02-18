USE [healthwatch]
GO

CREATE TABLE [dbo].[AdminNews](
 [AdminNewsID] [int] IDENTITY(1,1) NOT NULL,
 [DT] [datetime] NULL,
 [News] [nvarchar](max) NULL,
 CONSTRAINT [PK_AdminNews] PRIMARY KEY CLUSTERED 
(
 [AdminNewsID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE dbo.SponsorExtendedSurvey ADD Total int NULL
GO

UPDATE SponsorExtendedSurvey
SET SponsorExtendedSurvey.Total =
(SELECT COUNT(*) FROM SponsorInvite WHERE SponsorInvite.[Stopped] IS NULL AND SponsorInvite.SponsorID = SponsorExtendedSurvey.SponsorID)
WHERE SponsorExtendedSurvey.Total IS NULL OR SponsorExtendedSurvey.SponsorExtendedSurveyID IN
(SELECT s.SponsorExtendedSurveyID FROM SponsorExtendedSurvey s INNER JOIN eform..ProjectRound p ON s.ProjectRoundID = p.ProjectRoundID WHERE p.Closed IS NULL)