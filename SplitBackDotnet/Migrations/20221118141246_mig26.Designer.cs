﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SplitBackDotnet.Data;

#nullable disable

namespace SplitBackDotnet.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221118141246_mig26")]
    partial class mig26
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7");

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<int>("GroupsGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MembersUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupsGroupId", "MembersUserId");

                    b.HasIndex("MembersUserId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LabelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExpenseId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LabelId");

                    b.HasIndex("UserId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("SpenderAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ExpenseId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseUsers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("GroupId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Label", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("LabelId");

                    b.HasIndex("GroupId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Unique")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SessionId");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Transfer", b =>
                {
                    b.Property<int>("TransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReceiverUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TransferId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany("Expenses")
                        .HasForeignKey("GroupId");

                    b.HasOne("SplitBackDotnet.Models.Label", "Label")
                        .WithMany()
                        .HasForeignKey("LabelId");

                    b.HasOne("SplitBackDotnet.Models.User", null)
                        .WithMany("Expenses")
                        .HasForeignKey("UserId");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseUser", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Expense", "Expense")
                        .WithMany("ExpenseUsers")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "User")
                        .WithMany("ExpenseUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.User", "Creator")
                        .WithMany("CreatedGroups")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Label", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany("Labels")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Session", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Transfer", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany("Transfers")
                        .HasForeignKey("GroupId");

                    b.HasOne("SplitBackDotnet.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.Navigation("ExpenseUsers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Labels");

                    b.Navigation("Transfers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.User", b =>
                {
                    b.Navigation("CreatedGroups");

                    b.Navigation("ExpenseUsers");

                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}