﻿using Company.Videomatic.Infrastructure.Data;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests;

public abstract class VideosTestsBase<TDbContext> : RepositoryTestsBase<TDbContext, Video>
    where TDbContext : VideomaticDbContext
{
    public VideosTestsBase(RepositoryFixture<TDbContext, Video> fixture) 
        : base(fixture)
    {
    }

    #region Queries 

    [Theory]
    [InlineData(null)]
    public virtual async Task GetVideosDTOQuery_AllVideoDTOs(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosDTOQuery();
        var response = await sender.Send(cmd);
        Fixture.Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(YouTubeVideos.HintsCount);

        var lastId = 0;
        foreach (var item in response.Items)
        {
            // Check they are in sequence by Id
            item.Id.Should().BeGreaterThan(lastId);
            lastId = item.Id;

            // Check they have basic properties
            item.Title.Should().NotBeNullOrWhiteSpace();    
            item.Description.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();
            item.VideoUrl.Should().NotBeNullOrWhiteSpace();
            item.ProviderId.Should().NotBeNullOrWhiteSpace();            

            // Check they don't include anything
            item.Artifacts.Should().BeEmpty();
            item.Thumbnails.Should().BeEmpty();
            item.Transcripts.Should().BeEmpty();            
        }
    }

    [Theory]
    [InlineData(null)]
    public virtual async Task GetVideosDTOQuery_Only2BVideosFromHttp(
            [FromServices] ISender sender)
    {
        var cmd = new GetVideosDTOQuery(
            ProviderVideoIdPrefix: "B",
            VideoUrlPrefix: "http"
            );
        
        var response = await sender.Send(cmd);
        Fixture.Output.WriteLine(JsonHelper.Serialize(response));

        // Should be all videos
        response.Items.Should().HaveCount(2); // 2 videos: BBd3aHnVnuE and BFfb2P5wxC0        
    }

    [Theory]
    [InlineData(null)]
    public virtual async Task GetTranscriptDTOQuery_Aldous(
            [FromServices] ISender sender)
    {
        var getVideosQry = new GetVideosDTOQuery(Take: 1, Includes: new[] { nameof(Video.Transcripts) } );
        QueryResponse<VideoDTO> firstVideo = await sender.Send(getVideosQry);

        var getTranscriptQry = new GetTranscriptDTOQuery(
            TranscriptId: firstVideo.Items!.First()!.Transcripts!.First().Id
            );

        var transcript = await sender.Send(getTranscriptQry);
        transcript.LineCount.Should().BeGreaterThan(0);

        Fixture.Output.WriteLine(JsonHelper.Serialize(transcript));
    }

    [Theory]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.RickAstley_NeverGonnaGiveYouUp}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.AldousHuxley_DancingShiva}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism}", null)]
    [InlineData($"https://www.youtube.com/watch?v={YouTubeVideos.HyonGakSunim_WhatIsZen}", null)]
    public async Task ImportVideoAndPersistToRepository(
       string url,
       [FromServices] IVideoImporter importer)
    {
        // Imports 
        Video video = await importer.ImportAsync(new Uri(url));

        video.Transcripts.Should().HaveCountGreaterThan(0);
        video.Transcripts.First().Lines.Should().HaveCountGreaterThan(0);
        video.Thumbnails.Should().HaveCountGreaterThan(0);

        // Persists
        await Fixture.Repository.UpdateAsync(video); // Will add a new record
        await Fixture.Repository.SaveChangesAsync();

        // Now reads
        video.Id.Should().BeGreaterThan(0);

        // TODO: fix this
        // var record = await Fixture.Repository.ListAsync()
        //     //.AsNoTracking()
        //     .Include(x => x.Transcripts)
        //     .ThenInclude(x => x.Lines)
        //     .Include(x => x.Thumbnails)
        //     .FirstAsync(v => v.Id == video.Id);
        // 
        // record.Should().NotBeNull();
        // record!.Id.Should().Be(video.Id);
        // record!.Title.Should().Be(video.Title);
        // record!.Description.Should().Be(video.Description);
        // 
        // record!.Thumbnails.Should().BeEquivalentTo(video.Thumbnails);
        // record!.Transcripts.Should().BeEquivalentTo(video.Transcripts);
        // 
        // db.Remove(video);
        // var res = await db.SaveChangesAsync();
        // res.Should().BeGreaterThan(0);
    }

    #endregion

    #region Commands

    [Theory]
    [InlineData(null, null, null, YouTubeVideos.HyonGakSunim_WhatIsZen)]
    public virtual async Task ImportVideoCommandWorks(
            [FromServices] ISender sender,
            [FromServices] IRepositoryBase<Video> repository,
            [FromServices] IRepositoryBase<Video> repository2,
            string videoId)
    {
        // Imports a video
        string url = YouTubeVideos.GetUrl(videoId);
        ImportVideoResponse response = await sender.Send(new ImportVideoCommand(url));

        // Verifies
        response.Should().NotBeNull();
        response.VideoId.Should().BeGreaterThan(0);

        Video? dbVideo = await repository.FirstOrDefaultAsync(
            new GetOneSpecification<Video>(response.VideoId, new[]
            {
                nameof(Video.Artifacts),
                nameof(Video.Thumbnails),
                nameof(Video.Transcripts),
                nameof(Video.Transcripts)+'.'+nameof(Transcript.Lines),
            }));

        dbVideo!.Should().NotBeNull();
        dbVideo!.Thumbnails.Count().Should().BeGreaterThan(0);
        dbVideo!.Transcripts.Count().Should().BeGreaterThan(0);
        dbVideo!.Artifacts.Count().Should().BeGreaterThan(0);

        // Cleans up
        await repository2.DeleteAsync(dbVideo!);
    }

    [Theory]
    [InlineData(null, null)]
    public virtual async Task ImportVideoCommandWorksForAllVideos(
            [FromServices] ISender sender,
            [FromServices] IRepositoryBase<Video> repository)
    {
        var newIds = new HashSet<int>();
        foreach (var videoId in YouTubeVideos.GetVideoIds())
        {
            ImportVideoResponse response = await sender.Send(
                new ImportVideoCommand(YouTubeVideos.GetUrl(videoId)));

            newIds.Add(response.VideoId).Should().BeTrue();
        }

        // Queries 
        IEnumerable<Video> videos = await repository.ListAsync(new GetVideosSpecification(newIds.ToArray()));
        videos.Should().HaveCount(newIds.Count);

        await repository.DeleteRangeAsync(videos);
    }

    [Theory]
    [InlineData(null)]
    public virtual async Task DeleteVideoCommandWorksForAllVideos(
            [FromServices] ISender sender)
    {
        // Imports 4 videos
        var videoIds = YouTubeVideos.GetVideoIds();
        var newIds = new HashSet<int>();
        foreach (var videoId in videoIds)
        {
            ImportVideoResponse response = await sender.Send(
                new ImportVideoCommand(YouTubeVideos.GetUrl(videoId)));

            response.VideoId.Should().BeGreaterThan(0);
            newIds.Add(response.VideoId);
        }

        // Queries 
        IEnumerable<Video> videos = await Fixture.Repository.ListAsync(new GetVideosSpecification(newIds.ToArray()));
        videos.Should().HaveCount(newIds.Count);

        // Deletes
        foreach (var video in videos)
        {
            DeleteVideoResponse response = await sender.Send(new DeleteVideoCommand(video.Id));
            response.Deleted.Should().BeTrue();
        }
    }
    #endregion
}