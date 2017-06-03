﻿using Xunit;
using DotNetStandardSpotifyWebApi.Helpers;
using DotNetStandardSpotifyWebApi.Authorization;
using DotNetStandardSpotifyWebApi.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DotNetStandardSpotifyWebApi.Tests {
    public class ObjectModelTests {
        //The user to test with
        private const string CurrentUserId = "nishumvar";

        //An Artist the current user follows
        private const string Artist_Follow = "13FGWDOwAoQyIBuZLtCjN9"; //Taku Takahashi

        //An Artist the current user does not follow
        private const string Artist_NoFollow = "22bE4uQ6baNwSHPVcDxLCe"; // The Rolling Stones

        //A User the current user follows
        private const string User_Follow = "22vlmkwgjsj2qt7wmztbymneq"; //Tanya Giang

        //A User the current user does not follow
        private const string User_NoFollow = "canaloff"; //canaloff

        //An Album the current user has saved
        private const string Album_Saved = "1BhxcmomhUniLCu173Rpn4"; //Tower of Heaven (Original Soundtrack) - flashygoodness

        //An Album the current user has not saved
        private const string Album_NoSaved = "4l4u9e9jSbotSXNjYfOugy"; //Let It Bleed - The Rolling Stones

        //A playlist the current user follows
        private const string Playlist_Follow = "5gS0M9C45kZvJfgLRKRTJ1"; //This House - nishumvar

        //A playlist the current user does not follow
        private const string Playlist_NoFollow = "06m5HzAGIkYyLsDdNpWoCp"; //Rolling Stones Best Of

        //A track the current user has in their library
        private const string Track_Saved = "6Prexw6BkRje5joeSDg0iN"; //Deezy Daisy - Oxford Remix

        //A track the current user does not have in their library
        private const string Track_NoSaved = "6H3kDe7CGoWYBabAeVWGiD"; //Gimme Shelter - The Rolling Stones

        //Track URIs for endpoints that require multiple track uris
        private static List<string> TrackUris = new List<string>() {
                "spotify:track:7rXhnFjG74YKMgq0R89Bpz",//Papi
                "spotify:track:1TG5DvegcKAJOuKmKCKOIU",//Over You
                "spotify:track:3GK0gr4QMLeXSm50eCBWp8", //Dance - Oliver Remix
            };

        private const string CategoryCheck = "party";

        //Credentials object to use in testing
        OAuthCredentials Creds = null;

        //Private setup function called by each test to ensure that the spotify api can be successfully called
        private async Task setupCreds() {
            if (Creds == null) {
                SecretManager.InitializeAppAPIKeys();
                Creds = await AuthorizationCodeFlow.RefreshAccessToken(SecretManager.RefreshToken, SecretManager.client_id, SecretManager.client_secret);
            }
        }


        [Fact]
        public async void ShouldGetCurrentUser() {
            await setupCreds();
            User me = await Endpoints.GetCurrentUser(Creds.Access_token);
            Assert.False(me.WasError, "Object Error");
            Assert.True(me.Id == CurrentUserId, $"Expected {CurrentUserId}, got {me.Id}");
        }

        public async void ShouldGetAnAlbum() {
            await setupCreds();

        }

        public async void ShouldGetSeveralAlbums() {
            await setupCreds();

        }

        public async void ShouldGetAnAblumsTracks() {
            await setupCreds();

        }

        public async void ShouldGetAnArtist() {
            await setupCreds();

        }

        public async void ShouldGetSeveralArtists() {
            await setupCreds();

        }

        public async void ShouldGetAnArtistsAlbums() {
            await setupCreds();

        }

        public async void ShouldGetAnArtistsTopTracks() {
            await setupCreds();

        }

        public async void ShouldGetRelatedArtists() {
            await setupCreds();

        }

        public async void ShouldGetAudoAnalysis() {
            await setupCreds();

        }

        public async void ShouldGetAudioFeatures() {
            await setupCreds();

        }

        public async void ShoudlGetSeveralAudioFeatures() {
            await setupCreds();

        }

        public async void ShouldGetFeaturedPlaylists() {
            await setupCreds();

        }

        public async void ShouldGetNewReleases() {
            await setupCreds();

        }

        public async void ShouldGetCategories() {
            await setupCreds();

        }

        public async void ShouldGetACategory() {
            await setupCreds();

        }

        public async void ShouldGetCategoriesPlaylists() {
            await setupCreds();

        }

        public async void ShouldGetUsersFollowedArtists() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldFollowAnArtist() {
            await setupCreds();
            List<string> ids = new List<string>() {
                Artist_NoFollow
            };
            RegularError res = await Endpoints.FollowArtists(Creds.Access_token, ids);
            Assert.False(res.WasError, "Object Error");
            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsArtists(Creds.Access_token, ids);
            Assert.True(bools[0], $"Expected to follow artist {ids[0]}, but don't");
            //Restoring to default state
            await Endpoints.UnfollowArtists(Creds.Access_token, ids);
        }

        [Fact]
        public async void ShouldFollowAUser() {
            await setupCreds();
            List<string> ids = new List<string>() {
                User_NoFollow
            };
            RegularError res = await Endpoints.FollowUsers(Creds.Access_token, ids);
            Assert.False(res.WasError, "Object Error");
            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsUsers(Creds.Access_token, ids);
            Assert.True(bools[0], $"Expected to follow User {ids[0]}, but don't");
            //Restoring to default state
            await Endpoints.UnfollowUsers(Creds.Access_token, ids);

        }

        [Fact]
        public async void ShouldUnfollowAnArtist() {
            await setupCreds();
            List<string> ids = new List<string>() {
                Artist_NoFollow
            };
            RegularError res = await Endpoints.UnfollowArtists(Creds.Access_token, ids);
            Assert.False(res.WasError, "Object Error");
            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsArtists(Creds.Access_token, ids);
            Assert.False(bools[0], $"Expected to not follow artist {ids[0]}, but do");
        }

        [Fact]
        public async void ShouldUnfollowAUser() {
            await setupCreds();
            List<string> ids = new List<string>() {
                User_NoFollow
            };
            RegularError res = await Endpoints.UnfollowUsers(Creds.Access_token, ids);
            Assert.False(res.WasError, "Object Error");
            IReadOnlyList<bool> bools = await Endpoints.CheckCurrentUserFollowsUsers(Creds.Access_token, ids);
            Assert.False(bools[0], $"Expected to not follow artist {ids[0]}, but do");
        }

        public async void ShouldCheckIfUserFollowsUser() {
            await setupCreds();

        }

        public async void ShouldCheckIfUserFollowsArtist() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldFollowAPlaylist() {
            await setupCreds();
            //https://open.spotify.com/user/rollingstonesmusic/playlist/06m5HzAGIkYyLsDdNpWoCp
            RegularError res = await Endpoints.FollowAPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow);
            Assert.False(res.WasError, "Object Error");
            IReadOnlyList<bool> bools = await Endpoints.CheckUsersFollowsPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow, new string[] { CurrentUserId });
            Assert.True(bools[0], $"Expected to follow playlist {Playlist_NoFollow}, but don't");
            //Restoring to default state
            await Endpoints.UnfollowAPlaylist(Creds.Access_token, "rollingstonesmusic", Playlist_NoFollow);
        }

        public async void ShouldUnfollowAPlaylist() {
            await setupCreds();

        }

        public async void ShouldSaveTrackForUser() {
            await setupCreds();

        }

        public async void ShouldGetUsersSavedTracks() {
            await setupCreds();

        }

        public async void ShouldRemoveUsersSavedTrack() {
            await setupCreds();

        }

        public async void ShouldCheckUsersSavedTracks() {
            await setupCreds();

        }

        public async void ShouldSaveAlbumForUser() {
            await setupCreds();

        }

        public async void ShouldGetUsersSavedAlbums() {
            await setupCreds();

        }

        public async void ShouldRemoveUsersSavedAlbum() {
            await setupCreds();

        }

        public async void ShouldCheckUsersSavedAlbums() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldGetUsersTopArtists() {
            await setupCreds();
            Paging<Artist> page = await Endpoints.GetUsersTopArtists(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
        }

        [Fact]
        public async void ShouldGetUsersTopTracks() {
            await setupCreds();
            Paging<Track> page = await Endpoints.GetUsersTopTracks(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Items.Count == 20, $"Expected 20 items, got {page.Items.Count}");
        }

        public async void ShouldGetRecommendations() {
            await setupCreds();
        }

        public async void ShouldSearchForArtists() {
            await setupCreds();
        }

        public async void ShouldSearchForAlbums() {
            await setupCreds();

        }

        public async void ShouldSearchForTracks() {
            await setupCreds();

        }

        public async void ShouldSearchForPlaylists() {
            await setupCreds();

        }

        public async void ShoudlSearchForEverything() {
            await setupCreds();

        }

        [Fact]
        public async void ShouldGetATrack() {
            await setupCreds();
            Track t = await Endpoints.GetATrack(Creds.Access_token, Track_Saved);
            Assert.False(t.WasError, "Object Error");
            Assert.True(t.Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {t.Name}");
        }

        [Fact]
        public async void ShouldGetSeveralTracks() {
            await setupCreds();
            List<string> ids = new List<string>(){
              Track_Saved,
              Track_NoSaved,
            };
            List<Track> tracks = (await Endpoints.GetSeveralTracks(Creds.Access_token, ids)).ToList();
            Assert.True(tracks.Count == 2, $"Expected 2 tracks, got {tracks.Count}");
            Assert.True(tracks[0].Name == "Deezy Daisy - Oxford Remix", $"Expected Deezy Daisy - Oxford Remix, got {tracks[0].Name}");
            Assert.True(tracks[1].Name == "Gimme Shelter", $"Expected Gimme Shelter, got {tracks[1].Name}");
        }

        [Fact]
        public async void GetAUsersPublicProfile() {
            await setupCreds();
            User u = await Endpoints.GetUsersProfile(Creds.Access_token, User_Follow);
            Assert.False(u.WasError, "Object Error");
            Assert.True(u.DisplayName == "Tanya Giang", $"Expected Tanya Giang, got {u.DisplayName}");
        }

        [Fact]
        public async void GetAUsersPublicPlaylists() {
            await setupCreds();
            Paging<Playlist> page = await Endpoints.GetUsersPlaylists(Creds.Access_token, User_Follow);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total >= 7, $"Expected at least 7 items, got {page.Total}");
        }

        [Fact]
        public async void GetCurrentUsersPlaylists() {
            await setupCreds();
            Paging<Playlist> page = await Endpoints.GetCurrentUsersPlaylists(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total >= 20, $"Expected at least 20 items, but got {page.Total}");
        }

        [Fact]
        public async void ShouldGetAPlaylist() {
            await setupCreds();
            Playlist p = await Endpoints.GetAPlaylist(Creds.Access_token, CurrentUserId, Playlist_Follow);
            Assert.False(p.WasError, "Object Error");
            Assert.True(p.Total == 5, $"Expected 5 tracks, got {p.Total}");
            Assert.True(p.Name == "This House");
        }

        [Fact]
        public async void ShouldGetAPlaylistsTracks() {
            await setupCreds();
            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, Playlist_Follow);
            Assert.False(page.WasError, "Object Error");
            Assert.True(page.Total == 5, $"Expected 5 tracks, got {page.Total}");
        }

        [Fact]
        public async void ShouldCreateAPlaylist() {
            await setupCreds();
            Playlist created = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "TEST");
            Assert.False(created.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldModifyAPlaylist() {
            await setupCreds();
            RegularError res = await Endpoints.ChangePlaylistDetails(Creds.Access_token, CurrentUserId, Playlist_Follow, null, null, "", "Songs for This House, baby.");
            Assert.False(res.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldAddSongsToPlaylist() {
            await setupCreds();
            string pid = "58g0qfBM60xjsJadLkzumx";
            List<string> uris = new List<string>(){
                "spotify:track:7rXhnFjG74YKMgq0R89Bpz"
            };
            RegularError res = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, pid, uris);
            Assert.False(res.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldRemoveSongsFromPlaylist() {
            await setupCreds();
            string pid = "6cFJgP266Kp31iY8ewuZrv"; // TEST playlist
            RegularError res = await Endpoints.RemoveTracksFromPlaylist(Creds.Access_token, CurrentUserId, pid, TrackUris);
            Assert.False(res.WasError, "Failed to delete from playlist");
        }

        [Fact]
        public async void ShouldReorderAPlaylistsTracks() {
            await setupCreds();
            //First create a new playlist
            Playlist p = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "Test Reordering");
            //Then Add songs in a certain order
            RegularError AddTrackError = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, p.Id, TrackUris);
            Assert.False(AddTrackError.WasError, "Failed to add tracks to playlist");
            //Then reorder
            RegularError  ReorderError = await Endpoints.ReorderPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id, 0, 3); //Should move Papi to end
            Assert.False(ReorderError.WasError, "Failed to issue reorder command");
            //Then check the reorder
            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
            Assert.True(page.Items[0].Track.Uri.Equals(TrackUris[1]), "Expected the uris to be different.");
            Assert.True(page.Items[1].Track.Uri.Equals(TrackUris[2]), "Expected the uris to be different.");
            Assert.True(page.Items[2].Track.Uri.Equals(TrackUris[0]), "Expected the uris to be different.");
            //Then unfollow the playlist
            await Endpoints.UnfollowAPlaylist(Creds.Access_token, CurrentUserId, p.Id);
        }

        [Fact]
        public async void ShouldReplaceAPlaylistsTracks() {
            await setupCreds();
            //First create a new playlist
            Playlist p = await Endpoints.CreateAPlaylist(Creds.Access_token, CurrentUserId, "Test Replacement");
            //Then Add songs in a certain order
            RegularError AddTrackError = await Endpoints.AddTracksToPlaylist(Creds.Access_token, CurrentUserId, p.Id, TrackUris);
            Assert.False(AddTrackError.WasError, "Failed to add tracks to playlist");
            //Then remove every song
            RegularError ReorderError = await Endpoints.ReplacePlaylistTracks(Creds.Access_token, CurrentUserId, p.Id, new List<string>() { "spotify:track:1TG5DvegcKAJOuKmKCKOIU"}); //Should have only Over You in it
            Assert.False(ReorderError.WasError, "Failed to issue reorder command at first");
            //Then check the reorder
            Paging<PlaylistTrack> page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
            Assert.True(page.Total == 1, $"Expected there to be 1 item, but playlist still has {TrackUris.Count}.");
            //Test for clearing the list
            ReorderError = await Endpoints.ReplacePlaylistTracks(Creds.Access_token, CurrentUserId, p.Id, new List<string>()); //Should now be empty
            Assert.False(ReorderError.WasError, "Failed to issue replace command at second");
            //Then check the reorder
            page = await Endpoints.GetAPlaylistsTracks(Creds.Access_token, CurrentUserId, p.Id);
            Assert.True(page.Total == 0, $"Expected there to be no items, but playlist still has {TrackUris.Count}.");
            //Then unfollow the playlist
            await Endpoints.UnfollowAPlaylist(Creds.Access_token, CurrentUserId, p.Id);
        }

        [Fact]
        public async void ShouldGetUsersRecentlyPlayedTracks() {
            await setupCreds();
            CursorBasedPaging<PlayHistory> page = await Endpoints.GetCurrentUsersRecentlyPlayedTracks(Creds.Access_token);
            Assert.False(page.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldGetUsersAvailableDevices() {
            await setupCreds();
            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
            Assert.True(devices.Any(), "Expected at least 1 device, got none");
        }

        [Fact]
        public async void ShouldGetCurrentlyPlayingSong() {
            await setupCreds();
            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);
            Assert.False(current.WasError, "Object Error");
        }

        [Fact]
        public async void ShouldToggleUsersPlayback() {
            await setupCreds();
            //First get current playback devices
            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
            Assert.True(devices.Any(), "Expected at least 1 playback device. Got none");

            //Get currently playing song so we can restore it later.
            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);
            //Stop playback
            RegularError reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id);
            Assert.False(reg.WasError, "Expected no error, got an error");
            //Wait 4 seconds
            await Task.Delay(4000);
            //Restart playback
            reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id, uris:new List<string>() {current.Item.Uri });
            Assert.False(reg.WasError, "Expected no error, got an error");

        }

        [Fact]
        public async void ShouldStartPlaybackAtSecondOffset() {
            await setupCreds();
            //First get current playback devices
            IReadOnlyList<Device> devices = await Endpoints.GetUsersAvailableDevices(Creds.Access_token);
            Assert.True(devices.Any(), "Expected at least 1 playback device. Got none");

            //Get currently playing song so we can restore it later.
            CurrentlyPlayingContext current = await Endpoints.GetUsersCurrentlyPlayingInformation(Creds.Access_token);

            //Start playback
            RegularError reg = await Endpoints.StartOrResumePlayback(Creds.Access_token, devices[0].Id, uris:TrackUris, offset:1 );
            Assert.False(reg.WasError, "Expected no error, got an error");
        }

        [Fact]
        public async void ShouldPauseUsersPlayback() {
            await setupCreds();
            RegularError res = await Endpoints.PauseUsersPlayback(Creds.Access_token);
            Assert.False(res.WasError, $"Expected to puase users playback, but failed. See: {res.Message}");
        }

        [Fact]
        public async void ShouldSkipToNextTrack() {
            await setupCreds();
            RegularError res = await Endpoints.SkipPlaybackToUsersNextTrack(Creds.Access_token);
            Assert.False(res.WasError, $"Expected to skip users playback forwards, but failed. See: {res.Message}");

        }

        [Fact]
        public async void ShouldSkipToPreviousTrack() {
            await setupCreds();
            RegularError res = await Endpoints.SkipPlaybackToUsersPreviousTrack(Creds.Access_token);
            Assert.False(res.WasError, $"Expected to skip users playback backwards, but failed. See: {res.Message}");
        }

        [Fact]
        public async void ShouldSeekTo0() {
            await setupCreds();
            RegularError res = await Endpoints.SeekToPositionInCurrentlyPlayingTrack(Creds.Access_token, 0);
            Assert.False(res.WasError, $"Expected to seek to 0, but failed. See: {res.Message}");
        }
    }
}
