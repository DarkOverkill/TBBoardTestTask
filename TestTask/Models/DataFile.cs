using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask.Models
{
    public class DataFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int ParentFolderId { get; set; }
    }
}