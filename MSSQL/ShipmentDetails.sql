CREATE TABLE [dbo].[ShipmentDetails]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [ReceiptId] UNIQUEIDENTIFIER NOT NULL,
    [BarCode] VARCHAR(50) NOT NULL UNIQUE, 
    [PostalCode] VARCHAR(50) NULL, 
    [HouseNumber] VARCHAR(10) NULL, 
    [City] VARCHAR(50) NULL, 
    [Country] VARCHAR(50) NULL, 
    FOREIGN KEY (ReceiptId) REFERENCES Receipts(Id)
)