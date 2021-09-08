using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Delivery.SelfServiceKioskApi.DbModel
{
    public partial class DeliveryKioskApiContext : DbContext
    {
        public DeliveryKioskApiContext()
        {
        }

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
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:10.0.1.41,51433;Initial Catalog=DeliveryKioskApi;Persist Security Info=False;User ID=sa;Password=queue@2020");
            }
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
