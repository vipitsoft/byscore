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
        /// code
        /// </summary>
        /// <value>The code.</value>
        [Column("code")]
        public string Code { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 添加时间
        /// </summary>
        /// <value>The add time.</value>
        /// 
        [Column("created_time")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否删除
        /// </summary>
        /// <value><c>true</c> if is delete; otherwise, <c>false</c>.</value>
        /// 
        [Column("is_delete")]
        public bool IsDelete { get; set; } = false;
    }
}
