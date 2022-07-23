
namespace Spotify.Database.UnitTests
{
    /// <summary>
    /// Setup for in memory database used in tests
    /// </summary>
    public abstract class DatabaseTest : IDisposable
    {
        protected AppDbContext _dbContext;

        public DatabaseTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>();
            options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new AppDbContext(options.Options);

            MusicianSetup();
            AlbumsSetup();
            SongsSetup();

            _dbContext.SaveChanges();
        }

        // I am fully aware that this code is way too long and looks awful
        private void MusicianSetup()
        {
            var musicians = new List<Musician>
            {
                new Musician
                {
                    Id = 1,
                    Name = "Metallica",
                    Description = "Most popular metal band",
                },
                new Musician
                {
                    Id = 2,
                    Name = "Gojira",
                    Description = "Best prog metal band"
                },
                new Musician
                {
                    Id = 3,
                    Name = "Amon Amarth",
                    Description = "VIKINGS"
                },
            };

            _dbContext.Musicians.AddRange(musicians);
        }

        private void AlbumsSetup()
        {
            var albums = new List<Album>
            {
                new Album
                {
                    Id = 1,
                    MusicianId = 1,
                    Name = "Ride the Lightning",
                },
                new Album
                {
                    Id = 2,
                    MusicianId = 1,
                    Name = "...And Justice for All"
                },
                new Album
                {
                    Id = 3,
                    MusicianId = 2,
                    Name = "The Way of All Flesh"
                },
                new Album
                {
                    Id = 4,
                    MusicianId = 2,
                    Name = "Magma"
                },
                new Album
                {
                    Id = 5,
                    MusicianId = 2,
                    Name = "Fortitude"
                },
                new Album
                {
                    Id = 6,
                    MusicianId = 3,
                    Name = "Twilight of the Thunder God"
                },
                new Album
                {
                    Id = 7,
                    MusicianId = 3,
                    Name = "Versus the World"
                },
            };

            _dbContext.Albums.AddRange(albums);
        }

