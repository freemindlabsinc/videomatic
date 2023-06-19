using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.Videos.Commands;
using Company.Videomatic.Application.Features.Videos.Queries;

namespace Company.Videomatic.Infrastructure.Data.Tests.SqlServer;

[Collection("DbContextTests")]
public class SqlServerPlaylistsTests : IClassFixture<SqlServerDbContextFixture>
{
    public SqlServerPlaylistsTests(
        SqlServerDbContextFixture fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        
        Fixture.SkipDeletingDatabase = true;
    }

    public SqlServerDbContextFixture Fixture { get; }
    
    [Fact]
    public async Task T01_CreatePlaylist()
    {
        // Executes
        CreatePlaylistCommand createCmd = new (Name: "My playlist 1", Description: $"A description for my playlist {DateTime.Now}");
        CreatePlaylistResponse createResponse = await Fixture.PlaylistCommands.Handle(createCmd);

        // Checks
        createResponse.Id.Should().BeGreaterThan(0);

        GetPlaylistByIdQuery qry = new(createResponse.Id);
        GetPlaylistByIdResponse getByIdResponse = await Fixture.PlaylistsQueries.Handle(qry);
        
        var playlist = getByIdResponse.Items.Single();

        playlist.Id.Should().Be(createResponse.Id);   
        playlist.Name.Should().Be(createCmd.Name);
        playlist.Description.Should().Be(createCmd.Description);
    }

    [Fact]
    public async Task T02_CreatesPlaylistWithTwoVideos()
    {
        // Executes
        CreatePlaylistCommand createPlaylistCmd = new(Name: "My playlist 2", Description: $"A description for my playlist {DateTime.Now}");
        CreatePlaylistResponse createPlaylistResponse = await Fixture.PlaylistCommands.Handle(createPlaylistCmd);

        CreateVideoCommand createVid1Cmd = new(Location: "youtube.com/v?V1", Title: "A title", Description: "A description");
        CreateVideoResponse createVid1Response = await Fixture.VideoCommands.Handle(createVid1Cmd);

        CreateVideoCommand createVid2Cmd = new(Location: "youtube.com/v?V2", Title: "A second title", Description: "A second description");
        CreateVideoResponse createVid2Response = await Fixture.VideoCommands.Handle(createVid2Cmd);

        AddVideosToPlaylistCommand addVidsCmd = new(PlaylistId: createPlaylistResponse.Id, VideoIds: new[] {  createVid1Response.Id, createVid2Response.Id });
        AddVideosToPlaylistResponse addVidsResponse = await Fixture.PlaylistCommands.Handle(addVidsCmd); // Should add 2 videos
        AddVideosToPlaylistResponse emptyAddVidsResponse = await Fixture.PlaylistCommands.Handle(addVidsCmd); // Should not add anything as they are both dups

        // Checks
        createPlaylistResponse.Id.Should().BeGreaterThan(0);
        createVid1Response.Id.Should().BeGreaterThan(0);
        createVid2Response.Id.Should().BeGreaterThan(0);

        GetVideosByIdResponse videosResponse = await Fixture.VideoQueries.Handle(new GetVideosByIdQuery(new[] { createVid1Response.Id, createVid2Response.Id }));
        videosResponse.Items.Should().HaveCount(2);

        emptyAddVidsResponse.PlaylistId.Should().Be(createPlaylistResponse.Id);
        emptyAddVidsResponse.VideoIds.Should().BeEmpty();   
    }

    readonly string[] AllPlaylistFields = new[] { nameof(Playlist.Videos), "Videos.Thumbnails", "Videos.Tags", "Videos.Artifacts", "Videos.Transcripts", "Videos.Transcripts.Lines" };

