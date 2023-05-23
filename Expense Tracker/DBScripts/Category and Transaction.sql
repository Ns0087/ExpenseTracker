USE [ExpenseDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23-05-2023 12:40:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Icon] [nvarchar](10) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[UserId] [int] NULL DEFAULT 0,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object: Table [dbo].[Categories] Insert Query Script Date: 23-05-2023 12:40:50 *******/

INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Salary/Wages', N'💰', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Freelance/Contract Work', N'💼', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Investments', N'📈', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Business Income', N'📊', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Gifts', N'🎁', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Refunds/Reimbursements', N'🔁', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Other Income', N'💸', N'Income')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Food and Dining', N'🍔', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Transportation', N'🚗', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Rent/Mortgage', N'🏠', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Entertainment', N'🎬', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Shopping', N'🛍️', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Health and Fitness', N'💪', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Travel', N'✈️', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Education', N'📚', N'Expense')
GO
INSERT [dbo].[Categories] ([Title], [Icon], [Type]) VALUES (N'Utilities', N'💡', N'Expense')
GO

/****** Object:  Table [dbo].[Transactions]    Script Date: 23-05-2023 12:40:50 ******/
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


INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (8, 25, N'Dinner at a local restaurant', CAST(N'2023-05-05T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (8, 12, N'Lunch with colleagues', CAST(N'2023-05-10T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (8, 7, N'Coffee and snacks at a café', CAST(N'2023-05-18T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (9, 40, N'Gasoline refill', CAST(N'2023-05-03T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (9, 15, N'Uber ride to the airport', CAST(N'2023-05-23T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (9, 20, N'Public transportation monthly pass', CAST(N'2023-05-01T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (16, 85, N'Electricity bill', CAST(N'2023-05-12T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (16, 30, N'Internet service provider bill', CAST(N'2023-05-16T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (16, 20, N'Water bill', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (10, 1200, N'Monthly rent payment', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (10, 500, N'Additional security deposit', CAST(N'2023-05-18T00:00:00.0000000' AS DateTime2), 4)
GO
INSERT [dbo].[Transactions] ([CategoryId], [Amount], [Note], [Date], [UserId]) VALUES (11, 30, N'Movie tickets', CAST(N'2023-05-20T00:00:00.0000000' AS DateTime2), 4)
GO

ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Categories_CategoryId]
GO
