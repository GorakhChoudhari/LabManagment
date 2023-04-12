USE [master]
GO
/****** Object:  Database [GC-DataBase]    Script Date: 12-04-2023 17:54:04 ******/
CREATE DATABASE [GC-DataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GC-DataBase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GC-DataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GC-DataBase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\GC-DataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [GC-DataBase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GC-DataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GC-DataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GC-DataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GC-DataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GC-DataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GC-DataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [GC-DataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GC-DataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GC-DataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GC-DataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GC-DataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GC-DataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GC-DataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GC-DataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GC-DataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GC-DataBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GC-DataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GC-DataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GC-DataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GC-DataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GC-DataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GC-DataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GC-DataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GC-DataBase] SET RECOVERY FULL 
GO
ALTER DATABASE [GC-DataBase] SET  MULTI_USER 
GO
ALTER DATABASE [GC-DataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GC-DataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GC-DataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GC-DataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GC-DataBase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GC-DataBase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GC-DataBase', N'ON'
GO
ALTER DATABASE [GC-DataBase] SET QUERY_STORE = OFF
GO
USE [GC-DataBase]
GO
/****** Object:  Table [dbo].[BookCategories]    Script Date: 12-04-2023 17:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [nvarchar](50) NULL,
	[SubCategory] [nvarchar](50) NULL,
 CONSTRAINT [PK_BookCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 12-04-2023 17:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Author] [nvarchar](100) NOT NULL,
	[Price] [float] NOT NULL,
	[Ordered] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12-04-2023 17:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[OrderedOn] [datetime] NOT NULL,
	[Returned] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12-04-2023 17:54:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Mobile] [nvarchar](15) NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Blocked] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UserType] [nvarchar](20) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BookCategories] ON 

INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (1, N'computer', N'algorithm')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (2, N'mechanical', N'machine')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (3, N'computer', N'programming languages')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (4, N'computer', N'networking')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (5, N'computer', N'hardware')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (6, N'computer', N'operating systems')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (7, N'mechanical', N'transfer of energy')
INSERT [dbo].[BookCategories] ([Id], [Category], [SubCategory]) VALUES (8, N'mathematics', N'calculus')
SET IDENTITY_INSERT [dbo].[BookCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (2, N'Data Structures and Algorithms', N'Narsimha Karumanchi', 500, 0, 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (3, N'Let us C', N'Adam Drozdek', 400, 0, 3)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (5, N'Introduction to Algorithms', N'Thomas H. Cormen', 500, 1, 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (9, N'Introduction to Algorithms: A Creative Approach', N'Udi Manber', 500, 0, 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (13, N'Data Structures and Algorithms in C++', N'Adam Drozdek', 500, 0, 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (14, N'Python for Everybody: Exploring Data Using Python 3', N'Charles Severance', 400, 0, 3)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (16, N'Java: A Beginner''s Guide', N'Herbert Schildt', 400, 1, 3)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (17, N'JavaScript: The Definitive Guide', N'David Flanagan', 900, 1, 3)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (22, N'The C++ Programming Language', N'Bjarne Stroustrup', 1000, 1, 3)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (24, N'Computer Networking: A Top Down', N'James Kurose and Keith Ross', 350, 1, 4)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (25, N'Data Communications and Networking', N'Behrouz A. Forouzan', 670, 1, 4)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (27, N'Introduction to Networking: How the Internet Works', N'Charles Severance', 600, 1, 4)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (28, N'Computer Networking for Beginners', N'Russell Scott', 600, 1, 4)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (30, N'Microprocessor 80386 Hardware Reference Manual', N'Intel', 2000, 1, 5)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (32, N'Microprocessor 80387 Hardware Reference Manual', N'Intel', 2000, 0, 5)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (34, N'Microprocessor 8085', N'Ramesh Gaonkar', 2000, 1, 5)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (36, N'Microprocessor 8086', N'Ramesh Gaonkar', 2000, 0, 5)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (38, N'Operating System Concepts', N'Abraham Silberschatz and Peter Galvin', 1500, 1, 6)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (40, N'Design of the UNIX Operating Systems', N'Maurice Bach', 1500, 1, 6)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (41, N'Operating System: A Design-oriented Approach', N'Charles Crowley', 1500, 1, 6)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (43, N'Operating Systems: A Concept-Based Approach', N'D M Dhamdhere', 1500, 1, 6)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (45, N'Fluid Mechanics and Hydraulic Machines', N'Dr. R K Bansal', 1000, 0, 7)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (51, N'An Introduction to Mechanics', N'David Kleppne', 1000, 0, 2)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (53, N'Theory of Machines', N'SS Rattan', 1000, 0, 2)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (54, N'Design of Machine Elements', N'V B Bhandari', 1200, 0, 2)
INSERT [dbo].[Books] ([Id], [Title], [Author], [Price], [Ordered], [CategoryId]) VALUES (55, N'Fundamentals of Thermodynamics', N'Claus Borgnakke', 1200, 1, 7)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (1, 2, 2, CAST(N'2023-04-10T16:08:38.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (2, 2, 3, CAST(N'2023-04-10T16:09:03.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (3, 2, 3, CAST(N'2023-04-10T16:09:04.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (4, 2, 36, CAST(N'2023-04-10T16:09:31.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (5, 1, 2, CAST(N'2023-04-10T16:11:47.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (6, 1, 2, CAST(N'2023-04-10T16:11:58.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (7, 1, 13, CAST(N'2023-04-10T16:12:08.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (8, 1, 13, CAST(N'2023-04-10T16:12:09.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (9, 1, 13, CAST(N'2023-04-10T16:12:09.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (10, 1, 13, CAST(N'2023-04-10T16:12:10.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (11, 2, 2, CAST(N'2023-04-10T16:17:57.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (12, 2, 13, CAST(N'2023-04-10T16:18:06.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (13, 2, 9, CAST(N'2023-04-10T17:17:33.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (14, 2, 14, CAST(N'2023-03-10T16:08:38.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (15, 2, 14, CAST(N'2023-03-10T16:08:38.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (16, 2, 14, CAST(N'2023-03-10T16:08:38.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (17, 2, 16, CAST(N'2023-03-10T16:08:38.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (18, 2, 17, CAST(N'2023-04-10T17:19:54.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (19, 2, 17, CAST(N'2023-04-10T17:20:33.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (20, 2, 22, CAST(N'2023-04-10T17:20:45.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (21, 2, 34, CAST(N'2023-04-10T17:21:33.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (22, 2, 24, CAST(N'2023-04-10T17:22:29.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (23, 2, 24, CAST(N'2023-04-10T17:22:31.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (24, 2, 24, CAST(N'2023-04-10T17:22:32.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (25, 2, 24, CAST(N'2023-04-10T17:22:32.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (26, 2, 24, CAST(N'2023-04-10T17:22:32.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (27, 2, 24, CAST(N'2023-04-10T17:22:33.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (28, 2, 24, CAST(N'2023-04-10T17:22:33.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (29, 4, 25, CAST(N'2023-04-10T17:27:53.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (30, 4, 27, CAST(N'2023-04-10T17:31:24.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (31, 4, 28, CAST(N'2023-04-10T17:31:40.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (32, 4, 32, CAST(N'2023-04-10T17:32:31.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (33, 4, 30, CAST(N'2023-04-10T17:33:34.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (34, 4, 45, CAST(N'2023-04-10T17:33:44.000' AS DateTime), 1)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (35, 4, 55, CAST(N'2023-04-10T17:33:46.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (36, 4, 38, CAST(N'2023-04-10T17:38:21.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (37, 4, 40, CAST(N'2023-04-10T17:38:57.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (38, 4, 41, CAST(N'2023-04-10T17:42:23.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (39, 4, 43, CAST(N'2023-04-10T17:42:30.000' AS DateTime), 0)
INSERT [dbo].[Orders] ([Id], [UserId], [BookId], [OrderedOn], [Returned]) VALUES (1002, 5, 51, CAST(N'2023-04-11T12:14:19.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Mobile], [Password], [Blocked], [Active], [CreatedOn], [UserType]) VALUES (1, N'admin', N'admin', N'admin@gmail.com', N'string', N'admin1996', 0, 1, CAST(N'2023-04-03T17:48:55.000' AS DateTime), N'ADMIN')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Mobile], [Password], [Blocked], [Active], [CreatedOn], [UserType]) VALUES (2, N'Gorakh', N'Chaudhari', N'gorakh@gmail.com', N'9922687061', N'gorakh@777', 0, 1, CAST(N'2023-04-03T17:57:51.000' AS DateTime), N'USER')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Mobile], [Password], [Blocked], [Active], [CreatedOn], [UserType]) VALUES (4, N'Sammed', N'Rangole', N'sammed123@gmail.com', N'', N'sammed123', 0, 1, CAST(N'2023-04-04T12:43:55.000' AS DateTime), N'USER')
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [Mobile], [Password], [Blocked], [Active], [CreatedOn], [UserType]) VALUES (5, N'Nandhakumar', N'A', N'nandhakumar123@gmail.com', N'', N'nandha55', 0, 1, CAST(N'2023-04-04T14:30:20.000' AS DateTime), N'USER')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_BookCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[BookCategories] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_BookCategories]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Books]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
USE [master]
GO
ALTER DATABASE [GC-DataBase] SET  READ_WRITE 
GO
