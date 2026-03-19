-- =============================================
-- TicketsPro — 01_MainBusiness.sql
-- Base: TicketsProDB_v2
-- Para Advanced Installer (SQL Scripts)
-- Idempotente — seguro de re-ejecutar
-- =============================================

USE [master];
GO

-- =============================================
-- CREAR BASE DE DATOS
-- =============================================
IF DB_ID(N'TicketsProDB_v2') IS NULL
BEGIN
    CREATE DATABASE [TicketsProDB_v2];
    PRINT 'Base de datos TicketsProDB_v2 creada.';
END
ELSE
    PRINT 'TicketsProDB_v2 ya existe.';
GO

-- Configuración segura (compatible con SQL 2016+)
ALTER DATABASE [TicketsProDB_v2] SET AUTO_CLOSE OFF;
ALTER DATABASE [TicketsProDB_v2] SET AUTO_SHRINK OFF;
ALTER DATABASE [TicketsProDB_v2] SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE [TicketsProDB_v2] SET RECOVERY SIMPLE;
ALTER DATABASE [TicketsProDB_v2] SET MULTI_USER;
ALTER DATABASE [TicketsProDB_v2] SET PAGE_VERIFY CHECKSUM;
GO

-- Full-text (solo si está instalado)
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    EXEC [TicketsProDB_v2].[dbo].[sp_fulltext_database] @action = 'enable';
GO

USE [TicketsProDB_v2];
GO

-- =============================================
-- TABLAS
-- =============================================

-- Usuario
IF OBJECT_ID(N'dbo.Usuario', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuario](
        [IdUsuario] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [UserName] [nvarchar](50) NOT NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario]),
        CONSTRAINT [UQ_Usuario_UserName] UNIQUE ([UserName])
    );
    PRINT 'Tabla Usuario creada.';
END
GO

-- PrioridadTicket
IF OBJECT_ID(N'dbo.PrioridadTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PrioridadTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](50) NOT NULL,
        [NivelPeso] [int] NOT NULL,
        [CodigoColor] [nvarchar](7) NOT NULL,
        [NivelImpacto] [nvarchar](50) NULL,
        CONSTRAINT [PK_PrioridadTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_PrioridadTicket_Nombre] UNIQUE ([Nombre]),
        CONSTRAINT [CHK_PrioridadTicket_NivelPeso] CHECK ([NivelPeso] >= 1 AND [NivelPeso] <= 100),
        CONSTRAINT [CHK_PrioridadTicket_Color] CHECK ([CodigoColor] LIKE '#%')
    );
    PRINT 'Tabla PrioridadTicket creada.';
END
GO

-- PoliticaSLA
IF OBJECT_ID(N'dbo.PoliticaSLA', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PoliticaSLA](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](100) NOT NULL,
        [HorasAtencion] [int] NOT NULL,
        [HorasResolucion] [int] NOT NULL,
        [PrioridadId] [uniqueidentifier] NOT NULL,
        [SoloHorasLaborales] [bit] NOT NULL DEFAULT (0),
        CONSTRAINT [PK_PoliticaSLA] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_PoliticaSLA_PrioridadTicket] FOREIGN KEY ([PrioridadId]) REFERENCES [PrioridadTicket]([Id]),
        CONSTRAINT [CHK_PoliticaSLA_Horas] CHECK ([HorasResolucion] >= [HorasAtencion])
    );
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PoliticaSLA_PrioridadId')
        CREATE NONCLUSTERED INDEX [IX_PoliticaSLA_PrioridadId] ON [PoliticaSLA]([PrioridadId]);
    PRINT 'Tabla PoliticaSLA creada.';
END
GO

-- EstadoTicket
IF OBJECT_ID(N'dbo.EstadoTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[EstadoTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](50) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        [Orden] [int] NOT NULL,
        [EsEstadoFinal] [bit] NOT NULL DEFAULT (0),
        CONSTRAINT [PK_EstadoTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_EstadoTicket_Nombre] UNIQUE ([Nombre])
    );
    PRINT 'Tabla EstadoTicket creada.';
END
GO

-- CategoriaTicket (con PoliticaSLAId para auto-SLA por categoría)
IF OBJECT_ID(N'dbo.CategoriaTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CategoriaTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        [PoliticaSLAId] [uniqueidentifier] NULL,
        CONSTRAINT [PK_CategoriaTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_CategoriaTicket_Nombre] UNIQUE ([Nombre]),
        CONSTRAINT [FK_CategoriaTicket_PoliticaSLA] FOREIGN KEY ([PoliticaSLAId]) REFERENCES [PoliticaSLA]([Id])
    );
    PRINT 'Tabla CategoriaTicket creada.';
