
namespace Spotify.Database.UnitTests
{
    public class AlbumsManagerTests : DatabaseTest
    {
        private readonly IAlbumsManager _albumsManager;
        public AlbumsManagerTests() : base()
        {
            _albumsManager = new AlbumsManager(_dbContext);
        }
    }
}
