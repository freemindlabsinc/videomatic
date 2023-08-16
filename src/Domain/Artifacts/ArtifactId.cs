﻿namespace Domain.Artifacts;

public readonly record struct ArtifactId(int Value = 0)
{
    public static implicit operator int(ArtifactId x) => x.Value;
    public static implicit operator ArtifactId(int x) => new (x);
}