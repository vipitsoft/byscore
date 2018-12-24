using System;
namespace BYSCORE.Entity
{
    public class PublicQuery
    {
        public int PageSize { get; set; }

        public int Page { get; set; }

        public string Name { get; set; }

        public string AddTime { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        /// <value>The level.</value>
        public string Level { get; set; }

    }
}
