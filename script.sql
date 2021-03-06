USE [tempdb]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2022/7/4 下午 02:35:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[Account] [varchar](200) NOT NULL,
	[Password] [varchar](128) NOT NULL,
	[HashKey] [varchar](128) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[MediaFileID] [uniqueidentifier] NULL,
	[is_available] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[b_empno] [uniqueidentifier] NOT NULL,
	[b_date] [datetime] NOT NULL,
	[e_empno] [uniqueidentifier] NULL,
	[e_date] [datetime] NULL,
	[d_empno] [uniqueidentifier] NULL,
	[d_date] [datetime] NULL,
 CONSTRAINT [PK_Users_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 2022/7/4 下午 02:35:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[is_available] [bit] NOT NULL,
	[b_empno] [uniqueidentifier] NULL,
	[b_date] [datetime] NOT NULL,
	[e_empno] [uniqueidentifier] NULL,
	[e_date] [datetime] NULL,
	[d_empno] [uniqueidentifier] NULL,
	[d_date] [datetime] NULL,
 CONSTRAINT [PK__Vendors__3214EC27EFC4DCBD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [b_date]
GO
ALTER TABLE [dbo].[Vendors] ADD  CONSTRAINT [DF_Vendors_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Vendors] ADD  CONSTRAINT [DF_Vendors_b_date]  DEFAULT (getdate()) FOR [b_date]
GO
