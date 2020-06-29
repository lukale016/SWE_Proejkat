using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("FavoriteRecord")]
    public class FavoriteRecord
    {
        [Column("UserRef")]
        [DataType("nvarchar(13)")]
        public string UserRef { get; set; }

        [NotMapped]
        [ForeignKey("UserRef")]
        public virtual User User { get; set; }

        [Column("RecordRef")]
        [DataType("int")]
        public int RecordRef { get; set; }

        [NotMapped]
        [ForeignKey("RecordRef")]
        public virtual Record Record { get; set; }
    }
}