    [Fact]
    public async Task T03_CreatePlaylistWithACompleteVideo()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist 3", description: $"A playlist with 2 complete videos {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");
        
        vid1.AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100))
            .AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200));

        vid1.AddTag(new Tag("Tag1"))
            .AddTag(new Tag("Tag2"));

        vid1.AddArtifact(new Artifact(title: "A complete summary", type: "AI", text: "Bla bla"))
            .AddArtifact(new Artifact(title: "A complete analysis", type: "AI", text: "More bla bla"));
        
        var trans1 = new Transcript("EN");
        trans1.AddLine(new TranscriptLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        var trans2 = new Transcript("IT");
        trans1.AddLine(new TranscriptLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        vid1.AddTranscript(trans1)
            .AddTranscript(trans2);
        
        newPlaylist.AddVideo(vid1);

        // Executes
        //var updatedPlaylist = await Fixture.CommandsHandler.CreateAsync(newPlaylist);
        //
        //// Asserts
        //newPlaylist.Id.Should().Be(0);
        //updatedPlaylist.Id.Should().BeGreaterThan(0);
        //
        //var fromDbPlaylist = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields));
        //
        //fromDbPlaylist.Should().NotBeNull();
        //fromDbPlaylist!.Videos.Should().HaveCount(newPlaylist.Videos.Count);
        //foreach (var vid in fromDbPlaylist!.Videos)
        //{
        //    vid.Thumbnails.Should().NotBeEmpty();
        //    vid.Tags.Should().NotBeEmpty();
        //    vid.Artifacts.Should().NotBeEmpty();            
        //    vid.Transcripts.Should().NotBeEmpty();
        //    vid.Transcripts.First().Lines.Should().NotBeEmpty();    
        //}

        // If I use Should().BeEquivalentTo I get the following error. I assume it is because of Video.Playlists...
        //    Expected fromDbPlaylist2.Videos[0].Tags[0].Videos[0] to be [youtube.com/v?VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Tags[1].Videos[0] to be[youtube.com / v ? VCompleteA, Thumbnails: 2, Transcripts: 2] A complete title, but it contains a cyclic reference.
        //    Expected fromDbPlaylist2.Videos[0].Playlists[0] to be Company.Videomatic.Domain.Model.Playlist
        //    { JSON }, but it contains a cyclic reference.
        //fromDbPlaylist2.Should().BeEquivalentTo(updatedPlaylist);
    }

    [Fact]
    public async Task T04_CreateNonEmptyPlaylistAndUpdatesIt()
    {
        // Prepares
        var newPlaylist = new Playlist(name: "My playlist 4", description: $"A playlist with 2 complete videos {DateTime.Now}");
        var vid1 = new Video(location: "youtube.com/v?VCompleteA", title: "A complete title", description: "A complete description");

        vid1.AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_1", resolution: ThumbnailResolution.Default, height: 100, width: 100))
            .AddThumbnail(new Thumbnail(location: "youtubethumbs.com/T1_2", resolution: ThumbnailResolution.Medium, height: 200, width: 200));

        vid1.AddTag(new Tag("Tag1"))
            .AddTag(new Tag("Tag2"));

        vid1.AddArtifact(new Artifact(title: "A complete summary", type: "AI", text: "Bla bla"))
            .AddArtifact(new Artifact(title: "A complete analysis", type: "AI", text: "More bla bla"));

        var trans1 = new Transcript("EN");
        trans1.AddLine(new TranscriptLine(text: "This is", startsAt: TimeSpan.FromSeconds(0), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "a long transcript", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        var trans2 = new Transcript("IT");
        trans1.AddLine(new TranscriptLine(text: "Questa e'", startsAt: TimeSpan.FromSeconds(1), duration: TimeSpan.FromSeconds(1)));
        trans1.AddLine(new TranscriptLine(text: "una lunga transcrizione", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2)));

        vid1.AddTranscript(trans1)
            .AddTranscript(trans2);

        newPlaylist.AddVideo(vid1);

        //// Executes
        //var updatedPlaylist = await Fixture.CommandsHandler.CreateAsync(newPlaylist);
        //var fromDb = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id)) ?? throw new Exception("Playlist not found");
        //
        //const string NewDescription = "I changed the description";
        //fromDb.UpdateDescription(NewDescription);
        //var updatedPlaylist2 = await Fixture.CommandsHandler.UpdateAsync(fromDb);
        //var fromDb2 = await Fixture.CommandsHandler.GetByIdAsync(new(updatedPlaylist.Id, AllPlaylistFields)) ?? throw new Exception("Playlist not found");
        //
        //// Asserts
        //fromDb2.Description.Should().Be(NewDescription);    
    }
}