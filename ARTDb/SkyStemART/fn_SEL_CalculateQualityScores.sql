



  
  
ALTER FUNCTION [QualityScore].[fn_SEL_CalculateQualityScores]  
(  
	@RecPeriodID INT,
	@udtGLDataIDTable udt_BigIntIDTableType READONLY,
	@ForceCalculation BIT = 0
)
RETURNS @tblGLDataQualityScore TABLE
(	
	GLDataID BIGINT
	,IsSRA BIT NULL
	,QualityScoreID INT
	,CompanyQualityScoreID INT
	,IsApplicableForSRA BIT NULL
	,SystemQualityScoreStatusID SMALLINT NULL
	,UserQualityScoreStatusID SMALLINT NULL
)
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Vinay Khandelwal
	DATE CREATED:	11/15/2011
	PURPOSE:		If period is closed then return saved data otherwise 
					calculate and return Quality Scores for passed GLDataID's.
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	12/09/2011		Vinay		Quality Score Storage Framework changes
	12/09/2011		Vinay		Some rules needs to be ignored with SRA
	12/23/2011		Vinay		Column Renamed
	12/23/2011		Vinay		Parameter added to Force the Calculation
	01/09/2102		Manish		Adding WITH(NOLOCK)
-------------------------------------------------------------------------------
	SAMPLE CALL
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------*/ 
BEGIN
/*
	Step 1. Get the Enabled Quality Scores
	Step 2. Check the Passed Rec Period Status if it is closed the get the saved data else follow the below steps
		Step a. Prepare temp table with ALL GL and Enabled Scores
		Step b. Process the Quality Score Rules
		Step c. Get and Update User Quality Score Status
	Step 3. Return Quality Scores
*/

DECLARE @tblCompanyQualityScore TABLE
(
	QualityScoreID INT NULL
	,CompanyQualityScoreID INT
	,IsApplicableForSRA BIT
)

DECLARE @UnexpVarThresholdRCCY DECIMAL(18,4)
DECLARE @CompanyID INT
DECLARE @PeriodEndDate SMALLDATETIME
DECLARE @AgingDays SMALLINT
DECLARE @RecPeriodStatusID SMALLINT

SELECT 
	@CompanyID = CompanyID
	,@PeriodEndDate = PeriodEndDate
	,@RecPeriodStatusID = ReconciliationPeriodStatusID
FROM
	ReconciliationPeriod RP  WITH(NOLOCK)
WHERE
	RP.ReconciliationPeriodID = @RecPeriodID
	
SET @UnexpVarThresholdRCCY = [dbo].[fn_GET_UnexplainedVarianceThresholdByRecPeriodID](@RecPeriodID, @CompanyID)
SET @AgingDays = QualityScore.fn_GET_QualityScoreAgingDays()	
	
-- Step 1 BEGIN.
BEGIN
	-- Get Quality Scores Enabled for this Rec Period
	INSERT INTO 
		@tblCompanyQualityScore
		SELECT
			QualityScoreID
			,CompanyQualityScoreID
			,IsApplicableForSRA
		FROM
			fn_SEL_CompanyQualityScore(@RecPeriodID, 1, 1)
END
-- Step 1 END

-- Step 2 BEGIN
IF (@RecPeriodStatusID = 4 AND IsNull(@ForceCalculation,0) = 0)
BEGIN
	INSERT INTO 
		@tblGLDataQualityScore
		SELECT
			GQS.GLDataID
			,AGL.IsSystemReconcilied
			,CQS.QualityScoreID
			,CQS.CompanyQualityScoreID  -- OLD CODE GQS.CompanyQualityScoreID
			,CQS.IsApplicableForSRA
			,GQS.SystemQualityScoreStatusID
			,GQS.UserQualityScoreStatusID
		FROM
			@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID			
			INNER JOIN
				QualityScore.GLDataQualityScore GQS
					ON AGL.GLDataID = GQS.GLDataID AND GQS.IsActive = 1
			INNER JOIN
				@tblCompanyQualityScore CQS
					ON GQS.CompanyQualityScoreID = CQS.CompanyQualityScoreID -- OLD CODE GQS.CompanyQualityScoreID = CQS.CompanyQualityScoreID
