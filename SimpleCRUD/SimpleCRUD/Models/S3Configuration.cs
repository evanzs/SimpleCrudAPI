using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public class S3Configuration
    {
        public string BucketName { get; set; }
        public string AcessKey { get; set; }
        public string SecretKey { get; set; }

    }
}
