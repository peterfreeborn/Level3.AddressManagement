IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_Insert' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_Insert];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_Insert
(@OrderAddressTypeID int, @MigrationStatusID int, @OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250), @CDWCLII varchar(50), @ValidCLII bit, @NumberOfFailedGLMSiteCalls int, @ExistsInGLMAsSite bit, @GLMPLNumber varchar(50), @NumberOfFailedGLMSiteCodeExistenceCalls int, @NumberOfFailedGLMSiteCodeCreationCalls int, @GLMSiteCode varchar(50), @HasGLMSiteCode bit, @NumberOfFailedSAPSiteAddressSearchCalls int, @NumberOfFailedSAPSiteAddressImportCalls int, @ExistsInSAPAsSiteAddress bit, @NumberOfRecordsInSAPWithPL int, @NumberOfFailedGLMServiceLocationSearchCalls int, @NumberOfFailedGLMServiceLocationCreationCalls int, @GLMSLNumber varchar(50), @ExistsInGLMAsServiceLocation bit, @NumberOfFailedGLMSCodeExistenceCalls int, @NumberOfFailedGLMSCodeCreationCalls int, @GLMSCode varchar(50), @HasGLMSCode bit, @NumberOfFailedSAPServiceLocationAddressSearchCalls int, @NumberOfFailedSAPServiceLocationAddressImportCalls int, @ExistsInSAPAsServiceLocationAddress bit, @NumberOfRecordsInSAPWithSL int, @SourceCreationDate datetime, @SourceLastModifyDate datetime, @DateTimeOfLastMigrationStatusUpdate datetime, @DateTimeOfLastDupDetection datetime, @DateCreated datetime, @DateUpdated datetime, @ServiceOrderNumber varchar(100), @FIRST_ORDER_CREATE_DT datetime, @OPE_LAST_MODIFY_DATE datetime, @PL_LAST_MODIFY_DATE datetime, @PS_LAST_MODIFY_DATE datetime, @TotalProcessingTimeInTickString varchar(200), @TotalProcessingTimeAsHumanReadable varchar(200))
AS
SET NOCOUNT  ON
INSERT INTO tblOrderAddress
(OrderAddressTypeID, MigrationStatusID, OrderSystemOfRecordID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState, CDWPostalCode, CDWCountry, CDWFloor, CDWRoom, CDWSuite, CDWCLII, ValidCLII, NumberOfFailedGLMSiteCalls, ExistsInGLMAsSite, GLMPLNumber, NumberOfFailedGLMSiteCodeExistenceCalls, NumberOfFailedGLMSiteCodeCreationCalls, GLMSiteCode, HasGLMSiteCode, NumberOfFailedSAPSiteAddressSearchCalls, NumberOfFailedSAPSiteAddressImportCalls, ExistsInSAPAsSiteAddress, NumberOfRecordsInSAPWithPL, NumberOfFailedGLMServiceLocationSearchCalls, NumberOfFailedGLMServiceLocationCreationCalls, GLMSLNumber, ExistsInGLMAsServiceLocation, NumberOfFailedGLMSCodeExistenceCalls, NumberOfFailedGLMSCodeCreationCalls, GLMSCode, HasGLMSCode, NumberOfFailedSAPServiceLocationAddressSearchCalls, NumberOfFailedSAPServiceLocationAddressImportCalls, ExistsInSAPAsServiceLocationAddress, NumberOfRecordsInSAPWithSL, SourceCreationDate, SourceLastModifyDate, DateTimeOfLastMigrationStatusUpdate, DateTimeOfLastDupDetection, DateCreated, DateUpdated, ServiceOrderNumber, FIRST_ORDER_CREATE_DT, OPE_LAST_MODIFY_DATE, PL_LAST_MODIFY_DATE, PS_LAST_MODIFY_DATE, TotalProcessingTimeInTickString, TotalProcessingTimeAsHumanReadable)
VALUES (@OrderAddressTypeID, @MigrationStatusID, @OrderSystemOfRecordID, @CDWCustomerOrderNumber, @CDWAddressOne, @CDWCity, @CDWState, @CDWPostalCode, @CDWCountry, @CDWFloor, @CDWRoom, @CDWSuite, @CDWCLII, @ValidCLII, @NumberOfFailedGLMSiteCalls, @ExistsInGLMAsSite, @GLMPLNumber, @NumberOfFailedGLMSiteCodeExistenceCalls, @NumberOfFailedGLMSiteCodeCreationCalls, @GLMSiteCode, @HasGLMSiteCode, @NumberOfFailedSAPSiteAddressSearchCalls, @NumberOfFailedSAPSiteAddressImportCalls, @ExistsInSAPAsSiteAddress, @NumberOfRecordsInSAPWithPL, @NumberOfFailedGLMServiceLocationSearchCalls, @NumberOfFailedGLMServiceLocationCreationCalls, @GLMSLNumber, @ExistsInGLMAsServiceLocation, @NumberOfFailedGLMSCodeExistenceCalls, @NumberOfFailedGLMSCodeCreationCalls, @GLMSCode, @HasGLMSCode, @NumberOfFailedSAPServiceLocationAddressSearchCalls, @NumberOfFailedSAPServiceLocationAddressImportCalls, @ExistsInSAPAsServiceLocationAddress, @NumberOfRecordsInSAPWithSL, @SourceCreationDate, @SourceLastModifyDate, @DateTimeOfLastMigrationStatusUpdate, @DateTimeOfLastDupDetection, @DateCreated, @DateUpdated, @ServiceOrderNumber, @FIRST_ORDER_CREATE_DT, @OPE_LAST_MODIFY_DATE, @PL_LAST_MODIFY_DATE, @PS_LAST_MODIFY_DATE, @TotalProcessingTimeInTickString, @TotalProcessingTimeAsHumanReadable)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ
(@CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50))
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddress.OrderAddressID,
	 tblOrderAddress.OrderAddressTypeID,
	 tblOrderAddress.MigrationStatusID,
	 tblOrderAddress.OrderSystemOfRecordID,
	 tblOrderAddress.CDWCustomerOrderNumber,
	 tblOrderAddress.CDWAddressOne,
	 tblOrderAddress.CDWCity,
	 tblOrderAddress.CDWState,
	 tblOrderAddress.CDWPostalCode,
	 tblOrderAddress.CDWCountry,
	 tblOrderAddress.CDWFloor,
	 tblOrderAddress.CDWRoom,
	 tblOrderAddress.CDWSuite,
	 tblOrderAddress.CDWCLII,
	 tblOrderAddress.ValidCLII,
	 tblOrderAddress.NumberOfFailedGLMSiteCalls,
	 tblOrderAddress.ExistsInGLMAsSite,
	 tblOrderAddress.GLMPLNumber,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls,
	 tblOrderAddress.GLMSiteCode,
	 tblOrderAddress.HasGLMSiteCode,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsSiteAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithPL,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls,
	 tblOrderAddress.GLMSLNumber,
	 tblOrderAddress.ExistsInGLMAsServiceLocation,
	 tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls,
	 tblOrderAddress.GLMSCode,
	 tblOrderAddress.HasGLMSCode,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsServiceLocationAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithSL,
	 tblOrderAddress.SourceCreationDate,
	 tblOrderAddress.SourceLastModifyDate,
	 tblOrderAddress.DateTimeOfLastMigrationStatusUpdate,
	 tblOrderAddress.DateTimeOfLastDupDetection,
	 tblOrderAddress.DateCreated,
	 tblOrderAddress.DateUpdated,
	 tblOrderAddress.ServiceOrderNumber,
	 tblOrderAddress.FIRST_ORDER_CREATE_DT,
	 tblOrderAddress.OPE_LAST_MODIFY_DATE,
	 tblOrderAddress.PL_LAST_MODIFY_DATE,
	 tblOrderAddress.PS_LAST_MODIFY_DATE,
	 tblOrderAddress.TotalProcessingTimeInTickString,
	 tblOrderAddress.TotalProcessingTimeAsHumanReadable
