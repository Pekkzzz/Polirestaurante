/* ============================================================
   PoliRestaurante - SQL Server
   Convertido desde el modelo MySQL Workbench
   ============================================================ */

IF DB_ID(N'PoliRestaurante') IS NULL
BEGIN
    CREATE DATABASE [PoliRestaurante];
END
GO

USE [PoliRestaurante];
GO

/* ============================================================
   TABLAS DE CATÁLOGO
   ============================================================ */

CREATE TABLE [UserRole] (
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

CREATE TABLE [ReservationStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,

    CONSTRAINT [PK_ReservationStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [PickupStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,

    CONSTRAINT [PK_PickupStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [OrderStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,

    CONSTRAINT [PK_OrderStatus] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_OrderStatus_Name] UNIQUE ([Name])
);
GO

CREATE TABLE [PaymentStatus] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NULL,
    [Description] VARCHAR(255) NULL,

    CONSTRAINT [PK_PaymentStatus] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [OrderType] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,

    CONSTRAINT [PK_OrderType] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [PaymentType] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(45) NOT NULL,
    [Description] VARCHAR(45) NOT NULL,

    CONSTRAINT [PK_PaymentType] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [ProductType] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,

    CONSTRAINT [PK_ProductType] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   USUARIOS
   ============================================================ */

CREATE TABLE [User] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Username] VARCHAR(255) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [PasswordHash] VARCHAR(255) NOT NULL,
    [CreationDate] DATETIME2 NOT NULL,
    [isDeleted] BIT NOT NULL
        CONSTRAINT [DF_User_isDeleted] DEFAULT (0),
    [DeletedDate] DATETIME2 NULL,
    [UserRole_ID] INT NOT NULL,

    CONSTRAINT [PK_User] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email]),
    CONSTRAINT [UQ_User_Username] UNIQUE ([Username])
);
GO

/* ============================================================
   MESAS
   ============================================================ */

CREATE TABLE [Table] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [TableNum] INT NOT NULL,
    [Capacity] INT NOT NULL,

    CONSTRAINT [PK_Table] PRIMARY KEY ([ID]),
    CONSTRAINT [UQ_Table_TableNum] UNIQUE ([TableNum])
);
GO

/* ============================================================
   PRODUCTOS
   ============================================================ */

CREATE TABLE [Product] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Stock] INT NOT NULL,
    [Price] DECIMAL(10,2) NOT NULL,
    [ProductType_ID] INT NOT NULL,

    CONSTRAINT [PK_Product] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   PEDIDOS
   ============================================================ */

CREATE TABLE [Order] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [TotalPrice] DECIMAL(10,2) NOT NULL,
    [Payments] DECIMAL(10,2) NOT NULL,
    [TotalBalance] DECIMAL(10,2) NOT NULL,
    [CreationDate] DATETIME2 NOT NULL,
    [PaymentStatus_ID] INT NOT NULL,
    [OrderStatus_ID] INT NOT NULL,
    [OrderType_ID] INT NOT NULL,
    [Table_ID] INT NULL,

    CONSTRAINT [PK_Order] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   DETALLE DE PEDIDOS
   ============================================================ */

CREATE TABLE [OrderDetail] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Product_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,

    CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   RESERVAS
   ============================================================ */

