﻿using Domain;
using Infrastructure.Data.Configurations.ValueObjects;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Data.Configurations.Entities;

public abstract class ImportedEntityConfiguration<T> : TrackedEntityConfiguration<T>,
    IEntityTypeConfiguration<T> // Unnecessary, but makes it easier to understand
    where T : ImportedEntity
{    
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);

        // ---------- Fields ----------
        builder.Property(x => x.Name)
               .HasMaxLength(FieldLengths.Name);

        builder.Property(x => x.Description);

        // Tags
        var valueComparer = new ValueComparer<IEnumerable<string>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(x => x.Tags)
               .HasConversion(x => string.Join(',', x),
                              y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet(StringComparer.OrdinalIgnoreCase))
               .Metadata.SetValueComparer(valueComparer);

        // TopicCategories
        builder.Property(x => x.TopicCategories)
               .HasConversion(x => string.Join(',', x),
                              y => y.Split(',', StringSplitOptions.RemoveEmptyEntries).ToHashSet(StringComparer.OrdinalIgnoreCase))
               .Metadata.SetValueComparer(valueComparer);

        // ---------- Owned Types ----------

        builder.OwnsOne(x => x.Thumbnail, ImageReferenceConfigurator.Configure);

        builder.OwnsOne(x => x.Picture, ImageReferenceConfigurator.Configure);

        builder.OwnsOne(x => x.Origin, EntityOriginConfigurator.Configure);

        // ---------- Indices ----------
        builder.HasIndex(x => x.Name);
    }
}