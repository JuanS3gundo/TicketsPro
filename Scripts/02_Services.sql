-- =============================================
-- TicketsPro ??? 02_Services.sql
-- Base: ServicesPP (Seguridad, Bit?!cora, Recuperaci??n)
-- Para Advanced Installer (SQL Scripts)
-- Idempotente ??? seguro de re-ejecutar
-- Ejecutar DESPU??S de 01_MainBusiness.sql
-- =============================================

USE [master];
GO

-- =============================================
-- CREAR BASE DE DATOS
-- =============================================
IF DB_ID(N'ServicesPP') IS NULL
BEGIN
    CREATE DATABASE [ServicesPP];
    PRINT 'Base de datos ServicesPP creada.';
END
ELSE
    PRINT 'ServicesPP ya existe.';
GO

-- Configuraci??n segura (compatible con SQL 2016+)
ALTER DATABASE [ServicesPP] SET AUTO_CLOSE OFF;
ALTER DATABASE [ServicesPP] SET AUTO_SHRINK OFF;
ALTER DATABASE [ServicesPP] SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE [ServicesPP] SET RECOVERY SIMPLE;
ALTER DATABASE [ServicesPP] SET MULTI_USER;
ALTER DATABASE [ServicesPP] SET PAGE_VERIFY CHECKSUM;
GO

-- Full-text (solo si est?! instalado)
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    EXEC [ServicesPP].[dbo].[sp_fulltext_database] @action = 'enable';
GO

USE [ServicesPP];
GO

-- =============================================
-- TABLAS
-- =============================================

-- Usuario (seguridad ??? separado de Tickets)
IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuario](
        [IdUsuario] [uniqueidentifier] NOT NULL,
        [UserName] [varchar](1000) NULL,
        [Password] [varchar](1000) NULL,
        [timestamp] [timestamp] NOT NULL,
        [Email] [nvarchar](100) NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario])
    );
    PRINT 'Tabla Usuario creada.';
END
GO

-- ??ndice ??nico en UserName (fuera del CREATE TABLE por idempotencia)
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Usuario_UserName' AND object_id = OBJECT_ID('dbo.Usuario'))
    CREATE UNIQUE NONCLUSTERED INDEX [UQ_Usuario_UserName] ON [dbo].[Usuario]([UserName]);
GO

-- Patente (permisos)
IF OBJECT_ID(N'dbo.Patente', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Patente](
        [IdPatente] [uniqueidentifier] NOT NULL,
        [Nombre] [varchar](1000) NULL,
        [DataKey] [varchar](1000) NULL,
        [TipoAcceso] [int] NULL,
        [timestamp] [timestamp] NOT NULL,
        CONSTRAINT [PK_Patente] PRIMARY KEY CLUSTERED ([IdPatente])
    );
    PRINT 'Tabla Patente creada.';
END
GO

-- Familia (roles)
IF OBJECT_ID(N'dbo.Familia', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Familia](
        [IdFamilia] [uniqueidentifier] NOT NULL,
        [Nombre] [varchar](1000) NULL,
        [timestamp] [timestamp] NOT NULL,
        [EsDefault] [bit] NOT NULL DEFAULT (0),
        CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED ([IdFamilia])
    );
    PRINT 'Tabla Familia creada.';
END
GO

-- ??ndice ??nico filtrado: solo una familia puede ser default
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_Familia_Default' AND object_id = OBJECT_ID('dbo.Familia'))
    CREATE UNIQUE NONCLUSTERED INDEX [UX_Familia_Default] ON [dbo].[Familia]([EsDefault]) WHERE [EsDefault] = 1;
GO

-- Familia_Familia (jerarqu??a de roles)
IF OBJECT_ID(N'dbo.Familia_Familia', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Familia_Familia](
        [IdFamilia] [uniqueidentifier] NOT NULL,
        [IdFamiliaHijo] [uniqueidentifier] NOT NULL,
        [timestamp] [timestamp] NOT NULL,
        CONSTRAINT [PK_Familia_Familia] PRIMARY KEY CLUSTERED ([IdFamilia], [IdFamiliaHijo]),
        CONSTRAINT [FK_FF_Familia] FOREIGN KEY ([IdFamilia]) REFERENCES [Familia]([IdFamilia]),
        CONSTRAINT [FK_FF_FamiliaHijo] FOREIGN KEY ([IdFamiliaHijo]) REFERENCES [Familia]([IdFamilia])
    );
    CREATE NONCLUSTERED INDEX [IX_FF_IdFamilia] ON [Familia_Familia]([IdFamilia]);
    CREATE NONCLUSTERED INDEX [IX_FF_IdFamiliaHijo] ON [Familia_Familia]([IdFamiliaHijo]);
    PRINT 'Tabla Familia_Familia creada.';
