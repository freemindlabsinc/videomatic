﻿using Ardalis.GuardClauses;

namespace Company.Videomatic.Application.Abstractions;

public static class RepositoryExtensions
{
    public static async Task<Result<int>> LinkPlaylistToVideos(this IRepository<Playlist> repository, PlaylistId playlistId, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(repository, nameof(repository));
        Guard.Against.Null(playlistId, nameof(playlistId));

        var pl = await repository.GetByIdAsync(playlistId, cancellationToken);
        if (pl is null)
        { 
            return Result<int>.NotFound();
        }

        var newLinks = pl.LinkToVideos(videoIds);

        await repository.SaveChangesAsync();

        return newLinks;
    }

    public static async Task<IReadOnlyDictionary<string, VideoId>> GetVideoProviderIds(this IRepository<Video> repository, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        var videos = await repository.ListAsync(new VideosByIdsSpec(videoIds.ToArray()), cancellationToken);

        return videos.ToDictionary(v => v.Details.ProviderVideoId, v => v.Id);
    }

    //public static async Task<IReadOnlyDictionary<VideoId, string>> GetPlaylistVideoIds(this IRepository<Playlist> repository, PlaylistId playlistId, CancellationToken cancellationToken = default)
    //{        
    //    var playlists = await repository.ListAsync(new PlaylistWithVideosSpec(playlistId), cancellationToken);
    //    var videoIds = playlists.SelectMany(pl => pl.Videos)
    //                            .Select(v => v.VideoId);
    //
    //    
    //    return videos.ToDictionary(v => v.Id, v => v.Details.ProviderVideoId);
    //}
}

public class VideosByIdsSpec : Specification<Video>
{
    public VideosByIdsSpec(params VideoId[] ids)
    {
        Query.Where(v => ids.Contains(v.Id));
    }
}

public class PlaylistWithVideosSpec : Specification<Playlist>
{
    public PlaylistWithVideosSpec(params PlaylistId[] playlistIds)
    {
        Query.Where(pl => playlistIds.Contains(pl.Id))
             .Include(pl => pl.Videos);
    }    
}