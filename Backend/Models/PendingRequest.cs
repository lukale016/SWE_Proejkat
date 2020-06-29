using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("PendingRequest")]
    public class PendingRequest
    {
        [Column("UserSentRef")]
        [DataType("nvarchar(13)")]
        public string UserSentRef { get; set; }

        [Column("UserReceavedRef")]
        [DataType("nvarchar(13)")]
        public string UserReceavedRef { get; set; }

        [NotMapped]
        [ForeignKey("UserSent")]
        public virtual User UserSent { get; set; }

        [NotMapped]
        [ForeignKey("UserReceaved")]
        public virtual User UserReceaved { get; set; }
    }
}