FROM tblOrderAddress
WHERE (
(tblOrderAddress.CDWAddressOne=@CDWAddressOne) AND 
(tblOrderAddress.CDWCity=@CDWCity) AND 
(tblOrderAddress.CDWState=@CDWState) AND 
(tblOrderAddress.CDWPostalCode=@CDWPostalCode) AND 
(tblOrderAddress.CDWCountry=@CDWCountry)
)
GO -- Execute




IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC
(@OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250))
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddress.OrderAddressID,
	 tblOrderAddress.OrderAddressTypeID,
	 tblOrderAddress.MigrationStatusID,
	 tblOrderAddress.OrderSystemOfRecordID,
	 tblOrderAddress.CDWCustomerOrderNumber,
	 tblOrderAddress.CDWAddressOne,
	 tblOrderAddress.CDWCity,
	 tblOrderAddress.CDWState,
	 tblOrderAddress.CDWPostalCode,
	 tblOrderAddress.CDWCountry,
	 tblOrderAddress.CDWFloor,
	 tblOrderAddress.CDWRoom,
	 tblOrderAddress.CDWSuite,
	 tblOrderAddress.CDWCLII,
	 tblOrderAddress.ValidCLII,
	 tblOrderAddress.NumberOfFailedGLMSiteCalls,
	 tblOrderAddress.ExistsInGLMAsSite,
	 tblOrderAddress.GLMPLNumber,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls,
	 tblOrderAddress.GLMSiteCode,
	 tblOrderAddress.HasGLMSiteCode,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsSiteAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithPL,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls,
	 tblOrderAddress.GLMSLNumber,
	 tblOrderAddress.ExistsInGLMAsServiceLocation,
	 tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls,
	 tblOrderAddress.GLMSCode,
	 tblOrderAddress.HasGLMSCode,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsServiceLocationAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithSL,
	 tblOrderAddress.SourceCreationDate,
	 tblOrderAddress.SourceLastModifyDate,
	 tblOrderAddress.DateTimeOfLastMigrationStatusUpdate,
	 tblOrderAddress.DateTimeOfLastDupDetection,
	 tblOrderAddress.DateCreated,
	 tblOrderAddress.DateUpdated,
	 tblOrderAddress.ServiceOrderNumber,
	 tblOrderAddress.FIRST_ORDER_CREATE_DT,
	 tblOrderAddress.OPE_LAST_MODIFY_DATE,
	 tblOrderAddress.PL_LAST_MODIFY_DATE,
	 tblOrderAddress.PS_LAST_MODIFY_DATE,
	 tblOrderAddress.TotalProcessingTimeInTickString,
	 tblOrderAddress.TotalProcessingTimeAsHumanReadable
FROM tblOrderAddress
WHERE (
(tblOrderAddress.OrderSystemOfRecordID=@OrderSystemOfRecordID) AND 
(tblOrderAddress.CDWCustomerOrderNumber=@CDWCustomerOrderNumber) AND 
(tblOrderAddress.CDWAddressOne=@CDWAddressOne) AND 
(tblOrderAddress.CDWCity=@CDWCity) AND 
(tblOrderAddress.CDWState=@CDWState) AND 
(tblOrderAddress.CDWPostalCode=@CDWPostalCode) AND 
(tblOrderAddress.CDWCountry=@CDWCountry) AND 
(tblOrderAddress.CDWFloor=@CDWFloor) AND 
(tblOrderAddress.CDWRoom=@CDWRoom) AND 
(tblOrderAddress.CDWSuite=@CDWSuite)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelByIDENT_OrderAddressID_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelByIDENT_OrderAddressID_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelByIDENT_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_SelByIDENT_OrderAddressID_EQ
(@OrderAddressID int)
AS
SET NOCOUNT  ON
SELECT 
	   CONVERT(VARCHAR(MAX), HASHBYTES('MD5', COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderAddressID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderAddressTypeID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.MigrationStatusID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderSystemOfRecordID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCustomerOrderNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWAddressOne), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCity), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWState), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWPostalCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCountry), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWFloor), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWRoom), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWSuite), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCLII), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ValidCLII), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInGLMAsSite), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMPLNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSiteCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.HasGLMSiteCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInSAPAsSiteAddress), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfRecordsInSAPWithPL), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSLNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInGLMAsServiceLocation), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.HasGLMSCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInSAPAsServiceLocationAddress), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfRecordsInSAPWithSL), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.SourceCreationDate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.SourceLastModifyDate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateTimeOfLastMigrationStatusUpdate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateTimeOfLastDupDetection), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateCreated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateUpdated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ServiceOrderNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.FIRST_ORDER_CREATE_DT), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OPE_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.PL_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.PS_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.TotalProcessingTimeInTickString), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.TotalProcessingTimeAsHumanReadable), ''))) AS DataHash,
	tblOrderAddress.OrderAddressID,
	 tblOrderAddress.OrderAddressTypeID,
	 tblOrderAddress.MigrationStatusID,
	 tblOrderAddress.OrderSystemOfRecordID,
	 tblOrderAddress.CDWCustomerOrderNumber,
	 tblOrderAddress.CDWAddressOne,
	 tblOrderAddress.CDWCity,
	 tblOrderAddress.CDWState,
	 tblOrderAddress.CDWPostalCode,
	 tblOrderAddress.CDWCountry,
	 tblOrderAddress.CDWFloor,
	 tblOrderAddress.CDWRoom,
	 tblOrderAddress.CDWSuite,
	 tblOrderAddress.CDWCLII,
	 tblOrderAddress.ValidCLII,
	 tblOrderAddress.NumberOfFailedGLMSiteCalls,
	 tblOrderAddress.ExistsInGLMAsSite,
	 tblOrderAddress.GLMPLNumber,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls,
	 tblOrderAddress.GLMSiteCode,
	 tblOrderAddress.HasGLMSiteCode,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsSiteAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithPL,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls,
	 tblOrderAddress.GLMSLNumber,
	 tblOrderAddress.ExistsInGLMAsServiceLocation,
	 tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls,
	 tblOrderAddress.GLMSCode,
	 tblOrderAddress.HasGLMSCode,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsServiceLocationAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithSL,
	 tblOrderAddress.SourceCreationDate,
	 tblOrderAddress.SourceLastModifyDate,
	 tblOrderAddress.DateTimeOfLastMigrationStatusUpdate,
	 tblOrderAddress.DateTimeOfLastDupDetection,
	 tblOrderAddress.DateCreated,
	 tblOrderAddress.DateUpdated,
	 tblOrderAddress.ServiceOrderNumber,
	 tblOrderAddress.FIRST_ORDER_CREATE_DT,
	 tblOrderAddress.OPE_LAST_MODIFY_DATE,
	 tblOrderAddress.PL_LAST_MODIFY_DATE,
	 tblOrderAddress.PS_LAST_MODIFY_DATE,
	 tblOrderAddress.TotalProcessingTimeInTickString,
	 tblOrderAddress.TotalProcessingTimeAsHumanReadable
