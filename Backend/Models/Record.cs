
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Record")]
    public class Record
    {
        [Key]
        [Column("Id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("Name")]
        [DataType("nvarchar(255)")]
        public string Name { get; set; }
        
        [Column("DisplayImg")]
        [DataType("nvarchar(255)")]
        public string DisplayImg { get; set; }
        
        [Column("Info")]
        [DataType("nvarchar(2500)")]
        public string Info { get; set; }
        
        [Column("Category")]
        [DataType("nvarchar(255)")]
        public string Category { get; set; }
        
        [Column("Author")]
        [DataType("nvarchar(255)")]
        public string Author { get; set; }

        [Column("Demo")]
        [DataType("nvarchar(255)")]
        public string Demo { get; set; }

        [NotMapped]
        [ForeignKey("Id")]
        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public virtual ICollection<Song> Songs { get; set; }
    }
}