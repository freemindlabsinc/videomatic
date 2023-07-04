﻿global using FluentValidation;
global using MediatR;

global using Company.Videomatic.Domain.Extensions;
global using Company.Videomatic.Application.Abstractions;

global using Company.Videomatic.Application.Features.Artifacts;
global using Company.Videomatic.Application.Features.Playlists;
global using Company.Videomatic.Application.Features.Transcripts;
global using Company.Videomatic.Application.Features.Videos;

global using Company.Videomatic.Domain.Abstractions;
global using Company.Videomatic.Domain.Aggregates.Artifact;
global using Company.Videomatic.Domain.Aggregates.Playlist;
global using Company.Videomatic.Domain.Aggregates.Transcript;
global using Company.Videomatic.Domain.Aggregates.Video;

global using Company.Videomatic.Application.Features.Artifacts.Commands;
global using Company.Videomatic.Application.Features.Playlists.Commands;
global using Company.Videomatic.Application.Features.Transcripts.Commands;
global using Company.Videomatic.Application.Features.Videos.Commands;

global using Company.Videomatic.Application.Features.Artifacts.Queries;
global using Company.Videomatic.Application.Features.Playlists.Queries;
global using Company.Videomatic.Application.Features.Transcripts.Queries;
global using Company.Videomatic.Application.Features.Videos.Queries;


global using Company.Videomatic.Domain.Specifications.Artifacts;
global using Company.Videomatic.Domain.Specifications.Videos;
global using Company.Videomatic.Domain.Specifications.Transcripts;
global using Company.Videomatic.Domain.Specifications.Playlists;