FROM tblOrderAddress
WHERE (
(tblOrderAddress.OrderAddressID=@OrderAddressID)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_UpdateOptimistic' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_UpdateOptimistic];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_UpdateOptimistic
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_UpdateOptimistic
(@DataHash varchar(max), @OrderAddressID int, @OrderAddressTypeID int, @MigrationStatusID int, @OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250), @CDWCLII varchar(50), @ValidCLII bit, @NumberOfFailedGLMSiteCalls int, @ExistsInGLMAsSite bit, @GLMPLNumber varchar(50), @NumberOfFailedGLMSiteCodeExistenceCalls int, @NumberOfFailedGLMSiteCodeCreationCalls int, @GLMSiteCode varchar(50), @HasGLMSiteCode bit, @NumberOfFailedSAPSiteAddressSearchCalls int, @NumberOfFailedSAPSiteAddressImportCalls int, @ExistsInSAPAsSiteAddress bit, @NumberOfRecordsInSAPWithPL int, @NumberOfFailedGLMServiceLocationSearchCalls int, @NumberOfFailedGLMServiceLocationCreationCalls int, @GLMSLNumber varchar(50), @ExistsInGLMAsServiceLocation bit, @NumberOfFailedGLMSCodeExistenceCalls int, @NumberOfFailedGLMSCodeCreationCalls int, @GLMSCode varchar(50), @HasGLMSCode bit, @NumberOfFailedSAPServiceLocationAddressSearchCalls int, @NumberOfFailedSAPServiceLocationAddressImportCalls int, @ExistsInSAPAsServiceLocationAddress bit, @NumberOfRecordsInSAPWithSL int, @SourceCreationDate datetime, @SourceLastModifyDate datetime, @DateTimeOfLastMigrationStatusUpdate datetime, @DateTimeOfLastDupDetection datetime, @DateCreated datetime, @DateUpdated datetime, @ServiceOrderNumber varchar(100), @FIRST_ORDER_CREATE_DT datetime, @OPE_LAST_MODIFY_DATE datetime, @PL_LAST_MODIFY_DATE datetime, @PS_LAST_MODIFY_DATE datetime, @TotalProcessingTimeInTickString varchar(200), @TotalProcessingTimeAsHumanReadable varchar(200), @UpdateResult int OUT)
AS
SET NOCOUNT  ON
DECLARE @Result int
SET @Result = 0

DECLARE @NewDataHash varchar(max)
SET @NewDataHash =
   (SELECT
      CONVERT(VARCHAR(MAX), HASHBYTES('MD5', COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderAddressID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderAddressTypeID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.MigrationStatusID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OrderSystemOfRecordID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCustomerOrderNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWAddressOne), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCity), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWState), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWPostalCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCountry), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWFloor), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWRoom), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWSuite), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.CDWCLII), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ValidCLII), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInGLMAsSite), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMPLNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSiteCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.HasGLMSiteCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInSAPAsSiteAddress), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfRecordsInSAPWithPL), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSLNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInGLMAsServiceLocation), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.GLMSCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.HasGLMSCode), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ExistsInSAPAsServiceLocationAddress), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.NumberOfRecordsInSAPWithSL), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.SourceCreationDate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.SourceLastModifyDate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateTimeOfLastMigrationStatusUpdate), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateTimeOfLastDupDetection), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateCreated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.DateUpdated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.ServiceOrderNumber), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.FIRST_ORDER_CREATE_DT), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.OPE_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.PL_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.PS_LAST_MODIFY_DATE), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.TotalProcessingTimeInTickString), '') + COALESCE(CONVERT(VARCHAR(MAX), tblOrderAddress.TotalProcessingTimeAsHumanReadable), '')))
    FROM tblOrderAddress WHERE tblOrderAddress.OrderAddressID=@OrderAddressID)

PRINT 'HashCreated: ' + @NewDataHash

