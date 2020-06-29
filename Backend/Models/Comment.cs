using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [Column("Id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("BoundRecordRef")]
        [DataType("int")]
        public int BoundRecordRef { get; set; }

        [NotMapped]
        [ForeignKey("BoundRecordRef")]
        public virtual Record BoundRecord { get; set; }

        [Column("OwnerRef")]
        [DataType("nvrachar(13)")]
        public string OwnerRef { get; set; }

        [NotMapped]
        [ForeignKey("OwnerRef")]
        public virtual User Owner { get; set; }

        [Column("Content")]
        [DataType("nvrachar(2500)")]
        public string Content { get; set; }
    }
}