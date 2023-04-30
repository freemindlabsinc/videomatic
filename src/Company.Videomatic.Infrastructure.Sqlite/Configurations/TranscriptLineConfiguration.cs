﻿using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class TranscriptLineConfiguration : EntityConfigurationBase<TranscriptLine>
{
    public override void Configure(EntityTypeBuilder<TranscriptLine> builder)
    {
        base.Configure(builder);
        //Indices
        //builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.Text);
    }
}