END
GO

-- Familia_Patente (permisos por rol)
IF OBJECT_ID(N'dbo.Familia_Patente', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Familia_Patente](
        [IdFamilia] [uniqueidentifier] NOT NULL,
        [IdPatente] [uniqueidentifier] NOT NULL,
        [timestamp] [timestamp] NOT NULL,
        CONSTRAINT [PK_Familia_Patente] PRIMARY KEY CLUSTERED ([IdFamilia], [IdPatente]),
        CONSTRAINT [FK_FamiliaPatente_Familia] FOREIGN KEY ([IdFamilia]) REFERENCES [Familia]([IdFamilia]),
        CONSTRAINT [FK_FamiliaPatente_Patente] FOREIGN KEY ([IdPatente]) REFERENCES [Patente]([IdPatente]) ON DELETE CASCADE
    );
    PRINT 'Tabla Familia_Patente creada.';
END
GO

-- Usuario_Familia (roles por usuario)
IF OBJECT_ID(N'dbo.Usuario_Familia', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuario_Familia](
        [IdUsuario] [uniqueidentifier] NOT NULL,
        [IdFamilia] [uniqueidentifier] NOT NULL,
        [timestamp] [timestamp] NOT NULL,
        CONSTRAINT [PK_Usuario_Familia] PRIMARY KEY CLUSTERED ([IdUsuario], [IdFamilia]),
        CONSTRAINT [FK_UF_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([IdUsuario]),
        CONSTRAINT [FK_UF_Familia] FOREIGN KEY ([IdFamilia]) REFERENCES [Familia]([IdFamilia])
    );
    CREATE NONCLUSTERED INDEX [IX_UF_IdUsuario] ON [Usuario_Familia]([IdUsuario]);
    CREATE NONCLUSTERED INDEX [IX_UF_IdFamilia] ON [Usuario_Familia]([IdFamilia]);
    PRINT 'Tabla Usuario_Familia creada.';
END
GO

-- Usuario_Patente (permisos directos por usuario)
IF OBJECT_ID(N'dbo.Usuario_Patente', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuario_Patente](
        [IdUsuario] [uniqueidentifier] NOT NULL,
        [IdPatente] [uniqueidentifier] NOT NULL,
        [timestamp] [timestamp] NOT NULL,
        CONSTRAINT [PK_Usuario_Patente] PRIMARY KEY CLUSTERED ([IdUsuario], [IdPatente]),
        CONSTRAINT [FK_UP_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuario]([IdUsuario]),
        CONSTRAINT [FK_UP_Patente] FOREIGN KEY ([IdPatente]) REFERENCES [Patente]([IdPatente])
    );
    CREATE NONCLUSTERED INDEX [IX_UP_IdUsuario] ON [Usuario_Patente]([IdUsuario]);
    CREATE NONCLUSTERED INDEX [IX_UP_IdPatente] ON [Usuario_Patente]([IdPatente]);
    PRINT 'Tabla Usuario_Patente creada.';
END
GO

-- Bitacora (auditor??a)
IF OBJECT_ID(N'dbo.Bitacora', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Bitacora](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Fecha] [datetime] NOT NULL DEFAULT (GETDATE()),
        [Usuario] [nvarchar](100) NOT NULL,
        [Accion] [nvarchar](255) NOT NULL,
        [Detalle] [nvarchar](max) NULL,
        [Nivel] [nvarchar](50) NOT NULL,
        [EquipoId] [int] NULL,
        [TicketId] [uniqueidentifier] NULL,
        CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED ([Id])
    );
    CREATE NONCLUSTERED INDEX [IX_Bitacora_TicketId] ON [Bitacora]([TicketId]) WHERE [TicketId] IS NOT NULL;
    PRINT 'Tabla Bitacora creada.';
END
GO

