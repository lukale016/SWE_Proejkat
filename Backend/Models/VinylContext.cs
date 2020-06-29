using Microsoft.EntityFrameworkCore;

namespace Projekat.Models
{
    public class VinylContext : DbContext
    {
        public VinylContext(DbContextOptions<VinylContext> options):base(options){}

        public DbSet<Caffe> Caffes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FavoriteRecord> FavoriteRecords { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<PendingRequest> PendingRequests { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Interested> Interested { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Comment>().HasOne<Record>(c => c.BoundRecord).WithMany(r => r.Comments).HasForeignKey(c => c.BoundRecordRef).OnDelete(DeleteBehavior.ClientCascade);
            mb.Entity<Comment>().HasOne<User>(c => c.Owner).WithMany(u => u.PostedComments).HasForeignKey(c => c.OwnerRef).OnDelete(DeleteBehavior.ClientCascade);
            
            mb.Entity<PendingRequest>().HasKey(k => new {k.UserSentRef, k.UserReceavedRef});
            mb.Entity<PendingRequest>().HasOne<User>(pr => pr.UserSent).WithMany(u => u.SentRequests).HasForeignKey(pr => pr.UserSentRef).OnDelete(DeleteBehavior.ClientCascade);
            mb.Entity<PendingRequest>().HasOne<User>(pr => pr.UserReceaved).WithMany(u => u.PendingRequests).HasForeignKey(pr => pr.UserReceavedRef).OnDelete(DeleteBehavior.ClientCascade);

            mb.Entity<Friend>().HasKey(k => new {k.User1Ref, k.User2Ref});
            mb.Entity<Friend>().HasOne<User>(f => f.User1).WithMany(u => u.Friends).HasForeignKey(f => f.User1Ref).OnDelete(DeleteBehavior.ClientCascade);

            mb.Entity<FavoriteRecord>().HasKey(fr => new {fr.UserRef, fr.RecordRef});
            mb.Entity<FavoriteRecord>().HasOne<User>(fr => fr.User).WithMany(u => u.FavoriteRecords).HasForeignKey(fr => fr.UserRef).OnDelete(DeleteBehavior.ClientCascade);

            mb.Entity<Interested>().HasKey(k => new {k.EventRef, k.AttenderRef});
            mb.Entity<Interested>().HasOne<Event>(i => i.Event).WithMany(e => e.Interested).HasForeignKey(i => i.EventRef).OnDelete(DeleteBehavior.ClientCascade);
            mb.Entity<Interested>().HasOne<User>(i => i.Attender).WithMany(u => u.EventsInterested).HasForeignKey(i => i.AttenderRef).OnDelete(DeleteBehavior.ClientCascade);

            mb.Entity<Event>().HasOne<User>(e => e.Owner).WithMany(u => u.EventsOwned).HasForeignKey(e => e.OwnerRef).OnDelete(DeleteBehavior.ClientSetNull);
            mb.Entity<Event>().HasOne<Caffe>(e => e.Caffe).WithMany(c => c.OrganizedEvents).HasForeignKey(e => e.CaffeRef).OnDelete(DeleteBehavior.ClientCascade);

            mb.Entity<Caffe>().HasOne<User>(c => c.Owner).WithMany(u => u.OwnedCaffes).HasForeignKey(c => c.OwnerRef).OnDelete(DeleteBehavior.ClientCascade);
            mb.Entity<Caffe>().HasIndex(c => c.Name);

            mb.Entity<Song>().HasOne(s => s.Record).WithMany(r => r.Songs).HasForeignKey(s => s.RecordRef).OnDelete(DeleteBehavior.ClientCascade);
        }    
    }
}