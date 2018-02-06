using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiApp.Models.Domain
{
    public class DeleteFile
    {
        public int FileId { get; set; }
        public string SystemFileName { get; set; }
    }
}