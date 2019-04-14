using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace BYSCORE.UI
{
    public class UploadModel
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }


    }
}
