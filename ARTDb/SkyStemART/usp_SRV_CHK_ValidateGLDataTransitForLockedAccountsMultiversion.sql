
CREATE PROCEDURE [dbo].[usp_SRV_CHK_ValidateGLDataTransitForLockedAccountsMultiversion]  
(
	@CompanyID INT
	,@DataImportID INT
	,@RecPeriodID INT
	,@PeriodEndDate DATETIME
	,@LanguageID INT
	,@BusinessEntityID INT
	,@DefaultLanguageID INT
	,@DateAdded DATETIME
)  
 
  
AS  
/*------------------------------------------------------------------------------  
 AUTHOR    Vinay 
 Creation Date  08/01/2017  
 Purpose: If Account from GLdataTransit already exists in GLDataHDR with different ReportingCurrencyAmount, RCCY, BCCY and Rec is Locked  
-------------------------------------------------------------------------------  
 MODIFIED		AUTHOR				DESCRIPTION  
 DATE 
-------------------------------------------------------------------------------  

-------------------------------------------------------------------------------  
 Table Affected:
		1. GLDataTransit
Usage Example
	 Exec usp_SRV_CHK_ValidateGLDataTransitForLockedAccountsMultiversion 246,1189,1140,1033,246,1033
-------------------------------------------------------------------------------*/   
BEGIN

	DECLARE @tblAffectedRecords udt_BigIntIDTableType
	DECLARE @MessageSchemaXML XML
	DECLARE @tblMessageData TABLE(ExcelRowNumber INT, AccountID BIGINT, ImportFieldID INT, OldValue NVARCHAR(2000), NewValue NVARCHAR(2000))
	DECLARE @udtDataImportMessageDetailType udt_DataImportMessageDetailType
	DECLARE @GLBalanceRCCYImportFieldID SMALLINT = 11
		,@GLBalanceBCCYImportFieldID SMALLINT = 9
		,@CurrencyCodeRCCYImportFieldID SMALLINT = 10
		,@CurrencyCodeBCCYImportFieldID SMALLINT = 8


	INSERT into @tblAffectedRecords(ID)
	SELECT GLDataTransitID
	FROM 
		[dbo].[GLDataTransit] GLT
	INNER JOIN 
		[dbo].[vw_Select_AccountGLData] AGL
		ON AGL.AccountID = GLT.AccountID
			AND AGL.ReconciliationPeriodID = @recPeriodID
	LEFT JOIN [dbo].[fn_SEL_LockedAccounts](@companyID, @PeriodEndDate) L
		ON GLT.AccountID = L.AccountID
	INNER JOIN 
		[dbo].[vw_Select_AccountKeyInfo] VW 
		ON GLT.AccountID = VW.AccountID 		
	WHERE
		GLT.DataImportID = @dataImportID
		AND (L.IsLocked = 1)
		AND (CAST(GLT.GLBalanceReportingCurrency AS decimal(18,4)) <> AGL.GLBalanceReportingCurrency
			OR GLT.ReportingCurrencyCode <> AGL.ReportingCurrencyCode
			OR CAST(GLT.GLBalanceBaseCurrency AS decimal(18,4)) <> AGL.GLBalanceBaseCurrency 
			OR GLT.BaseCurrencyCode <> AGL.BaseCurrencyCode 
			)
		
	INSERT INTO @tblMessageData (ExcelRowNumber, AccountID, ImportFieldID, OldValue, NewValue)
	SELECT
		GLT.ExcelRowNumber
		,GLT.AccountID
		,@GLBalanceRCCYImportFieldID
		,CONVERT(NVARCHAR(2000),AGL.GLBalanceReportingCurrency)
		,GLT.GLBalanceReportingCurrency
	FROM 
		[dbo].[GLDataTransit] GLT
	INNER JOIN @tblAffectedRecords AR
		ON GLT.GLDataTransitID = AR.ID
	INNER JOIN
		[dbo].[vw_Select_ActiveGLData] AGL
		ON GLT.GLDataID = AGL.GLDataID
	WHERE CAST(GLT.GLBalanceReportingCurrency AS decimal(18,4)) <> AGL.GLBalanceReportingCurrency
	UNION
	SELECT
		GLT.ExcelRowNumber
		,GLT.AccountID
		,@GLBalanceBCCYImportFieldID
		,CONVERT(NVARCHAR(2000),AGL.GLBalanceBaseCurrency)
		,GLT.GLBalanceBaseCurrency
	FROM 
		[dbo].[GLDataTransit] GLT
	INNER JOIN @tblAffectedRecords AR
		ON GLT.GLDataTransitID = AR.ID
	INNER JOIN
		[dbo].[vw_Select_ActiveGLData] AGL
		ON GLT.GLDataID = AGL.GLDataID
	WHERE CAST(GLT.GLBalanceBaseCurrency AS decimal(18,4)) <> AGL.GLBalanceBaseCurrency
	UNION
	SELECT
		GLT.ExcelRowNumber
		,GLT.AccountID
		,@CurrencyCodeRCCYImportFieldID
		,AGL.ReportingCurrencyCode
		,GLT.ReportingCurrencyCode
	FROM 
		[dbo].[GLDataTransit] GLT
	INNER JOIN @tblAffectedRecords AR
		ON GLT.GLDataTransitID = AR.ID
	INNER JOIN
		[dbo].[vw_Select_ActiveGLData] AGL
		ON GLT.GLDataID = AGL.GLDataID
	WHERE GLT.ReportingCurrencyCode <> AGL.ReportingCurrencyCode
	UNION
	SELECT
		GLT.ExcelRowNumber
		,GLT.AccountID
		,@CurrencyCodeBCCYImportFieldID
		,AGL.BaseCurrencyCode
		,GLT.BaseCurrencyCode
	FROM 
		[dbo].[GLDataTransit] GLT
	INNER JOIN @tblAffectedRecords AR
		ON GLT.GLDataTransitID = AR.ID
	INNER JOIN
		[dbo].[vw_Select_ActiveGLData] AGL
		ON GLT.GLDataID = AGL.GLDataID
	WHERE GLT.BaseCurrencyCode <> AGL.BaseCurrencyCode

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
					,40
					,dbo.fn_GET_DataImportMessageLabelID(40)
					,null
					,RD.ExcelRowNumber
					,RD.AccountID
					,@MessageSchemaXML
					,RD.MessageDataXML
					,1
					,@DateAdded
			FROM
				cteRowData RD

		EXEC [dbo].[usp_INS_DataImportMessageDetail]   
			@DataImportMessageDetailType = @udtDataImportMessageDetailType
			,@DataImportID = @dataImportID  
			,@DateAdded = @dateAdded
	END

END
