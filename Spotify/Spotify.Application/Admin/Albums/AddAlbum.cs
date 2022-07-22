
namespace Spotify.Application.Admin.Albums
{
    public class AddAlbum
    {
        private IAlbumsManager _albumsManager;

        public AddAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public async Task<bool> Execute(Request request)
            => await _albumsManager.AddAlbumAsync(new Album 
            { 
                Name = request.Name,
                MusicianId = request.MusicianId 
            });

        public class Request
        {
            public string Name { get; set; }
            public int MusicianId { get; set; }
        }
    }
}
