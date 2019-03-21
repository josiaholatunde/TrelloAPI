﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrelloWebAPI.Data;

namespace TrelloWebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190320174638_AddMigrations")]
    partial class AddMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrelloWebAPI.Models.BookingSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingType");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("MainDescription");

                    b.Property<string>("Name");

                    b.Property<int>("NoOfBookingSubjectsLeft");

                    b.Property<int>("NoOfVoters");

                    b.Property<string>("SubDescription");

                    b.Property<int>("TotalBookingSubjects");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BookingSubjects");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingSubjectId");

                    b.Property<DateTime>("DateCommented");

                    b.Property<string>("Description");

                    b.Property<bool>("IsRecommended");

                    b.Property<double>("Rating");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookingSubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Feature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingSubjectId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookingSubjectId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.GalleryPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingSubjectId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("BookingSubjectId");

                    b.ToTable("GalleryPictures");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateRead");

                    b.Property<DateTime>("DateSent");

                    b.Property<bool>("IsRead");

                    b.Property<string>("MessageContent");

                    b.Property<bool>("RecipientDeleted");

                    b.Property<int>("RecipientId");

                    b.Property<bool>("SenderDeleted");

                    b.Property<int>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("UserName");

                    b.Property<int>("UserRole");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TrelloWebAPI.Models.BookingSubject", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.User", "User")
                        .WithMany("BookingSubjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Comment", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.BookingSubject", "BookingSubject")
                        .WithMany("Comments")
                        .HasForeignKey("BookingSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TrelloWebAPI.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Feature", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.BookingSubject", "BookingSubject")
                        .WithMany("Features")
                        .HasForeignKey("BookingSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloWebAPI.Models.GalleryPicture", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.BookingSubject", "BookingSubject")
                        .WithMany("GalleryPictures")
                        .HasForeignKey("BookingSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Message", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TrelloWebAPI.Models.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TrelloWebAPI.Models.Photo", b =>
                {
                    b.HasOne("TrelloWebAPI.Models.User", "User")
                        .WithOne("Photo")
                        .HasForeignKey("TrelloWebAPI.Models.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
