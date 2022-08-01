
namespace Spotify.Application.Admin.Albums
{
    [Service]
    public class AddAlbum
    {
        private IAlbumsManager _albumsManager;

        public AddAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Adds album with info specified in request and returns model of album used in admin panel
        /// </summary>
        public async Task<Response> Execute(Request request)
        {
            var album = new Album
            {
                Name = request.Name,
                MusicianId = request.MusicianId,
                FileName = request.FileName
            };

            await _albumsManager.AddAlbumAsync(album);

            album = _albumsManager.GetAlbumById(album.Id, x => x);

            return new Response
            {
                Id = album.Id,
                Creator = album.Musician.Name,
                Name = album.Name,
                SongCount = album.Songs.Count()
            };
        }

        public class Request
        {
            public string Name { get; set; }
            public int MusicianId { get; set; }
            public string FileName { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
            public string Creator { get; set; }
        }
    }
}
