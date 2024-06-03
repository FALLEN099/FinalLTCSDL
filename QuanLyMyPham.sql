USE [master]
GO
/****** Object:  Database [QuanLyMyPham]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE DATABASE [QuanLyMyPham]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyMyPham].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyMyPham] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyMyPham] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyMyPham] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyMyPham] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyMyPham] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [QuanLyMyPham] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyMyPham] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyMyPham] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyMyPham] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyMyPham] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyMyPham] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyMyPham] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyMyPham] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyMyPham', N'ON'
GO
ALTER DATABASE [QuanLyMyPham] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanLyMyPham] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanLyMyPham]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItem]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_CartItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaHoaDon] [int] NOT NULL,
	[MaSp] [int] NOT NULL,
	[SoLuong] [smallint] NULL,
	[DonGia] [float] NULL,
	[GiamGia] [float] NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaSp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConTact]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConTact](
	[ID] [int] NOT NULL,
	[ConTent] [nvarchar](500) NULL,
	[Status] [nvarchar](500) NULL,
 CONSTRAINT [PK_ConTact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [int] NULL,
	[NgayLapHD] [datetime] NULL,
	[TongSL] [decimal](18, 0) NULL,
 CONSTRAINT [PK__HoaDon__835ED13B10DD4A71] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TenKH] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[Email] [nvarchar](50) NULL,
	[SDT] [int] NULL,
 CONSTRAINT [PK__KhachhHa__2725CF1EEB1BC594] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSanPham]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSanPham](
	[MaLoaiSP] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiSP] [nvarchar](255) NULL,
 CONSTRAINT [PK__LoaiSanP__1224CA7C35FBE213] PRIMARY KEY CLUSTERED 
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [nvarchar](50) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](50) NULL,
	[Gia] [float] NULL,
	[MaLoaiSP] [int] NULL,
	[HinhAnh] [nvarchar](max) NULL,
 CONSTRAINT [PK__SanPham__2725081CA07072F7] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/28/2024 10:27:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[PassWord] [nvarchar](50) NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](250) NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cart_UserId]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cart_UserId] ON [dbo].[Cart]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItem_CartId]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_CartItem_CartId] ON [dbo].[CartItem]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItem_ProductId]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_CartItem_ProductId] ON [dbo].[CartItem]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ChiTietHoaDon_MaSp]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_ChiTietHoaDon_MaSp] ON [dbo].[ChiTietHoaDon]
(
	[MaSp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HoaDon_MaKH]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_HoaDon_MaKH] ON [dbo].[HoaDon]
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_KhachHang_UserId]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_KhachHang_UserId] ON [dbo].[KhachHang]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SanPham_MaLoaiSP]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_SanPham_MaLoaiSP] ON [dbo].[SanPham]
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Role]    Script Date: 5/28/2024 10:27:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_User_Role] ON [dbo].[User]
(
	[Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_KhachHang] FOREIGN KEY([UserId])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_KhachHang]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_Cart] FOREIGN KEY([CartId])
REFERENCES [dbo].[Cart] ([Id])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_Cart]
GO
ALTER TABLE [dbo].[CartItem]  WITH CHECK ADD  CONSTRAINT [FK_CartItem_SanPham] FOREIGN KEY([ProductId])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[CartItem] CHECK CONSTRAINT [FK_CartItem_SanPham]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_HoaDon] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDon_SanPham] FOREIGN KEY([MaSp])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_ChiTietHoaDon_SanPham]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_User]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiSanPham] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LoaiSanPham] ([MaLoaiSP])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiSanPham]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
INSERT [dbo].[LoaiSanPham] ( [TenLoaiSP]) VALUES ( N'Trang Điểm')
INSERT [dbo].[LoaiSanPham] ( [TenLoaiSP]) VALUES ( N'Chăm Sóc Da')
INSERT [dbo].[LoaiSanPham] ( [TenLoaiSP]) VALUES ( N'Chăm Sóc Toàn Thân')
GO

INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'New Name', 110000, 1, N'Images/TrangDiem1.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Son Kem FOIF Daily Velvet Tint ', 140000, 1, N'Images/TrangDiem2.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Phấn Má Hồng Etude House Blusher', 110000, 1, N'Images/TrangDiem3.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Phấn Nước AprilSkin Snow Cushion', 290000, 1, N'Images/TrangDiem4.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Son Uemura Rouge Unlimited Matte', 290000, 1, N'Images/TrangDiem5.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Phấn Nước April Skin Blossom Cushion', 450000, 1, N'Images/TrangDiem6.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Phấn Mắt Habaria Professional', 95000, 1, N'Images/TrangDiem7.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Mặt Mạ Yuejin Repair Control', 18000, 2, N'Images/ChamDa1.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Rửa Mặt Cocoon Cà Phê Đắk Lắk', 160000, 2, N'Images/ChamDa2.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Mặt Nạ Moisturising Mask', 35000, 2, N'Images/ChamDa3.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Sữa Rửa Mặt Byvibes Wonder Bath', 165000, 2, N'Images/ChamDa4.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Chống Nắng PrettySkin Scream', 170000, 2, N'Images/ChamDa5.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Nước Tẩy Trang PrettySkin The Pure', 210000, 2, N'Images/ChamDa6.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Gel Giảm Mụn Actidem Derma', 130000, 2, N'Images/ChamDa6.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Nước Tẩy Trang L''Oreal Làm Sạch ', 230000, 2, N'Images/ChamDa7.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Chống Nắng La Roche-Posay ', 420000, 2, N'Images/ChamDa8.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Miếng Dán Răng Crest Blanc Brillant', 55000, 3, N'Images/ToanThan1.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Sữa Dưỡng Thể Trắng Da Vaseline ', 130000, 3, N'Images/ToanThan2.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Dưỡng Bioderma Cicabio ', 220000, 3, N'Images/ToanThan3.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Dung Dịch Vệ Sinh Ziaja Intima', 105000, 3, N'Images/ToanThan4.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Sáp Lạnh Wax Lông Horshion ', 75000, 3, N'Images/ToanThan5.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Dưỡng Da Tay Và Móng Vaseline', 50000, 3, N'Images/ToanThan6.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Dưỡng Thể RMON White Label', 350000, 3, N'Images/ToanThan7.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Tẩy Da Chết Dove Body Scrub', 160000, 3, N'Images/ToanThan8.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Sáp Che Khuyết Điểm Và Tạo Khối', 85000, 1, N'Images/TrangDiem8.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES (N'Son Kem MAC Locked Kiss', 530000, 1, N'Images/TrangDiem10.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Mặt Nạ Ngủ Môi Laneige ', 440000, 1, N'Images/TrangDiem11.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES (N'Son Tint Merzy The Watery Dew', 170000, 1, N'Images/TrangDiem12.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Son Dưỡng Dior Addict Lip ', 320000, 1, N'Images/TrangDiem13.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Gel Chống Nắng Anessa', 430000, 2, N'Images/ChamDa9.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Nước Tẩy Trang Garnier Micellar Water', 180000, 2, N'Images/ChamDa10.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES (N'Xịt Khoáng Kyung Lab PDRN', 350000, 2, N'Images/ChamDa11.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Tinh Chất Estee Lauder Advanced', 2500000, 2, N'Images/ChamDa12.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Dưỡng Trẻ Hóa Và Săn Chắc Vùng Da', 280000, 3, N'Images/ToanThan9.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Kem Dưỡng Ẩm SVR Xérial ', 190000, 3, N'Images/ToanThan10.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES ( N'Sữa Tắm Hương Nước Hoa Tesori D''Oriente', 130000, 3, N'Images/ToanThan11.jpg')
INSERT [dbo].[SanPham] (  [TenSP], [Gia], [MaLoaiSP], [HinhAnh]) VALUES (N'Xịt Giảm Mụn Lưng Caryophy Body', 330000, 3, N'Images/ToanThan12.jpg')

