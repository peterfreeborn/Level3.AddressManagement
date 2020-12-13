-- GO LIVE RE-HEARSAL

-- [ 1 ] Run the CDW SYNC only : /pullFromCDW
		
		SELECT Count(*)  as TOTAL_ADDRESS_COUNT FROM tblOrderAddress; 
		SELECT Count(*) as EON_RECORDS FROM tblOrderAddress WHERE OrderSystemOfRecordID=1;
		SELECT Count(*) as PIPELINE_RECORDS FROM tblOrderAddress WHERE OrderSystemOfRecordID=2;

		SELECT Count(*)  as STAGED_TO_RUN FROM tblOrderAddress WHERE MigrationStatusID = 1; 
		SELECT Count(*)  as IGRNORED FROM tblOrderAddress WHERE MigrationStatusID = 52; 
		SELECT Count(*)  as IN_WORFKLOW_OR_ISSUES FROM tblOrderAddress WHERE MigrationStatusID NOT IN (1,52); 


-- [ 2 ] Stage the FIRST BATCH TO RUN (OCTOBER ONLY)
		
		SELECT Count(*)  as TOTAL_BATCH_1 FROM tblOrderAddress WHERE FIRST_ORDER_CREATE_DT >= '2018-10-01 00:00:00.000';
		-- Count the number of ignored records
		SELECT Count(*) as STAGED_PRE_UPDATE_COUNT FROM tblOrderAddress WHERE MigrationStatusID=1;
		--UPDATE tblOrderAddress SET MigrationStatusID = 52 WHERE MigrationStatusID=1;
		-- Count the number of ignored records
		SELECT Count(*) as STAGED_POST_UPDATE_COUNT FROM tblOrderAddress WHERE MigrationStatusID=1;
		-- REVERT BATCH 1
		--UPDATE tblOrderAddress SET MigrationStatusID = 1 WHERE FIRST_ORDER_CREATE_DT >= '2018-10-01 00:00:00.000' AND MigrationStatusID IN (52);
		-- Count the number of ignored records
		SELECT Count(*) as IGNORED_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 52;
		-- Count the number of STAGED records
		SELECT Count(*) as STAGED_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 1;
		-- Count the number of OTHER records
		SELECT Count(*) as OTHER_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID NOT IN (1,52);


-- [ 1 ] Run the CONSOLE APP with an ADDITIONAL SWITCH TO PROCESS ALL: /pullFromCDW /processAll

		-- Count the number of ignored records
		SELECT Count(*) as IGNORED_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 52;
		-- Count the number of STAGED records
		SELECT Count(*) as STAGED_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 1;
		-- Count the number of OTHER records
		SELECT Count(*) as OTHER_COUNT FROM tblOrderAddress WHERE MigrationStatusID NOT IN (1,52);

		-- COUNT the number of API Calls
		SELECT Count(*)  as TOTAL_API_CALLS FROM tblAPICallLogItem;
		SELECT Count(*) as SAP_API_CALLS FROM tblAPICallLogItem WHERE Host LIKE '%SAP%';
		SELECT Count(*) as GLM_API_CALLS FROM tblAPICallLogItem WHERE Host LIKE '%GLM%';

		-- Count the number of processed records
		SELECT Count(*) as OTHER_COUNT FROM tblOrderAddress WHERE MigrationStatusID IN (51);


-- [ 2 ] Stage the SECOND BATCH TO RUN (OCTOBER ONLY)
		
		SELECT Count(*)  as TOTAL_BATCH_1 FROM tblOrderAddress WHERE FIRST_ORDER_CREATE_DT >= '2018-09-27 00:00:00.000' AND MigrationStatusID=52;
		-- Count the number of ignored records
		SELECT Count(*) as STAGED_PRE_UPDATE_COUNT FROM tblOrderAddress WHERE MigrationStatusID=1;
		-- REVERT BATCH 2
		--UPDATE tblOrderAddress SET MigrationStatusID = 1 WHERE FIRST_ORDER_CREATE_DT >= '2018-09-27 00:00:00.000'  AND MigrationStatusID IN (52);
		-- Count the number of ignored records
		SELECT Count(*) as IGNORED_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 52;
		-- Count the number of STAGED records
		SELECT Count(*) as STAGED_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID = 1;
		-- Count the number of OTHER records
		SELECT Count(*) as OTHER_BEFOREKICKOFF_COUNT FROM tblOrderAddress WHERE MigrationStatusID NOT IN (1,52);


