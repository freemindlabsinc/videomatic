﻿namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This niterface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IYouTubeHelper
{
    IAsyncEnumerable<PlaylistDTO> GetPlaylistsOfChannel(string channelId); // TODO: add cancellation  

    IAsyncEnumerable<Video> ImportVideosOfPlaylist(string playlistId); // TODO: add cancellation  

    IAsyncEnumerable<Video> ImportVideos(IEnumerable<string> youtubeVideoIds); // TODO: add cancellation    
    
    IAsyncEnumerable<Transcript> ImportTranscriptions(IEnumerable<VideoId> videoIds); // TODO: add cancellation    
}