-- Recuperacion (recuperaci??n de contrase??as)
IF OBJECT_ID(N'dbo.Recuperacion', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Recuperacion](
        [IdRecuperacion] [int] IDENTITY(1,1) NOT NULL,
        [Email] [nvarchar](100) NOT NULL,
        [Codigo] [nvarchar](10) NOT NULL,
        [FechaExpiracion] [datetime] NOT NULL,
        CONSTRAINT [PK_Recuperacion] PRIMARY KEY CLUSTERED ([IdRecuperacion])
    );
    PRINT 'Tabla Recuperacion creada.';
END
GO

-- =============================================
-- DATOS INICIALES (SEED)
-- Solo roles (Familia), permisos (Patente) y
-- sus relaciones. NO se insertan usuarios 
-- (se crean desde la app) ni datos de auditor??a.
-- =============================================

-- Patentes (permisos del sistema)
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_MAESTROS')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'b13eeae6-34ca-4d0a-b241-226df9f4866b', 'Gestion de Maestros', 'GESTION_MAESTROS', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_BACKUP')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'5f0004d7-2a3a-4767-82df-692a229f674f', 'Gestion de BackUp', 'GESTION_BACKUP', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_ACCESOS')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'62ed30f3-985a-475b-8c81-783ff5d193a0', 'Gestion de Accesos', 'GESTION_ACCESOS', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_TICKETS')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'a1f0d798-3f8a-4707-a09f-7c5e5f3efdb2', 'Gestion de tickets', 'GESTION_TICKETS', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_INVENTARIO')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'35d4fbac-a464-455a-9654-c745ecdec44c', N'Gesti??n de Inventario', 'GESTION_INVENTARIO', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'VISUALIZAR_Bitacora')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'423c3adc-f7bc-4771-b886-ca68b0a38660', 'Gestion de Bitacora', 'VISUALIZAR_Bitacora', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'CREAR_TICKET')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'9a258043-4307-48a7-9a53-dd4f6d84923d', 'Creacion de tickets', 'CREAR_TICKET', 0);
IF NOT EXISTS (SELECT 1 FROM [Patente] WHERE [DataKey] = 'GESTION_ANALITICAS')
    INSERT INTO [Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (N'16c13f52-271d-4516-aaeb-e5bc90eef9b6', 'Gestion de analiticas', 'GESTION_ANALITICAS', 0);
PRINT 'Patentes (permisos) OK.';
GO

-- Familias (roles del sistema)
IF NOT EXISTS (SELECT 1 FROM [Familia] WHERE [Nombre] = 'Administrador')
    INSERT INTO [Familia] ([IdFamilia], [Nombre], [EsDefault])
    VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', 'Administrador', 0);
IF NOT EXISTS (SELECT 1 FROM [Familia] WHERE [Nombre] = 'Cliente')
    INSERT INTO [Familia] ([IdFamilia], [Nombre], [EsDefault])
    VALUES (N'5c6fad1a-891f-416a-a490-be42c162621d', 'Cliente', 1);
IF NOT EXISTS (SELECT 1 FROM [Familia] WHERE [Nombre] = N'T??cnico')
    INSERT INTO [Familia] ([IdFamilia], [Nombre], [EsDefault])
    VALUES (N'80209e46-f459-4087-ac91-ef009b581e0b', N'T??cnico', 0);
PRINT 'Familias (roles) OK.';
GO

-- Familia_Patente: asignaci??n de permisos a roles
-- Administrador ??? TODOS los permisos (8)
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'b13eeae6-34ca-4d0a-b241-226df9f4866b')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'b13eeae6-34ca-4d0a-b241-226df9f4866b'); -- GESTION_MAESTROS
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'5f0004d7-2a3a-4767-82df-692a229f674f')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'5f0004d7-2a3a-4767-82df-692a229f674f'); -- GESTION_BACKUP
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'62ed30f3-985a-475b-8c81-783ff5d193a0')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'62ed30f3-985a-475b-8c81-783ff5d193a0'); -- GESTION_ACCESOS
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'a1f0d798-3f8a-4707-a09f-7c5e5f3efdb2')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'a1f0d798-3f8a-4707-a09f-7c5e5f3efdb2'); -- GESTION_TICKETS
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'35d4fbac-a464-455a-9654-c745ecdec44c')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'35d4fbac-a464-455a-9654-c745ecdec44c'); -- GESTION_INVENTARIO
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'423c3adc-f7bc-4771-b886-ca68b0a38660')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'423c3adc-f7bc-4771-b886-ca68b0a38660'); -- VISUALIZAR_Bitacora
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'9a258043-4307-48a7-9a53-dd4f6d84923d')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'9a258043-4307-48a7-9a53-dd4f6d84923d'); -- CREAR_TICKET
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'9a818c81-ba52-490c-a4c3-97f30a5db69c' AND [IdPatente] = N'16c13f52-271d-4516-aaeb-e5bc90eef9b6')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'9a818c81-ba52-490c-a4c3-97f30a5db69c', N'16c13f52-271d-4516-aaeb-e5bc90eef9b6'); -- GESTION_ANALITICAS

