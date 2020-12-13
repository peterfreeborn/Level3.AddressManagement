using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Level3.AddressManagement.UI.Web
{
    public enum FileDisplayContentMimeTypes
    {
        Pdf,
        Jpeg,
        Gif,
        Bmp,
        Png,
        Xlsx,
        Docx
    }

    public partial class FileDisplayHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO:  add some error handling and expose errors to users
            // TODO:  move the enums to their own folders
            // TODO:  centralize the logic here for reuse in other places if appropriate.

            string strFileName = Request.QueryString["strFileName"];

            if (!(String.IsNullOrEmpty(strFileName)))
            {
                GetFileBytesFromDiskAndPushToBrowser(strFileName);
            }

        }

        public void GetFileBytesFromDiskAndPushToBrowser(string strFileName)
        {
            FileDisplayContentMimeTypes eMimeType;

            string strAppDataDirectoryLocation = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/");

            byte[] aBuffer = null;

            FileStream objFile = null;
            try
            {

                string strFullPath = Path.Combine(strAppDataDirectoryLocation, strFileName);
                objFile = File.OpenRead(strFullPath);
                aBuffer = new byte[objFile.Length];
                objFile.Read(aBuffer, 0, Convert.ToInt32(objFile.Length));
                objFile.Close();
                objFile.Dispose();
                objFile = null;

                eMimeType = GetMimeTypeForFileExtension(Path.GetExtension(strFullPath).ToLower());
            }
            finally
            {
                if (objFile != null)
                {
                    objFile.Dispose();
                    objFile = null;
                }
            }


            if (aBuffer != null)
            {
                if (String.IsNullOrEmpty(strFileName) == false)
                {
                    Page.Title = strFileName;
                }
                else
                {
                    Page.Title = "AddressManagement_SystemDesignDoc." + eMimeType.ToString(); ;
                    strFileName = Page.Title;
                }


                SendFileToBrowser(strFileName, aBuffer, eMimeType, Response);
            }

        }

        public void SendFileToBrowser(string strFileName, byte[] aBuffer, FileDisplayContentMimeTypes eMimeType, System.Web.HttpResponse Response)
        {
            // Deliver the file bytes to the browser.
            Response.Clear();

            // .xlsx   application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
            // .xltx   application/vnd.openxmlformats-officedocument.spreadsheetml.template
            // .potx   application/vnd.openxmlformats-officedocument.presentationml.template
            // .ppsx   application/vnd.openxmlformats-officedocument.presentationml.slideshow
            // .pptx   application/vnd.openxmlformats-officedocument.presentationml.presentation
            // .sldx   application/vnd.openxmlformats-officedocument.presentationml.slide
            // .docx   application/vnd.openxmlformats-officedocument.wordprocessingml.document
            // .dotx   application/vnd.openxmlformats-officedocument.wordprocessingml.template
            // .xlam   application/vnd.ms-excel.addin.macroEnabled.12
            // .xlsb   application/vnd.ms-excel.sheet.binary.macroEnabled.12
            // .doc    application/msword
            // .dot    application/msword
            // .xls    application/vnd.ms-excel
            // .xlt    application/vnd.ms-excel
            // .xla    application/vnd.ms-excel
            // .ppt    application/vnd.ms-powerpoint
            // .pot    application/vnd.ms-powerpoint
            // .pps    application/vnd.ms-powerpoint
            // .ppa    application/vnd.ms-powerpoint

            switch (eMimeType)
            {
                case FileDisplayContentMimeTypes.Pdf:
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Location", "Gemini Document");
                    Response.AddHeader("Content-Disposition", String.Concat("inline;filename=\"", strFileName, "\""));
                    break;

                case FileDisplayContentMimeTypes.Jpeg:
                    Response.ContentType = "image/JPEG";
                    break;

                case FileDisplayContentMimeTypes.Gif:
                    Response.ContentType = "image/gif";
                    break;

                case FileDisplayContentMimeTypes.Bmp:
                    Response.ContentType = "image/bmp";
                    break;

                case FileDisplayContentMimeTypes.Png:
                    Response.ContentType = "image/png";
                    break;

                case FileDisplayContentMimeTypes.Docx:
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.AddHeader("Content-Disposition", String.Concat("attachment;filename=\"", strFileName, "\""));
                    break;

                case FileDisplayContentMimeTypes.Xlsx:
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", String.Concat("attachment;filename=\"", strFileName, "\""));
                    break;

                default:
                    Response.ContentType = "image/bmp";
                    break;
            }

            //Response.AddHeader("Content-Length", wre.ContentLength.ToString);
            Response.BinaryWrite(aBuffer);
            Response.Flush();
            Response.End();
        }

        public FileDisplayContentMimeTypes GetMimeTypeForFileExtension(string strFileExtension)
        {
            switch (strFileExtension.ToLower())
            {
                case ".pdf":
                    return FileDisplayContentMimeTypes.Pdf;

                case ".jpeg":
                    return FileDisplayContentMimeTypes.Jpeg;

                case ".jpg":
                    return FileDisplayContentMimeTypes.Jpeg;

                case ".gif":
                    return FileDisplayContentMimeTypes.Gif;

                case ".bmp":
                    return FileDisplayContentMimeTypes.Bmp;

                case ".png":
                    return FileDisplayContentMimeTypes.Png;

                case ".xls":
                    return FileDisplayContentMimeTypes.Xlsx;

                case ".xlsx":
                    return FileDisplayContentMimeTypes.Xlsx;

                case ".doc":
                    return FileDisplayContentMimeTypes.Docx;

                case ".docx":
                    return FileDisplayContentMimeTypes.Docx;

                default:
                    return FileDisplayContentMimeTypes.Jpeg;
            }


        }
    }
}