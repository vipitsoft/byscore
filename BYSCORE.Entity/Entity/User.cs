using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BYSCORE.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("user")]
    public class User : BaseEntity<int>
    {

        /// <summary>
        /// 角色id
        /// </summary>
        /// <value></value>
        [Column("role_id")]
        public int RoleId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <value>The name of the user.</value>
        /// 
        [Column("name"), StringLength(32), Required]
        public string Name { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        /// <value>The user number.</value>
        /// 
        [Column("user_number"), StringLength(128), Required]
        public string UserNumber { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <value>The name of the user.</value>
        /// 
        [Column("user_name"), StringLength(128), Required]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>The phone.</value>
        [Column("phone"), StringLength(32), Required]
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        [Column("password"), StringLength(32), Required]
        public string PassWord { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        /// <value>The phone.</value>
        [Column("email"), StringLength(128), Required]
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <value>The head image.</value>
        /// 
        [Column("head_img"), StringLength(128), Required]
        public string HeadImg { get; set; }


        /// <summary>
        /// 是否冻结
        /// </summary>
        /// <value><c>true</c> if is freeze; otherwise, <c>false</c>.</value>
        /// 
        [Column("is_freeze")]
        public bool IsFreeze { get; set; } = false;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        /// <value>The last login time.</value>
        [Column("last_login_time")]
        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 部门id
        /// </summary>
        /// <value>The department identifier.</value>
        [Column("department_id")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 区域id
        /// </summary>
        /// <value>The area identifier.</value>
        /// 
        [Column("area_id")]
        public int AreaId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        /// <value>The name of the role.</value>
        public string RoleName { get; set; }

        public virtual Role Role { get; set; }

        public virtual ICollection<UserMenu> UserMenus { get; set; }

        public List<int> MenuIds { get; set; }

        public string DepartmentName { get; set; }
        public string AreaName { get; set; }

        public virtual ConfigInfo Department { get; set; }

        public virtual ConfigInfo Area { get; set; }

        /// <summary>
        /// 1.base 2.imgHead
        /// </summary>
        public string UpType { get; set; }

    }
}
