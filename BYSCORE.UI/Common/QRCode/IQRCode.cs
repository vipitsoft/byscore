using System;
using System.Drawing;

namespace BYSCORE.UI
{
    public interface IQRCode
    {
        Bitmap GetQRCode(string url, int pixel);
    }
}
