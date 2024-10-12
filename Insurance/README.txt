How to setup the application
Note: I have usd the database first apprch, as I had the table structure along with sample data with little
understanding on the code side.

I have used SQL Server Managment Studio 15.0.1833
Application target framework .Net Core 3.1
Used following NuGet package:
Microsoft Entity Framework Core SqlServer 3.1.4
Microsoft Entity Framework Core Tool 3.1.4

Application Setup Steps:
1. Database Setup: Run the following script to setup the database and table structure:

1.1 Create Database
USE [master]
GO

/****** Object:  Database [InsuranceDB]    Script Date: 08-06-2020 00:46:52 ******/
CREATE DATABASE [InsuranceDB2]

1.2 Create table CoveragePlan
USE [InsuranceDB]
GO

/****** Object:  Table [dbo].[CoveragePlan]    Script Date: 08-06-2020 00:52:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CoveragePlan](
	[Coverage_Plan] [nvarchar](50) NOT NULL,
	[Eligibility_Date_From] [date] NOT NULL,
	[Eligibility_Date_To] [date] NOT NULL,
	[Eligibility_Country] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CoveragePlan] PRIMARY KEY CLUSTERED 
(
	[Coverage_Plan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


1.3 Create RateChart
USE [InsuranceDB]
GO

/****** Object:  Table [dbo].[RateChart]    Script Date: 08-06-2020 00:54:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RateChart](
	[Coverage_Plan] [nvarchar](50) NOT NULL,
	[Customer_Gender] [nvarchar](50) NOT NULL,
	[Start_Age] [numeric](18, 0) NOT NULL,
	[End_Age] [numeric](18, 0) NOT NULL,
	[Net_Price] [money] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RateChart]  WITH CHECK ADD  CONSTRAINT [FK_RateChart_CoveragePlan] FOREIGN KEY([Coverage_Plan])
REFERENCES [dbo].[CoveragePlan] ([Coverage_Plan])
GO

ALTER TABLE [dbo].[RateChart] CHECK CONSTRAINT [FK_RateChart_CoveragePlan]
GO





1.4 Create Tsble Contracts
USE [InsuranceDB]
GO

/****** Object:  Table [dbo].[Contracts]    Script Date: 08-06-2020 00:55:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contracts](
	[Customer_Name] [varchar](100) NOT NULL,
	[Customer_Address] [varchar](max) NOT NULL,
	[Customer_Gender] [nchar](10) NOT NULL,
	[Customer_Country] [varchar](20) NOT NULL,
	[Customer_Date_Of_Birth] [date] NOT NULL,
	[Sale_Date] [date] NOT NULL,
	[Net_Price] [decimal](18, 0) NOT NULL,
	[Coverage_Plan] [nvarchar](50) NOT NULL,
	[ContractId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_CoveragePlan] FOREIGN KEY([Coverage_Plan])
REFERENCES [dbo].[CoveragePlan] ([Coverage_Plan])
GO

ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_CoveragePlan]
GO

2. Application Setup: Open the project in visual studio

2.1 Install the NuGet packages
    Microsoft Entity Framework Core SqlServer 3.1.4
    Microsoft Entity Framework Core Tool 3.1.4

2.2 Scaffold the DBContext
    In Visual studio in NuGet packager manger console give the command
    Scaffold-DbContext "Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

2.3 Start the IIS server thats it.


3. Hit the API provide as postman collection.
# ToDo
