
namespace Spotify.Application.Albums
{
    [Service]
    public class SearchAlbums
    {
        private IAlbumsManager _albumsManager;

        public SearchAlbums(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        public IEnumerable<AlbumModel> Execute(string name)
            => _albumsManager.GetAlbumsByName(name, 10, album => new AlbumModel
            {
                FileName = album.FileName,
                Id = album.Id,
                Name = album.Name
            });

        public class AlbumModel
        {
            public int Id { get; set; }
            public string Name { get; set; } 
            public string FileName { get; set; }
        }
    }
}
