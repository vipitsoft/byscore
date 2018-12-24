using System;
using System.Collections;
using System.Collections.Generic;

namespace BYSCORE.Entity
{
    public class PublicView
    {
        public int TotalCount { get; set; }

        public IEnumerable<Product> ProductList { get; set; }

        public IEnumerable<ApplicationLog> ApplicationLogList { get; set; }
    }
}