-- Cliente ??? CREAR_TICKET
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'5c6fad1a-891f-416a-a490-be42c162621d' AND [IdPatente] = N'9a258043-4307-48a7-9a53-dd4f6d84923d')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'5c6fad1a-891f-416a-a490-be42c162621d', N'9a258043-4307-48a7-9a53-dd4f6d84923d'); -- CREAR_TICKET

-- T??cnico ??? GESTION_ACCESOS + GESTION_TICKETS
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'80209e46-f459-4087-ac91-ef009b581e0b' AND [IdPatente] = N'62ed30f3-985a-475b-8c81-783ff5d193a0')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'80209e46-f459-4087-ac91-ef009b581e0b', N'62ed30f3-985a-475b-8c81-783ff5d193a0'); -- GESTION_ACCESOS
IF NOT EXISTS (SELECT 1 FROM [Familia_Patente] WHERE [IdFamilia] = N'80209e46-f459-4087-ac91-ef009b581e0b' AND [IdPatente] = N'a1f0d798-3f8a-4707-a09f-7c5e5f3efdb2')
    INSERT INTO [Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (N'80209e46-f459-4087-ac91-ef009b581e0b', N'a1f0d798-3f8a-4707-a09f-7c5e5f3efdb2'); -- GESTION_TICKETS

PRINT 'Asignaciones Familia-Patente OK.';
GO

-- =============================================
-- STORED PROCEDURES
-- Necesarios para que la aplicaci??n funcione.
-- Usa CREATE OR ALTER para idempotencia.
-- =============================================

-- ========================
-- USUARIO
-- ========================
GO
CREATE OR ALTER PROCEDURE [dbo].[UsuarioInsert]
    @IdUsuario UNIQUEIDENTIFIER,
    @UserName  VARCHAR(1000),
    @Password  VARCHAR(1000),
    @Email     NVARCHAR(100) = NULL
AS
BEGIN
    -- SET NOCOUNT OFF (default) so ExecuteNonQuery returns rows affected
    INSERT INTO [dbo].[Usuario] ([IdUsuario], [UserName], [Password], [Email])
    VALUES (@IdUsuario, @UserName, @Password, @Email);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[UsuarioSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [UserName], [Password], [Email] FROM [dbo].[Usuario];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[UsuarioSelect]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [UserName], [Password], [Email]
    FROM [dbo].[Usuario]
    WHERE [IdUsuario] = @IdUsuario;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_SelectByUserName]
    @UserName VARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [UserName], [Password], [Email]
    FROM [dbo].[Usuario]
    WHERE [UserName] = @UserName;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_SelectByEmail]
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [UserName], [Password], [Email]
    FROM [dbo].[Usuario]
    WHERE [Email] = @Email;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[UsuarioDelete]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    -- Borrar relaciones primero
    DELETE FROM [dbo].[Usuario_Familia] WHERE [IdUsuario] = @IdUsuario;
    DELETE FROM [dbo].[Usuario_Patente] WHERE [IdUsuario] = @IdUsuario;
    DELETE FROM [dbo].[Usuario] WHERE [IdUsuario] = @IdUsuario;
    SELECT @@ROWCOUNT;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[UsuarioUpdate]
    @IdUsuario UNIQUEIDENTIFIER,
    @UserName  VARCHAR(1000) = NULL,
    @Password  VARCHAR(1000) = NULL,
    @Email     NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Usuario]
    SET [UserName] = ISNULL(@UserName, [UserName]),
        [Password] = ISNULL(@Password, [Password]),
        [Email]    = ISNULL(@Email, [Email])
    WHERE [IdUsuario] = @IdUsuario;
END
GO

PRINT 'SPs de Usuario OK.';
GO

