CREATE TABLE [dbo].[Receipts]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ImageId] UNIQUEIDENTIFIER NULL,
    [LocationCode] VARCHAR(50) NULL, 
    [Street] VARCHAR(50) NULL, 
    [Number] VARCHAR(10) NULL, 
    [Date] VARCHAR(50) NULL, 
    [Time] VARCHAR(50) NULL, 
    [ShipmentCode] VARCHAR(100) NULL UNIQUE, 
    [Quantity] INT NULL,
    [ServiceProvider] INT NULL, 
    [Status] INT NULL,
    FOREIGN KEY (ImageId) REFERENCES Images(Id)
)
