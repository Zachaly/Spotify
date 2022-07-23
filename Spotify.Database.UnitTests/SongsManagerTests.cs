
namespace Spotify.Database.UnitTests
{
    public class SongsManagerTests : DatabaseTest
    {
        private readonly ISongsManager _songsManager;
        public SongsManagerTests()
        {
            _songsManager = new SongsManager(_dbContext);
        }
    }
}
