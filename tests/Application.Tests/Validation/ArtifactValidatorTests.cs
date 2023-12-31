﻿using Application.Tests.Helpers;
using Application.Features.Artifacts.Commands;
using Domain.Videos;
using Domain.Playlists;
using Azure.Core;

namespace Application.Tests.Validation;

public class ArtifactValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }
    public IServiceProvider ServiceProvider { get; }

    public ArtifactValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
        ServiceProvider = serviceProvider;
    }

    void ValidateInternal<TCommand>(TCommand command, int expectedErrors)
        where TCommand : class
    {
        Type? validatorType = typeof(TCommand).GetNestedType("Validator");
        if (validatorType == null)
        {
            throw new InvalidOperationException($"Validator for {typeof(TCommand).Name} not found.");
        }
        // create an instance of the validator
        var validator = (IValidator<TCommand>?)Activator.CreateInstance(validatorType);
        var validation = validator.TestValidate(command);
        validation.Errors.Should().HaveCount(expectedErrors);
    }
    
    [Theory]
    [InlineData(1, "AI", "Name", "Text", 0)]
    [InlineData(0, "AI", "Name", "Text", 1)]
    [InlineData(3, null, "Name", "Text", 1)]
    [InlineData(4, "AI", null, "Text", 1)]
    [InlineData(5, null, null, null, 3)]
    public void ValidateCreateArtifactCommand(int videoId, string type, string name, string? text, int expectedErrors)
    {
        ValidatorHelper.Validate<CreateArtifactCommand>(new((VideoId)videoId, name, type, text), expectedErrors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]
    public void ValidateDeletePlaylistCommand(PlaylistId id, int expectedErrors)
    {
        ValidatorHelper.Validate<DeletePlaylistCommand>(new(id), expectedErrors);
    }

    [Theory]
    [InlineData(-1, null, null, 2)]
    [InlineData(-1, "", null, 2)]
    [InlineData(1, null, null, 1)]
    [InlineData(1, "Play list", null, 0)]
    [InlineData(2, "Play list", "Description", 0)]
    public void ValidateUpdatePlaylistCommand(PlaylistId id, string name, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<UpdatePlaylistCommand>(new(id, name, description), expectedErrors);
    }
}
