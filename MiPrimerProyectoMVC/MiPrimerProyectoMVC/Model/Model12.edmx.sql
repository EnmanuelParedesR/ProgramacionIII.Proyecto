
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/28/2017 06:04:03
-- Generated from EDMX file: C:\Users\ema\Desktop\Original_DatePicker - Copy (2)\MiPrimerProyectoMVC\MiPrimerProyectoMVC\Model\Model12.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BaseDeDatos];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_PermisoDenegadoPorRol_dbo_Permiso_PermisoID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermisoDenegadoPorRols] DROP CONSTRAINT [FK_dbo_PermisoDenegadoPorRol_dbo_Permiso_PermisoID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_PermisoDenegadoPorRol_dbo_Rol_RolID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermisoDenegadoPorRols] DROP CONSTRAINT [FK_dbo_PermisoDenegadoPorRol_dbo_Rol_RolID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Usuario_dbo_Rol_Rol_id]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Usuarios] DROP CONSTRAINT [FK_dbo_Usuario_dbo_Rol_Rol_id];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PermisoDenegadoPorRols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermisoDenegadoPorRols];
GO
IF OBJECT_ID(N'[dbo].[Permisoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permisoes];
GO
IF OBJECT_ID(N'[dbo].[Rols]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rols];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PermisoDenegadoPorRols'
CREATE TABLE [dbo].[PermisoDenegadoPorRols] (
    [PermisoID] int  NOT NULL,
    [RolID] int  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Permisoes'
CREATE TABLE [dbo].[Permisoes] (
    [PermisoID] int  NOT NULL,
    [Modulo] varchar(20)  NOT NULL,
    [Descripcion] varchar(100)  NOT NULL
);
GO

-- Creating table 'Rols'
CREATE TABLE [dbo].[Rols] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NOT NULL,
    [PermisoPermisoID] int  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(max)  NOT NULL,
    [Correo] varchar(max)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [Rol_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'PermisoDenegadoPorRols'
ALTER TABLE [dbo].[PermisoDenegadoPorRols]
ADD CONSTRAINT [PK_PermisoDenegadoPorRols]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [PermisoID] in table 'Permisoes'
ALTER TABLE [dbo].[Permisoes]
ADD CONSTRAINT [PK_Permisoes]
    PRIMARY KEY CLUSTERED ([PermisoID] ASC);
GO

-- Creating primary key on [id] in table 'Rols'
ALTER TABLE [dbo].[Rols]
ADD CONSTRAINT [PK_Rols]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PermisoID] in table 'PermisoDenegadoPorRols'
ALTER TABLE [dbo].[PermisoDenegadoPorRols]
ADD CONSTRAINT [FK_dbo_PermisoDenegadoPorRol_dbo_Permiso_PermisoID]
    FOREIGN KEY ([PermisoID])
    REFERENCES [dbo].[Permisoes]
        ([PermisoID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_PermisoDenegadoPorRol_dbo_Permiso_PermisoID'
CREATE INDEX [IX_FK_dbo_PermisoDenegadoPorRol_dbo_Permiso_PermisoID]
ON [dbo].[PermisoDenegadoPorRols]
    ([PermisoID]);
GO

-- Creating foreign key on [RolID] in table 'PermisoDenegadoPorRols'
ALTER TABLE [dbo].[PermisoDenegadoPorRols]
ADD CONSTRAINT [FK_dbo_PermisoDenegadoPorRol_dbo_Rol_RolID]
    FOREIGN KEY ([RolID])
    REFERENCES [dbo].[Rols]
        ([id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_PermisoDenegadoPorRol_dbo_Rol_RolID'
CREATE INDEX [IX_FK_dbo_PermisoDenegadoPorRol_dbo_Rol_RolID]
ON [dbo].[PermisoDenegadoPorRols]
    ([RolID]);
GO

-- Creating foreign key on [Rol_id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [FK_dbo_Usuario_dbo_Rol_Rol_id]
    FOREIGN KEY ([Rol_id])
    REFERENCES [dbo].[Rols]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Usuario_dbo_Rol_Rol_id'
CREATE INDEX [IX_FK_dbo_Usuario_dbo_Rol_Rol_id]
ON [dbo].[Usuarios]
    ([Rol_id]);
GO

-- Creating foreign key on [PermisoPermisoID] in table 'Rols'
ALTER TABLE [dbo].[Rols]
ADD CONSTRAINT [FK_PermisoRol]
    FOREIGN KEY ([PermisoPermisoID])
    REFERENCES [dbo].[Permisoes]
        ([PermisoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermisoRol'
CREATE INDEX [IX_FK_PermisoRol]
ON [dbo].[Rols]
    ([PermisoPermisoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------