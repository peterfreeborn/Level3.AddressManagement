USE [AddressManagement]
GO
/****** Object:  StoredProcedure [dbo].[tblAPICallLogItem_Insert]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblAPICallLogItem_Insert]
(@OrderAddressID int, @Host varchar(200), @FullUrl varchar(500), @ResponseCode varchar(50), @RunTimeHumanReadable varchar(50), @RunTimeInTicksString varchar(50), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblAPICallLogItem
(OrderAddressID, Host, FullUrl, ResponseCode, RunTimeHumanReadable, RunTimeInTicksString, DateCreated, DateUpdated)
VALUES (@OrderAddressID, @Host, @FullUrl, @ResponseCode, @RunTimeHumanReadable, @RunTimeInTicksString, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

GO
/****** Object:  StoredProcedure [dbo].[tblAPICallLogItem_SelBy_DateCreated_GToEQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_SelBy_DateCreated_GToEQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblAPICallLogItem_SelBy_DateCreated_GToEQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblAPICallLogItem_SelByFK_OrderAddressID_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblAPICallLogItem_SelByFK_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblAPICallLogItem_SelByFK_OrderAddressID_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblMigrationStatus_SelAll]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblMigrationStatus_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblMigrationStatus_SelAll]

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

GO
/****** Object:  StoredProcedure [dbo].[tblMigrationStatus_SelByPK_MigrationStatusID_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblMigrationStatus_SelByPK_MigrationStatusID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblMigrationStatus_SelByPK_MigrationStatusID_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_Insert]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_Insert]
(@OrderAddressTypeID int, @MigrationStatusID int, @OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250), @CDWCLII varchar(50), @ValidCLII bit, @NumberOfFailedGLMSiteCalls int, @ExistsInGLMAsSite bit, @GLMPLNumber varchar(50), @NumberOfFailedGLMSiteCodeExistenceCalls int, @NumberOfFailedGLMSiteCodeCreationCalls int, @GLMSiteCode varchar(50), @HasGLMSiteCode bit, @NumberOfFailedSAPSiteAddressSearchCalls int, @NumberOfFailedSAPSiteAddressImportCalls int, @ExistsInSAPAsSiteAddress bit, @NumberOfRecordsInSAPWithPL int, @NumberOfFailedGLMServiceLocationSearchCalls int, @NumberOfFailedGLMServiceLocationCreationCalls int, @GLMSLNumber varchar(50), @ExistsInGLMAsServiceLocation bit, @NumberOfFailedGLMSCodeExistenceCalls int, @NumberOfFailedGLMSCodeCreationCalls int, @GLMSCode varchar(50), @HasGLMSCode bit, @NumberOfFailedSAPServiceLocationAddressSearchCalls int, @NumberOfFailedSAPServiceLocationAddressImportCalls int, @ExistsInSAPAsServiceLocationAddress bit, @NumberOfRecordsInSAPWithSL int, @SourceCreationDate datetime, @SourceLastModifyDate datetime, @DateTimeOfLastMigrationStatusUpdate datetime, @DateTimeOfLastDupDetection datetime, @DateCreated datetime, @DateUpdated datetime, @ServiceOrderNumber varchar(100), @FIRST_ORDER_CREATE_DT datetime, @OPE_LAST_MODIFY_DATE datetime, @PL_LAST_MODIFY_DATE datetime, @PS_LAST_MODIFY_DATE datetime, @TotalProcessingTimeInTickString varchar(200), @TotalProcessingTimeAsHumanReadable varchar(200))
AS
SET NOCOUNT  ON
INSERT INTO tblOrderAddress
(OrderAddressTypeID, MigrationStatusID, OrderSystemOfRecordID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState, CDWPostalCode, CDWCountry, CDWFloor, CDWRoom, CDWSuite, CDWCLII, ValidCLII, NumberOfFailedGLMSiteCalls, ExistsInGLMAsSite, GLMPLNumber, NumberOfFailedGLMSiteCodeExistenceCalls, NumberOfFailedGLMSiteCodeCreationCalls, GLMSiteCode, HasGLMSiteCode, NumberOfFailedSAPSiteAddressSearchCalls, NumberOfFailedSAPSiteAddressImportCalls, ExistsInSAPAsSiteAddress, NumberOfRecordsInSAPWithPL, NumberOfFailedGLMServiceLocationSearchCalls, NumberOfFailedGLMServiceLocationCreationCalls, GLMSLNumber, ExistsInGLMAsServiceLocation, NumberOfFailedGLMSCodeExistenceCalls, NumberOfFailedGLMSCodeCreationCalls, GLMSCode, HasGLMSCode, NumberOfFailedSAPServiceLocationAddressSearchCalls, NumberOfFailedSAPServiceLocationAddressImportCalls, ExistsInSAPAsServiceLocationAddress, NumberOfRecordsInSAPWithSL, SourceCreationDate, SourceLastModifyDate, DateTimeOfLastMigrationStatusUpdate, DateTimeOfLastDupDetection, DateCreated, DateUpdated, ServiceOrderNumber, FIRST_ORDER_CREATE_DT, OPE_LAST_MODIFY_DATE, PL_LAST_MODIFY_DATE, PS_LAST_MODIFY_DATE, TotalProcessingTimeInTickString, TotalProcessingTimeAsHumanReadable)
VALUES (@OrderAddressTypeID, @MigrationStatusID, @OrderSystemOfRecordID, @CDWCustomerOrderNumber, @CDWAddressOne, @CDWCity, @CDWState, @CDWPostalCode, @CDWCountry, @CDWFloor, @CDWRoom, @CDWSuite, @CDWCLII, @ValidCLII, @NumberOfFailedGLMSiteCalls, @ExistsInGLMAsSite, @GLMPLNumber, @NumberOfFailedGLMSiteCodeExistenceCalls, @NumberOfFailedGLMSiteCodeCreationCalls, @GLMSiteCode, @HasGLMSiteCode, @NumberOfFailedSAPSiteAddressSearchCalls, @NumberOfFailedSAPSiteAddressImportCalls, @ExistsInSAPAsSiteAddress, @NumberOfRecordsInSAPWithPL, @NumberOfFailedGLMServiceLocationSearchCalls, @NumberOfFailedGLMServiceLocationCreationCalls, @GLMSLNumber, @ExistsInGLMAsServiceLocation, @NumberOfFailedGLMSCodeExistenceCalls, @NumberOfFailedGLMSCodeCreationCalls, @GLMSCode, @HasGLMSCode, @NumberOfFailedSAPServiceLocationAddressSearchCalls, @NumberOfFailedSAPServiceLocationAddressImportCalls, @ExistsInSAPAsServiceLocationAddress, @NumberOfRecordsInSAPWithSL, @SourceCreationDate, @SourceLastModifyDate, @DateTimeOfLastMigrationStatusUpdate, @DateTimeOfLastDupDetection, @DateCreated, @DateUpdated, @ServiceOrderNumber, @FIRST_ORDER_CREATE_DT, @OPE_LAST_MODIFY_DATE, @PL_LAST_MODIFY_DATE, @PS_LAST_MODIFY_DATE, @TotalProcessingTimeInTickString, @TotalProcessingTimeAsHumanReadable)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelAll]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelAll]

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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelBy_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_CDWPostalCode_EQ_CDWCountry_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelBy_OrderSystemOfRecordID_EQ_CDWCustomerOrderNumber_EQ_CDWAddressOne_EQ_CDWCity_EQ_CDWState_EQ_C_TRUNC]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelByDyn_H1_Custom]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelByDyn_H1_Custom_Ct]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_SelByIDENT_OrderAddressID_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_SelByIDENT_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_SelByIDENT_OrderAddressID_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_Update]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_Update
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_Update]
(@OrderAddressID int, @OrderAddressTypeID int, @MigrationStatusID int, @OrderSystemOfRecordID int, @CDWCustomerOrderNumber varchar(500), @CDWAddressOne varchar(1000), @CDWCity varchar(500), @CDWState varchar(500), @CDWPostalCode varchar(50), @CDWCountry varchar(50), @CDWFloor varchar(250), @CDWRoom varchar(250), @CDWSuite varchar(250), @CDWCLII varchar(50), @ValidCLII bit, @NumberOfFailedGLMSiteCalls int, @ExistsInGLMAsSite bit, @GLMPLNumber varchar(50), @NumberOfFailedGLMSiteCodeExistenceCalls int, @NumberOfFailedGLMSiteCodeCreationCalls int, @GLMSiteCode varchar(50), @HasGLMSiteCode bit, @NumberOfFailedSAPSiteAddressSearchCalls int, @NumberOfFailedSAPSiteAddressImportCalls int, @ExistsInSAPAsSiteAddress bit, @NumberOfRecordsInSAPWithPL int, @NumberOfFailedGLMServiceLocationSearchCalls int, @NumberOfFailedGLMServiceLocationCreationCalls int, @GLMSLNumber varchar(50), @ExistsInGLMAsServiceLocation bit, @NumberOfFailedGLMSCodeExistenceCalls int, @NumberOfFailedGLMSCodeCreationCalls int, @GLMSCode varchar(50), @HasGLMSCode bit, @NumberOfFailedSAPServiceLocationAddressSearchCalls int, @NumberOfFailedSAPServiceLocationAddressImportCalls int, @ExistsInSAPAsServiceLocationAddress bit, @NumberOfRecordsInSAPWithSL int, @SourceCreationDate datetime, @SourceLastModifyDate datetime, @DateTimeOfLastMigrationStatusUpdate datetime, @DateTimeOfLastDupDetection datetime, @DateCreated datetime, @DateUpdated datetime, @ServiceOrderNumber varchar(100), @FIRST_ORDER_CREATE_DT datetime, @OPE_LAST_MODIFY_DATE datetime, @PL_LAST_MODIFY_DATE datetime, @PS_LAST_MODIFY_DATE datetime, @TotalProcessingTimeInTickString varchar(200), @TotalProcessingTimeAsHumanReadable varchar(200))
AS
SET NOCOUNT  OFF
UPDATE tblOrderAddress
SET OrderAddressTypeID=@OrderAddressTypeID, MigrationStatusID=@MigrationStatusID, OrderSystemOfRecordID=@OrderSystemOfRecordID, CDWCustomerOrderNumber=@CDWCustomerOrderNumber, CDWAddressOne=@CDWAddressOne, CDWCity=@CDWCity, CDWState=@CDWState, CDWPostalCode=@CDWPostalCode, CDWCountry=@CDWCountry, CDWFloor=@CDWFloor, CDWRoom=@CDWRoom, CDWSuite=@CDWSuite, CDWCLII=@CDWCLII, ValidCLII=@ValidCLII, NumberOfFailedGLMSiteCalls=@NumberOfFailedGLMSiteCalls, ExistsInGLMAsSite=@ExistsInGLMAsSite, GLMPLNumber=@GLMPLNumber, NumberOfFailedGLMSiteCodeExistenceCalls=@NumberOfFailedGLMSiteCodeExistenceCalls, NumberOfFailedGLMSiteCodeCreationCalls=@NumberOfFailedGLMSiteCodeCreationCalls, GLMSiteCode=@GLMSiteCode, HasGLMSiteCode=@HasGLMSiteCode, NumberOfFailedSAPSiteAddressSearchCalls=@NumberOfFailedSAPSiteAddressSearchCalls, NumberOfFailedSAPSiteAddressImportCalls=@NumberOfFailedSAPSiteAddressImportCalls, ExistsInSAPAsSiteAddress=@ExistsInSAPAsSiteAddress, NumberOfRecordsInSAPWithPL=@NumberOfRecordsInSAPWithPL, NumberOfFailedGLMServiceLocationSearchCalls=@NumberOfFailedGLMServiceLocationSearchCalls, NumberOfFailedGLMServiceLocationCreationCalls=@NumberOfFailedGLMServiceLocationCreationCalls, GLMSLNumber=@GLMSLNumber, ExistsInGLMAsServiceLocation=@ExistsInGLMAsServiceLocation, NumberOfFailedGLMSCodeExistenceCalls=@NumberOfFailedGLMSCodeExistenceCalls, NumberOfFailedGLMSCodeCreationCalls=@NumberOfFailedGLMSCodeCreationCalls, GLMSCode=@GLMSCode, HasGLMSCode=@HasGLMSCode, NumberOfFailedSAPServiceLocationAddressSearchCalls=@NumberOfFailedSAPServiceLocationAddressSearchCalls, NumberOfFailedSAPServiceLocationAddressImportCalls=@NumberOfFailedSAPServiceLocationAddressImportCalls, ExistsInSAPAsServiceLocationAddress=@ExistsInSAPAsServiceLocationAddress, NumberOfRecordsInSAPWithSL=@NumberOfRecordsInSAPWithSL, SourceCreationDate=@SourceCreationDate, SourceLastModifyDate=@SourceLastModifyDate, DateTimeOfLastMigrationStatusUpdate=@DateTimeOfLastMigrationStatusUpdate, DateTimeOfLastDupDetection=@DateTimeOfLastDupDetection, DateCreated=@DateCreated, DateUpdated=@DateUpdated, ServiceOrderNumber=@ServiceOrderNumber, FIRST_ORDER_CREATE_DT=@FIRST_ORDER_CREATE_DT, OPE_LAST_MODIFY_DATE=@OPE_LAST_MODIFY_DATE, PL_LAST_MODIFY_DATE=@PL_LAST_MODIFY_DATE, PS_LAST_MODIFY_DATE=@PS_LAST_MODIFY_DATE, TotalProcessingTimeInTickString=@TotalProcessingTimeInTickString, TotalProcessingTimeAsHumanReadable=@TotalProcessingTimeAsHumanReadable
WHERE tblOrderAddress.OrderAddressID=@OrderAddressID

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddress_UpdateOptimistic]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddress_UpdateOptimistic
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddress_UpdateOptimistic]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddressLogItem_Insert]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddressLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddressLogItem_Insert]
(@OrderAddressID int, @MigrationStatusID int, @LogMessage varchar(2000), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblOrderAddressLogItem
(OrderAddressID, MigrationStatusID, LogMessage, DateCreated, DateUpdated)
VALUES (@OrderAddressID, @MigrationStatusID, @LogMessage, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddressLogItem_SelByFK_H1_OrderAddressID_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblOrderAddressType_SelAll]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderAddressType_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderAddressType_SelAll]

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderAddressType.OrderAddressTypeID,
	 tblOrderAddressType.OrderAddressTypeDesc,
	 tblOrderAddressType.DateCreated,
	 tblOrderAddressType.DateUpdated
FROM tblOrderAddressType

GO
/****** Object:  StoredProcedure [dbo].[tblOrderSystemOfRecord_SelAll]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblOrderSystemOfRecord_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblOrderSystemOfRecord_SelAll]

AS
SET NOCOUNT  ON
SELECT 
	'' AS DataHash,
	tblOrderSystemOfRecord.OrderSystemOfRecordID,
	 tblOrderSystemOfRecord.OrderSystemOfRecordDesc,
	 tblOrderSystemOfRecord.DateCreated,
	 tblOrderSystemOfRecord.DateUpdated
FROM tblOrderSystemOfRecord

GO
/****** Object:  StoredProcedure [dbo].[tblSystemConfigItem_SelAll]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_SelAll
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblSystemConfigItem_SelAll]

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

GO
/****** Object:  StoredProcedure [dbo].[tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ
-- Author:      Muir, Brendan
-- Company:     Innovative Info Systems, LLC.
-- Project:     AddressManagement
-- Description:
-- This stored procedure was created by a tool.  !!! DO NOT MODIFY!!!
-- =====================================================================
CREATE PROCEDURE [dbo].[tblSystemConfigItem_SelByIDENT_SystemConfigItemID_EQ]
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

GO
/****** Object:  StoredProcedure [dbo].[tblSystemConfigItem_UpdateOptimistic]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblSystemConfigItem_UpdateOptimistic
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblSystemConfigItem_UpdateOptimistic]
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

GO
/****** Object:  StoredProcedure [dbo].[tblSystemLogItem_Insert]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblSystemLogItem_Insert
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblSystemLogItem_Insert]
(@Note varchar(2000), @DateCreated datetime, @DateUpdated datetime)
AS
SET NOCOUNT  ON
INSERT INTO tblSystemLogItem
(Note, DateCreated, DateUpdated)
VALUES (@Note, @DateCreated, @DateUpdated)
SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];

GO
/****** Object:  StoredProcedure [dbo].[tblSystemLogItem_SelBy_DateCreated_GToEQ]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================================
-- Proc Name:   tblSystemLogItem_SelBy_DateCreated_GToEQ
-- Author:      Muir, Brendan
-- Project:     AddressManagement
-- Description:
-- =====================================================================
CREATE PROCEDURE [dbo].[tblSystemLogItem_SelBy_DateCreated_GToEQ]
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

GO
/****** Object:  Table [dbo].[tblAPICallLogItem]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAPICallLogItem](
	[APICallLogItemID] [int] IDENTITY(1,1) NOT NULL,
	[OrderAddressID] [int] NULL,
	[Host] [varchar](200) NOT NULL,
	[FullUrl] [varchar](500) NOT NULL,
	[ResponseCode] [varchar](50) NULL,
	[RunTimeHumanReadable] [varchar](50) NULL,
	[RunTimeInTicksString] [varchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblAPICallLogItem] PRIMARY KEY CLUSTERED 
(
	[APICallLogItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMigrationStatus]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMigrationStatus](
	[MigrationStatusID] [int] IDENTITY(1,1) NOT NULL,
	[MigrationStatusDesc] [varchar](150) NOT NULL,
	[MigrationStatusExtendedDescription] [varchar](1000) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblMigrationStatus] PRIMARY KEY CLUSTERED 
(
	[MigrationStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderAddress]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderAddress](
	[OrderAddressID] [int] IDENTITY(1,1) NOT NULL,
	[OrderAddressTypeID] [int] NOT NULL,
	[MigrationStatusID] [int] NOT NULL,
	[OrderSystemOfRecordID] [int] NOT NULL,
	[CDWCustomerOrderNumber] [varchar](500) NOT NULL,
	[CDWAddressOne] [varchar](1000) NULL,
	[CDWCity] [varchar](500) NULL,
	[CDWState] [varchar](500) NULL,
	[CDWPostalCode] [varchar](50) NULL,
	[CDWCountry] [varchar](50) NULL,
	[CDWFloor] [varchar](250) NOT NULL,
	[CDWRoom] [varchar](250) NOT NULL,
	[CDWSuite] [varchar](250) NOT NULL,
	[CDWCLII] [varchar](50) NULL,
	[ValidCLII] [bit] NOT NULL,
	[NumberOfFailedGLMSiteCalls] [int] NOT NULL,
	[ExistsInGLMAsSite] [bit] NOT NULL,
	[GLMPLNumber] [varchar](50) NULL,
	[NumberOfFailedGLMSiteCodeExistenceCalls] [int] NOT NULL,
	[NumberOfFailedGLMSiteCodeCreationCalls] [int] NOT NULL,
	[GLMSiteCode] [varchar](50) NULL,
	[HasGLMSiteCode] [bit] NOT NULL,
	[NumberOfFailedSAPSiteAddressSearchCalls] [int] NOT NULL,
	[NumberOfFailedSAPSiteAddressImportCalls] [int] NOT NULL,
	[ExistsInSAPAsSiteAddress] [bit] NOT NULL,
	[NumberOfRecordsInSAPWithPL] [int] NOT NULL,
	[NumberOfFailedGLMServiceLocationSearchCalls] [int] NOT NULL,
	[NumberOfFailedGLMServiceLocationCreationCalls] [int] NOT NULL,
	[GLMSLNumber] [varchar](50) NULL,
	[ExistsInGLMAsServiceLocation] [bit] NOT NULL,
	[NumberOfFailedGLMSCodeExistenceCalls] [int] NOT NULL,
	[NumberOfFailedGLMSCodeCreationCalls] [int] NOT NULL,
	[GLMSCode] [varchar](50) NULL,
	[HasGLMSCode] [bit] NOT NULL,
	[NumberOfFailedSAPServiceLocationAddressSearchCalls] [int] NOT NULL,
	[NumberOfFailedSAPServiceLocationAddressImportCalls] [int] NOT NULL,
	[ExistsInSAPAsServiceLocationAddress] [bit] NOT NULL,
	[NumberOfRecordsInSAPWithSL] [int] NOT NULL,
	[SourceCreationDate] [datetime] NULL,
	[SourceLastModifyDate] [datetime] NULL,
	[DateTimeOfLastMigrationStatusUpdate] [datetime] NOT NULL,
	[DateTimeOfLastDupDetection] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
	[ServiceOrderNumber] [varchar](100) NULL,
	[FIRST_ORDER_CREATE_DT] [datetime] NULL,
	[OPE_LAST_MODIFY_DATE] [datetime] NULL,
	[PL_LAST_MODIFY_DATE] [datetime] NULL,
	[PS_LAST_MODIFY_DATE] [datetime] NULL,
	[TotalProcessingTimeInTickString] [varchar](200) NULL,
	[TotalProcessingTimeAsHumanReadable] [varchar](200) NULL,
 CONSTRAINT [PK_tblOrderAddress] PRIMARY KEY CLUSTERED 
(
	[OrderAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderAddressLogItem]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderAddressLogItem](
	[OrderAddressLogItemID] [int] IDENTITY(1,1) NOT NULL,
	[OrderAddressID] [int] NOT NULL,
	[MigrationStatusID] [int] NOT NULL,
	[LogMessage] [varchar](2000) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblOrderAddressLogItem] PRIMARY KEY CLUSTERED 
(
	[OrderAddressLogItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderAddressType]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderAddressType](
	[OrderAddressTypeID] [int] IDENTITY(1,1) NOT NULL,
	[OrderAddressTypeDesc] [varchar](150) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblOrderAddressType] PRIMARY KEY CLUSTERED 
(
	[OrderAddressTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrderSystemOfRecord]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblOrderSystemOfRecord](
	[OrderSystemOfRecordID] [int] IDENTITY(1,1) NOT NULL,
	[OrderSystemOfRecordDesc] [varchar](150) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblOrderSystemOfRecord] PRIMARY KEY CLUSTERED 
(
	[OrderSystemOfRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSystemConfigItem]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSystemConfigItem](
	[SystemConfigItemID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigSettingName] [varchar](50) NOT NULL,
	[ConfigSettingValue] [varchar](2000) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblSystemConfigItem] PRIMARY KEY CLUSTERED 
(
	[SystemConfigItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSystemLogItem]    Script Date: 10/9/2018 3:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSystemLogItem](
	[SystemLogItemID] [int] IDENTITY(1,1) NOT NULL,
	[Note] [varchar](2000) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblSystemLogItem] PRIMARY KEY CLUSTERED 
(
	[SystemLogItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblMigrationStatus] ON 

INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (1, N'STAGED for Processing', N'The record has been identified as MISSING from GLM an/or as pertinent to the pool of Managed Service order addresses expected in SAP.  The address record was pulled in from the CDW and is now STAGED FOR and AWAITING further processing, which will attempt to add it to GLM and/or SAP.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (2, N'GLM SITE FOUND or CREATED', N'The BASE address record has been FOUND and/or CREATED in GLM as a SITE, and its PL Number (Master Site ID) can now be used to further process the record.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (3, N'API Call ERRORED while executing GLM SITE existence CHECK', N'The API call to FIND or CREATE the BASE address in GLM as a SITE LOCTION record failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (4, N'ABANDONED GLM SITE existence CHECK due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to FIND or CREATE the address as a SITE LOCATION in GLM have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.  It is recommended that a USER attempt to MANUALLY FINE or CREATE the SITE via the GLM User Interface.  Once the SITE exists in GLM and it has a SITE CODE assigned, then someone with PROD SUPPORT access in GLM can trigger an EMP Event for force SAP to pick the record up as part of its next scheduled run.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (5, N'STAGED for SITE CODE existence CHECK', N'A call to the GLM API has been SCHEDULED to determine if the SITE LOCATION has a valid SITE CODE assigned.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (6, N'API Call ERRORED while executing GLM SITE CODE existence CHECK', N'The API call to check the record for a SITE CODE failed.   The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (7, N'ABANDONED GLM SITE CODE existence CHECK due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to determine if the site has been assigned a SITE CODE have failed.  Processing cannot continue. The system will NOT take additional action on this record UNTIL a USER intervenes.  It is recommended that a USER attempt to MANUALLY VERIFY or ASSIGN a SITE CODE to the record via the GLM User Interface.  Once the SITE CODE exists in GLM,  someone with PROD SUPPORT access in GLM can trigger an EMP Event for force SAP to pick the record up as part of its next scheduled run.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (8, N'SITE CODE FOUND in GLM', N'The address record exists in GLM and HAS a valid SITE CODE assigned to it.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (9, N'SITE CODE does NOT EXIST in GLM', N'The address record exists in GLM but does NOT have a SITE CODE assigned to it.  Records that are missing SITE CODES are EXCLUDED from processing when SAP responds to EMP events published by GLM. ', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (10, N'STAGED for SITE CODE ASSIGNMENT TRIGGERING in GLM', N'The site DOES NOT have a SITE CODE, and so a subsequent call to the GLM API has been SCHEDULED to trigger the ASSIGNMENT of a SITE CODE.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (11, N'SITE CODE ASSIGNMENT Successfully TRIGGERED in GLM', N'The API call to GLM to TRIGGER the ASSIGNMENT of a SITE CODE was SUCCESSFUL.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (12, N'API Call ERRORED while trying to TRIGGER the ASSIGNMENT of a SITE CODE in GLM', N'The API call to trigger the subsequent assignment of a SITE CODE to the SITE in GLM has FAILED.  The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (13, N'ABANDONED GLM SITE CODE ASSIGNMENT due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple attempts to TRIGGER the ASSIGNMENT of a SITE CODE to the SITE in GLM have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.  It is recommended that a USER request that a SITE CODE be manually added to the SITE in GLM.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (14, N'WORK ITEM QUEUED in GLM for DELAYED SITE CODE ASSIGNMENT', N'Although the API call to GLM to TRIGGER the ASSIGNMENT of a SITE CODE was SUCCESSFUL, the response received from the API indicated that it COULD NOT be created in REAL-TIME.  It could take up to two days for the SITE CODE to be assigned.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (15, N'STAGED for SAP SITE LOCATION SEARCH', N'A call to the SAP API has been SCHEDULED to determine if the SITE LOCATION ADDRESS already exists in SAP.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (16, N'API Call ERRORED while executing SAP SITE LOCATION ADDRESS SEARCH', N'The API call to SEARCH for the SITE LOCATION ADDRESS in SAP has failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (17, N'ABANDONED SAP SITE LOCATION SEARCH due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to determine if the SITE LOCATION ADDRESS exists in SAP have failed.  Processing cannot continue.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (18, N'Site Location ADDRESS FOUND in SAP', N'The SITE LOCATION ADDRESS was FOUND in SAP', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (19, N'Site Location ADDRESS does NOT EXIST in SAP', N'The SITE LOCATION ADDRESS does NOT EXIST in SAP', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (20, N'READY to TRIGGER SITE LOCATION ADDRESS IMPORT to SAP', N'A call to the SAP API has been SCHEDULED, in an attempt to TRIGGER the IMMEDIATE import of the SITE LOCATION and its SERVICE LOCATIONS into SAP.  ', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (21, N'API Call ERRORED while attempting to TRIGGER SITE LOCATION ADDRESS IMPORT to SAP', N'The API call to TRIGGER the IMMEDIATE IMPORT of the SITE LOCATION into SAP has FAILED.  The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (22, N'ABANDONED attempts to TRIGGER SITE LOCATION ADDRESS IMPORT to SAP due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple attempts to TRIGGER the IMMEDIATE IMPORT of the SITE LOCATION into SAP have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (23, N'READY for ADDRESS TYPE BRANCHING Logic EVALUATION', N'The record has been QUEUED for FUTHER EVALUATION to determine if further processing is required to handle any Room, Floor, or Suite data.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (24, N'STAGED for GLM SERVICE LOCATION SEARCH', N'The ADDRESS record contains a Floor, Room, and/or Suite, making it a SERVICE LOCATION and causing it to be queued for further processing, which will attempt to SEARCH for the SERVICE LOCATION in GLM.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (25, N'API Call ERRORED while executing GLM SERVICE LOCATION SEARCH', N'The API call to SEARCH for the SERVICE LOCATION in GLM failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (26, N'ABANDONED GLM SERVICE LOCATION SEARCH due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple attempts to SEARCH for the SERVICE LOCATION in SAP have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (27, N'SERVICE LOCATION FOUND in GLM', N'The SERVICE LOCATION was FOUND in GLM, and its SL Number can now be used to further process the record.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (28, N'SERVICE LOCATION does NOT EXIST in GLM', N'The SERVICE LOCATION does NOT EXIST in GLM', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (29, N'STAGED for SERVICE LOCATION CREATION in GLM', N'A call to the GLM API has been SCHEDULED to trigger the CREATION of the SERVICE LOCATION', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (30, N'SERVICE LOCATION Successfully CREATED in GLM', N'The API call to GLM to CREATE the SERVICE LOCATION was SUCCESSFUL.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (31, N'API Call ERRORED while executing the GLM SERVICE LOCATION CREATION attempt', N'The API call to CREATE the SERVICE LOCATION in GLM has failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (32, N'ABANDONED GLM SERVICE LOCATION CREATION due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple attempts to CREATE the SERVICE LCOATION in GLM have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (33, N'STAGED for S CODE existence CHECK', N'A call to the GLM API has been SCHEDULED to determine if the SERVICE LOCATION has a valid S CODE assigned.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (34, N'API Call ERRORED while executing GLM S CODE existence CHECK', N'The API call to check the record for a S CODE failed.   The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (35, N'ABANDONED GLM S CODE existence CHECK due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to determine if the service location has been assigned an S CODE have failed.  Processing cannot continue. The system will NOT take additional action on this record UNTIL a USER intervenes.  It is recommended that a USER attempt to MANUALLY VERIFY or ASSIGN an S CODE to the record via the GLM User Interface.  Once the S CODE exists in GLM, someone with PROD SUPPORT access in GLM can trigger an EMP Event for force SAP to pick the record up as part of its next scheduled run.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (36, N'S CODE FOUND in GLM', N'The address record exists in GLM and HAS a valid S CODE assigned to it.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (37, N'S CODE does NOT EXIST in GLM', N'The address record exists in GLM but does NOT have an S CODE assigned to it.  Records that are missing S CODES are EXCLUDED from processing when SAP responds to EMP events published by GLM. ', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (38, N'STAGED for S CODE CREATION in GLM', N'The service location DOES NOT have an S CODE, and so a subsequent call to the GLM API has been SCHEDULED to trigger the ASSIGNMENT of an S CODE.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (39, N'S CODE Successfully CREATED in GLM', N'The API call to GLM to TRIGGER the ASSIGNMENT of an S CODE was SUCCESSFUL.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (40, N'API Call ERRORED while executing GLM S CODE CREATION attempt', N'The API call to trigger the subsequent assignment of an S CODE to the SERVICE LOCATION in GLM has FAILED.  The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (41, N'ABANDONED GLM S CODE CREATION due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple attempts to TRIGGER the ASSIGNMENT of an S CODE to the SERVIC LOCATION in GLM have failed.  The system will NOT take additional action on this record UNTIL a USER intervenes.  It is recommended that a USER request that an S CODE be manually added to the SSERVICE LOCATION in GLM.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (42, N'WORK ITEM QUEUED in GLM for DELAYED S CODE CREATION', N'Although the API call to GLM to TRIGGER the ASSIGNMENT of an S CODE was SUCCESSFUL, the response received from the API indicated that it COULD NOT be created in REAL-TIME.  It could take up to two days for the S CODE to be assigned.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (43, N'STAGED for SAP SERVICE LOCATION SEARCH', N'A call to the SAP API has been SCHEDULED to determine if the SERVICE LOCATION ADDRESS already exists in SAP.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (44, N'API Call ERRORED while executing SAP SERVICE LOCATION ADDRESS SEARCH', N'The API call to SEARCH for the SERVICE LOCATION ADDRESS in SAP has failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (45, N'ABANDONED SAP SERVICE LOCATION SEARCH due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to determine if the SERVICE LOCATION ADDRESS exists in SAP have failed.  Processing cannot continue.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (46, N'Service Location ADDRESS FOUND in SAP', N'The SERVICE LOCATION ADDRESS was FOUND in SAP', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (47, N'Service Location ADDRESS does NOT EXIST in SAP', N'The SERVICE LOCATION ADDRESS does NOT EXIST in SAP', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (48, N'READY to TRIGGER SERVICE LOCATION ADDRESS IMPORT to SAP', N'A call to the SAP API has been SCHEDULED, in an attempt to TRIGGER the IMMEDIATE import of the SERVICE LOCATION into SAP, along with its parent SITE LOCATION and any of its sibling SERVICE LOCATIONS with S Codes.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (49, N'API Call ERRORED while attempting to TRIGGER SERVICE LOCATION ADDRESS IMPORT to SAP', N'The API call to SEARCH for the SERVICE LOCATION ADDRESS in SAP has failed. The system will automatically re-attempt the call, and so a user does NOT need to intervene unless the processing of this record needs to be expedited.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (50, N'ABANDONED attempts to TRIGGER SERVICE LOCATION ADDRESS IMPORT to SAP due to API Call Errors', N'The record NEEDS USER INTERVENTION.  Multiple API calls intended to determine if the SERVICE LOCATION ADDRESS exists in SAP have failed.  Processing cannot continue.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (51, N'Processing COMPLETE', N'The record''s SITE and/or SERVICE LOCATION address has been fully PROCESSED and VERIFIED as EXISTING in SAP', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
INSERT [dbo].[tblMigrationStatus] ([MigrationStatusID], [MigrationStatusDesc], [MigrationStatusExtendedDescription], [DateCreated], [DateUpdated]) VALUES (52, N'IGNORED indefinitely', N'A USER or something about the state of the address RECORD has caused it to be flagged as a record that the system can/should IGNORE.  The system will NOT take any further action on this record unless a USER reverts its status accordingly.', CAST(N'2018-09-13 00:00:00.000' AS DateTime), CAST(N'2018-09-13 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblMigrationStatus] OFF
SET IDENTITY_INSERT [dbo].[tblOrderAddressType] ON 

INSERT [dbo].[tblOrderAddressType] ([OrderAddressTypeID], [OrderAddressTypeDesc], [DateCreated], [DateUpdated]) VALUES (1, N'Site', CAST(N'2018-08-28 00:00:00.000' AS DateTime), CAST(N'2018-08-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblOrderAddressType] ([OrderAddressTypeID], [OrderAddressTypeDesc], [DateCreated], [DateUpdated]) VALUES (2, N'Service Location', CAST(N'2018-08-28 00:00:00.000' AS DateTime), CAST(N'2018-08-28 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblOrderAddressType] OFF
SET IDENTITY_INSERT [dbo].[tblOrderSystemOfRecord] ON 

INSERT [dbo].[tblOrderSystemOfRecord] ([OrderSystemOfRecordID], [OrderSystemOfRecordDesc], [DateCreated], [DateUpdated]) VALUES (1, N'EON', CAST(N'2018-08-24 00:00:00.000' AS DateTime), CAST(N'2018-08-24 00:00:00.000' AS DateTime))
INSERT [dbo].[tblOrderSystemOfRecord] ([OrderSystemOfRecordID], [OrderSystemOfRecordDesc], [DateCreated], [DateUpdated]) VALUES (2, N'Pipeline', CAST(N'2018-08-24 00:00:00.000' AS DateTime), CAST(N'2018-08-24 00:00:00.000' AS DateTime))
INSERT [dbo].[tblOrderSystemOfRecord] ([OrderSystemOfRecordID], [OrderSystemOfRecordDesc], [DateCreated], [DateUpdated]) VALUES (3, N'Unknown', CAST(N'2018-08-24 00:00:00.000' AS DateTime), CAST(N'2018-08-24 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblOrderSystemOfRecord] OFF
SET IDENTITY_INSERT [dbo].[tblSystemConfigItem] ON 

INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (1, N'StatsEmailRecipientList', N'brendan.muir@level3.com;matt.hansard@level3.com', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-09-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (2, N'SystemNotificationEmailRecipientList', N'brendan.muir@level3.com', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-09-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (3, N'CDWQueryPreciseProductCodeListForInClause', N'BMGC046,BMGC093,BMGC020,BMGC034,BMGC038,BMGC018,BMGC028,BMGC090', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-09-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (4, N'CDWQueryWildcardProductCodeListForLikeClauses', N'gcms%', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-09-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (5, N'CDWQuerySourceSystemCodesForInClause', N'PIPELINE,EON', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-09-28 00:00:00.000' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (6, N'CDWQueryExcludedRegionsNotInClause', N'LATAM,EMEA,APAC,NULL', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-10-09 14:00:42.197' AS DateTime))
INSERT [dbo].[tblSystemConfigItem] ([SystemConfigItemID], [ConfigSettingName], [ConfigSettingValue], [DateCreated], [DateUpdated]) VALUES (7, N'CDWQueryIncludedCountryCodes', N'USA,CAN', CAST(N'2018-09-28 00:00:00.000' AS DateTime), CAST(N'2018-10-09 15:24:47.843' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblSystemConfigItem] OFF
ALTER TABLE [dbo].[tblAPICallLogItem]  WITH CHECK ADD  CONSTRAINT [FK_tblAPICallLogItem_tblOrderAddress] FOREIGN KEY([OrderAddressID])
REFERENCES [dbo].[tblOrderAddress] ([OrderAddressID])
GO
ALTER TABLE [dbo].[tblAPICallLogItem] CHECK CONSTRAINT [FK_tblAPICallLogItem_tblOrderAddress]
GO
ALTER TABLE [dbo].[tblOrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderAddress_tblMigrationStatus] FOREIGN KEY([MigrationStatusID])
REFERENCES [dbo].[tblMigrationStatus] ([MigrationStatusID])
GO
ALTER TABLE [dbo].[tblOrderAddress] CHECK CONSTRAINT [FK_tblOrderAddress_tblMigrationStatus]
GO
ALTER TABLE [dbo].[tblOrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderAddress_tblOrderAddressType] FOREIGN KEY([OrderAddressTypeID])
REFERENCES [dbo].[tblOrderAddressType] ([OrderAddressTypeID])
GO
ALTER TABLE [dbo].[tblOrderAddress] CHECK CONSTRAINT [FK_tblOrderAddress_tblOrderAddressType]
GO
ALTER TABLE [dbo].[tblOrderAddress]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderAddress_tblOrderSystemOfRecord] FOREIGN KEY([OrderSystemOfRecordID])
REFERENCES [dbo].[tblOrderSystemOfRecord] ([OrderSystemOfRecordID])
GO
ALTER TABLE [dbo].[tblOrderAddress] CHECK CONSTRAINT [FK_tblOrderAddress_tblOrderSystemOfRecord]
GO
ALTER TABLE [dbo].[tblOrderAddressLogItem]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderAddressLogItem_tblMigrationStatus] FOREIGN KEY([MigrationStatusID])
REFERENCES [dbo].[tblMigrationStatus] ([MigrationStatusID])
GO
ALTER TABLE [dbo].[tblOrderAddressLogItem] CHECK CONSTRAINT [FK_tblOrderAddressLogItem_tblMigrationStatus]
GO
ALTER TABLE [dbo].[tblOrderAddressLogItem]  WITH CHECK ADD  CONSTRAINT [FK_tblOrderAddressLogItem_tblOrderAddress] FOREIGN KEY([OrderAddressID])
REFERENCES [dbo].[tblOrderAddress] ([OrderAddressID])
GO
ALTER TABLE [dbo].[tblOrderAddressLogItem] CHECK CONSTRAINT [FK_tblOrderAddressLogItem_tblOrderAddress]
GO
