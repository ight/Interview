using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;

namespace Insurance.Models
{
    public partial class InsuranceDBContext : DbContext
    {
        public InsuranceDBContext()
        {
        }

        public InsuranceDBContext(DbContextOptions<InsuranceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<CoveragePlan> CoveragePlan { get; set; }
        public virtual DbSet<RateChart> RateChart { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
             optionsBuilder.UseSqlServer("Data Source=SERVER_NAME;Initial Catalog=DATABASE_NAME;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.Property(e => e.CoveragePlan)
                    .IsRequired()
                    .HasColumnName("Coverage_Plan")
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerAddress)
                    .IsRequired()
                    .HasColumnName("Customer_Address")
                    .IsUnicode(false);

                entity.Property(e => e.CustomerCountry)
                    .IsRequired()
                    .HasColumnName("Customer_Country")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerDateOfBirth)
                    .HasColumnName("Customer_Date_Of_Birth")
                    .HasColumnType("date");

                entity.Property(e => e.CustomerGender)
                    .IsRequired()
                    .HasColumnName("Customer_Gender")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasColumnName("Customer_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NetPrice)
                    .HasColumnName("Net_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SaleDate)
                    .HasColumnName("Sale_Date")
                    .HasColumnType("date");

                entity.HasOne(d => d.CoveragePlanNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CoveragePlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contracts_CoveragePlan");
            });

            modelBuilder.Entity<CoveragePlan>(entity =>
            {
                entity.HasKey(e => e.CoveragePlan1);

                entity.Property(e => e.CoveragePlan1)
                    .HasColumnName("Coverage_Plan")
                    .HasMaxLength(50);

                entity.Property(e => e.EligibilityCountry)
                    .IsRequired()
                    .HasColumnName("Eligibility_Country")
                    .HasMaxLength(50);

                entity.Property(e => e.EligibilityDateFrom)
                    .HasColumnName("Eligibility_Date_From")
                    .HasColumnType("date");

                entity.Property(e => e.EligibilityDateTo)
                    .HasColumnName("Eligibility_Date_To")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<RateChart>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CoveragePlan)
                    .IsRequired()
                    .HasColumnName("Coverage_Plan")
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerGender)
                    .IsRequired()
                    .HasColumnName("Customer_Gender")
                    .HasMaxLength(50);

                entity.Property(e => e.EndAge)
                    .HasColumnName("End_Age")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.NetPrice)
                    .HasColumnName("Net_Price")
                    .HasColumnType("money");

                entity.Property(e => e.StartAge)
                    .HasColumnName("Start_Age")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.CoveragePlanNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CoveragePlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RateChart_CoveragePlan");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
