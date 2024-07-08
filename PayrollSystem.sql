USE [master]
GO
/****** Object:  Database [PayrollSystem]    Script Date: 7/7/2024 11:36:44 PM ******/
CREATE DATABASE [PayrollSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PayrollSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayrollSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PayrollSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PayrollSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PayrollSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PayrollSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PayrollSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PayrollSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PayrollSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PayrollSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PayrollSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [PayrollSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PayrollSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PayrollSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PayrollSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PayrollSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PayrollSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PayrollSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PayrollSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PayrollSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PayrollSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PayrollSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PayrollSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PayrollSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PayrollSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PayrollSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PayrollSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PayrollSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PayrollSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PayrollSystem] SET  MULTI_USER 
GO
ALTER DATABASE [PayrollSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PayrollSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PayrollSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PayrollSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PayrollSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PayrollSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PayrollSystem] SET QUERY_STORE = OFF
GO
USE [PayrollSystem]
GO
/****** Object:  UserDefinedTableType [dbo].[DependentTableType]    Script Date: 7/7/2024 11:36:44 PM ******/
CREATE TYPE [dbo].[DependentTableType] AS TABLE(
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[BirthDate] [date] NULL,
	[Relation] [int] NULL
)
GO
/****** Object:  Table [dbo].[Dependent]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dependent](
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[EmployeeId] [int] NOT NULL,
	[DependentId] [int] IDENTITY(1,1) NOT NULL,
	[BirthDate] [date] NULL,
	[Relation] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DependentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DependentType]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DependentType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[DateOfBirth] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeBenefits]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeBenefits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StandardEmployeeBenefitsCost] [decimal](18, 2) NULL,
	[AgedDependentBenefitCost] [decimal](18, 2) NULL,
	[CostlyResourceBenefitCost] [decimal](18, 2) NULL,
	[TotalEnrolledBenefitCost] [decimal](18, 2) NULL,
	[TotalBaseSalary] [decimal](18, 2) NULL,
	[TotalBaseSalaryAfterDeduction] [decimal](18, 2) NULL,
	[MonthlyPayCheckSalaryAfterDeduction] [decimal](18, 2) NULL,
	[DependentChildBenefitsCost] [decimal](18, 2) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Dependent] ON 

INSERT [dbo].[Dependent] ([FirstName], [LastName], [EmployeeId], [DependentId], [BirthDate], [Relation]) VALUES (N'Spouse', N'Morant', 2, 1, CAST(N'1998-03-03' AS Date), 2)
INSERT [dbo].[Dependent] ([FirstName], [LastName], [EmployeeId], [DependentId], [BirthDate], [Relation]) VALUES (N'Child1', N'Morant', 2, 2, CAST(N'2020-06-23' AS Date), 4)
INSERT [dbo].[Dependent] ([FirstName], [LastName], [EmployeeId], [DependentId], [BirthDate], [Relation]) VALUES (N'Child2', N'Morant', 2, 3, CAST(N'2021-05-18' AS Date), 4)
INSERT [dbo].[Dependent] ([FirstName], [LastName], [EmployeeId], [DependentId], [BirthDate], [Relation]) VALUES (N'DP', N'Jordan', 3, 4, CAST(N'1974-01-02' AS Date), 3)
SET IDENTITY_INSERT [dbo].[Dependent] OFF
GO
SET IDENTITY_INSERT [dbo].[DependentType] ON 

INSERT [dbo].[DependentType] ([Id], [Name]) VALUES (1, N'None')
INSERT [dbo].[DependentType] ([Id], [Name]) VALUES (2, N'Spouse')
INSERT [dbo].[DependentType] ([Id], [Name]) VALUES (3, N'DomesticPartner')
INSERT [dbo].[DependentType] ([Id], [Name]) VALUES (4, N'Child')
SET IDENTITY_INSERT [dbo].[DependentType] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Email], [DateOfBirth]) VALUES (1, N'LeBron', N'James', NULL, CAST(N'1984-12-30' AS Date))
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Email], [DateOfBirth]) VALUES (2, N'Ja', N'Morant', NULL, CAST(N'1999-08-10' AS Date))
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Email], [DateOfBirth]) VALUES (3, N'Michael', N'Jordan', NULL, CAST(N'1963-02-17' AS Date))
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeBenefits] ON 

INSERT [dbo].[EmployeeBenefits] ([Id], [EmployeeId], [StandardEmployeeBenefitsCost], [AgedDependentBenefitCost], [CostlyResourceBenefitCost], [TotalEnrolledBenefitCost], [TotalBaseSalary], [TotalBaseSalaryAfterDeduction], [MonthlyPayCheckSalaryAfterDeduction], [DependentChildBenefitsCost]) VALUES (1, 1, CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(12000.00 AS Decimal(18, 2)), CAST(75420.99 AS Decimal(18, 2)), CAST(63420.99 AS Decimal(18, 2)), CAST(2439.27 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[EmployeeBenefits] ([Id], [EmployeeId], [StandardEmployeeBenefitsCost], [AgedDependentBenefitCost], [CostlyResourceBenefitCost], [TotalEnrolledBenefitCost], [TotalBaseSalary], [TotalBaseSalaryAfterDeduction], [MonthlyPayCheckSalaryAfterDeduction], [DependentChildBenefitsCost]) VALUES (2, 2, CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(1847.30 AS Decimal(18, 2)), CAST(28247.30 AS Decimal(18, 2)), CAST(92365.22 AS Decimal(18, 2)), CAST(64117.92 AS Decimal(18, 2)), CAST(2466.07 AS Decimal(18, 2)), CAST(14400.00 AS Decimal(18, 2)))
INSERT [dbo].[EmployeeBenefits] ([Id], [EmployeeId], [StandardEmployeeBenefitsCost], [AgedDependentBenefitCost], [CostlyResourceBenefitCost], [TotalEnrolledBenefitCost], [TotalBaseSalary], [TotalBaseSalaryAfterDeduction], [MonthlyPayCheckSalaryAfterDeduction], [DependentChildBenefitsCost]) VALUES (3, 3, CAST(12000.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(2864.22 AS Decimal(18, 2)), CAST(14864.22 AS Decimal(18, 2)), CAST(143211.12 AS Decimal(18, 2)), CAST(128346.90 AS Decimal(18, 2)), CAST(4936.42 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[EmployeeBenefits] OFF
GO
ALTER TABLE [dbo].[Dependent]  WITH CHECK ADD  CONSTRAINT [Dependent_Relation_FK] FOREIGN KEY([Relation])
REFERENCES [dbo].[DependentType] ([Id])
GO
ALTER TABLE [dbo].[Dependent] CHECK CONSTRAINT [Dependent_Relation_FK]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_dependent_details]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_dependent_details]
@dependentId int = 0
AS

BEGIN
    SELECT 
	E.EmployeeId,
	D.DependentId,
	D.FirstName AS D_FirstName,
	D.LastName AS D_LastName,
	D.Relation,
	D.BirthDate
FROM 
	[dbo].[Dependent] D
INNER JOIN 
	[dbo].[Employee] E  ON E.EmployeeId = D.EmployeeId
where 
	D.DependentId = 	@dependentId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_dependent_details_all]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_get_dependent_details_all]
AS

BEGIN
     SELECT 
	 E.EmployeeId,
	 D.DependentId,
	 D.FirstName AS D_FirstName,
	 D.LastName AS D_LastName,
	 D.Relation,
	 D.BirthDate
	FROM 
		[dbo].[Dependent] D
	INNER JOIN 
		[dbo].[Employee] E  ON E.EmployeeId = D.EmployeeId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_employee_details]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_employee_details]
@employeeId int = 0
AS

BEGIN	
SELECT 
	 E.EmployeeId,
	 E.FirstName,
	 E.LastName,
	 E.DateOfBirth AS E_DateOfBirth,
	 D.DependentId,
	 D.FirstName AS D_FirstName,
	 D.LastName AS D_LastName,
	 D.Relation,
	 D.BirthDate as D_DateOfBirth
    ,B.TotalBaseSalary
		FROM [dbo].[Employee] E
		LEFT JOIN  [dbo].[Dependent] D ON D.EmployeeId = E.EmployeeId
		LEFT JOIN  [dbo].[EmployeeBenefits] B ON B.EmployeeId = E.EmployeeId
	where 
		E.EmployeeId = 	@employeeId
END

GO
/****** Object:  StoredProcedure [dbo].[sp_get_employee_details_all]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_employee_details_all]
AS

BEGIN

SELECT 
	 E.EmployeeId,
	 E.FirstName,
	 E.LastName,
	 E.Email,
	 D.DependentId,
	 E.DateOfBirth AS E_DateOfBirth,
	 D.BirthDate as D_DateOfBirth,
	 D.FirstName AS D_FirstName,
	 D.LastName AS D_LastName,
	 D.Relation
    ,B.TotalBaseSalary
		FROM [dbo].[Employee] E
		LEFT JOIN  [dbo].[Dependent] D ON D.EmployeeId = E.EmployeeId
		LEFT JOIN  [dbo].[EmployeeBenefits] B ON B.EmployeeId = E.EmployeeId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_enrolled_benefits]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_enrolled_benefits]
@employeeId int = 0
AS

BEGIN	
SELECT 
	 E.EmployeeId,
	 E.FirstName,
	 E.LastName,
	 E.Email,
	 E.DateOfBirth AS E_DateOfBirth,
	 D.DependentId,
	 D.FirstName AS D_FirstName,
	 D.LastName AS D_LastName,
	 D.Relation,
	 D.BirthDate as D_DateOfBirth,
     B.StandardEmployeeBenefitsCost
    ,B.AgedDependentBenefitCost
    ,B.CostlyResourceBenefitCost
    ,B.TotalEnrolledBenefitCost
    ,B.TotalBaseSalary
    ,B.TotalBaseSalaryAfterDeduction
    ,B.MonthlyPayCheckSalaryAfterDeduction
	,B.DependentChildBenefitsCost
		FROM [dbo].[Employee] E
		LEFT JOIN  [dbo].[Dependent] D ON D.EmployeeId = E.EmployeeId
		LEFT JOIN  [dbo].[EmployeeBenefits] B ON B.EmployeeId = E.EmployeeId
	where 
		E.EmployeeId = 	@employeeId
END

GO
/****** Object:  StoredProcedure [dbo].[sp_get_enrolled_benefits_all]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_get_enrolled_benefits_all]
AS

BEGIN

SELECT 
	 E.EmployeeId,
	 E.FirstName,
	 E.LastName,
	 E.Email,
	 D.DependentId,
	 E.DateOfBirth AS E_DateOfBirth,
	 D.BirthDate as D_DateOfBirth,
	 D.FirstName AS D_FirstName,
	 D.LastName AS D_LastName,
	 D.Relation,
     B.StandardEmployeeBenefitsCost
    ,B.AgedDependentBenefitCost
    ,B.CostlyResourceBenefitCost
    ,B.TotalEnrolledBenefitCost
    ,B.TotalBaseSalary
    ,B.TotalBaseSalaryAfterDeduction
    ,B.MonthlyPayCheckSalaryAfterDeduction
	,B.DependentChildBenefitsCost
		FROM [dbo].[Employee] E
		LEFT JOIN  [dbo].[Dependent] D ON D.EmployeeId = E.EmployeeId
		LEFT JOIN  [dbo].[EmployeeBenefits] B ON B.EmployeeId = E.EmployeeId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_save_dependent]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--This is the Stored Procedure
CREATE PROCEDURE [dbo].[sp_save_dependent]
(
-- which accepts one table value parameter. 
-- It should be noted that the parameter is readonly
  @Dependent As [dbo].[DependentTableType] Readonly
)
AS

Begin
-- We simply insert values into the DB table from the parameter
-- The table value parameter can be used like a table with only read rights
Insert Into dbo.Dependent(FirstName,LastName,EmployeeId,BirthDate,Relation)
Select FirstName, LastName, EmployeeId,BirthDate,Relation From @Dependent
End
GO
/****** Object:  StoredProcedure [dbo].[sp_save_employee]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_save_employee]
@employeeId INT,
@firstName VARCHAR(50),
@lastName VARCHAR(50),
@dateOfBirth Date
AS
Begin

    Declare @id int;

	-- CREATE EMPLOYEE RECORD
	INSERT INTO [dbo].[Employee] (FirstName,LastName,DateOfBirth)
     VALUES (@firstName,@lastName,@dateOfBirth)
	SET @id=SCOPE_IDENTITY()

	select @id
END


GO
/****** Object:  StoredProcedure [dbo].[sp_save_enrolledbenefits]    Script Date: 7/7/2024 11:36:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_save_enrolledbenefits]
@employeeId INT,
@StandardEmployeeBenefitsCost DECIMAL(18,2),
@dependentChildBenefitsCost DECIMAL(18,2),
@agedDependentBenefitCost DECIMAL(18,2),
@CostlyResourceBenefitCost  DECIMAL(18,2),
@totalEnrolledBenefitCost DECIMAL(18,2),
@totalBaseSalary		DECIMAL(18,2),
@totalBaseSalaryAfterDeduction DECIMAL(18,2),
@monthlyPayCheckSalaryAfterDeduction DECIMAL(18,2)
AS
BEGIN
INSERT INTO [dbo].[EmployeeBenefits]
           ([EmployeeId]
           ,[StandardEmployeeBenefitsCost]
           ,[AgedDependentBenefitCost]
           ,[CostlyResourceBenefitCost]
           ,[TotalEnrolledBenefitCost]
           ,[TotalBaseSalary]
           ,[TotalBaseSalaryAfterDeduction]
           ,[MonthlyPayCheckSalaryAfterDeduction]
           ,[DependentChildBenefitsCost])
     VALUES
           (@employeeId
           ,@StandardEmployeeBenefitsCost
           ,@agedDependentBenefitCost
           ,@CostlyResourceBenefitCost
           ,@totalEnrolledBenefitCost
           ,@totalBaseSalary
           ,@totalBaseSalaryAfterDeduction
           ,@monthlyPayCheckSalaryAfterDeduction,
		    @dependentChildBenefitsCost)
END


GO
USE [master]
GO
ALTER DATABASE [PayrollSystem] SET  READ_WRITE 
GO