CREATE TABLE [Reservation] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [ReservationStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,

    CONSTRAINT [PK_Reservation] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   RESERVAS <-> MESAS
   ============================================================ */

CREATE TABLE [Reservation_has_Table] (
    [Reservation_ID] INT NOT NULL,
    [Table_ID] INT NOT NULL,

    CONSTRAINT [PK_Reservation_has_Table]
        PRIMARY KEY ([Reservation_ID], [Table_ID])
);
GO

/* ============================================================
   PAGOS / RECIBOS
   ============================================================ */

CREATE TABLE [Receipt] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [PaymentAmount] DECIMAL(10,2) NOT NULL,
    [Datetime] DATETIME2 NOT NULL,
    [PaymentType_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,

    CONSTRAINT [PK_Receipt] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   DOMICILIOS / RECOGIDA
   ============================================================ */

CREATE TABLE [ToPickUp] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Address] VARCHAR(255) NOT NULL,
    [CreationTime] DATETIME2 NOT NULL,
    [CompletedTime] DATETIME2 NULL,
    [PickupStatus_ID] INT NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,

    CONSTRAINT [PK_ToPickUp] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   HISTORIAL DE MODIFICACIONES DE PEDIDOS
   ============================================================ */

CREATE TABLE [OrderModifications] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NOT NULL,
    [Date] DATETIME2 NOT NULL,
    [User_ID] INT NOT NULL,
    [Order_ID] INT NOT NULL,

    CONSTRAINT [PK_OrderModifications] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   HISTORIAL GENERAL
   ============================================================ */

CREATE TABLE [ModificationsHistory] (
    [ID] INT IDENTITY(1,1) NOT NULL,
    [Description] VARCHAR(255) NULL,
    [Date] DATETIME2 NULL,
    [User_ID] INT NOT NULL,

    CONSTRAINT [PK_ModificationsHistory] PRIMARY KEY ([ID])
);
GO

/* ============================================================
   FOREIGN KEYS
   ============================================================ */

ALTER TABLE [User]
ADD CONSTRAINT [FK_User_UserRole]
    FOREIGN KEY ([UserRole_ID])
    REFERENCES [UserRole] ([ID]);
GO

ALTER TABLE [Reservation]
ADD CONSTRAINT [FK_Reservation_ReservationStatus]
    FOREIGN KEY ([ReservationStatus_ID])
    REFERENCES [ReservationStatus] ([ID]);
GO

ALTER TABLE [Reservation]
ADD CONSTRAINT [FK_Reservation_User]
    FOREIGN KEY ([User_ID])
    REFERENCES [User] ([ID]);
GO

ALTER TABLE [ToPickUp]
ADD CONSTRAINT [FK_ToPickUp_PickupStatus]
    FOREIGN KEY ([PickupStatus_ID])
    REFERENCES [PickupStatus] ([ID]);
GO

ALTER TABLE [ToPickUp]
ADD CONSTRAINT [FK_ToPickUp_User]
    FOREIGN KEY ([User_ID])
    REFERENCES [User] ([ID]);
GO

ALTER TABLE [ToPickUp]
ADD CONSTRAINT [FK_ToPickUp_Order]
    FOREIGN KEY ([Order_ID])
    REFERENCES [Order] ([ID]);
GO

ALTER TABLE [Receipt]
ADD CONSTRAINT [FK_Receipt_PaymentType]
    FOREIGN KEY ([PaymentType_ID])
    REFERENCES [PaymentType] ([ID]);
GO

ALTER TABLE [Receipt]
ADD CONSTRAINT [FK_Receipt_Order]
    FOREIGN KEY ([Order_ID])
    REFERENCES [Order] ([ID]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_PaymentStatus]
    FOREIGN KEY ([PaymentStatus_ID])
    REFERENCES [PaymentStatus] ([ID]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_OrderStatus]
    FOREIGN KEY ([OrderStatus_ID])
    REFERENCES [OrderStatus] ([ID]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_OrderType]
    FOREIGN KEY ([OrderType_ID])
    REFERENCES [OrderType] ([ID]);
GO

ALTER TABLE [Order]
ADD CONSTRAINT [FK_Order_Table]
    FOREIGN KEY ([Table_ID])
    REFERENCES [Table] ([ID]);
GO

ALTER TABLE [Product]
ADD CONSTRAINT [FK_Product_ProductType]
    FOREIGN KEY ([ProductType_ID])
    REFERENCES [ProductType] ([ID]);
GO

ALTER TABLE [OrderDetail]
ADD CONSTRAINT [FK_OrderDetail_Product]
    FOREIGN KEY ([Product_ID])
    REFERENCES [Product] ([ID]);
GO

ALTER TABLE [OrderDetail]
ADD CONSTRAINT [FK_OrderDetail_Order]
    FOREIGN KEY ([Order_ID])
    REFERENCES [Order] ([ID]);
GO

ALTER TABLE [OrderModifications]
ADD CONSTRAINT [FK_OrderModifications_User]
    FOREIGN KEY ([User_ID])
    REFERENCES [User] ([ID]);
GO

ALTER TABLE [OrderModifications]
ADD CONSTRAINT [FK_OrderModifications_Order]
    FOREIGN KEY ([Order_ID])
    REFERENCES [Order] ([ID]);
GO

ALTER TABLE [ModificationsHistory]
ADD CONSTRAINT [FK_ModificationsHistory_User]
    FOREIGN KEY ([User_ID])
    REFERENCES [User] ([ID]);
GO

ALTER TABLE [Reservation_has_Table]
ADD CONSTRAINT [FK_Reservation_has_Table_Reservation]
    FOREIGN KEY ([Reservation_ID])
    REFERENCES [Reservation] ([ID]);
GO

ALTER TABLE [Reservation_has_Table]
ADD CONSTRAINT [FK_Reservation_has_Table_Table]
    FOREIGN KEY ([Table_ID])
    REFERENCES [Table] ([ID]);
GO

/* ============================================================
   ÍNDICES DE FOREIGN KEYS
   ============================================================ */

CREATE INDEX [IX_User_UserRole_ID]
    ON [User] ([UserRole_ID]);
GO

CREATE INDEX [IX_Reservation_ReservationStatus_ID]
    ON [Reservation] ([ReservationStatus_ID]);
GO

CREATE INDEX [IX_Reservation_User_ID]
    ON [Reservation] ([User_ID]);
GO

CREATE INDEX [IX_ToPickUp_PickupStatus_ID]
    ON [ToPickUp] ([PickupStatus_ID]);
GO

CREATE INDEX [IX_ToPickUp_User_ID]
    ON [ToPickUp] ([User_ID]);
GO

CREATE INDEX [IX_ToPickUp_Order_ID]
    ON [ToPickUp] ([Order_ID]);
GO

CREATE INDEX [IX_Receipt_PaymentType_ID]
    ON [Receipt] ([PaymentType_ID]);
GO

CREATE INDEX [IX_Receipt_Order_ID]
    ON [Receipt] ([Order_ID]);
GO

CREATE INDEX [IX_Order_PaymentStatus_ID]
    ON [Order] ([PaymentStatus_ID]);
GO

CREATE INDEX [IX_Order_OrderStatus_ID]
    ON [Order] ([OrderStatus_ID]);
GO

CREATE INDEX [IX_Order_OrderType_ID]
    ON [Order] ([OrderType_ID]);
GO

CREATE INDEX [IX_Order_Table_ID]
    ON [Order] ([Table_ID]);
GO

CREATE INDEX [IX_Product_ProductType_ID]
    ON [Product] ([ProductType_ID]);
GO

CREATE INDEX [IX_OrderDetail_Product_ID]
    ON [OrderDetail] ([Product_ID]);
GO

CREATE INDEX [IX_OrderDetail_Order_ID]
    ON [OrderDetail] ([Order_ID]);
GO

CREATE INDEX [IX_OrderModifications_User_ID]
    ON [OrderModifications] ([User_ID]);
GO

CREATE INDEX [IX_OrderModifications_Order_ID]
    ON [OrderModifications] ([Order_ID]);
GO

CREATE INDEX [IX_ModificationsHistory_User_ID]
    ON [ModificationsHistory] ([User_ID]);
GO

CREATE INDEX [IX_Reservation_has_Table_Table_ID]
    ON [Reservation_has_Table] ([Table_ID]);
GO

CREATE INDEX [IX_Reservation_has_Table_Reservation_ID]
    ON [Reservation_has_Table] ([Reservation_ID]);
GO

PRINT 'Base de datos PoliRestaurante creada correctamente.';
GO
