﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(HaSpManContext))]
    partial class HaSpManContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("HaSpMan")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("varchar(34)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("Domain.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTimeOffset?>("MembershipExpiryDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("MembershipFee")
                        .HasColumnType("float");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Domain.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("BankAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CounterPartyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateFiled")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("ReceivedDateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId", "Sequence")
                        .IsUnique();

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.Member", b =>
                {
                    b.OwnsOne("Types.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("HouseNumber")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("varchar(15)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("varchar(200)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("varchar(10)");

                            b1.HasKey("MemberId");

                            b1.ToTable("Members");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.OwnsMany("Types.AuditEvent", "AuditEvents", b1 =>
                        {
                            b1.Property<Guid>("MemberId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("varchar(1000)");

                            b1.Property<string>("PerformedBy")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100)");

                            b1.Property<DateTimeOffset>("Timestamp")
                                .HasColumnType("datetimeoffset");

                            b1.HasKey("MemberId", "Id");

                            b1.ToTable("Member_AuditEvents");

                            b1.WithOwner()
                                .HasForeignKey("MemberId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("AuditEvents");
                });

            modelBuilder.Entity("Domain.Transaction", b =>
                {
                    b.OwnsMany("Types.TransactionAttachment", "Attachments", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("BlobURI")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TransactionId", "Id");

                            b1.ToTable("Transaction_Attachments");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.OwnsMany("Types.TransactionTypeAmount", "TransactionTypeAmounts", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("TransactionType")
                                .HasColumnType("int");

                            b1.HasKey("TransactionId", "Id");

                            b1.ToTable("Transaction_TransactionTypeAmount");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.Navigation("Attachments");

                    b.Navigation("TransactionTypeAmounts");
                });
#pragma warning restore 612, 618
        }
    }
}
