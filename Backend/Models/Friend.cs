using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Friend")]
    public class Friend
    {
        [Column("User1Ref")]
        [DataType("nvarchar(13)")]
        public string User1Ref { get; set; }

        [NotMapped]
        [ForeignKey("User1Ref")]
        public virtual User User1 { get; set; }

        [Column("User2Ref")]
        [DataType("nvarchar(13)")]
        public string User2Ref { get; set; }
    }
}