using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekat.Models
{
    [Table("Event")]
    public class Event
    {
        [Key]
        [Column("Id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Title")]
        [DataType("nvarchar(255)")]
        public string Title { get; set; }

        [Column("Info")]
        [DataType("nvarchar(2500)")]
        public string Info { get; set; }

        [Column("Owner")]
        [DataType("nvarchar(13)")]
        public string OwnerRef { get; set; }

        [NotMapped]
        [ForeignKey("Owner")]
        public virtual User Owner { get; set; }

        [NotMapped]
        public virtual ICollection<Interested> Interested { get; set; }

        [Column("Caffe")]
        [DataType("int")]
        public int CaffeRef { get; set; }

        [NotMapped]
        public virtual Caffe Caffe { get; set; }

        [Column("Time")]
        [DataType("nvarchar(255)")]
        public string Time { get; set; }

        [Column("Date")]
        [DataType("nvarchar(255)")]
        public string Date { get; set; }

        [Column("Modifier")]
        [DataType("char(1)")]
        public string Modifier { get; set; }
    }
}