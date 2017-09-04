/****** Object:  UserDefinedFunction [dbo].[fn_GET_IsWorkingDay]    Script Date: 7/12/2017 8:48:04 AM ******/
DROP FUNCTION [dbo].[fn_GET_IsWorkingDay]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GET_IsWorkingDay]    Script Date: 7/12/2017 8:48:04 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [dbo].[fn_GET_IsWorkingDay]
(
	@CompanyID INT,
	@Date smalldatetime
)
RETURNS BIT

/*------------------------------------------------------------------------------
	AUTHOR:			Manoj Kumar
	DATE CREATED:	12/23/2010
	PURPOSE:		To get if Date is on working Dat for the Company
	                Returns 1 if YES
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	5/25/2012		Vinay		Bug Fix on Demo Site
-------------------------------------------------------------------------------
	SAMPLE CALL
	SELECT dbo.[fn_GET_IsWorkingDay](147,'2010-09-02')
-------------------------------------------------------------------------------*/
BEGIN

DECLARE @IsWorkingDay BIT
DECLARE @WeekDayNumber INT
DECLARE @CompanyRecPeriodSetID INT
DECLARE @CompanyRecPeriodSetTypeID SMALLINT = 2 -- Work Week
DECLARE @MinDate DATETIME
	,@MaxDate DATETIME
	,@SetDate DATETIME

SELECT @MinDate = min (PeriodEndDate)
	,@MaxDate = max (PeriodEndDate)
FROM
	ReconciliationPeriod
WHERE CompanyID = @CompanyID AND IsActive = 1

SET @SetDate = @Date
IF (@SetDate < @MinDate) 
	SET @SetDate = @MinDate
IF (@SetDate > @MaxDate) 
	SET @SetDate = @MaxDate

SELECT @WeekDayNumber = DATEPART(weekday, @Date)-1
SELECT 
	@CompanyRecPeriodSetID = CompanyRecPeriodSetID
FROM 
	dbo.fn_GET_CompanyRecPeriodSetID(@SetDate, @CompanyID, @CompanyRecPeriodSetTypeID)

IF EXISTS 
		(SELECT 
				CWD.WeekDayID
		FROM
				CompanyWeekDay CWD
				INNER JOIN WeekDayMst WDM
					ON CWD.WeekDayID = WDM.WeekDayID 
		WHERE
				CWD.CompanyRecPeriodSetID = @CompanyRecPeriodSetID
				AND WDM.WeekDayNumber = @WeekDayNumber
				And WDM.IsActive =1
		)
BEGIN 
	-- Mean the Day is a Working Day as per the Work Week
	-- Now Check For HolidayCalendar
	
		IF EXISTS 
		(SELECT 
				HC.HolidayCalendarID 
		FROM
				HolidayCalendar HC	
				
		WHERE
				HC.CompanyID = @CompanyID
				AND convert(DATE, HC.HolidayDate, 101) = convert(DATE, @Date, 101)
				AND HC.IsActive = 1
		)
		SET @IsWorkingDay = 0;
	ELSE
		SET @IsWorkingDay =1;
		--******************---- END Check For HolidayCalendar***----------------
		
END  
ELSE  
BEGIN 
	SET @IsWorkingDay =0;
END

RETURN @IsWorkingDay

END


GO


