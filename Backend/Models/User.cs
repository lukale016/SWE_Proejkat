using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Projekat.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Column("Username")]
        [DataType("nvarchar(13)")]
        public string Username { get; set; }

        [Column("Password")]
        [DataType("nvarchar(255)")]
        public string Password { get; set; }

        [Column("Name")]
        [DataType("nvarchar(255)")]
        public string Name { get; set; }

        [Column("Surname")]
        [DataType("nvarchar(255)")]
        public string Surname { get; set; }

        [Column("City")]
        [DataType("nvarchar(255)")]
        public string City { get; set; }

        [Column("ProfileImg")]
        [DataType("nvarchar(255)")]
        public string ProfileImg { get; set; }

        [Column("Bio")]
        [DataType("nvarchar(2500)")]
        public string Bio { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<Friend> Friends { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<Event> EventsOwned { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<Interested> EventsInterested { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<PendingRequest> SentRequests { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<PendingRequest> PendingRequests { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<FavoriteRecord> FavoriteRecords { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<Comment> PostedComments { get; set; }

        [Column("IsOwner")]
        [DataType("int")]
        public int IsOwner { get; set; }

        [NotMapped]
        [ForeignKey("Username")]
        public virtual ICollection<Caffe> OwnedCaffes { get; set; }

        [Column("IsAdmin")]
        [DataType("int")]
        public int IsAdmin { get; set; }
    }
}