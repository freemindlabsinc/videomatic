﻿using System.ComponentModel.Design;

namespace Company.Videomatic.Infrastructure.Data.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistRepository(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Playlist> AddAsync(Playlist playlist, CancellationToken cancellationToken)
    {
        PlaylistDb dbPlaylist = _mapper.Map<Playlist, PlaylistDb>(playlist);

        var entry = _dbContext.Add(dbPlaylist);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDb, Playlist>(entry.Entity);
    }

    public async Task<Playlist> UpdateAsync(Playlist playlist, CancellationToken cancellationToken)
    {
        var dataModel = _mapper.Map<Playlist, PlaylistDb>(playlist);
        var attached = await _dbContext.Playlists
            .Include(x => x.Videos)
            .SingleAsync(x => x.Id == playlist.Id, cancellationToken);

        _dbContext.Entry(attached).State = EntityState.Detached;
        foreach (var item in attached.Videos.ToList())
            _dbContext.Entry(item).State = EntityState.Detached;

        var entry = _dbContext.Attach(dataModel);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDb, Playlist>(entry.Entity);
    }

    public async Task<Playlist?> GetByIdAsync(GetPlaylistById args, CancellationToken cancellationToken)
    {
        IQueryable<PlaylistDb> source = _dbContext.Playlists.AsNoTracking();

        if (args.Includes != null)
        {
            foreach (var include in args.Includes)
            {
                source = source.Include(include);
            }
        }        

        var dbPlaylist = await source.FirstOrDefaultAsync(p => p.Id == args.Id, cancellationToken);
        if (dbPlaylist == null)
            return null;

        var playlist = _mapper.Map<PlaylistDb, Playlist>(dbPlaylist);
        return playlist;
    }
}