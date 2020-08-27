using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageUploadNetCore.Models
{
    public class UploadImageViewModel
    {
        public string FullName { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
    }
}
