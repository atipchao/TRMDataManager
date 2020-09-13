using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRMDataManager.Models
{
    public class DataFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileOrgName { get; set; }
        public string FileStatus { get; set; }
        public string FileURI { get; set; }
        public string FileScanId { get; set; }
        public DateTime? FileDateCreate { get; set; }
        public DateTime? FileDateUpdate { get; set; }
        public string FileUserStmp { get; set; }
    }
}