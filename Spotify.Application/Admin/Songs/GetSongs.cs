
namespace Spotify.Application.Admin.Songs
{
    [Service]
    public class GetSongs
    {
        private readonly IAlbumsManager _albumsManager;

        public GetSongs(IAlbumsManager albumsManager)
        {
            _albumsManager = albumsManager;
        }

        /// <summary>
        /// Gets song models used in admin panel grouped by album
        /// </summary>
        public IEnumerable<AlbumModel> Execute()
            => _albumsManager.GetAlbums(album => new AlbumModel
            {
                Id = album.Id,
                Name = album.Name,
                CreatorName = album.Musician.Name,
                CreatorId = album.MusicianId,
                Songs = album.Songs.Select(song => new SongViewModel
                {
                    Id = song.Id,
                    Name = song.Name,
                    Plays = song.Plays
                })
            });

        public class SongViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }

        public class AlbumModel
        {
            public int CreatorId { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public IEnumerable<SongViewModel> Songs { get; set; }
            public int Id { get; set; }
        }
    }
}
