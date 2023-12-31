﻿using Application.Tests.Helpers;
using Domain.Playlists;

namespace Application.Tests.Validation;

public class PlaylistRequestsValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public PlaylistRequestsValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
    }

    [Theory]
    [InlineData(null, null, 1)]
    [InlineData("", null, 1)]
    [InlineData("Play list", null, 0)]
    [InlineData("Play list", "Description", 0)]
    public void ValidateCreatePlaylistCommand(string name, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<CreatePlaylistCommand>(new(name, description), expectedErrors);
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

    [Theory]
    [InlineData(null, null, null, null, 0)]
    [InlineData(null, null, -1, -1, 2)]
    [InlineData("", null, 1, 1, 0)]
    [InlineData("filter", null, 1, 1, 0)]
    public void ValidateGetPlaylistQuery(string? filter, string? orderBy, int? page, int? pageSize, int expectedErrors)
    {
        ValidatorHelper.Validate<GetPlaylistsQuery>(new(filter, orderBy, pageSize, page), expectedErrors);
    }
}