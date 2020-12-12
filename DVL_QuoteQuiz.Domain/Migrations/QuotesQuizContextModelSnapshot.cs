﻿// <auto-generated />
using System;
using DVL_QuoteQuiz.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DVL_QuoteQuiz.Domain.Migrations
{
    [DbContext(typeof(QuotesQuizContext))]
    partial class QuotesQuizContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CorrectAnswersCount")
                        .HasColumnType("int");

                    b.Property<int>("QuestionsCount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.GameAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("QuoteAnswerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("QuoteAnswerId");

                    b.ToTable("GameAnswers");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.QuoteAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRightAnswer")
                        .HasColumnType("bit");

                    b.Property<int>("QuoteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuoteAnswers");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit");

                    b.Property<bool>("IsDisabled")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdatedDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Game", b =>
                {
                    b.HasOne("DVL_QuoteQuiz.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.GameAnswer", b =>
                {
                    b.HasOne("DVL_QuoteQuiz.Domain.Models.Game", "Game")
                        .WithMany("GameAnswers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DVL_QuoteQuiz.Domain.Models.QuoteAnswer", "QuoteAnswer")
                        .WithMany()
                        .HasForeignKey("QuoteAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("QuoteAnswer");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.QuoteAnswer", b =>
                {
                    b.HasOne("DVL_QuoteQuiz.Domain.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DVL_QuoteQuiz.Domain.Models.Quote", "Quote")
                        .WithMany("QuoteAnswers")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Game", b =>
                {
                    b.Navigation("GameAnswers");
                });

            modelBuilder.Entity("DVL_QuoteQuiz.Domain.Models.Quote", b =>
                {
                    b.Navigation("QuoteAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
