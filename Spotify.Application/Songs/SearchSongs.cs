
namespace Spotify.Application.Songs
{
    [Service]
    public class SearchSongs
    {
        private readonly ISongsManager _songsManager;

        public SearchSongs(ISongsManager songsManager)
        {
            _songsManager = songsManager;
        }

        /// <summary>
        /// Searches for songs with similar name
        /// </summary>
        public IEnumerable<SongModel> Execute(string name)
            => _songsManager.GetSongsByName(name, 10, song => new SongModel
            {
                AlbumFileName = song.Album.FileName,
                AlbumId = song.AlbumId,
                Id = song.Id,
                Name = song.Name
            });

        public class SongModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string AlbumFileName { get; set; }
            public int AlbumId { get; set; }
        }
    }
}