SELECT Min(FIRST_ORDER_CREATE_DT)  as MIN_ORDER_DATE FROM tblOrderAddress

-- START HERE
SELECT Max(DateCreated) FROM tblAPICallLogItem;
SELECT * FROM tblAPICallLogItem ORDER BY DateCreated DESC
SELECT * FROM tblAPICallLogItem WHERE DateCreated >= '2018-10-05 13:36:18.740'  ORDER BY DateCreated DESC
SELECT Count(*)  as TOTAL_CALLS FROM tblAPICallLogItem WHERE DateCreated >= '2018-10-05 13:36:18.740'
SELECT Count(*) as SAP_CALLS FROM tblAPICallLogItem WHERE DateCreated >= '2018-10-05 13:36:18.740' AND Host LIKE '%SAP%' 
SELECT Count(*) as GLM_CALLS FROM tblAPICallLogItem WHERE DateCreated >= '2018-10-05 13:36:18.740' AND Host LIKE '%GLM%'
SELECT Count(*) FROM tblAPICallLogItem WHERE DateCreated >= '2018-10-05 13:36:18.740'


SELECT Count(*) FROM tblOrderAddress WHERE CDWCountry NOT LIKE '%USA%' AND CDWCountry NOT LIKE '%CAN%'
SELECT Count(*) FROM tblOrderAddress WHERE CDWCountry LIKE '%USA%' OR CDWCountry LIKE '%CAN%'

--DELETE FROM tblAPICallLogItem WHERE OrderAddressID IN (SELECT OrderAddressID FROM tblOrderAddress WHERE CDWCountry NOT LIKE '%USA%' AND CDWCountry NOT LIKE '%CAN%')
--DELETE FROM tblOrderAddressLogItem  WHERE OrderAddressID IN (SELECT OrderAddressID FROM tblOrderAddress WHERE CDWCountry NOT LIKE '%USA%' AND CDWCountry NOT LIKE '%CAN%')
--DELETE FROM tblOrderAddress  WHERE OrderAddressID IN (SELECT OrderAddressID FROM tblOrderAddress WHERE CDWCountry NOT LIKE '%USA%' AND CDWCountry NOT LIKE '%CAN%')

SELECT Count(*) FROM tblOrderAddress WHERE CDWCountry NOT LIKE '%USA%' AND CDWCountry NOT LIKE '%CAN%'



--UPDATE tblOrderAddress SET MigrationStatusID = (MigrationStatusID -1) WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41)
SELECT COUNT(*) as GLM_ERRORS FROM tblOrderAddress WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41)
SELECT COUNT(*) as SAP_ERRORS FROM tblOrderAddress WHERE MigrationStatusID IN (16,17,21,22,44,45,49,50)
SELECT * FROM tblMigrationStatus WHERE MigrationStatusID IN (16,17,21,22,44,45,49,50)