END
GO

-- Ubicacion
IF OBJECT_ID(N'dbo.Ubicacion', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Ubicacion](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        CONSTRAINT [PK_Ubicacion] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_Ubicacion_Nombre] UNIQUE ([Nombre])
    );
    PRINT 'Tabla Ubicacion creada.';
END
GO

-- TipoEquipo
IF OBJECT_ID(N'dbo.TipoEquipo', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TipoEquipo](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](50) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        CONSTRAINT [PK_TipoEquipo] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_TipoEquipo_Nombre] UNIQUE ([Nombre])
    );
    PRINT 'Tabla TipoEquipo creada.';
END
GO

-- UbicacionEquipo
IF OBJECT_ID(N'dbo.UbicacionEquipo', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[UbicacionEquipo](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        CONSTRAINT [PK_UbicacionEquipo] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_UbicacionEquipo_Nombre] UNIQUE ([Nombre])
    );
    PRINT 'Tabla UbicacionEquipo creada.';
END
GO

-- CategoriaItem
IF OBJECT_ID(N'dbo.CategoriaItem', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[CategoriaItem](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        CONSTRAINT [PK_CategoriaItem] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_CategoriaItem_Nombre] UNIQUE ([Nombre])
    );
    PRINT 'Tabla CategoriaItem creada.';
END
GO

-- EquipoInformatico
IF OBJECT_ID(N'dbo.EquipoInformatico', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[EquipoInformatico](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [NroInventario] [nvarchar](50) NOT NULL,
        [ModeloEquipo] [nvarchar](100) NULL,
        [FechaCreacion] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
        [Estado] [nvarchar](50) NULL,
        [TipoEquipoId] [uniqueidentifier] NOT NULL,
        [UbicacionEquipoId] [uniqueidentifier] NOT NULL,
        [IdUsuarioAsignado] [uniqueidentifier] NULL,
        CONSTRAINT [PK_EquipoInformatico] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_EquipoInformatico_NroInv] UNIQUE ([NroInventario]),
        CONSTRAINT [FK_EquipoInformatico_TipoEquipo] FOREIGN KEY ([TipoEquipoId]) REFERENCES [TipoEquipo]([Id]),
        CONSTRAINT [FK_EquipoInformatico_UbicacionEquipo] FOREIGN KEY ([UbicacionEquipoId]) REFERENCES [UbicacionEquipo]([Id]),
        CONSTRAINT [FK_EquipoInformatico_Usuario] FOREIGN KEY ([IdUsuarioAsignado]) REFERENCES [Usuario]([IdUsuario])
    );
    CREATE NONCLUSTERED INDEX [IX_Equipo_TipoEquipoId] ON [EquipoInformatico]([TipoEquipoId]);
    CREATE NONCLUSTERED INDEX [IX_Equipo_UbicacionEquipoId] ON [EquipoInformatico]([UbicacionEquipoId]);
    CREATE NONCLUSTERED INDEX [IX_Equipo_IdUsuarioAsignado] ON [EquipoInformatico]([IdUsuarioAsignado]);
    PRINT 'Tabla EquipoInformatico creada.';
END
GO

