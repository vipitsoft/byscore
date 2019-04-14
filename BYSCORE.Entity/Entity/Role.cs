using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Table("role")]
    public class Role : BaseEntity<int>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        /// <value>The name of the role.</value>
        /// 
        [Column("c_name"), StringLength(32), Required]
        public string CName { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        /// <value>The name of the role.</value>
        /// 
        [Column("e_name"), StringLength(32), Required]
        public string EName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        /// <value>The description.</value>
        [Column("description"), StringLength(1024), Required]
        public string Description { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public List<int> MenuIds { get; set; }

    }
}
