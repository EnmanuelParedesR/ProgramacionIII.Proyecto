
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/23/2017 06:04:14
-- Generated from EDMX file: C:\Users\ema\Desktop\COPIA MAS ORIGINAL OJO PROBAR - Copy (6) - Copy\MiPrimerProyectoMVC\MiPrimerProyectoMVC\Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Proyecto_Final];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Plan_Service_Plan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Plan_Service] DROP CONSTRAINT [FK_Plan_Service_Plan];
GO
IF OBJECT_ID(N'[dbo].[FK_Plan_Service_Service]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Plan_Service] DROP CONSTRAINT [FK_Plan_Service_Service];
GO
IF OBJECT_ID(N'[dbo].[FK_Subscription_Client]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subscriptions] DROP CONSTRAINT [FK_Subscription_Client];
GO
IF OBJECT_ID(N'[dbo].[FK_Subscription_Plan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Subscriptions] DROP CONSTRAINT [FK_Subscription_Plan];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Plan_Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plan_Service];
GO
IF OBJECT_ID(N'[dbo].[Plans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plans];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[Subscriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Subscriptions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [ClientID] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [MiddleName] varchar(50)  NULL,
    [LastName] varchar(50)  NOT NULL,
    [PassportNumber] varchar(50)  NULL,
    [IsActive] bit  NOT NULL,
    [IsUsed] bit  NOT NULL
);
GO

-- Creating table 'Plan_Service'
CREATE TABLE [dbo].[Plan_Service] (
    [Plan_Service1] int IDENTITY(1,1) NOT NULL,
    [PlanID] int  NOT NULL,
    [ServiceID] int  NOT NULL
);
GO

-- Creating table 'Plans'
CREATE TABLE [dbo].[Plans] (
    [PlanID] int IDENTITY(1,1) NOT NULL,
    [PlanName] varchar(50)  NOT NULL,
    [PlanDescription] varchar(250)  NULL,
    [Price] float  NULL,
    [IsActive] bit  NOT NULL,
    [IsUsed] bit  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [ServiceID] int IDENTITY(1,1) NOT NULL,
    [ServiceName] varchar(50)  NOT NULL,
    [ServiceDescription] varchar(250)  NULL,
    [IsActive] bit  NOT NULL,
    [IsUsed] bit  NOT NULL
);
GO

-- Creating table 'Subscriptions'
CREATE TABLE [dbo].[Subscriptions] (
    [SubscriptionID] int IDENTITY(1,1) NOT NULL,
    [PlanID] int  NOT NULL,
    [ClientID] int  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [IsExpired] bit  NOT NULL,
    [IsUsed] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientID] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([ClientID] ASC);
GO

-- Creating primary key on [Plan_Service1] in table 'Plan_Service'
ALTER TABLE [dbo].[Plan_Service]
ADD CONSTRAINT [PK_Plan_Service]
    PRIMARY KEY CLUSTERED ([Plan_Service1] ASC);
GO

-- Creating primary key on [PlanID] in table 'Plans'
ALTER TABLE [dbo].[Plans]
ADD CONSTRAINT [PK_Plans]
    PRIMARY KEY CLUSTERED ([PlanID] ASC);
GO

-- Creating primary key on [ServiceID] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([ServiceID] ASC);
GO

-- Creating primary key on [SubscriptionID] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [PK_Subscriptions]
    PRIMARY KEY CLUSTERED ([SubscriptionID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ClientID] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [FK_Subscription_Client]
    FOREIGN KEY ([ClientID])
    REFERENCES [dbo].[Clients]
        ([ClientID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Subscription_Client'
CREATE INDEX [IX_FK_Subscription_Client]
ON [dbo].[Subscriptions]
    ([ClientID]);
GO

-- Creating foreign key on [PlanID] in table 'Plan_Service'
ALTER TABLE [dbo].[Plan_Service]
ADD CONSTRAINT [FK_Plan_Service_Plan]
    FOREIGN KEY ([PlanID])
    REFERENCES [dbo].[Plans]
        ([PlanID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Plan_Service_Plan'
CREATE INDEX [IX_FK_Plan_Service_Plan]
ON [dbo].[Plan_Service]
    ([PlanID]);
GO

-- Creating foreign key on [ServiceID] in table 'Plan_Service'
ALTER TABLE [dbo].[Plan_Service]
ADD CONSTRAINT [FK_Plan_Service_Service]
    FOREIGN KEY ([ServiceID])
    REFERENCES [dbo].[Services]
        ([ServiceID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Plan_Service_Service'
CREATE INDEX [IX_FK_Plan_Service_Service]
ON [dbo].[Plan_Service]
    ([ServiceID]);
GO

-- Creating foreign key on [PlanID] in table 'Subscriptions'
ALTER TABLE [dbo].[Subscriptions]
ADD CONSTRAINT [FK_Subscription_Plan]
    FOREIGN KEY ([PlanID])
    REFERENCES [dbo].[Plans]
        ([PlanID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Subscription_Plan'
CREATE INDEX [IX_FK_Subscription_Plan]
ON [dbo].[Subscriptions]
    ([PlanID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------