
namespace Spotify.Application.Albums
{
    [Service]
    public class GetAlbum
    {
        private readonly IAlbumsManager _albumsManager;

        public GetAlbum(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Gets album with all songs and info needed to play them by user
        /// </summary>
        public Response Execute(int id) => _albumsManager.GetAlbumById(id, album => new Response
        {
            Id = album.Id,
            Name = album.Name,
            Songs = album.Songs.Select(song => new SongModel
            {
                Id = song.Id,
                Name = song.Name,
                AlbumId = song.AlbumId,
                CreatorName = song.Creator.Name,
                Plays = song.Plays,
                CreatorId = song.Creator.Id,
                FileName = song.FileName,
                AlbumFileName = album.FileName
            }),
            SongCount = album.Songs.Count()
        });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
            public IEnumerable<SongModel> Songs { get; set; }
        }

        public class SongModel
        {
            public int Id { get; set; }
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public int AlbumId { get; set; }
            public long Plays { get; set; }
            public string FileName { get; set; }
            public string AlbumFileName { get; set; }
        }
    }
}
