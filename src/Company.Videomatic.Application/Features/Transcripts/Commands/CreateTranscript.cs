﻿namespace Company.Videomatic.Application.Features.Transcripts.Commands;

public record CreateTranscriptCommand(long VideoId,
                                      string Language,
                                      IEnumerable<string> Lines) : 
    ICreateCommand<Transcript>;

public class CreateTranscriptCommandValidator : AbstractValidator<CreateTranscriptCommand>
{
    public CreateTranscriptCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Language).NotEmpty();
        RuleFor(x => x.Lines).NotEmpty();
    }
}