-- Ticket
IF OBJECT_ID(N'dbo.Ticket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Ticket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Titulo] [nvarchar](255) NOT NULL,
        [Descripcion] [nvarchar](max) NULL,
        [FechaApertura] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
        [FechaCierre] [datetime2](7) NULL,
        [FechaUltModificacion] [datetime2](7) NULL,
        [IntegridadHash] [nvarchar](255) NULL,
        [FueAlterado] [bit] NOT NULL DEFAULT (0),
        [CategoriaId] [uniqueidentifier] NOT NULL,
        [EstadoId] [uniqueidentifier] NOT NULL,
        [UbicacionId] [uniqueidentifier] NOT NULL,
        [CreadorUsuarioId] [uniqueidentifier] NOT NULL,
        [TecnicoAsignadoId] [uniqueidentifier] NULL,
        [EquipoAsignadoId] [int] NULL,
        [PrioridadId] [uniqueidentifier] NOT NULL,
        [PoliticaSLAId] [uniqueidentifier] NULL,
        [FechaVencimiento] [datetime2](7) NULL,
        [FechaPrimeraRespuesta] [datetime2](7) NULL,
        CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Ticket_CategoriaTicket] FOREIGN KEY ([CategoriaId]) REFERENCES [CategoriaTicket]([Id]),
        CONSTRAINT [FK_Ticket_EstadoTicket] FOREIGN KEY ([EstadoId]) REFERENCES [EstadoTicket]([Id]),
        CONSTRAINT [FK_Ticket_Ubicacion] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion]([Id]),
        CONSTRAINT [FK_Ticket_CreadorUsuario] FOREIGN KEY ([CreadorUsuarioId]) REFERENCES [Usuario]([IdUsuario]),
        CONSTRAINT [FK_Ticket_TecnicoAsignado] FOREIGN KEY ([TecnicoAsignadoId]) REFERENCES [Usuario]([IdUsuario]),
        CONSTRAINT [FK_Ticket_EquipoAsignado] FOREIGN KEY ([EquipoAsignadoId]) REFERENCES [EquipoInformatico]([Id]),
        CONSTRAINT [FK_Ticket_PrioridadTicket] FOREIGN KEY ([PrioridadId]) REFERENCES [PrioridadTicket]([Id]),
        CONSTRAINT [FK_Ticket_PoliticaSLA] FOREIGN KEY ([PoliticaSLAId]) REFERENCES [PoliticaSLA]([Id]),
        CONSTRAINT [CHK_Ticket_FechaCierre] CHECK ([FechaCierre] IS NULL OR [FechaCierre] >= [FechaApertura])
    );
    CREATE NONCLUSTERED INDEX [IX_Ticket_CategoriaId] ON [Ticket]([CategoriaId]);
    CREATE NONCLUSTERED INDEX [IX_Ticket_EstadoId] ON [Ticket]([EstadoId]);
    CREATE NONCLUSTERED INDEX [IX_Ticket_PrioridadId] ON [Ticket]([PrioridadId]);
    CREATE NONCLUSTERED INDEX [IX_Ticket_CreadorUsuarioId] ON [Ticket]([CreadorUsuarioId]);
    CREATE NONCLUSTERED INDEX [IX_Ticket_TecnicoAsignadoId] ON [Ticket]([TecnicoAsignadoId]);
    CREATE NONCLUSTERED INDEX [IX_Ticket_FechaApertura] ON [Ticket]([FechaApertura] DESC);
    CREATE NONCLUSTERED INDEX [IX_Ticket_FechaVencimiento] ON [Ticket]([FechaVencimiento]) WHERE [FechaVencimiento] IS NOT NULL;
    PRINT 'Tabla Ticket creada.';
END
GO

-- ComentarioTicket
IF OBJECT_ID(N'dbo.ComentarioTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ComentarioTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [Mensaje] [nvarchar](max) NOT NULL,
        [Fecha] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
        [EsInterno] [bit] NOT NULL DEFAULT (0),
        [TicketId] [uniqueidentifier] NOT NULL,
        [UsuarioId] [uniqueidentifier] NOT NULL,
        CONSTRAINT [PK_ComentarioTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_ComentarioTicket_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ComentarioTicket_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario]([IdUsuario])
    );
    CREATE NONCLUSTERED INDEX [IX_Comentario_TicketId] ON [ComentarioTicket]([TicketId]);
    CREATE NONCLUSTERED INDEX [IX_Comentario_UsuarioId] ON [ComentarioTicket]([UsuarioId]);
    CREATE NONCLUSTERED INDEX [IX_Comentario_Fecha] ON [ComentarioTicket]([Fecha] DESC);
    PRINT 'Tabla ComentarioTicket creada.';
END
GO

-- AdjuntoTicket
IF OBJECT_ID(N'dbo.AdjuntoTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[AdjuntoTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [NombreArchivo] [nvarchar](255) NOT NULL,
        [Extension] [nvarchar](10) NOT NULL,
        [Ruta] [nvarchar](500) NOT NULL,
        [TicketId] [uniqueidentifier] NOT NULL,
        [UsuarioId] [uniqueidentifier] NOT NULL,
        CONSTRAINT [PK_AdjuntoTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_AdjuntoTicket_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AdjuntoTicket_Usuario] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario]([IdUsuario])
    );
    CREATE NONCLUSTERED INDEX [IX_Adjunto_TicketId] ON [AdjuntoTicket]([TicketId]);
    CREATE NONCLUSTERED INDEX [IX_Adjunto_UsuarioId] ON [AdjuntoTicket]([UsuarioId]);
    PRINT 'Tabla AdjuntoTicket creada.';
END
GO

