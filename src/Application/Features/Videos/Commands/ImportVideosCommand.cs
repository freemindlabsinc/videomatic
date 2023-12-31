﻿namespace Application.Features.Videos.Commands;

// TODO: iffy name
public class ImportVideosCommand(IEnumerable<string> urls, PlaylistId? destinationPlaylistId = null) : IRequest<Result<string>>
{
    public IEnumerable<string> Urls { get; } = urls;
    public PlaylistId? DestinationPlaylistId { get; } = destinationPlaylistId;

    #region Validator

    internal class Validator : AbstractValidator<ImportVideosCommand>
    {
        public Validator()
        {
            RuleForEach(v => v.Urls)
                .NotEmpty();
            //.Must(url => Uri.TryCreate(url, UriKind.Absolute, out _));        
        }
    }

    #endregion

    #region Handler

    internal class Handler(IBackgroundJobClient jobClient) : IRequestHandler<ImportVideosCommand, Result<string>>
    {
        public IBackgroundJobClient JobClient { get; } = jobClient ?? throw new ArgumentNullException(nameof(jobClient));


        public Task<Result<string>> Handle(ImportVideosCommand request, CancellationToken cancellationToken = default)
        {
            var jobId = JobClient.Enqueue<IVideoImporter>(imp => imp.ImportVideosAsync(request.Urls, (PlaylistId?)request.DestinationPlaylistId, null, cancellationToken));
            
            return Task.FromResult(new Result<string>(jobId));
        }
    }

    #endregion
}