
namespace Spotify.Database.UnitTests
{
    public class AlbumsManagerTests : DatabaseTest
    {
        private readonly IAlbumsManager _albumsManager;
        public AlbumsManagerTests() : base()
        {
            _albumsManager = new AlbumsManager(_dbContext);
        }

        [Fact]
        public async Task Add_Album()
        {
            var album = new Album
            {
                Name = "Berserker",
                MusicianId = 3,
            };

            await _albumsManager.AddAlbumAsync(album);

            var amonAmarth = _dbContext.Musicians.FirstOrDefault(musician => musician.Id == 3);

            Assert.Contains(_dbContext.Albums, x => x.Id == album.Id && album.Name == "Berserker");
            Assert.Contains(amonAmarth.Albums, x => x.Id == album.Id && x.Name == "Berserker");
        }

        [Fact]
        public async Task Add_Album_With_Invalid_Data()
        {
            var album = new Album
            {
                Name = null,
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _albumsManager.AddAlbumAsync(album));
        }

        [Fact]
        public void Get_Album_By_Id()
        {
            var album = _albumsManager.GetAlbumById(4, x => x);

            Assert.Equal(4, album.Id);
            Assert.Equal("Magma", album.Name);
            Assert.Equal(10, album.Songs.Count());
            Assert.Equal("Gojira", album.Musician.Name);
            Assert.Contains(album.Songs, song => song.Name == "Silvera");
        }

        [Fact]
        public void Get_Album_By_Id_Selector()
        {
            var albumModel = _albumsManager.GetAlbumById(4, x => new { Name = x.Name, Musician = x.Musician.Name });

            Assert.Equal("Magma", albumModel.Name);
            Assert.Equal("Gojira", albumModel.Musician);
        }

        [Fact]
        public void Get_Album_By_Nonexistent_Id()
            => Assert.Null(_albumsManager.GetAlbumById(2137, x => x));

        [Fact]
        public void Get_Albums()
        {
            var albums = _albumsManager.GetAlbums(x => x);

            Assert.Equal(7, albums.Count());
            Assert.Equal(2, albums.Where(x => x.MusicianId == 1).Count());
            Assert.Equal(3, albums.Where(x => x.MusicianId == 2).Count());
            Assert.Equal(2, albums.Where(x => x.MusicianId == 3).Count());
        }

        [Fact]
        public void Get_Albums_Selector()
        {
            var albumModels = _albumsManager.GetAlbums(x => new { Name = x.Name, SongCount = x.Songs.Count() });

            Assert.Equal(7, albumModels.Count());
            Assert.Contains(albumModels, album => album.Name == "Magma" && album.SongCount == 10);
            Assert.Contains(albumModels, album => album.Name == "Ride the Lightning" && album.SongCount == 8);
        }

        [Fact]
        public async Task Remove_Album()
        {
            var result = await _albumsManager.RemoveAlbumAsync(2);

            Assert.True(result);
            Assert.DoesNotContain(_dbContext.Albums, album => album.Name == "...And Justice for All");
        }

        [Fact]
        public async Task UpdateAlbum()
        {
            var result = await _albumsManager.UpdateAlbumAsync(1, album => 
            { 
                album.Name = "Ride the Thunder";
                album.MusicianId = 2;
            });

            var metallica = _dbContext.Musicians.FirstOrDefault(x => x.Id == 1);
            var gojira = _dbContext.Musicians.FirstOrDefault(x => x.Id == 2);

            Assert.True(result);
            Assert.Contains(_dbContext.Albums, album => album.Name == "Ride the Thunder" && album.Id == 1);
            Assert.DoesNotContain(_dbContext.Albums, album => album.Name == "Ride the Lightning");
            Assert.DoesNotContain(metallica.Albums, album => album.Id == 1);
            Assert.Contains(gojira.Albums, album => album.Id == 1);
        }
    }
}
