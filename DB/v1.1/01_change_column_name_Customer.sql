sp_RENAME 'Customer.District' , 'Region', 'COLUMN'
GO
sp_RENAME 'Customer.City' , 'Area', 'COLUMN'
GO
ALTER TABLE dbo.Customer ALTER COLUMN Note ntext
GO