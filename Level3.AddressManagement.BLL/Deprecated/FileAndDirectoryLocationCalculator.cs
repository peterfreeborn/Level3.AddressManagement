using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Level3.AddressManagement.BLL
{
    public class FileAndDirectoryLocationCalculator
    {
        // Private Members
        private string _strWorkspaceRootDirectory;
        private string _strProcessingRoot;
        private string _strArchiveRoot;

        // Constructor
        public FileAndDirectoryLocationCalculator()
        {
            _strWorkspaceRootDirectory = ConfigHelper.GetRootDirectory();
            _strProcessingRoot = Path.Combine(_strWorkspaceRootDirectory, "Processing");
            _strArchiveRoot = Path.Combine(_strWorkspaceRootDirectory, "Archive");

        }


        // Public Methods
        public string GetWorkSpaceRootDirectory()
        {
            return _strWorkspaceRootDirectory;
        }
        public string GetProcessingRoot()
        {
            return _strProcessingRoot;
        }
        public string GetArchiveRoot()
        {
            return _strArchiveRoot;
        }

    }
}
