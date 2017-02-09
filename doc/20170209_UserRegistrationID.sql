USE [healthwatch]
GO

/****** Object:  Table [dbo].[UserRegistrationID]    Script Date: 02/09/2017 13:48:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserRegistrationID](
	[UserRegistrationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RegistrationID] [varchar](255) NULL,
	[PhoneName] [varchar](255) NULL,
	[Inactive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserRegistrationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

