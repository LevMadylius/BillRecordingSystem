namespace BillRecordingSystem.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBExpenceContext : DbContext
    {
        public DBExpenceContext()
            : base("name=DBExpenceContext")
        {
        }

        public virtual DbSet<Expences> Expences { get; set; }
        public virtual DbSet<ExpenceTypes> ExpenceTypes { get; set; }
        public virtual DbSet<LoginInfo> LoginInfo { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expences>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Expences>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenceTypes>()
                .HasMany(e => e.Expences)
                .WithRequired(e => e.ExpenceTypes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoginInfo>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<LoginInfo>()
                .Property(e => e.Pass)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PictureLink)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Expences)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.LoginInfo)
                .WithRequired(e => e.Users)
                .WillCascadeOnDelete(false);
        }
    }
}