PRINT 'Attempting to update record by IDENTITY value AND data hash value...'
UPDATE tblOrderAddress
SET OrderAddressTypeID=@OrderAddressTypeID, MigrationStatusID=@MigrationStatusID, OrderSystemOfRecordID=@OrderSystemOfRecordID, CDWCustomerOrderNumber=@CDWCustomerOrderNumber, CDWAddressOne=@CDWAddressOne, CDWCity=@CDWCity, CDWState=@CDWState, CDWPostalCode=@CDWPostalCode, CDWCountry=@CDWCountry, CDWFloor=@CDWFloor, CDWRoom=@CDWRoom, CDWSuite=@CDWSuite, CDWCLII=@CDWCLII, ValidCLII=@ValidCLII, NumberOfFailedGLMSiteCalls=@NumberOfFailedGLMSiteCalls, ExistsInGLMAsSite=@ExistsInGLMAsSite, GLMPLNumber=@GLMPLNumber, NumberOfFailedGLMSiteCodeExistenceCalls=@NumberOfFailedGLMSiteCodeExistenceCalls, NumberOfFailedGLMSiteCodeCreationCalls=@NumberOfFailedGLMSiteCodeCreationCalls, GLMSiteCode=@GLMSiteCode, HasGLMSiteCode=@HasGLMSiteCode, NumberOfFailedSAPSiteAddressSearchCalls=@NumberOfFailedSAPSiteAddressSearchCalls, NumberOfFailedSAPSiteAddressImportCalls=@NumberOfFailedSAPSiteAddressImportCalls, ExistsInSAPAsSiteAddress=@ExistsInSAPAsSiteAddress, NumberOfRecordsInSAPWithPL=@NumberOfRecordsInSAPWithPL, NumberOfFailedGLMServiceLocationSearchCalls=@NumberOfFailedGLMServiceLocationSearchCalls, NumberOfFailedGLMServiceLocationCreationCalls=@NumberOfFailedGLMServiceLocationCreationCalls, GLMSLNumber=@GLMSLNumber, ExistsInGLMAsServiceLocation=@ExistsInGLMAsServiceLocation, NumberOfFailedGLMSCodeExistenceCalls=@NumberOfFailedGLMSCodeExistenceCalls, NumberOfFailedGLMSCodeCreationCalls=@NumberOfFailedGLMSCodeCreationCalls, GLMSCode=@GLMSCode, HasGLMSCode=@HasGLMSCode, NumberOfFailedSAPServiceLocationAddressSearchCalls=@NumberOfFailedSAPServiceLocationAddressSearchCalls, NumberOfFailedSAPServiceLocationAddressImportCalls=@NumberOfFailedSAPServiceLocationAddressImportCalls, ExistsInSAPAsServiceLocationAddress=@ExistsInSAPAsServiceLocationAddress, NumberOfRecordsInSAPWithSL=@NumberOfRecordsInSAPWithSL, SourceCreationDate=@SourceCreationDate, SourceLastModifyDate=@SourceLastModifyDate, DateTimeOfLastMigrationStatusUpdate=@DateTimeOfLastMigrationStatusUpdate, DateTimeOfLastDupDetection=@DateTimeOfLastDupDetection, DateCreated=@DateCreated, DateUpdated=@DateUpdated, ServiceOrderNumber=@ServiceOrderNumber, FIRST_ORDER_CREATE_DT=@FIRST_ORDER_CREATE_DT, OPE_LAST_MODIFY_DATE=@OPE_LAST_MODIFY_DATE, PL_LAST_MODIFY_DATE=@PL_LAST_MODIFY_DATE, PS_LAST_MODIFY_DATE=@PS_LAST_MODIFY_DATE, TotalProcessingTimeInTickString=@TotalProcessingTimeInTickString, TotalProcessingTimeAsHumanReadable=@TotalProcessingTimeAsHumanReadable
WHERE tblOrderAddress.OrderAddressID=@OrderAddressID AND @DataHash=@NewDataHash

SET @Result = @@ROWCOUNT 
IF @Result = 0
	BEGIN
		PRINT 'Record could not be updated(@@ROWCOUNT=0); checking to see if record exists...'
		SELECT 1 FROM tblOrderAddress WHERE tblOrderAddress.OrderAddressID=@OrderAddressID
		SET @Result = @@ROWCOUNT

		IF @Result = 1
			BEGIN
				SET @Result = -1
				PRINT 'Record exists, so datahash must be different ==> OPTIMISTIC CONCURRENCY COLLISION.'
				PRINT 'Returning -1.'
			END
		ELSE
			PRINT 'Record no longer exists.'
	END
ELSE
	PRINT 'Record was succesfully updated.'

SET @UpdateResult = @Result

