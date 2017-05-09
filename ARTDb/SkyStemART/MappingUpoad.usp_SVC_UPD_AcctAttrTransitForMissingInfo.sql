
ALTER PROCEDURE [MappingUpload].[usp_SVC_UPD_AcctAttrTransitForMissingInfo]  
 (
	@companyID int,
	@dataImportID int,
	@RecPeriodID INT,
	@RecPeriodendDate SmallDateTime,
	@LanguageID INT,
	@DefaultLanguageID INT,
	@IsAccountUpload BIT
) 
 
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Harsh Kuntail
	DATE CREATED:	11/19/2011
	PURPOSE:		UPDATE [dbo].[AccountAttributeTransit] on the basis of Mapped Account keys for missing info
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
	Manoj			11/24/2011			make changes aboy account upload
	Harsh			07/05/2012			Field width changes
	Vinay			09/02/2012			Optimization Changes
	Vinay			12/10/2012			Production Bug Fixed
	28/05/2014		Manoj Kumar			Increase Error Message length
-------------------------------------------------------------------------------
sample call
declare @addedby varchar(128)
declare @PeriodEndDate smalldatetime = (Select PeriodEndDate From ReconciliationPeriod Where ReconciliationPeriodID = 1384)
declare @ReturnValue nvarchar(max)
exec [MappingUpload].[usp_SVC_UPD_AcctAttrTransitForMissingInfo]  292, 1713, 1384, @PeriodEndDate, 1033, 1033 
-------------------------------------------------------------------------------
*/
BEGIN 
	
	DECLARE @Key2Selected  BIT = 0
	DECLARE @Key3Selected  BIT = 0
	DECLARE @Key4Selected  BIT = 0
	DECLARE @Key5Selected  BIT = 0
	DECLARE @Key6Selected  BIT = 0
	DECLARE @Key7Selected  BIT = 0
	DECLARE @Key8Selected  BIT = 0
	DECLARE @Key9Selected  BIT = 0
	DECLARE @FsCaptionSelected  BIT = 0
	DECLARE @AccountNumberSelected  BIT = 0
	DECLARE @AccountTypeSelected  BIT = 0
	DECLARE @ErrorMessageWith2param NVARCHAR(3000)
	Declare @ErrorMessageInvalidAccount NVARCHAR(3000)
	DECLARE @DefaultErrorMessage NVARCHAR(MAX)  
	DECLARE @Seperator CHAR(1) = '_'
	
	SET @DEFAULTErrorMessage = ''
	 
	SET @ErrorMessageInvalidAccount = dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000105,@LanguageID,0)--Invalid Account
	--5000193  Error in Row# {0}. {1} 
	SET @ErrorMessageWith2param = REPLACE(dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000193,@LanguageID, 0),'{1}',@ErrorMessageInvalidAccount)
	

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpPhraseForAcctAttrMIssingInfo'))
	BEGIN
		DROP TABLE #TmpPhraseForAcctAttrMIssingInfo
	END	
	CREATE TABLE #TmpPhraseForAcctAttrMIssingInfo (LabelID INT, Phrase NVARCHAR(2000))

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpAccountKeyInDB'))
	BEGIN
		DROP TABLE #tmpAccountKeyInDB
	END	
	CREATE TABLE #tmpAccountKeyInDB  
	(
		AccountID INT
		, AccountName NVARCHAR(2000)
		, FSCaptionID INT
		, FSCaptionLabelID INT
		, AccountTypeID SMALLINT
		, AccountTypeLabelID INT
		, GeographyObjectID INT
		, Key2LabelID INT
		, Key3LabelID INT
		, Key4LabelID INT
		, Key5LabelID INT
		, Key6LabelID INT
		, Key7LabelID INT
		, Key8LabelID INT
		, Key9LabelID INT
		, AccountNumber VARCHAR(20)
		, FSCaption NVARCHAR(2000)
		, AccountType NVARCHAR(2000)
		, Key2 NVARCHAR(2000)
		, Key3 NVARCHAR(2000)
		, Key4 NVARCHAR(2000)
		, Key5 NVARCHAR(2000)
		, Key6 NVARCHAR(2000)
		, Key7 NVARCHAR(2000)
		, Key8 NVARCHAR(2000)
		, Key9 NVARCHAR(2000)
		, SubSETKey NVARCHAR(MAX)
	)
		
	SELECT 
		@RecPeriodID = D.ReconciliationPeriodID
		, @RecPeriodEndDate =  RP.PeriodENDDate
		, @LanguageID = D.LanguageID  
	FROM 
		DataImportHdr D 
		INNER JOIN ReconciliationPeriod RP
			ON D.ReconciliationPeriodID = RP.ReconciliationPeriodID 
	WHERE 
		D.DataImportID = @dataImportID
	
	--Determine configured subSET keys for mapping 
	SELECT 
		@FsCaptionSelected = Case When F.AccountMappingKeyID = 2 Then 1 else @FsCaptionSelected END
		, @AccountTypeSelected =Case When F.AccountMappingKeyID = 3 Then 1 else @AccountTypeSelected END
		, @Key2Selected = case When F.AccountMappingKeyID  = 4 then 1 else @Key2Selected END 
		, @Key3Selected = case When F.AccountMappingKeyID  = 5 then 1 else @Key3Selected END 
		, @Key4Selected = case When F.AccountMappingKeyID  = 6 then 1 else @Key4Selected END 
		, @Key5Selected = case When F.AccountMappingKeyID  = 7 then 1 else @Key5Selected END
		, @Key6Selected = case When F.AccountMappingKeyID  = 8 then 1 else @Key6Selected END 
		, @Key7Selected = case When F.AccountMappingKeyID  = 9 then 1 else @Key7Selected END 
		, @Key8Selected = case When F.AccountMappingKeyID  = 10 then 1 else @Key8Selected END 
		, @Key9Selected = case When F.AccountMappingKeyID  = 11 then 1 else @Key9Selected END
		, @AccountNumberSelected = case When F.AccountMappingKeyID = 12 then 1 else @AccountNumberSelected END
	FROM 
		MappingUpload.fn_SEL_CompanyAccountMappingKey (@RecPeriodENDDate, @companyID, 1) F

	-- Get All Phrases, LabelIDs for This company and as per this language
	INSERT INTO #TmpPhraseForAcctAttrMIssingInfo (LabelID , Phrase )
	SELECT 
		P.LabelID 
		, RTRIM(LTRIM(CAST(P.Phrase AS NVARCHAR(2000))))
	FROM
		dbo.fn_SEL_BusinessEntityPhrases(@companyID, @LanguageID, @DefaultLanguageID) P
	
	INSERT INTO #TmpPhraseForAcctAttrMIssingInfo (LabelID , Phrase )	
	Select 
		P.LabelID 
		, RTRIM(LTRIM(CAST(P.Phrase AS NVARCHAR(2000)))) 
	From
		dbo.fn_SEL_SystemPhrases(0, @LanguageID, @DefaultLanguageID) P --Default Business entity
		INNER JOIN AccountTypeMst AT
			ON P.LabelID = AT.AccountTypeLabelID 
			
	--Get All Data For Account. We cannot use vw_AccountKeyInfo as it does give information about P&N Accounts
	INSERT INTO #tmpAccountKeyInDB 
	(
		AccountID
		, AccountName 
		, FSCaptionID
		, FSCaptionLabelID 
		, AccountTypeID
		, AccountTypeLabelID
		, GeographyObjectID
		, Key2LabelID
		, Key3LabelID
		, Key4LabelID
		, Key5LabelID
		, Key6LabelID
		, Key7LabelID
		, Key8LabelID
		, Key9LabelID
		, AccountNumber
		, FSCaption
		, AccountType
		, Key2
		, Key3
		, Key4
		, Key5
		, Key6
		, Key7
		, Key8
		, Key9
	)
	SELECT 
		A.AccountID
		, PAN.Phrase  
		, F.FSCaptionID 
		, F.FSCaptionLabelID 
		, AT.AccountTypeID 
		, AT.AccountTypeLabelID 
		, GOH.GeographyObjectID 
		, GOH.Key2LabelID 
		, GOH.Key3LabelID 
		, GOH.Key4LabelID 
		, GOH.Key5LabelID 
		, GOH.Key6LabelID 
		, GOH.Key7LabelID 
		, GOH.Key8LabelID 
		, GOH.Key9LabelID
		, A.AccountNumber
		, PF.Phrase 
		, PA.Phrase 
		, P2.Phrase 
		, P3.Phrase
		, P4.Phrase
		, P5.Phrase
		, P6.Phrase
		, P7.Phrase
		, P8.Phrase
		, P9.Phrase  
	FROM
		vw_Select_ActiveAccounts A
		INNER JOIN GeographyObjectHdr GOH
			ON A.GeographyObjectID = GOH.GeographyObjectID 
		LEFT OUTER JOIN FSCaptionHdr F
			ON A.FSCaptionID = F.FSCaptionID AND F.IsActive = 1
		LEFT OUTER JOIN AccountTypeMst AT
			ON A.AccountTypeID = AT.AccountTypeID AND AT.IsActive = 1
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo PF 
			ON F.FSCaptionLabelID = PF.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo PA 
			ON AT.AccountTypeLabelID = PA.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P2 
			ON GOH.Key2LabelID = P2.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P3 
			ON GOH.Key3LabelID = P3.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P4 
			ON GOH.Key4LabelID = P4.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P5 
			ON GOH.Key5LabelID = P5.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P6 
			ON GOH.Key6LabelID = P6.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P7 
			ON GOH.Key7LabelID = P7.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P8 
			ON GOH.Key8LabelID = P8.LabelID
		LEFT OUTER JOIN #TmpPhraseForAcctAttrMIssingInfo P9 
			ON GOH.Key9LabelID = P9.LabelID
		INNER JOIN #TmpPhraseForAcctAttrMIssingInfo PAN
			ON A.AccountNameLabelID = PAN.LabelID		
	WHERE
		GOH.CompanyID = @companyID
		AND GOH.IsActive =1 
		
		
	IF @FsCaptionSelected  = 1
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey = RTRIM(LTRIM(AAT.FSCaption))
		FROM
			AccountAttributeTransit AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		Update 
			A
		Set
			A.SubSetKey = RTRIM(LTRIM(A.FSCaption)) 
		From	
			#tmpAccountKeyInDB A
 			 
	END

	IF (@AccountTypeSelected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') +RTRIM(LTRIM(AAT.AccountType)) 
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 

		Update 
			A
		Set
			A.SubSetKey = ISNULL(A.SubSetKey+@Seperator,'')+ RTRIM(LTRIM(A.AccountType))   
		From	
			#tmpAccountKeyInDB A 	 
	END

	IF (@Key2Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') +RTRIM(LTRIM(AAT.Key2))  
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key2)) 
		FROM	
			#tmpAccountKeyInDB A	

	END	

	IF (@Key3Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key3))  
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key3))  
		FROM	
			#tmpAccountKeyInDB A
 	
	END

	IF @Key4Selected = 1
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key4)) 
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key4))  
		FROM	
			#tmpAccountKeyInDB A 
	END

	IF @Key5Selected = 1
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key5))   
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
		
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key5))  
		FROM	
			#tmpAccountKeyInDB A 	
	END

	IF (@Key6Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key6))   
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key6))  
		FROM	
			#tmpAccountKeyInDB A 
	END

	IF (@Key7Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key7))
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key7))  
		FROM	
			#tmpAccountKeyInDB A 	
	END

	IF (@Key8Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key8))   
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key8))  
		FROM	
			#tmpAccountKeyInDB A	
		 
	END

	IF (@Key9Selected = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(AAT.Key9))  
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID = @dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(A.Key9))  
		FROM	
			#tmpAccountKeyInDB A	
	 END
 
	IF (@AccountNumberSelected  = 1)
	BEGIN
		UPDATE 
			AAT 
		SET
			AAT.AccountSubSETKey =  ISNULL(AAT.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(CAST(AAT.AccountNumber AS NVARCHAR(20))))  
		FROM
			[dbo].[AccountAttributeTransit] AAT
		WHERE
			AAT.DataImportID =@dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(CAST(A.AccountNumber AS VARCHAR(20)))) 
		FROM	
			#tmpAccountKeyInDB A	
	 END
	
	UPDATE 
		AAT
	SET
		AAT.AccountID = A.AccountID 
		, AAT.FSCaption = CASE WHEN AAT.FSCaption IS NULL AND A.FSCaptionID IS NOT NULL THEN A.FSCaption ELSE AAT.FSCaption END
		, AAT.AccountType = CASE WHEN AAT.AccountType IS NULL AND A.AccountTypeID IS NOT NULL THEN A.AccountType ELSE AAT.AccountType END 
		, AAT.AccountName = CASE WHEN AAT.AccountName IS NULL AND A.AccountID IS NOT NULL THEN A.AccountName ELSE AAT.AccountName END 
		, AAT.Key2 = CASE WHEN AAT.Key2 IS NULL AND A.Key2LabelID IS NOT NULL THEN A.Key2 ELSE AAT.Key2 END 
		, AAT.Key3 = CASE WHEN AAT.Key3 IS NULL AND A.Key3LabelID IS NOT NULL THEN A.Key3 ELSE AAT.Key3 END  
		, AAT.Key4 = CASE WHEN AAT.Key4 IS NULL AND A.Key4LabelID IS NOT NULL THEN A.Key4 ELSE AAT.Key4 END   
		, AAT.Key5 = CASE WHEN AAT.Key5 IS NULL AND A.Key5LabelID IS NOT NULL THEN A.Key5 ELSE AAT.Key5 END   
		, AAT.Key6 = CASE WHEN AAT.Key6 IS NULL AND A.Key6LabelID IS NOT NULL THEN A.Key6 ELSE AAT.Key6 END   
		, AAT.Key7 = CASE WHEN AAT.Key7 IS NULL AND A.Key7LabelID IS NOT NULL THEN A.Key7 ELSE AAT.Key7 END   
		, AAT.Key8 = CASE WHEN AAT.Key8 IS NULL AND A.Key8LabelID IS NOT NULL THEN A.Key8 ELSE AAT.Key8 END   
		, AAT.Key9 = CASE WHEN AAT.Key9 IS NULL AND A.Key9LabelID IS NOT NULL THEN A.Key9 ELSE AAT.Key9 END   
		, AAT.AccountNumber = A.AccountNumber
		, AAT.GeographyObjectID = A.GeographyObjectID 
		, AAT.FSCaptionID = A.FSCaptionID
		, AAT.AccountTypeID = A.AccountTypeID
	FROM
		[dbo].[AccountAttributeTransit] AAT
		INNER JOIN 	#tmpAccountKeyInDB A
			ON  AAT.AccountSubSetKey = A.SubSETKey 
	WHERE
		AAT.DataImportID = @dataImportID	
		
		
	--Update [dbo].[AccountAttributeTransit] for invalid Accounts
	UPDATE
		AAT
	SET
		AAT.IsValidRow = 0
		, AAT.ErrorMessage = REPLACE(@ErrorMessageWith2param,'{0}',CAST(AAT.ExcelRowNumber AS VARCHAR(10)))
	FROM
		[dbo].[AccountAttributeTransit] AAT
	WHERE
		AAT.AccountID IS NULL		
		

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpPhraseForAcctAttrMIssingInfo'))
	BEGIN
		DROP TABLE #TmpPhraseForAcctAttrMIssingInfo
	END	

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpAccountKeyInDB'))
	BEGIN
		DROP TABLE #tmpAccountKeyInDB
	END				
END
