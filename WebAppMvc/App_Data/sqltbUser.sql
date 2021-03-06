USE [AchieveDB]
GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 06/07/2017 16:15:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RealName] [nvarchar](50) NOT NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[IsAble] [bit] NULL,
	[IfChangePwd] [bit] NULL,
	[Description] [nvarchar](max) NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateBy] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_tbUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帐户ID(网站关联的AccountID)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帐户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'AccountName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帐户密码（32位MD5加密）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'MobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系的邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'介绍描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbUser', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
SET IDENTITY_INSERT [dbo].[tbUser] ON
INSERT [dbo].[tbUser] ([ID], [AccountName], [Password], [RealName], [MobilePhone], [Email], [IsAble], [IfChangePwd], [Description], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [IsDel]) VALUES (1, N'admin', N'96E79218965EB72C92A549DD5A330112', N'超级管理员', N'13600000002', N'1232@qq.com', 1, 1, N'超级管理员', N'admin', CAST(0x0000A62B00B5308C AS DateTime), N'wzy', CAST(0x0000A78B00AEC292 AS DateTime), 0)
INSERT [dbo].[tbUser] ([ID], [AccountName], [Password], [RealName], [MobilePhone], [Email], [IsAble], [IfChangePwd], [Description], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [IsDel]) VALUES (2, N'test001', N'E10ADC3949BA59ABBE56E057F20F88', N'测试', N'13588888888', N'test@qq.com', 1, 1, N'测试', N'admin', CAST(0x0000A62B00B5308C AS DateTime), N'admin', CAST(0x0000A62B00B5308C AS DateTime), 0)
INSERT [dbo].[tbUser] ([ID], [AccountName], [Password], [RealName], [MobilePhone], [Email], [IsAble], [IfChangePwd], [Description], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [IsDel]) VALUES (3, N'lf123456', N'E10ADC3949BA59ABBE56E057F20F88', N'技术测试', N'13600000000', N'1231@qq.com', 1, 1, N'', N'admin', CAST(0x0000A62B00B5308C AS DateTime), N'wzy', CAST(0x0000A78B00AED8EF AS DateTime), 0)
INSERT [dbo].[tbUser] ([ID], [AccountName], [Password], [RealName], [MobilePhone], [Email], [IsAble], [IfChangePwd], [Description], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [IsDel]) VALUES (6, N'wzy', N'28A0C6171DB3E623FD9D2E11A09238C7', N'超级管理员', NULL, NULL, 1, 1, N'超级管理员', N'wzy', CAST(0x0000A78900FD4395 AS DateTime), N'wzy', CAST(0x0000A78900FD4749 AS DateTime), 0)
INSERT [dbo].[tbUser] ([ID], [AccountName], [Password], [RealName], [MobilePhone], [Email], [IsAble], [IfChangePwd], [Description], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [IsDel]) VALUES (23, N'test1', N'C51CD8E64B0AEB778364765013DF9EBE', N'test1', N'11111', N'111@qq.com', 0, 1, N'aaa', N'wzy', CAST(0x0000A78B00F0879C AS DateTime), N'wzy', CAST(0x0000A78B01064FB1 AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[tbUser] OFF
