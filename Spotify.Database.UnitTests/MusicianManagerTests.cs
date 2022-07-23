
namespace Spotify.Database.UnitTests
{
    public class MusicianManagerTests : DatabaseTest
    {
        private readonly IMusicianManager _musicianManager;
        public MusicianManagerTests() : base()
        {
            _musicianManager = new MusicianManager(_dbContext);
        }

        [Fact]
        public async Task Add_Musician_With_Valid_Data()
        {
            var musician = new Musician 
            { 
                Name = "Death",
                Description = "Best death metal band",
            };

            await _musicianManager.AddMusicianAsync(musician);

            var id = musician.Id;

            Assert.Contains(_dbContext.Musicians, musician => musician.Id == id);
            Assert.Contains(_dbContext.Musicians, musician => musician.Id == id && musician.Name == "Death");
            Assert.Contains(_dbContext.Musicians, 
                musician => musician.Id == id && musician.Description == "Best death metal band");
        }

        [Fact]
        public async Task Add_Musician_With_Invalid_Data()
        {
            var musician = new Musician
            {
                Name = "Death",
                Description = null,
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _musicianManager.AddMusicianAsync(musician));
        }

        [Fact]
        public async Task Delete_Musician()
        {
            await _musicianManager.DeleteMusicianAsync(1);

            Assert.DoesNotContain(_dbContext.Musicians, musician => musician.Id == 1);
            Assert.DoesNotContain(_dbContext.Musicians, musician => musician.Name == "Metallica");
            Assert.DoesNotContain(_dbContext.Musicians, musician => musician.Description == "Most popular metal band");
        }

        [Fact]
        public void Get_Musician_By_Id()
        {
            var musician = _musicianManager.GetMusicianById(2, x => x);

            Assert.NotNull(musician);
            Assert.Equal("Gojira", musician.Name);
            Assert.Equal("Best prog metal band", musician.Description);
            Assert.Equal(3, musician.Albums.Count());
            Assert.Equal(33, musician.Songs.Count());
        }

        [Fact]
        public void Get_Musician_By_Id_Selector()
        {
            var musicianModel = _musicianManager.GetMusicianById(3, x => new { x.Name, x.Description });

            Assert.NotNull(musicianModel);
            Assert.Equal("Amon Amarth", musicianModel.Name);
            Assert.Equal("VIKINGS", musicianModel.Description);
        }

        [Fact]
        public void Get_Musician_By_Non_Existent_Id()
            => Assert.Null(_musicianManager.GetMusicianById(10, x => x));

        [Fact]
        public void Get_Musicians()
        {
            var musicians = _musicianManager.GetMusicians(x => x);

            Assert.Equal(3, musicians.Count());
            Assert.Contains(musicians, musician => musician.Name == "Metallica");
            Assert.Contains(musicians, musician => musician.Name == "Gojira");
            Assert.Contains(musicians, musician => musician.Name == "Amon Amarth");
        }

        [Fact]
        public void Get_Musicians_Selector()
        {
            var musicians = _musicianManager.GetMusicians(x => new { Name = x.Name, AlbumCount = x.Albums.Count() });

            Assert.Contains(musicians, musician => musician.Name == "Metallica" && musician.AlbumCount == 2);
            Assert.Contains(musicians, musician => musician.Name == "Gojira" && musician.AlbumCount == 3);
            Assert.Contains(musicians, musician => musician.Name == "Amon Amarth" && musician.AlbumCount == 2);
        }

        [Fact]
        public async Task Update_Musician()
        {
            var metallica = _dbContext.Musicians.FirstOrDefault(x => x.Id == 1);

            await _musicianManager.UpdateMusicianAsync(1, "Metollica", "Popular metal band");

            Assert.Contains(_dbContext.Musicians, musician => musician.Id == 1 && musician.Name == "Metollica");
            Assert.Contains(_dbContext.Musicians, musician => musician.Id == 1 && musician.Description == "Popular metal band");
        } 
    }
}
