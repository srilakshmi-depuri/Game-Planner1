USE [gameplanner1]
GO

/****** Object:  Table [dbo].[matches]    Script Date: 4/23/2024 7:21:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[matches](
	[gameName] [varchar](255) NOT NULL,
	[Team1] [varchar](255) NULL,
	[Team2] [varchar](255) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[matches]  WITH CHECK ADD FOREIGN KEY([gameName])
REFERENCES [dbo].[games] ([gameName])
GO


