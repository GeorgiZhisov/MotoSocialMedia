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
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [DisplayName] nvarchar(50) NOT NULL,
    [Bio] nvarchar(500) NULL,
    [City] nvarchar(80) NULL,
    [AvatarUrl] nvarchar(500) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [ListingCategories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(60) NOT NULL,
    [IsVehicle] bit NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_ListingCategories] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Follows] (
    [FollowerId] nvarchar(450) NOT NULL,
    [FolloweeId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_Follows] PRIMARY KEY ([FollowerId], [FolloweeId]),
    CONSTRAINT [CK_Follows_NotSelf] CHECK ([FollowerId] <> [FolloweeId]),
    CONSTRAINT [FK_Follows_AspNetUsers_FolloweeId] FOREIGN KEY ([FolloweeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Follows_AspNetUsers_FollowerId] FOREIGN KEY ([FollowerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Groups] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(80) NOT NULL,
    [Description] nvarchar(2000) NULL,
    [CoverImageUrl] nvarchar(500) NULL,
    [IsPrivate] bit NOT NULL,
    [CreatorId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Groups_AspNetUsers_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Motorcycles] (
    [Id] int NOT NULL IDENTITY,
    [Make] nvarchar(50) NOT NULL,
    [Model] nvarchar(80) NOT NULL,
    [Year] int NOT NULL,
    [EngineCc] int NOT NULL,
    [Category] int NOT NULL,
    [Nickname] nvarchar(50) NULL,
    [Description] nvarchar(2000) NULL,
    [ImageUrl] nvarchar(500) NULL,
    [OwnerId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Motorcycles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Motorcycles_AspNetUsers_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Routes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(120) NOT NULL,
    [Description] nvarchar(3000) NULL,
    [StartPlace] nvarchar(120) NOT NULL,
    [EndPlace] nvarchar(120) NOT NULL,
    [DistanceKm] float NOT NULL,
    [Difficulty] int NOT NULL,
    [GpxFileUrl] nvarchar(500) NULL,
    [AuthorId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Routes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Routes_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Stories] (
    [Id] int NOT NULL IDENTITY,
    [MediaUrl] nvarchar(500) NOT NULL,
    [MediaType] int NOT NULL,
    [Caption] nvarchar(200) NULL,
    [ExpiresOn] datetime2 NOT NULL,
    [AuthorId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Stories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Stories_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [Listings] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(120) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Condition] int NOT NULL,
    [Status] int NOT NULL,
    [City] nvarchar(80) NOT NULL,
    [ViewsCount] int NOT NULL,
    [CategoryId] int NOT NULL,
    [SellerId] nvarchar(450) NOT NULL,
    [Make] nvarchar(50) NULL,
    [Model] nvarchar(80) NULL,
    [Year] int NULL,
    [MileageKm] int NULL,
    [EngineCc] int NULL,
    [MotorcycleCategory] int NULL,
    [Modifications] nvarchar(2000) NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Listings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Listings_AspNetUsers_SellerId] FOREIGN KEY ([SellerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Listings_ListingCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [ListingCategories] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [GroupMembers] (
    [GroupId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [Role] int NOT NULL,
    [JoinedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_GroupMembers] PRIMARY KEY ([GroupId], [UserId]),
    CONSTRAINT [FK_GroupMembers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_GroupMembers_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Posts] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(3000) NOT NULL,
    [Location] nvarchar(120) NULL,
    [AuthorId] nvarchar(450) NOT NULL,
    [MotorcycleId] int NULL,
    [GroupId] int NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Posts_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Posts_Motorcycles_MotorcycleId] FOREIGN KEY ([MotorcycleId]) REFERENCES [Motorcycles] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [Events] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(120) NOT NULL,
    [Description] nvarchar(3000) NULL,
    [Location] nvarchar(200) NOT NULL,
    [StartsOn] datetime2 NOT NULL,
    [EndsOn] datetime2 NULL,
    [Capacity] int NULL,
    [OrganizerId] nvarchar(450) NOT NULL,
    [GroupId] int NULL,
    [RouteId] int NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Events_AspNetUsers_OrganizerId] FOREIGN KEY ([OrganizerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Events_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Events_Routes_RouteId] FOREIGN KEY ([RouteId]) REFERENCES [Routes] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [Conversations] (
    [Id] int NOT NULL IDENTITY,
    [BuyerId] nvarchar(450) NOT NULL,
    [SellerId] nvarchar(450) NOT NULL,
    [ListingId] int NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Conversations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Conversations_AspNetUsers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Conversations_AspNetUsers_SellerId] FOREIGN KEY ([SellerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Conversations_Listings_ListingId] FOREIGN KEY ([ListingId]) REFERENCES [Listings] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [ListingImages] (
    [Id] int NOT NULL IDENTITY,
    [Url] nvarchar(500) NOT NULL,
    [IsCover] bit NOT NULL,
    [ListingId] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_ListingImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ListingImages_Listings_ListingId] FOREIGN KEY ([ListingId]) REFERENCES [Listings] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Reviews] (
    [Id] int NOT NULL IDENTITY,
    [Rating] int NOT NULL,
    [Comment] nvarchar(1000) NULL,
    [ReviewerId] nvarchar(450) NOT NULL,
    [SellerId] nvarchar(450) NOT NULL,
    [ListingId] int NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Reviews_NotSelf] CHECK ([ReviewerId] <> [SellerId]),
    CONSTRAINT [CK_Reviews_Rating] CHECK ([Rating] BETWEEN 1 AND 5),
    CONSTRAINT [FK_Reviews_AspNetUsers_ReviewerId] FOREIGN KEY ([ReviewerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reviews_AspNetUsers_SellerId] FOREIGN KEY ([SellerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Reviews_Listings_ListingId] FOREIGN KEY ([ListingId]) REFERENCES [Listings] ([Id]) ON DELETE SET NULL
);

CREATE TABLE [Comments] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(1000) NOT NULL,
    [PostId] int NOT NULL,
    [AuthorId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comments_AspNetUsers_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Media] (
    [Id] int NOT NULL IDENTITY,
    [Url] nvarchar(500) NOT NULL,
    [Type] int NOT NULL,
    [Order] int NOT NULL,
    [PostId] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Media_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PostLikes] (
    [PostId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    CONSTRAINT [PK_PostLikes] PRIMARY KEY ([PostId], [UserId]),
    CONSTRAINT [FK_PostLikes_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PostLikes_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [EventParticipants] (
    [EventId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [RegisteredOn] datetime2 NOT NULL,
    CONSTRAINT [PK_EventParticipants] PRIMARY KEY ([EventId], [UserId]),
    CONSTRAINT [FK_EventParticipants_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_EventParticipants_Events_EventId] FOREIGN KEY ([EventId]) REFERENCES [Events] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Messages] (
    [Id] int NOT NULL IDENTITY,
    [Content] nvarchar(2000) NOT NULL,
    [IsRead] bit NOT NULL,
    [ConversationId] int NOT NULL,
    [SenderId] nvarchar(450) NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ModifiedOn] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [DeletedOn] datetime2 NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE INDEX [IX_AspNetUsers_IsDeleted] ON [AspNetUsers] ([IsDeleted]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Comments_AuthorId] ON [Comments] ([AuthorId]);

CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);

CREATE INDEX [IX_Conversations_BuyerId_SellerId_ListingId] ON [Conversations] ([BuyerId], [SellerId], [ListingId]);

CREATE INDEX [IX_Conversations_ListingId] ON [Conversations] ([ListingId]);

CREATE INDEX [IX_Conversations_SellerId] ON [Conversations] ([SellerId]);

CREATE INDEX [IX_EventParticipants_UserId] ON [EventParticipants] ([UserId]);

CREATE INDEX [IX_Events_GroupId] ON [Events] ([GroupId]);

CREATE INDEX [IX_Events_OrganizerId] ON [Events] ([OrganizerId]);

CREATE INDEX [IX_Events_RouteId] ON [Events] ([RouteId]);

CREATE INDEX [IX_Events_StartsOn] ON [Events] ([StartsOn]);

CREATE INDEX [IX_Follows_FolloweeId] ON [Follows] ([FolloweeId]);

CREATE INDEX [IX_GroupMembers_UserId] ON [GroupMembers] ([UserId]);

CREATE INDEX [IX_Groups_CreatorId] ON [Groups] ([CreatorId]);

CREATE UNIQUE INDEX [IX_Groups_Name] ON [Groups] ([Name]);

CREATE UNIQUE INDEX [IX_ListingCategories_Name] ON [ListingCategories] ([Name]);

CREATE INDEX [IX_ListingImages_ListingId] ON [ListingImages] ([ListingId]);

CREATE INDEX [IX_Listings_CategoryId_Status_Price] ON [Listings] ([CategoryId], [Status], [Price]);

CREATE INDEX [IX_Listings_City] ON [Listings] ([City]);

CREATE INDEX [IX_Listings_EngineCc] ON [Listings] ([EngineCc]);

CREATE INDEX [IX_Listings_SellerId] ON [Listings] ([SellerId]);

CREATE INDEX [IX_Media_PostId] ON [Media] ([PostId]);

CREATE INDEX [IX_Messages_ConversationId_CreatedOn] ON [Messages] ([ConversationId], [CreatedOn]);

CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);

CREATE INDEX [IX_Motorcycles_OwnerId] ON [Motorcycles] ([OwnerId]);

CREATE INDEX [IX_PostLikes_UserId] ON [PostLikes] ([UserId]);

CREATE INDEX [IX_Posts_AuthorId] ON [Posts] ([AuthorId]);

CREATE INDEX [IX_Posts_CreatedOn] ON [Posts] ([CreatedOn]);

CREATE INDEX [IX_Posts_GroupId] ON [Posts] ([GroupId]);

CREATE INDEX [IX_Posts_MotorcycleId] ON [Posts] ([MotorcycleId]);

CREATE INDEX [IX_Reviews_ListingId] ON [Reviews] ([ListingId]);

CREATE INDEX [IX_Reviews_ReviewerId] ON [Reviews] ([ReviewerId]);

CREATE INDEX [IX_Reviews_SellerId] ON [Reviews] ([SellerId]);

CREATE INDEX [IX_Routes_AuthorId] ON [Routes] ([AuthorId]);

CREATE INDEX [IX_Stories_AuthorId] ON [Stories] ([AuthorId]);

CREATE INDEX [IX_Stories_ExpiresOn] ON [Stories] ([ExpiresOn]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261109092033_InitialCreate', N'10.0.14');

COMMIT;
GO

