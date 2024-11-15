﻿// <auto-generated />
using System;
using CommentsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommentsAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241110151005_AddSeedData")]
    partial class AddSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CommentsAPI.Models.Entities.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FileURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Homepage")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            Id = 11101,
                            Content = "This is the first comment!",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 0, 4, 699, DateTimeKind.Local).AddTicks(2317),
                            Email = "user1@example.com",
                            FileURL = "gt86.png",
                            Homepage = "https://user1.com",
                            Username = "user1"
                        },
                        new
                        {
                            Id = 11102,
                            Content = "I agree with the above comment.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 2, 4, 699, DateTimeKind.Local).AddTicks(2368),
                            Email = "user2@example.com",
                            FileURL = "hello.txt",
                            ParentId = 11107,
                            Username = "user2"
                        },
                        new
                        {
                            Id = 11103,
                            Content = "This is a great post. Thank you for sharing!",
                            CreatedAt = new DateTime(2024, 11, 10, 11, 10, 4, 699, DateTimeKind.Local).AddTicks(2374),
                            Email = "user3@example.com",
                            FileURL = "code.txt",
                            Username = "user3"
                        },
                        new
                        {
                            Id = 11104,
                            Content = "I have a question about the code in the previous post.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 5, 4, 699, DateTimeKind.Local).AddTicks(2378),
                            Email = "user4@example.com",
                            ParentId = 11107,
                            Username = "user4"
                        },
                        new
                        {
                            Id = 11105,
                            Content = "<strong>This is a bold comment.</strong>",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 6, 4, 699, DateTimeKind.Local).AddTicks(2382),
                            Email = "user5@example.com",
                            FileURL = "tiger.jpg",
                            Username = "user5"
                        },
                        new
                        {
                            Id = 11106,
                            Content = "Can you provide more details on this?",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 7, 4, 699, DateTimeKind.Local).AddTicks(2385),
                            Email = "user6@example.com",
                            FileURL = "test.gif",
                            ParentId = 11111,
                            Username = "user6"
                        },
                        new
                        {
                            Id = 11107,
                            Content = "<i>This comment is in italics.</i>",
                            CreatedAt = new DateTime(2024, 11, 8, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2389),
                            Email = "user7@example.com",
                            Username = "user7"
                        },
                        new
                        {
                            Id = 11108,
                            Content = "I found the issue. It was a small typo.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 9, 4, 699, DateTimeKind.Local).AddTicks(2393),
                            Email = "user8@example.com",
                            FileURL = "hello.txt",
                            ParentId = 11109,
                            Username = "user8"
                        },
                        new
                        {
                            Id = 11109,
                            Content = "Great job on the tutorial!",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2396),
                            Email = "user9@example.com",
                            FileURL = "gt86.png",
                            Username = "user9"
                        },
                        new
                        {
                            Id = 11110,
                            Content = "Thank you for your feedback!",
                            CreatedAt = new DateTime(2024, 11, 8, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2399),
                            Email = "user10@example.com",
                            ParentId = 11130,
                            Username = "user10"
                        },
                        new
                        {
                            Id = 11111,
                            Content = "I have a suggestion. Maybe you could add more examples.",
                            CreatedAt = new DateTime(2024, 11, 6, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2401),
                            Email = "user11@example.com",
                            FileURL = "code.txt",
                            Username = "user11"
                        },
                        new
                        {
                            Id = 11112,
                            Content = "Could you explain how to use this in production?",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 5, 4, 699, DateTimeKind.Local).AddTicks(2405),
                            Email = "user12@example.com",
                            FileURL = "test.gif",
                            ParentId = 11101,
                            Username = "user12"
                        },
                        new
                        {
                            Id = 11113,
                            Content = "This is a helpful post. I learned a lot.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 3, 4, 699, DateTimeKind.Local).AddTicks(2408),
                            Email = "user13@example.com",
                            FileURL = "tiger.jpg",
                            Username = "user13"
                        },
                        new
                        {
                            Id = 11114,
                            Content = "<code>let x = 10;</code> Simple code example.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 2, 4, 699, DateTimeKind.Local).AddTicks(2411),
                            Email = "user14@example.com",
                            ParentId = 11112,
                            Username = "user14"
                        },
                        new
                        {
                            Id = 11115,
                            Content = "Nice! I will try this approach.",
                            CreatedAt = new DateTime(2024, 11, 10, 7, 10, 4, 699, DateTimeKind.Local).AddTicks(2414),
                            Email = "user15@example.com",
                            FileURL = "gt86.png",
                            Username = "user15"
                        },
                        new
                        {
                            Id = 11116,
                            Content = "Is there any way to optimize this further?",
                            CreatedAt = new DateTime(2024, 11, 10, 5, 10, 4, 699, DateTimeKind.Local).AddTicks(2417),
                            Email = "user16@example.com",
                            FileURL = "hello.txt",
                            ParentId = 11112,
                            Username = "user16"
                        },
                        new
                        {
                            Id = 11117,
                            Content = "I would love to see a video tutorial on this.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 56, 4, 699, DateTimeKind.Local).AddTicks(2420),
                            Email = "user17@example.com",
                            FileURL = "code.txt",
                            Username = "user17"
                        },
                        new
                        {
                            Id = 11118,
                            Content = "I'm stuck. Can someone help me with this part?",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 55, 4, 699, DateTimeKind.Local).AddTicks(2423),
                            Email = "user18@example.com",
                            ParentId = 11111,
                            Username = "user18"
                        },
                        new
                        {
                            Id = 11119,
                            Content = "<strong>Very informative!</strong> I will be using this soon.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 52, 4, 699, DateTimeKind.Local).AddTicks(2427),
                            Email = "user19@example.com",
                            FileURL = "test.gif",
                            Username = "user19"
                        },
                        new
                        {
                            Id = 11120,
                            Content = "Can you provide the source code for this?",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 50, 4, 699, DateTimeKind.Local).AddTicks(2430),
                            Email = "user20@example.com",
                            FileURL = "tiger.jpg",
                            ParentId = 11110,
                            Username = "user20"
                        },
                        new
                        {
                            Id = 11121,
                            Content = "I think there's an error in this example.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 48, 4, 699, DateTimeKind.Local).AddTicks(2433),
                            Email = "user21@example.com",
                            FileURL = "code.txt",
                            Username = "user21"
                        },
                        new
                        {
                            Id = 11122,
                            Content = "Can you explain the error you're facing?",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 46, 4, 699, DateTimeKind.Local).AddTicks(2436),
                            Email = "user22@example.com",
                            ParentId = 11101,
                            Username = "user22"
                        },
                        new
                        {
                            Id = 11123,
                            Content = "I'm having trouble understanding this part.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 44, 4, 699, DateTimeKind.Local).AddTicks(2439),
                            Email = "user23@example.com",
                            FileURL = "hello.txt",
                            Username = "user23"
                        },
                        new
                        {
                            Id = 11124,
                            Content = "Can someone clarify how to use the function?",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 42, 4, 699, DateTimeKind.Local).AddTicks(2442),
                            Email = "user24@example.com",
                            FileURL = "gt86.png",
                            ParentId = 11125,
                            Username = "user24"
                        },
                        new
                        {
                            Id = 11125,
                            Content = "I love how simple this approach is.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 40, 4, 699, DateTimeKind.Local).AddTicks(2445),
                            Email = "user25@example.com",
                            FileURL = "code.txt",
                            Username = "user25"
                        },
                        new
                        {
                            Id = 11126,
                            Content = "This helped me a lot. Thanks!",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 38, 4, 699, DateTimeKind.Local).AddTicks(2448),
                            Email = "user26@example.com",
                            ParentId = 11126,
                            Username = "user26"
                        },
                        new
                        {
                            Id = 11127,
                            Content = "Is it possible to use this with a different framework?",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 36, 4, 699, DateTimeKind.Local).AddTicks(2451),
                            Email = "user27@example.com",
                            FileURL = "test.gif",
                            Username = "user27"
                        },
                        new
                        {
                            Id = 11128,
                            Content = "Thanks for the clarification.",
                            CreatedAt = new DateTime(2024, 11, 10, 16, 34, 4, 699, DateTimeKind.Local).AddTicks(2454),
                            Email = "user28@example.com",
                            FileURL = "hello.txt",
                            ParentId = 11125,
                            Username = "user28"
                        },
                        new
                        {
                            Id = 11129,
                            Content = "<code>const y = 20;</code> Another code example.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 48, 4, 699, DateTimeKind.Local).AddTicks(2458),
                            Email = "user29@example.com",
                            FileURL = "tiger.jpg",
                            Username = "user29"
                        },
                        new
                        {
                            Id = 11130,
                            Content = "This is great! I'm going to try it.",
                            CreatedAt = new DateTime(2024, 11, 10, 17, 50, 4, 699, DateTimeKind.Local).AddTicks(2461),
                            Email = "user30@example.com",
                            FileURL = "code.txt",
                            ParentId = 11126,
                            Username = "user30"
                        });
                });

            modelBuilder.Entity("CommentsAPI.Models.Entities.CommentEntity", b =>
                {
                    b.HasOne("CommentsAPI.Models.Entities.CommentEntity", "Parent")
                        .WithMany("Comments")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("CommentsAPI.Models.Entities.CommentEntity", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
