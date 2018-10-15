USE [master]
GO

CREATE DATABASE [DeviceDb-ten2]
GO

USE [DeviceDb-ten2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices]
(
	[DeviceId] [uniqueidentifier] NOT NULL,
	[DeviceTitle] [nvarchar](max) NULL,
	CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NULL,
	[Token] [nvarchar](500) NULL,
	[LoginState] [nvarchar](50) NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Devices]
	([DeviceId], [DeviceTitle])
VALUES
	(N'a888caad-9860-49e6-92a2-c26101228f1b', N'Test Dev on Tenant 2')
GO
INSERT [dbo].[Users]
	([Id], [Username], [Password], [CreateDate], [Token], [LoginState])
VALUES
	(N'5d2bf8db-d979-43fd-a791-5fc870e3f024', N'Tenant2', N'Tenant2', CAST(N'2018-10-02T10:03:04.180' AS DateTime), NULL, NULL)
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
USE [master]
GO
ALTER DATABASE [DeviceDb-ten2] SET  READ_WRITE 
GO
