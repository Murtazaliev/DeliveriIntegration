using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Delivery.SelfServiceKioskApi.DbModel
{
    public partial class DeliveryKioskApiContext : DbContext
    {
        //private static DeliveryKioskApiContext _instance;

        //public static DeliveryKioskApiContext GetInstance()
        //{
        //    if (_instance == null)
        //    {
        //        _instance = new DeliveryKioskApiContext();
        //    }
        //    return _instance;
        //}
        public DeliveryKioskApiContext(DbContextOptions<DeliveryKioskApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QueueRequest> QueueRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<QueueRequest>(entity =>
            {
                entity.ToTable("QueueRequest");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