        private void SongsSetup()
        {
            var rideTheLightning = new List<Song>
            {
                new Song
                {
                    Id = 1,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Fight Fire With Fire"
                },
                new Song
                {
                    Id = 2,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Ride the Lightning"
                },
                new Song
                {
                    Id = 3,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "For Whom the Bell Tolls"
                },
                new Song
                {
                    Id = 4,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Fade to Black"
                },
                new Song
                {
                    Id = 5,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Trapped Under Ice"
                },
                new Song
                {
                    Id = 6,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Escape"
                },
                new Song
                {
                    Id = 7,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Creeping Death"
                },
                new Song
                {
                    Id = 8,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "The Call of Ktulu"
                },
            };
            var justiceForAll = new List<Song>
            {
                new Song
                {
                    Id = 9,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Blackened"
                },
                new Song
                {
                    Id = 10,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "...And Justice for All"
                },
                new Song
                {
                    Id = 11,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "One"
                },
                new Song
                {
                    Id = 12,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "The Shortest Straw"
                },
                new Song
                {
                    Id = 13,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Harvester of Sorrow"
                },
                new Song
                {
                    Id = 14,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "The Frayed Ends of Sanity"
                },
                new Song
                {
                    Id = 15,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Harvester of Sorrow"
                },
                new Song
                {
                    Id = 16,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "To Live Is To Die"
                },
                new Song
                {
                    Id = 17,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Dyers Eve"
                },
            };
            var theWayOfAllFlesh = new List<Song>
            {
                new Song
                {
                    Id = 18,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Oroborus"
                },
                new Song
                {
                    Id = 19,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Toxic Garbage Island"
                },
                new Song
                {
                    Id = 20,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "A Sight to Behold"
                },
                new Song
                {
                    Id = 21,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Yama's Messengers"
                },
                new Song
                {
                    Id = 22,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Silver Cord"
                },
                new Song
                {
                    Id = 23,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "All the Tears"
                },
                new Song
                {
                    Id = 24,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Adoration for None"
                },
                new Song
                {
                    Id = 25,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Art of Dying"
                },
                new Song
                {
                    Id = 26,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Esoteric Surgery"
                },
                new Song
                {
                    Id = 27,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Vacuity"
                },
                new Song
                {
                    Id = 28,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Wolf Down the Earth"
                },
                new Song
                {
                    Id = 29,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Way of All Flesh"
                },
            };
            var magma = new List<Song>
            {
                new Song
                {
                    Id = 30,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "The Shooting Star"
                },
                new Song
                {
                    Id = 31,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Silvera"
                },
                new Song
                {
                    Id = 32,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "The Cell"
                },
                new Song
                {
                    Id = 33,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Stranded"
                },
                new Song
                {
                    Id = 34,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Yellow Stone"
                },
                new Song
                {
                    Id = 35,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Magma"
                },
                new Song
                {
                    Id = 36,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Pray"
                },
                new Song
                {
                    Id = 37,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Only Pain"
                },
                new Song
                {
                    Id = 38,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Low Lands"
                },
                new Song
                {
                    Id = 39,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Liberation"
                },
            };
            var fortitude = new List<Song>
            {
                new Song
                {
                    Id = 40,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Born for One Thing"
                },
                new Song
                {
                    Id = 41,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Amazonia"
                },
                new Song
                {
                    Id = 42,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Another World"
                },
                new Song
                {
                    Id = 43,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Hold On"
                },
                new Song
                {
                    Id = 44,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "New Found"
                },
                new Song
                {
                    Id = 45,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Fortitude"
                },
                new Song
                {
                    Id = 46,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "The Chant"
                },
                new Song
                {
                    Id = 47,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Sphinx"
                },
                new Song
                {
                    Id = 48,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Into the Storm"
                },
                new Song
                {
                    Id = 49,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "The Trails"
                },
                new Song
                {
                    Id = 50,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Grind"
                },
            };
            var twilight = new List<Song>
            {
                new Song
                {
                    Id = 50,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Twilight of the Thunder God"
                },
                new Song
                {
                    Id = 51,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Free Will Sacrifice"
                },
                new Song
                {
                    Id = 52,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Guardians of Asgaard"
                },
                new Song
                {
                    Id = 53,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Where Is Your God?"
                },
                new Song
                {
                    Id = 54,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Varyags of Miklagaard"
                },
                new Song
                {
                    Id = 55,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Tattered Banners and Bloody Flags"
                },
                new Song
                {
                    Id = 56,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "No Fear for the Setting Sun"
                },
                new Song
                {
                    Id = 57,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "The Hero"
                },
                new Song
                {
                    Id = 58,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Live for the Kill"
                },
                new Song
                {
                    Id = 59,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Embrace of the Endless Ocean"
                },
            };
            var versusTheWorld = new List<Song>
            {
                new Song
                {
                    Id = 60,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Death in Fire"
                },
                new Song
                {
                    Id = 61,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "For the Stabwounds in Our Backs"
                },
                new Song
                {
                    Id = 62,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Where Silent Gods Stand Guard"
                },
                new Song
                {
                    Id = 63,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Versus the World"
                },
                new Song
                {
                    Id = 64,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Across the Rainbow Bridge"
                },
                new Song
                {
                    Id = 65,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Down the Slopes of Death"
                },
                new Song
                {
                    Id = 66,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Thousand Years of Oppression"
                },
                new Song
                {
                    Id = 67,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Bloodshed"
                },
                new Song
                {
                    Id = 68,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "...And Soon the World Will Cease to Be"
                },
            };

            _dbContext.Songs.AddRange(rideTheLightning);
            _dbContext.Songs.AddRange(justiceForAll);
            _dbContext.Songs.AddRange(theWayOfAllFlesh);
            _dbContext.Songs.AddRange(magma);
            _dbContext.Songs.AddRange(fortitude);
            _dbContext.Songs.AddRange(twilight);
            _dbContext.Songs.AddRange(versusTheWorld);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
