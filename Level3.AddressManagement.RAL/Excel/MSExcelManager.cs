using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using Syncfusion.XlsIO;

namespace Level3.AddressManagement.RAL
{
    public class MSExcelManager
    {
        // Declare the static logger object that will be used to implement logging via log4net
        private static ILog _objLogger = LogManager.GetLogger(typeof(MSExcelManager));

        //https://help.syncfusion.com/file-formats/xlsio/working-with-excel-worksheet

        // Private Member
        private string _strBaseLoggingString;
        private string _strInputFileName;

        private ExcelEngine _objExcelEngine;
        private IWorkbook _objWorkbook;
        private IWorksheet _objWorksheet;

        private int _intIndex_Address = 3;
        private int _intIndex_City = 4;
        private int _intIndex_Zip = 5;
        private int _intIndex_State = 6;
        private int _intIndex_Country = 7;
        private int _intIndex_Floor = 8;
        private int _intIndex_Room = 9;
        private int _intIndex_Building = 10;


        // Public Properties
        private List<string> _lstErrorMessages;
        public List<string> ErrorMessages
        {
            get { return _lstErrorMessages; }
            set { _lstErrorMessages = value; }
        }


        private List<Model.SourceAddress> _lstSourceAdddresses;
        public List<Model.SourceAddress> SourceAddresses
        {
            get { return _lstSourceAdddresses; }
            set { _lstSourceAdddresses = value; }
        }


        // Constructor
        public MSExcelManager()
        {
            _lstErrorMessages = new List<string>();
            _lstSourceAdddresses = new List<Model.SourceAddress>();
        }


        // Public Methods
        public bool Load(string strFileName)
        {

            try
            {
                _strInputFileName = strFileName;
                _strBaseLoggingString = String.Concat("FileName = [", _strInputFileName, "]");

                // Instantiate the Excel Engine
                _objExcelEngine = new ExcelEngine();
                _objExcelEngine.ThrowNotSavedOnDestroy = true;

                // Open the workbook
                _objWorkbook = _objExcelEngine.Excel.Workbooks.Open(_strInputFileName, ExcelOpenType.Automatic);
                _objWorksheet = _objWorkbook.Worksheets[0];

                _objLogger.Info(String.Concat("MS Excel Document found and opened to the first worksheet. ", _strBaseLoggingString));

            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an Error while trying to locate and OPEN the MS Excel file.  Error Message = [", ex.Message, "], ", _strBaseLoggingString));
            }

            return (_lstErrorMessages.Count == 0);
        }
        public bool ReadWorksheetRowsIntoSourceAddressObjects()
        {
            try
            {
                int intStartingRowZeroBased = 1; // Row 0 is the header row, so start reading at row 1
                int intNumberOfRows = _objWorksheet.UsedRange.Rows.Count() - 1; // Subtract one to account for the header row


                // Iterate over all the used rows in the worksheeet
                int intCounter = 0;
                foreach (IRange row in _objWorksheet.UsedRange.Rows)
                {
                    // Skip the header row 
                    if (intCounter < intStartingRowZeroBased)
                    {
                        intCounter++;
                        // Skip this row
                        continue;
                    }

                    if (intCounter >= intNumberOfRows)
                    {
                        // We've reached the stopping point, so exit the loop and close the workbook
                        break;
                    }

                    // Continue to increment a counter for logging purposes
                    intCounter++;

                    // Write to the log file so we can track progress since this is a long running process
                    if (intCounter % 10 == 0)
                    {
                        decimal dcmPercentComplete = intCounter / intNumberOfRows;
                        string strPercentageComplete = String.Format("Value: {0:P2}.", dcmPercentComplete);

                        _objLogger.Info(String.Format("Processing progress... {0} of {1} address lines have been read.  {2} Complete", intCounter.ToString(), intNumberOfRows.ToString(), strPercentageComplete));
                    }

                    // Try to translate the spreadsheet row into a SourceAddress model object
                    try
                    {
                        ProcessRow(row);
                    }
                    catch (Exception ex)
                    {

                        string strErrorMessage = String.Concat("There was an ERROR while trying to process a row in the spreadsheet.  Row Number = [", intCounter.ToString(), "], Error Message = [", ex.Message, "]");
                        _objLogger.Warn(strErrorMessage);
                        _lstErrorMessages.Add(strErrorMessage);
                        // Skip to the next record
                        continue;
                    }
                }

                // Close the MS Excel File
                CloseFile();

                // If we get here, then the spreadsheet could be iterated over until its final row, and was closed.
                return true;
            }
            catch (Exception ex)
            {
                string strErrorMessage = String.Format("There was an error while trying to read the data from the MS Excel Spreadsheet.  This is a hard exception, and so none of the addresses in the spreadsheet supplied can be processed.  {0},  Error Message = [{1}]", _strBaseLoggingString, ex.Message);

                _objLogger.Warn(String.Format("An exception is about to be thrown.  {0}", strErrorMessage));

                _lstErrorMessages.Add(strErrorMessage);

                // Something went wrong, so tell the caller
                return false;
            }
        }


        // Private methods
        private void ProcessRow(IRange row)
        {
            // Translate the row object to an address object.  This throws an exception is the street fields are not potentially valid, etc.
            Model.SourceAddress objSourceAddress = new Model.SourceAddress();

            TranslateRowToAddress(row, out objSourceAddress);

            // Add the address to the list 
            _lstSourceAdddresses.Add(objSourceAddress);
        }
        private void TranslateRowToAddress(IRange row, out Model.SourceAddress objSourceAddress)
        {
            objSourceAddress = new Model.SourceAddress();

            // Build an address object out of the row data
            // Get the values from the spreadsheet
            objSourceAddress.Address1 = row.Cells[_intIndex_Address].Value;
            objSourceAddress.City = row.Cells[_intIndex_City].Value;
            objSourceAddress.Zip = row.Cells[_intIndex_Zip].Value;
            objSourceAddress.State = row.Cells[_intIndex_State].Value;
            objSourceAddress.Country = row.Cells[_intIndex_Country].Value;
            objSourceAddress.Floor = row.Cells[_intIndex_Floor].Value;
            objSourceAddress.Room = row.Cells[_intIndex_Room].Value;
            objSourceAddress.Building = row.Cells[_intIndex_Building].Value;
        }
        private bool CloseFile()
        {
            try
            {
                // Close the instance of IWorkbook.
                _objWorkbook.Close();

                // Dispose the instance of ExcelEngine.
                _objExcelEngine.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                _lstErrorMessages.Add(String.Concat("There was an Error while trying to close the MS Excel file.  Error Message = [", ex.Message, "], ", _strBaseLoggingString));
                return false;
            }
        }



    }
}
