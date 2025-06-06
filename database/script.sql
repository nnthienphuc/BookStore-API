USE [master]
GO
/****** Object:  Database [BOOK_STORE_API]    Script Date: 14-May-25 5:11:32 PM ******/
CREATE DATABASE [BOOK_STORE_API]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BOOK_STORE_API', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BOOK_STORE_API.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BOOK_STORE_API_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BOOK_STORE_API_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BOOK_STORE_API] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BOOK_STORE_API].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BOOK_STORE_API] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET ARITHABORT OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BOOK_STORE_API] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BOOK_STORE_API] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BOOK_STORE_API] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BOOK_STORE_API] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET RECOVERY FULL 
GO
ALTER DATABASE [BOOK_STORE_API] SET  MULTI_USER 
GO
ALTER DATABASE [BOOK_STORE_API] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BOOK_STORE_API] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BOOK_STORE_API] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BOOK_STORE_API] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BOOK_STORE_API] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BOOK_STORE_API] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BOOK_STORE_API', N'ON'
GO
ALTER DATABASE [BOOK_STORE_API] SET QUERY_STORE = ON
GO
ALTER DATABASE [BOOK_STORE_API] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BOOK_STORE_API]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Book](
	[id] [uniqueidentifier] NOT NULL,
	[isbn] [char](13) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[categoryId] [uniqueidentifier] NOT NULL,
	[authorId] [uniqueidentifier] NOT NULL,
	[publisherId] [uniqueidentifier] NOT NULL,
	[yearOfPublication] [smallint] NOT NULL,
	[price] [decimal](8, 0) NOT NULL,
	[image] [varchar](255) NOT NULL,
	[quantity] [int] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [uniqueidentifier] NOT NULL,
	[familyName] [nvarchar](70) NOT NULL,
	[givenName] [nvarchar](30) NOT NULL,
	[dateOfBirth] [date] NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[phone] [char](10) NOT NULL,
	[gender] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [uniqueidentifier] NOT NULL,
	[staffId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[promotionId] [uniqueidentifier] NULL,
	[createdTime] [datetime2](7) NOT NULL,
	[totalAmount] [decimal](11, 3) NOT NULL,
	[subTotalAmount] [decimal](11, 3) NOT NULL,
	[promotionAmount] [decimal](11, 3) NOT NULL,
	[status] [bit] NOT NULL,
	[note] [nvarchar](max) NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[orderId] [uniqueidentifier] NOT NULL,
	[bookId] [uniqueidentifier] NOT NULL,
	[quantity] [smallint] NOT NULL,
	[price] [decimal](8, 0) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[bookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promotion]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promotion](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[startDate] [datetime2](7) NOT NULL,
	[endDate] [datetime2](7) NOT NULL,
	[condition] [decimal](8, 0) NOT NULL,
	[discountPercent] [decimal](3, 2) NOT NULL,
	[quantity] [smallint] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 14-May-25 5:11:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [uniqueidentifier] NOT NULL,
	[familyName] [nvarchar](70) NOT NULL,
	[givenName] [nvarchar](30) NOT NULL,
	[dateOfBirth] [date] NOT NULL,
	[address] [nvarchar](50) NOT NULL,
	[phone] [char](10) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[citizenIdentification] [char](12) NOT NULL,
	[hashPassword] [varchar](255) NOT NULL,
	[role] [bit] NOT NULL,
	[gender] [bit] NOT NULL,
	[isActived] [bit] NOT NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'Nguyễn Nhật Ánh', 0)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'Thạch Lam', 0)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'bc4ce17f-a2d8-44b8-995a-8834385bb24d', N'Thạch Lm', 1)
