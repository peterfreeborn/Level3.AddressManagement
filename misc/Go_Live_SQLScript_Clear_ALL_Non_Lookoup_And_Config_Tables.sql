----RESET DB
---- THIS SCRIPT REMOVES ALL INSTANCE DATA FROM THE DB, LEAVING THE NECESSARY LOOKUP AND CONFIG TABLES ALONE
---- ONLY RUN THIS SCRIPT IF YOU KNOW WHAT YOU ARE DOING - THIS IS HARD CODED TO RESET THE PROD DB INSTANCE

--DELETE FROM [dbo].[tblSystemLogItem]
--USE AddressManagement;
--GO
--DBCC CHECKIDENT ('AddressManagement.dbo.tblSystemLogItem', RESEED, 0);
--GO

--DELETE FROM [dbo].[tblOrderAddressLogItem]
--USE AddressManagement;
--GO
--DBCC CHECKIDENT ('AddressManagement.dbo.tblOrderAddressLogItem', RESEED, 0);
--GO

--DELETE FROM [dbo].[tblAPICallLogItem]
--USE AddressManagement;
--GO
--DBCC CHECKIDENT ('AddressManagement.dbo.tblAPICallLogItem', RESEED, 0);
--GO

--DELETE FROM [dbo].[tblOrderAddress]
--USE AddressManagement;
--GO
--DBCC CHECKIDENT ('AddressManagement.dbo.tblOrderAddress', RESEED, 0);
--GO
