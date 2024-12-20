USE [BookAPIDb]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 17.11.2024 21:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id_Books] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Genre_ID] [nvarchar](max) NOT NULL,
	[GenresId_Genres_Books] [int] NOT NULL,
	[PublicationYear] [int] NOT NULL,
	[AvailableCopies] [int] NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id_Books] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 17.11.2024 21:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[Id_Genres_Books] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[Id_Genres_Books] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History_Book_Rentals]    Script Date: 17.11.2024 21:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History_Book_Rentals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Books_Id] [int] NOT NULL,
	[Reader_ID] [int] NOT NULL,
	[RentalDate] [datetime2](7) NOT NULL,
	[ReturnDate] [datetime2](7) NULL,
	[DueDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_History_Book_Rentals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 17.11.2024 21:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Readers](
	[Id_Readers] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[ContactInfo] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Readers] PRIMARY KEY CLUSTERED 
(
	[Id_Readers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id_Books], [Title], [Description], [Author], [Genre_ID], [GenresId_Genres_Books], [PublicationYear], [AvailableCopies], [DateAdded]) VALUES (1, N'string', N'string', N'string', N'string', 2, 0, 0, CAST(N'2024-10-21T21:18:42.0180000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([Id_Genres_Books], [Name]) VALUES (2, N'string')
INSERT [dbo].[Genres] ([Id_Genres_Books], [Name]) VALUES (3, N'string')
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Readers] ON 

INSERT [dbo].[Readers] ([Id_Readers], [FirstName], [LastName], [DateOfBirth], [ContactInfo]) VALUES (1, N'Kamalov', N'Ramil', CAST(N'2005-12-10T00:00:00.0000000' AS DateTime2), N'ramilka')
SET IDENTITY_INSERT [dbo].[Readers] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Genres_GenresId_Genres_Books] FOREIGN KEY([GenresId_Genres_Books])
REFERENCES [dbo].[Genres] ([Id_Genres_Books])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Genres_GenresId_Genres_Books]
GO
ALTER TABLE [dbo].[History_Book_Rentals]  WITH CHECK ADD  CONSTRAINT [FK_History_Book_Rentals_Books_Books_Id] FOREIGN KEY([Books_Id])
REFERENCES [dbo].[Books] ([Id_Books])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[History_Book_Rentals] CHECK CONSTRAINT [FK_History_Book_Rentals_Books_Books_Id]
GO
ALTER TABLE [dbo].[History_Book_Rentals]  WITH CHECK ADD  CONSTRAINT [FK_History_Book_Rentals_Readers_Reader_ID] FOREIGN KEY([Reader_ID])
REFERENCES [dbo].[Readers] ([Id_Readers])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[History_Book_Rentals] CHECK CONSTRAINT [FK_History_Book_Rentals_Readers_Reader_ID]
GO
