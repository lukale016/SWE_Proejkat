using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        [Column("id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("RecordRef")]
        [DataType("int")]
        public int RecordRef { get; set; }

        [NotMapped]
        public virtual Record Record { get; set; }

        [Column("Title")]
        [DataType("nvarchar(255)")]
        public string Title { get; set; }
    }
}