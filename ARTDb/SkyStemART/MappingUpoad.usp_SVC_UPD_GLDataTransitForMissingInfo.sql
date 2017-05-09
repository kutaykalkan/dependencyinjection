
ALTER PROCEDURE [MappingUpload].[usp_SVC_UPD_GLDataTransitForMissingInfo]  
 (
	@companyID INT
	,@dataImportID INT
	,@RecPeriodID INT
	,@RecPeriodendDate SMALLDATETIME
	,@LanguageID INT
	,@DefaultLanguageID INT
	,@IsAccountUpload BIT
	,@DateAdded DATETIME
) 
 
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Harsh Kuntail
	DATE CREATED:	11/19/2011
	PURPOSE:		UPDATE GLDataTransit on the basis of Mapped Account keys for missing info
-------------------------------------------------------------------------------
	MODIFIED		DATE MODIFIED		DESCRIPTION
	BY
	Manoj			11/24/2011			make changes aboy account upload
	Vinay			4/12/2012			Commented if condition as duplicate check should be done always
	Harsh			07/04/2012			Increased field width from 250 --> 2000 for AccountName, 100--> 2000 for Keys in @tblAccountKeyInDB
	Harsh			07/05/2012			Field width changes
	Vinay			09/02/2012			Optimization Changes
	Harsh			09/04/2012			used fn_SEL_SystemPhrases and @DefaultLanguageID
	Vinay			12/10/2012			Vinay Production bug fixed
	Manoj			29/05/2014			Increase Error Message length
	Vinay			3/18/2015			Warning Storage Changes
	Vinay			4/1/2015			Custom Field Changes
	Vinay			4/10/2015			Mark Original row in error to suppress will create new account
	Vinay			4/28/2015			As per request message format changed 
