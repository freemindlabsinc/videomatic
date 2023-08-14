﻿using Company.Videomatic.Application.Specifications;
using System.Linq;
using System.Runtime.CompilerServices;

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

        await repository.SaveChangesAsync(cancellationToken);

        return newLinks;
    }

    public static async Task<IReadOnlyDictionary<string, VideoId>> GetVideoProviderIds(this IRepository<Video> repository, IEnumerable<VideoId> videoIds, CancellationToken cancellationToken = default)
    {
        var videos = await repository.ListAsync(new VideosByIdsSpec(videoIds.ToArray()), cancellationToken);

        return videos.Where(v => v.Origin?.ProviderItemId != null)
                     .ToDictionary(v => v.Origin!.ProviderItemId , v => v.Id);
    }
}

public class TranscriptionByVideoId : Specification<Transcript>
{
    public TranscriptionByVideoId(IEnumerable<VideoId> videoIds)
    {
        Query.Where(t => videoIds.Contains(t.VideoId));      
    }
}

public class PlaylistsByProviderItemId : Specification<Playlist>
{
    public PlaylistsByProviderItemId(string providerId, IEnumerable<string> itemIds)
    {
        Query.Where(v =>
            v.Origin.ProviderId.Equals(providerId) &&
            itemIds.Contains(v.Origin.ProviderItemId)
        );
    }
}
