﻿namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="Id"> The id of the video to delete. </param>
public record DeleteVideoCommand(long Id) : IRequest<DeleteVideoResponse>, ICommandWithEntityId;

public record DeleteVideoResponse(long Id, bool Deleted);

/// <summary>
/// The validator for DeleteVideoCommand.
/// </summary>
internal class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}