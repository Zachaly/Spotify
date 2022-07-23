
namespace Spotify.Application.Admin.Albums
{
    public class GetAlbums
    {
        private IAlbumsManager _albumsManager;

        public GetAlbums(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public IEnumerable<AlbumViewModel> Execute() => _albumsManager.GetAlbums(album => new AlbumViewModel
        {
            Creator = album.Musician.Name,
            Id = album.Id,
            Name = album.Name,
            SongCount = album.Songs.Count()
        });

        public class AlbumViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int SongCount { get; set; }
            public string Creator { get; set; }
        }
    }
}
