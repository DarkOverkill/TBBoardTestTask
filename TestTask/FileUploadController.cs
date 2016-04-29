using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TestTask
{
    public class FileUploadController : ApiController
    {
        [HttpPost]
        public KeyValuePair<bool, string> UploadFile()
        {
            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Current.Request.Files["UploadFile"];
                    var id = HttpContext.Current.Request["Folder"];
                    if (httpPostedFile != null)
                    {
                        Repository.GetRepository().AddFile(httpPostedFile, Convert.ToInt32(id));
                        return new KeyValuePair<bool, string>(true, id);
                    }

                    return new KeyValuePair<bool, string>(true, "Could not get the uploaded file.");
                }

                return new KeyValuePair<bool, string>(true, "No file found to upload.");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, "An error occurred while uploading the file. Error Message: " + ex.Message);
            }
        }
    }
}