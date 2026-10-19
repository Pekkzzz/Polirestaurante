/*
    PoliRestaurante - SQL Server
    Convertido desde PoliRestaurante_MYSQL_Script(20261018-221532).sql
    Todas las columnas ID que eran AUTO_INCREMENT se convierten a IDENTITY(1,1).
*/

IF DB_ID(N'PoliRestaurante') IS NULL
BEGIN
    CREATE DATABASE [PoliRestaurante];
END;
GO

USE [PoliRestaurante];
GO

CREATE TABLE [dbo].[UserRole]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(55) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [IsOwner] BIT NOT NULL,
    [CanEditSystems] BIT NOT NULL,
    [CanEditOrders] BIT NOT NULL,
    [CanSetOrdersStatus] BIT NOT NULL,
    [CanSetToPickUpStatus] BIT NOT NULL,
    [CanSetProductStock] BIT NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_UserRole_Name] UNIQUE ([Name])
);
GO

CREATE TABLE [dbo].[User]
(
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
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email]),
    CONSTRAINT [UQ_User_Username] UNIQUE ([Username]),
    CONSTRAINT [FK_User_UserRole1] FOREIGN KEY ([UserRole_ID]) REFERENCES [dbo].[UserRole] ([ID])
);
GO
CREATE INDEX [IX_User_UserRole_ID] ON [dbo].[User] ([UserRole_ID]);
GO

CREATE TABLE [dbo].[ReservationStatus]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_ReservationStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Reservation]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [ReservationStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Reservation_ReservationStatus1] FOREIGN KEY ([ReservationStatus_ID]) REFERENCES [dbo].[ReservationStatus] ([ID]),
    CONSTRAINT [FK_Reservation_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID])
);
GO
CREATE INDEX [IX_Reservation_ReservationStatus_ID] ON [dbo].[Reservation] ([ReservationStatus_ID]);
CREATE INDEX [IX_Reservation_User_ID] ON [dbo].[Reservation] ([User_ID]);
GO

CREATE TABLE [dbo].[PickupStatus]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_PickupStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[PaymentStatus]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT [PK_PaymentStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[OrderStatus]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_OrderStatus_Name] UNIQUE ([Name])
);
GO

CREATE TABLE [dbo].[OrderType]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,
    CONSTRAINT [PK_OrderType] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [dbo].[Table]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [TableNum] INT NOT NULL,
    [Capacity] INT NOT NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_Table_TableNum] UNIQUE ([TableNum])
);
GO

CREATE TABLE [dbo].[Order]
(
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
    CONSTRAINT [FK_Order_PaymentStatus1] FOREIGN KEY ([PaymentStatus_ID]) REFERENCES [dbo].[PaymentStatus] ([ID]),
    CONSTRAINT [FK_Order_OrderStatus1] FOREIGN KEY ([OrderStatus_ID]) REFERENCES [dbo].[OrderStatus] ([ID]),
    CONSTRAINT [FK_Order_OrderType1] FOREIGN KEY ([OrderType_ID]) REFERENCES [dbo].[OrderType] ([ID]),
    CONSTRAINT [FK_Order_Table1] FOREIGN KEY ([Table_ID]) REFERENCES [dbo].[Table] ([ID])
);
GO
CREATE INDEX [IX_Order_PaymentStatus_ID] ON [dbo].[Order] ([PaymentStatus_ID]);
CREATE INDEX [IX_Order_OrderStatus_ID] ON [dbo].[Order] ([OrderStatus_ID]);
CREATE INDEX [IX_Order_OrderType_ID] ON [dbo].[Order] ([OrderType_ID]);
CREATE INDEX [IX_Order_Table_ID] ON [dbo].[Order] ([Table_ID]);
GO

