
ALTER PROCEDURE [MappingUpload].[usp_SVC_UPD_GLDataTransitForMissingInfoSubedgerImport]  
 (
	@companyID INT
	,@dataImportID INT
	,@RecPeriodID INT
	,@RecPeriodendDate SMALLDATETIME
	,@LanguageID INT
	,@DefaultLanguageID INT
	,@IsAccountUpload BIT
	,@DateAdded DateTime
) 
 
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Harsh Kuntail
	DATE CREATED:	11/19/2011
	PURPOSE:		UPDATE GLDataTransit on the basis of Mapped Account keys for missing info
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	11/24/2011		Manoj				make changes aboy account upload
	07/05/2012		Harsh				Field width changes
	09/02/2012		Vinay				Optimization Changes
	09/04/2012		Harsh				used fn_SEL_SystemPhrases
	12/10/2012		Vinay				Production Bug fixed
	29/05/2014		Manoj				Increase Error Message length
	04/05/2015		Vinay				Data Import Message changes
	04/15/2015		Vinay				Warning added
-------------------------------------------------------------------------------
sample call
declare @addedby varchar(128)
declare @PeriodEndDate smalldatetime = (Select PeriodEndDate From ReconciliationPeriod Where ReconciliationPeriodID = 1384)
declare @ReturnValue nvarchar(max)
exec [MappingUpload].[usp_SVC_UPD_GLDataTransitForMissingInfoSubedgerImport]  292, 1713, 1384, @PeriodEndDate, 1033, 1033 
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
	DECLARE @tblExcelRowNumber TABLE(ExcelRowNumber INT)
	DECLARE @udtDataImportMessageDetailType udt_DataImportMessageDetailType
	DECLARE @MessageSchemaXML XML
	DECLARE @tblMessageData TABLE(ExcelRowNumber INT,  AccountID BIGINT, ImportFieldID INT, OldValue NVARCHAR(2000), NewValue NVARCHAR(2000))

	DECLARE @key2ImportFieldID SMALLINT = 12
		,@key3ImportFieldID SMALLINT = 13
		,@key4ImportFieldID SMALLINT = 14
		,@key5ImportFieldID SMALLINT = 15
		,@key6ImportFieldID SMALLINT = 16
		,@key7ImportFieldID SMALLINT = 17
		,@key8ImportFieldID SMALLINT = 18
		,@key9ImportFieldID SMALLINT = 19
		,@AcctNameImportFieldID SMALLINT = 7
		,@AcctNumberImportFieldID SMALLINT = 6
		,@FSCaptionImportFieldID SMALLINT = 3
		,@AccountTypeImportFieldID SMALLINT = 4
	
	SET @DEFAULTErrorMessage = ''
	--5000193  Error in Row# {0}. {1}  
	SET @ErrorMessageInvalidAccount = dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000105,@LanguageID,0)--Invalid Account
	SET @ErrorMessageWith2param = REPLACE(dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000193,@LanguageID, 0),'{1}',@ErrorMessageInvalidAccount)
	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpPhraseForSubLedgerMIssingInfo'))
	BEGIN
		DROP TABLE #TmpPhraseForSubLedgerMIssingInfo
	END	
	CREATE TABLE #TmpPhraseForSubLedgerMIssingInfo (LabelID INT, Phrase NVARCHAR(2000))
	
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
	INSERT INTO #TmpPhraseForSubLedgerMIssingInfo (LabelID , Phrase )
	SELECT 
		P.LabelID 
		, RTRIM(LTRIM(CAST(P.Phrase AS NVARCHAR(2000)))) 
	FROM
		dbo.fn_SEL_BusinessEntityPhrases(@companyID, @LanguageID, @DefaultLanguageID) P
	
	INSERT INTO #TmpPhraseForSubLedgerMIssingInfo (LabelID , Phrase )	
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
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo PF 
			ON F.FSCaptionLabelID = PF.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo PA 
			ON AT.AccountTypeLabelID = PA.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P2 
			ON GOH.Key2LabelID = P2.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P3 
			ON GOH.Key3LabelID = P3.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P4 
			ON GOH.Key4LabelID = P4.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P5 
			ON GOH.Key5LabelID = P5.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P6 
			ON GOH.Key6LabelID = P6.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P7 
			ON GOH.Key7LabelID = P7.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P8 
			ON GOH.Key8LabelID = P8.LabelID
		LEFT OUTER JOIN #TmpPhraseForSubLedgerMIssingInfo P9 
			ON GOH.Key9LabelID = P9.LabelID
		INNER JOIN #TmpPhraseForSubLedgerMIssingInfo PAN
			ON A.AccountNameLabelID = PAN.LabelID		
	WHERE
		GOH.CompanyID = @companyID 
		AND GOH.IsActive =1
		
		
	IF @FsCaptionSelected  = 1
	BEGIN
		UPDATE 
			G 
		SET
			G.AccountSubSETKey = RTRIM(LTRIM(G.FSCaption))    
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') +RTRIM(LTRIM(G.AccountType)) 
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 

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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') +RTRIM(LTRIM(G.Key2))  
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key3))  
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key4)) 
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key5))   
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
		
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key6))   
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key7))
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key8))   
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(G.Key9))  
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID = @dataImportID 
			
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
			G 
		SET
			G.AccountSubSETKey =  ISNULL(G.AccountSubSETKey+@Seperator,'') + RTRIM(LTRIM(CAST(G.AccountNumber AS NVARCHAR(20))))  
		FROM
			GLDataTransit G
		WHERE
			G.DataImportID =@dataImportID 
			
		UPDATE 
			A
		SET
			A.SubSETKey = ISNULL(A.SubSETKey+@Seperator,'')+ RTRIM(LTRIM(CAST(A.AccountNumber AS VARCHAR(20)))) 
		FROM	
			#tmpAccountKeyInDB A	
	 END
	
	UPDATE 
		G
	SET
		G.AccountID = A.AccountID 
		, G.FSCaption = CASE WHEN G.FSCaption IS NULL AND A.FSCaptionID IS NOT NULL THEN A.FSCaption ELSE G.FSCaption END
		, G.AccountType = CASE WHEN G.AccountType IS NULL AND A.AccountTypeID IS NOT NULL THEN A.AccountType ELSE G.AccountType END 
		, G.AccountName = CASE WHEN G.AccountName IS NULL AND A.AccountID IS NOT NULL THEN A.AccountName ELSE G.AccountName END 
		, G.Key2 = CASE WHEN G.Key2 IS NULL AND A.Key2LabelID IS NOT NULL THEN A.Key2 ELSE G.Key2 END 
		, G.Key3 = CASE WHEN G.Key3 IS NULL AND A.Key3LabelID IS NOT NULL THEN A.Key3 ELSE G.Key3 END  
		, G.Key4 = CASE WHEN G.Key4 IS NULL AND A.Key4LabelID IS NOT NULL THEN A.Key4 ELSE G.Key4 END   
		, G.Key5 = CASE WHEN G.Key5 IS NULL AND A.Key5LabelID IS NOT NULL THEN A.Key5 ELSE G.Key5 END   
		, G.Key6 = CASE WHEN G.Key6 IS NULL AND A.Key6LabelID IS NOT NULL THEN A.Key6 ELSE G.Key6 END   
		, G.Key7 = CASE WHEN G.Key7 IS NULL AND A.Key7LabelID IS NOT NULL THEN A.Key7 ELSE G.Key7 END   
		, G.Key8 = CASE WHEN G.Key8 IS NULL AND A.Key8LabelID IS NOT NULL THEN A.Key8 ELSE G.Key8 END   
		, G.Key9 = CASE WHEN G.Key9 IS NULL AND A.Key9LabelID IS NOT NULL THEN A.Key9 ELSE G.Key9 END   
		, G.AccountNumber = A.AccountNumber
		, G.GeographyObjectID = A.GeographyObjectID 
		, G.FSCaptionID = A.FSCaptionID
		, G.AccountTypeID = A.AccountTypeID
		, G.AccountTypeLabelID = A.AccountTypeLabelID  
	FROM
		GLDataTransit G
		INNER JOIN 	#tmpAccountKeyInDB A
			ON  G.AccountSubSetKey = A.SubSETKey 
	WHERE
		G.DataImportID = @dataImportID	

	INSERT INTO @tblMessageData (ExcelRowNumber, AccountID, ImportFieldID, OldValue, NewValue)
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@FSCaptionImportFieldID
		,A.FSCaption
		,G.FSCaption
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  		
	WHERE
		G.DataImportID = @dataImportID				
		AND (RTRIM(LTRIM(ISNULL(G.FSCaption,''))) <> RTRIM(LTRIM(ISNULL(A.FSCaption,'')))) 
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@AccountTypeImportFieldID
		,A.AccountType
		,G.AccountType
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  		
	WHERE
		G.DataImportID = @dataImportID				
		AND (RTRIM(LTRIM(ISNULL(G.AccountType,''))) <> RTRIM(LTRIM(ISNULL(A.AccountType,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key2ImportFieldID
		,A.Key2
		,G.Key2
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key2,''))) <> RTRIM(LTRIM(ISNULL(A.Key2,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key3ImportFieldID
		,A.Key3
		,G.Key3
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key3,''))) <> RTRIM(LTRIM(ISNULL(A.Key3,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key4ImportFieldID
		,A.Key4
		,G.Key4
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key4,''))) <> RTRIM(LTRIM(ISNULL(A.Key4,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key5ImportFieldID
		,A.Key5
		,G.Key5
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key5,''))) <> RTRIM(LTRIM(ISNULL(A.Key5,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key6ImportFieldID
		,A.Key6
		,G.Key6
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key6,''))) <> RTRIM(LTRIM(ISNULL(A.Key6,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key7ImportFieldID
		,A.Key7
		,G.Key7
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key7,''))) <> RTRIM(LTRIM(ISNULL(A.Key7,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key8ImportFieldID
		,A.Key8
		,G.Key8
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key8,''))) <> RTRIM(LTRIM(ISNULL(A.Key8,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@key9ImportFieldID
		,A.Key9
		,G.Key9
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.Key9,''))) <> RTRIM(LTRIM(ISNULL(A.Key9,''))))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@AcctNameImportFieldID
		,A.AccountName
		,G.AccountName
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.AccountName,''))) != RTRIM(LTRIM(A.AccountName)))
	UNION ALL
	SELECT
		G.ExcelRowNumber
		,G.AccountID
		,@AcctNumberImportFieldID
		,A.AccountNumber
		,G.AccountNumber
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID 
		AND (RTRIM(LTRIM(ISNULL(G.AccountNumber,''))) != RTRIM(LTRIM(A.AccountNumber)))

	IF (SELECT COUNT(1) FROM @tblMessageData)>0
	BEGIN

		SELECT @MessageSchemaXML = 
		N'<?xml version="1.0" encoding="utf-16"?>
		  <xs:schema xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata"
		  xmlns:msprop="urn:schemas-microsoft-com:xml-msprop">
		  <xs:element name="MessageData" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
			<xs:complexType>
			  <xs:choice minOccurs="0" maxOccurs="unbounded">
				<xs:element name="MessageRow">
				  <xs:complexType>
					<xs:sequence>
					  <xs:element name="ImportFieldID" type="xs:integer" minOccurs="0" msprop:IsLabelID="true" msprop:IsVisible="false" />
					  <xs:element name="ImportField" type="xs:string" minOccurs="0" msprop:HeaderLabelID="2104" msprop:IsVisible="true" />
					  <xs:element name="OldValue" type="xs:string" minOccurs="0" msprop:HeaderLabelID="2864" msprop:IsVisible="true" />
					  <xs:element name="NewValue" type="xs:string" minOccurs="0" msprop:HeaderLabelID="2865" msprop:IsVisible="true" />
					</xs:sequence>
				  </xs:complexType>
				</xs:element>
			  </xs:choice>
			</xs:complexType>
		  </xs:element>
		</xs:schema>'

		;with cteExcelRows AS
		(
			SELECT DISTINCT ExcelRowNumber, AccountID
			FROM @tblMessageData
		)
		,cteImportField AS
		(
			SELECT
				ImportFieldID
				,IsNull(ImportTemplateField, dbo.fn_GetPhrase(DI.ImportFieldLabelID, @LanguageID, @companyID, @DefaultLanguageID)) AS ImportField
			FROM
				dbo.fn_SEL_DataImportFields(@dataImportID) DI
		)
		,cteRowData AS
		(
			SELECT ExcelRowNumber, AccountID
				,(SELECT WD2.ImportFieldID, IMF.ImportField, WD2.OldValue, WD2.NewValue
					FROM
						@tblMessageData WD2
					INNER JOIN cteImportField IMF
						ON WD2.ImportFieldID = IMF.ImportFieldID
					WHERE WD2.ExcelRowNumber = WD1.ExcelRowNumber
					FOR XML RAW('MessageRow'), ELEMENTS, Type, ROOT ('MessageData')
				) AS MessageDataXML
			FROM cteExcelRows WD1
		)
		INSERT INTO @udtDataImportMessageDetailType 
			(
				DataImportID
				,DataImportMessageID
				,DataImportMessageLabelID
				,DataImportMessage
				,ExcelRowNumber
				,AccountID
				,MessageSchema
				,MessageData
				,IsActive
				,DateAdded
			)
			SELECT @DataImportID
					,32
					,dbo.fn_GET_DataImportMessageLabelID(32)
					,null
					,RD.ExcelRowNumber
					,RD.AccountID
					,@MessageSchemaXML
					,RD.MessageDataXML
					,1
					,@DateAdded
			FROM
				cteRowData RD
	END
	--Update GLDataTransit for invalid Accounts
	UPDATE
		G
	SET
		G.IsValidRow = 0
		, G.ErrorMessage = REPLACE(@ErrorMessageWith2param,'{0}',CAST(G.ExcelRowNumber AS VARCHAR(10)))
	OUTPUT Inserted.ExcelRowNumber INTO @tblExcelRowNumber(ExcelRowNumber)
	FROM
		GLDataTransit G
	WHERE
		G.DataImportID = @dataImportID
		AND G.AccountID IS NULL				

	INSERT INTO @udtDataImportMessageDetailType 
	(
		DataImportID
		,DataImportMessageID
		,DataImportMessageLabelID
		,DataImportMessage
		,ExcelRowNumber
		,MessageSchema
		,MessageData
		,IsActive
		,DateAdded
	)
	SELECT @DataImportID
		,26
		,dbo.fn_GET_DataImportMessageLabelID(26)
		,null
		,ExcelRowNumber
		,NULL
		,NULL
		,1
		,@DateAdded
	FROM @tblExcelRowNumber

	IF (SELECT COUNT(1) FROM @udtDataImportMessageDetailType)>0
	BEGIN
		EXEC [dbo].[usp_INS_DataImportMessageDetail]   
			@DataImportMessageDetailType = @udtDataImportMessageDetailType
			,@DataImportID = @dataImportID  
			,@DateAdded = @dateAdded
	END
END
