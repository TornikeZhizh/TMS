IF DB_ID('TMS') IS NULL
	CREATE DATABASE TMS
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Permissions] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tasks] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [ShortDescription] nvarchar(max) NULL,
    [LongDescription] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(450) NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RolePermissions] (
    [Id] bigint NOT NULL IDENTITY,
    [RoleId] bigint NOT NULL,
    [PermissionName] nvarchar(max) NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolePermissions_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TaskAttachments] (
    [Id] bigint NOT NULL IDENTITY,
    [TaskId] bigint NOT NULL,
    [FileUid] nvarchar(max) NULL,
    [FileName] nvarchar(max) NULL,
    CONSTRAINT [PK_TaskAttachments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaskAttachments_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [RoleId] bigint NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_RolePermissions_RoleId] ON [RolePermissions] ([RoleId]);
GO

CREATE INDEX [IX_TaskAttachments_TaskId] ON [TaskAttachments] ([TaskId]);
GO

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Users_UserName] ON [Users] ([UserName]) WHERE [UserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210421045112_InitialCreate', N'6.0.0-preview.3.21201.2');
GO

INSERT INTO Users(UserName, PasswordHash, PasswordSalt)
VALUES('admin', 0xCD960906C727EC1910BCEBF07C70987539C699F100F14EDC589A86F1BE56D729FCAB7AE59F3D4058DAB98A1F80F1CE7C59415064EEAC18F716AF1C2A0FB520F5,
0x9B9ABEB8BC73EDD87A9C1D1F61FCCA230964B9EFE7F1C4C98359EC04F3C5F3390582C7A7A26CB97E8B925510670008EB90B0ECDEB1BD1A50611C352009F465E7DDDCF363EA877AF2C31136D25D960337B5297779EC7AFD4A292CC4634374F3073EF6F463252149F60E4395C1F0963AC30EB76F714F540FF45302BD55CAF68A88
)
GO

INSERT INTO [Permissions](Name)
VALUES ('task_view'), ('task_add'),('task_edit'),('task_delete')
GO

COMMIT;
GO

