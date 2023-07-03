﻿using Company.Videomatic.Domain.Aggregates.Playlist;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Playlists.Commands;

public sealed class CreatePlaylistHandler : BaseRequestHandler<CreatePlaylistCommand, CreatePlaylistResponse>
{
    public CreatePlaylistHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<CreatePlaylistResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken = default)
    {
        Playlist dbPlaylist = Mapper.Map<CreatePlaylistCommand, Playlist>(request);

        var entry = DbContext.Add(dbPlaylist);
        var res = await DbContext.CommitChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return new CreatePlaylistResponse(Id: entry.Entity.Id);
    }
}
