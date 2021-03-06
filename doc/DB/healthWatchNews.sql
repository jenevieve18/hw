USE [healthWatchNews]
GO
/****** Object:  UserDefinedFunction [dbo].[cf_newsID2STR]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[cf_newsID2STR](@ID INT)
RETURNS VARCHAR(256)
AS
BEGIN
	DECLARE @RET VARCHAR(256), @TMP VARCHAR(4096), @CX INT

	SET @RET = ''
	SELECT @TMP = LOWER(Headline) FROM News WHERE NewsID = @ID
	
	SET @CX = LEN(@TMP)
	WHILE LEN(@TMP) > 0 BEGIN
		IF ASCII(@TMP) >= 97 AND ASCII(@TMP) <= 122
			SET @RET = @RET + LEFT(@TMP,1)
		IF ASCII(@TMP) = 228 OR ASCII(@TMP) = 229
			SET @RET = @RET + 'a'
		IF ASCII(@TMP) >= 246
			SET @RET = @RET + 'o'
		IF ASCII(@TMP) = 32
			IF @CX > 50 AND LEN(@RET) > 50
				SET @TMP = ''
			ELSE BEGIN
				SET @RET = @RET + '_'
				SET @TMP = SUBSTRING(@TMP,2,LEN(@TMP)-1)
			END
		ELSE
			SET @TMP = SUBSTRING(@TMP,2,LEN(@TMP)-1)
	END

	RETURN @RET

END

GO
/****** Object:  Table [dbo].[News]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[NewsCategoryID] [int] NULL,
	[Headline] [varchar](256) NULL,
	[DT] [smalldatetime] NULL,
	[Teaser] [varchar](1024) NULL,
	[Body] [text] NULL,
	[TeaserImageID] [int] NULL,
	[ImageID] [int] NULL,
	[Link] [varchar](256) NULL,
	[LinkText] [varchar](256) NULL,
	[LinkLangID] [int] NULL,
	[Published] [datetime] NULL,
	[Deleted] [datetime] NULL,
	[DirectFromFeed] [int] NULL,
	[OnlyInCategory] [int] NULL,
	[HeadlineShort] [varchar](256) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsCategory]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsCategory](
	[NewsCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[NewsCategory] [varchar](64) NULL,
	[OnlyDirectFromFeed] [int] NULL,
	[NewsCategoryShort] [varchar](16) NULL,
 CONSTRAINT [PK_NewsCategory] PRIMARY KEY CLUSTERED 
(
	[NewsCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsCategoryLang]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsCategoryLang](
	[NewsCategoryLangID] [int] IDENTITY(1,1) NOT NULL,
	[NewsCategoryID] [int] NULL,
	[LangID] [int] NULL,
	[NewsCategory] [varchar](255) NULL,
 CONSTRAINT [PK_NewsCategoryLang] PRIMARY KEY CLUSTERED 
(
	[NewsCategoryLangID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsChannel]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsChannel](
	[channelID] [int] IDENTITY(1,1) NOT NULL,
	[sourceID] [int] NULL,
	[feed] [varchar](255) NULL,
	[langID] [int] NULL,
	[pause] [smalldatetime] NULL,
	[NewsCategoryID] [int] NULL,
	[internal] [varchar](255) NULL,
 CONSTRAINT [PK_channel] PRIMARY KEY CLUSTERED 
(
	[channelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsImage]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsImage](
	[NewsImageID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](1024) NULL,
	[Filename] [varchar](256) NULL,
	[Alt] [varchar](1024) NULL,
	[Ext] [varchar](64) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Size] [int] NULL,
 CONSTRAINT [PK_NewsImage] PRIMARY KEY CLUSTERED 
(
	[NewsImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsRead]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsRead](
	[NewsReadID] [bigint] IDENTITY(1,1) NOT NULL,
	[NewsID] [int] NULL,
	[SessionID] [int] NULL,
	[DT] [smalldatetime] NULL,
 CONSTRAINT [PK_NewsRead] PRIMARY KEY CLUSTERED 
(
	[NewsReadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsRSS]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsRSS](
	[rssID] [int] IDENTITY(1,1) NOT NULL,
	[channelID] [int] NULL,
	[link] [varchar](512) NULL,
	[altlink] [varchar](512) NULL,
	[title] [varchar](512) NULL,
	[description] [text] NULL,
	[dt] [smalldatetime] NULL,
	[deleted] [bit] NULL,
	[item] [text] NULL,
	[checksum] [varchar](64) NULL,
 CONSTRAINT [PK_rss] PRIMARY KEY CLUSTERED 
(
	[rssID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsRSStmp]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsRSStmp](
	[rssID] [int] IDENTITY(1,1) NOT NULL,
	[channelID] [int] NULL,
	[link] [varchar](512) NULL,
	[altlink] [varchar](512) NULL,
	[title] [varchar](512) NULL,
	[description] [text] NULL,
	[dt] [smalldatetime] NULL,
	[deleted] [bit] NULL,
	[item] [text] NULL,
	[checksum] [varchar](64) NULL,
 CONSTRAINT [PK_rss_tmp] PRIMARY KEY CLUSTERED 
(
	[rssID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NewsSource]    Script Date: 2017-06-20 11:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NewsSource](
	[sourceID] [int] IDENTITY(1,1) NOT NULL,
	[source] [varchar](256) NULL,
	[sourceShort] [varchar](64) NULL,
	[favourite] [int] NULL,
 CONSTRAINT [PK_source] PRIMARY KEY CLUSTERED 
(
	[sourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[NewsRSS] ADD  CONSTRAINT [DF_rss_deleted]  DEFAULT ((0)) FOR [deleted]
GO
ALTER TABLE [dbo].[NewsRSStmp] ADD  CONSTRAINT [DF_rss_tmp_deleted]  DEFAULT ((0)) FOR [deleted]
GO
