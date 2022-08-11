
namespace Spotify.Database.UnitTests
{
    public class PlaylistManagerTests : DatabaseTest
    {
        private readonly IPlaylistManager _playlistManager;

        public PlaylistManagerTests() : base()
        {
            _playlistManager = new PlaylistManager(_dbContext);
        }

        [Fact]
        public async Task Add_Playlist()
        {
            var playlist = new Playlist
            {
                Name = "fun stuff",
                CreatorId = "id3",
                FileName = "image.jpg",
            };

            var result = await _playlistManager.AddPlaylist(playlist);

            Assert.True(result);
            Assert.Contains(_dbContext.Playlists,
                playlist => playlist.Name == "fun stuff" && playlist.CreatorId == "id3");
        }

        [Fact]
        public async Task Add_Invalid_Playlist()
            => await Assert.ThrowsAsync<DbUpdateException>(
                async () => await _playlistManager.AddPlaylist(new Playlist()));

        [Fact]
        public async Task Add_Song_To_Playlist()
        {
            var result = await _playlistManager.AddSongToPlaylist(10, 1);

            var songs = _dbContext.Playlists.Include(x => x.Songs).FirstOrDefault(x => x.Id == 1)?.Songs;

            Assert.True(result);
            Assert.Contains(songs, song => song.SongId == 10);
            Assert.Contains(_dbContext.PlaylistSongs, song => song.PlaylistId == 1 && song.SongId == 10);
        }

        [Fact]
        public void Get_Playlist()
        {
            var playlist = _playlistManager.GetPlaylist(2, x => x);

            Assert.Equal("playlist2", playlist.Name);
            Assert.Equal(3, playlist.Songs.Count());
            Assert.Equal("id1", playlist.CreatorId);
        }

        [Fact]
        public void Get_Playlist_Selector()
        {
            var playlistModel = _playlistManager.GetPlaylist(3, x => new { Name = x.Name, CreatorName = x.Creator.UserName });

            Assert.Equal("playlist3", playlistModel.Name);
            Assert.Equal("user1", playlistModel.CreatorName);
        }

        [Fact]
        public void Get_User_Playlists()
        {
            var playlists = _playlistManager.GetUserPlaylists("id1", x => x);

            Assert.Equal(3, playlists.Count());
            Assert.All(playlists, playlist => Assert.Equal("id1", playlist.CreatorId));
        }

        [Fact]
        public async Task Remove_Playlist()
        {
            var result = await _playlistManager.RemovePlaylist(1);

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Playlists, playlist => playlist.Id == 1);
            Assert.DoesNotContain(_dbContext.PlaylistSongs, song => song.PlaylistId == 1);
        }

        [Fact]
        public async Task Remove_Song_From_Playlist()
        {
            var result = await _playlistManager.RemoveSongFromPlaylist(21, 3);

            var songs = _dbContext.Playlists.Include(x => x.Songs).FirstOrDefault(x => x.Id == 3)?.Songs;

            Assert.True(result);
            Assert.DoesNotContain(songs, song => song.SongId == 21);
            Assert.Single(songs);
            Assert.DoesNotContain(_dbContext.PlaylistSongs, song => song.SongId == 21 && song.PlaylistId == 3);
        }

        [Fact]
        public async Task Update_Playlist()
        {
            var result = await _playlistManager.UpdatePlaylist(1, playlist => playlist.Name = "new name");

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Playlists, playlist => playlist.Id == 1 && playlist.Name == "playlist1");
            Assert.Contains(_dbContext.Playlists, playlist => playlist.Id == 1 && playlist.Name == "new name");
        }

        [Fact]
        public void Does_Playlist_Contain_Song()
        {
            Assert.True(_playlistManager.DoesPlaylistContainSong(3, 21));
            Assert.False(_playlistManager.DoesPlaylistContainSong(2, 37));
        }

        [Fact]
        public async Task Set_Cover_Picture()
        {
            var result = await _playlistManager.SetCoverPicture(1, "newimg.jpg");

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Playlists, playlist => playlist.FileName == "playlist1.jpg");
            Assert.Contains(_dbContext.Playlists, playlist => playlist.Id == 1 && playlist.FileName == "newimg.jpg");
        }

        [Fact]
        public void Search_Playlist()
        {
            var result = _playlistManager.GetPlaylistsByName("playlist3", 5, x => x);

            Assert.Equal(4, result.Count());
            Assert.Equal("playlist3", result.First().Name);
        }
    }
}
