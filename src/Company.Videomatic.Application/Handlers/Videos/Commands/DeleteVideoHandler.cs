﻿using Company.SharedKernel.Common.CQRS;

namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteEntityHandler<DeleteVideoCommand, Video, VideoId>
{
    public DeleteVideoHandler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
