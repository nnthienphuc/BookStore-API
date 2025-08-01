USE [master]
GO
/****** Object:  Database [BOOK_STORE_API]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Author]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Book]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[OrderItem]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Promotion]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Publisher]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
/****** Object:  Table [dbo].[Staff]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'Nam Cao', 0)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'eae1cb0d-2104-4f68-884d-562675b04f1a', N'Fujiko F Fujio', 0)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'bc4ce17f-a2d8-44b8-995a-8834385bb24d', N'Deleted Author', 1)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'89f6fb1e-d679-4bd3-8da6-b7a08df522d2', N'Thiên Phúc', 0)
INSERT [dbo].[Author] ([id], [name], [isDeleted]) VALUES (N'dcc14c9f-4974-459f-9e5c-fe2652c2b994', N'Thùy Ngâ', 1)
GO
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'358808bc-9c89-4431-8a5f-228bbbc86a6d', N'8935212366533', N'Gió Lạnh Đầu Mùa (Tái Bản 2024)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'79994ae3-d421-4b92-b895-323c19e12db9', 2024, CAST(59000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/9/7/9786043079517.jpg', 20, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'fb1e00c7-bbe2-46c4-a987-24ad892a9072', N'8934974164135', N'Làm bạn với bầu trời', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'79994ae3-d421-4b92-b895-323c19e12db9', 2019, CAST(92400 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/u/n/untitled-2_44.jpg', 37, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'816d2a16-2316-4a73-b6d7-3147db173fdc', N'8934974201500', N'Thiên Phúc và các bạn', N'e0a0e347-3265-4b1e-9368-e1e65421c54a', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(19000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974201571.jpg', 100, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'55c70857-c0a9-4710-9ed5-34d6abb2bd10', N'8935244822571', N'Văn Học Trong Nhà Trường - Gió Lạnh Đầu Mùa (Tái Bản 2019)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'79994ae3-d421-4b92-b895-323c19e12db9', 2019, CAST(44000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/9/7/9786043079517.jpg', 15, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'0b72ee3b-7e66-4cba-b5ee-3fcc947b3a03', N'8934974178194', N'Bong Bóng Lên Trời (2022)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(95000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974178194.jpg', 76, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'77a42cbf-c1f8-442f-b0c0-3ff68de98fba', N'5758795777321', N'Test 1', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2000, CAST(10000 AS Decimal(8, 0)), N'https://tailieuhust.com/wp-content/uploads/2023/05/Khoa-hoc-Lap-trinh-C-1024x576.png', 0, 1)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'c862fb23-a2d2-42d2-8253-44824c4dfecc', N'9786043077940', N'Hai Đứa Trẻ (Tái Bản 2024)', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'79994ae3-d421-4b92-b895-323c19e12db9', 2024, CAST(150000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/flashmagazine/images/page_images/hai_dua_tre/2024_06_10_11_08_25_8-390x510.jpg', 0, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'643db72a-f24b-4e07-9e16-458db1f4b135', N'8934974178682', N'Phòng Trọ Ba Người (Tái Bản 2022)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(45000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974177982.jpg', 13, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'e56476b5-2bc9-4908-9ba9-57dafa5959a6', N'8935244864427', N'Kính vạn hoa', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'79994ae3-d421-4b92-b895-323c19e12db9', 2022, CAST(110700 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/k/i/kinh-van-hoa---phien-ban-moi---tap-15---tb-2022.jpg', 39, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'e2ccf867-c124-4e33-9b48-5cca2fb7a7f0', N'8934974178620', N'Hoa Hồng Xứ Khác (Tái Bản 2022)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(50000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974178620.jpg', 20, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'686f2b9e-5da2-4078-bb27-61fbeadda342', N'9786043725889', N'Đời Thừa (Tái Bản 2022)', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'79994ae3-d421-4b92-b895-323c19e12db9', 2022, CAST(63000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/9/7/9786044916958.jpg', 8, 1)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'35170fc7-a012-4baf-a15a-644a2121957f', N'9786043947465', N'Ngày Mới', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'79994ae3-d421-4b92-b895-323c19e12db9', 2024, CAST(83000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/flashmagazine/images/page_images/ngay_moi/2024_06_11_10_32_42_7-390x510.jpg', 16, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'66d85958-f12d-43af-9827-684fe9e387d4', N'9786044916927', N'Sống Mòn', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'79994ae3-d421-4b92-b895-323c19e12db9', 2024, CAST(76000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/9/7/9786044916927.jpg', 35, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'7ae62fcb-f5c5-4ea9-985f-81914c4a97bd', N'8934974187622', N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh (Tái Bản 2023)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2023, CAST(150000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/n/n/nna-hvtcx.jpg', 10, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'616af302-3f04-4d40-b0be-87142f537b2f', N'4381316315685', N'test', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 1901, CAST(10000 AS Decimal(8, 0)), N'https://tailieuhust.com/wp-content/uploads/2023/05/Khoa-hoc-Lap-trinh-C-1024x576.png', 10, 1)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'5c558548-e6b9-424c-9949-8ef7cf63e1ac', N'8935230001218', N'Danh Tác Việt Nam - Chí Phèo (Tái Bản 2025)', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2025, CAST(98000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/v/n/vn-11134207-7ra0g-m6nr4tgwi1k706.jpg', 13, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'9d22dade-e1b4-4a2a-9b9e-a567f839f189', N'8934974177319', N'Những Chàng Trai Xấu Tính (Tái Bản 2022)', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(85000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974178651.jpg', 46, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'b11ff973-84f6-4adb-9c31-b6cc0fe93fdf', N'8936203362206', N'Gió Đầu Mùa & Hà Nội Băm Sáu Phố Phường', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'09189d44-9a03-44fa-a026-3704ad76ffd8', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2022, CAST(160000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8936203362206.jpg', 33, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'ad1fcaaf-6b6a-419f-95af-c29f8ec9982e', N'8934974209393', N'Nguyễn Nhật Ánh - Người Giữ Lửa Cho Văn Học Thiếu Nhi', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2025, CAST(277000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/8/9/8934974209393.jpg', 100, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'da8d2d28-ec88-4514-b7c4-c8d19a43b857', N'8935095623945', N'Chí Phèo (2017)', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2017, CAST(40000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/9/7/9786044916972.jpg', 19, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'4bfd61cb-691d-49fc-acf4-d91aedd5fbf6', N'8934974170600', N'Con Chim Xanh Biếc Bay Về', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'79994ae3-d421-4b92-b895-323c19e12db9', 2020, CAST(202500 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/b/i/biamem.jpg', 70, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'5edd82ec-df87-44f9-ad8b-d9fa1f2ea8bc', N'8934974188811', N'This book already deleted', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2023, CAST(200000 AS Decimal(8, 0)), N'/images/books/toiLaBeto.png', 10, 1)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'ddfde72a-7e11-49d5-89fb-e2766610c1cb', N'8934974188841', N'Tôi Là Bêtô', N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'91396c60-f995-40ef-b732-2b1bc90bf66c', N'2d64142c-1280-48e1-9207-c7a0023fa88f', 2023, CAST(200000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/n/x/nxbtre_full_24352023_043531.jpg', 8, 0)
INSERT [dbo].[Book] ([id], [isbn], [title], [categoryId], [authorId], [publisherId], [yearOfPublication], [price], [image], [quantity], [isDeleted]) VALUES (N'd1284f66-fa38-4c68-8d3d-fea26cd44cd8', N'8935230007685', N'Giăng Sáng', N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'9d988694-ec03-4141-8f41-3fddce6bf4e7', N'79994ae3-d421-4b92-b895-323c19e12db9', 2016, CAST(59000 AS Decimal(8, 0)), N'https://cdn1.fahasa.com/media/catalog/product/i/m/image_110145.jpg', 34, 0)
GO
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'a559103b-19d2-4766-9a98-2486e9a0d431', N'Test 30-5', 1)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'aacac6e9-cdf4-4e53-9ddc-309e776c8612', N'Văn Học', 0)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'e2bc6d43-b35c-4b43-9215-8e186ffa60c0', N'Tiểu Thuyết', 0)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'714c0770-bb88-406c-974f-8fe95aa0f6ff', N'Test Category', 1)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'e0a0e347-3265-4b1e-9368-e1e65421c54a', N'Thien Phuc', 0)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'e2a5d950-76a5-45d8-a3cb-e28470a099f1', N'Truyện cổ tích', 0)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'30c742ff-a0f7-4de4-a6ce-ec51415fd6ee', N'Deleted cate', 1)
INSERT [dbo].[Category] ([id], [name], [isDeleted]) VALUES (N'35b44e59-daf3-4561-8a4f-efa0ff9f629e', N'Truyện Tranh', 0)
GO
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'13810a11-3a0d-4c80-be69-0184cab1c814', N'Nguyễn Ngọc Huyền', N'Trân', CAST(N'1997-11-09' AS Date), N'Phan Thiết, Bình Thuận', N'0978654321', 1, 0)
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'c14f035d-3bbb-4f6b-8ae3-420d7186f222', N'Nguyễn Ngọc Thiên ', N'Kim', CAST(N'2001-06-01' AS Date), N'LonDon', N'0989998887', 1, 0)
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'8525e0fe-8ad9-4ca7-9ef4-603e1daf78ec', N'Thùy', N'Ngân', CAST(N'2003-03-01' AS Date), N'Gia Lai', N'0987898767', 1, 0)
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'01e3f4e0-6d99-4853-8c4e-8e518d6ed732', N'Nguyễn', N'Ngân', CAST(N'0001-01-01' AS Date), N'Địa chỉ', N'0369852147', 1, 0)
INSERT [dbo].[Customer] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [gender], [isDeleted]) VALUES (N'4259c85a-c5dd-4d78-aab4-e6bc71bdad9e', N'Trần', N'Ngân', CAST(N'2003-12-21' AS Date), N'97 Gia Lai', N'0987654300', 1, 1)
GO
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'ee528f3f-744c-45f0-9320-10ecfe6d7eea', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'13810a11-3a0d-4c80-be69-0184cab1c814', NULL, CAST(N'2025-06-01T15:21:45.9031231' AS DateTime2), CAST(44000.000 AS Decimal(11, 3)), CAST(44000.000 AS Decimal(11, 3)), CAST(0.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'f744a73a-d102-41d2-85b9-3929fcdbb909', N'a0ef187d-3284-49a2-b6fc-9b6fef273bfd', N'13810a11-3a0d-4c80-be69-0184cab1c814', NULL, CAST(N'2025-06-01T15:00:06.2489329' AS DateTime2), CAST(92400.000 AS Decimal(11, 3)), CAST(92400.000 AS Decimal(11, 3)), CAST(0.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'3f610f90-418f-48b3-b182-40413962a22e', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-05-29T16:45:32.7669535' AS DateTime2), CAST(92400.000 AS Decimal(11, 3)), CAST(92400.000 AS Decimal(11, 3)), CAST(0.000 AS Decimal(11, 3)), 0, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'dd650a6d-8471-4ed2-8259-5ba82c7414e5', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'4259c85a-c5dd-4d78-aab4-e6bc71bdad9e', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-05-14T16:58:07.2123698' AS DateTime2), CAST(170000.000 AS Decimal(11, 3)), CAST(200000.000 AS Decimal(11, 3)), CAST(30000.000 AS Decimal(11, 3)), 1, N'Sách bị rách', 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'714d5f3a-5313-497d-9e0b-6e0793594f0b', N'a0ef187d-3284-49a2-b6fc-9b6fef273bfd', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-06-01T14:57:19.1559946' AS DateTime2), CAST(127500.000 AS Decimal(11, 3)), CAST(150000.000 AS Decimal(11, 3)), CAST(22500.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'364f16ff-04af-40d1-9574-954ff78b467f', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-06-01T11:16:41.6240829' AS DateTime2), CAST(613785.000 AS Decimal(11, 3)), CAST(722100.000 AS Decimal(11, 3)), CAST(108315.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'1f2c4053-d6a8-47ca-a4ac-a90f61e5e79e', N'd79863c2-de29-4e2b-8598-a2a30023b472', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-06-01T14:46:07.2359752' AS DateTime2), CAST(149600.000 AS Decimal(11, 3)), CAST(176000.000 AS Decimal(11, 3)), CAST(26400.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'69759f71-416a-4bc7-ba10-c61dbc5cdec0', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a87d963e-4c3e-491c-8c43-ab3e4792cf10', CAST(N'2025-05-31T10:55:57.8316032' AS DateTime2), CAST(238000.000 AS Decimal(11, 3)), CAST(238000.000 AS Decimal(11, 3)), CAST(0.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'db75f252-706a-4480-ab55-ce8b4f7197b9', N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-05-29T16:42:08.2047317' AS DateTime2), CAST(115940.000 AS Decimal(11, 3)), CAST(136400.000 AS Decimal(11, 3)), CAST(20460.000 AS Decimal(11, 3)), 1, NULL, 0)
INSERT [dbo].[Order] ([id], [staffId], [customerId], [promotionId], [createdTime], [totalAmount], [subTotalAmount], [promotionAmount], [status], [note], [isDeleted]) VALUES (N'f77f3581-0da0-4d5c-98e3-e99406bd2240', N'd79863c2-de29-4e2b-8598-a2a30023b472', N'13810a11-3a0d-4c80-be69-0184cab1c814', N'a38f02af-6754-40fd-bba1-3607bf60427a', CAST(N'2025-05-17T16:39:02.4588768' AS DateTime2), CAST(1105850.000 AS Decimal(11, 3)), CAST(1301000.000 AS Decimal(11, 3)), CAST(195150.000 AS Decimal(11, 3)), 1, NULL, 0)
GO
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'ee528f3f-744c-45f0-9320-10ecfe6d7eea', N'55c70857-c0a9-4710-9ed5-34d6abb2bd10', 1, CAST(44000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'f744a73a-d102-41d2-85b9-3929fcdbb909', N'fb1e00c7-bbe2-46c4-a987-24ad892a9072', 1, CAST(92400 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'3f610f90-418f-48b3-b182-40413962a22e', N'fb1e00c7-bbe2-46c4-a987-24ad892a9072', 1, CAST(92400 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'dd650a6d-8471-4ed2-8259-5ba82c7414e5', N'ddfde72a-7e11-49d5-89fb-e2766610c1cb', 1, CAST(200000 AS Decimal(8, 0)), 1)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'714d5f3a-5313-497d-9e0b-6e0793594f0b', N'c862fb23-a2d2-42d2-8253-44824c4dfecc', 1, CAST(150000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'364f16ff-04af-40d1-9574-954ff78b467f', N'c862fb23-a2d2-42d2-8253-44824c4dfecc', 2, CAST(150000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'364f16ff-04af-40d1-9574-954ff78b467f', N'643db72a-f24b-4e07-9e16-458db1f4b135', 2, CAST(45000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'364f16ff-04af-40d1-9574-954ff78b467f', N'e56476b5-2bc9-4908-9ba9-57dafa5959a6', 3, CAST(110700 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'1f2c4053-d6a8-47ca-a4ac-a90f61e5e79e', N'55c70857-c0a9-4710-9ed5-34d6abb2bd10', 4, CAST(44000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'69759f71-416a-4bc7-ba10-c61dbc5cdec0', N'55c70857-c0a9-4710-9ed5-34d6abb2bd10', 2, CAST(44000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'69759f71-416a-4bc7-ba10-c61dbc5cdec0', N'c862fb23-a2d2-42d2-8253-44824c4dfecc', 1, CAST(150000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'db75f252-706a-4480-ab55-ce8b4f7197b9', N'fb1e00c7-bbe2-46c4-a987-24ad892a9072', 1, CAST(92400 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'db75f252-706a-4480-ab55-ce8b4f7197b9', N'55c70857-c0a9-4710-9ed5-34d6abb2bd10', 1, CAST(44000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'f77f3581-0da0-4d5c-98e3-e99406bd2240', N'358808bc-9c89-4431-8a5f-228bbbc86a6d', 3, CAST(59000 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'f77f3581-0da0-4d5c-98e3-e99406bd2240', N'fb1e00c7-bbe2-46c4-a987-24ad892a9072', 10, CAST(92400 AS Decimal(8, 0)), 0)
INSERT [dbo].[OrderItem] ([orderId], [bookId], [quantity], [price], [isDeleted]) VALUES (N'f77f3581-0da0-4d5c-98e3-e99406bd2240', N'ddfde72a-7e11-49d5-89fb-e2766610c1cb', 1, CAST(200000 AS Decimal(8, 0)), 0)
GO
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'a38f02af-6754-40fd-bba1-3607bf60427a', N'Khuyến Mãi Mùa hè', CAST(N'2025-05-14T00:00:00.0000000' AS DateTime2), CAST(N'2025-08-16T00:00:00.0000000' AS DateTime2), CAST(120000 AS Decimal(8, 0)), CAST(0.15 AS Decimal(3, 2)), 0, 0)
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'960b5170-dff3-462e-a72d-534c6c528a60', N'Con Ngân Gà', CAST(N'2025-05-27T11:25:01.8480000' AS DateTime2), CAST(N'2025-05-28T11:25:01.8480000' AS DateTime2), CAST(60000 AS Decimal(8, 0)), CAST(0.10 AS Decimal(3, 2)), 2, 0)
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'a87d963e-4c3e-491c-8c43-ab3e4792cf10', N'Khuyến Mãi Cuối Năm', CAST(N'2025-11-01T11:00:57.5100000' AS DateTime2), CAST(N'2025-12-31T11:00:57.5100000' AS DateTime2), CAST(60000 AS Decimal(8, 0)), CAST(0.20 AS Decimal(3, 2)), 11, 0)
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'97b74c1b-b693-4f13-928e-d35d580f30f3', N'Tét 31-5', CAST(N'2025-06-01T10:53:43.8610000' AS DateTime2), CAST(N'2025-06-02T10:53:43.8610000' AS DateTime2), CAST(60001 AS Decimal(8, 0)), CAST(0.10 AS Decimal(3, 2)), 1, 1)
INSERT [dbo].[Promotion] ([id], [name], [startDate], [endDate], [condition], [discountPercent], [quantity], [isDeleted]) VALUES (N'6622cd9a-984d-400b-8b6a-f908c1a5bdec', N'Khuyến Mãi Quốc Khánh', CAST(N'2025-08-31T00:00:00.0000000' AS DateTime2), CAST(N'2025-09-04T00:00:00.0000000' AS DateTime2), CAST(230000 AS Decimal(8, 0)), CAST(0.23 AS Decimal(3, 2)), 10, 0)
GO
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'f06bb8bc-857c-40c5-a60a-0162f7ce1e5e', N'Deleted publisher', 1)
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'79994ae3-d421-4b92-b895-323c19e12db9', N'Nhà xuất bản Trẻ', 0)
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'5ed7cb31-567e-4639-b497-3fcc9b0bc4a7', N'Nhà xuất bản xanh mát', 1)
INSERT [dbo].[Publisher] ([id], [name], [isDeleted]) VALUES (N'2d64142c-1280-48e1-9207-c7a0023fa88f', N'Nhà xuất bản Kim Đồng', 0)
GO
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'dfabe65f-f997-4430-a877-07f1e2d960fa', N'Thùy', N'Ngân', CAST(N'2003-01-01' AS Date), N'Gia Lai', N'0987899909', N'thuyngan@gmail.com', N'076568990923', N'523333wgwgwgwegew', 0, 1, 1, 0)
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'add67045-cdb5-4ae8-ab83-3a1aa6c3954a', N'Thiên', N'Phúc', CAST(N'0001-01-01' AS Date), N'97 Man thiện', N'0397111111', N'phucnaoto123@gmail.com', N'098779880123', N'$2a$11$XBBsoi5ojemooCVO3p/QWOGyBMVy36/RLjmQQQzHDj71dDRWLfDOS', 0, 0, 0, 1)
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'a0ef187d-3284-49a2-b6fc-9b6fef273bfd', N'Thiên', N'Phúc 2', CAST(N'2003-01-01' AS Date), N'97 Man Thiện', N'0987676771', N'nnthienphuc.ptit@gmail.com', N'098779880333', N'$2a$11$CFlEgQc7psKDAifbkQ6eie2ov9Jn0ffMUCjLq.IK4wUT.EIlfFFz6', 0, 0, 1, 0)
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'd79863c2-de29-4e2b-8598-a2a30023b472', N'PTIT', N'Nguyen Ngoc Thien Phuc', CAST(N'2003-01-01' AS Date), N'Dong Nai', N'0391111111', N'n21dccn066@student.ptithcm.edu.vn', N'012345678111', N'$2a$11$jpp4W79szY3EI5VPmpE83Ow2R.3i.4qVvwRJNeeAULP/atrHYHEnW', 0, 0, 1, 0)
INSERT [dbo].[Staff] ([id], [familyName], [givenName], [dateOfBirth], [address], [phone], [email], [citizenIdentification], [hashPassword], [role], [gender], [isActived], [isDeleted]) VALUES (N'cd0ff279-bff4-4812-a8f6-c710004a17f9', N'Nguyễn Ngọc Thiên', N'Phúc', CAST(N'2003-08-16' AS Date), N'Dong Nai', N'0999999999', N'phucnaoto@gmail.com', N'012345678901', N'$2a$11$a8ID1eZIy4y.Dz6qy0SLTuH8mWe1xcPUgfycCnLBAZ7Yjb1Kcs/CO', 1, 0, 1, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Book]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [IX_Book] UNIQUE NONCLUSTERED 
(
	[isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Category]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [IX_Category] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [IX_Customer] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Promotion]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Promotion] ADD  CONSTRAINT [IX_Promotion] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Publisher]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Publisher] ADD  CONSTRAINT [IX_Publisher] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_CI]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [IX_Staff_CI] UNIQUE NONCLUSTERED 
(
	[citizenIdentification] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_Email]    Script Date: 18-Jun-25 3:00:09 PM ******/
ALTER TABLE [dbo].[Staff] ADD  CONSTRAINT [IX_Staff_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Staff_Phone]    Script Date: 18-Jun-25 3:00:09 PM ******/
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
ALTER TABLE [dbo].[Promotion]  WITH CHECK ADD  CONSTRAINT [CK_Promotion_Condition] CHECK  (([condition]>(0)))
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
