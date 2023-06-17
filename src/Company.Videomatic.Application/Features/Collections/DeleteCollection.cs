﻿namespace Company.Videomatic.Application.Features.Collections;

/// <summary>
/// Deletes an existing collection.
/// </summary>
/// <param name="id"></param>
public record DeleteCollectionCommand(int Id) : IRequest<DeleteCollectionResponse>;

/// <summary>
/// This response is returned by DeleteCollectionCommand.
/// </summary>
/// <param name="Item"></param>
/// <param name="Deleted"></param>
public record DeleteCollectionResponse(Playlist? Item, bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record CollectionDeletedEvent(Playlist Item) : INotification;

/// <summary>
/// Handles the DeleteCollectionCommand.
/// </summary>
public class DeleteCollectionHandler : IRequestHandler<DeleteCollectionCommand, DeleteCollectionResponse>
{
    private readonly IPlaylistRepository _repository;
    private readonly IPublisher _publisher;
    public DeleteCollectionHandler(IPlaylistRepository playlistRepository, IPublisher publisher)
    {
        _repository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }
    public async Task<DeleteCollectionResponse> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var target = await _repository.GetByIdAsync(request.Id, null, cancellationToken);
        //if (target == null)
        //    return new DeleteCollectionResponse(null, false);
        //
        //await _repository.DeleteRangeAsync(new[] { target }, cancellationToken);
        //
        //await _publisher.Publish(new CollectionDeletedEvent(target!), cancellationToken);
        //
        //return new DeleteCollectionResponse(target, true);               
    }
}