PRINT '@Result: ' + CAST(@Result AS VARCHAR(2))
RETURN @Result
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_Update' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_Update];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_Update
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_Update
(@OrderAddressID int, @OrderAddressTypeID int, @MigrationStatusID int, @OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250), @CDWCLII varchar(50), @ValidCLII bit, @NumberOfFailedGLMSiteCalls int, @ExistsInGLMAsSite bit, @GLMPLNumber varchar(50), @NumberOfFailedGLMSiteCodeExistenceCalls int, @NumberOfFailedGLMSiteCodeCreationCalls int, @GLMSiteCode varchar(50), @HasGLMSiteCode bit, @NumberOfFailedSAPSiteAddressSearchCalls int, @NumberOfFailedSAPSiteAddressImportCalls int, @ExistsInSAPAsSiteAddress bit, @NumberOfRecordsInSAPWithPL int, @NumberOfFailedGLMServiceLocationSearchCalls int, @NumberOfFailedGLMServiceLocationCreationCalls int, @GLMSLNumber varchar(50), @ExistsInGLMAsServiceLocation bit, @NumberOfFailedGLMSCodeExistenceCalls int, @NumberOfFailedGLMSCodeCreationCalls int, @GLMSCode varchar(50), @HasGLMSCode bit, @NumberOfFailedSAPServiceLocationAddressSearchCalls int, @NumberOfFailedSAPServiceLocationAddressImportCalls int, @ExistsInSAPAsServiceLocationAddress bit, @NumberOfRecordsInSAPWithSL int, @SourceCreationDate datetime, @SourceLastModifyDate datetime, @DateTimeOfLastMigrationStatusUpdate datetime, @DateTimeOfLastDupDetection datetime, @DateCreated datetime, @DateUpdated datetime, @ServiceOrderNumber varchar(100), @FIRST_ORDER_CREATE_DT datetime, @OPE_LAST_MODIFY_DATE datetime, @PL_LAST_MODIFY_DATE datetime, @PS_LAST_MODIFY_DATE datetime, @TotalProcessingTimeInTickString varchar(200), @TotalProcessingTimeAsHumanReadable varchar(200))
AS
SET NOCOUNT  OFF
UPDATE tblOrderAddress
SET OrderAddressTypeID=@OrderAddressTypeID, MigrationStatusID=@MigrationStatusID, OrderSystemOfRecordID=@OrderSystemOfRecordID, CDWCustomerOrderNumber=@CDWCustomerOrderNumber, CDWAddressOne=@CDWAddressOne, CDWCity=@CDWCity, CDWState=@CDWState, CDWPostalCode=@CDWPostalCode, CDWCountry=@CDWCountry, CDWFloor=@CDWFloor, CDWRoom=@CDWRoom, CDWSuite=@CDWSuite, CDWCLII=@CDWCLII, ValidCLII=@ValidCLII, NumberOfFailedGLMSiteCalls=@NumberOfFailedGLMSiteCalls, ExistsInGLMAsSite=@ExistsInGLMAsSite, GLMPLNumber=@GLMPLNumber, NumberOfFailedGLMSiteCodeExistenceCalls=@NumberOfFailedGLMSiteCodeExistenceCalls, NumberOfFailedGLMSiteCodeCreationCalls=@NumberOfFailedGLMSiteCodeCreationCalls, GLMSiteCode=@GLMSiteCode, HasGLMSiteCode=@HasGLMSiteCode, NumberOfFailedSAPSiteAddressSearchCalls=@NumberOfFailedSAPSiteAddressSearchCalls, NumberOfFailedSAPSiteAddressImportCalls=@NumberOfFailedSAPSiteAddressImportCalls, ExistsInSAPAsSiteAddress=@ExistsInSAPAsSiteAddress, NumberOfRecordsInSAPWithPL=@NumberOfRecordsInSAPWithPL, NumberOfFailedGLMServiceLocationSearchCalls=@NumberOfFailedGLMServiceLocationSearchCalls, NumberOfFailedGLMServiceLocationCreationCalls=@NumberOfFailedGLMServiceLocationCreationCalls, GLMSLNumber=@GLMSLNumber, ExistsInGLMAsServiceLocation=@ExistsInGLMAsServiceLocation, NumberOfFailedGLMSCodeExistenceCalls=@NumberOfFailedGLMSCodeExistenceCalls, NumberOfFailedGLMSCodeCreationCalls=@NumberOfFailedGLMSCodeCreationCalls, GLMSCode=@GLMSCode, HasGLMSCode=@HasGLMSCode, NumberOfFailedSAPServiceLocationAddressSearchCalls=@NumberOfFailedSAPServiceLocationAddressSearchCalls, NumberOfFailedSAPServiceLocationAddressImportCalls=@NumberOfFailedSAPServiceLocationAddressImportCalls, ExistsInSAPAsServiceLocationAddress=@ExistsInSAPAsServiceLocationAddress, NumberOfRecordsInSAPWithSL=@NumberOfRecordsInSAPWithSL, SourceCreationDate=@SourceCreationDate, SourceLastModifyDate=@SourceLastModifyDate, DateTimeOfLastMigrationStatusUpdate=@DateTimeOfLastMigrationStatusUpdate, DateTimeOfLastDupDetection=@DateTimeOfLastDupDetection, DateCreated=@DateCreated, DateUpdated=@DateUpdated, ServiceOrderNumber=@ServiceOrderNumber, FIRST_ORDER_CREATE_DT=@FIRST_ORDER_CREATE_DT, OPE_LAST_MODIFY_DATE=@OPE_LAST_MODIFY_DATE, PL_LAST_MODIFY_DATE=@PL_LAST_MODIFY_DATE, PS_LAST_MODIFY_DATE=@PS_LAST_MODIFY_DATE, TotalProcessingTimeInTickString=@TotalProcessingTimeInTickString, TotalProcessingTimeAsHumanReadable=@TotalProcessingTimeAsHumanReadable
WHERE tblOrderAddress.OrderAddressID=@OrderAddressID
GO -- Execute







IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelByDyn_H1_Custom' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelByDyn_H1_Custom];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelByDyn_H1_Custom
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelByDyn_H1_Custom]
(@DynamicWhereClause varchar(max),@DynamicOrderByClause varchar(max), @RowsPerPage int, @PageNumber int)
AS
SET NOCOUNT  ON
DECLARE @SQL NVARCHAR(max)
DECLARE @InnerWhereClauseStatement NVARCHAR(max)
IF(LEN(@DynamicWhereClause) > 0)
SET @InnerWhereClauseStatement = ' WHERE (' + @DynamicWhereClause + ')'
ELSE
SET @InnerWhereClauseStatement = ''
DECLARE @InnerOrderByClauseStatement NVARCHAR(max)
 IF(LEN(@DynamicOrderByClause) > 0)
SET @InnerOrderByClauseStatement = '(ORDER BY ' + @DynamicOrderByClause + ' )'
ELSE
SET @InnerOrderByClauseStatement = '(ORDER BY (Select 1))'
SET @SQL = convert(nvarchar(max), N'') + N'SELECT 
	*
