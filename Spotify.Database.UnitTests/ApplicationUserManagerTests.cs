
namespace Spotify.Database.UnitTests
{
    public class ApplicationUserManagerTests : DatabaseTest
    {
        private readonly IApplicationUserManager _appUserManager;

        public ApplicationUserManagerTests() : base()
        {
            _appUserManager = new ApplicationUserManager(_dbContext);
        }

        [Fact]
        public void Get_User_By_Email()
        {
            var result = _appUserManager.GetUserByEmail("user1@email.com");

            Assert.Equal("user1", result.UserName);
            Assert.Equal("user1.jpg", result.FileName);
            Assert.Equal("id1", result.Id);
        }

        [Fact]
        public void Get_User_By_Unused_Email()
            => Assert.Null(_appUserManager.GetUserByEmail("user4@gmail.com"));
        

        [Fact]
        public void Is_Email_Occupied()
        {
            Assert.True(_appUserManager.IsEmailOccupied("user1@email.com"));
            Assert.False(_appUserManager.IsEmailOccupied("user4@email.com"));
        }

        [Fact]
        public void Get_User_By_Id()
        {
            var result = _appUserManager.GetUserById("id1", x => x);

            Assert.Equal("user1", result.UserName);
            Assert.Equal("user1@email.com", result.Email);
            Assert.Equal(4, result.LikedSongs.Count());
            Assert.Equal(3, result.LikedAlbums.Count());
            Assert.Equal(2, result.FollowedMusicians.Count());
        }

        [Fact]
        public void Get_User_By_Unused_Id() => 
            Assert.Null(_appUserManager.GetUserById("id2137", x => x));

        [Fact]
        public void Get_User_Liked_Songs()
        {
            var songs = _appUserManager.GetUserLikedSongs("id2", x => x);

            Assert.Equal(2, songs.Count());
            Assert.Contains(songs, song => song.SongId == 1);
            Assert.Contains(songs, song => song.SongId == 5);
            Assert.All(songs, song => Assert.Equal("id2", song.ApplicationUserId));
        }

        [Fact]
        public void Get_User_Liked_Songs_Selector()
        {
            var songModels = _appUserManager.
                GetUserLikedSongs("id1", x => new { Name = x.Song.Name, CreatorName = x.Song.Creator.Name });

            Assert.Contains(songModels, song => song.CreatorName == "Metallica");
            Assert.Contains(songModels, song => song.Name == "Fight Fire With Fire");
            Assert.Contains(songModels, song => song.Name == "Ride the Lightning");
            Assert.Contains(songModels, song => song.Name == "For Whom the Bell Tolls");
            Assert.Contains(songModels, song => song.Name == "Fade to Black");
        }

        [Fact]
        public async Task Follow_Musician_Add()
        {
            var result = await _appUserManager.FollowMusician("id1", 3);

            var user = _dbContext.Users.Include(x => x.FollowedMusicians).
                ThenInclude(x => x.Musician)
                .FirstOrDefault(x => x.Id == "id1");

            Assert.True(result);
            Assert.Equal(3, user.FollowedMusicians.Count());
            Assert.Contains(user.FollowedMusicians, follow => follow.Musician.Name == "Gojira");
            Assert.Contains(user.FollowedMusicians, follow => follow.Musician.Name == "Amon Amarth");
            Assert.Contains(user.FollowedMusicians, follow => follow.Musician.Name == "Metallica");
        }

        [Fact]
        public async Task Follow_Musician_Remove()
        {
            var result = await _appUserManager.FollowMusician("id1", 1);

            var user = _dbContext.Users.Include(x => x.FollowedMusicians).
                ThenInclude(x => x.Musician)
                .FirstOrDefault(x => x.Id == "id1");

            Assert.True(result);
            Assert.Single(user.FollowedMusicians);
            Assert.Contains(user.FollowedMusicians, follow => follow.Musician.Name == "Gojira");
            Assert.DoesNotContain(user.FollowedMusicians, follow => follow.Musician.Name == "Metallica");
        }

        [Fact]
        public async Task Like_Song_Add()
        {
            var result = await _appUserManager.LikeSong("id2", 2);

            var user = _dbContext.Users.Include(x => x.LikedSongs).FirstOrDefault(x => x.Id == "id2");

            Assert.True(result);
            Assert.Equal(3, user.LikedSongs.Count());
            Assert.Contains(user.LikedSongs, song => song.SongId == 2);
        }

        [Fact]
        public async Task Like_Song_Remove()
        {
            var result = await _appUserManager.LikeSong("id2", 1);

            var user = _dbContext.Users.Include(x => x.LikedSongs).FirstOrDefault(x => x.Id == "id2");

            Assert.True(result);
            Assert.Single(user.LikedSongs);
            Assert.DoesNotContain(user.LikedSongs, song => song.SongId == 1);
        }

        [Fact]
        public async Task Like_Album_Add()
        {
            var result = await _appUserManager.LikeAlbum("id3", 1);

            var user = _dbContext.Users.Include(x => x.LikedAlbums).FirstOrDefault(x => x.Id == "id3");

            Assert.True(result);
            Assert.Equal(5, user.LikedAlbums.Count());
            Assert.Contains(user.LikedAlbums, album => album.AlbumId == 1);
        }

        [Fact]
        public async Task Like_Album_Remove()
        {
            var result = await _appUserManager.LikeAlbum("id3", 2);

            var user = _dbContext.Users.Include(x => x.LikedAlbums).FirstOrDefault(x => x.Id == "id3");

            Assert.True(result);
            Assert.Equal(3, user.LikedAlbums.Count());
            Assert.DoesNotContain(user.LikedAlbums, album => album.AlbumId == 2);
        }

        [Fact]
        public void Is_Song_Liked()
        {
            Assert.True(_appUserManager.IsSongLiked("id3", 6));
            Assert.False(_appUserManager.IsSongLiked("id3", 1));
        }

        [Fact]
        public void Is_Album_Liked()
        {
            Assert.True(_appUserManager.IsAlbumLiked("id1", 1));
            Assert.False(_appUserManager.IsAlbumLiked("id1", 4));
        }

        [Fact]
        public void Is_Musician_Followed()
        {
            Assert.True(_appUserManager.IsMusicianFollowed("id2", 3));
            Assert.False(_appUserManager.IsMusicianFollowed("id2", 1));
        }

        [Fact]
        public async Task Update_User()
        {
            var result = await _appUserManager.UpdateUser("id1", user => user.UserName = "firstUser");

            Assert.True(result);
            Assert.Contains(_dbContext.Users, user => user.Id == "id1" && user.UserName == "firstUser");
            Assert.DoesNotContain(_dbContext.Users, user => user.UserName == "user1");
        }

        [Fact]
        public async Task Set_Default_Profile_Picture()
        {
            var result = await _appUserManager.SetDefaultProfilePicture("id3", "default.jpg");

            Assert.True(result);
            Assert.Contains(_dbContext.Users, user => user.Id == "id3" && user.FileName == "default.jpg");
            Assert.DoesNotContain(_dbContext.Users, user => user.FileName == "user3.jpg");
        }

        [Fact]
        public void Search_User()
        {
            var result = _appUserManager.GetUsersByName("user1", 2, x => x);

            Assert.Equal(2, result.Count());
            Assert.Equal("user1", result.First().UserName);
        }

        [Fact]
        public void Is_User_Manager()
        {
            Assert.True(_appUserManager.IsUserManagerOfMusician("id3", 1));
            Assert.False(_appUserManager.IsUserManagerOfMusician("id3", 2));
        }
    }
}
