SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].[Employee] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (1, N'John', N'Nash', N'343-456-4567', N'John@gmail.com')
INSERT INTO [dbo].[Employee] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (2, N'Kate', N'Winslet', N'325-789-9056', N'kate@gmail.com')
SET IDENTITY_INSERT [dbo].[Employee] OFF

SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT INTO [dbo].[Customer] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (1, N'Jarred', N'Paul', N'345-567-6789', N'jarr@gmail.com')
INSERT INTO [dbo].[Customer] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (2, N'Nic', N'wolf', N'234-345-8905', N'nick@gmail.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF

SET IDENTITY_INSERT [dbo].[Item] ON
INSERT INTO [dbo].[Item] ([Id], [Name], [UnitPrice]) VALUES (1, N'Machines', N'100')
SET IDENTITY_INSERT [dbo].[Item] OFF



