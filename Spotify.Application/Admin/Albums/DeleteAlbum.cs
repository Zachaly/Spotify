
namespace Spotify.Application.Admin.Albums
{
    public class DeleteAlbum
    {
        private IAlbumsManager _albumsManager;

        public DeleteAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public async Task<bool> Execute(int id) => await _albumsManager.RemoveAlbumAsync(id);
    }
}
