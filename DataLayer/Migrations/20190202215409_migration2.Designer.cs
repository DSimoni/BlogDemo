﻿// <auto-generated />
using System;
using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    [DbContext(typeof(EFDataContext))]
    [Migration("20190202215409_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Model.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Category_ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DataLayer.Model.Comment", b =>
                {
                    b.Property<int>("Comment_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("Created_Time");

                    b.Property<int>("PostRefID");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Comment_ID");

                    b.HasIndex("PostRefID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DataLayer.Model.Post", b =>
                {
                    b.Property<int>("Post_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Created_Time");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("Update_Time");

                    b.Property<int>("UserRefID");

                    b.HasKey("Post_ID");

                    b.HasIndex("UserRefID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DataLayer.Model.Post_Category", b =>
                {
                    b.Property<int>("Post_ID");

                    b.Property<int>("Category_ID");

                    b.HasKey("Post_ID", "Category_ID");

                    b.HasAlternateKey("Category_ID", "Post_ID");

                    b.ToTable("Post_Category");
                });

            modelBuilder.Entity("DataLayer.Model.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Salt")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(20)");

                    b.HasKey("User_ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DataLayer.Model.Comment", b =>
                {
                    b.HasOne("DataLayer.Model.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostRefID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Model.Post", b =>
                {
                    b.HasOne("DataLayer.Model.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserRefID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Model.Post_Category", b =>
                {
                    b.HasOne("DataLayer.Model.Category", "Category")
                        .WithMany("Post_Categories")
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Model.Post", "Post")
                        .WithMany("Post_Categories")
                        .HasForeignKey("Post_ID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
