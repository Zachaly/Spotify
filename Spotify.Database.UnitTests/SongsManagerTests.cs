
namespace Spotify.Database.UnitTests
{
    public class SongsManagerTests : DatabaseTest
    {
        private readonly ISongsManager _songsManager;
        public SongsManagerTests()
        {
            _songsManager = new SongsManager(_dbContext);
        }

        [Fact]
        public async Task Add_Song()
        {
            var song = new Song
            {
                Name = "Of Blood And Salt",
                AlbumId = 3,
                MusicianId = 2,
                FileName = "song.mp3"
            };

            var result = await _songsManager.AddSongAsync(song);

            var gojira = _dbContext.Musicians.FirstOrDefault(x => x.Id == 2);
            var album = _dbContext.Albums.FirstOrDefault(x => x.Id == 3);

            Assert.True(result);
            Assert.Contains(_dbContext.Songs, x => x.Name == "Of Blood And Salt");
            Assert.Contains(gojira.Songs, x => x.Id == song.Id && x.Name == "Of Blood And Salt");
            Assert.Contains(album.Songs, x => x.Id == song.Id && x.Name == "Of Blood And Salt");
        }

        [Fact]
        public async Task Add_Song_With_Invalid_Data()
        {
            var song = new Song
            {
                Name = null,
                MusicianId = 1,
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _songsManager.AddSongAsync(song));
        }

        [Fact]
        public void Get_Song_By_Id()
        {
            var song = _songsManager.GetSongById(9, x => x);

            Assert.NotNull(song);
            Assert.Equal(9, song.Id);
            Assert.Equal("Blackened", song.Name);
            Assert.Equal("Metallica", song.Creator.Name);
            Assert.Equal("...And Justice for All", song.Album.Name);
            Assert.Equal("song.mp3", song.FileName);
        }

        [Fact]
        public void Get_Song_By_Id_Selector()
        {
            var song = _songsManager.GetSongById(61, x => new { Name = x.Name, Creator = x.Creator.Name });

            Assert.Equal("Death in Fire", song.Name);
            Assert.Equal("Amon Amarth", song.Creator);
        }

        [Fact]
        public void Get_Song_By_Nonexistent_Id()
            => Assert.Null(_songsManager.GetSongById(2137, x => x));

        [Fact]
        public void Get_Songs()
        {
            var songs = _songsManager.GetSongs(x => x);

            Assert.Equal(69, songs.Count());
        }

        [Fact]
        public void Get_Songs_Selector()
        {
            var songModels = _songsManager.GetSongs(x => new { Name = x.Name, Album = x.Album.Name });

            Assert.Contains(songModels, song => song.Album == "Versus the World");
            Assert.Contains(songModels, song => song.Name == "Oroborus");
        }

        [Fact]
        public async Task Remove_Song()
        {
            var result = await _songsManager.RemoveSongAsync(1);

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Songs, song => song.Name == "Fight Fire With Fire");
            Assert.DoesNotContain(_dbContext.Songs, song => song.Id == 1);
        }

        [Fact]
        public async Task Update_Song()
        {
            var result = await _songsManager.UpdateSongAsync(37, song =>
            {
                song.Name = "Only Pleasure";
                song.MusicianId = 3;
                song.AlbumId = 6;
            });

            var originalCreator = _dbContext.Musicians.FirstOrDefault(x => x.Id == 2);
            var newCreator = _dbContext.Musicians.FirstOrDefault(x => x.Id == 3);

            var originalAlbum = _dbContext.Albums.FirstOrDefault(x => x.Id == 4);
            var newAlbum = _dbContext.Albums.FirstOrDefault(x => x.Id == 6);

            Assert.Contains(_dbContext.Songs, song => song.Id == 37 && song.Name == "Only Pleasure");
            Assert.DoesNotContain(_dbContext.Songs, song => song.Name == "Only Pain");
            Assert.Contains(newCreator.Songs, song => song.Id == 37);
            Assert.DoesNotContain(originalCreator.Songs, song => song.Id == 37);
            Assert.Contains(newAlbum.Songs, song => song.Id == 37);
            Assert.DoesNotContain(originalAlbum.Songs, song => song.Id == 37);
        }

        [Fact]
        public async Task Add_Play()
        {
            var result = await _songsManager.AddPlay(69);

            Assert.True(result);
            Assert.Equal(1, _dbContext.Songs.FirstOrDefault(x => x.Id == 69).Plays);
        }

        [Fact]
        public void Get_Top_Songs()
        {
            var result = _songsManager.GetTopSongs(1, 3, x => x).ToList();

            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
            Assert.Equal(3, result[2].Id);

            Assert.Equal(3, result[0].Plays);
            Assert.Equal(2, result[1].Plays);
            Assert.Equal(1, result[2].Plays);
        }
    }
}
