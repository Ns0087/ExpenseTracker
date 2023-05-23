Create Database ExpenseDB

USE [ExpenseDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23-05-2023 17:15:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Icon] [nvarchar](10) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 23-05-2023 17:15:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Note] [nvarchar](75) NULL,
	[Date] [datetime2](7) NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23-05-2023 17:15:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Gender] [varchar](50) NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (1, N'Salary/Wages', N'💰', N'Income', 4)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (2, N'Freelance/Contract Work', N'💼', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (3, N'Investments', N'📈', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (4, N'Business Income', N'📊', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (5, N'Gifts', N'🎁', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (6, N'Refunds/Reimbursements', N'🔁', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (7, N'Other Income', N'💸', N'Income', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (8, N'Food and Dining', N'🍔', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (9, N'Transportation', N'🚗', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (10, N'Rent/Mortgage', N'🏠', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (11, N'Entertainment', N'🎬', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (12, N'Shopping', N'🛍️', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (13, N'Health and Fitness', N'💪', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (14, N'Travel', N'✈️', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (15, N'Education', N'📚', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (16, N'Utilities', N'💡', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (17, N'Insurance', N'🛡️', N'Expense', 0)
GO
INSERT [dbo].[Categories] ([CategoryId], [Title], [Icon], [Type], [UserId]) VALUES (18, N'Taxes', N'📝', N'Expense', 0)
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (2, 8, 12, N'Lunch with colleagues', CAST(N'2023-05-10T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (5, 8, 7, N'Coffee and snacks at a café', CAST(N'2023-05-15T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (7, 9, 40, N'Gasoline refill', CAST(N'2023-05-03T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (8, 9, 15, N'Uber ride to the airport', CAST(N'2023-05-23T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (9, 9, 20, N'Public transportation monthly pass', CAST(N'2023-05-01T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (10, 16, 85, N'Electricity bill', CAST(N'2023-05-12T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (11, 16, 30, N'Internet service provider bill', CAST(N'2023-05-16T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (12, 16, 20, N'Water bill', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (13, 10, 1200, N'Monthly rent payment', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (14, 10, 500, N'Additional security deposit', CAST(N'2023-05-18T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (15, 11, 30, N'Movie tickets', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([TransactionId], [CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (16, 8, 7, N'Coffee and snacks at a café', CAST(N'2023-05-18T00:00:00.0000000' AS DateTime2), 4)
GO
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
INSERT [dbo].[User] ([UserId], [FullName], [UserName], [Email], [Phone], [Password], [Gender], [IsDeleted]) VALUES (4, N'Suraj Pandey', N'Suraj02', N'suraj.pandey@gmail.com', N'12946385', N'Suraj', N'Male', 0)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Categories] ADD  DEFAULT ((0)) FOR [UserId]
GO
