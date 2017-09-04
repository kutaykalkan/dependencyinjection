
BEGIN TRY
BEGIN TRAN
INSERT INTO Labels (LabelID, LabelDescription, LabelTypeID, AddedBy, DateAdded, RevisedBy, DateRevised, HostName) VALUES (5000429,'Balances cannot be updated because work has already been started in subsequent period.',3,'sa','Aug  1 2017 12:43PM',NULL,NULL,'DEV-SQL-01')

SET IDENTITY_INSERT JoinApplicationLabel ON
INSERT INTO JoinApplicationLabel (JoinApplicationLabelID, ApplicationID, LabelID) VALUES (2876509,1,5000429)

SET IDENTITY_INSERT JoinApplicationLabel OFF
SET IDENTITY_INSERT Phrases ON
INSERT INTO Phrases (PhraseID, LanguageID, BusinessEntityID, LabelID, Phrase, Remarks, AddedBy, DateAdded, RevisedBy, DateRevised, HostName) VALUES (210833,1033,0,5000429,'Balances cannot be updated because work has already been started in subsequent period.',NULL,'sa','Aug  1 2017 12:43PM',NULL,NULL,'DEV-SQL-01')
INSERT INTO Phrases (PhraseID, LanguageID, BusinessEntityID, LabelID, Phrase, Remarks, AddedBy, DateAdded, RevisedBy, DateRevised, HostName) VALUES (210834,1034,0,5000429,'S:Balances cannot be updated because work has already been started in subsequent period.',NULL,'sa','Aug  1 2017 12:43PM',NULL,NULL,'DEV-SQL-01')

SET IDENTITY_INSERT Phrases OFF
COMMIT TRAN
END TRY
BEGIN CATCH
	PRINT ERROR_MESSAGE()
	ROLLBACK TRAN
END CATCH
