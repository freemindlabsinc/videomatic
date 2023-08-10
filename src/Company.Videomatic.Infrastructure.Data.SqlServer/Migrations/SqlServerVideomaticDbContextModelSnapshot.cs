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
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("ArtifactSequence");

            modelBuilder.HasSequence("PlaylistSequence");

            modelBuilder.HasSequence("TagsSequence");

            modelBuilder.HasSequence("ThumbnailSequence");

            modelBuilder.HasSequence("TranscriptLineSequence");

            modelBuilder.HasSequence("TranscriptSequence");

            modelBuilder.HasSequence("VideoSequence");

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Artifact.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR ArtifactSequence");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("VideoId");

                    b.ToTable("Artifacts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR PlaylistSequence");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsStarred")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.HasIndex("Description");

                    b.HasIndex("Name");

                    b.ToTable("Playlists", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.PlaylistVideo", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("PlaylistVideos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Transcript.Transcript", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR TranscriptSequence");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Language");

                    b.HasIndex("VideoId");

                    b.ToTable("Transcripts", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR VideoSequence");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStarred")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("Location");

                    b.HasIndex("Name");

                    b.ToTable("Videos", (string)null);
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Artifact.Artifact", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", b =>
                {
                    b.OwnsOne("Company.Videomatic.Domain.Aggregates.Playlist.PlaylistOrigin", "Origin", b1 =>
                        {
                            b1.Property<int>("PlaylistId")
                                .HasColumnType("int");

                            b1.Property<string>("ChannelId")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("DefaultLanguage")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ETag")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("EmbedHtml")
                                .HasMaxLength(2048)
                                .HasColumnType("nvarchar(2048)");

                            b1.Property<string>("Id")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<string>("PictureUrl")
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<DateTime?>("PublishedAt")
                                .HasColumnType("datetime2");

                            b1.Property<string>("ThumbnailUrl")
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.HasKey("PlaylistId");

                            b1.HasIndex("ChannelId");

                            b1.HasIndex("DefaultLanguage");

                            b1.HasIndex("ETag");

                            b1.HasIndex("Id");

                            b1.HasIndex("Name");

                            b1.ToTable("Playlists");

                            b1.WithOwner()
                                .HasForeignKey("PlaylistId");
                        });

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.PlaylistVideo", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", null)
                        .WithMany("Videos")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Transcript.Transcript", b =>
                {
                    b.HasOne("Company.Videomatic.Domain.Aggregates.Video.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Transcript.TranscriptLine", "Lines", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR TranscriptLineSequence");

                            b1.Property<TimeSpan?>("Duration")
                                .HasColumnType("time");

                            b1.Property<TimeSpan?>("StartsAt")
                                .HasColumnType("time");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("TranscriptId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("Text");

                            b1.HasIndex("TranscriptId");

                            b1.ToTable("TranscriptLines", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TranscriptId");
                        });

                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Video.Video", b =>
                {
                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Video.Thumbnail", "Thumbnails", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR ThumbnailSequence");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<string>("Location")
                                .IsRequired()
                                .HasMaxLength(1024)
                                .HasColumnType("nvarchar(1024)");

                            b1.Property<int>("Resolution")
                                .HasColumnType("int");

                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("VideoId");

                            b1.ToTable("Thumbnails", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsOne("Company.Videomatic.Domain.Aggregates.Video.VideoDetails", "Details", b1 =>
                        {
                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.Property<string>("Provider")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("ProviderVideoId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("VideoOwnerChannelId")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("VideoOwnerChannelTitle")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<DateTime>("VideoPublishedAt")
                                .HasColumnType("datetime2");

                            b1.HasKey("VideoId");

                            b1.HasIndex("Provider");

                            b1.HasIndex("ProviderVideoId");

                            b1.HasIndex("VideoOwnerChannelId");

                            b1.HasIndex("VideoOwnerChannelTitle");

                            b1.HasIndex("VideoPublishedAt");

                            b1.ToTable("Videos");

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.OwnsMany("Company.Videomatic.Domain.Aggregates.Video.VideoTag", "Tags", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasDefaultValueSql("NEXT VALUE FOR TagsSequence");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<int>("VideoId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("VideoId");

                            b1.HasIndex("Name", "VideoId")
                                .IsUnique();

                            b1.ToTable("VideoTags", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("VideoId");
                        });

                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("Tags");

                    b.Navigation("Thumbnails");
                });

            modelBuilder.Entity("Company.Videomatic.Domain.Aggregates.Playlist.Playlist", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