-- SolucionTicket
IF OBJECT_ID(N'dbo.SolucionTicket', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[SolucionTicket](
        [Id] [uniqueidentifier] NOT NULL DEFAULT (NEWID()),
        [DescripcionSolucion] [nvarchar](max) NOT NULL,
        [FechaCierre] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
        [TicketId] [uniqueidentifier] NOT NULL,
        CONSTRAINT [PK_SolucionTicket] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_SolucionTicket_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id]) ON DELETE CASCADE,
        CONSTRAINT [UQ_SolucionTicket_TicketId] UNIQUE ([TicketId])
    );
    CREATE NONCLUSTERED INDEX [IX_Solucion_TicketId] ON [SolucionTicket]([TicketId]);
    CREATE NONCLUSTERED INDEX [IX_Solucion_FechaCierre] ON [SolucionTicket]([FechaCierre] DESC);
    PRINT 'Tabla SolucionTicket creada.';
END
GO

-- InventarioItem
IF OBJECT_ID(N'dbo.InventarioItem', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[InventarioItem](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [CodigoInventario] [nvarchar](50) NOT NULL,
        [Nombre] [nvarchar](100) NOT NULL,
        [Descripcion] [nvarchar](255) NULL,
        [Cantidad] [int] NOT NULL DEFAULT (0),
        [Valor] [decimal](18, 2) NOT NULL DEFAULT (0),
        [Unidad] [nvarchar](20) NULL,
        [FechaIngreso] [datetime2](7) NOT NULL DEFAULT (GETDATE()),
        [Disponible] [bit] NOT NULL DEFAULT (1),
        [CategoriaItemId] [uniqueidentifier] NOT NULL,
        [UbicacionEquipoId] [uniqueidentifier] NULL,
        [EquipoId] [int] NULL,
        CONSTRAINT [PK_InventarioItem] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [UQ_InventarioItem_Codigo] UNIQUE ([CodigoInventario]),
        CONSTRAINT [FK_InventarioItem_CategoriaItem] FOREIGN KEY ([CategoriaItemId]) REFERENCES [CategoriaItem]([Id]),
        CONSTRAINT [FK_InventarioItem_UbicacionEquipo] FOREIGN KEY ([UbicacionEquipoId]) REFERENCES [UbicacionEquipo]([Id]),
        CONSTRAINT [FK_InventarioItem_Equipo] FOREIGN KEY ([EquipoId]) REFERENCES [EquipoInformatico]([Id]),
        CONSTRAINT [CHK_InventarioItem_Cantidad] CHECK ([Cantidad] >= 0),
        CONSTRAINT [CHK_InventarioItem_Valor] CHECK ([Valor] >= 0)
    );
    CREATE NONCLUSTERED INDEX [IX_InvItem_CategoriaItemId] ON [InventarioItem]([CategoriaItemId]);
    CREATE NONCLUSTERED INDEX [IX_InvItem_Disponible] ON [InventarioItem]([Disponible]);
    CREATE NONCLUSTERED INDEX [IX_InvItem_EquipoId] ON [InventarioItem]([EquipoId]);
    PRINT 'Tabla InventarioItem creada.';
END
GO

-- =============================================
-- DATOS INICIALES (SEED)
-- =============================================

-- Estados de Ticket
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'Nuevo')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('Nuevo', 'Ticket recién creado, esperando asignación', 0, 0);
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'Asignado')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('Asignado', 'Ticket asignado a un técnico', 1, 0);
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'En Progreso')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('En Progreso', 'Ticket en proceso de resolución activa', 2, 0);
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'Resuelto')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('Resuelto', 'Ticket resuelto, esperando confirmación del cliente', 4, 1);
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'Cerrado')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('Cerrado', 'Ticket cerrado y finalizado', 5, 1);
IF NOT EXISTS (SELECT 1 FROM [EstadoTicket] WHERE [Nombre] = 'Cancelado')
    INSERT INTO [EstadoTicket] ([Nombre], [Descripcion], [Orden], [EsEstadoFinal]) VALUES ('Cancelado', 'Ticket cancelado por el usuario o sistema', 6, 1);
PRINT 'Estados de ticket OK.';
GO

-- Prioridades
IF NOT EXISTS (SELECT 1 FROM [PrioridadTicket] WHERE [Nombre] = N'Crítica')
    INSERT INTO [PrioridadTicket] ([Nombre], [NivelPeso], [CodigoColor], [NivelImpacto]) VALUES (N'Crítica', 1, '#FF0000', N'Impacto crítico - Sistema caído o pérdida de datos');
