﻿// <auto-generated />
using System;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Company.Videomatic.Infrastructure.Data.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerVideomaticDbContext))]
    partial class SqlServerVideomaticDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ArtifactSequence");

            modelBuilder.HasSequence("PlaylistSequence");

            modelBuilder.HasSequence("TagSequence");

            modelBuilder.HasSequence("ThumbnailSequence");

            modelBuilder.HasSequence("TranscriptLineSequence");

            modelBuilder.HasSequence("TranscriptSequence");

            modelBuilder.HasSequence("VideoSequence");

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.ArtifactDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR ArtifactSequence");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Title");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifacts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.PlaylistDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR PlaylistSequence");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Playlists", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.PlaylistDbVideoDb", b =>
                {
                    b.Property<long>("PlaylistId")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("PlaylistId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TagDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR TagSequence");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.ThumbnailDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR ThumbnailSequence");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<int>("Resolution")
                        .HasColumnType("int");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Height");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Location");

                    b.HasIndex("Resolution");

                    b.HasIndex("VideoId");

                    b.HasIndex("Width");

                    b.ToTable("Thumbnails", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TranscriptDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR TranscriptSequence");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TranscriptLineDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR TranscriptLineSequence");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartsAt")
                        .HasColumnType("time");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("TranscriptId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Text");

                    b.HasIndex("TranscriptId");

                    b.ToTable("TranscriptLines", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.VideoDb", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR VideoSequence");

                    b.Property<string>("Description")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("Description");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Location");

                    b.HasIndex("Title");

                    b.ToTable("Videos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.VideoDbTagDb", b =>
                {
                    b.Property<long>("TagId")
                        .HasColumnType("bigint");

                    b.Property<long>("VideoId")
                        .HasColumnType("bigint");

                    b.HasKey("TagId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideoTags", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.ArtifactDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.VideoDb", null)
                        .WithMany("Artifacts")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.PlaylistDbVideoDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.PlaylistDb", "Playlist")
                        .WithMany("PlaylistVideos")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.VideoDb", "Video")
                        .WithMany("PlaylistVideos")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.ThumbnailDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.VideoDb", null)
                        .WithMany("Thumbnails")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TranscriptDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.VideoDb", null)
                        .WithMany("Transcripts")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TranscriptLineDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.TranscriptDb", null)
                        .WithMany("Lines")
                        .HasForeignKey("TranscriptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.VideoDbTagDb", b =>
                {
                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.TagDb", "Tag")
                        .WithMany("VideoTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company.Videomatic.Infrastructure.Data.Model.VideoDb", "Video")
                        .WithMany("VideoTags")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.PlaylistDb", b =>
                {
                    b.Navigation("PlaylistVideos");
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TagDb", b =>
                {
                    b.Navigation("VideoTags");
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.TranscriptDb", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Company.Videomatic.Infrastructure.Data.Model.VideoDb", b =>
                {
                    b.Navigation("Artifacts");

                    b.Navigation("PlaylistVideos");

                    b.Navigation("Thumbnails");

                    b.Navigation("Transcripts");

                    b.Navigation("VideoTags");
                });
#pragma warning restore 612, 618
        }
    }
}
