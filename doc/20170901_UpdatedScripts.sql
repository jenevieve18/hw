USE [healthWatch]
GO

-- Use for GRP-WS.
CREATE TABLE [dbo].[SponsorAdminToken](
	[SponsorAdminToken] [uniqueidentifier] NOT NULL,
	[SponsorAdminID] [int] NOT NULL,
	[Expires] [smalldatetime] NOT NULL,
	[SessionID] [int] NULL,
 CONSTRAINT [PK_SponsorAdminToken] PRIMARY KEY CLUSTERED 
(
	[SponsorAdminToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SponsorAdminToken] ADD  CONSTRAINT [DF_SponsorAdminToken_SponsorAdminToken]  DEFAULT (newid()) FOR [SponsorAdminToken]
GO

-- Use for SAML login.
ALTER TABLE SponsorAdmin ADD ExternalUserKey varchar(50);
GO

ALTER TABLE Sponsor 
ADD HideEmail int
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Use for IDP
CREATE TABLE [dbo].[Realm](
[SponsorId] [int] NULL,
[RealmType] [int] NULL,
[RealmIdentifier] varchar(50) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Realm] WITH CHECK ADD CONSTRAINT [FK_Realm_Sponsor] FOREIGN KEY([SponsorId])
REFERENCES [dbo].[Sponsor] ([SponsorID])
GO

ALTER TABLE [dbo].[Realm] CHECK CONSTRAINT [FK_Realm_Sponsor]
GO

ALTER TABLE Realm 
ADD IdpUrl VARCHAR(255), 
IdpCertificate TEXT;
GO

ALTER TABLE SponsorInvite 
ADD ExternalUserKey VARCHAR(50)
GO

ALTER TABLE Sponsor 
ADD SAMLOnly int 
GO