IF NOT EXISTS (SELECT 1 FROM [PrioridadTicket] WHERE [Nombre] = 'Alta')
    INSERT INTO [PrioridadTicket] ([Nombre], [NivelPeso], [CodigoColor], [NivelImpacto]) VALUES ('Alta', 25, '#FF8C00', N'Alto impacto - Funcionalidad importante afectada');
IF NOT EXISTS (SELECT 1 FROM [PrioridadTicket] WHERE [Nombre] = 'Media')
    INSERT INTO [PrioridadTicket] ([Nombre], [NivelPeso], [CodigoColor], [NivelImpacto]) VALUES ('Media', 50, '#FFD700', N'Impacto medio - Funcionalidad secundaria afectada');
IF NOT EXISTS (SELECT 1 FROM [PrioridadTicket] WHERE [Nombre] = 'Baja')
    INSERT INTO [PrioridadTicket] ([Nombre], [NivelPeso], [CodigoColor], [NivelImpacto]) VALUES ('Baja', 75, '#1E90FF', N'Bajo impacto - Inconveniente menor');
IF NOT EXISTS (SELECT 1 FROM [PrioridadTicket] WHERE [Nombre] = 'Muy Baja')
    INSERT INTO [PrioridadTicket] ([Nombre], [NivelPeso], [CodigoColor], [NivelImpacto]) VALUES ('Muy Baja', 100, '#808080', N'Impacto mínimo - Mejora o consulta');
PRINT 'Prioridades OK.';
GO

-- Políticas SLA
IF NOT EXISTS (SELECT 1 FROM [PoliticaSLA] WHERE [Nombre] = N'SLA Crítico 24x7')
    INSERT INTO [PoliticaSLA] ([Nombre], [HorasAtencion], [HorasResolucion], [PrioridadId], [SoloHorasLaborales])
    VALUES (N'SLA Crítico 24x7', 1, 4, (SELECT Id FROM PrioridadTicket WHERE Nombre = N'Crítica'), 0);
IF NOT EXISTS (SELECT 1 FROM [PoliticaSLA] WHERE [Nombre] = 'SLA Alto 24x7')
    INSERT INTO [PoliticaSLA] ([Nombre], [HorasAtencion], [HorasResolucion], [PrioridadId], [SoloHorasLaborales])
    VALUES ('SLA Alto 24x7', 2, 8, (SELECT Id FROM PrioridadTicket WHERE Nombre = 'Alta'), 0);
IF NOT EXISTS (SELECT 1 FROM [PoliticaSLA] WHERE [Nombre] = 'SLA Medio Laboral')
    INSERT INTO [PoliticaSLA] ([Nombre], [HorasAtencion], [HorasResolucion], [PrioridadId], [SoloHorasLaborales])
    VALUES ('SLA Medio Laboral', 8, 24, (SELECT Id FROM PrioridadTicket WHERE Nombre = 'Media'), 1);
IF NOT EXISTS (SELECT 1 FROM [PoliticaSLA] WHERE [Nombre] = 'SLA Bajo Laboral')
    INSERT INTO [PoliticaSLA] ([Nombre], [HorasAtencion], [HorasResolucion], [PrioridadId], [SoloHorasLaborales])
    VALUES ('SLA Bajo Laboral', 16, 48, (SELECT Id FROM PrioridadTicket WHERE Nombre = 'Baja'), 1);
IF NOT EXISTS (SELECT 1 FROM [PoliticaSLA] WHERE [Nombre] = 'SLA Muy Bajo Laboral')
    INSERT INTO [PoliticaSLA] ([Nombre], [HorasAtencion], [HorasResolucion], [PrioridadId], [SoloHorasLaborales])
    VALUES ('SLA Muy Bajo Laboral', 24, 72, (SELECT Id FROM PrioridadTicket WHERE Nombre = 'Muy Baja'), 1);
PRINT 'Políticas SLA OK.';
GO

