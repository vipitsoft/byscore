using Microsoft.AspNetCore.Http;

namespace BYSCORE.UI
{
    public class UploadModel
    {
        public IFormFile file { get; set; }
        public string FileName { get; set; }


    }
}