END
ELSE
BEGIN
	-- Step 2.a BEGIN
	--BEGIN
	--	-- Prepare the result set for all the GL Records
	--	INSERT INTO
	--		@tblGLDataQualityScore
	--		SELECT
	--			udtGL.ID
	--			,IsSystemReconcilied
	--			,QualityScoreID
	--			,CompanyQualityScoreID
	--			,IsApplicableForSRA
	--			,NULL
	--			,NULL
	--		FROM
	--			@udtGLDataIDTable udtGL
	--			INNER JOIN 
	--				vw_Select_ActiveGLData AGL
	--					ON udtGL.ID = AGL.GLDataID
	--			CROSS JOIN
	--				@tblCompanyQualityScore CQS

	--END
	-- Step 2.a END

	-- Step 2.b BEGIN
	BEGIN
		-- Process the rules
		--#1 BEGIN	Supporting Detail matches the Reconciled balance to unexplained variance threshold.
			-- Get the Company Quality Score ID
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleSupportingDetailBalance(@udtGLDataIDTable, @UnexpVarThresholdRCCY) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID --AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)
					
			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 1
		END		
		--# 1 END
				
		--#2 BEGIN	Supporting Documentation has been attached.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleSupportingDocumentation(@udtGLDataIDTable) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)
					
			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 2
		END
		--# 2 END

		--#3	GL adjustments have not been open for over 30 days.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleGLAdjustmentsAging(@udtGLDataIDTable, @AgingDays) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 3
		END
		--# 3 END

		--#4	Write offs/ons have not been open for over 30 days.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleWriteOffAging(@udtGLDataIDTable, @AgingDays) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)
					
			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 4
		END
		--# 4 END

		--#5	Timing Differences have not been open for over 30 days.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleTimingDiffAging(@udtGLDataIDTable, @AgingDays) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 5
		END
		--# 5 END

		--#6	Reconciliation was prepared by due date.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRulePreparerDueDate(@udtGLDataIDTable, @RecPeriodID) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID --AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 6
		END
		--# 6 END

		--#7	Reconciliation was reviewed by due date.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleReviewerDueDate(@udtGLDataIDTable, @RecPeriodID) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID --AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 7
		END
		--# 7 END

		--#8	Reconciliation was approved by due date.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleApproverDueDate(@udtGLDataIDTable, @RecPeriodID) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 8
		END
		--# 8 END

		--#9	Risk Rating or Reconciliation Frequency has not changed during the last four periods.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleRiskRating(@udtGLDataIDTable, @RecPeriodID) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID -- AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 9
		END
		--#9 END

		--#10	There are no Write offs/ons entered for this reconciliation.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreNoWriteOff(@udtGLDataIDTable) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID --AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 10
		END
		--#10 END		

		--#11	There are no capability changes for this reconciliation.
		BEGIN
			--UPDATE GQS
			--	SET SystemQualityScoreStatusID = GLQSR.SystemQualityScoreStatusID
			--FROM 
			--	@tblGLDataQualityScore GQS
		INSERT INTO
			@tblGLDataQualityScore
			SELECT
				udtGL.ID
				,IsSystemReconcilied
				,CQS.QualityScoreID
				,CQS.CompanyQualityScoreID
				,IsApplicableForSRA
				,GLQSR.SystemQualityScoreStatusID
				,GQS.UserQualityScoreStatusID
			FROM
				@udtGLDataIDTable udtGL
			INNER JOIN 
				vw_Select_ActiveGLData AGL
					ON udtGL.ID = AGL.GLDataID
			INNER JOIN
				QualityScore.fn_SEL_CHK_GLDataQualityScoreRuleNoCapabilityChanges(@udtGLDataIDTable, @CompanyID) GLQSR
				ON AGL.GLDataID = GLQSR.GLDataID --AND GQS.QualityScoreID = GLQSR.QualityScoreID
			INNER JOIN
				@tblCompanyQualityScore CQS
				ON CQS.QualityScoreID = GLQSR.QualityScoreID
			LEFT JOIN
				QualityScore.GLDataQualityScore GQS
				ON AGL.GLDataID = GQS.GLDataID 
					AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID 
					AND GQS.IsActive = 1
			WHERE
				(IsNull(AGL.IsSystemReconcilied,0) = 0 OR IsNull(IsApplicableForSRA,0) = 1)

			--SELECT * FROM @tblGLDataQualityScore WHERE QualityScoreID = 11
		END
		--#11 END	
	END
	-- Step 2.b END

	-- Step 2.c BEGIN
	--BEGIN
	--	UPDATE TGL
	--		SET TGL.UserQualityScoreStatusID = GQS.UserQualityScoreStatusID
	--	FROM
	--		@tblGLDataQualityScore TGL
	--		LEFT JOIN
	--			QualityScore.GLDataQualityScore GQS
	--			ON TGL.GLDataID = GQS.GLDataID 
	--				AND TGL.CompanyQualityScoreID = GQS.CompanyQualityScoreID -- OLD CODE AND TGL.CompanyQualityScoreID = GQS.CompanyQualityScoreID
	--				AND GQS.IsActive = 1
	--END
	-- Step 2.c END
END
-- Step 3 BEGIN
RETURN
-- Step 3 END
END

