
namespace Spotify.Database.UnitTests
{
    public class MusicianManagerTests : DatabaseTest
    {
        private readonly IMusicianManager _musicianManager;
        public MusicianManagerTests() : base()
        {
            _musicianManager = new MusicianManager(_dbContext);
        }
    }
}
