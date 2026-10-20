/*
    PoliRestaurante - SQL Server
    Conversión del script MySQL Workbench del 19/10/2026.
    Los campos ID que eran AUTO_INCREMENT se convierten a IDENTITY(1,1).
*/

IF DB_ID(N'PoliRestaurante') IS NULL
BEGIN
    CREATE DATABASE [PoliRestaurante];
END;
GO

USE [PoliRestaurante];
GO

CREATE TABLE [dbo].[UserRole] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(55) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [IsOwner] BIT NOT NULL,
    [CanEditSystems] BIT NOT NULL,
    [CanEditOrders] BIT NOT NULL,
    [CanSetOrdersStatus] BIT NOT NULL,
    [CanSetToPickUpStatus] BIT NOT NULL,
    [CanSetProductStock] BIT NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[User] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Username] VARCHAR(255) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [PasswordHash] VARCHAR(255) NOT NULL,
    [CreationDate] DATETIME2 NOT NULL,
    [isDeleted] BIT NOT NULL CONSTRAINT [DF_User_isDeleted] DEFAULT (0),
    [DeletedDate] DATETIME2 NULL,
    [UserRole_ID] INT NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_User_UserRole1] FOREIGN KEY ([UserRole_ID]) REFERENCES [dbo].[UserRole] ([ID])
);
GO

CREATE TABLE [dbo].[ReservationStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_ReservationStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Table] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [TableNum] INT NOT NULL,
    [Capacity] INT NOT NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Reservation] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [People] INT NOT NULL,
    [Reason] VARCHAR(255) NULL,
    [ReservationStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,
    [Table_ID] INT NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_Reservation_ReservationStatus1] FOREIGN KEY ([ReservationStatus_ID]) REFERENCES [dbo].[ReservationStatus] ([ID]),
    CONSTRAINT [fk_Reservation_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [fk_Reservation_Table1] FOREIGN KEY ([Table_ID]) REFERENCES [dbo].[Table] ([ID])
);
GO

CREATE TABLE [dbo].[PickupStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_PickupStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[PaymentStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT [PK_PaymentStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[OrderStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[OrderType] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_OrderType] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Order] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [TotalPrice] DECIMAL(10,2) NOT NULL,
    [Payments] DECIMAL(10,2) NOT NULL,
    [TotalBalance] DECIMAL(10,2) NOT NULL,
    [CreationDate] DATETIME2 NOT NULL,
    [PaymentStatus_ID] INT NOT NULL,
    [OrderStatus_ID] INT NOT NULL,
    [OrderType_ID] INT NOT NULL,
    [Table_ID] INT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_Order_PaymentStatus1] FOREIGN KEY ([PaymentStatus_ID]) REFERENCES [dbo].[PaymentStatus] ([ID]),
    CONSTRAINT [fk_Order_OrderStatus1] FOREIGN KEY ([OrderStatus_ID]) REFERENCES [dbo].[OrderStatus] ([ID]),
    CONSTRAINT [fk_Order_OrderType1] FOREIGN KEY ([OrderType_ID]) REFERENCES [dbo].[OrderType] ([ID]),
    CONSTRAINT [fk_Order_Table1] FOREIGN KEY ([Table_ID]) REFERENCES [dbo].[Table] ([ID])
);
GO

CREATE TABLE [dbo].[ToPickUp] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Address] VARCHAR(255) NOT NULL,
    [CreationTime] DATETIME2 NOT NULL,
    [CompletedTime] DATETIME2 NULL,
    [PickupStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    CONSTRAINT [PK_ToPickUp] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_ToPickUp_PickupStatus1] FOREIGN KEY ([PickupStatus_ID]) REFERENCES [dbo].[PickupStatus] ([ID]),
    CONSTRAINT [fk_ToPickUp_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [fk_ToPickUp_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID])
);
GO

CREATE TABLE [dbo].[ProductType] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Product] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Stock] INT NOT NULL,
    [Price] DECIMAL(10,2) NOT NULL,
    [ProductType_ID] INT NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_Product_ProductType1] FOREIGN KEY ([ProductType_ID]) REFERENCES [dbo].[ProductType] ([ID])
);
GO

