




CREATE PROCEDURE [dbo].[usp_SEL_AllLabelTypes] 
(
	@ApplicationID INT,
	@BusinessEntityID INT
)
AS

/*------------------------------------------------------------------------------
	AUTHOR:			Vinay
	DATE CREATED:	01/05/2012
	PURPOSE:		Get All Languages
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------
	SAMPLE CALL
	exec dbo.usp_SEL_AllLanguages 
		@ApplicationID = 1,
		@BusinessEntityID = 1
-------------------------------------------------------------------------------*/
BEGIN
	 SELECT 
		LabelTypeID
		,Name
		,AddedBy
		,DateAdded
		,RevisedBy
		,DateRevised
		,HostName
	 FROM 
		dbo.LabelTypeMst
END

