﻿
using Ardalis.Specification;

namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

/// <summary>
/// The handler for ImportVideoCommand.
/// </summary>
public class ImportVideoCommandHandler : IRequestHandler<ImportVideoCommand, ImportVideoResponse>
{
    readonly IVideoImporter _importer;
    readonly IRepositoryBase<Video> _repository;
    readonly IVideoAnalyzer _analyzer;

    public ImportVideoCommandHandler(
        IVideoImporter importer,
        IRepositoryBase<Video> repository,
        IVideoAnalyzer analyzer)
    {
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _analyzer = analyzer ?? throw new ArgumentNullException(nameof(analyzer));
    }

    public async Task<ImportVideoResponse> Handle(ImportVideoCommand request, CancellationToken cancellationToken)
    {
        // Imports the video from the provider url
        Video video = await _importer.ImportAsync(new Uri(request.VideoUrl));

        // Generates artifacts for the video
        // TODO: these should be queued and processed asynchronously
        Task<Artifact> summaryTask = _analyzer.SummarizeVideoAsync(video);
        Task<Artifact> reviewTask = _analyzer.ReviewVideoAsync(video);

        Artifact[] artifacts = await Task.WhenAll(summaryTask, reviewTask);

        video.AddArtifacts(artifacts);

        // Creates the video 
        var updatedVideo = await _repository.AddAsync(video);

        return new ImportVideoResponse(
            VideoId: updatedVideo.Id,
            ThumbNailCount: updatedVideo.Thumbnails.Count(),
            TranscriptCount: updatedVideo.Transcripts.Count(),
            ArtifactsCount: updatedVideo.Artifacts.Count()
            );
    }

}