SELECT * FROM tblMigrationStatus WHERE MigrationStatusDesc like '%SAP%'
--UPDATE tblOrderAddress SET MigrationStatusID = (MigrationStatusID -1) WHERE MigrationStatusID IN (4,7,13,26,32,35,41)
SELECT * FROM tblMigrationStatus WHERE MigrationStatusDesc like '%GLM%' AND MigrationStatusDesc like '%ABAN%'
SELECT * FROM tblMigrationStatus WHERE MigrationStatusID IN (4-1,7-1,13-1,26-1,32-1,35-1,41-1)
SELECT * FROM tblMigrationStatus WHERE MigrationStatusID IN (4,7,13,26,32,35,41)


		SELECT Count(*)  as TOTAL_ADDRESS_COUNT FROM tblOrderAddress; 
		SELECT Count(*) as EON_RECORDS FROM tblOrderAddress WHERE OrderSystemOfRecordID=1;
		SELECT Count(*) as PIPELINE_RECORDS FROM tblOrderAddress WHERE OrderSystemOfRecordID=2;

		SELECT Count(*)  as STAGED_TO_RUN FROM tblOrderAddress WHERE MigrationStatusID = 1; 
		SELECT Count(*)  as IGRNORED FROM tblOrderAddress WHERE MigrationStatusID = 52; 
		SELECT Count(*)  as IN_WORFKLOW_OR_ISSUES FROM tblOrderAddress WHERE MigrationStatusID NOT IN (1,51,52); 

		
		SELECT Count(*)  as IN_WORFKLOW_WITHOUT_ISSUE FROM tblOrderAddress WHERE MigrationStatusID NOT IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50,1,52,51) 
		SELECT Count(*)  as ISSUES FROM tblOrderAddress WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50) 
		SELECT Count(*)  as IN_WORFKLOW_OR_ISSUES FROM tblOrderAddress WHERE MigrationStatusID NOT IN (51,52); 
		SELECT Count(*)  as PROCESSED FROM tblOrderAddress WHERE MigrationStatusID IN (51); 
		SELECT COUNT(*) as GLM_ERRORS FROM tblOrderAddress WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41)
		SELECT COUNT(*) as SAP_ERRORS FROM tblOrderAddress WHERE MigrationStatusID IN (16,17,21,22,44,45,49,50)


		SELECT * FROM tblOrderAddress WHERE OrderAddressID=7174
		SELECT * FROm tblOrderAddressLogItem WHERE OrderAddressID=7174
		

		SELECT * FROM tblOrderAddress WHERE MigrationStatusID IN (12,13,40,41)

		SELECT Max(DateTimeOfLastMigrationStatusUpdate) from tblOrderAddress

		-- COUNTS
		SELECT Count(*) as ACTED_UPON FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52);
		SELECT COUNT(*) as ERRORS FROM tblOrderAddress WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50)
		SELECT Count(*) as PROCESSED FROM tblOrderAddress WHERE MigrationStatusID IN (51); 
		SELECT COUNT(*) as STILL_PROCESSING FROM tblOrderAddress WHERE MigrationStatusID NOT IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50,51,52)

		SELECT Count(*)  as TOTAL_ADDRESS_COUNT FROM tblOrderAddress; 
		SELECT CAST(Count(*) as float) as NOTIGNORED FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52)
		SELECT ((SELECT CAST(Count(*) as float) as NOTIGNORED FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52)) / (SELECT CAST(Count(*) as float) as ACTED_UPON FROM tblOrderAddress WHERE MigrationStatusID IN (52))) * 100 as PERCENT_ATTEMPTED;
		-- PERCENTAGES
		SELECT ((SELECT CAST(Count(*) as float) as PROCESSED FROM tblOrderAddress WHERE MigrationStatusID IN (51)) / (SELECT CAST(Count(*) as float) as ACTED_UPON FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52))) * 100 as PERCENT_SUCCESS;
		SELECT ((SELECT CAST(Count(*) as float) as ERRORING FROM tblOrderAddress WHERE MigrationStatusID IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50)) / (SELECT CAST(Count(*) as float) as ACTED_UPON FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52))) * 100 as PERCENT_ERRORED;
		SELECT ((SELECT CAST(Count(*) as float) as STILL FROM tblOrderAddress WHERE MigrationStatusID NOT IN (3,4,6,7,12,13,25,26,31,32,40,41,16,17,21,22,44,45,49,50,51,52)) / (SELECT CAST(Count(*) as float) as ACTED_UPON FROM tblOrderAddress WHERE MigrationStatusID NOT IN (52))) * 100 as PERCENT_STILL_PROCESSING;
		