using System;
namespace BYSCORE.Entity
{
    public class LogQuery
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public object Obj { get; set; }
    }
}