-------------------------------------------------------------------------------
sample call
declare @addedby varchar(128)
declare @PeriodEndDate smalldatetime = (Select PeriodEndDate From ReconciliationPeriod Where ReconciliationPeriodID = 1384)
declare @ReturnValue nvarchar(max)
exec [MappingUpload].[usp_SVC_UPD_GLDataTransitForMissingInfo]  292, 1713, 1384, @PeriodEndDate, 1033, 1033 
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
	DECLARE @ErrorMessageDuplicateKey NVARCHAR(3000)
	DECLARE @DefaultErrorMessage NVARCHAR(MAX)  
	DECLARE @Seperator CHAR(1) = '_'

	DECLARE @MessageSchemaXML XML
	DECLARE @tblMessageData TABLE(ExcelRowNumber INT,  AccountID BIGINT, ImportFieldID INT, OldValue NVARCHAR(2000), NewValue NVARCHAR(2000))
	DECLARE @udtDataImportMessageDetailType udt_DataImportMessageDetailType
	DECLARE @tblDuplicateRows TABLE(ExcelRowNumber INT,DuplicateRowNumber INT)

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
	--5000153  Row: {0} is Duplicate of Row: {1}  
	SET @ErrorMessageDuplicateKey = dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000153,@LanguageID, 0)
	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpPhraseForGLDataTransitMIssingInfo'))
	BEGIN
		DROP TABLE #TmpPhraseForGLDataTransitMIssingInfo
	END	
	CREATE TABLE #TmpPhraseForGLDataTransitMIssingInfo (LabelID INT, Phrase NVARCHAR(2000))

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
		, IsProfitAndLoss NVARCHAR(100)
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
	DECLARE @ErrorMessageMismatched NVARCHAR(MAX)
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpDuplicateSubSetKey'))
	BEGIN
		DROP TABLE #tmpDuplicateSubSetKey
	END	

	CREATE TABLE #tmpDuplicateSubSetKey 
	(
		AccountSubSetKey NVARCHAR (MAX)
		, ParentExcelRowNumber INT
	)
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpUniqueSubSetKey'))
	BEGIN
		DROP TABLE #tmpUniqueSubSetKey
	END	
	CREATE TABLE #tmpUniqueSubSetKey 
	(
		AccountSubSetKey NVARCHAR (MAX)
		, AccountID BIGINT
	)
	
	
	SET @ErrorMessageMismatched = ISNULL((Select [dbo].fn_GetPhraseForDefaultBuinsessEntityID (5000313 ,@languageID,@defaultLanguageID) ),@DEFAULTErrorMessage)
		
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
	INSERT INTO #TmpPhraseForGLDataTransitMIssingInfo (LabelID , Phrase )
	SELECT 
		P.LabelID 
		, RTRIM(LTRIM(CAST(P.Phrase AS NVARCHAR(2000)))) 
	FROM
		dbo.fn_SEL_BusinessEntityPhrases(@companyID, @LanguageID, @DefaultLanguageID) P
	
	INSERT INTO #TmpPhraseForGLDataTransitMIssingInfo (LabelID , Phrase )	
	Select 
		P.LabelID 
		, RTRIM(LTRIM(CAST(P.Phrase AS NVARCHAR(2000)))) 
	From
		dbo.fn_SEL_SystemPhrases(0, @LanguageID, @DefaultLanguageID) P --Default Business entity
		INNER JOIN AccountTypeMst AT
			ON P.LabelID = AT.AccountTypeLabelID 
			
	--Get All Data For Account. We cannot use vw_AccountKeyInfo as it does not give information about P&N Accounts
	INSERT INTO #tmpAccountKeyInDB 
	(
		AccountID
		, AccountName 
		, FSCaptionID
		, FSCaptionLabelID 
		, AccountTypeID
		, AccountTypeLabelID
		, IsProfitAndLoss
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
		, CASE WHEN IsNull(A.IsProfitAndLoss,0) = 1 
			THEN dbo.fn_GetPhraseForDefaultBuinsessEntityID (1252, @LanguageID, @DefaultLanguageID)  
			ELSE dbo.fn_GetPhraseForDefaultBuinsessEntityID (1251, @LanguageID, @DefaultLanguageID)  
		  END	
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
		dbo.vw_Select_ActiveAccounts A
		INNER JOIN GeographyObjectHdr GOH
			ON A.GeographyObjectID = GOH.GeographyObjectID 
		LEFT OUTER JOIN FSCaptionHdr F
			ON A.FSCaptionID = F.FSCaptionID AND F.IsActive = 1
		LEFT OUTER JOIN AccountTypeMst AT
			ON A.AccountTypeID = AT.AccountTypeID AND AT.IsActive = 1
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo PF 
			ON F.FSCaptionLabelID = PF.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo PA 
			ON AT.AccountTypeLabelID = PA.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P2 
			ON GOH.Key2LabelID = P2.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P3 
			ON GOH.Key3LabelID = P3.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P4 
			ON GOH.Key4LabelID = P4.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P5 
			ON GOH.Key5LabelID = P5.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P6 
			ON GOH.Key6LabelID = P6.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P7 
			ON GOH.Key7LabelID = P7.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P8 
			ON GOH.Key8LabelID = P8.LabelID
		LEFT OUTER JOIN #TmpPhraseForGLDataTransitMIssingInfo P9 
			ON GOH.Key9LabelID = P9.LabelID
		INNER JOIN #TmpPhraseForGLDataTransitMIssingInfo PAN
			ON A.AccountNameLabelID = PAN.LabelID		
	WHERE
		GOH.CompanyID = @companyID 
		AND GOH.IsActive = 1
	
		
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
	 END--*******SubSet Key Generation End *****************
	

	--Duplicate SubSetKey Check within GLDataTransit

	
	INSERT INTO #tmpDuplicateSubSetKey (AccountSubSetKey , ParentExcelRowNumber )
	SELECT
		AccountSubSetKey, MIN(ExcelRowNumber) AS ParentRow
	FROM
		GLDataTransit G 
	WHERE
		G.DataImportID = @dataImportID
	GROUP BY 
		G.AccountSubSetKey 
	HAVING
		COUNT(1) > 1				
	
	INSERT INTO #tmpUniqueSubSetKey (AccountSubSetKey)
	SELECT
		AccountSubSetKey
	FROM
		GLDataTransit G
	WHERE
		G.DataImportID = @dataImportID
	GROUP BY 
		G.AccountSubSetKey 
	HAVING
		COUNT(1) = 1	
	
	UPDATE 
		G
	SET
		G.IsValidRow = 0
		--Row: {0} is Duplicate of Row: {1}
		,G.ErrorMessage = REPLACE(REPLACE(@ErrorMessageDuplicateKey,'{0}',CAST(G.ExcelRowNumber AS VARCHAR(10))),'{1}',CAST(D.ParentExcelRowNumber AS NVARCHAR(10)))
	OUTPUT inserted.ExcelRowNumber, D.ParentExcelRowNumber INTO @tblDuplicateRows (DuplicateRowNumber, ExcelRowNumber)
	FROM
		GLDataTransit G
		INNER JOIN #tmpDuplicateSubSetKey D 
			ON G.AccountSubSetKey = D.AccountSubSetKey 
	WHERE
		G.DataImportID = @dataImportID
		AND G.ExcelRowNumber <> D.ParentExcelRowNumber 

	IF (SELECT COUNT(1) FROM @tblDuplicateRows)>0
	BEGIN

		;with cteOriginalRows
		AS
		(
			SELECT DISTINCT ExcelRowNumber
			FROM
				@tblDuplicateRows
		)
		UPDATE GT
			SET IsValidRow = 0
		FROM 
			dbo.GLDataTransit GT
		INNER JOIN
			cteOriginalRows TGT
			ON GT.ExcelRowNumber = TGT.ExcelRowNumber
		WHERE
			GT.DataImportID = @DataImportID

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
					  <xs:element name="DuplicateRowNumber" type="xs:integer" minOccurs="0" msprop:HeaderLabelID="2878" msprop:IsVisible="true" />
					</xs:sequence>
				  </xs:complexType>
				</xs:element>
			  </xs:choice>
			</xs:complexType>
		  </xs:element>
		</xs:schema>'

		;with cteExcelRows AS
		(
			SELECT DISTINCT ExcelRowNumber
			FROM @tblDuplicateRows
		),
		cteRowData AS
		(
			SELECT WD1.ExcelRowNumber
				,(	SELECT 
						WD2.DuplicateRowNumber
					FROM
						@tblDuplicateRows WD2
					WHERE WD2.ExcelRowNumber = WD1.ExcelRowNumber
					ORDER BY WD2.DuplicateRowNumber
					FOR XML RAW('MessageRow'), ELEMENTS, Type, ROOT ('MessageData')
				) AS MessageDataXML
			FROM cteExcelRows AS WD1
		)
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
					,2
					,dbo.fn_GET_DataImportMessageLabelID(2)
					,null
					,RD.ExcelRowNumber
					,@MessageSchemaXML
					,RD.MessageDataXML
					,1
					,@DateAdded
			FROM
				cteRowData RD
	END

	-- Update AccountID based on subsetkey comparision for existing accounts 
	UPDATE 
		G
	SET
		G.AccountID = A.AccountID
	FROM
		GLDataTransit G
		INNER JOIN #tmpUniqueSubSetKey C
			ON G.AccountSubSetKey = C.AccountSubSetKey
		INNER JOIN 	#tmpAccountKeyInDB A
			ON  C.AccountSubSetKey = A.SubSETKey  
	WHERE
		G.DataImportID = @dataImportID
			
	IF (@IsAccountUpload = 0)
	BEGIN
		--Data enrichment: Fill missing information for which there is one and only one matching record in db
		UPDATE 
			G
		SET
			G.FSCaption = CASE WHEN G.FSCaption IS NULL AND A.FSCaptionID IS NOT NULL THEN A.FSCaption ELSE G.FSCaption END
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
			, G.AccountNumber = CASE WHEN G.AccountNumber IS NULL THEN A.AccountNumber ELSE G.AccountNumber END
			, G.IsProfitAndLoss = CASE WHEN G.IsProfitAndLoss IS NULL THEN A.IsProfitAndLoss ELSE G.IsProfitAndLoss END 
		FROM
			GLDataTransit G
			INNER JOIN 	#tmpAccountKeyInDB A
				ON G.AccountID = A.AccountID   
		WHERE
			G.DataImportID = @dataImportID	
	END			

	--FSCaption, Account and SubSet Match with DB	
	UPDATE 
		G
	SET
		G.IsValidRow = 0
		, G.ErrorMessage = REPLACE (@ErrorMessageMismatched, '{0}',cast(ExcelRowNumber as Varchar(10)))
	FROM 
		GLDataTransit G
		INNER JOIN #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID				
		AND (RTRIM(LTRIM(ISNULL(G.FSCaption,''))) <> RTRIM(LTRIM(ISNULL(A.FSCaption,''))) 
			OR RTRIM(LTRIM(ISNULL(G.AccountType,''))) <> RTRIM(LTRIM(ISNULL(A.AccountType,''))))

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
	UNION
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
					  <xs:element name="ImportFieldID" type="xs:integer" minOccurs="0" msprop:IsVisible="false" />
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
					,20
					,dbo.fn_GET_DataImportMessageLabelID(20)
					,null
					,RD.ExcelRowNumber
					,RD.AccountID
					,@MessageSchemaXML
					,RD.MessageDataXML
					,1
					,@DateAdded
			FROM
				cteRowData RD
		DELETE FROM @tblMessageData
	END
				
	-- Check if there is any change in geography key for good records
	Declare @key2ChangedErrorMsg nvarchar(1000) 
	Declare @key3ChangedErrorMsg nvarchar(1000)
	Declare @key4ChangedErrorMsg nvarchar(1000)
	Declare @key5ChangedErrorMsg nvarchar(1000)
	Declare @key6ChangedErrorMsg nvarchar(1000)
	Declare @key7ChangedErrorMsg nvarchar(1000)
	Declare @key8ChangedErrorMsg nvarchar(1000)
	Declare @key9ChangedErrorMsg nvarchar(1000)
	Declare @AccountNameChangedErrorMsg nvarchar(1000)
	Declare @AccountNumberChangedErrorMsg nvarchar(1000)
	Declare @rowErrorMsg nvarchar(3000)
	
	--Set @rowErrorMsg = 'Row# {0}, {1} has changed from {2} to {3}'--Row# 3: Region has changed from abc to xyz
	Select @rowErrorMsg = dbo.fn_GetPhraseForDefaultBuinsessEntityID (5000343, @LanguageID, @DefaultLanguageID ) 
	
	Select
		@key2ChangedErrorMsg =  CASE WHEN GS.GeographyClassID = 2 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key2ChangedErrorMsg end
		,@key3ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 3 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key3ChangedErrorMsg end
		,@key4ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 4 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key4ChangedErrorMsg end
		,@key5ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 5 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key5ChangedErrorMsg end
		,@key6ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 6 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key6ChangedErrorMsg end
		,@key7ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 7 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key7ChangedErrorMsg end
		,@key8ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 8 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key8ChangedErrorMsg end
		,@key9ChangedErrorMsg = CASE WHEN GS.GeographyClassID = 9 THEN REPLACE(@rowErrorMsg,'{1}', dbo.fn_GetPhrase (GS.GeographyStructureLabelID, @LanguageID, @companyID, @DefaultLanguageID )) else @key9ChangedErrorMsg end
	From
		GeographyStructureHdr GS 
	Where
		GS.CompanyID = @companyID 		
	
	
	UPDATE
		G
	SET
		G.HasGeographyInfoChangedForExistingAccount = 1
		, G.RowHasWarning = 1
		, G.WarningMessage = 
							stuff (case when (RTRIM(LTRIM(ISNULL(G.Key2,''))) <> RTRIM(LTRIM(ISNULL(A.Key2,'')))) then	', ' + REPLACE (REPLACE(REPLACE(@key2ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key2), '{3}',G.Key2)  else '' end
							+ case when (RTRIM(LTRIM(ISNULL(G.Key3,''))) <> RTRIM(LTRIM(ISNULL(A.Key3,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key3ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key3), '{3}',G.Key3)  else '' end 
							+ case when (RTRIM(LTRIM(ISNULL(G.Key4,''))) <> RTRIM(LTRIM(ISNULL(A.Key4,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key4ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key4), '{3}',G.Key4)  else '' end 
							+ case when (RTRIM(LTRIM(ISNULL(G.Key5,''))) <> RTRIM(LTRIM(ISNULL(A.Key5,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key5ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key5), '{3}',G.Key5)  else '' end
							+ case when (RTRIM(LTRIM(ISNULL(G.Key6,''))) <> RTRIM(LTRIM(ISNULL(A.Key6,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key6ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key6), '{3}',G.Key6)  else '' end 
							+ case when (RTRIM(LTRIM(ISNULL(G.Key7,''))) <> RTRIM(LTRIM(ISNULL(A.Key7,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key7ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key7), '{3}',G.Key7)  else '' end
							+ case when (RTRIM(LTRIM(ISNULL(G.Key8,''))) <> RTRIM(LTRIM(ISNULL(A.Key8,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key8ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key8), '{3}',G.Key8)  else '' end
							+ case when (RTRIM(LTRIM(ISNULL(G.Key9,''))) <> RTRIM(LTRIM(ISNULL(A.Key9,'')))) then ', ' + REPLACE (REPLACE(REPLACE(@key9ChangedErrorMsg, '{0}', CAST(G.ExcelRowNumber as NVARCHAR(10))),'{2}',A.Key9), '{3}',G.Key9)  else '' end
							,1,2,'')
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountID = A.AccountID  
	WHERE
		G.DataImportID = @dataImportID
		AND 
		(
			(RTRIM(LTRIM(ISNULL(G.Key2,''))) <> RTRIM(LTRIM(ISNULL(A.Key2,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key3,''))) <> RTRIM(LTRIM(ISNULL(A.Key3,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key4,''))) <> RTRIM(LTRIM(ISNULL(A.Key4,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key5,''))) <> RTRIM(LTRIM(ISNULL(A.Key5,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key6,''))) <> RTRIM(LTRIM(ISNULL(A.Key6,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key7,''))) <> RTRIM(LTRIM(ISNULL(A.Key7,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key8,''))) <> RTRIM(LTRIM(ISNULL(A.Key8,''))))
			OR (RTRIM(LTRIM(ISNULL(G.Key9,''))) <> RTRIM(LTRIM(ISNULL(A.Key9,''))))
			 
		)
		
	DECLARE @accountNameChangedErrorMessage NVARCHAR(100)
	DECLARE @accountNumberChangedErrorMessage NVARCHAR(100)
	SELECT
		@accountNumberChangedErrorMessage = REPLACE (@rowErrorMsg, '{1}', dbo.fn_GetPhraseForDefaultBuinsessEntityID (1491,@LanguageID, @DefaultLanguageID))
		,  @accountNameChangedErrorMessage = REPLACE (@rowErrorMsg, '{1}',dbo.fn_GetPhraseForDefaultBuinsessEntityID(2243,@LanguageID, @DefaultLanguageID))
	
	-- Check if there is any change in Account Number and Account Name for good records
	--Set @rowErrorMsg = 'Row# {0}, {1} has changed from {2} to {3}'--Row# 3: Region has changed from abc to xyz
	UPDATE
		G
	SET
		G.HasAccountInfoChangedForExistingAccount = 1
		, G.RowHasWarning = 1
		, G.WarningMessage = ISNULL (G.WarningMessage,'') 
			+ ISNULL(stuff (CASE WHEN (RTRIM(LTRIM(ISNULL(G.AccountNumber,''))) != RTRIM(LTRIM(A.AccountNumber)) ) THEN ', ' 
			+ REPLACE(REPLACE (REPLACE (@accountNumberChangedErrorMessage, '{0}', CAST(G.ExcelRowNumber as VARCHAR(10))),'{2}',A.AccountNumber),'{3}',G.AccountNumber ) ELSE '' END
			+ CASE WHEN (RTRIM(LTRIM(ISNULL(G.AccountName,''))) != RTRIM(LTRIM(A.AccountName))) THEN ', '
			+REPLACE(REPLACE (REPLACE (@accountNameChangedErrorMessage, '{0}', CAST(G.ExcelRowNumber as VARCHAR(10))),'{2}',A.AccountName),'{3}',G.AccountName ) ELSE '' END
			,1,2,''),'')
	FROM
		GLDataTransit G
		inner join #tmpAccountKeyInDB A
			ON G.AccountSubSetKey  = A.SubSETKey  			
	WHERE
		G.DataImportID = @dataImportID
		AND 
		(
			(RTRIM(LTRIM(ISNULL(G.AccountNumber,''))) != RTRIM(LTRIM(A.AccountNumber)))
			OR (RTRIM(LTRIM(ISNULL(G.AccountName,''))) != RTRIM(LTRIM(A.AccountName)))
		)		

	UPDATE GLT
		SET GLDataID = AGL.GLDataID
	FROM
		dbo.GLDataTransit GLT
	INNER JOIN
		dbo.vw_Select_ActiveGLData AGL
		ON GLT.AccountID = AGL.AccountID AND AGL.ReconciliationPeriodID = @RecPeriodID
	WHERE
		GLT.DataImportID = @dataImportID

	INSERT INTO @tblMessageData (ExcelRowNumber, AccountID, ImportFieldID, OldValue, NewValue)
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasGeographyInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasAccountInfoChangedForExistingAccount = 1
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
		G.DataImportID = @dataImportID AND G.HasAccountInfoChangedForExistingAccount = 1
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
					,4
					,dbo.fn_GET_DataImportMessageLabelID(4)
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
	IF (SELECT COUNT(1) FROM @udtDataImportMessageDetailType)>0
	BEGIN
		EXEC [dbo].[usp_INS_DataImportMessageDetail]   
			@DataImportMessageDetailType = @udtDataImportMessageDetailType
			,@DataImportID = @dataImportID  
			,@DateAdded = @dateAdded
	END

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpPhraseForGLDataTransitMIssingInfo'))
	BEGIN
		DROP TABLE #TmpPhraseForGLDataTransitMIssingInfo
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpAccountKeyInDB'))
	BEGIN
		DROP TABLE #tmpAccountKeyInDB
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpDuplicateSubSetKey'))
	BEGIN
		DROP TABLE #tmpDuplicateSubSetKey
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpUniqueSubSetKey'))
	BEGIN
		DROP TABLE #tmpUniqueSubSetKey
	END	
END
