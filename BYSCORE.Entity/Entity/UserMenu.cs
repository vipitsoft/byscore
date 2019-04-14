using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 用户权限表
    /// </summary>
    /// 
    [Table("user_menu")]
    public class UserMenu : BaseEntity<int>
    {
        /// <summary>
        /// 用户code
        /// </summary>
        /// <value>The user identifier.</value>
        /// 
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        /// <value>The menu id.</value>
        [Column("menu_id")]
        public int MenuId { get; set; }


    }
}
