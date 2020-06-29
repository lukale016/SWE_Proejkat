using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("News")]
    public class News
    {
        [Key]
        [Column("Id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("TitleSmall")]
        [DataType("nvarchar(255)")]
        public string TitleSmall { get; set; }

        [Column("TitleLarge")]
        [DataType("nvarchar(255)")]
        public string TitleLarge { get; set; }

        [Column("TextSmall")]
        [DataType("nvarchar(2500)")]
        public string TextSmall { get; set; }

        [Column("TextLarge")]
        [DataType("nvarchar(10000)")]
        public string TextLarge { get; set; }

        [Column("ImageSmall")]
        [DataType("nvarchar(255)")]
        public string ImageSmall { get; set; }

        [Column("ImageLarge")]
        [DataType("nvarchar(255)")]
        public string ImageLarge { get; set; }
    }
}