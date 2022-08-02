
namespace Spotify.Application.Albums
{
    [Service]
    public class GetAlbums
    {
        private IAlbumsManager _albumsManager;

        public GetAlbums(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Gets albums with info needed to link show them to user
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AlbumModel> Execute() => _albumsManager.GetAlbums(album => new AlbumModel
        {
            Id = album.Id,
            Name = album.Name,
            CreatorName = album.Musician.Name,
            Plays = album.Songs.Sum(song => song.Plays),
            FileName = album.FileName,
        });

        public class AlbumModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CreatorName { get; set; }
            public long Plays { get; set; }
            public string FileName { get; set; }
        }
    }
}
