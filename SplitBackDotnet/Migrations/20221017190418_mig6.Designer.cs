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
    [Migration("20221017190418_mig6")]
    partial class mig6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7");

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MembersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GroupsId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("GroupUser");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LabelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LabelId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseUser", b =>
                {
                    b.Property<int>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpenderId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("SpenderAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("ExpenseId", "SpenderId");

                    b.HasIndex("SpenderId");

                    b.ToTable("ExpenseUser");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Unique")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Share", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("UserId");

                    b.ToTable("Shares");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Transfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transfers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
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

                    b.Navigation("Label");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseUser", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Expense", "Expense")
                        .WithMany("ExpenseUsers")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "Spender")
                        .WithMany("ExpenseUsers")
                        .HasForeignKey("SpenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("Spender");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.User", "Creator")
                        .WithMany("CreatedGroups")
                        .HasForeignKey("CreatorId")
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

            modelBuilder.Entity("SplitBackDotnet.Models.Share", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Expense", null)
                        .WithMany("Shares")
                        .HasForeignKey("ExpenseId");

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
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.Navigation("ExpenseUsers");

                    b.Navigation("Shares");
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
                });
#pragma warning restore 612, 618
        }
    }
}
