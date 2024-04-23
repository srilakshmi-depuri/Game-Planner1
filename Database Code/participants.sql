USE [gameplanner1]
GO

/****** Object:  Table [dbo].[participants]    Script Date: 4/23/2024 7:22:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[participants](
	[participantId] [int] IDENTITY(1,1) NOT NULL,
	[gameName] [varchar](255) NOT NULL,
	[teamName] [varchar](255) NOT NULL,
	[participantName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[participantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[participants]  WITH CHECK ADD  CONSTRAINT [FK_participants_games] FOREIGN KEY([gameName])
REFERENCES [dbo].[games] ([gameName])
GO

ALTER TABLE [dbo].[participants] CHECK CONSTRAINT [FK_participants_games]
GO

ALTER TABLE [dbo].[participants]  WITH CHECK ADD  CONSTRAINT [FK_participants_teams] FOREIGN KEY([teamName])
REFERENCES [dbo].[teams] ([TeamName])
GO

ALTER TABLE [dbo].[participants] CHECK CONSTRAINT [FK_participants_teams]
GO


