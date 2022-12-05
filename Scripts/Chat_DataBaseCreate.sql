CREATE DATABASE [ChatDB]

GO

USE [ChatDB]

GO

CREATE TABLE [dbo].[Memberships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Memberships] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]

]GO

CREATE TABLE [dbo].[ChatRooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[CreatorId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ChatRooms] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO

ALTER TABLE [dbo].[ChatRooms]  WITH CHECK ADD  CONSTRAINT [FK_ChatRooms_Memberships_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Memberships] ([Id])
GO

ALTER TABLE [dbo].[ChatRooms] CHECK CONSTRAINT [FK_ChatRooms_Memberships_CreatorId]

GO

CREATE TABLE [dbo].[ChatMessages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](256) NOT NULL,
	[CreatorId] [int] NOT NULL,
	[ChatRoomId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ChatMessages] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO

ALTER TABLE [dbo].[ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessages_ChatRooms_ChatRoomId] FOREIGN KEY([ChatRoomId])
REFERENCES [dbo].[ChatRooms] ([Id])
GO

ALTER TABLE [dbo].[ChatMessages] CHECK CONSTRAINT [FK_ChatMessages_ChatRooms_ChatRoomId]
GO

ALTER TABLE [dbo].[ChatMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatMessages_Memberships_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Memberships] ([Id])
GO

ALTER TABLE [dbo].[ChatMessages] CHECK CONSTRAINT [FK_ChatMessages_Memberships_CreatorId]
GO

USE [ChatDB]
GO

DECLARE @MembershipId as INT;

INSERT INTO [dbo].[Memberships]
           ([Name]
           ,[Email]
           ,[Password]
           ,[CreatedAt])
     VALUES
           ('System'
           ,'system@email.com'
           ,'123456'
           ,GETDATE())

SET @MembershipId = SCOPE_IDENTITY();

INSERT INTO [dbo].[ChatRooms]
           ([Name]
           ,[CreatorId]
           ,[CreatedAt])
     VALUES
           ('Main'
           ,@MembershipId
           ,GetDate())
GO

