USE [master]
GO

CREATE DATABASE [DeviceDb]
GO

USE [DeviceDb]
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
	(N'53bf97de-6226-4d64-b4ab-c38858580f2d', N'Test Dev on Tenant 1')
GO
INSERT [dbo].[Users]
	([Id], [Username], [Password], [CreateDate], [Token], [LoginState])
VALUES
	(N'67cf7924-6e29-4380-a6a9-e473f4c83df8', N'Tenant1', N'Tenant1', GETDATE(), NULL, NULL)
GO
