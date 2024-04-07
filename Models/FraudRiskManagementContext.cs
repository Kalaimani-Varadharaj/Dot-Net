using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FraudDetectionRepositoryPatternProject.Models;

public partial class FraudRiskManagementContext : DbContext
{
    public FraudRiskManagementContext()
    {
    }

    public FraudRiskManagementContext(DbContextOptions<FraudRiskManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FraudulentIncidentDetail> FraudulentIncidentDetails { get; set; }

    public virtual DbSet<TransactionHistory> TransactionHistories { get; set; }

    public DbSet<UserRegister> UserRegisters { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PGLUOBL\\SQLEXPRESS;Database=Fraud_Risk_Management;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FraudulentIncidentDetail>(entity =>
        {
            entity.HasKey(e => e.IncidentNumber).HasName("PK__Fraudule__DBAD0997440DBEAA");

            entity.ToTable("Fraudulent_Incident_Details");

            entity.Property(e => e.IncidentNumber).HasColumnName("Incident_Number");
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.FraudulentType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Fraudulent_Type");
            entity.Property(e => e.IncidentStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Incident_Status");
            entity.Property(e => e.TransactionReferenceNumber).HasColumnName("Transaction_Reference_Number");

            
            entity.HasOne(d => d.TransactionReferenceNumberNavigation).WithMany(p => p.FraudulentIncidentDetails)
                .HasForeignKey(d => d.TransactionReferenceNumber)
                .HasConstraintName("FK__Fraudulen__Trans__778AC167");
        });

        modelBuilder.Entity<TransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionReferenceNumber).HasName("PK__Transact__E92BA0E4E089E93E");

            entity.ToTable("Transaction_History");

            entity.HasIndex(e => e.SenderAccountNumber, "UQ__Transact__AAAD967C9E7E4890").IsUnique();

            entity.HasIndex(e => e.BeneficiaryAccountNumber, "UQ__Transact__CC76BB83FD038907").IsUnique();

            entity.Property(e => e.TransactionReferenceNumber)
                .ValueGeneratedNever()
                .HasColumnName("Transaction_Reference_Number");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BeneficiaryAccountNumber).HasColumnName("Beneficiary_Account_Number");
            entity.Property(e => e.BeneficiaryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Beneficiary_Name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("Modified_At");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.SenderAccountNumber).HasColumnName("Sender_Account_Number");
            entity.Property(e => e.SenderName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Sender_Name");
            entity.Property(e => e.TransactionDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Transaction_DateTime");
        });

        modelBuilder.Entity<UserRegister>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserRegister");

            entity.ToTable("UserRegister");

            entity.Property(e => e.UserId).HasColumnName("UserId");
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ConfirmPassword)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Ignore<UserLogin>();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
