﻿using Company.SharedKernel.CQRS.Commands;

namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class UpdateArtifactHandler : UpdateEntityHandler<UpdateArtifactCommand, Artifact, ArtifactId>
{
    public UpdateArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
