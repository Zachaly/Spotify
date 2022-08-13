
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class UpdateAlbum
    {
        private readonly IAlbumsManager _albumsManager;

        public UpdateAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Updated album with data given in request and returns model used in admin album panel
        /// </summary>
        public async Task<Response> Execute(Request request)
        { 
            await _albumsManager.UpdateAlbumAsync(request.AlbumId, album =>
            {
                album.Name = request.Name;
            });

            return _albumsManager.GetAlbumById(request.AlbumId, album => new Response
            {
                Id = album.Id,
                Name = album.Name,
                SongCount = album.Songs.Count()
            });
        }

        public class Request
        {
            public int AlbumId { get; set; }
            public string Name { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
        }
    }
}
