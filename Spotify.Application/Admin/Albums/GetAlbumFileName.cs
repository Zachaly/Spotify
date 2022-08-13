
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class GetAlbumFileName
    {
        private readonly IAlbumsManager _albumsManager;

        public GetAlbumFileName(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public string Execute(int id) => _albumsManager.GetAlbumById(id, album => album.FileName);
    }
}
