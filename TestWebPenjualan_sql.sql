USE [master]
GO
/****** Object:  Database [TestWebPenjualan]    Script Date: 29/07/2024 13:07:17 ******/
CREATE DATABASE [TestWebPenjualan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestWebPenjualan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestWebPenjualan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestWebPenjualan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TestWebPenjualan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TestWebPenjualan] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestWebPenjualan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestWebPenjualan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestWebPenjualan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestWebPenjualan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestWebPenjualan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestWebPenjualan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET RECOVERY FULL 
GO
ALTER DATABASE [TestWebPenjualan] SET  MULTI_USER 
GO
ALTER DATABASE [TestWebPenjualan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestWebPenjualan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestWebPenjualan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestWebPenjualan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestWebPenjualan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestWebPenjualan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestWebPenjualan', N'ON'
GO
ALTER DATABASE [TestWebPenjualan] SET QUERY_STORE = OFF
GO
USE [TestWebPenjualan]
GO
/****** Object:  User [app1]    Script Date: 29/07/2024 13:07:17 ******/
CREATE USER [app1] FOR LOGIN [app1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [app1]
GO
/****** Object:  Table [dbo].[MsBrand]    Script Date: 29/07/2024 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsBrand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MsBrand] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsCategory]    Script Date: 29/07/2024 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_MsCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsProduct]    Script Date: 29/07/2024 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsProduct](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](5) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[UnitTypeId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Barcode] [varchar](20) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_MsProduct] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsUnitType]    Script Date: 29/07/2024 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsUnitType](
	[UnitTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MsUnitType] PRIMARY KEY CLUSTERED 
(
	[UnitTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrOrder]    Script Date: 29/07/2024 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrOrder](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_TrOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MsBrand] ON 

INSERT [dbo].[MsBrand] ([BrandId], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Sample 1  ', N'admin', CAST(N'2024-07-29T08:54:43.287' AS DateTime), NULL, NULL)
INSERT [dbo].[MsBrand] ([BrandId], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Others    ', N'admin', CAST(N'2024-07-29T08:54:43.287' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[MsBrand] OFF
GO
SET IDENTITY_INSERT [dbo].[MsCategory] ON 

INSERT [dbo].[MsCategory] ([CategoryId], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Food', CAST(N'2024-07-29T08:54:35.753' AS DateTime), N'admin', NULL, NULL)
INSERT [dbo].[MsCategory] ([CategoryId], [Name], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Others', CAST(N'2024-07-29T08:54:35.753' AS DateTime), N'admin', NULL, NULL)
SET IDENTITY_INSERT [dbo].[MsCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[MsProduct] ON 

INSERT [dbo].[MsProduct] ([ProductId], [ProductCode], [Name], [UnitTypeId], [CategoryId], [BrandId], [Price], [Barcode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'B0001', N'Telor Curah update', 1, 1, 1, CAST(1000.50 AS Decimal(18, 2)), N'asdf2332435', CAST(N'2024-07-26T13:47:23.633' AS DateTime), N'Admin', CAST(N'2024-07-29T12:07:40.063' AS DateTime), N'admin')
INSERT [dbo].[MsProduct] ([ProductId], [ProductCode], [Name], [UnitTypeId], [CategoryId], [BrandId], [Price], [Barcode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'B0002', N'Test update', 1, 1, 1, CAST(100.00 AS Decimal(18, 2)), N'23432fdsf123', NULL, NULL, CAST(N'2024-07-29T12:06:29.487' AS DateTime), N'admin')
INSERT [dbo].[MsProduct] ([ProductId], [ProductCode], [Name], [UnitTypeId], [CategoryId], [BrandId], [Price], [Barcode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, N'B0004', N'Test', 1, 1, 1, CAST(1000.00 AS Decimal(18, 2)), NULL, CAST(N'2024-07-26T16:01:28.480' AS DateTime), N'roy', CAST(N'2024-07-29T10:27:08.453' AS DateTime), N'roy')
INSERT [dbo].[MsProduct] ([ProductId], [ProductCode], [Name], [UnitTypeId], [CategoryId], [BrandId], [Price], [Barcode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (28, N'TS02', N'Test Aja', 1, 1, 1, CAST(1000.00 AS Decimal(18, 2)), N'hkjhffy', CAST(N'2024-07-29T00:00:48.953' AS DateTime), N'roy', CAST(N'2024-07-29T10:16:08.683' AS DateTime), N'roy')
SET IDENTITY_INSERT [dbo].[MsProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[MsUnitType] ON 

INSERT [dbo].[MsUnitType] ([UnitTypeId], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'Gram', N'Admin', CAST(N'2024-07-26T13:50:02.350' AS DateTime), NULL, NULL)
INSERT [dbo].[MsUnitType] ([UnitTypeId], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'Pcs', N'Admin', CAST(N'2024-07-26T13:50:02.350' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[MsUnitType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MsProduct_Unique_ProductCode]    Script Date: 29/07/2024 13:07:18 ******/
ALTER TABLE [dbo].[MsProduct] ADD  CONSTRAINT [IX_MsProduct_Unique_ProductCode] UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MsProduct]  WITH CHECK ADD  CONSTRAINT [FK_MsProduct_MsBrand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[MsBrand] ([BrandId])
GO
ALTER TABLE [dbo].[MsProduct] CHECK CONSTRAINT [FK_MsProduct_MsBrand]
GO
ALTER TABLE [dbo].[MsProduct]  WITH CHECK ADD  CONSTRAINT [FK_MsProduct_MsCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[MsCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[MsProduct] CHECK CONSTRAINT [FK_MsProduct_MsCategory]
GO
ALTER TABLE [dbo].[MsProduct]  WITH CHECK ADD  CONSTRAINT [FK_MsProduct_MsUnitType] FOREIGN KEY([UnitTypeId])
REFERENCES [dbo].[MsUnitType] ([UnitTypeId])
GO
ALTER TABLE [dbo].[MsProduct] CHECK CONSTRAINT [FK_MsProduct_MsUnitType]
GO
ALTER TABLE [dbo].[MsUnitType]  WITH CHECK ADD  CONSTRAINT [FK_MsUnitType_MsUnitType] FOREIGN KEY([UnitTypeId])
REFERENCES [dbo].[MsUnitType] ([UnitTypeId])
GO
ALTER TABLE [dbo].[MsUnitType] CHECK CONSTRAINT [FK_MsUnitType_MsUnitType]
GO
ALTER TABLE [dbo].[TrOrder]  WITH CHECK ADD  CONSTRAINT [FK_TrOrder_MsProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[MsProduct] ([ProductId])
GO
ALTER TABLE [dbo].[TrOrder] CHECK CONSTRAINT [FK_TrOrder_MsProduct]
GO
USE [master]
GO
ALTER DATABASE [TestWebPenjualan] SET  READ_WRITE 
GO
