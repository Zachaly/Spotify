
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class DeleteAlbum
    {
        private readonly IAlbumsManager _albumsManager;

        public DeleteAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Removes album with given id
        /// </summary>
        public async Task<bool> Execute(int id) => await _albumsManager.RemoveAlbumAsync(id);
    }
}
