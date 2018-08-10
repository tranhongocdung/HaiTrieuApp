CREATE TABLE [dbo].[ProductImport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[CreatedById] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductImport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE TABLE [dbo].[ProductImportDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductImportId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_ProductImportDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProductImportDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductImportDetail_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductImportDetail] CHECK CONSTRAINT [FK_ProductImportDetail_Product_ProductId]
GO

ALTER TABLE [dbo].[ProductImportDetail]  WITH CHECK ADD  CONSTRAINT [FK_ProductImportDetail_ProductImport_ProductImportId] FOREIGN KEY([ProductImportId])
REFERENCES [dbo].[ProductImport] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductImportDetail] CHECK CONSTRAINT [FK_ProductImportDetail_ProductImport_ProductImportId]
GO



