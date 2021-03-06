USE [SoruHazirla]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 17.1.2018 15:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Kullanici](
	[KullaniciAdi] [varchar](50) NOT NULL,
	[Parola] [varchar](50) NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[KullaniciAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sinavlar]    Script Date: 17.1.2018 15:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sinavlar](
	[SinavId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NULL,
	[Icerik] [varchar](max) NULL,
	[s1] [varchar](max) NULL,
	[s1a] [varchar](max) NULL,
	[s1b] [varchar](max) NULL,
	[s1c] [varchar](max) NULL,
	[s1d] [varchar](max) NULL,
	[s1cevap] [varchar](max) NULL,
	[s2] [varchar](max) NULL,
	[s2a] [varchar](max) NULL,
	[s2b] [varchar](max) NULL,
	[s2c] [varchar](max) NULL,
	[s2d] [varchar](max) NULL,
	[s2cevap] [varchar](max) NULL,
	[s3] [varchar](max) NULL,
	[s3a] [varchar](max) NULL,
	[s3b] [varchar](max) NULL,
	[s3c] [varchar](max) NULL,
	[s3d] [varchar](max) NULL,
	[s3cevap] [varchar](max) NULL,
	[s4] [varchar](max) NULL,
	[s4a] [varchar](max) NULL,
	[s4b] [varchar](max) NULL,
	[s4c] [varchar](max) NULL,
	[s4d] [varchar](max) NULL,
	[s4cevap] [varchar](max) NULL,
	[Tarih] [varchar](50) NULL,
	[Hazirlayan] [varchar](50) NULL,
 CONSTRAINT [PK_Sinavlar] PRIMARY KEY CLUSTERED 
(
	[SinavId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sinavlar]  WITH CHECK ADD  CONSTRAINT [FK_Sinavlar_Kullanici] FOREIGN KEY([Hazirlayan])
REFERENCES [dbo].[Kullanici] ([KullaniciAdi])
GO
ALTER TABLE [dbo].[Sinavlar] CHECK CONSTRAINT [FK_Sinavlar_Kullanici]
GO
