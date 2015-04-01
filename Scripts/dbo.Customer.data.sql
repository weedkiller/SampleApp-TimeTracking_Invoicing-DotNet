SET IDENTITY_INSERT [dbo].[Customer] ON
INSERT INTO [dbo].[Customer] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (1, N'Jarred', N'Paul', N'345-567-6789', N'jarr@gmail.com')
INSERT INTO [dbo].[Customer] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (2, N'Nic', N'wolf', N'234-345-8905', N'nick@gmail.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF
