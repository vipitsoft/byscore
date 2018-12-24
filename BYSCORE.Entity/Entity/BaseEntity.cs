using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <value>The identifier.</value>
        /// 
        [Column("id")]
        public T Id { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        /// <value>The add time.</value>
        /// 
        [Column("addtime")]
        public DateTime AddTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否删除
        /// </summary>
        /// <value><c>true</c> if is delete; otherwise, <c>false</c>.</value>
        /// 
        [Column("isdelete")]
        public bool IsDelete { get; set; } = false;
    }
}
