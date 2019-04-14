using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    [Table("role_menu")]
    public class RoleMenu : BaseEntity<int>
    {
        /// <summary>
        /// 角色id
        /// </summary>
        /// <value></value>
        /// 
        [Column("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        /// <value>The menu id.</value>
        /// 
        [Column("menu_id")]
        public int MenuId { get; set; }
    }
}