GO
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'5edd82ec-df87-44f9-ad8b-d9fa1f2ea8bc', N'8934974188811', N'This book already deleted', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2023, CAST(200000 AS Decimal(8, 0)), N'/images/books/toiLaBeto.png', 10, 1)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'ddfde72a-7e11-49d5-89fb-e2766610c1cb', N'8934974188841', N'Tôi Là Bêtô', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2023, CAST(200000 AS Decimal(8, 0)), N'/images/books/toiLaBeto.png', 9, 0)
GO
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'Văn Học', 1)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'Tiểu Thuyết', 0)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'30c742ff-a0f7-4de4-a6ce-ec51415fd6ee', N'Deleted cate', 1)
GO
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'4259c85a-c5dd-4d78-aab4-e6bc71bdad9e', N'Con', N'Ngân', CAST(N'2003-12-21' AS Date), N'97 Gia Lai', N'0987654300', 0, 1)
GO
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'dd650a6d-8471-4ed2-8259-5ba82c7414e5', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'4259c85a-c5dd-4d78-aab4-e6bc71bdad9e', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-05-14T16:58:07.2123698' AS DateTime2), CAST(170000.000 AS Decimal(11, 3)), CAST(200000.000 AS Decimal(11, 3)), CAST(30000.000 AS Decimal(11, 3)), 1, N'Sách bị rách', 1)
GO
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'dd650a6d-8471-4ed2-8259-5ba82c7414e5', N'ddfde72a-7e11-49d5-89fb-e2766610c1cb', 1, CAST(200000 AS Decimal(8, 0)), 1)
GO
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'a38f02af-6754-40fd-bba1-3607bf60427a', N'Khuyến Mãi Mùa hè', CAST(N'2025-05-14T00:00:00.0000000' AS DateTime2), CAST(N'2025-08-16T00:00:00.0000000' AS DateTime2), CAST(120000 AS Decimal(8, 0)), CAST(0.15 AS Decimal(3, 2)), 9, 0)
GO
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'f06bb8bc-857c-40c5-a60a-0162f7ce1e5e', N'Deleted publisher', 1)
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'2d64142c-1280-48e1-9207-c7a0023fa88f', N'Nhà xuất bản Kim Đồng', 0)
GO
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'fa5e2677-b1b5-438d-af63-140ac1eb4745', N'PTIT', N'Phuc', CAST(N'2003-08-16' AS Date), N'Dong Nai', N'0397357111', N'n21dccn066@student.ptithcm.edu.vn', N'012345678111', N'$2a$11$u/QNr7jZ8KsG/0veiyvb3eMaenNf/9.frd9f5yBKmtvduy0HSWZr2', 0, 0, 1, 0)
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'Nguyen', N'Phuc', CAST(N'2003-08-16' AS Date), N'Dong Nai', N'0397357001', N'phucnaoto@gmail.com', N'012345678901', N'$2a$11$a8ID1eZIy4y.Dz6qy0SLTuH8mWe1xcPUgfycCnLBAZ7Yjb1Kcs/CO', 1, 0, 1, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Book]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [IX_Book] UNIQUE NONCLUSTERED 
(
	[isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Category]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [IX_Category] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [IX_Customer] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Promotion]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [IX_Promotion] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Publisher]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Publisher] ADD  CONSTRAINT [IX_Publisher] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_CI]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [IX_Staff_CI] UNIQUE NONCLUSTERED 
(
	[citizenIdentification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_Email]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [IX_Staff_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_Phone]    Script Date: 14-May-25 5:11:32 PM ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [IX_Staff_Phone] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Author] ADD  CONSTRAINT [DF_Author_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Book_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_gender]  DEFAULT ((0)) FOR [gender]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_created_time]  DEFAULT (sysdatetime()) FOR [createdTime]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[OrderItem] ADD  CONSTRAINT [DF_OrderItem_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [DF_Promotion_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [DF_Promotion_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Publisher] ADD  CONSTRAINT [DF_Publisher_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Publisher] ADD  CONSTRAINT [DF_Publisher_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_id]  DEFAULT (newid()) FOR [id]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_role]  DEFAULT ((0)) FOR [role]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_gender]  DEFAULT ((0)) FOR [gender]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_isActived]  DEFAULT ((0)) FOR [isActived]
GO
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [DF_Staff_is_delete]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([authorId])
REFERENCES [dbo].[Author] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Publisher] FOREIGN KEY([publisherId])
REFERENCES [dbo].[Publisher] ([id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Publisher]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Promotion] FOREIGN KEY([promotionId])
REFERENCES [dbo].[Promotion] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Promotion]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Staff] FOREIGN KEY([staffId])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Staff]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Book] FOREIGN KEY([bookId])
REFERENCES [dbo].[Book] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Book]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [CK_Book_Price] CHECK  (([price]>(1000)))
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [CK_Book_Price]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [CK_Book_Quantity] CHECK  (([quantity]>=(0)))
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [CK_Book_Quantity]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [CK_Book_YOP] CHECK  (([yearOfPublication]>(1500)))
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [CK_Book_YOP]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [CK_OrderItem_Price] CHECK  (([price]>(1000)))
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [CK_OrderItem_Price]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [CK_OrderItem_Quantity] CHECK  (([quantity]>(0)))
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [CK_OrderItem_Quantity]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [CK_Promotion_Condition] CHECK  (([condition]>(1000)))
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [CK_Promotion_Condition]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [CK_Promotion_Date] CHECK  (([endDate]>[startDate]))
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [CK_Promotion_Date]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [CK_Promotion_DP] CHECK  (([discountPercent]>(0.0)))
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [CK_Promotion_DP]
GO
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [CK_Promotion_Quantity] CHECK  (([quantity]>=(0)))
GO
ALTER TABLE [dbo].[Promotion] CHECK CONSTRAINT [CK_Promotion_Quantity]
GO
USE [master]
GO
ALTER DATABASE [BOOK_STORE_API] SET  READ_WRITE 
GO