-- ========================
-- PATENTE
-- ========================
CREATE OR ALTER PROCEDURE [dbo].[PatenteInsert]
    @IdPatente  UNIQUEIDENTIFIER,
    @Nombre     VARCHAR(1000),
    @DataKey    VARCHAR(1000),
    @TipoAcceso INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Patente] ([IdPatente], [Nombre], [DataKey], [TipoAcceso])
    VALUES (@IdPatente, @Nombre, @DataKey, @TipoAcceso);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PatenteSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdPatente], [Nombre], [DataKey], [TipoAcceso], [Timestamp] FROM [dbo].[Patente];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PatenteSelect]
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdPatente], [Nombre], [DataKey], [TipoAcceso], [Timestamp]
    FROM [dbo].[Patente]
    WHERE [IdPatente] = @IdPatente;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PatenteUpdate]
    @IdPatente  UNIQUEIDENTIFIER,
    @Nombre     VARCHAR(1000),
    @DataKey    VARCHAR(1000),
    @TipoAcceso INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Patente]
    SET [Nombre] = @Nombre, [DataKey] = @DataKey, [TipoAcceso] = @TipoAcceso
    WHERE [IdPatente] = @IdPatente;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PatenteDelete]
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Familia_Patente] WHERE [IdPatente] = @IdPatente;
    DELETE FROM [dbo].[Usuario_Patente] WHERE [IdPatente] = @IdPatente;
    DELETE FROM [dbo].[Patente] WHERE [IdPatente] = @IdPatente;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[PatentesEfectivasPorUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    -- Permisos directos del usuario
    SELECT DISTINCT p.[IdPatente], p.[Nombre], p.[DataKey], p.[TipoAcceso], p.[Timestamp]
    FROM [dbo].[Usuario_Patente] up
    INNER JOIN [dbo].[Patente] p ON up.[IdPatente] = p.[IdPatente]
    WHERE up.[IdUsuario] = @IdUsuario
    UNION
    -- Permisos via familias
    SELECT DISTINCT p.[IdPatente], p.[Nombre], p.[DataKey], p.[TipoAcceso], p.[Timestamp]
    FROM [dbo].[Usuario_Familia] uf
    INNER JOIN [dbo].[Familia_Patente] fp ON uf.[IdFamilia] = fp.[IdFamilia]
    INNER JOIN [dbo].[Patente] p ON fp.[IdPatente] = p.[IdPatente]
    WHERE uf.[IdUsuario] = @IdUsuario;
END
GO

PRINT 'SPs de Patente OK.';
GO

-- ========================
-- FAMILIA
-- ========================
CREATE OR ALTER PROCEDURE [dbo].[FamiliaInsert]
    @IdFamilia     UNIQUEIDENTIFIER,
    @NombreFamilia VARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Familia] ([IdFamilia], [Nombre])
    VALUES (@IdFamilia, @NombreFamilia);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaUpdate]
    @IdFamilia     UNIQUEIDENTIFIER,
    @NombreFamilia VARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Familia]
    SET [Nombre] = @NombreFamilia
    WHERE [IdFamilia] = @IdFamilia;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdFamilia], [Nombre], [Timestamp] FROM [dbo].[Familia];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaSelect]
    @IdFamilia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdFamilia], [Nombre], [Timestamp] FROM [dbo].[Familia] WHERE [IdFamilia] = @IdFamilia;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaDelete]
    @IdFamilia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Familia_Patente] WHERE [IdFamilia] = @IdFamilia;
    DELETE FROM [dbo].[Usuario_Familia] WHERE [IdFamilia] = @IdFamilia;
    DELETE FROM [dbo].[Familia] WHERE [IdFamilia] = @IdFamilia;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Familia_SelectDefault]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 [IdFamilia] FROM [dbo].[Familia] WHERE [EsDefault] = 1;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaPatenteInsert]
    @IdFamilia UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Familia_Patente] WHERE [IdFamilia] = @IdFamilia AND [IdPatente] = @IdPatente)
        INSERT INTO [dbo].[Familia_Patente] ([IdFamilia], [IdPatente]) VALUES (@IdFamilia, @IdPatente);
END
GO


