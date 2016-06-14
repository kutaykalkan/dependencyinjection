IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CompanyAlertDetailUser]') AND name = N'IX_CompanyAlertDetailUser_CompanyAlertDetailID_MailSentDate')
DROP INDEX [IX_CompanyAlertDetailUser_CompanyAlertDetailID_MailSentDate] ON [CompanyAlertDetailUser]
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CompanyAlertDetailUser]') AND name = N'IX_CompanyAlertDetailUser_CompanyAlertDetailID_MailSentDate')
CREATE NONCLUSTERED INDEX [IX_CompanyAlertDetailUser_CompanyAlertDetailID_MailSentDate] ON [CompanyAlertDetailUser]
(
	[CompanyAlertDetailID] ASC,
	[MailSentDate] ASC
)
INCLUDE ( 	[CompanyAlertDetailUserID],
	[AlertDescription],
	[UserID],
	[RoleID]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


