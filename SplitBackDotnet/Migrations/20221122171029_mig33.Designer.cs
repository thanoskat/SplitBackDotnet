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
    [Migration("20221122171029_mig33")]
    partial class mig33
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

                    b.HasKey("ExpenseId");

                    b.HasIndex("GroupId");

                    b.HasIndex("LabelId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseParticipant", b =>
                {
                    b.Property<int>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParticipantId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ContributionAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("ExpenseId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ExpenseParticipants");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseSpender", b =>
                {
                    b.Property<int>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpenderId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("SpenderAmount")
                        .HasColumnType("TEXT");

                    b.HasKey("ExpenseId", "SpenderId");

                    b.HasIndex("SpenderId");

                    b.ToTable("ExpenseSpenders");
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

            modelBuilder.Entity("SplitBackDotnet.Models.PendingTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("PendingTransaction");
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

                    b.Property<int>("ReceiverId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TransferId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

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

                    b.Navigation("Label");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseParticipant", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Expense", "Expense")
                        .WithMany("ExpenseParticipants")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "Participant")
                        .WithMany("ExpenseParticipants")
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("Participant");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.ExpenseSpender", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Expense", "Expense")
                        .WithMany("ExpenseSpenders")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SplitBackDotnet.Models.User", "Spender")
                        .WithMany("ExpenseSpenders")
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

            modelBuilder.Entity("SplitBackDotnet.Models.PendingTransaction", b =>
                {
                    b.HasOne("SplitBackDotnet.Models.Group", null)
                        .WithMany("PendingTransactions")
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

                    b.HasOne("SplitBackDotnet.Models.User", null)
                        .WithMany("Transfers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Expense", b =>
                {
                    b.Navigation("ExpenseParticipants");

                    b.Navigation("ExpenseSpenders");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.Group", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Labels");

                    b.Navigation("PendingTransactions");

                    b.Navigation("Transfers");
                });

            modelBuilder.Entity("SplitBackDotnet.Models.User", b =>
                {
                    b.Navigation("CreatedGroups");

                    b.Navigation("ExpenseParticipants");

                    b.Navigation("ExpenseSpenders");

                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
