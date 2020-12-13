using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Level3.AddressManagement.DAL;
using Level3.AddressManagement.BLL;
using System.Collections.Generic;

namespace Level3.AddressManagement.UnitTest
{
    [TestClass]
    public class CDWOrderAddressTester
    {
        [TestMethod]
        public void Test_GetRecsFromDB()
        {
            string strSQL = SQLQueryBuilder.GetSQLStatement_DWOrderAddressesNotInGLM();
            DAL.CDWOrderAddressRecord objCDWQuery = new CDWOrderAddressRecord();
            List<CDWOrderAddressRecord> lstResults = objCDWQuery.GetRecords(strSQL);
            Assert.IsTrue(lstResults.Count > 0);

        }


        [TestMethod]
        public void Test_GetRecsFromDB_Changeset()
        {
            DateTime dteDateTimeCutoff = new DateTime(2018, 04, 01, 13, 59, 59);

            string strSQL = SQLQueryBuilder.GetSQLStatement_DWOrderAddressesChangeset(dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff);
            DAL.CDWOrderAddressRecord objCDWChangesetQuery = new CDWOrderAddressRecord();
            List<CDWOrderAddressRecord> lstResults = objCDWChangesetQuery.GetRecords(strSQL);
            Assert.IsTrue(lstResults.Count > 0);

        }


        [TestMethod]
        public void Test_GetRecsFromDB_Changeset_DYNAMIC()
        {
            DateTime dteDateTimeCutoff = new DateTime(2018, 04, 01, 13, 59, 59);

            string strSQL = DynamicQueryBuilder.GetDynamicManagedServiceOrdersQuery(dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff, dteDateTimeCutoff);

            DAL.CDWOrderAddressRecord objCDWChangesetQuery = new CDWOrderAddressRecord();
            List<CDWOrderAddressRecord> lstResults = objCDWChangesetQuery.GetRecords(strSQL);
            Assert.IsTrue(lstResults.Count > 0);

        }




    }
}
