USE [healthWatch]
GO

/****** Object:  Table [dbo].[SponsorProject]    Script Date: 2016-04-02 13:04:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SponsorProject](
	[SponsorProjectID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorID] [int] NULL,
	[StartDT] [smalldatetime] NULL,
	[EndDT] [smalldatetime] NULL,
	[ProjectName] [nvarchar](max) NULL,
 CONSTRAINT [PK_SponsorProjectMeasure] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[SponsorProjectMeasure](
	[SponsorProjectMeasureID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorProjectID] [int] NULL,
	[MeasureID] [int] NULL,
 CONSTRAINT [PK_SponsorProjectMeasure_1] PRIMARY KEY CLUSTERED 
(
	[SponsorProjectMeasureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[UserSponsorProject](
	[UserSponsorProjectID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SponsorProjectID] [int] NULL,
	[ConsentDT] [smalldatetime] NULL,
 CONSTRAINT [PK_UserSponsorProject] PRIMARY KEY CLUSTERED 
(
	[UserSponsorProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

