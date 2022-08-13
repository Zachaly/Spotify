
namespace Spotify.Application.Admin.Albums
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
        /// Gets precise info about an album
        /// </summary>
        public Response Execute(int id) => _albumsManager.GetAlbumById(id, album => new Response
        {
            Id = album.Id,
            CreatorId = album.Musician.Id,
            Creator = album.Musician.Name,
            Name = album.Name,
            Songs = album.Songs.Select(song => new SongViewModel
            {
                Id = song.Id,
                Name = song.Name,
                Plays = song.Plays
            })
        });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IEnumerable<SongViewModel> Songs { get; set; }
            public int CreatorId { get; set; }
            public string Creator { get; set; }
        }

        public class SongViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }
    }
}
