using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 菜单表
    /// </summary>
    /// 
    [Table("menu")]
    public class Menu : BaseEntity<int>
    {
        /// <summary>
        /// 父级id
        /// </summary>
        /// <value>The parent identifier.</value>
        [Column("parent_id")]
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单等级
        /// </summary>
        /// <value>The level .</value>
        [Column("level")]
        public int Level { get; set; }

        /// <summary>
        /// 菜单名称(中文)
        /// </summary>
        /// <value>The name.</value>
        /// 
        [Column("c_name"), StringLength(128), Required]
        public string CName { get; set; }

        /// <summary>
        /// 菜单名称(英文)
        /// </summary>
        /// <value>The name.</value>
        /// 
        [Column("e_name"), StringLength(128), Required]
        public string EName { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        /// <value>The URL.</value>
        [Column("url"), StringLength(128)]
        public string Url { get; set; }

        /// <summary>
        /// 菜单类型 类型：0导航菜单；1操作按钮。
        /// </summary>
        /// <value>The type.</value>
        [Column("type")]
        public int Type { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        /// <value>The icon.</value>
        [Column("icon"), StringLength(32)]
        public string Icon { get; set; }

        /// <summary>
        /// 排序，数字越小越靠前
        /// </summary>
        /// <value>The type.</value>
        [Column("sort")]
        public int Sort { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        /// <value>The remarks.</value>
        [Column("remarks"), StringLength(1024)]
        public string Remarks { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
