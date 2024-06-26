USE [master]
GO
/****** Object:  Database [Shahab]    Script Date: 9/15/2015 10:14:43 AM ******/
CREATE DATABASE [Shahab] ON  PRIMARY 
( NAME = N'Shahab', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Shahab.mdf' , SIZE = 39936KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Shahab_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\Shahab_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1256_CI_AS
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'Shahab', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shahab].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shahab] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shahab] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shahab] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shahab] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shahab] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shahab] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shahab] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Shahab] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shahab] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shahab] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shahab] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shahab] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shahab] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shahab] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shahab] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shahab] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shahab] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shahab] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shahab] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shahab] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shahab] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shahab] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shahab] SET RECOVERY FULL 
GO
ALTER DATABASE [Shahab] SET  MULTI_USER 
GO
ALTER DATABASE [Shahab] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shahab] SET DB_CHAINING OFF 
GO
USE [Shahab]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFamily]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DeleteFamily](
	@FamilyId int
)
As
BEGIN
	Delete FROM Family Where FamilyId = @FamilyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetBaseInfo]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetBaseInfo]
(
  @BaseInfoId int
)
As
SELECT * 
   FROM BaseInfo 
		WHERE BaseInfoId = @BaseInfoId
GO
/****** Object:  StoredProcedure [dbo].[GetFamilyByUserId]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetFamilyByUserId]
(
  @UserId int
)
As
SELECT * 
   FROM Family 
		WHERE UserId = @UserId
GO
/****** Object:  StoredProcedure [dbo].[GetFamilyMembersByFamilyId]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetFamilyMembersByFamilyId](
	@FamilyId int
)
As
BEGIN
	SELECT * FROM FamilyMembers WHERE FamilyId = @FamilyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetPlace]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetPlace](
	@PlaceId int
)
As
BEGIN
	SELECT * FROM Places WHERE PlaceId = @PlaceId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetUser](
@UserId int
)
As
BEGIN
	SELECT * FROM Users WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByUsernamePass]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetUserByUsernamePass](
@NationalCode varchar(50),
@Password nvarchar(50)
)
As
BEGIN
	SELECT * FROM Users WHERE NationalCode = @NationalCode AND Password = HashBytes('MD5', @Password)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertFamily]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[InsertFamily](
	@FamilyId int, 
	@UserId int, 
	@BlockNumber VARCHAR(50),
	@FamilyNumber VARCHAR(50),
	@BuildingNumber VARCHAR(50),
	@FamilyType int,
	@PopulationType int,
	@PlaceId int,
	@RegionStatus int,
	@OwnerShipStatus int,
	@PostalCode VARCHAR(50),
	@MobileNumber VARCHAR(50),
	@PhoneNumber VARCHAR(50),
	@Address NVARCHAR(MAX),
	@Status int,
	@Priority FLOAT,
	@RegisterDate DATETIME
)
As
BEGIN
	INSERT INTO Family([FamilyId], [UserId], [BlockNumber], [FamilyNumber], [BuildingNumber]
	, [FamilyType], [PopulationType], [PlaceId], [RegionStatus], [OwnerShipStatus]
	, [PostalCode], [MobileNumber], [PhoneNumber], [Address], [Status], [Priority], [RegisterDate])
	VALUES(@FamilyId, @UserId, @BlockNumber,@FamilyNumber, @BuildingNumber, @FamilyType
	, @PopulationType, @PlaceId, @RegionStatus, @OwnerShipStatus, @PostalCode
	, @MobileNumber, @PhoneNumber, @Address, @Status, @Priority, @RegisterDate)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertFamilyMemeber]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[InsertFamilyMemeber](
	@FamilyMemberId int, 
	@FirstName nvarchar(250),
	@LastName nvarchar(250), 
	@Nationality int,
	@NationalCode varchar(50), 
	@Gender int, 
	@RelationShip int, 
	@BirthDate datetime,
	@InhabitancyStatus int,
	@MaritalStatus int, 
	@EducationStatus int, 
	@ActivityStatus int, 
	@JobType int, 
	@InsuranceFirst int,
	@InsuranceSecond int, 
	@FamilyId int, 
	@Status int, 
	@Priority float
)
As
BEGIN
	INSERT INTO FamilyMembers(FamilyMemberId, FirstName, LastName, Nationality
	,NationalCode, Gender, RelationShip, BirthDate
	,InhabitancyStatus, MaritalStatus, EducationStatus
	,ActivityStatus, JobType, InsuranceFirst
	,InsuranceSecond, FamilyId, Status, Priority)
	VALUES(@FamilyMemberId, @FirstName,@LastName, @Nationality
	, @NationalCode, @Gender, @RelationShip, @BirthDate, @InhabitancyStatus
	, @MaritalStatus, @EducationStatus, @ActivityStatus, @JobType, @InsuranceFirst
	, @InsuranceSecond, @FamilyId, @Status, @Priority)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[InsertUser](
	@UserId int, 
	@FirstName nvarchar(250), 
	@LastName nvarchar(250), 
	@NationalCode varchar(250),
	@MobileNumber varchar(250), 
	@Password nvarchar(250), 
	@RegisterDate datetime, 
	@LastLoginDate datetime,
	@Email varchar(250), 
	@Status int, 
	@Priority float, 
	@Flag bit
)
As
BEGIN
	INSERT INTO [dbo].[Users] ([UserId], [FirstName], [LastName]
	, [NationalCode], [MobileNumber], [Password], [RegisterDate]
	, [LastLoginDate], [Email], [Status], [Priority], [Flag])
	VALUES (@UserId, @FirstName, @LastName, @NationalCode
	,@MobileNumber, HashBytes('MD5', @Password), @RegisterDate, @LastLoginDate
	,@Email, @Status, @Priority, @Flag)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[UpdateUser](
	@UserId int, 
	@FirstName nvarchar(250), 
	@LastName nvarchar(250), 
	@MobileNumber varchar(250), 
	@Password varchar(250), 
	@Email varchar(250)
)
As
BEGIN
	UPDATE [dbo].[Users] SET [FirstName] = @FirstName, [LastName] = @LastName
	, [MobileNumber] = @MobileNumber
	, [Password] = CASE WHEN @Password != '' THEN HashBytes('MD5', @Password) ELSE [Password] END
	, [Email] = @Email WHERE [UserId] = @UserId
END
GO
/****** Object:  UserDefinedFunction [dbo].[CheckUniqueNationalCodeInFamilyMembers]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CheckUniqueNationalCodeInFamilyMembers](@NationalCode [varchar](50),@SupervisorId int, @FamilyId int)
RETURNS [varchar](50)
AS 
Begin
Declare	@result bit
select @result = count(NationalCode)
from FamilyMembers
where NationalCode = @NationalCode and (RelationShip != @SupervisorId OR RelationShip is NULL) and FamilyId != @FamilyId 
Return @result
END 
GO
/****** Object:  UserDefinedFunction [dbo].[CheckUniqueNationalCodeInUsers]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CheckUniqueNationalCodeInUsers](@NationalCode [varchar](50))
RETURNS [varchar](50)
AS 
Begin
Declare	@result bit
select @result = count(NationalCode)
from Users
where NationalCode = @NationalCode
Return @result
END 
GO
/****** Object:  UserDefinedFunction [dbo].[ExistUser]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ExistUser](@NationalCode [varchar](50),@Password [NVarchar](50), @UserId int)
RETURNS int
AS 
Begin
Declare	@result int
select @result = count(NationalCode)
from Users
where NationalCode = @NationalCode and [Password] = HashBytes('MD5', @Password) AND UserId != @UserId
Return @result
END 
GO
/****** Object:  UserDefinedFunction [dbo].[IsValid]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[IsValid](@date [varchar](10))
RETURNS [bit] WITH EXECUTE AS CALLER
AS 
Begin

	if	(
			(@date like '[0-9][0-9][0-9][0-9]/0[0-6]/[0-3][0-9]' and substring(@date,9,2)<='31') Or
			(@date like '[0-9][0-9][0-9][0-9]/0[789]/[0-3][0-9]' and substring(@date,9,2)<='30') Or
			(@date like '[0-9][0-9][0-9][0-9]/1[01]/[0-3][0-9]' and substring(@date,9,2)<='30') Or
			(@date like '[0-9][0-9][0-9][0-9]/12/[0-3][0-9]' and substring(@date,9,2)<='29') Or
			(@date like '[0-9][0-9][0-9][0-9]/12/30' and cast(substring(@date,1,4) as int) % 4 = 3)
		)
		return 1

	return 0
End
GO
/****** Object:  UserDefinedFunction [dbo].[MD2SHD]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[MD2SHD](@SHD [varchar](10))
RETURNS [varchar](10) WITH EXECUTE AS CALLER
AS 
Begin

Declare	@Year 		int,
	@Mon 		int,
	@Day 		int,
	@MiladiDay 	int,
	@M_SH_DayDiff 	int,
	@Div400 	int,
	@Div100 	int,
	@Div4 		int,
	@KabCount 	int,
	@NormalCount 	int,
	@YearDayCount 	int,
	@MonDayCount 	int,
	@IsLeap 	int,
	@Value 		int,
	@Div33 		int,
	@Mod33 		int,
	@Mod4 		int,
	@Div1 		int,
	@Mod1 		int,
	@FinallYear	varchar(4),
	@FinallMon	varchar(2),
	@FinallDay	varchar(2)


Select	@M_SH_DayDiff 	= 226894, -- Days difference between SHamsi and Milladi Calender 
	@SHD		= Replace(@SHD , '-' , '/'),
	@Year		= Cast( SubString(@SHD , 1 , CharIndex ('/' ,@SHD)-1) As Int),
	@SHD		= SubString(@SHD , CharIndex ('/' , @SHD) + 1, Len(@SHD) ),
	@Mon		= Cast(SubString(@SHD , 1 , CharIndex ('/' , @SHD) - 1) As Int),
	@SHD		= SubString(@SHD, CharIndex ( '/' , @SHD) + 1, Len(@SHD)),
	@Day 		= Cast(@SHD As Int),
	@Div400		= (@Year - 1) /400,
	@Div100		= (@Year - 1) /100,
	@Div4		= (@Year - 1) / 4,
	@KabCount	= @Div4 - @Div100 + @Div400,
	@NormalCount	= @Year - @KabCount - 1,
	@YearDayCount	= @KabCount * 366 + @NormalCount * 365,
	@IsLeap		= Case
				When (@Year % 4 = 0) And ((@Year % 100 <> 0) Or (@Year % 400 = 0))	Then	1
				Else									0
			  End,
	@MonDayCount 	= Case 
				When @Mon = 1 	Then 0
				When @Mon = 2 	Then 31
				When @Mon = 3 	Then 28 + 31
				When @Mon = 4 	Then 28 + 2 * 31
				When @Mon = 5 	Then 28 + 2 * 31 + 30
				When @Mon = 6 	Then 28 + 3 * 31 + 30
				When @Mon = 7 	Then 28 + 3 * 31 + 2 * 30
				When @Mon = 8 	Then 28 + 4 * 31 + 2 * 30
				When @Mon = 9 	Then 28 + 5 * 31 + 2 * 30
				When @Mon = 10 	Then 28 + 5 * 31 + 3 * 30
				When @Mon = 11 	Then 28 + 6 * 31 + 3 * 30
				When @Mon = 12 	Then 28 + 6 * 31 + 4 * 30
			  End,
	@MonDayCount	= Case
				When @IsLeap = 1 And @Mon > 2	Then 	@MonDayCount + 1
				Else					@MonDayCount
			  End,
	@MiladiDay	= @YearDayCount + @MonDayCount + @Day,
	@Value 		= @MiladiDay - @M_SH_DayDiff,
	@Div33		= (@Value - 1) / (8 * 366 + 25 * 365),
	@Mod33		= (@Value - 1) % (8 * 366 + 25 * 365)

If @Mod33 < 5 * 1461 + 365

	Select	@Div4	= @Mod33 / 1461,
		@Mod4	= @Mod33 % 1461,
		@Div1	= 0
Else
	Select 	@Div4	= (@Mod33 - 5 * 1461 - 365) / 1461  + 5,
		@Mod4	= (@Mod33 - 5 * 1461 - 365) % 1461,
		@Div1	= 1

If @Mod4 >= 366
	Select 	@Div1	= @Div1 +(@Mod4 - 366)/365 + 1,
		@Mod1	= (@Mod4 - 366) % 365 
Else
	Select 	@Mod1	= @Mod4
	
If @Mod1<(6*31) 
	Select	@Mon	= @Mod1 / 31 + 1,
		@Day	= (@Mod1 % 31) + 1
Else
	Select 	@Mon	= (@Mod1 -6 * 31) / 30 + 6 + 1,
		@Day	= (@Mod1 -6 * 31) % 30 + 1

Select	@Year		= @Div33 * 33 + @Div4 * 4 + @Div1 + 1,
	@FinallYear	= Cast(@Year as varchar(4)),
	@FinallMon 	= Replicate('0' , 2 - Len(@Mon)) + Cast(@Mon As varchar),
	@FinallDay 	= Replicate('0' , 2 - Len(@Day)) + Cast(@Day As varchar)

Return  (@FinallYear +'/'+ @FinallMon +'/'+ @FinallDay)

end

GO
/****** Object:  UserDefinedFunction [dbo].[SHD2MD]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[SHD2MD](@SHD [varchar](10))
RETURNS [varchar](10) WITH EXECUTE AS CALLER
AS 
Begin

If 	[dbo].[IsValid](@SHD) = 0
	Return Null

Declare	@Year 		Int,
	@Mon 		Int,
	@Day 		Int,
	@ShamsiDay 	Int,
	@M_SH_DayDiff	Int,
	@Div33 		Int,
	@Mod33 		Int,
	@KabCount 	Int,
	@NormalCount 	Int,
	@YearDayCount 	Int,
	@MonDayCount 	Int,
	@Div400 	Int,
	@Mod400 	Int,
	@Div100 	Int,
	@Mod100 	Int,
	@Div4 		Int,
	@Mod4 		Int,
	@Div1 		Int,
	@Mod1 		Int,
	@Value		Int,
	@Mon_Day 	Int,
	@FebDayCount 	Int

Select	@M_SH_DayDiff	= 226894, -- Days difference between SHamsi and Milladi Calender 
	@SHD		= Replace(@SHD , '-' , '/'),
	@Year		= Cast( SubString( @SHD,1,CharIndex ('/' , @SHD) - 1) As Int),
	@SHD		= SubString( @SHD , CharIndex ('/' , @SHD) + 1 , Len(@SHD)),
	@Mon		= Cast( SubString( @SHD,1,CharIndex ('/' ,@SHD) - 1) As Int),
	@SHD		= SubString( @SHD , CharIndex ('/' ,@SHD) + 1,Len(@SHD)),
	@Day		= Cast(@SHD As Int),
	@Div33		= (@Year - 1) / 33,
	@Mod33		= (@Year - 1) % 33,
	@KabCount	= Case
				When @Mod33<21	Then	(@Mod33 + 3) / 4
				Else			((@Mod33 + 3 - 21) / 4) + 5
			  End,
	@NormalCount	= @Mod33 - @KabCount,
	@YearDayCount	= @Div33 * (33 * 365 + 8) + @KabCount * 366 + @NormalCount * 365,
	@MonDayCount	= Case
				When @Mon<7 Then	(@Mon-1)*31
				Else 			6 * 31 + (@Mon - 7) * 30
			  End,
	@ShamsiDay	= @YearDayCount + @MonDayCount + @Day,
	@Value 		= @ShamsiDay + @M_SH_DayDiff,
	@Div400		= (@Value - 1) / (4 * (25 * 1461 - 1) + 1),
	@Mod400		= (@Value - 1) % (4 * (25 * 1461 - 1) + 1),
	@Div100		= @Mod400 / (25 * 1461 - 1),
	@Mod100		= @Mod400 % (25 * 1461 - 1),
	@Div4		= @Mod100 / 1461,
	@Mod4		= @Mod100 % 1461,
	@Div1		= Case
				When @Mod4 < 1095 Then	@Mod4/365
				Else 			3
			  End,
	@Mod1		= Case
				When @Mod4 < 1095 Then 	@Mod4 % 365 + 1
				Else 			@Mod4-1095+1
			  End,
	@Year		= @Div400 * 400 + @Div100 * 100 + @Div4 * 4 + @Div1 + 1,
	@FebDayCount	= Case
				When (@Year%4=0)and((@Year%100<>0)or(@Year%400=0))	Then	29
				Else								28
			  End,
	@Mon_Day	= Case 
		                  When @Mod1 <= 31 				Then 1 * 100 + @Mod1
		                  When @Mod1 <= 31 + @FebDayCount 		Then 2 * 100 + @Mod1 - 31
		                  When @Mod1 <= 2 * 31 + @FebDayCount 		Then 3 * 100 + @Mod1 - 31 - @FebDayCount
		                  When @Mod1 <= 2 * 31 + @FebDayCount + 30 	Then 4 * 100 + @Mod1 - 2 * 31 - @FebDayCount
		                  When @Mod1 <= 3 * 31 + @FebDayCount + 30 	Then 5 * 100 + @Mod1 - 2 * 31 - @FebDayCount - 30
		                  When @Mod1 <= 3 * 31 + @FebDayCount + 2 * 30 	Then 6 * 100 + @Mod1 - 3 * 31 - @FebDayCount - 30
		                  When @Mod1 <= 4 * 31 + @FebDayCount + 2 * 30 	Then 7 * 100 + @Mod1 - 3 * 31 - @FebDayCount - 2 * 30
		                  When @Mod1 <= 5 * 31 + @FebDayCount + 2 * 30 	Then 8 * 100 + @Mod1 - 4 * 31 - @FebDayCount - 2 * 30
		                  When @Mod1 <= 5 * 31 + @FebDayCount + 3 * 30 	Then 9 * 100 + @Mod1 - 5 * 31 - @FebDayCount - 2 * 30
		                  When @Mod1 <= 6 * 31 + @FebDayCount + 3 * 30 	Then 10 * 100 + @Mod1 - 5 * 31 - @FebDayCount - 3 * 30
		                  When @Mod1 <= 6 * 31 + @FebDayCount + 4 * 30 	Then 11 * 100 + @Mod1 - 6 * 31 - @FebDayCount - 3 * 30
		                  When @Mod1 <= 7 * 31 + @FebDayCount + 4 * 30 	Then 12 * 100 + @Mod1 - 6 * 31 - @FebDayCount - 4 * 30
	                    End,
	@Mon		= @Mon_Day / 100,
	@Day		= @Mon_Day % 100

Return 	(
	Cast(@Year As Varchar(4)) + '/' +
	Replicate('0' , 2 - Len(@Mon)) + Cast(@Mon As  Varchar(2)) + '/' +
	Replicate('0' , 2 - Len(@Day)) + Cast(@Day As  Varchar(2))
	) 



End


GO
/****** Object:  Table [dbo].[BaseInfo]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseInfo](
	[BaseInfoId] [int] NOT NULL,
	[Value] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[ParentId] [int] NULL,
	[Priority] [float] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_SHB_BASE_INFO] PRIMARY KEY CLUSTERED 
(
	[BaseInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Family]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Family](
	[FamilyId] [int] NOT NULL,
	[BlockNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[FamilyNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[BuildingNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[FamilyType] [int] NOT NULL,
	[PopulationType] [int] NULL,
	[PlaceId] [int] NULL,
	[RegionStatus] [int] NULL,
	[OwnerShipStatus] [int] NOT NULL,
	[PostalCode] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[MobileNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[PhoneNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[Address] [nvarchar](max) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[Status] [int] NOT NULL,
	[Priority] [float] NOT NULL,
	[UserId] [int] NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SHB_FAMILY] PRIMARY KEY CLUSTERED 
(
	[FamilyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FamilyMembers]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FamilyMembers](
	[FamilyMemberId] [int] NOT NULL,
	[FirstName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[LastName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[Nationality] [int] NOT NULL,
	[NationalCode] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[Gender] [int] NOT NULL,
	[RelationShip] [int] NULL,
	[BirthDate] [datetime] NOT NULL,
	[InhabitancyStatus] [int] NOT NULL,
	[MaritalStatus] [int] NOT NULL,
	[EducationStatus] [int] NULL,
	[ActivityStatus] [int] NULL,
	[JobType] [int] NULL,
	[InsuranceFirst] [int] NOT NULL,
	[InsuranceSecond] [int] NOT NULL,
	[FamilyId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Priority] [float] NOT NULL,
 CONSTRAINT [PK_SHB_FAMILY_MEMBER] PRIMARY KEY CLUSTERED 
(
	[FamilyMemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Places]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[PlaceId] [int] NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[ParentId] [int] NULL,
	[Status] [int] NOT NULL,
	[Priority] [int] NOT NULL,
 CONSTRAINT [PK_SHB_CITY] PRIMARY KEY CLUSTERED 
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/15/2015 10:14:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[FirstName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[LastName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[NationalCode] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[MobileNumber] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[Password] [varchar](50) COLLATE SQL_Latin1_General_CP1256_CI_AS NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[Email] [varchar](250) COLLATE SQL_Latin1_General_CP1256_CI_AS NULL,
	[Status] [int] NOT NULL,
	[Priority] [float] NOT NULL,
	[Flag] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BaseInfo]  WITH CHECK ADD  CONSTRAINT [FK_BaseInfo_BaseInfo] FOREIGN KEY([ParentId])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[BaseInfo] CHECK CONSTRAINT [FK_BaseInfo_BaseInfo]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_FamilyType_BaseInfo] FOREIGN KEY([FamilyType])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_FamilyType_BaseInfo]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_OwnerShipStatus_BaseInfo] FOREIGN KEY([OwnerShipStatus])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_OwnerShipStatus_BaseInfo]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_Places] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Places] ([PlaceId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_Places]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_PopulationType_BaseInfo] FOREIGN KEY([PopulationType])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_PopulationType_BaseInfo]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_RegionStatus_BaseInfo] FOREIGN KEY([RegionStatus])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_RegionStatus_BaseInfo]
GO
ALTER TABLE [dbo].[Family]  WITH CHECK ADD  CONSTRAINT [FK_Family_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Family] CHECK CONSTRAINT [FK_Family_Users]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_ActivityStatus_BaseInfo] FOREIGN KEY([ActivityStatus])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_ActivityStatus_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_Family] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Family] ([FamilyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_Family]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_Gender_BaseInfo] FOREIGN KEY([Gender])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_Gender_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_InhabitancyStatus_BaseInfo] FOREIGN KEY([InhabitancyStatus])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_InhabitancyStatus_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_InsuranceFirst_BaseInfo] FOREIGN KEY([InsuranceFirst])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_InsuranceFirst_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_InsuranceSecond_BaseInfo] FOREIGN KEY([InsuranceSecond])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_InsuranceSecond_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_JobType_BaseInfo] FOREIGN KEY([JobType])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_JobType_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_MaritalStatus_BaseInfo] FOREIGN KEY([MaritalStatus])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_MaritalStatus_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_Nationality_BaseInfo] FOREIGN KEY([Nationality])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_Nationality_BaseInfo]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_RelationShip_BaseInfo] FOREIGN KEY([RelationShip])
REFERENCES [dbo].[BaseInfo] ([BaseInfoId])
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_RelationShip_BaseInfo]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD  CONSTRAINT [FK_Places_Places] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Places] ([PlaceId])
GO
ALTER TABLE [dbo].[Places] CHECK CONSTRAINT [FK_Places_Places]
GO
USE [master]
GO
ALTER DATABASE [Shahab] SET  READ_WRITE 
GO
