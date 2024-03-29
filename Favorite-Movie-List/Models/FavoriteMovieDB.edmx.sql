
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/09/2019 11:31:06
-- Generated from EDMX file: C:\Users\yendi\source\repos\APIMovieProject\Favorite-Movie-List\Models\FavoriteMovieDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FavoriteMovieDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FavoriteMovies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FavoriteMovies];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FavoriteMovies'
CREATE TABLE [dbo].[FavoriteMovies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(50)  NULL,
    [ImdbId] nchar(20)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'FavoriteMovies'
ALTER TABLE [dbo].[FavoriteMovies]
ADD CONSTRAINT [PK_FavoriteMovies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------