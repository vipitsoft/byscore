using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYSCORE.Entity
{
    public class ApplicationLog
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("application"), StringLength(50), Required]
        public string Application { get; set; }

        [Column("logged")]
        public DateTime Logged { get; set; }

        [Column("level"), StringLength(50), Required]
        public string Level { get; set; }

        [Column("message"), StringLength(512), Required]
        public string Message { get; set; }

        [Column("logger"), StringLength(250)]
        public string Logger { get; set; }

        [Column("callsite"), StringLength(512)]
        public string Callsite { get; set; }

        [Column("exception"), StringLength(512)]
        public string Exception { get; set; }

        [Column("text")]
        public string Text { get; set; }
    }
}
