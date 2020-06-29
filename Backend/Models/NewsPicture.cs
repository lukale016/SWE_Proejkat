using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("NewsPicture")]
    public class NewsPicture
    {
        [Column("NewsRef")]
        [DataType("int")]
        public int NewsRef { get; set; }

        [Column("Image")]
        [DataType("nvarchar(255)")]
        public string Image { get; set; }

        [NotMapped]
        [ForeignKey("NewsRef")]
        public virtual News News { get; set; }
    }
}