CREATE OR ALTER PROCEDURE [dbo].[FamiliaPatenteSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdFamilia], [IdPatente] FROM [dbo].[Familia_Patente];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaPatenteSelectByFamilia]
    @IdFamilia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT fp.[IdFamilia], fp.[IdPatente]
    FROM [dbo].[Familia_Patente] fp
    INNER JOIN [dbo].[Patente] p ON fp.[IdPatente] = p.[IdPatente]
    WHERE fp.[IdFamilia] = @IdFamilia;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[FamiliaPatenteDelete]
    @IdFamilia UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Familia_Patente]
    WHERE [IdFamilia] = @IdFamilia AND [IdPatente] = @IdPatente;
END
GO

PRINT 'SPs de Familia OK.';
GO

-- ========================
-- USUARIO_PATENTE
-- ========================
CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteInsert]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuario_Patente] WHERE [IdUsuario] = @IdUsuario AND [IdPatente] = @IdPatente)
        INSERT INTO [dbo].[Usuario_Patente] ([IdUsuario], [IdPatente]) VALUES (@IdUsuario, @IdPatente);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [IdPatente] FROM [dbo].[Usuario_Patente];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteSelectByIdUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [IdPatente] FROM [dbo].[Usuario_Patente] WHERE [IdUsuario] = @IdUsuario;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteSelectByUsuarioPatente]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [IdPatente]
    FROM [dbo].[Usuario_Patente]
    WHERE [IdUsuario] = @IdUsuario AND [IdPatente] = @IdPatente;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteDeleteByIdUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Usuario_Patente] WHERE [IdUsuario] = @IdUsuario;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteDeleteByUsuarioPatente]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Usuario_Patente]
    WHERE [IdUsuario] = @IdUsuario AND [IdPatente] = @IdPatente;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_PatenteUpdate]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdPatente UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    -- En tabla de v??nculo, update = asegurar que existe
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuario_Patente] WHERE [IdUsuario] = @IdUsuario AND [IdPatente] = @IdPatente)
        INSERT INTO [dbo].[Usuario_Patente] ([IdUsuario], [IdPatente]) VALUES (@IdUsuario, @IdPatente);
END
GO

PRINT 'SPs de Usuario_Patente OK.';
GO

-- ========================
-- USUARIO_FAMILIA
-- ========================
CREATE OR ALTER PROCEDURE [dbo].[Usuario_FamiliaInsert]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdFamilia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuario_Familia] WHERE [IdUsuario] = @IdUsuario AND [IdFamilia] = @IdFamilia)
        INSERT INTO [dbo].[Usuario_Familia] ([IdUsuario], [IdFamilia]) VALUES (@IdUsuario, @IdFamilia);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_FamiliaSelectAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [IdFamilia] FROM [dbo].[Usuario_Familia];
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_FamiliaSelectByIdUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [IdUsuario], [IdFamilia] FROM [dbo].[Usuario_Familia] WHERE [IdUsuario] = @IdUsuario;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_FamiliaDeleteByIdUsuario]
    @IdUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Usuario_Familia] WHERE [IdUsuario] = @IdUsuario;
END
GO

CREATE OR ALTER PROCEDURE [dbo].[Usuario_FamiliaUpdate]
    @IdUsuario UNIQUEIDENTIFIER,
    @IdFamilia UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuario_Familia] WHERE [IdUsuario] = @IdUsuario AND [IdFamilia] = @IdFamilia)
        INSERT INTO [dbo].[Usuario_Familia] ([IdUsuario], [IdFamilia]) VALUES (@IdUsuario, @IdFamilia);
END
GO

PRINT 'SPs de Usuario_Familia OK.';
GO

-- ========================
-- Recuperacion
-- ========================
CREATE OR ALTER PROCEDURE [dbo].[RecuperacionInsert]
    @Email           NVARCHAR(100),
    @Codigo          NVARCHAR(10),
    @FechaExpiracion DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Recuperacion] ([Email], [Codigo], [FechaExpiracion])
    VALUES (@Email, @Codigo, @FechaExpiracion);
END
GO

CREATE OR ALTER PROCEDURE [dbo].[RecuperacionValidar]
    @Email  NVARCHAR(100),
    @Codigo NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1)
    FROM [dbo].[Recuperacion]
    WHERE [Email] = @Email AND [Codigo] = @Codigo AND [FechaExpiracion] > GETDATE();
END
GO

PRINT 'SPs de Recuperacion OK.';
GO

-- NOTA: No se insertan usuarios. El primer usuario admin
-- se crea desde la pantalla de registro de la aplicaci??n.

PRINT '';
PRINT '=== ServicesPP lista (con Stored Procedures) ===';
GO
