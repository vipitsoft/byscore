using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 配置信息表
    /// </summary>
    [Table("config_info")]
    public class ConfigInfo : BaseEntity<int>
    {
        /// <summary>
        /// 类型
        /// </summary>
        /// <value>The type.</value>
        [Column("type")]
        public int Type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <value>The name.</value>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remarks.</value>
        [Column("remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        /// <value>The parent identifier.</value>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [Column("sort_number")]
        public int? SortNumber { get; set; }


        public string ParentName { get; set; }

    }
}
