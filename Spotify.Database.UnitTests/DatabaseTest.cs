
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
            UsersSetup();
            LikesSetup();

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
                    FileName = "metallica.jpg"
                },
                new Musician
                {
                    Id = 2,
                    Name = "Gojira",
                    Description = "Best prog metal band",
                    FileName = "gojira.jpg"
                },
                new Musician
                {
                    Id = 3,
                    Name = "Amon Amarth",
                    Description = "VIKINGS",
                    FileName = "amonamarth.jpg"
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
                    FileName = "ridethelightning.jpg"
                },
                new Album
                {
                    Id = 2,
                    MusicianId = 1,
                    Name = "...And Justice for All",
                    FileName = "justiceforall.jpg"
                },
                new Album
                {
                    Id = 3,
                    MusicianId = 2,
                    Name = "The Way of All Flesh",
                    FileName = "thewayofallflesh.jpg"
                },
                new Album
                {
                    Id = 4,
                    MusicianId = 2,
                    Name = "Magma",
                    FileName = "magma.jpg"
                },
                new Album
                {
                    Id = 5,
                    MusicianId = 2,
                    Name = "Fortitude",
                    FileName = "fortitude.jpg"
                },
                new Album
                {
                    Id = 6,
                    MusicianId = 3,
                    Name = "Twilight of the Thunder God",
                    FileName = "twilightofthethundergod.jpg"
                },
                new Album
                {
                    Id = 7,
                    MusicianId = 3,
                    Name = "Versus the World",
                    FileName = "versustheworld.jpg"
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
                    Name = "Fight Fire With Fire",
                    FileName = "song.mp3",
                    Plays = 3,
                },
                new Song
                {
                    Id = 2,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Ride the Lightning",
                    FileName = "song.mp3",
                    Plays = 2,
                },
                new Song
                {
                    Id = 3,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "For Whom the Bell Tolls",
                    FileName = "song.mp3",
                    Plays = 1,
                },
                new Song
                {
                    Id = 4,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Fade to Black",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 5,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Trapped Under Ice",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 6,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Escape",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 7,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "Creeping Death",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 8,
                    MusicianId = 1,
                    AlbumId = 1,
                    Name = "The Call of Ktulu",
                    FileName = "song.mp3"
                },
            };
            var justiceForAll = new List<Song>
            {
                new Song
                {
                    Id = 9,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Blackened",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 10,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "...And Justice for All",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 11,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "One",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 12,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "The Shortest Straw",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 13,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Harvester of Sorrow",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 14,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "The Frayed Ends of Sanity",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 15,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Harvester of Sorrow",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 16,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "To Live Is To Die",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 17,
                    MusicianId = 1,
                    AlbumId = 2,
                    Name = "Dyers Eve",
                    FileName = "song.mp3"
                },
            };
            var theWayOfAllFlesh = new List<Song>
            {
                new Song
                {
                    Id = 18,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Oroborus",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 19,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Toxic Garbage Island",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 20,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "A Sight to Behold",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 21,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Yama's Messengers",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 22,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Silver Cord",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 23,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "All the Tears",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 24,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Adoration for None",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 25,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Art of Dying",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 26,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Esoteric Surgery",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 27,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Vacuity",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 28,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "Wolf Down the Earth",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 29,
                    AlbumId = 3,
                    MusicianId = 2,
                    Name = "The Way of All Flesh",
                    FileName = "song.mp3"
                },
            };
            var magma = new List<Song>
            {
                new Song
                {
                    Id = 30,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "The Shooting Star",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 31,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Silvera",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 32,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "The Cell",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 33,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Stranded",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 34,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Yellow Stone",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 35,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Magma",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 36,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Pray",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 37,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Only Pain",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 38,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Low Lands",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 39,
                    AlbumId = 4,
                    MusicianId = 2,
                    Name = "Liberation",
                    FileName = "song.mp3"
                },
            };
            var fortitude = new List<Song>
            {
                new Song
                {
                    Id = 40,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Born for One Thing",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 41,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Amazonia",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 42,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Another World",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 43,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Hold On",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 44,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "New Found",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 45,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Fortitude",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 46,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "The Chant",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 47,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Sphinx",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 48,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Into the Storm",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 49,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "The Trails",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 50,
                    AlbumId = 5,
                    MusicianId = 2,
                    Name = "Grind",
                    FileName = "song.mp3"
                },
            };
            var twilight = new List<Song>
            {
                new Song
                {
                    Id = 51,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Twilight of the Thunder God",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 52,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Free Will Sacrifice",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 53,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Guardians of Asgaard",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 54,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Where Is Your God?",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 55,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Varyags of Miklagaard",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 56,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Tattered Banners and Bloody Flags",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 57,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "No Fear for the Setting Sun",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 58,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "The Hero",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 59,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Live for the Kill",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 60,
                    AlbumId = 6,
                    MusicianId = 3,
                    Name = "Embrace of the Endless Ocean",
                    FileName = "song.mp3"
                },
            };
            var versusTheWorld = new List<Song>
            {
                new Song
                {
                    Id = 61,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Death in Fire",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 62,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "For the Stabwounds in Our Backs",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 63,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Where Silent Gods Stand Guard",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 64,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Versus the World",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 65,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Across the Rainbow Bridge",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 66,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Down the Slopes of Death",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 67,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Thousand Years of Oppression",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 68,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "Bloodshed",
                    FileName = "song.mp3"
                },
                new Song
                {
                    Id = 69,
                    AlbumId = 7,
                    MusicianId = 3,
                    Name = "...And Soon the World Will Cease to Be",
                    FileName = "song.mp3"
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

        private void UsersSetup()
        {
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "id1",
                    UserName = "user1",
                    FileName = "user1.jpg",
                    Email = "user1@email.com"
                },
                new ApplicationUser
                {
                    Id = "id2",
                    UserName = "user2",
                    FileName = "user2.jpg",
                    Email = "user2@email.com"
                },
                new ApplicationUser
                {
                    Id = "id3",
                    UserName = "user3",
                    FileName = "user3.jpg",
                    Email = "user3@email.com"
                }
            };

            _dbContext.Users.AddRange(users);

            _dbContext.SaveChanges();
        }

        private void LikesSetup()
        {
            var songLikes = new List<SongLike>
            {
                //user1
                new SongLike
                {
                    ApplicationUserId = "id1",
                    SongId = 1
                },
                new SongLike
                {
                    ApplicationUserId = "id1",
                    SongId = 2
                },
                new SongLike
                {
                    ApplicationUserId = "id1",
                    SongId = 3
                },
                new SongLike
                {
                    ApplicationUserId = "id1",
                    SongId = 4
                },
                //user2
                new SongLike
                {
                    ApplicationUserId = "id2",
                    SongId = 1
                },
                new SongLike
                {
                    ApplicationUserId = "id2",
                    SongId = 5
                },
                //user3
                new SongLike
                {
                    ApplicationUserId = "id3",
                    SongId = 6
                },
            };
            var albumLikes = new List<AlbumLike>
            {
                //user1
                new AlbumLike
                {
                    ApplicationUserId = "id1",
                    AlbumId = 1,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id1",
                    AlbumId = 2,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id1",
                    AlbumId = 3,
                },
                //user2
                new AlbumLike
                {
                    ApplicationUserId = "id2",
                    AlbumId = 5,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id2",
                    AlbumId = 6,
                },
                //user3
                new AlbumLike
                {
                    ApplicationUserId = "id3",
                    AlbumId = 2,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id3",
                    AlbumId = 3,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id3",
                    AlbumId = 4,
                },
                new AlbumLike
                {
                    ApplicationUserId = "id3",
                    AlbumId = 5,
                },
            };
            var musicianFollows = new List<MusicianFollow>
            {
                //user1
                new MusicianFollow
                {
                    ApplicationUserId = "id1",
                    MusicianId = 1,
                },
                new MusicianFollow
                {
                    ApplicationUserId = "id1",
                    MusicianId = 2,
                },
                //user2
                new MusicianFollow
                {
                    ApplicationUserId = "id2",
                    MusicianId = 3,
                },
                //user3
                new MusicianFollow
                {
                    ApplicationUserId = "id3",
                    MusicianId = 1,
                },
                new MusicianFollow
                {
                    ApplicationUserId = "id3",
                    MusicianId = 2,
                },
                new MusicianFollow
                {
                    ApplicationUserId = "id3",
                    MusicianId = 3,
                },
            };

            _dbContext.SongLikes.AddRange(songLikes);
            _dbContext.AlbumLikes.AddRange(albumLikes);
            _dbContext.MusicianFollows.AddRange(musicianFollows);

            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }
    }
}
