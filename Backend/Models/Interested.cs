using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Interested")]
    public class Interested
    {
        [Column("EventRef")]
        [DataType("int")]
        public int EventRef { get; set; }

        [NotMapped]
        [ForeignKey("EventRef")]
        public virtual Event Event { get; set; }

        [Column("AttenderRef")]
        [DataType("nvrachar(13)")]
        public string AttenderRef { get; set; }

        [NotMapped]
        [ForeignKey("AttenderRef")]
        public virtual User Attender { get; set; }
    }
}