USE [master]
GO
/****** Object:  Database [ProgettoParadigmi]    Script Date: 22/03/2024 13:55:00 ******/
CREATE DATABASE [ProgettoParadigmi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProgettoParadigmi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProgettoParadigmi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProgettoParadigmi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProgettoParadigmi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProgettoParadigmi] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProgettoParadigmi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProgettoParadigmi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProgettoParadigmi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProgettoParadigmi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProgettoParadigmi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProgettoParadigmi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET RECOVERY FULL 
GO
ALTER DATABASE [ProgettoParadigmi] SET  MULTI_USER 
GO
ALTER DATABASE [ProgettoParadigmi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProgettoParadigmi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProgettoParadigmi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProgettoParadigmi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProgettoParadigmi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProgettoParadigmi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProgettoParadigmi', N'ON'
GO
ALTER DATABASE [ProgettoParadigmi] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProgettoParadigmi] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProgettoParadigmi]
GO
/****** Object:  User [paradigmi]    Script Date: 22/03/2024 13:55:00 ******/
CREATE USER [paradigmi] FOR LOGIN [paradigmi] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Ordini]    Script Date: 22/03/2024 13:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordini](
	[NumeroOrdine] [int] IDENTITY(1,1) NOT NULL,
	[DataOrdine] [date] NOT NULL,
	[ClienteEmail] [varchar](100) NOT NULL,
	[IndirizzoConsegna_Citta] [varchar](200) NULL,
	[IndirizzoConsegna_Cap] [varchar](200) NULL,
	[IndirizzoConsegna_Via] [varchar](200) NULL,
	[IndirizzoConsegna_Civico] [varchar](10) NULL,
 CONSTRAINT [PK_Ordini] PRIMARY KEY CLUSTERED 
(
	[NumeroOrdine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Portate]    Script Date: 22/03/2024 13:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Portate](
	[Nome] [varchar](50) NOT NULL,
	[Prezzo] [decimal](10, 2) NOT NULL,
	[Tipo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Portate] PRIMARY KEY CLUSTERED 
(
	[Nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortateOrdinate]    Script Date: 22/03/2024 13:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortateOrdinate](
	[Quantita] [int] NOT NULL,
	[OrdinazioneId] [int] NOT NULL,
	[PortataNome] [varchar](50) NOT NULL,
	[Turno] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utenti]    Script Date: 22/03/2024 13:55:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utenti](
	[Email] [varchar](100) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[Cognome] [varchar](100) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Ruolo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Utenti] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Antipasto', CAST(8.00 AS Decimal(10, 2)), N'Antipasto')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Bevanda', CAST(1.50 AS Decimal(10, 2)), N'Bevande')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Contorno', CAST(3.50 AS Decimal(10, 2)), N'Contorno')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Crostini', CAST(2.00 AS Decimal(10, 2)), N'Antipasto')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Dolce', CAST(5.00 AS Decimal(10, 2)), N'Dolce')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Primo', CAST(9.50 AS Decimal(10, 2)), N'Primo')
INSERT [dbo].[Portate] ([Nome], [Prezzo], [Tipo]) VALUES (N'Secondo', CAST(15.00 AS Decimal(10, 2)), N'Secondo')
GO
INSERT [dbo].[Utenti] ([Email], [Nome], [Cognome], [Password], [Ruolo]) VALUES (N'changeMe@admin.com', N'Admin', N'Admin', N'Adm1n!!', N'Amministratore')
GO
USE [master]
GO
ALTER DATABASE [ProgettoParadigmi] SET  READ_WRITE 
GO
