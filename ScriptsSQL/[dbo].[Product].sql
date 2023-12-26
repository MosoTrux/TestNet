USE [TestNet]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 25/12/2023 20:47:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[Stock] [decimal](18, 2) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CreatedUser] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedUser] [varchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Status]  DEFAULT ((1)) FOR [Status]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Stock]  DEFAULT ((0)) FOR [Stock]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Price]  DEFAULT ((0)) FOR [Price]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO


