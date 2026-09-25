USE PoliRestaurante;
GO

-- Desactivar temporalmente las restricciones FK
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL';
GO

-- Eliminar todos los registros
EXEC sp_MSforeachtable 'DELETE FROM ?';
GO

-- Reiniciar todos los contadores IDENTITY
EXEC sp_MSforeachtable '
    IF OBJECTPROPERTY(OBJECT_ID(''?''), ''TableHasIdentity'') = 1
    BEGIN
        DBCC CHECKIDENT (''?'', RESEED, 0);
    END
';
GO

-- Volver a activar las restricciones FK
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL';
GO