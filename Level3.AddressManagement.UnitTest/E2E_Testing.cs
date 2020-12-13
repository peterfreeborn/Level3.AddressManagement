using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class E2E_Testing
    {
        #region Past TESTING - IDs no longer valid
        [TestMethod]
        public void Test_SERVICE_LOCATION_NothingInGLM()
        {
            // NOTE!!!!!!  Prior to testing, this record was found in our DB, but was a SERVICE LOCATION as opposed to a SITE LOCATION as th address provided by Jennifer reflects
            //            
            //            (NOT IN GLM)
            //            (SHOULD NOT BE IN SAP)
            //            203 Enterprise Dr.                         
            //            Calhoun, GA 30701
            //                      Also came in with:
            //                          1045 Roswell Manor Cir.
            //                          Roswell, GA 30076
            //                          (Emailed Jennifer - NOT a "DELIVERY" address in SAP according to Jonathan... AND JENNIFER indicated this is not an address we need to worry about for now
            //
            // QUERY:                   SELECT * FROM tblOrderAddress WHERE CDWAddressOne LIKE '%203%' and CDWState='GA'
            // OrderAddressID:          717
            // CUSTOMER ORDER NUMBER:   ORDER1440638

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(717));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SITE_LOCATION_InGLM_NoSiteCode()
        {
            //            
            //            
            //            (In GLM, but NO Site Code and No SERVICE LOCATION and thereby No SCODE) --> http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0009355623
            //            (NOT IN SAP) --> http://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=PL0009355623
            //
            //          1010ZPWH
            //          FIRST INTERNATIONAL TITLE, INC.	
            //          234 FOREST PARK CIR, 
            //          Floor 1st, Room Telco,
            //          PANAMA CITY, FL, 32405 - 4919
            //          
            // QUERY:                   SELECT * FROM tblOrderAddress WHERE CDWAddressOne LIKE '%234%' and CDWState='FL'
            // OrderAddressID:          980
            // CUSTOMER ORDER NUMBER:   ORDER1613994


            // NOTE:  Prior to testing, this record was found in our DB, but was a SERVICE LOCATION as opposed to a SITE LOCATION as th address provided by Jennifer reflects
            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(980));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SERVICE_LOCATION_InGLM_NoSiteCode_Has_ServiceLocation_BUT_No_SCODE()
        {
            //            
            //            
            //            (In GLM, but NO Site Code, SL Exists but does NOT have an SCODE) --> http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0000713667
            //            (NOT IN SAP) --> http://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=PL0009355623
            //
            //          1011JXRT
            //          7579 W 151st ST	
            //          Florr 1st, Room telco
            //          Stanley, KS, 66223
            //          
            // QUERY:                   SELECT * FROM tblOrderAddress WHERE CDWAddressOne LIKE '%7579%' and CDWState='KS'
            // OrderAddressID:          1088
            // CUSTOMER ORDER NUMBER:   ORDER1685533


            // NOTE:  Prior to testing, this record was found in our DB, but was a SERVICE LOCATION as opposed to a SITE LOCATION as th address provided by Jennifer reflects
            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1088));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }


        // SELF - PULLED SAMPLES --------------------------------------------------------------------------

        [TestMethod]
        public void Test_SITE_LOCATION_InGLM_WithSiteCode__BUT_No_SCODE()
        {
            //            
            //            
            //            (In GLM, with Site Code) --> http://glmenv1.level3.com/GLMSWeb/Location/Details/PL0012598698
            //            (In SAP) --> https://sapecq.corp.intranet/sap/bc/rest/lvlt/address/verify?saml2=disabled&im_sort2=PL0012598698
            //
            //          
            //          7000 BROOKTREE RD
            //          WEXFORD, PA  15090
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              108	ORDER1195617	7000 BROOKTREE RD	WEXFORD	PA	15090	USA
            // OrderAddressID:          108
            // CUSTOMER ORDER NUMBER:   ORDER1195617


            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(108));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SITE_LOCATION_NOT_in_GLM()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          2451 STONE MYERS PKWY
            //          GRAPEVINE, TX  76051-4782
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              1237	ORDER1788272	2451 STONE MYERS PKWY	GRAPEVINE	TX	76051-4782	USA
            // OrderAddressID:          1237
            // CUSTOMER ORDER NUMBER:   ORDER1788272


            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1237));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SITE_LOCATION_NOT_in_GLM_BUCKNER_MO()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          26911 E US HIGHWAY 24
            //          BUCKNER, MO  64016-8140
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              1218	ORDER1778187	26911 E US HIGHWAY 24	BUCKNER	MO	64016-8140	USA
            // OrderAddressID:          1218
            // CUSTOMER ORDER NUMBER:   ORDER1778187

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1218));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SITE_LOCATION_NOT_in_GLM_ATLANTA_GA()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          2202 GENERAL BOOTH BLVD
            //          VIRGINIA BEACH, VA  23456-3907
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              1083	ORDER1680347	2202 GENERAL BOOTH BLVD	VIRGINIA BEACH	VA	23456-3907	USA
            // OrderAddressID:          1083
            // CUSTOMER ORDER NUMBER:   ORDER1680347

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1083));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SITE_LOCATION_NOTHING_in_GLM_SWEDESBORO_NJ()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          200 ARLINGTON BLVD
            //          SWEDESBORO, NJ  08085
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressID=1353
            // RESULT ROW:              1353	ORDER1829450	200 ARLINGTON BLVD	SWEDESBORO	NJ	08085	USA
            // OrderAddressID:          1353
            // CUSTOMER ORDER NUMBER:   ORDER1829450

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1353));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SERVICE_LOCATION_NOTHING_in_GLM_CUBA_MO()
        {
            // -- ??? WAITING FOR RESPONSE FROM JONATHAN AND CINO... SL NOT SHOWING UP IN SAP
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          715 S FRANKLIN ST
            //          CUBA, MO  65453
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressID=1270
            // RESULT ROW:              1270	ORDER1818419	715 S FRANKLIN ST	CUBA	MO	65453	USA
            // OrderAddressID:          1270
            // CUSTOMER ORDER NUMBER:   ORDER1818419

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1270));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SERVICE_LOCATION_NOTHING_in_GLM_CHATANOOGA_TN()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          2396 LIFESTYLE WAY
            //          CHATTANOOGA, TN  37421-2291
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              1224	ORDER1782828	2396 LIFESTYLE WAY	CHATTANOOGA	TN	37421-2291	USA
            // OrderAddressID:          1224
            // CUSTOMER ORDER NUMBER:   ORDER1782828

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1224));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        }

        [TestMethod]
        public void Test_SERVICE_LOCATION_NOTHING_in_GLM_DORAL_FL_AlsoGoodRETEST_For_FloorString_Cleanup()
        {
            //            
            //            
            //            (NOT In GLM) --> 
            //            (NOT IN SAP) --> 
            //
            //          
            //          10915 NW 41ST ST
            //          DORAL, FL  33178-4866
            //          Floor = 1st, Room = MDF
            //          USA

            // QUERY:                   SELECT OrderAddressID, CDWCustomerOrderNumber, CDWAddressOne, CDWCity, CDWState,CDWPostalCode, CDWCountry  From tblOrderAddress WHERE OrderAddressTypeID=1
            // RESULT ROW:              1216	ORDER1777442	10915 NW 41ST ST	1st	MDF		DORAL	FL	33178-4866	USA
            // OrderAddressID:          1216
            // CUSTOMER ORDER NUMBER:   ORDER1777442

            // ON HOLD ---------------------------------->>>

            BLL.OrderAddressProcessor objOrderAddressProcessor = new BLL.OrderAddressProcessor();
            Assert.IsTrue(objOrderAddressProcessor.Load(1216));
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
            Assert.IsTrue(objOrderAddressProcessor.Process());
            Assert.IsTrue(objOrderAddressProcessor.ErrorMessages.Count == 0);
        } 
        #endregion

        // ON HOLD - FOR DEMO OR ADDITIONAL TESTING -------------------------------------------------------------------------------------

    }
}