CREATE TABLE [dbo].[OrderDetailStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT [PK_OrderDetailStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[OrderDetail] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Observations] VARCHAR(255) NULL,
    [Product_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    [OrderDetailStatus_ID] INT NOT NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_OrderDetail_Product1] FOREIGN KEY ([Product_ID]) REFERENCES [dbo].[Product] ([ID]),
    CONSTRAINT [fk_OrderDetail_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID]),
    CONSTRAINT [fk_OrderDetail_OrderDetailStatus1] FOREIGN KEY ([OrderDetailStatus_ID]) REFERENCES [dbo].[OrderDetailStatus] ([ID])
);
GO

CREATE TABLE [dbo].[OrderModifications] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    CONSTRAINT [PK_OrderModifications] PRIMARY KEY ([ID]),
    CONSTRAINT [fk_OrderModifications_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [fk_OrderModifications_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID])
);
GO

CREATE UNIQUE INDEX [Name_UNIQUE] ON [dbo].[UserRole] ([Name]);
GO
CREATE INDEX [fk_User_UserRole1_idx] ON [dbo].[User] ([UserRole_ID]);
GO
CREATE UNIQUE INDEX [Email_UNIQUE] ON [dbo].[User] ([Email]);
GO
CREATE UNIQUE INDEX [Username_UNIQUE] ON [dbo].[User] ([Username]);
GO
CREATE UNIQUE INDEX [TableNum_UNIQUE] ON [dbo].[Table] ([TableNum]);
GO
CREATE INDEX [fk_Reservation_ReservationStatus1_idx] ON [dbo].[Reservation] ([ReservationStatus_ID]);
GO
CREATE INDEX [fk_Reservation_User1_idx] ON [dbo].[Reservation] ([User_ID]);
GO
CREATE INDEX [fk_Reservation_Table1_idx] ON [dbo].[Reservation] ([Table_ID]);
GO
CREATE UNIQUE INDEX [Name_UNIQUE] ON [dbo].[OrderStatus] ([Name]);
GO
CREATE INDEX [fk_Order_PaymentStatus1_idx] ON [dbo].[Order] ([PaymentStatus_ID]);
GO
CREATE INDEX [fk_Order_OrderStatus1_idx] ON [dbo].[Order] ([OrderStatus_ID]);
GO
CREATE INDEX [fk_Order_OrderType1_idx] ON [dbo].[Order] ([OrderType_ID]);
GO
CREATE INDEX [fk_Order_Table1_idx] ON [dbo].[Order] ([Table_ID]);
GO
CREATE INDEX [fk_ToPickUp_PickupStatus1_idx] ON [dbo].[ToPickUp] ([PickupStatus_ID]);
GO
CREATE INDEX [fk_ToPickUp_User1_idx] ON [dbo].[ToPickUp] ([User_ID]);
GO
CREATE INDEX [fk_ToPickUp_Order1_idx] ON [dbo].[ToPickUp] ([Order_ID]);
GO
CREATE UNIQUE INDEX [Name_UNIQUE] ON [dbo].[ProductType] ([Name]);
GO
CREATE INDEX [fk_Product_ProductType1_idx] ON [dbo].[Product] ([ProductType_ID]);
GO
CREATE UNIQUE INDEX [Name_UNIQUE] ON [dbo].[Product] ([Name]);
GO
CREATE UNIQUE INDEX [Name_UNIQUE] ON [dbo].[OrderDetailStatus] ([Name]);
GO
CREATE INDEX [fk_OrderDetail_Product1_idx] ON [dbo].[OrderDetail] ([Product_ID]);
GO
CREATE INDEX [fk_OrderDetail_Order1_idx] ON [dbo].[OrderDetail] ([Order_ID]);
GO
CREATE INDEX [fk_OrderDetail_OrderDetailStatus1_idx] ON [dbo].[OrderDetail] ([OrderDetailStatus_ID]);
GO
CREATE INDEX [fk_OrderModifications_User1_idx] ON [dbo].[OrderModifications] ([User_ID]);
GO
CREATE INDEX [fk_OrderModifications_Order1_idx] ON [dbo].[OrderModifications] ([Order_ID]);
GO

PRINT N'Base de datos PoliRestaurante creada correctamente.';
GO