-- Categorías de Ticket (con SLA asignada)
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Conectividad')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Conectividad', 'Problemas de red, internet y conectividad', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Alto 24x7'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Email')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Email', N'Problemas con correo electrónico', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Alto 24x7'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = N'Telefonía')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES (N'Telefonía', N'Problemas con líneas telefónicas y sistemas VoIP', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Alto 24x7'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Hardware')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Hardware', N'Problemas con equipos físicos (PC, impresoras, periféricos)', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Medio Laboral'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Software')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Software', 'Problemas con aplicaciones y sistemas operativos', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Medio Laboral'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Actualizaciones')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Actualizaciones', N'Solicitudes de actualización de software o sistemas', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Bajo Laboral'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'Accesos')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('Accesos', 'Solicitudes de permisos, credenciales y accesos', (SELECT Id FROM PoliticaSLA WHERE Nombre = 'SLA Bajo Laboral'));
IF NOT EXISTS (SELECT 1 FROM [CategoriaTicket] WHERE [Nombre] = 'ProblemasVarios')
    INSERT INTO [CategoriaTicket] ([Nombre], [Descripcion], [PoliticaSLAId]) VALUES ('ProblemasVarios', 'Problemas generales no categorizados', NULL);
PRINT 'Categorías de ticket OK.';
GO

-- Ubicaciones
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'IT')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('IT', N'Departamento de Tecnología de la Información');
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'Administracion')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('Administracion', N'Departamento de Administración');
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'Finanzas')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('Finanzas', 'Departamento de Finanzas y Contabilidad');
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'Operaciones')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('Operaciones', 'Departamento de Operaciones');
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'Marketing')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('Marketing', 'Departamento de Marketing y Comunicaciones');
IF NOT EXISTS (SELECT 1 FROM [Ubicacion] WHERE [Nombre] = 'Compras')
    INSERT INTO [Ubicacion] ([Nombre], [Descripcion]) VALUES ('Compras', 'Departamento de Compras y Adquisiciones');
PRINT 'Ubicaciones OK.';
GO

-- Tipos de Equipo
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'PC')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('PC', 'Computadora de escritorio');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Notebook')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Notebook', N'Computadora portátil');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Servidor')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Servidor', N'Servidor físico o virtual');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Impresora')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Impresora', 'Impresora de oficina');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Scanner')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Scanner', N'Escáner de documentos');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Router')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Router', 'Enrutador de red');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Switch')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Switch', 'Switch de red');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Firewall')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Firewall', 'Dispositivo de seguridad de red');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Tablet')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Tablet', N'Tableta electrónica');
IF NOT EXISTS (SELECT 1 FROM [TipoEquipo] WHERE [Nombre] = 'Telefono')
    INSERT INTO [TipoEquipo] ([Nombre], [Descripcion]) VALUES ('Telefono', N'Teléfono IP o móvil corporativo');
PRINT 'Tipos de equipo OK.';
GO

-- Ubicaciones de Equipo
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Oficina 101')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Oficina 101', 'Oficina administrativa planta 1');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Oficina 201')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Oficina 201', 'Oficina gerencial planta 2');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Sala de Servidores')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Sala de Servidores', 'Datacenter principal');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = N'Almacén TI')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES (N'Almacén TI', N'Almacén de equipos y repuestos');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Sala de Reuniones A')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Sala de Reuniones A', 'Sala de reuniones principal');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Sala de Reuniones B')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Sala de Reuniones B', 'Sala de reuniones secundaria');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = 'Open Space')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES ('Open Space', N'Área de trabajo abierta');
IF NOT EXISTS (SELECT 1 FROM [UbicacionEquipo] WHERE [Nombre] = N'Recepción')
    INSERT INTO [UbicacionEquipo] ([Nombre], [Descripcion]) VALUES (N'Recepción', N'Área de recepción y atención al público');
PRINT 'Ubicaciones de equipo OK.';
GO

-- Categorías de Item (Inventario)
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'RAM')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('RAM', 'Memoria de acceso aleatorio');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'ROM')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('ROM', 'Memoria de solo lectura');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Procesador')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Procesador', 'Unidad central de procesamiento (CPU)');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Disco Duro')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Disco Duro', 'Disco duro HDD o SSD');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Monitor')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Monitor', 'Pantalla o display');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Teclado')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Teclado', 'Teclado de computadora');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Mouse')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Mouse', N'Ratón de computadora');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Cable')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Cable', N'Cables diversos (red, alimentación, etc.)');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Tarjeta de Red')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Tarjeta de Red', 'Adaptador de red NIC');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Fuente de Poder')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Fuente de Poder', N'Fuente de alimentación');
IF NOT EXISTS (SELECT 1 FROM [CategoriaItem] WHERE [Nombre] = 'Otro')
    INSERT INTO [CategoriaItem] ([Nombre], [Descripcion]) VALUES ('Otro', 'Otros componentes no categorizados');
PRINT 'Categorías de item OK.';
GO

PRINT '';
PRINT '=== TicketsProDB_v2 lista ===';
GO
