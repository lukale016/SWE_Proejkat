using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Projekat.Models
{
    [Table("Caffe")]
    public class Caffe
    {
        [Key]
        [Column("Id")]
        [DataType("int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name")]
        [DataType("nvarchar(255)")]
        public string Name { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Address")]
        [DataType("nvarchar(255)")]
        public string Address { get; set; }

        [Column("Info")]
        [DataType("nvarchar(2500)")]
        public string Info { get; set; }

        [Column("BackgroundImg")]
        [DataType("nvarchar(255)")]
        public string BackgroundImg { get; set; }

        [Column("OwnerRef")]
        [DataType("nvrachar(13)")]
        public string OwnerRef { get; set; }

        [NotMapped]
        [ForeignKey("OwnerRef")]
        public virtual User Owner { get; set; }

        [Column("Long")]
        [DataType("float")]
        public double Long { get; set; }

        [Column("Lat")]
        [DataType("float")]
        public double Lat { get; set; }

        [NotMapped]
        public virtual ICollection<Event> OrganizedEvents { get; set; }
    }
}