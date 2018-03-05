ALTER TABLE dbo.[Order]
ADD CreatedById int NULL;
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Order_dbo.User_CreatedById] FOREIGN KEY([CreatedById])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_dbo.Order_dbo.User_CreatedById]
GO