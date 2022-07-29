
namespace Spotify.Application.Musicians
{
    [Service]
    public class GetMusician
    {
        private IMusicianManager _musicianManager;
        private IAlbumsManager _albumsManager;
        private ISongsManager _songsManager;

        public GetMusician(IMusicianManager musicianManager, IAlbumsManager albumsManager, ISongsManager songsManager)
        {
            _musicianManager = musicianManager;
            _albumsManager = albumsManager;
            _songsManager = songsManager;
        }

        public Response Execute(int id) => _musicianManager.GetMusicianById(id, musician => new Response
        {
            Id = id,
            Name = musician.Name,
            Description = musician.Description,
            NumberOfFollowers = musician.Followers.Count(),
            TopAlbums = _albumsManager.GetTopAlbums(musician.Id, 5, album => new AlbumModel
            {
                Name = album.Name,
                Id = album.Id,
                Plays = album.Songs.Sum(song => song.Plays)
            }),
            TopSongs = _songsManager.GetTopSongs(musician.Id, 10, song => new SongModel
            {
                Id = song.Id,
                CreatorName = musician.Name,
                Name = song.Name,
                Plays = song.Plays,
                AlbumId = song.AlbumId
            })
        });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int NumberOfFollowers { get; set; }

            public IEnumerable<SongModel> TopSongs { get; set; }
            public IEnumerable<AlbumModel> TopAlbums { get; set; }
        }

        public class AlbumModel 
        { 
            public int Id { get; set; }
            public string Name { get; set; }
            public long Plays { get; set; }
        }

        public class SongModel 
        { 
            public int Id { get; set; }
            public string CreatorName { get; set; }
            public string Name { get; set; }
            public int AlbumId { get; set; }
            public long Plays { get; set; }
        }
    }
}
