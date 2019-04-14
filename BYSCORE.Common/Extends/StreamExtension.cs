using System;
using System.IO;

namespace BYSCORE.Common
{
    public static class StreamExtension
    {
        public static string StreamToBase64(this Stream stream)
        {
            using (MemoryStream msForRead = new MemoryStream())
            {
                stream.CopyTo(msForRead);
                return Convert.ToBase64String(msForRead.ToArray());
            }

        }
    }
}
