USE [dbNoiThat]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id] [int] NULL,
	[username] [nchar](100) NULL,
	[password] [nchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[brands]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[brands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[description] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
	[description] [text] NOT NULL,
 CONSTRAINT [PK__categori__3213E83FC500CE2A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[phone] [varchar](255) NOT NULL,
	[address] [varchar](255) NOT NULL,
	[password] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderDetail]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[order_id] [int] NULL,
	[quantity] [int] NULL,
	[price] [float] NULL,
 CONSTRAINT [PK_orderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[date_create] [datetime] NULL,
	[date_delivery] [date] NULL,
	[totalPrice] [float] NULL,
	[status] [tinyint] NULL,
	[paymentMethod] [nvarchar](50) NULL,
	[paymentStatus] [tinyint] NULL,
 CONSTRAINT [PK__orders__3213E83F442A8C20] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[description] [text] NULL,
	[image] [nvarchar](255) NULL,
	[price] [float] NULL,
	[category_id] [int] NULL,
	[brand_id] [int] NULL,
	[ngayCapNhap] [date] NULL,
 CONSTRAINT [PK__products__3213E83F55FDA7AE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slogan]    Script Date: 09/12/2023 17:52:08  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slogan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[noi_dung] [nvarchar](max) NULL,
 CONSTRAINT [PK_Slogan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[admin] ([id], [username], [password]) VALUES (1, N'admin                                                                                               ', N'admin                                                                                               ')
GO
SET IDENTITY_INSERT [dbo].[brands] ON 

INSERT [dbo].[brands] ([id], [name], [description]) VALUES (1, N'Herman Miller', N'Chuyên s?n xu?t n?i th?t van phòng và gh?, Herman Miller du?c bi?t d?n v?i các thi?t k? d?nh cao và s?n ph?m cao c?p nhu gh? Aeron')
INSERT [dbo].[brands] ([id], [name], [description]) VALUES (3, N'Ashley Furniture', N'Ashley Furniture là m?t trong nh?ng nhà s?n xu?t n?i th?t l?n nh?t th? gi?i, cung c?p nhi?u lo?i s?n ph?m t? gh?, sofa d?n giu?ng và bàn an')
INSERT [dbo].[brands] ([id], [name], [description]) VALUES (4, N'BoConcept', N' Hãng n?i th?t Ðan M?ch BoConcept n?i ti?ng v?i thi?t k? da d?ng và hi?n d?i cho c? không gian s?ng và làm vi?c.')
INSERT [dbo].[brands] ([id], [name], [description]) VALUES (8, N'Luis Vuitton', N'')
SET IDENTITY_INSERT [dbo].[brands] OFF
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [name], [description]) VALUES (2, N'Ghế Đơn', N'')
INSERT [dbo].[categories] ([id], [name], [description]) VALUES (17, N'Ghế Sofa', N'Không có')
INSERT [dbo].[categories] ([id], [name], [description]) VALUES (18, N'Đèn chùm', N'ko')
INSERT [dbo].[categories] ([id], [name], [description]) VALUES (20, N'Áo sơ mi', N'')
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[customers] ON 

INSERT [dbo].[customers] ([id], [name], [email], [phone], [address], [password]) VALUES (25, N'Tuân', N'timle1002@gmail.com', N'1111111111', N'BD', N'q9gWIC4O')
INSERT [dbo].[customers] ([id], [name], [email], [phone], [address], [password]) VALUES (26, N'Tuân', N'1002@gmail.com', N'1111111111', N'BD', N'123')
INSERT [dbo].[customers] ([id], [name], [email], [phone], [address], [password]) VALUES (27, N'Tuân', N'10022@gmail.com', N'1111111111', N'BD', N'123')
INSERT [dbo].[customers] ([id], [name], [email], [phone], [address], [password]) VALUES (28, N'Tuân', N'1002222@gmail.com', N'1111111111', N'BD', N'123')
SET IDENTITY_INSERT [dbo].[customers] OFF
GO
SET IDENTITY_INSERT [dbo].[orderDetail] ON 

INSERT [dbo].[orderDetail] ([id], [product_id], [order_id], [quantity], [price]) VALUES (222, 40, 40, 1, 26000000)
INSERT [dbo].[orderDetail] ([id], [product_id], [order_id], [quantity], [price]) VALUES (223, 40, 41, 1, 26000000)
INSERT [dbo].[orderDetail] ([id], [product_id], [order_id], [quantity], [price]) VALUES (224, 39, 41, 1, 26000000)
INSERT [dbo].[orderDetail] ([id], [product_id], [order_id], [quantity], [price]) VALUES (225, 38, 41, 1, 26000000)
SET IDENTITY_INSERT [dbo].[orderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [customer_id], [date_create], [date_delivery], [totalPrice], [status], [paymentMethod], [paymentStatus]) VALUES (40, 25, CAST(N'2023-12-08T23:36:35.553' AS DateTime), CAST(N'2023-12-09' AS Date), 26000000, 1, N'Chuyển khoản', 1)
INSERT [dbo].[orders] ([id], [customer_id], [date_create], [date_delivery], [totalPrice], [status], [paymentMethod], [paymentStatus]) VALUES (41, 28, CAST(N'2023-12-09T06:02:30.723' AS DateTime), CAST(N'2023-12-10' AS Date), 78000000, 1, N'Thanh toán khi nhận hàng', 1)
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (2, N'Ghế LaZBoy2', N'Lazboy là m?t thuong hi?u gh? thu giãn du?c Nhà Xinh phân ph?i d?c quy?n v?i Vi?t Nam. Gh? n?i b?t b?i s? ti?n nghi, tho?i mái và êm ái t?i da', N'LaZBoy2.jpg', 120000000, 2, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (3, N'Ghế LaZBoy3', N'Lazboy là m?t thuong hi?u gh? thu giãn du?c Nhà Xinh phân ph?i d?c quy?n v?i Vi?t Nam. Gh? n?i b?t b?i s? ti?n nghi, tho?i mái và êm ái t?i da', N'banan1.jpg', 13000000, 17, 3, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (36, N'Ghế đơn AZ', N'', N'LaZBoy1.jpg', 12000000, 2, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (37, N'Ghế sofa AA', N'', N'sofa1.jpg', 25000000, 17, 3, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (38, N'Ghế sofa BB', N'', N'sofa2.jpg', 26000000, 17, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (39, N'Ghế sofa CC', N'', N'sofa3.jpg', 26000000, 17, 3, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (40, N'Ghế sofa DD', N'qqqq', N'sofa4.jpg', 26000000, 17, 4, CAST(N'2023-10-28' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (41, N'Ghế sofa EE', N'', N'sofa5.jpg', 26000000, 17, 4, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (42, N'Ghế sofa FF', N'', N'sofa6.jpg', 26000000, 2, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (43, N'Đèn chùm', N'', N'dt1.jpg', 20000000, 18, 4, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (44, N'Đèn chùm AA', N'Lazboy là m?t thuong hi?u gh? thu giãn du?c Nhà Xinh phân ph?i d?c quy?n v?i Vi?t Nam. Gh? n?i b?t b?i s? ti?n nghi, tho?i mái và êm ái t?i da', N'dt2.jpg', 20000000, 18, 3, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (45, N'Đèn chùm RE', N'', N'dt3.jpg', 20000000, 18, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (46, N'Đèn chùm RF', N'', N'dt4.jpg', 20000000, 18, 1, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (47, N'Đèn chùm RY', N'', N'dt5.jpg', 20000000, 18, 3, CAST(N'2023-10-25' AS Date))
INSERT [dbo].[products] ([id], [name], [description], [image], [price], [category_id], [brand_id], [ngayCapNhap]) VALUES (48, N'Đèn chùm RI', N'', N'dt6.jpg', 200000000, 18, 1, CAST(N'2023-10-25' AS Date))
SET IDENTITY_INSERT [dbo].[products] OFF
GO
SET IDENTITY_INSERT [dbo].[Slogan] ON 

INSERT [dbo].[Slogan] ([id], [noi_dung]) VALUES (1, N'Tạo nên không gian hoàn hảo cùng chúng tôi')
INSERT [dbo].[Slogan] ([id], [noi_dung]) VALUES (2, N'Nghệ thuật trong từng chi tiết nội thất')
INSERT [dbo].[Slogan] ([id], [noi_dung]) VALUES (3, N'Tận hưởng cuộc sống với nội thất tinh tế')
SET IDENTITY_INSERT [dbo].[Slogan] OFF
GO
ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_orders]
GO
ALTER TABLE [dbo].[orderDetail]  WITH CHECK ADD  CONSTRAINT [FK_orderDetail_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orderDetail] CHECK CONSTRAINT [FK_orderDetail_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[customers] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_brands] FOREIGN KEY([brand_id])
REFERENCES [dbo].[brands] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_brands]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_categories]
GO
