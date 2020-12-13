using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;


using Level3.AddressManagement.DAL;
using Level3.AddressManagement.Model;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class OrderAddressTester
    {
        [TestMethod]
        public void Test_Select()
        {
            List<OrderAddress> lstOrderAddress = new OrderAddress().GetRecords("SELECT '' as DataHash, * FROM tblOrderAddress");
        }

        [TestMethod]
        public void Test_Insert_Site_No_CLII()
        {
            // https://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=SL0000606479
            // https://sapprd.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=PL0000025421

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);

            objOrderAddress.ServiceOrderNumber = "S" + DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.FIRST_ORDER_CREATE_DT = new DateTime(2018, 08, 01);
            objOrderAddress.OPE_LAST_MODIFY_DATE = new DateTime(2018, 08, 02);
            objOrderAddress.PL_LAST_MODIFY_DATE = new DateTime(2018, 08, 03);
            objOrderAddress.PS_LAST_MODIFY_DATE = new DateTime(2018, 08, 04);
            objOrderAddress.TotalProcessingTimeInTickString = DateTime.Now.Ticks.ToString();
            objOrderAddress.TotalProcessingTimeAsHumanReadable = Model.StopwatchUtil.GetHumanReadableTimeElapsedStringFromTicks(DateTime.Now.Ticks + DateTime.Now.Ticks);


            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

        }

        [TestMethod]
        public void Test_Insert_Site_Duplicate_BLOCKED()
        {
            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);

            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            // Try to insert a DUP record
            objOrderAddress.OrderAddressID = 0;
            Assert.IsFalse(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(strLastError != String.Empty);

        }

        [TestMethod]
        public void Test_Insert_Site_Duplicate_BLOCKED_BASICS()
        {
            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);

            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            // Try to insert a DUP record
            OrderAddress objOrderAddressSecondBasic = new OrderAddress();

            objOrderAddressSecondBasic.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddressSecondBasic.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddressSecondBasic.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddressSecondBasic.CDWCustomerOrderNumber = objOrderAddress.CDWCustomerOrderNumber;
            objOrderAddressSecondBasic.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddressSecondBasic.CDWCity = "SAN MATEO";
            objOrderAddressSecondBasic.CDWState = "CA";
            objOrderAddressSecondBasic.CDWPostalCode = "94401";
            objOrderAddressSecondBasic.CDWCountry = "USA";
            objOrderAddressSecondBasic.CDWFloor = String.Empty;
            objOrderAddressSecondBasic.CDWRoom = String.Empty;
            objOrderAddressSecondBasic.CDWSuite = String.Empty;
            objOrderAddressSecondBasic.ValidCLII = false;
            objOrderAddressSecondBasic.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddressSecondBasic.ExistsInGLMAsSite = true;
            objOrderAddressSecondBasic.GLMPLNumber = "";
            objOrderAddressSecondBasic.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddressSecondBasic.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddressSecondBasic.GLMSiteCode = "";
            objOrderAddressSecondBasic.HasGLMSiteCode = true;
            objOrderAddressSecondBasic.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddressSecondBasic.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddressSecondBasic.ExistsInSAPAsSiteAddress = true;
            objOrderAddressSecondBasic.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddressSecondBasic.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddressSecondBasic.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddressSecondBasic.GLMSLNumber = "";
            objOrderAddressSecondBasic.ExistsInGLMAsServiceLocation = true;
            objOrderAddressSecondBasic.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddressSecondBasic.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddressSecondBasic.GLMSCode = "";
            objOrderAddressSecondBasic.HasGLMSCode = true;
            objOrderAddressSecondBasic.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddressSecondBasic.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddressSecondBasic.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddressSecondBasic.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddressSecondBasic.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddressSecondBasic.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddressSecondBasic.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddressSecondBasic.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddressSecondBasic.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddressSecondBasic.DateUpdated = new DateTime(2018, 08, 01);

            objOrderAddress.OrderAddressID = 0;
            Assert.IsTrue((objOrderAddress.OrderAddressID == null) || (objOrderAddress.OrderAddressID == 0));
            Assert.IsFalse(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(strLastError != String.Empty);

        }

        [TestMethod]
        public void Test_Insert_Site_Then_Update()
        {

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);

            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            objOrderAddress.CDWCity += " UPDATED VALUE";
            Assert.IsTrue(objOrderAddress.Update(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);


            objOrderAddress.CDWCity += " AGAIN";
            Assert.IsTrue(objOrderAddress.Update(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);


            objOrderAddress.CDWCity = "SAN MATEO";
            Assert.IsTrue(objOrderAddress.Update(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);
        }

        [TestMethod]
        public void Test_Insert_Site_Then_UpdateOptimistic()
        {

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);


            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            // This will fail... no data hash populated
            objOrderAddress.CDWCity += " UPDATED VALUE";
            Assert.IsFalse(objOrderAddress.UpdateOptimistic(out strLastError));
            Assert.IsTrue(strLastError.Contains("Concurrent action violation"));

            // This should work since it is populated with a method that returns the current data hash
            OrderAddress objOrderAddressViaGet = new OrderAddress().Get(objOrderAddress.OrderAddressID.Value, out strLastError);
            objOrderAddressViaGet.CDWCity += " UPDATED VIA GET";
            Assert.IsTrue(objOrderAddressViaGet.UpdateOptimistic(out strLastError));
            Assert.IsTrue(objOrderAddressViaGet.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            // This should fail since we intentionally override things with a bogus data hash
            OrderAddress objOrderAddress2ViaGet = new OrderAddress().Get(objOrderAddress.OrderAddressID.Value, out strLastError);
            objOrderAddress2ViaGet.DataHash += "BogusDataHash";
            Assert.IsFalse(objOrderAddress2ViaGet.UpdateOptimistic(out strLastError));
            Assert.IsTrue(strLastError.Contains("Concurrent action violation"));

        }

        [TestMethod]
        public void Test_Insert_Site_Then_Get()
        {

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);

            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            OrderAddress objOrderAddressViaGet = new OrderAddress().Get(objOrderAddress.OrderAddressID.Value, out strLastError);
            Assert.IsTrue(objOrderAddressViaGet.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

        }


        [TestMethod]
        public void Test_Insert_Site_Then_GetByCompositeFields_NullSLFields()
        {

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.EON;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "72 E 3RD AVE";
            objOrderAddress.CDWCity = "SAN MATEO";
            objOrderAddress.CDWState = "CA";
            objOrderAddress.CDWPostalCode = "94401";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = String.Empty;
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);


            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

            OrderAddress objOrderAddressViaGet = new OrderAddress().Get(objOrderAddress.OrderSystemOfRecordID.Value, objOrderAddress.CDWCustomerOrderNumber, objOrderAddress.CDWAddressOne, objOrderAddress.CDWCity, objOrderAddress.CDWState, objOrderAddress.CDWPostalCode, objOrderAddress.CDWCountry, objOrderAddress.CDWFloor, objOrderAddress.CDWRoom, objOrderAddress.CDWSuite, out strLastError);
            Assert.IsTrue(objOrderAddressViaGet.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

        }

        [TestMethod]
        public void Test_OneOffDebugger()
        {

            //        1058552	143 MILTON PARK DR 1	ABINGDON NULL    OX14 4SE GBR
            string strLastError = String.Empty;
            OrderAddress objOrderAddressViaGet = new OrderAddress().Get(1, "1058552", "143 MILTON PARK DR 1", "ABINGDON", "", "OX14 4SE", "GBR", "", "", "", out strLastError);
            Assert.IsTrue(objOrderAddressViaGet.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

        }


        [TestMethod]
        public void Test_Get_DynamicWhereClause()
        {
            //  WHERE tblOrderAddress.MigrationStatusID IN (1,2,3,5,6,7,8,9,10,11,12,13,15,16,17,18,19)
            //  ORDER BY OrderAddressID

            string strLastError = String.Empty;

            List<OrderAddress> lstOrderAddresses = new OrderAddress().Search("tblOrderAddress.MigrationStatusID IN (1,2,3,5,6,7,8,9,10,11,12,13,15,16,17,18,19)", "OrderAddressID", out strLastError, 1, 10);
            Assert.IsTrue(lstOrderAddresses.Count > 0);
            Assert.IsTrue(strLastError == String.Empty);
        }


        [TestMethod]
        public void Test_Get_DynamicWhereClause_SearchWithCount()
        {
            //  WHERE tblOrderAddress.MigrationStatusID IN (1,2,3,5,6,7,8,9,10,11,12,13,15,16,17,18,19)
            int intCount;
            string strLastError = String.Empty;
            string strWhereClause = "tblOrderAddress.OrderSystemOfRecordID IN (1)";
            
            //List<OrderAddress> lstOrderAddresses = new OrderAddress().SearchPaymentWithCount("tblOrderAddress.MigrationStatusID IN (1,2,3,5,6,7,8,9,10,11,12,13,15,16,17,18,19)", "OrderAddressID", 1, 10, out intCount);

            List<OrderAddress> lstOrderAddresses = new OrderAddress().SearchPaymentWithCount(strWhereClause, "OrderAddressID", 1, 10, out intCount);

            Assert.IsTrue(intCount > 0);
        }


        [TestMethod]
        public void Test_Insert_Site_Brendan()
        {
            // https://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=SL0000606479
            // https://sapprd.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=PL0000025421

            OrderAddress objOrderAddress = new OrderAddress();

            //objOrderAddress.OrderAddressID = 0;
            objOrderAddress.OrderAddressTypeID = (int)OrderAddressTypes.Site;
            objOrderAddress.MigrationStatusID = (int)MigrationStatuses.STAGED_for_Processing;
            objOrderAddress.OrderSystemOfRecordID = (int)OrderSystemOfRecords.Pipeline;
            objOrderAddress.CDWCustomerOrderNumber = DateTime.UtcNow.Ticks.ToString();
            objOrderAddress.CDWAddressOne = "1744 SHEA CENTER DR";
            objOrderAddress.CDWCity = "HIGHLANDS RANCH";
            objOrderAddress.CDWState = "CO";
            objOrderAddress.CDWPostalCode = "80129";
            objOrderAddress.CDWCountry = "USA";
            objOrderAddress.CDWFloor = String.Empty;
            objOrderAddress.CDWRoom = String.Empty;
            objOrderAddress.CDWSuite = "APT 204";
            objOrderAddress.CDWCLII = null;
            objOrderAddress.ValidCLII = false;
            objOrderAddress.NumberOfFailedGLMSiteCalls = 0;
            objOrderAddress.ExistsInGLMAsSite = true;
            objOrderAddress.GLMPLNumber = "";
            objOrderAddress.NumberOfFailedGLMSiteCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSiteCodeCreationCalls = 0;
            objOrderAddress.GLMSiteCode = "";
            objOrderAddress.HasGLMSiteCode = true;
            objOrderAddress.NumberOfFailedSAPSiteAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPSiteAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsSiteAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithPL = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationSearchCalls = 0;
            objOrderAddress.NumberOfFailedGLMServiceLocationCreationCalls = 0;
            objOrderAddress.GLMSLNumber = "";
            objOrderAddress.ExistsInGLMAsServiceLocation = true;
            objOrderAddress.NumberOfFailedGLMSCodeExistenceCalls = 0;
            objOrderAddress.NumberOfFailedGLMSCodeCreationCalls = 0;
            objOrderAddress.GLMSCode = "";
            objOrderAddress.HasGLMSCode = true;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressSearchCalls = 0;
            objOrderAddress.NumberOfFailedSAPServiceLocationAddressImportCalls = 0;
            objOrderAddress.ExistsInSAPAsServiceLocationAddress = true;
            objOrderAddress.NumberOfRecordsInSAPWithSL = 0;
            objOrderAddress.SourceCreationDate = new DateTime(2018, 08, 01);
            objOrderAddress.SourceLastModifyDate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastMigrationStatusUpdate = new DateTime(2018, 08, 01);
            objOrderAddress.DateTimeOfLastDupDetection = new DateTime(2018, 08, 01);
            objOrderAddress.DateCreated = new DateTime(2018, 08, 01);
            objOrderAddress.DateUpdated = new DateTime(2018, 08, 01);


            string strLastError = String.Empty;
            Assert.IsTrue(objOrderAddress.Insert(out strLastError));
            Assert.IsTrue(objOrderAddress.OrderAddressID > 0);
            Assert.IsTrue(strLastError == String.Empty);

        }


    }
}
