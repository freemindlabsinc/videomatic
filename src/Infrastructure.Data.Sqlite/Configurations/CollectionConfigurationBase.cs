﻿namespace Company.Videomatic.Infrastructure.Data.Configurations;

public abstract class CollectionConfigurationBase : IEntityTypeConfiguration<VideoCollection>
{
    public virtual void Configure(EntityTypeBuilder<VideoCollection> builder)
    {
        // Common
        builder.HasIndex(x => x.Id)
               .IsUnique();

        // Fields
        builder.Property(x => x.Name)
               .HasMaxLength(VideomaticConstants.DbFieldLengths.CollectionName);
        //builder.Property(x => x.Description)
        //       .HasMaxLength(VideomaticConstants.DbFieldLengths.YTVideoDescription);
        // Relationships
        builder.HasMany(x => x.Videos)
               .WithMany(x => x.Collections);
        // Indices
        //builder.HasIndex(x => x.Id).IsUnique();
    }
}
