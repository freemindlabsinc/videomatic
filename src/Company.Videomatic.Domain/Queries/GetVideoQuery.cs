﻿namespace Company.Videomatic.Domain.Queries;

public class GetVideoQuery : GetOneSpecification<Video>,
    IRequest<Video>
{
    public GetVideoQuery(
        int id,
        string[]? includes = null)
        : base(id, includes)
    { }

    public GetVideoQuery(
        string? providerVideoId = default, 
        string? videoUrl = default, 
        string[]? includes = default)    
        : base(includes)
    {
        if (!string.IsNullOrWhiteSpace(providerVideoId))
        {
            Query.Where(x => x.ProviderVideoId.StartsWith(providerVideoId));
        }

        if (!string.IsNullOrWhiteSpace(videoUrl))
        {
            Query.Where(x => (x.VideoUrl.StartsWith(videoUrl)));
        }
    }
}