FROM (
	SELECT 
	'''' AS DataHash,
 	 tblOrderAddress.OrderAddressID as OrderAddressID,
	 tblOrderAddress.OrderAddressTypeID as OrderAddressTypeID,
	 tblOrderAddress.MigrationStatusID as MigrationStatusID,
	 tblOrderAddress.OrderSystemOfRecordID as OrderSystemOfRecordID,
	 tblOrderAddress.CDWCustomerOrderNumber as CDWCustomerOrderNumber,
	 tblOrderAddress.CDWAddressOne as CDWAddressOne,
	 tblOrderAddress.CDWCity as CDWCity,
	 tblOrderAddress.CDWState as CDWState,
	 tblOrderAddress.CDWPostalCode as CDWPostalCode,
	 tblOrderAddress.CDWCountry as CDWCountry,
	 tblOrderAddress.CDWFloor as CDWFloor,
	 tblOrderAddress.CDWRoom as CDWRoom,
	 tblOrderAddress.CDWSuite as CDWSuite,
	 tblOrderAddress.CDWCLII as CDWCLII,
	 tblOrderAddress.ValidCLII as ValidCLII,
	 tblOrderAddress.NumberOfFailedGLMSiteCalls as NumberOfFailedGLMSiteCalls,
	 tblOrderAddress.ExistsInGLMAsSite as ExistsInGLMAsSite,
	 tblOrderAddress.GLMPLNumber as GLMPLNumber,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls as NumberOfFailedGLMSiteCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls as NumberOfFailedGLMSiteCodeCreationCalls,
	 tblOrderAddress.GLMSiteCode as GLMSiteCode,
	 tblOrderAddress.HasGLMSiteCode as HasGLMSiteCode,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls as NumberOfFailedSAPSiteAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls as NumberOfFailedSAPSiteAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsSiteAddress as ExistsInSAPAsSiteAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithPL as NumberOfRecordsInSAPWithPL,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls as NumberOfFailedGLMServiceLocationSearchCalls,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls as NumberOfFailedGLMServiceLocationCreationCalls,
	 tblOrderAddress.GLMSLNumber as GLMSLNumber,
	 tblOrderAddress.ExistsInGLMAsServiceLocation as ExistsInGLMAsServiceLocation,
	 tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls as NumberOfFailedGLMSCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls as NumberOfFailedGLMSCodeCreationCalls,
	 tblOrderAddress.GLMSCode as GLMSCode,
	 tblOrderAddress.HasGLMSCode as HasGLMSCode,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls as NumberOfFailedSAPServiceLocationAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls as NumberOfFailedSAPServiceLocationAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsServiceLocationAddress as ExistsInSAPAsServiceLocationAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithSL as NumberOfRecordsInSAPWithSL,
	 tblOrderAddress.SourceCreationDate as SourceCreationDate,
	 tblOrderAddress.SourceLastModifyDate as SourceLastModifyDate,
	 tblOrderAddress.DateTimeOfLastMigrationStatusUpdate as DateTimeOfLastMigrationStatusUpdate,
	 tblOrderAddress.DateTimeOfLastDupDetection as DateTimeOfLastDupDetection,
	 tblOrderAddress.DateCreated as DateCreated,
	 tblOrderAddress.DateUpdated as DateUpdated,
	 tblOrderAddress.ServiceOrderNumber as ServiceOrderNumber,
	 tblOrderAddress.FIRST_ORDER_CREATE_DT as FIRST_ORDER_CREATE_DT,
	 tblOrderAddress.OPE_LAST_MODIFY_DATE as OPE_LAST_MODIFY_DATE,
	 tblOrderAddress.PL_LAST_MODIFY_DATE as PL_LAST_MODIFY_DATE,
	 tblOrderAddress.PS_LAST_MODIFY_DATE as PS_LAST_MODIFY_DATE,
	 tblOrderAddress.TotalProcessingTimeInTickString as TotalProcessingTimeInTickString,
	 tblOrderAddress.TotalProcessingTimeAsHumanReadable as TotalProcessingTimeAsHumanReadable,

	 tblMigrationStatusH1.MigrationStatusID as MigrationStatusID_H1,
	 tblMigrationStatusH1.MigrationStatusDesc as MigrationStatusDesc,
	 tblMigrationStatusH1.MigrationStatusExtendedDescription as MigrationStatusExtendedDescription,

	 tblOrderAddressTypeH1.OrderAddressTypeID as OrderAddressTypeID_H1,
	 tblOrderAddressTypeH1.OrderAddressTypeDesc as OrderAddressTypeDesc,
	 tblOrderSystemOfRecordH1.OrderSystemOfRecordID as OrderSystemOfRecordID_H1,
	 tblOrderSystemOfRecordH1.OrderSystemOfRecordDesc as OrderSystemOfRecordDesc
	,ROW_NUMBER() OVER' +  @InnerOrderByClauseStatement +  ' AS RowNum
	FROM 

tblOrderAddress
 LEFT JOIN tblMigrationStatus tblMigrationStatusH1 ON tblOrderAddress.MigrationStatusID = tblMigrationStatusH1.MigrationStatusID 
 LEFT JOIN tblOrderAddressType tblOrderAddressTypeH1 ON tblOrderAddress.OrderAddressTypeID = tblOrderAddressTypeH1.OrderAddressTypeID 
 LEFT JOIN tblOrderSystemOfRecord tblOrderSystemOfRecordH1 ON tblOrderAddress.OrderSystemOfRecordID = tblOrderSystemOfRecordH1.OrderSystemOfRecordID
	' + @InnerWhereClauseStatement + ') AS ResultSet 
	WHERE ResultSet.RowNum BETWEEN ((' + CONVERT(varchar(MAX), @PageNumber) + '-1)*' + CONVERT(varchar(MAX), @RowsPerPage) +')+1 AND '+ CONVERT(varchar(MAX), @RowsPerPage) +'*(' + CONVERT(varchar(MAX), @PageNumber) + ')' 

PRINT @SQL;
EXECUTE sp_executesql @SQL

GO


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelByDyn_H1_Custom_Ct' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelByDyn_H1_Custom_Ct];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelByDyn_H1_Custom_Ct
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelByDyn_H1_Custom_Ct]
(@DynamicWhereClause varchar(max))
AS
SET NOCOUNT  ON
DECLARE @SQL NVARCHAR(max)
DECLARE @InnerWhereClauseStatement NVARCHAR(max)
IF(LEN(@DynamicWhereClause) > 0)
SET @InnerWhereClauseStatement = ' WHERE (' + @DynamicWhereClause + ')'
ELSE
SET @InnerWhereClauseStatement = ''
SET @SQL = convert(nvarchar(max), N'') + N'SELECT 
	Count(*)
FROM 
	tblOrderAddress
	LEFT JOIN tblMigrationStatus tblMigrationStatusH1 ON tblOrderAddress.MigrationStatusID = tblMigrationStatusH1.MigrationStatusID 
	LEFT JOIN tblOrderAddressType tblOrderAddressTypeH1 ON tblOrderAddress.OrderAddressTypeID = tblOrderAddressTypeH1.OrderAddressTypeID 
	LEFT JOIN tblOrderSystemOfRecord tblOrderSystemOfRecordH1 ON tblOrderAddress.OrderSystemOfRecordID = tblOrderSystemOfRecordH1.OrderSystemOfRecordID
	' + @InnerWhereClauseStatement

PRINT @SQL;
EXECUTE sp_executesql @SQL

GO


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ
(@OrderAddressID int)
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddressLogItem.OrderAddressLogItemID,
	 tblOrderAddressLogItem.OrderAddressID,
	 tblOrderAddressLogItem.MigrationStatusID,
	 tblOrderAddressLogItem.LogMessage,
	 tblOrderAddressLogItem.DateCreated,
	 tblOrderAddressLogItem.DateUpdated,
	 tblMigrationStatusH1.MigrationStatusID,
	 tblMigrationStatusH1.MigrationStatusDesc,
	 tblMigrationStatusH1.MigrationStatusExtendedDescription,
	 tblOrderAddressH1.OrderAddressID
FROM 
tblOrderAddressLogItem
 LEFT JOIN tblMigrationStatus tblMigrationStatusH1 ON tblOrderAddressLogItem.MigrationStatusID = tblMigrationStatusH1.MigrationStatusID 
 LEFT JOIN tblOrderAddress tblOrderAddressH1 ON tblOrderAddressLogItem.OrderAddressID = tblOrderAddressH1.OrderAddressID
WHERE (
(tblOrderAddressLogItem.OrderAddressID=@OrderAddressID)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddressLogItem_Insert' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddressLogItem_Insert];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddressLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddressLogItem_Insert
(@OrderAddressID int, @MigrationStatusID int, @LogMessage varchar(2000), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblOrderAddressLogItem
(OrderAddressID, MigrationStatusID, LogMessage, DateCreated, DateUpdated)
VALUES (@OrderAddressID, @MigrationStatusID, @LogMessage, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblMigrationStatus_SelByPK_MigrationStatusID_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblMigrationStatus_SelByPK_MigrationStatusID_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblMigrationStatus_SelByPK_MigrationStatusID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblMigrationStatus_SelByPK_MigrationStatusID_EQ
(@MigrationStatusID int)
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblMigrationStatus.MigrationStatusID,
	 tblMigrationStatus.MigrationStatusDesc,
	 tblMigrationStatus.MigrationStatusExtendedDescription,
	 tblMigrationStatus.DateCreated,
	 tblMigrationStatus.DateUpdated
FROM tblMigrationStatus
WHERE (
(tblMigrationStatus.MigrationStatusID=@MigrationStatusID)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblMigrationStatus_SelAll' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblMigrationStatus_SelAll];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblMigrationStatus_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblMigrationStatus_SelAll

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblMigrationStatus.MigrationStatusID,
	 tblMigrationStatus.MigrationStatusDesc,
	 tblMigrationStatus.MigrationStatusExtendedDescription,
	 tblMigrationStatus.DateCreated,
	 tblMigrationStatus.DateUpdated
FROM tblMigrationStatus
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderSystemOfRecord_SelAll' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderSystemOfRecord_SelAll];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderSystemOfRecord_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderSystemOfRecord_SelAll

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderSystemOfRecord.OrderSystemOfRecordID,
	 tblOrderSystemOfRecord.OrderSystemOfRecordDesc,
	 tblOrderSystemOfRecord.DateCreated,
	 tblOrderSystemOfRecord.DateUpdated
FROM tblOrderSystemOfRecord
GO -- Execute


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddressType_SelAll' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddressType_SelAll];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddressType_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddressType_SelAll

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddressType.OrderAddressTypeID,
	 tblOrderAddressType.OrderAddressTypeDesc,
	 tblOrderAddressType.DateCreated,
	 tblOrderAddressType.DateUpdated
FROM tblOrderAddressType
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblSystemConfigItem_SelAll' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblSystemConfigItem_SelAll];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblSystemConfigItem_SelAll

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblSystemConfigItem.SystemConfigItemID,
	 tblSystemConfigItem.ConfigSettingName,
	 tblSystemConfigItem.ConfigSettingValue,
	 tblSystemConfigItem.DateCreated,
	 tblSystemConfigItem.DateUpdated
FROM tblSystemConfigItem
GO -- Execute


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ
-- Author:      Muir, Brendan
-- Company:     Innovative Info Systems, LLC.
-- Project:     AddressManagement
-- Description:
-- This stored procedure was created by a tool.  !!! DO NOT MODIFY!!!
-- =====================================================================
CREATE PROCEDURE tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ
(@SystemConfigItemID int)
AS
SET NOCOUNT  ON
SELECT 
	   CONVERT(VARCHAR(MAX), HASHBYTES('MD5', COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.SystemConfigItemID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.ConfigSettingName), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.ConfigSettingValue), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.DateCreated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.DateUpdated), ''))) AS DataHash,
	tblSystemConfigItem.SystemConfigItemID,
	 tblSystemConfigItem.ConfigSettingName,
	 tblSystemConfigItem.ConfigSettingValue,
	 tblSystemConfigItem.DateCreated,
	 tblSystemConfigItem.DateUpdated
FROM tblSystemConfigItem
WHERE (
(tblSystemConfigItem.SystemConfigItemID=@SystemConfigItemID)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblSystemConfigItem_UpdateOptimistic' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblSystemConfigItem_UpdateOptimistic];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_UpdateOptimistic
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblSystemConfigItem_UpdateOptimistic
(@DataHash varchar(max), @SystemConfigItemID int, @ConfigSettingName varchar(50), @ConfigSettingValue varchar(2000), @DateCreated datetime, @DateUpdated datetime, @UpdateResult int OUT)
AS
SET NOCOUNT  ON
DECLARE @Result int
SET @Result = 0

DECLARE @NewDataHash varchar(max)
SET @NewDataHash =
   (SELECT
      CONVERT(VARCHAR(MAX), HASHBYTES('MD5', COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.SystemConfigItemID), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.ConfigSettingName), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.ConfigSettingValue), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.DateCreated), '') + COALESCE(CONVERT(VARCHAR(MAX), tblSystemConfigItem.DateUpdated), '')))
    FROM tblSystemConfigItem WHERE tblSystemConfigItem.SystemConfigItemID=@SystemConfigItemID)

PRINT 'HashCreated: ' + @NewDataHash

PRINT 'Attempting to update record by IDENTITY value AND data hash value...'
UPDATE tblSystemConfigItem
SET ConfigSettingName=@ConfigSettingName, ConfigSettingValue=@ConfigSettingValue, DateCreated=@DateCreated, DateUpdated=@DateUpdated
WHERE tblSystemConfigItem.SystemConfigItemID=@SystemConfigItemID AND @DataHash=@NewDataHash

SET @Result = @@ROWCOUNT 
IF @Result = 0
	BEGIN
		PRINT 'Record could not be updated(@@ROWCOUNT=0); checking to see if record exists...'
		SELECT 1 FROM tblSystemConfigItem WHERE tblSystemConfigItem.SystemConfigItemID=@SystemConfigItemID
		SET @Result = @@ROWCOUNT

		IF @Result = 1
			BEGIN
				SET @Result = -1
				PRINT 'Record exists, so datahash must be different ==> OPTIMISTIC CONCURRENCY COLLISION.'
				PRINT 'Returning -1.'
			END
		ELSE
			PRINT 'Record no longer exists.'
	END
ELSE
	PRINT 'Record was succesfully updated.'

SET @UpdateResult = @Result

PRINT '@Result: ' + CAST(@Result AS VARCHAR(2))
RETURN @Result
GO -- Execute


IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblSystemLogItem_SelBy_DateCreated_GToEQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblSystemLogItem_SelBy_DateCreated_GToEQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblSystemLogItem_SelBy_DateCreated_GToEQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblSystemLogItem_SelBy_DateCreated_GToEQ
(@DateCreated datetime)
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblSystemLogItem.SystemLogItemID,
	 tblSystemLogItem.Note,
	 tblSystemLogItem.DateCreated,
	 tblSystemLogItem.DateUpdated
FROM tblSystemLogItem
WHERE (
(tblSystemLogItem.DateCreated>=@DateCreated)
)
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblSystemLogItem_Insert' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblSystemLogItem_Insert];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblSystemLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblSystemLogItem_Insert
(@Note varchar(2000), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblSystemLogItem
(Note, DateCreated, DateUpdated)
VALUES (@Note, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];
GO -- Execute




IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblOrderAddress_SelAll' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblOrderAddress_SelAll];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblOrderAddress_SelAll

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddress.OrderAddressID,
	 tblOrderAddress.OrderAddressTypeID,
	 tblOrderAddress.MigrationStatusID,
	 tblOrderAddress.OrderSystemOfRecordID,
	 tblOrderAddress.CDWCustomerOrderNumber,
	 tblOrderAddress.CDWAddressOne,
	 tblOrderAddress.CDWCity,
	 tblOrderAddress.CDWState,
	 tblOrderAddress.CDWPostalCode,
	 tblOrderAddress.CDWCountry,
	 tblOrderAddress.CDWFloor,
	 tblOrderAddress.CDWRoom,
	 tblOrderAddress.CDWSuite,
	 tblOrderAddress.CDWCLII,
	 tblOrderAddress.ValidCLII,
	 tblOrderAddress.NumberOfFailedGLMSiteCalls,
	 tblOrderAddress.ExistsInGLMAsSite,
	 tblOrderAddress.GLMPLNumber,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls,
	 tblOrderAddress.GLMSiteCode,
	 tblOrderAddress.HasGLMSiteCode,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPSiteAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsSiteAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithPL,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls,
	 tblOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls,
	 tblOrderAddress.GLMSLNumber,
	 tblOrderAddress.ExistsInGLMAsServiceLocation,
	 tblOrderAddress.NumberOfFailedGLMSCodeExistenceCalls,
	 tblOrderAddress.NumberOfFailedGLMSCodeCreationCalls,
	 tblOrderAddress.GLMSCode,
	 tblOrderAddress.HasGLMSCode,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls,
	 tblOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls,
	 tblOrderAddress.ExistsInSAPAsServiceLocationAddress,
	 tblOrderAddress.NumberOfRecordsInSAPWithSL,
	 tblOrderAddress.SourceCreationDate,
	 tblOrderAddress.SourceLastModifyDate,
	 tblOrderAddress.DateTimeOfLastMigrationStatusUpdate,
	 tblOrderAddress.DateTimeOfLastDupDetection,
	 tblOrderAddress.DateCreated,
	 tblOrderAddress.DateUpdated,
	 tblOrderAddress.ServiceOrderNumber,
	 tblOrderAddress.FIRST_ORDER_CREATE_DT,
	 tblOrderAddress.OPE_LAST_MODIFY_DATE,
	 tblOrderAddress.PL_LAST_MODIFY_DATE,
	 tblOrderAddress.PS_LAST_MODIFY_DATE,
	 tblOrderAddress.TotalProcessingTimeInTickString,
	 tblOrderAddress.TotalProcessingTimeAsHumanReadable
FROM tblOrderAddress
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblAPICallLogItem_Insert' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblAPICallLogItem_Insert];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblAPICallLogItem_Insert
(@OrderAddressID int, @Host varchar(200), @FullUrl varchar(500), @ResponseCode varchar(50), @RunTimeHumanReadable varchar(50), @RunTimeInTicksString varchar(50), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblAPICallLogItem
(OrderAddressID, Host, FullUrl, ResponseCode, RunTimeHumanReadable, RunTimeInTicksString, DateCreated, DateUpdated)
VALUES (@OrderAddressID, @Host, @FullUrl, @ResponseCode, @RunTimeHumanReadable, @RunTimeInTicksString, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];
GO -- Execute



IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblAPICallLogItem_SelByFK_OrderAddressID_EQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblAPICallLogItem_SelByFK_OrderAddressID_EQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_SelByFK_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblAPICallLogItem_SelByFK_OrderAddressID_EQ
(@OrderAddressID int)
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblAPICallLogItem.APICallLogItemID,
	 tblAPICallLogItem.OrderAddressID,
	 tblAPICallLogItem.Host,
	 tblAPICallLogItem.FullUrl,
	 tblAPICallLogItem.ResponseCode,
	 tblAPICallLogItem.RunTimeHumanReadable,
	 tblAPICallLogItem.RunTimeInTicksString,
	 tblAPICallLogItem.DateCreated,
	 tblAPICallLogItem.DateUpdated
FROM tblAPICallLogItem
WHERE (
(tblAPICallLogItem.OrderAddressID=@OrderAddressID)
)
GO -- Execute

IF EXISTS (SELECT * FROM sysobjects WHERE name = 'tblAPICallLogItem_SelBy_DateCreated_GToEQ' AND user_name(uid) = 'dbo')
DROP PROCEDURE [dbo].[tblAPICallLogItem_SelBy_DateCreated_GToEQ];
GO -- Execute
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_SelBy_DateCreated_GToEQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE tblAPICallLogItem_SelBy_DateCreated_GToEQ
(@DateCreated datetime)
AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblAPICallLogItem.APICallLogItemID,
	 tblAPICallLogItem.OrderAddressID,
	 tblAPICallLogItem.Host,
	 tblAPICallLogItem.FullUrl,
	 tblAPICallLogItem.ResponseCode,
	 tblAPICallLogItem.RunTimeHumanReadable,
	 tblAPICallLogItem.RunTimeInTicksString,
	 tblAPICallLogItem.DateCreated,
	 tblAPICallLogItem.DateUpdated
FROM tblAPICallLogItem
WHERE (
(tblAPICallLogItem.DateCreated>=@DateCreated)
)
GO -- Execute