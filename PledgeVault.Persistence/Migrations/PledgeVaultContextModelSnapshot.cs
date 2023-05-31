﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PledgeVault.Persistence;

#nullable disable

namespace PledgeVault.Persistence.Migrations
{
    [DbContext(typeof(PledgeVaultContext))]
    partial class PledgeVaultContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PledgeVault.Core.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateEstablished")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("GovernmentType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Party", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateEstablished")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LogoUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SiteUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Pledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateFulfilled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePledged")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("FulfilledSummary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PledgeCategoryType")
                        .HasColumnType("int");

                    b.Property<int>("PledgeStatusType")
                        .HasColumnType("int");

                    b.Property<int>("PoliticianId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("PoliticianId");

                    b.HasIndex("Title", "DatePledged", "PoliticianId")
                        .IsUnique();

                    b.ToTable("Pledges");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Politician", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CountryOfBirth")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPartyLeader")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PartyId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.Property<int>("SexType")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PartyId");

                    b.HasIndex("PositionId");

                    b.ToTable("Politicians");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("EntityActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EntityCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EntityModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("PledgeId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceType")
                        .HasColumnType("int");

                    b.Property<string>("SiteUrl")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Summary")
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("PledgeId");

                    b.HasIndex("SiteUrl", "PledgeId")
                        .IsUnique();

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Party", b =>
                {
                    b.HasOne("PledgeVault.Core.Models.Country", "Country")
                        .WithMany("Parties")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Pledge", b =>
                {
                    b.HasOne("PledgeVault.Core.Models.Politician", "Politician")
                        .WithMany("Pledges")
                        .HasForeignKey("PoliticianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Politician");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Politician", b =>
                {
                    b.HasOne("PledgeVault.Core.Models.Party", "Party")
                        .WithMany("Politicians")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PledgeVault.Core.Models.Position", "Position")
                        .WithMany("Politicians")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Resource", b =>
                {
                    b.HasOne("PledgeVault.Core.Models.Pledge", "Pledge")
                        .WithMany("Resources")
                        .HasForeignKey("PledgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pledge");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Country", b =>
                {
                    b.Navigation("Parties");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Party", b =>
                {
                    b.Navigation("Politicians");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Pledge", b =>
                {
                    b.Navigation("Resources");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Politician", b =>
                {
                    b.Navigation("Pledges");
                });

            modelBuilder.Entity("PledgeVault.Core.Models.Position", b =>
                {
                    b.Navigation("Politicians");
                });
#pragma warning restore 612, 618
        }
    }
}
