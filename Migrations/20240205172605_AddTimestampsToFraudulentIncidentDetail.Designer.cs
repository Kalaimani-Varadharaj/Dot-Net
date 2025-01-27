﻿// <auto-generated />
using System;
using FraudDetectionRepositoryPatternProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FraudDetectionRepositoryPatternProject.Migrations
{
    [DbContext(typeof(FraudRiskManagementContext))]
    [Migration("20240205172605_AddTimestampsToFraudulentIncidentDetail")]
    partial class AddTimestampsToFraudulentIncidentDetail
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FraudDetectionRepositoryPatternProject.Models.FraudulentIncidentDetail", b =>
                {
                    b.Property<int>("IncidentNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Incident_Number");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncidentNumber"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("FraudulentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Fraudulent_Type");

                    b.Property<string>("IncidentStatus")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Incident_Status");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("ModifiedAt");

                    b.Property<int?>("TransactionReferenceNumber")
                        .HasColumnType("int")
                        .HasColumnName("Transaction_Reference_Number");

                    b.HasKey("IncidentNumber")
                        .HasName("PK__Fraudule__DBAD0997440DBEAA");

                    b.HasIndex("TransactionReferenceNumber");

                    b.ToTable("Fraudulent_Incident_Details", (string)null);
                });

            modelBuilder.Entity("FraudDetectionRepositoryPatternProject.Models.Login", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("User_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId")
                        .HasName("PK_Login");

                    b.ToTable("Login", (string)null);
                });

            modelBuilder.Entity("FraudDetectionRepositoryPatternProject.Models.TransactionHistory", b =>
                {
                    b.Property<int>("TransactionReferenceNumber")
                        .HasColumnType("int")
                        .HasColumnName("Transaction_Reference_Number");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long?>("BeneficiaryAccountNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("Beneficiary_Account_Number");

                    b.Property<string>("BeneficiaryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Beneficiary_Name");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_At");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Modified_At");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Payment_Method");

                    b.Property<long?>("SenderAccountNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("Sender_Account_Number");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Sender_Name");

                    b.Property<DateTime?>("TransactionDateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("Transaction_DateTime");

                    b.HasKey("TransactionReferenceNumber")
                        .HasName("PK__Transact__E92BA0E4E089E93E");

                    b.HasIndex(new[] { "SenderAccountNumber" }, "UQ__Transact__AAAD967C9E7E4890")
                        .IsUnique()
                        .HasFilter("[Sender_Account_Number] IS NOT NULL");

                    b.HasIndex(new[] { "BeneficiaryAccountNumber" }, "UQ__Transact__CC76BB83FD038907")
                        .IsUnique()
                        .HasFilter("[Beneficiary_Account_Number] IS NOT NULL");

                    b.ToTable("Transaction_History", (string)null);
                });

            modelBuilder.Entity("FraudDetectionRepositoryPatternProject.Models.FraudulentIncidentDetail", b =>
                {
                    b.HasOne("FraudDetectionRepositoryPatternProject.Models.TransactionHistory", "TransactionReferenceNumberNavigation")
                        .WithMany("FraudulentIncidentDetails")
                        .HasForeignKey("TransactionReferenceNumber")
                        .HasConstraintName("FK__Fraudulen__Trans__778AC167");

                    b.Navigation("TransactionReferenceNumberNavigation");
                });

            modelBuilder.Entity("FraudDetectionRepositoryPatternProject.Models.TransactionHistory", b =>
                {
                    b.Navigation("FraudulentIncidentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