CREATE TABLE [dbo].[ToPickUp]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Address] VARCHAR(255) NOT NULL,
    [CreationTime] DATETIME2 NOT NULL,
    [CompletedTime] DATETIME2 NULL,
    [PickupStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    CONSTRAINT [PK_ToPickUp] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ToPickUp_PickupStatus1] FOREIGN KEY ([PickupStatus_ID]) REFERENCES [dbo].[PickupStatus] ([ID]),
    CONSTRAINT [FK_ToPickUp_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_ToPickUp_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID])
);
GO
CREATE INDEX [IX_ToPickUp_PickupStatus_ID] ON [dbo].[ToPickUp] ([PickupStatus_ID]);
CREATE INDEX [IX_ToPickUp_User_ID] ON [dbo].[ToPickUp] ([User_ID]);
CREATE INDEX [IX_ToPickUp_Order_ID] ON [dbo].[ToPickUp] ([Order_ID]);
GO

CREATE TABLE [dbo].[ProductType]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    CONSTRAINT [PK_ProductType] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_ProductType_Name] UNIQUE ([Name])
);
GO

CREATE TABLE [dbo].[Product]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Stock] INT NOT NULL,
    [Price] DECIMAL(10,2) NOT NULL,
    [ProductType_ID] INT NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_Product_Name] UNIQUE ([Name]),
    CONSTRAINT [FK_Product_ProductType1] FOREIGN KEY ([ProductType_ID]) REFERENCES [dbo].[ProductType] ([ID])
);
GO
CREATE INDEX [IX_Product_ProductType_ID] ON [dbo].[Product] ([ProductType_ID]);
GO

CREATE TABLE [dbo].[OrderDetailStatus]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT [PK_OrderDetailStatus] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_OrderDetailStatus_Name] UNIQUE ([Name])
);
GO

CREATE TABLE [dbo].[OrderDetail]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Observations] VARCHAR(255) NULL,
    [Product_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    [OrderDetailStatus_ID] INT NOT NULL,
    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_OrderDetail_Product1] FOREIGN KEY ([Product_ID]) REFERENCES [dbo].[Product] ([ID]),
    CONSTRAINT [FK_OrderDetail_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID]),
    CONSTRAINT [FK_OrderDetail_OrderDetailStatus1] FOREIGN KEY ([OrderDetailStatus_ID]) REFERENCES [dbo].[OrderDetailStatus] ([ID])
);
GO
CREATE INDEX [IX_OrderDetail_Product_ID] ON [dbo].[OrderDetail] ([Product_ID]);
CREATE INDEX [IX_OrderDetail_Order_ID] ON [dbo].[OrderDetail] ([Order_ID]);
CREATE INDEX [IX_OrderDetail_OrderDetailStatus_ID] ON [dbo].[OrderDetail] ([OrderDetailStatus_ID]);
GO

CREATE TABLE [dbo].[OrderModifications]
(
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,
    CONSTRAINT [PK_OrderModifications] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_OrderModifications_User1] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[User] ([ID]),
    CONSTRAINT [FK_OrderModifications_Order1] FOREIGN KEY ([Order_ID]) REFERENCES [dbo].[Order] ([ID])
);
GO
CREATE INDEX [IX_OrderModifications_User_ID] ON [dbo].[OrderModifications] ([User_ID]);
CREATE INDEX [IX_OrderModifications_Order_ID] ON [dbo].[OrderModifications] ([Order_ID]);
GO

CREATE TABLE [dbo].[Reservation_has_Table]
(
    [Reservation_ID] INT NOT NULL,
    [Table_ID] INT NOT NULL,
    CONSTRAINT [PK_Reservation_has_Table] PRIMARY KEY ([Reservation_ID], [Table_ID]),
    CONSTRAINT [FK_Reservation_has_Table_Reservation1] FOREIGN KEY ([Reservation_ID]) REFERENCES [dbo].[Reservation] ([ID]),
    CONSTRAINT [FK_Reservation_has_Table_Table1] FOREIGN KEY ([Table_ID]) REFERENCES [dbo].[Table] ([ID])
);
GO
CREATE INDEX [IX_Reservation_has_Table_Table_ID] ON [dbo].[Reservation_has_Table] ([Table_ID]);
GO

PRINT N'Base de datos PoliRestaurante creada correctamente.';
GO
