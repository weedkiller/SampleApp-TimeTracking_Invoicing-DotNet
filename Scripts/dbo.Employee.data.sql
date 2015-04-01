SET IDENTITY_INSERT [dbo].[Employee] ON
INSERT INTO [dbo].[Employee] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (1, N'John', N'Nash', N'343-456-4567', N'John@gmail.com')
INSERT INTO [dbo].[Employee] ([Id], [GivenName], [FamilyName], [PrimaryPhone], [PrimaryEmailAddr]) VALUES (2, N'Kate', N'Winslet', N'325-789-9056', N'kate@gmail.com')
SET IDENTITY_INSERT [dbo].[Employee] OFF
