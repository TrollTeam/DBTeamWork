USE [master]
GO
/****** Object:  Database [TestComputerStore]    Script Date: 01-Sep-14 12:40:10 PM ******/
CREATE DATABASE [TestComputerStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestComputerStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TestComputerStore.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TestComputerStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\TestComputerStore_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TestComputerStore] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestComputerStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestComputerStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestComputerStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestComputerStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestComputerStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestComputerStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestComputerStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestComputerStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestComputerStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestComputerStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestComputerStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestComputerStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestComputerStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestComputerStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestComputerStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestComputerStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestComputerStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestComputerStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestComputerStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestComputerStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestComputerStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestComputerStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestComputerStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestComputerStore] SET RECOVERY FULL 
GO
ALTER DATABASE [TestComputerStore] SET  MULTI_USER 
GO
ALTER DATABASE [TestComputerStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestComputerStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestComputerStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestComputerStore] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TestComputerStore] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestComputerStore', N'ON'
GO
USE [TestComputerStore]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryCode] [nvarchar](50) NOT NULL,
	[Currency] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[CountryId] [int] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[ManufacturerId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_Manufacturers] PRIMARY KEY CLUSTERED 
(
	[ManufacturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[PriceDelivered] [money] NOT NULL,
	[ManufacturerId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sales]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SaleId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[SaleValue] [money] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesProducts]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesProducts](
	[SaleId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_SalesProducts] PRIMARY KEY CLUSTERED 
(
	[SaleId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stores]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[StoreId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SalesPrice] [money] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoresProducts]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoresProducts](
	[StoreId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_StoresProducts] PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Types]    Script Date: 01-Sep-14 12:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[TypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Countries]
GO
ALTER TABLE [dbo].[Manufacturers]  WITH CHECK ADD  CONSTRAINT [FK_Manufacturers_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[Manufacturers] CHECK CONSTRAINT [FK_Manufacturers_Countries]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Manufacturers] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturers] ([ManufacturerId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Manufacturers]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Types] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Types] ([TypeId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Types]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Customers]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Stores] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Stores]
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Products]
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Sales] FOREIGN KEY([SaleId])
REFERENCES [dbo].[Sales] ([SaleId])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Sales]
GO
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK_Stores_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK_Stores_Countries]
GO
ALTER TABLE [dbo].[StoresProducts]  WITH CHECK ADD  CONSTRAINT [FK_StoresProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[StoresProducts] CHECK CONSTRAINT [FK_StoresProducts_Products]
GO
ALTER TABLE [dbo].[StoresProducts]  WITH CHECK ADD  CONSTRAINT [FK_StoresProducts_Stores] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Stores] ([StoreId])
GO
ALTER TABLE [dbo].[StoresProducts] CHECK CONSTRAINT [FK_StoresProducts_Stores]
GO
USE [master]
GO
ALTER DATABASE [TestComputerStore] SET  READ_WRITE 
GO
