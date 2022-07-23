
namespace Spotify.Application.Admin.Albums
{
    public class UpdateAlbum
    {
        private IAlbumsManager _albumsManager;

        public UpdateAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public async Task<bool> Execute(Request request) 
            => await _albumsManager.UpdateAlbumAsync(request.AlbumId, album =>
            {
                album.Name = request.Name;
                album.MusicianId = request.MusicianId;
            });

        public class Request
        {
            public int AlbumId { get; set; }
            public int MusicianId { get; set; }
            public string Name { get; set; }
        }
    }
}
